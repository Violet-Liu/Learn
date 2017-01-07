using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QZ.RealTimeRequestLogService
{

    /// <summary>
    /// 请求日志对象
    /// </summary>
    public class RequestLog
    {

        /// <summary>
        /// 创建一个新的实时日志对象
        /// </summary>
        public RequestLog()
        {
            Time = DateTime.Now;
            Seed = RealTimeRequestLog.NewSeed();
            Gid = Guid.NewGuid().ToString().Replace("-","");
        }

        /// <summary>
        /// 自增种子
        /// </summary>
        public int Seed { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 请求代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 全路径URL
        /// </summary>
        public string FullUrl { get; set; }


        /// <summary>
        /// 请求IP地址
        /// </summary>
        public string Ip { get; set; }


        /// <summary>
        /// 请求表单内容
        /// </summary>
        public string Form { get; set; }


      
        /// <summary>
        /// code
        /// </summary>
        public int ResponseStatusCode { get; set; }


        /// <summary>
        /// status tr
        /// </summary>
        public string ResponseStatusStr { get; set; }


        /// <summary>
        /// 响应头
        /// </summary>
        public string ResponseHeader { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        public string ResponseContentType { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 请求头内容
        /// </summary>
        public string Headers { get; set; }


        /// <summary>
        /// url 模板
        /// </summary>
        public string UrlTemplate { get; set; }


        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求开始
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 总共发费时间 ms
        /// </summary>
        public int TotalSpend { get; set; }

        /// <summary>
        /// Gid
        /// </summary>
        public string Gid { get; set; }

    }
}
