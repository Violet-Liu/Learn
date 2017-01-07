using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Foundation.Utility;

namespace QZ.Service.Enterprise
{
    public class Constructor
    {
        #region error response
        /// <summary>
        /// create a response related to client cipher key error
        /// </summary>
        /// <returns></returns>
        public static Response Create_KeyErr_Response()
        {
            var head = new Response_Head(Message_Action.Sys_Err, "请重新操作");
            var body = string.Empty;
            return new Response(head.ToJson().ToEncryption(EncryptType.PT | EncryptType.ResetKey), 
                body.ToJson().ToEncryption(EncryptType.PT));
        }

        /// <summary>
        /// create a response related to token error
        /// </summary>
        /// <returns></returns>
        public static Response Create_TokErr_Response()
        {
            var head = new Response_Head(Message_Action.Login);
            var body = string.Empty;
            return new Response(head.ToJson().ToEncryption(EncryptType.PT), body.ToJson().ToEncryption(EncryptType.PT));
        }

        public static Response Create_BlackErr_Response()
        {
            var head = new Response_Head(Message_Action.Login);
            var body = "你的账号异常，已被列入黑名单，如有疑问请联系我们客服QQ1713694365";
            return new Response(head.ToJson().ToEncryption(EncryptType.PT), body.ToJson().ToEncryption(EncryptType.PT));
        }
        #endregion



        #region database entity
        public static LogError Create_LogError(Exception e)
        {
            var log = new LogError();
            log.err_referrer = WebOperationContext.Current.IncomingRequest.UserAgent ?? string.Empty;
            log.err_source = e.Source;
            log.err_time = DateTime.Now;
            log.err_stack = e.StackTrace;
            log.err_type = "error log";
            log.err_url = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString;
            log.err_ip = Util.Get_RemoteIp();
            log.err_message = e.Message;
            var es = e as ExceptionServer;
            log.err_guid = $"{DateTime.Now.ToString("yyyyMMdd")}-{Cipher_Md5.Md5_16(Guid.NewGuid().ToString())}";
            if (es != null)
                log.err_user = es.u_id;
            else
                log.err_user = "";
            return log;
        }

        public static LogError Create_TestLog(string input, string input2, string userid)
        {
            var log = new LogError();
            log.err_stack = input;
            log.err_referrer = WebOperationContext.Current.IncomingRequest.UserAgent ?? string.Empty;
            log.err_source = "";
            log.err_time = DateTime.Now;
            log.err_type = "error log";
            log.err_url = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString;
            log.err_ip = Util.Get_RemoteIp();
            log.err_message = input2 ?? "";

                log.err_user = userid;
            log.err_guid = $"{DateTime.Now.ToString("yyyyMMdd")}-{Cipher_Md5.Md5_16(Guid.NewGuid().ToString())}";
            return log;
        }
        #endregion
    }
}
