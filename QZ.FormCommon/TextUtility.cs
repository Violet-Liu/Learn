using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace QZ.FormCommon
{
    /// <summary>
    /// 控制台输出对象
    /// </summary>
    public class TextUtility
    {
        private object o = new object();
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rtb"></param>
        public TextUtility(RichTextBox rtb)
        {
            RichTextBox = rtb;   
        }

        #region 文本控制

        private RichTextBox _rtb;
        /// <summary>
        /// 设置输出富文本对象
        /// </summary>
        public RichTextBox RichTextBox
        {
            get { return _rtb; }
            set { _rtb = value; }
        }

        /// <summary>
        /// 添加字符串
        /// </summary>
        /// <param name="msg"></param>
        public void AppendText(string msg)
        {
            if (RichTextBox == null||RichTextBox.IsDisposed)
                return;
           
            lock (o)
            {
                //Thread.Sleep(2);
               
                RichTextBox.AppendText(GetMsgStr(msg));
            }
        }

        /// <summary>
        /// 检查是否需要清除日志
        /// </summary>
        /// <param name="maxLines"></param>
        public void CheckAndClear(int maxLines)
        {
            lock (o)
            {
                if (_rtb.Lines.Length > maxLines)
                    _rtb.Clear();
            }
        }

        /// <summary>
        /// 添加字符串，带颜色
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        public void AppendText(string msg, Color color)
        {
            if (RichTextBox == null||RichTextBox.IsDisposed)
                return;
         
            lock (o)
            {
                
                //Thread.Sleep(2);
                RichTextBox.SelectionColor = color;
                RichTextBox.SelectionStart = RichTextBox.TextLength;
              //  string str = GetMsgStr(msg);
                RichTextBox.SelectionLength = msg.Length;
                RichTextBox.AppendText(msg);
            }
        }

        /// <summary>
        /// 添加蓝色的消息
        /// </summary>
        /// <param name="msg"></param>
        public void AppendMessage(string msg)
        {
            AppendText(msg, Color.DeepSkyBlue);
        }

        /// <summary>
        /// 添加一行蓝色消息
        /// </summary>
        /// <param name="msg"></param>
        public void AppendLineMessage(string msg)
        {
            AppendText(this.GetMsgStr(msg), Color.DeepSkyBlue);
        }

        /// <summary>
        /// 添加一行蓝色消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="param"></param>
        public void AppendLineMessage(string msg, params object[] param)
        {
            if (param == null || param.Length < 1)
                AppendLineMessage(msg);
            AppendLineMessage(string.Format(msg, param));
        }

        /// <summary>
        /// 添加一行蓝色消息
        /// </summary>
        /// <param name="msg">小象</param>
        /// <param name="param"></param>
        public void AppendFormat(string msg,params object[] param)
        {
            AppendText(string.Format(msg,param),Color.DeepSkyBlue);
        }


        /// <summary>
        /// 添加异常信息，红色
        /// </summary>
        /// <param name="msg"></param>
        public void AppendExceptionMessage(string msg)
        {
            AppendText(msg, Color.Red);
        }

        /// <summary>
        /// 添加一行异常信息，红色
        /// </summary>
        /// <param name="msg"></param>
        public void AppendLineExceptionMessage(string msg)
        {
            AppendText(msg + "\r\n", Color.Red);
        }

        /// <summary>
        /// 添加异常信息， 红色
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="ex">异常对象</param>
        public void AppendLineExceptionMessage(string msg, Exception ex)
        {
            this.AppendLineExceptionMessage(this.GetMsgStr(string.Format("{0} 异常详细：{1}{2}", msg, ex.Message, ex.StackTrace)));
        }

        /// <summary>
        /// 添加换行符
        /// </summary>
        public void AppendLineBreadk()
        {
            this._rtb.AppendText("\r\n");
        }

        /// <summary>
        /// 组装一个消息语句
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetMsgStr(string msg)
        {
            return DateTime.Now.ToString() + " 线程：{" + Thread.CurrentThread.Name + "} " + msg + " \r\n";
        }
        #endregion

    }
}
