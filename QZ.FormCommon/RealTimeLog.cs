using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QZ.FormCommon
{

    /// <summary>
    /// 实时向磁盘写入的日志类
    /// </summary>
    public class RealTimeLog
    {

        /// <summary>
        /// 静态加载
        /// </summary>
        static RealTimeLog()
        {
            InternalCheckDir();
        }


        /// <summary>
        /// 检查文件夹
        /// </summary>
        private static void InternalCheckDir()
        {
            string dir = BaseDir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <summary>
        /// 获取日志保存文件夹路径
        /// </summary>
        public static string BaseDir
        {
            get {
                return string.Format("{0}{1}\\"
                    , AppDomain.CurrentDomain.BaseDirectory
                    , "log"
                    );
            }
        }


        /// <summary>
        /// 日志名称
        /// </summary>
        public string name;

        /// <summary>
        /// 日志类型
        /// </summary>
        public string type;

        /// <summary>
        /// 锁
        /// </summary>
        object o;


        /// <summary>
        /// 初始化一个错误日志
        /// </summary>
        /// <param name="name">唯一日志名称</param>
        /// <param name="type">日志类型，如错误、普通、警告</param>
        public RealTimeLog(string name,string type)
        {
            this.name = name;
            this.type = type;
            o = new object();
        }


        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetFileName(string name,string type)
        {
            return string.Format("{0}{3}_{1}_{2}.txt",BaseDir,name,DateTime.Now.ToString("yyyy-MM-dd"),type);
        }


        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="message">消息</param>
        public void WriteLog(string message)
        {
            InternalWrite(InternalGetMessage(message));
        }


        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        public void WriteLog(string message, Exception ex)
        {
            InternalWrite(InternalGetMessage(message, ex));
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message">消息</param>
        private void InternalWrite(string message)
        {
            string filepath = GetFileName(this.name,this.type);
            lock (o)
            {
                InternalWriteText(filepath, message);
            }
        }

        /// <summary>
        /// 写入文本日志
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="message">要写入的日志</param>
        private static void InternalWriteText(string filepath,string message)
        {
            System.IO.File.AppendAllText(filepath, message, Encoding.UTF8);
        }



        /// <summary>
        /// 获取一条消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string InternalGetMessage(string message)
        {
            return string.Format("{0}\t{1}\r\n",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),message);
        }

        /// <summary>
        /// 获取一条异常组装消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常对象</param>
        /// <returns></returns>
        private static string InternalGetMessage(string message, Exception ex)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(InternalGetMessage(message));
            sb.AppendLine("异常信息：");

            Exception curr = ex;
            while (curr != null)
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                curr = curr.InnerException;
            }
            return sb.ToString();
        }


    }
}
