using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Bogus;
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Log of application error
    /// </summary>
    [DbEntity]
    public class LogError
    {
        [Id]
        public int err_id { get; set; }

        /// <summary>
        /// 错误唯一编号
        /// </summary>
        public string err_guid { get; set; }

        /// <summary>
        /// err_type
        /// </summary>
        public string err_type { get; set; }

        /// <summary>
        /// err_message
        /// </summary>
        public string err_message { get; set; }

        /// <summary>
        /// err_source
        /// </summary>
        public string err_source { get; set; }

        /// <summary>
        /// err_stack
        /// </summary>
        public string err_stack { get; set; }

        /// <summary>
        /// err_url
        /// </summary>
        public string err_url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string err_ip { get; set; }

        /// <summary>
        /// err_referrer
        /// </summary>
        public string err_referrer { get; set; }

        /// <summary>
        /// err_user
        /// </summary>
        public string err_user { get; set; }

        /// <summary>
        /// err_time
        /// </summary>
        public DateTime err_time { get; set; }

        /// <summary>
        /// 累计毫秒数
        /// </summary>
        public int err_totalMilliseconds { get; set; }

        #region test
        public static Faker<LogError> Generator { get; } =
            new Faker<LogError>()
            .RuleFor(p => p.err_guid, p => $"{DateTime.Now.ToString("yyyyMMdd")}-{Cipher_Md5.Md5_16(Guid.NewGuid().ToString())}")
            .RuleFor(p => p.err_type, p => "error log")
            .RuleFor(p => p.err_message, p => "this log is generated at test environment")
            .RuleFor(p => p.err_source, p => Environment.Version.ToString())
            .RuleFor(p => p.err_stack, p => "simulate stack information at error time")
            .RuleFor(p => p.err_url, p => "System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString")
            .RuleFor(p => p.err_ip, p => "localhost")
            .RuleFor(p => p.err_referrer, p => "System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent")
            .RuleFor(p => p.err_user, p => Dns.GetHostName())
            .RuleFor(p => p.err_time, p => DateTime.Now);

        public static IList<LogError> ErrorLogs { get; } =
            Generator.Generate(2).ToList();
        #endregion
    }
}
