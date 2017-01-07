using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Common
{
    public class LogHelper
    {
        protected static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(LogHelper));

        static LogHelper()
        {
            string log4netConfigSource = ConfigurationManager.AppSettings["alipay_log"];
            string filename = "";
            if (!string.IsNullOrEmpty(log4netConfigSource))
            {
                filename = AppDomain.CurrentDomain.BaseDirectory + log4netConfigSource;
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(filename));
            }
            else
                throw new Exception("应用系统日志配置文件配置错误！");
        }

        public static void Error(string msg, Exception ex)
        {
            Logger.Error("\r\n===========================\r\n" + msg, ex);
        }

        public static void Info(string msg)
        {
            //Logger.Info("\r\n===========================\r\n" + msg);
            Logger.Info(msg);
        }
    }
}
