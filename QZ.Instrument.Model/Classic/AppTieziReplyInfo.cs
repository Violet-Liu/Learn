/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class AppTeiziReplyInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AppTeiziReplyInfo()
        {
            Dispose(false);
        }

        /// <summary>
        /// 调用虚拟的Dispose方法, 禁止Finalization（终结操作）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 虚拟的Dispose方法
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }
        #endregion

        #region 自动编号[atr_id]
        int _atr_id;
        /// <summary>
        /// 自动编号[atr_id]
        /// </summary>
        public int atr_id
        {
            get { return _atr_id; }
            set { _atr_id = value; }
        }
        #endregion

        #region 帖子编号[atr_teizi]
        int _atr_teizi;
        /// <summary>
        /// 帖子编号[atr_teizi]
        /// </summary>
        public int atr_teizi
        {
            get { return _atr_teizi; }
            set { _atr_teizi = value; }
        }
        #endregion

        #region 用户名[atr_u_name]
        string _atr_u_name;
        /// <summary>
        /// 用户名[atr_u_name]
        /// </summary>
        public string atr_u_name
        {
            get { return _atr_u_name; }
            set { _atr_u_name = value; }
        }
        #endregion

        #region 用户ID[atr_u_uid]
        int _atr_u_uid;
        /// <summary>
        /// 用户ID[atr_u_uid]
        /// </summary>
        public int atr_u_uid
        {
            get { return _atr_u_uid; }
            set { _atr_u_uid = value; }
        }
        #endregion

        #region 回复内容[atr_content]
        string _atr_content;
        /// <summary>
        /// 回复内容[atr_content]
        /// </summary>
        public string atr_content
        {
            get { return _atr_content; }
            set { _atr_content = value; }
        }
        #endregion

        #region 回复日期[atr_date]
        DateTime _atr_date;
        /// <summary>
        /// 回复日期[atr_date]
        /// </summary>
        public DateTime atr_date
        {
            get { return _atr_date; }
            set { _atr_date = value; }
        }
        #endregion

        #region 状态[atr_status]
        int _atr_status;
        /// <summary>
        /// 状态[atr_status]
        /// </summary>
        public int atr_status
        {
            get { return _atr_status; }
            set { _atr_status = value; }
        }
        #endregion


        #region 友好时间[FriendlyTime]
        string _FriendlyTime;
        /// <summary>
        ///友好时间[FriendlyTime]
        /// </summary>
        public string FriendlyTime
        {
            get { return _FriendlyTime; }
            set { _FriendlyTime = value; }
        }
        #endregion

        public List<string> list { get; set; }
    }
}
