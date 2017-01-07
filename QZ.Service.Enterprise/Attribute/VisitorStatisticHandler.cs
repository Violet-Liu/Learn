/********************************************************
 *
 *  File:   VisitorStatisticHandler.cs
 * 
 *  Class:  VisitorStatisticHandler
 * 
 *  Description:
 * 
 *  Author: Sha Jianjian
 * 
 *  Create: 2016/5/31 20:38:54
 * 
 *  Copyright(c) 2016 深圳前瞻资讯股份有限公司 all rights reserved
 * 
 *  Revision history:
 *      R1:
 *          修改作者：   
 *          修改日期：   
 *          修改理由：   
 *                                                 
 *
 ********************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using QZ.Instrument.Utility;
using System.Reflection;
using QZ.Instrument.Common;

namespace QZ.Service.Enterprise
{
    public class VisitorStatisticHandler : IDispatchMessageInspector, IClientMessageInspector
    {
        public static VisitorCounter counter;

        /// <summary>
        /// 是否调试
        /// </summary>
        public static bool DEBUG;

        static VisitorStatisticHandler()
        {
            counter = new VisitorCounter(null);
            DEBUG = System.Configuration.ConfigurationManager.AppSettings["Debug"].ToLower() == "true";
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // www-form
            try
            {
                ContentTransform(ref request);
            }
            catch (Exception ex)
            {
                LogHelper.Info(ex.Message+ex.StackTrace);
            }
            if (DEBUG)
            {
                QZ.RealTimeRequestLogService.RequestLog log = LogInccomingRequest(request);
                return log;
            }

            var res = Util.Get_RemoteIp().ToMaybe().Do(ip => counter.AddRec(ip))
                        .Select<VisitorCounter.IpInfo, string>(
                            ip => counter.GetIpInfo(ip, false).ToMaybe(),
                            (ip, info) =>
                            {
                                if (counter.IsMechineIpInfo(info))
                                {
                                    counter.AddRecCount(ip, 0x1000);
                                }
                                if (!counter.IsIpAuthorized(ip))
                                {
                                    if (VisitorCounter.GetReqPath().ToLower() == "orgcompany/branch/select/page")
                                        throw new WebFaultException<string>(string.Format("{0} Unauthorized!", ip), System.Net.HttpStatusCode.Unauthorized);
                                }
                                return string.Empty;
                            }
                            );

            

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (DEBUG && correlationState != null)
            {

                QZ.RealTimeRequestLogService.RequestLog rt = (QZ.RealTimeRequestLogService.RequestLog)correlationState;
                TimeSpan ts = DateTime.Now - rt.Time;
                rt.TotalSpend = ts.Milliseconds;

                if (reply != null)
                {
                    MessageProperties mp = reply.Properties;

                    object obj;

                    if (mp.TryGetValue("httpResponse", out obj))
                    {
                        System.ServiceModel.Channels.HttpResponseMessageProperty wh = (System.ServiceModel.Channels.HttpResponseMessageProperty)obj;
                        rt.ResponseHeader = wh.Headers.ToString();
                        rt.ResponseContentType = wh.Headers[System.Net.HttpResponseHeader.ContentType];
                        rt.ResponseStatusCode = (int)wh.StatusCode;
                        rt.ResponseStatusStr = wh.StatusCode.ToString();
                    }

                    if (rt.ResponseStatusCode == 200)
                    {
                        rt.Response = GetReponseBody(reply);
                    }
                    else
                    {
                        rt.Response = string.Empty;
                    }
                }
                else
                {
                    rt.ResponseStatusStr = "no reply message captured, unable to read the response infomation.";

                }
                QZ.RealTimeRequestLogService.RealTimeRequestLog.AppendLog(rt);
            }
        }

        /// <summary>
        /// Check if ip is authorized
        /// </summary>
        /// <returns></returns>
        public static bool AuthorizeIp()
        {
            return true;
            //return Util.Get_RemoteIp().ToMaybe().Select(ip => counter.GetIpInfo(ip, true).ToMaybe(), (ip, info) => counter.IsIpAuthorized(ip)).Value;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            //throw new NotImplementedException();
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //throw new NotImplementedException();
        }



        public Message ContentTransform(ref Message request)
        {
            const string action = "Vip_Order_AliPayNotify";

            // Only process action requests for now
            var operationName = request.Properties["HttpOperationName"] as string;
            if (operationName != action)
            {
                return null;
            }

            // Check that the content type of the request is set to a form post, otherwise do no more processing
            var prop = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            var contentType = prop.Headers["Content-Type"];
            if (!contentType.StartsWith("application/x-www-form-urlencoded"))
            {
                return null;
            }

            ///////////////////////////////////////
            // Build the body from the form values
            string body;

            // Retrieve the base64 encrypted message body
            using (var ms = new System.IO.MemoryStream())
            {
                using (var xw = System.Xml.XmlWriter.Create(ms))
                {
                    request.WriteBody(xw);
                    xw.Flush();
                    body = Encoding.UTF8.GetString(ms.ToArray());
                }
            }

            LogHelper.Info(body);
            // Trim any characters at the beginning of the string, if they're not a <
            body = TrimExtended(body);
            LogHelper.Info(body);
            // Grab base64 binary data from <Binary> XML node
            var doc = System.Xml.Linq.XDocument.Parse(body);
            if (doc.Root == null)
            {
                // Unable to parse body
                return null;
            }

            var node = doc.Root.Elements("Binary").FirstOrDefault();
            if (node == null)
            {
                // No "Binary" element
                return null;
            }

            // Decrypt the XML element value into a string
            var bodyBytes = Convert.FromBase64String(node.Value);
            var bodyDecoded = Encoding.UTF8.GetString(bodyBytes);

            // Deserialize the form request into the correct data contract

           // Console.Write(bodyDecoded);


            //System
            //var qss = new System.Web.HttpUtility. QueryStringSerializer();
            //var newContract = qss.Deserialize<MyServiceContract>(bodyDecoded);

            //// Form the new message and set it
            var newMessage = Message.CreateMessage(OperationContext.Current.IncomingMessageVersion, action, bodyDecoded);
            request = newMessage;
            return null;

        }

        /// <summary>
        ///     Trims any random characters from the start of the string. I would say this is a BOM, but it doesn't seem to be.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string TrimExtended(string s)
        {


            int idx = s.IndexOf('<');
            if (idx != -1)
            {
                return s.Substring(idx, s.Length - idx);
            }

            throw new Exception(string.Format("输入格式不正确：{0}",s));


            while (true)
            {
                if (s.StartsWith("<"))
                {
                    // Nothing to do, return the string
                    return s;
                }

                // Replace the first character of the string
                s = s.Substring(1);
                if (!s.StartsWith("<"))
                {
                    continue;
                }
                return s;
            }
        }

        /// <summary>
        /// 生成当前请求日志对象
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public QZ.RealTimeRequestLogService.RequestLog LogInccomingRequest(Message req)
        {
            var oc = System.ServiceModel.OperationContext.Current;
            QZ.RealTimeRequestLogService.RequestLog log = new RealTimeRequestLogService.RequestLog();
            //log.Ip = ip;
            MessageProperties mps = req.Properties;

            object obj;
            if (mps.TryGetValue("Via", out obj))
            {
                Uri uri = (Uri)obj;
                log.Url = uri.PathAndQuery;
                log.FullUrl = uri.OriginalString;
            }

            if (mps.TryGetValue("httpRequest", out obj))
            {
                HttpRequestMessageProperty http = (HttpRequestMessageProperty)obj;
                log.Headers = http.Headers.ToString();
                log.Method = http.Method;
                log.UserAgent = http.Headers[System.Net.HttpRequestHeader.UserAgent];

            }

            if (mps.TryGetValue("System.ServiceModel.Channels.RemoteEndpointMessageProperty", out obj))
            {
                RemoteEndpointMessageProperty ep = (RemoteEndpointMessageProperty)obj;
                log.Ip = string.Format("{0}:{1}", ep.Address, ep.Port);
            }

            if (mps.TryGetValue("UriTemplateMatchResults", out obj))
            {

                UriTemplateMatch utm = (UriTemplateMatch)obj;
                log.UrlTemplate = utm.Template.ToString();

            }

            if (mps.TryGetValue("HttpOperationName", out obj))
            {
                log.ActionName = (string)obj;
            }


            log.Form = GetRequestBody(req);

            return log;
        }


        /// <summary>
        /// 获取原始请求内容
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetRequestBody(Message req)
        {

            if (req.IsEmpty)
                return string.Empty;


            try
            {

                /*
                 System.Encoding.UTF8.GetString(((System.IO.BufferedStream)((System.Runtime.Serialization.Json.JsonEncodingStreamWrapper)((System.Xml.XmlBaseReader)((System.ServiceModel.Channels.StreamedMessage)req).reader).BufferReader.stream).stream)._buffer)
                 */

                object xmlreader = GetPrivateField<object>(req, "reader");
                object bufferReader = GetBaseTypePrivateField<object>(xmlreader, "bufferReader");
                object jsonStream = GetPrivateField<object>(bufferReader, "stream");
                object bufStream = GetPrivateField<object>(jsonStream, "stream");
                byte[] bs = GetPrivateField<byte[]>(bufStream, "_buffer");
                int length = GetPrivateField<int>(bufStream, "_readLen");

                return Encoding.UTF8.GetString(bs, 0, length);




                //System.Xml.XmlBaseReader

                //4.5 之前的数据获取方法
                Type mt = req.GetType();
                Object messageData = mt.InvokeMember("MessageData",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty,
                    null, req, null);

                Type jsonBufferedMessageDataType = messageData.GetType();

                object buffer = jsonBufferedMessageDataType.InvokeMember("Buffer",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty,
                    null, messageData, null);

                ArraySegment<byte> arrayBuffer = (ArraySegment<byte>)buffer;

                return Encoding.UTF8.GetString(arrayBuffer.Array, 0, arrayBuffer.Count);

            }
            catch(Exception ex)
            {
                return ex.Message+ex.StackTrace;
            }
        }


        /// <summary>
        /// get response as base64
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetReponseBody(Message req)
        {
            if (req.IsEmpty || req.IsFault)
                return string.Empty;



            object byteStreamMessage = GetPrivateField<object>(req, "bodyWriter");

            Type messageType = byteStreamMessage.GetType();
            if (messageType.FullName == "System.ServiceModel.Dispatcher.SingleBodyParameterMessageFormatter+SingleParameterBodyWriter")
            {
                try
                {
                    QZ.Service.Enterprise.Response response = GetPrivateField<QZ.Service.Enterprise.Response>(byteStreamMessage, "body");
                    return string.Format("{{\"h\":\"{0}\",\"b\":\"{1}\"}}", response.h, response.b);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }


            object messageData;
            try
            {
                messageData = GetPrivateField<Object>(byteStreamMessage, "stream");
                if (messageData is System.IO.MemoryStream)
                {

                    System.IO.MemoryStream ms = (System.IO.MemoryStream)messageData;
                    byte[] bs = GetPrivateField<byte[]>(ms, "_buffer");
                    int len = GetPrivateField<int>(ms, "_length");

                    string base64Str = Convert.ToBase64String(bs, 0, len);

                    return base64Str;

                }
                else
                {
                    return messageData.GetType().ToString();
                }
            }
            catch
            {
                return "unknown stream, may actionOfStream, capture response content failed";
            }




        }

        /// <summary>
        /// 获取私有成员的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        protected static T GetPrivateField<T>(object instance, string fieldname) { System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic; Type type = instance.GetType(); System.Reflection.FieldInfo field = type.GetField(fieldname, flag); return (T)field.GetValue(instance); }

        /// <summary>
        /// 获取私有成员的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        protected static T GetBaseTypePrivateField<T>(object instance, string fieldname) { System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic; Type type = instance.GetType().BaseType; System.Reflection.FieldInfo field = type.GetField(fieldname, flag); return (T)field.GetValue(instance); }


        /// <summary>
        /// 获取私有成员的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        protected static T GetPublicProperty<T>(object instance, string pName)
        {
            //System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
            Type type = instance.GetType();

            System.Reflection.PropertyInfo p = type.GetProperty(pName);

            //System.Reflection.FieldInfo field = type.GetField(fieldname, flag);
            return (T)p.GetValue(instance);

        }


    }
}
