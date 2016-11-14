/********************************************************
 *
 *  File:   VisitorStaticticBehavior.cs
 * 
 *  Class:  VisitorStaticticBehavior
 * 
 *  Description:
 * 
 *  Author: Sha Jianjian
 * 
 *  Create: 2016/6/1 8:31:17
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
using System.ServiceModel.Description;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace QZ.Service.Enterprise
{
    public class VisitorStaticticBehavior : Attribute, IServiceBehavior, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //foreach (ChannelDispatcher cDispatcher in serviceHostBase.ChannelDispatchers)
            //{
            //    foreach (EndpointDispatcher endpointDispatcher in cDispatcher.Endpoints)
            //    {
            //        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new VisitorStatisticHandler());
            //    }
            //}
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.MessageInspectors.Add(new VisitorStatisticHandler());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //throw new NotImplementedException();
        }
    }
}
