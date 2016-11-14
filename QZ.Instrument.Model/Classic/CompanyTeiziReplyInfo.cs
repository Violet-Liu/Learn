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
    public class CompanyTieziReplyInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyTieziReplyInfo()
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

        #region 自动编号[ctr_id]
        int _ctr_id;
        /// <summary>
        /// 自动编号[ctr_id]
        /// </summary>
        public int ctr_id
        {
            get { return _ctr_id; }
            set { _ctr_id = value; }
        }
        #endregion

        #region 帖子编号[ctr_teizi]
        int _ctr_teizi;
        /// <summary>
        /// 帖子编号[ctr_teizi]
        /// </summary>
        public int ctr_teizi
        {
            get { return _ctr_teizi; }
            set { _ctr_teizi = value; }
        }
        #endregion

        #region 用户名[ctr_u_name]
        string _ctr_u_name;
        /// <summary>
        /// 用户名[ctr_u_name]
        /// </summary>
        public string ctr_u_name
        {
            get { return _ctr_u_name; }
            set { _ctr_u_name = value; }
        }
        #endregion

        #region 用户ID[ctr_u_uid]
        int _ctr_u_uid;
        /// <summary>
        /// 用户ID[ctr_u_uid]
        /// </summary>
        public int ctr_u_uid
        {
            get { return _ctr_u_uid; }
            set { _ctr_u_uid = value; }
        }
        #endregion

        #region 回复内容[ctr_content]
        string _ctr_content;
        /// <summary>
        /// 回复内容[ctr_content]
        /// </summary>
        public string ctr_content
        {
            get { return _ctr_content; }
            set { _ctr_content = value; }
        }
        #endregion

        #region 回复日期[ctr_date]
        DateTime _ctr_date;
        /// <summary>
        /// 回复日期[ctr_date]
        /// </summary>
        public DateTime ctr_date
        {
            get { return _ctr_date; }
            set { _ctr_date = value; }
        }
        #endregion

        #region 状态[ctr_status]
        int _ctr_status;
        /// <summary>
        /// 状态[ctr_status]
        /// </summary>
        public int ctr_status
        {
            get { return _ctr_status; }
            set { _ctr_status = value; }
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

        //public List<TopicImage> list { get; set; }
    }
}
