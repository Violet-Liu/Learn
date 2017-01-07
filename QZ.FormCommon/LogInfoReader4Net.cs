using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QZ.FormCommon
{
    /// <summary>
    /// 网络日志读取器
    /// </summary>
    public class LogInfoReader4Net:LogInfoReader
    {

        /// <summary>
        /// 实例化网络日志读取器
        /// </summary>
        /// <param name="txt"></param>
        public LogInfoReader4Net(TextUtility txt)
            :base(txt,null)
        {
            
        }
        

        /// <summary>
        /// 获取消息委托
        /// </summary>
        /// <returns></returns>
        public delegate List<string> GetMessageDel(int readerIndex,out int writeIndex);

        /// <summary>
        /// 获取消息时间
        /// </summary>
        public event GetMessageDel GetMessageEvent;


        /// <summary>
        /// 不要显示日志消息
        /// </summary>
        public bool DoNotDisplayLog
        {
            get;set;
        }

        /// <summary>
        /// 执行监视
        /// </summary>
        protected override void DoWatch()
        {
            int w=base.r;
            List<string> logList = GetMessageEvent(base.r, out w);
            
            if (!DoNotDisplayLog && logList != null)
            {
                foreach (string msg in logList)
                {
                   
                    base.AppendMsg(msg);
                }
            }
            base.r = w;
            //base.DoWatch();
        }





        

    }
}
