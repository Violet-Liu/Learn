using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QZ.FormCommon
{
    /// <summary>
    /// 日志读取器
    /// </summary>
    public class LogInfoReader
    {
        /// <summary>
        /// 退出信号
        /// </summary>
        protected bool _exit;

        /// <summary>
        /// 挂起
        /// </summary>
        protected bool _suppend;

        /// <summary>
        /// 日志访问器
        /// </summary>
        protected LogInfo _logInfo;

        /// <summary>
        /// 控制台
        /// </summary>
        protected TextUtility _txt = null;

        /// <summary>
        /// reader index
        /// </summary>
        protected int r;


        protected int timer = 100;
        /// <summary>
        /// 监控间隔时间
        /// </summary>
        public int Timer
        {
            get{return timer;}
            set{timer=value;}
        }

        /// <summary>
        /// 获取读取器挂起状态
        /// </summary>
        public bool Suppend
        {
            get {
                return _suppend;
            }
        }

        /// <summary>
        /// 获取读取器是否已退出
        /// </summary>
        public bool Exited
        {
            get {
                return _exit;
            }
        }

        /// <summary>
        /// 日志读取器
        /// </summary>
        /// <param name="txt">控制台</param>
        /// <param name="log">日志对象</param>
        public LogInfoReader(TextUtility txt, LogInfo log)
        {
            this._txt = txt;
            this._logInfo = log;
        }

        /// <summary>
        /// 监视器
        /// </summary>
        public void StartWatcher()
        {
            Thread.Sleep(100);
            while (!CheckSuppend())
            {
                this.DoWatch();
                Thread.Sleep(Timer);
            }
            //Thread.Sleep(100 * 2);
            _txt.AppendExceptionMessage("输出被终止，无法继续！ ");
        }

        /// <summary>
        /// 执行监控
        /// </summary>
        protected virtual void DoWatch()
        {
            int w = _logInfo.WriteIndex;
            //int d = w - r;
            for (int i = r; i <= w; i++)
            {
                string msg = _logInfo.GetMsg(i);
               
                AppendMsg(msg);
            }
            r = w;
        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="msg"></param>
        public void AppendMsg(string msg)
        {

            if (msg == null || msg.Length == 0)
                return;
            if (_exit)
                return;
            //string head = msg.Substring(0, 3);
            //string body = msg.Substring(3);
            if (msg.StartsWith("#m#"))
                _txt.AppendMessage(msg.Substring(3));
            else if (msg.StartsWith("#e#"))
                _txt.AppendExceptionMessage(msg.Substring(3));
            else
                _txt.AppendText(msg);
        }



        //private void SuppendWatch

        protected bool CheckSuppend()
        {
            if (_suppend)
            {
                _txt.AppendLineMessage("输出挂起");
                while(_suppend)
                    Thread.Sleep(250);
                _txt.AppendLineMessage("输出恢复");
            }
            return _exit;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            _exit = true;
            if (_suppend)
                _suppend = false;
            
        }

        /// <summary>
        /// 设置为挂起或者继续
        /// <returns>挂起返回TRUE，继续返回FALSE</returns>
        /// </summary>
        public bool SetSuppendOrContinue()
        {
            _suppend = !_suppend;
            return _suppend;
        }
    }
}
