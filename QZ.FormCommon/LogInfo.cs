using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QZ.FormCommon
{
    /// <summary>
    /// 控制台信息
    /// </summary>
    [Serializable]
    public class LogInfo
    {

        /// <summary>
        /// 静态地址
        /// </summary>
        public static string logBaseDir;

        /// <summary>
        /// 是否产生日志文件
        /// </summary>
        public static bool noLogFile;

        /// <summary>
        /// 静态加载
        /// </summary>
        static LogInfo()
        {
            logBaseDir = AppDomain.CurrentDomain.BaseDirectory + "log\\";
            if (!System.IO.Directory.Exists(logBaseDir))
            {
                System.IO.Directory.CreateDirectory(logBaseDir);
            }
            noLogFile = System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory+"noLog.nolog");
        }

        /// <summary>
        /// 日志名
        /// </summary>
        public string LogName
        {
            get;
            set;
        }

        /// <summary>
        /// 实例化日志记录器
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="capacity">最大日志条数</param>
        public LogInfo(string logName, int capacity):this(capacity,1000)
        {
            this.LogName = logName;
        }

        /// <summary>
        /// 实例化日志记录器
        /// </summary>
        /// <param name="logName">日志名称</param>
        /// <param name="capacity">最大日志条数</param>
        /// <param name="remarkSize">最大备注字数</param>
        public LogInfo(string logName, int capacity, int remarkSize)
            :this(capacity,remarkSize)
        {
            this.LogName = logName;
        }


        /// <summary>
        /// 实例化一个日志记录器
        /// </summary>
        /// <param name="capacity">初始化条数,超过此条数，日志自动清除</param>
        public LogInfo(int capacity):this(capacity,1000)
        {
            
        }

        /// <summary>
        /// 实例化一个日志记录器
        /// </summary>
        /// <param name="capacity">初始化条数,超过此条数，日志自动清除</param>
        /// <param name="remarkSize">最大记录备注</param>
        public LogInfo(int capacity, int remarkSize)
        {
            this.RemarkSize = remarkSize;
            _sbLog = new StringBuilder(this.RemarkSize);
            this._info = new List<string>(capacity);
            Capacity = capacity;
        }

        /// <summary>
        /// 重要日志备注大小
        /// </summary>
        public int RemarkSize
        {
            get;
            set;
        }
        private int _readIndex;
        //锁对象
        protected object o = new object();

        /// <summary>
        /// 保存日志的最大条数
        /// </summary>
        public int Capacity
        {
            get;
            set;
        }

        /// <summary>
        /// 字增备注文件名
        /// </summary>
        private int fileNameIdentity;

        /// <summary>
        /// 读取索引
        /// </summary>
        public int ReadIndex
        {
            get { lock (o) { return _readIndex; } }
            //set { lock (o) _readIndex = value; }
        }

        private int _writeIndex;
        /// <summary>
        /// 写入索引
        /// </summary>
        public int WriteIndex
        {
            get { lock (o) { return _writeIndex; } }
            //set { lock(o)_writeIndex = value; }
        }

        /// <summary>
        /// 重置
        /// </summary>
        protected void ReSet()
        {
            _readIndex = 0;
            _writeIndex = 0;
        }

        protected List<string> _info;

        //日志
        protected  StringBuilder _sbLog;

        /// <summary>
        /// 得到错误日志
        /// </summary>
        public virtual string SbLog
        {
            get {
                string log = _sbLog.ToString();
                if (log.Length > RemarkSize)
                    log = log.Substring(0, RemarkSize);
                return log;
            }
        }

        /// <summary>
        /// 得到一条消息
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public string GetMsg(int readIndex)
        {
            lock (o)
            {
                if (readIndex < _info.Count)
                {
                    _readIndex = readIndex;
                    return _info[readIndex];
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取下一条日志
        /// </summary>
        /// <returns></returns>
        public string NextMsg()
        {
            lock (o)
            {

                if (_readIndex < _info.Count)
                {
                    string msg = _info[_readIndex];
                    _readIndex++;
                    return msg;
                    //return _info[_readIndex];
                }

                //_readIndex = 0;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <returns></returns>
        public List<string> NextMsgList()
        {
            lock (o)
            {
                if (_readIndex < _info.Count)
                {
                    List<string> lst = _info.GetRange(_readIndex, _info.Count - _readIndex);
                    _readIndex = _info.Count;
                    return lst;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="readerIndex">读取索引</param>
        /// <param name="writeIndex">写入索引</param>
        /// <returns>日志集合</returns>
        public List<string> NextMsgList(int readerIndex, out int writeIndex)
        {
            lock (o)
            {
                writeIndex = _info.Count;
                if (readerIndex < _info.Count)
                {
                    _readIndex = readerIndex;
                    if(_info.Count>readerIndex)
                        return _info.GetRange(readerIndex, _info.Count - readerIndex);
                    return null;
                }
                else if (readerIndex > _info.Count)
                {
                    _readIndex = readerIndex = 0;
                    if (_info.Count > 0)
                    {
                        return _info.GetRange(readerIndex, _info.Count - readerIndex);
                    }
                    return null;
                }
                else
                {
                    return null;
                }
                
            }
        }

        /// <summary>
        /// 重置读取索引
        /// </summary>
        /// <returns></returns>
        public void ResetReadIndex()
        {
            lock (o)
                _readIndex = 0;
        }


        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg"></param>
        public virtual void AppendMessage(string msg)
        {
            msg = GetMsgStr(msg);
            lock (o)
            {
                
                AddInfo(string.Format("#m#{0}",msg));
                
            }
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="parmas">参数</param>
        public virtual void AppendFormat(string msg,params object[] parmas )
        {
            if (parmas == null || parmas.Length < 1)
            {
                AppendMessage(msg);
                return;
            }
            this.AppendMessage(string.Format(msg, parmas));
        }

        /// <summary>
        /// 添加异常
        /// </summary>
        /// <param name="msg"></param>
        public virtual void AppendExceptionMessage(string msg,Exception ex)
        {
            msg = GetMsgStr(string.Format("{0} 异常信息：{1}  {2}  {3}",msg,ex.Message,ex.StackTrace,ex.Source));
            lock (o)
            {
                AddInfo(string.Format("#e#{0}",msg));
            }
        }

        public virtual void AppendExceptionMessage(string msg, string ex)
        {
            msg = GetMsgStr(string.Format("{0} 异常信息：{1}", msg, ex));
            lock (o)
            {
                AddInfo(string.Format("#e#{0}", msg));
            }
        }

        /// <summary>
        /// 添加异常，并添加到日志
        /// </summary>
        /// <param name="msg"></param>
        public virtual void AppendExceptionMessageV2(string msg, Exception ex)
        {
            string inf = string.Format("{0} 异常信息：{1}  {2}  {3}", msg, ex.Message, ex.StackTrace, ex.Source);
            string tmp = msg;
            msg = GetMsgStr(inf);
            lock (o)
            {
                if (_sbLog.Length <= RemarkSize)
                {
                    _sbLog.AppendLine(string.Format("{0} 异常信息：{1}", tmp, ex.Message));
                }
                AddInfo(string.Format("#e#{0}", msg));
                
            }
        }

        /// <summary>
        /// 添加异常，并添加到日志  
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public virtual void AppendExceptionMessageV3(string msg, Exception ex)
        {
            string inf = string.Format("{0} 异常信息：{1}  {2}  {3}", msg, ex.Message, ex.StackTrace, ex.Source);
            string tmp = msg;
            msg = this.GetMsgStrV2(inf);
            lock (o)
            {
                if (_sbLog.Length <= RemarkSize)
                {
                    _sbLog.AppendLine(string.Format("{0} 异常：{1}", tmp, ex.Message));
                }
                AddInfo(string.Format("#e#{0}", msg));

            }
        }

        /// <summary>
        /// 添加异常，并记录详细日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public virtual void AppendExceptionMessageV4(string msg, Exception ex)
        {
            msg = GetMsgStr(string.Format("{0} 异常信息：{1}  {2}  {3}", msg, ex!=null?ex.Message:string.Empty, ex!=null?ex.StackTrace:string.Empty,ex!=null? ex.Source:string.Empty));
            InternalAppendExceptionMessage(msg);
        }

        /// <summary>
        /// 添加异常，并记录详细日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errMsg"></param>
        public virtual void AppendExceptionMessageV4(string msg, string errMsg)
        {
            msg = GetMsgStr(string.Format("{0} 异常信息：{1}",msg,errMsg));
            InternalAppendExceptionMessage(msg);

        }

        /// <summary>
        /// 添加异常信息
        /// 记录到日志
        /// </summary>
        /// <param name="msg"></param>
        private void InternalAppendExceptionMessage(string msg)
        {
            lock (o)
            {

                CheckRemarkLog();
                _sbLog.AppendLine(msg);
                AddInfo(string.Format("#e#{0}", msg));
            }
        }



        /// <summary>
        /// 检查备注日志
        /// </summary>
        private void CheckRemarkLog()
        {
            if (_sbLog.Length > RemarkSize)
            {
                SaveLog();
                _sbLog.Remove(0, _sbLog.Length);
                _sbLog.AppendLine("日志已保存到程序跟目录");
            }
        }


        /// <summary>
        /// 添加日志备注
        /// </summary>
        /// <param name="msg"></param>
        public void AppendRemark(string msg)
        {
            lock (o)
            {
                CheckRemarkLog();
                this._sbLog.Append(GetMsgStr(msg));
            }
        }

        /// <summary>
        /// 添加日志备注 备注不包括精确时间和线程名
        /// </summary>
        /// <param name="msg"></param>
        public void AppendRemarkV2(string msg)
        {
            lock (o)
            {
                this._sbLog.Append(GetMsgStrV2(msg));
            }
        }

        /// <summary>
        /// 组装一个消息语句
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected string GetMsgStr(string msg)
        {
            return string.Format("{0} {{{1}}} {2} \r\n", DateTime.Now.ToString(), Thread.CurrentThread.Name, msg);
        }

        /// <summary>
        /// 组装一条消息语句  时间只包括 时、分、秒
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected string GetMsgStrV2(string msg)
        {
            return string.Format("{0} {1}\r\n",DateTime.Now.ToString("HH:mm:ss"),msg);
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="msg"></param>
        public virtual void AddInfo(string msg)
        {

            if (_info.Count+1>Capacity)
            {
                _info.Clear();
                ReSet();

                this._info.Add(string.Format("#e#日志记录已经超过{0}行，依据设定，清除日志\r\n", _info.Capacity));
                 _writeIndex++;
            }
            this._info.Add(msg);
            _writeIndex++;

        }

        /// <summary>
        /// 保存备注日志
        /// </summary>
        public void SaveLog()
        {
            if (noLogFile)
                return;
            try
            {
               
                using (System.IO.FileStream fs = new System.IO.FileStream(GetFileName(), System.IO.FileMode.Create))
                {
                    byte[] bs = Encoding.UTF8.GetBytes(_sbLog.ToString().ToCharArray());
                    fs.Write(bs, 0, bs.Length);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 获取保存备注日志的文件名
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            fileNameIdentity++;
            return string.Format("{4}{5}ExceptionRemark{0}_{1}_{2}_{3}.log",DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,fileNameIdentity,logBaseDir,LogName);
        }


    }
}
