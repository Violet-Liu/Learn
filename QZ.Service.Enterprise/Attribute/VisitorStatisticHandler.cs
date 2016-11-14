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

namespace QZ.Service.Enterprise
{
    public class VisitorStatisticHandler : IDispatchMessageInspector, IClientMessageInspector
    {
        public static VisitorCounter counter;
        static VisitorStatisticHandler()
        {
            counter = new VisitorCounter(null);
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
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
    }
}
