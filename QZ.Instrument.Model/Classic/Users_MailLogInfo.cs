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
    public class Users_MailLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~Users_MailLogInfo()
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

        #region ml_id
        int _ml_id;
        /// <summary>
        /// ml_id
        /// </summary>
        public int ml_id
        {
            get { return _ml_id; }
            set { _ml_id = value; }
        }
        #endregion

        #region ml_uid
        string _ml_uid;
        /// <summary>
        /// ml_uid
        /// </summary>
        public string ml_uid
        {
            get { return _ml_uid; }
            set { _ml_uid = value; }
        }
        #endregion

        #region ml_type
        string _ml_type;
        /// <summary>
        /// ml_type
        /// </summary>
        public string ml_type
        {
            get { return _ml_type; }
            set { _ml_type = value; }
        }
        #endregion

        #region ml_to
        string _ml_to;
        /// <summary>
        /// ml_to
        /// </summary>
        public string ml_to
        {
            get { return _ml_to; }
            set { _ml_to = value; }
        }
        #endregion

        #region ml_toName
        string _ml_toname;
        /// <summary>
        /// ml_toName
        /// </summary>
        public string ml_toName
        {
            get { return _ml_toname; }
            set { _ml_toname = value; }
        }
        #endregion

        #region ml_cc
        string _ml_cc;
        /// <summary>
        /// ml_cc
        /// </summary>
        public string ml_cc
        {
            get { return _ml_cc; }
            set { _ml_cc = value; }
        }
        #endregion

        #region ml_title
        string _ml_title;
        /// <summary>
        /// ml_title
        /// </summary>
        public string ml_title
        {
            get { return _ml_title; }
            set { _ml_title = value; }
        }
        #endregion

        #region ml_content
        string _ml_content;
        /// <summary>
        /// ml_content
        /// </summary>
        public string ml_content
        {
            get { return _ml_content; }
            set { _ml_content = value; }
        }
        #endregion

        #region ml_state
        int _ml_state;
        /// <summary>
        /// ml_state
        /// </summary>
        public int ml_state
        {
            get { return _ml_state; }
            set { _ml_state = value; }
        }
        #endregion

        #region ml_createTime
        DateTime _ml_createtime;
        /// <summary>
        /// ml_createTime
        /// </summary>
        public DateTime ml_createTime
        {
            get { return _ml_createtime; }
            set { _ml_createtime = value; }
        }
        #endregion

        #region ml_createUser
        string _ml_createuser;
        /// <summary>
        /// ml_createUser
        /// </summary>
        public string ml_createUser
        {
            get { return _ml_createuser; }
            set { _ml_createuser = value; }
        }
        #endregion

        #region ml_resend
        int _ml_resend;
        /// <summary>
        /// ml_resend
        /// </summary>
        public int ml_resend
        {
            get { return _ml_resend; }
            set { _ml_resend = value; }
        }
        #endregion

        #region ml_resendRemark
        string _ml_resendremark;
        /// <summary>
        /// ml_resendRemark
        /// </summary>
        public string ml_resendRemark
        {
            get { return _ml_resendremark; }
            set { _ml_resendremark = value; }
        }
        #endregion

        #region ml_from
        string _ml_from;
        /// <summary>
        /// ml_from
        /// </summary>
        public string ml_from
        {
            get { return _ml_from; }
            set { _ml_from = value; }
        }
        #endregion

        #region ml_fromName
        string _ml_fromname;
        /// <summary>
        /// ml_fromName
        /// </summary>
        public string ml_fromName
        {
            get { return _ml_fromname; }
            set { _ml_fromname = value; }
        }
        #endregion

    }
}
