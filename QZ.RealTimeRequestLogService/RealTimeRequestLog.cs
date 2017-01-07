using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QZ.FormCommon;
using fastJSON;

namespace QZ.RealTimeRequestLogService
{
    public class RealTimeRequestLog:IRealTimeRequestLog
    {

        /// <summary>
        /// 自增种子锁
        /// </summary>
        static object so;

        /// <summary>
        /// 自增种子
        /// </summary>
        static int seed;

        /// <summary>
        /// 静态初始日记记录器
        /// </summary>
        static RealTimeRequestLog()
        {
            _log = new LogInfo("hello", 0x1000);
            _jsp= new JSONParameters() { EnableAnonymousTypes = false, UseEscapedUnicode = false, UseOptimizedDatasetSchema = false, UsingGlobalTypes = false, UseExtensions = false };
            so = new object();
        }

        /// <summary>
        /// 日志对象
        /// </summary>
        static LogInfo _log;

        /// <summary>
        /// 静态json设定
        /// </summary>
        static JSONParameters _jsp;


        /// <summary>
        /// 生成一个新的自增种子
        /// </summary>
        /// <returns></returns>
        internal static int NewSeed()
        {
            lock (so)
                return ++seed;
        }



        /// <summary>
        /// 添加一条日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool LogRequest(RequestLog log)
        {
            AppendLog(log);
            return true;
        }

        /// <summary>
        /// 获取请求日志JSON字符串
        /// </summary>
        /// <param name="readerIndex">读索引</param>
        /// <param name="writeIndex">写索引</param>
        /// <returns></returns>
        public List<string> GetRequestLogString(int readerIndex, out int writeIndex)
        {
            return _log.NextMsgList(readerIndex, out writeIndex);
        }


        /// <summary>
        /// 获取请求日志监控
        /// </summary>
        /// <param name="readerIndex">读取索引</param>
        /// <param name="writeIndex">写入索引</param>
        /// <returns></returns>
        public List<RequestLog> GetRequestLogList(int readerIndex, out int writeIndex)
        {
            List<string> logList = GetRequestLogString(readerIndex, out writeIndex);
            List<RequestLog> requestLogList = new List<RequestLog>();
            foreach (string logStr in logList)
            {
                requestLogList.Add(LogStringToRequestLogObject(logStr));
            }
            return requestLogList;
        }


        /// <summary>
        /// 写入一条日志
        /// </summary>
        /// <param name="log"></param>
        public static void AppendLog(RequestLog log)
        {
            _log.AppendMessage(JSON.ToJSON(log, _jsp));
        }

        /// <summary>
        /// 将日志字符串转换为请求对象
        /// </summary>
        /// <param name="logStr"></param>
        /// <returns></returns>
        public static RequestLog LogStringToRequestLogObject(string logStr)
        {
            string rawJson = LogStringToRawJson(logStr);
            return JSON.ToObject<RequestLog>(rawJson);
        }

        /// <summary>
        /// 获取原始json字符串
        /// </summary>
        /// <param name="logStr"></param>
        /// <returns></returns>
        public static string LogStringToRawJson(string logStr)
        {
            int idx = logStr.IndexOf('}') + 2;
            return new string(logStr.ToCharArray(idx, logStr.Length - idx));
        }

    }
}
