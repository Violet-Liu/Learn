using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using QZ.Foundation.Model;

namespace QZ.Service.Enterprise
{
    public class ErrorHandler : IErrorHandler
    {
        /// <summary>
        /// 启用创建从服务方法过程中的异常返回的自定义SOAP错误
        /// </summary>
        /// <param name="error">服务操作过程中引发的异常</param>
        /// <param name="version">消息的 SOAP 版本</param>
        /// <param name="fault">双工情况下，返回到客户端或服务的通信单元对象 </param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message msg)
        {
            /* 
             * 记录错误日志
             * */
            //DbAccess.LogError_Insert(Constructor.Create_LogError(error));
            DataAccess.ErrorLog_Insert(Constructor.Create_LogError(error));

            
        }

        /// <summary>
        ///  启用错误相关处理并返回一个值，该值指示调度程序在某些情况下是否中止会话和实例上下文。
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool HandleError(Exception error)
        {
            // 不终止会话和实例上下文
            return true;
        }
    }
}
