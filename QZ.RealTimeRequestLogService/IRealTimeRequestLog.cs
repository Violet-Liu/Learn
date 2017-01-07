using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace QZ.RealTimeRequestLogService
{

    /// <summary>
    /// 实时日志记录对象
    /// </summary>
    [ServiceContract(Namespace = "http://service.qianzhan.com/", Name = "RealTimeRequestLogService")]
    public interface IRealTimeRequestLog
    {

        /// <summary>
        /// 记录一个请求
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        [OperationContract]
        bool LogRequest(RequestLog log);



        /// <summary>
        /// 获取请求日志
        /// </summary>
        /// <param name="readerIndex">客户端读索引</param>
        /// <param name="writeIndex">服务端写索引</param>
        /// <returns></returns>
        [OperationContract]
        List<string> GetRequestLogString(int readerIndex, out int writeIndex);


        /// <summary>
        /// 获取请求日志对象
        /// </summary>
        /// <param name="readerIndex">客户端读索引</param>
        /// <param name="writeIndex">服务端写索引</param>
        /// <returns></returns>
        [OperationContract]
        List<RequestLog> GetRequestLogList(int readerIndex, out int writeIndex);

    }
}
