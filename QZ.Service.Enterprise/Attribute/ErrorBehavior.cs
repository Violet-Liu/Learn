/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 *
 *
 *
 *
 */

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace QZ.Service.Enterprise
{
    /// <summary>
    ///  WCF服务异常处理机制，服务的行为将默认的异常处理添加到所有通信的调度程序中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ErrorBehavior : Attribute, IEndpointBehavior, IServiceBehavior
    {
        #region 端点行为IEndpointBehavior

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ErrorHandler());
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        #endregion

        #region 服务器行为IServiceBehavior

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 用于更改运行时属性值或插入自定义扩展对象（例如错误处理程序、消息或参数拦截器、安全扩展以及其他自定义扩展对象）
        /// </summary>
        /// <param name="serviceDescription">服务说明</param>
        /// <param name="serviceHostBase">当前正在生成的宿主</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (serviceHostBase != null && serviceHostBase.ChannelDispatchers.Any())
            {
                foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
                {
                    // 从ServiceHostBase宿主中遍历通道调度程序集ChannellDispatchers
                    // 合并在每个ChannelDispatcher中的ErrorHandlers插入自定义异常ErrorHandler,
                    // 这样的话当WCF服务出现异常时就会转交给ErrorHandler处理；
                    channelDispatcher.ErrorHandlers.Add(new ErrorHandler());
                }
            }
        }

        #endregion

    }
}
