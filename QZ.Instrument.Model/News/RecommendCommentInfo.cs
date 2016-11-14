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
    public class RecommendCommentInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~RecommendCommentInfo()
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

        #region rcmt_id
        int _rcmt_id;
        /// <summary>
        /// rcmt_id
        /// </summary>
        public int rcmt_id
        {
            get { return _rcmt_id; }
            set { _rcmt_id = value; }
        }
        #endregion

        #region rcmt_createUser
        string _rcmt_createuser;
        /// <summary>
        /// rcmt_createUser
        /// </summary>
        public string rcmt_createUser
        {
            get { return _rcmt_createuser; }
            set { _rcmt_createuser = value; }
        }
        #endregion

        #region rcmt_createTime
        DateTime _rcmt_createtime;
        /// <summary>
        /// rcmt_createTime
        /// </summary>
        public DateTime rcmt_createTime
        {
            get { return _rcmt_createtime; }
            set { _rcmt_createtime = value; }
        }
        #endregion

        #region cmt_id
        int _cmt_id;
        /// <summary>
        /// cmt_id
        /// </summary>
        public int cmt_id
        {
            get { return _cmt_id; }
            set { _cmt_id = value; }
        }
        #endregion

        #region cmt_uid
        string _cmt_uid;
        /// <summary>
        /// cmt_uid
        /// </summary>
        public string cmt_uid
        {
            get { return _cmt_uid; }
            set { _cmt_uid = value; }
        }
        #endregion

        #region cmt_sourceType
        string _cmt_sourcetype;
        /// <summary>
        /// cmt_sourceType
        /// </summary>
        public string cmt_sourceType
        {
            get { return _cmt_sourcetype; }
            set { _cmt_sourcetype = value; }
        }
        #endregion

        #region cmt_sourceId
        string _cmt_sourceid;
        /// <summary>
        /// cmt_sourceId
        /// </summary>
        public string cmt_sourceId
        {
            get { return _cmt_sourceid; }
            set { _cmt_sourceid = value; }
        }
        #endregion

        #region cmt_title
        string _cmt_title;
        /// <summary>
        /// cmt_title
        /// </summary>
        public string cmt_title
        {
            get { return _cmt_title; }
            set { _cmt_title = value; }
        }
        #endregion

        #region cmt_content
        string _cmt_content;
        /// <summary>
        /// cmt_content
        /// </summary>
        public string cmt_content
        {
            get { return _cmt_content; }
            set { _cmt_content = value; }
        }
        #endregion

        #region cmt_parentIds
        string _cmt_parentids;
        /// <summary>
        /// cmt_parentIds
        /// </summary>
        public string cmt_parentIds
        {
            get { return _cmt_parentids; }
            set { _cmt_parentids = value; }
        }
        #endregion

        #region cmt_accept
        int _cmt_accept;
        /// <summary>
        /// cmt_accept
        /// </summary>
        public int cmt_accept
        {
            get { return _cmt_accept; }
            set { _cmt_accept = value; }
        }
        #endregion

        #region cmt_status
        int _cmt_status;
        /// <summary>
        /// cmt_status
        /// </summary>
        public int cmt_status
        {
            get { return _cmt_status; }
            set { _cmt_status = value; }
        }
        #endregion

        #region cmt_ip
        string _cmt_ip;
        /// <summary>
        /// cmt_ip
        /// </summary>
        public string cmt_ip
        {
            get { return _cmt_ip; }
            set { _cmt_ip = value; }
        }
        #endregion

        #region cmt_ipArea
        string _cmt_iparea;
        /// <summary>
        /// cmt_ipArea
        /// </summary>
        public string cmt_ipArea
        {
            get { return _cmt_iparea; }
            set { _cmt_iparea = value; }
        }
        #endregion

        #region cmt_createUserID
        int _cmt_createuserid;
        /// <summary>
        /// cmt_createUserID
        /// </summary>
        public int cmt_createUserID
        {
            get { return _cmt_createuserid; }
            set { _cmt_createuserid = value; }
        }
        #endregion

        #region cmt_createUser
        string _cmt_createuser;
        /// <summary>
        /// cmt_createUser
        /// </summary>
        public string cmt_createUser
        {
            get { return _cmt_createuser; }
            set { _cmt_createuser = value; }
        }
        #endregion

        #region cmt_createTime
        DateTime _cmt_createtime;
        /// <summary>
        /// cmt_createTime
        /// </summary>
        public DateTime cmt_createTime
        {
            get { return _cmt_createtime; }
            set { _cmt_createtime = value; }
        }
        #endregion

        #region cmt_checkUser
        string _cmt_checkuser;
        /// <summary>
        /// cmt_checkUser
        /// </summary>
        public string cmt_checkUser
        {
            get { return _cmt_checkuser; }
            set { _cmt_checkuser = value; }
        }
        #endregion

        #region cmt_checkTime
        string _cmt_checktime;
        /// <summary>
        /// cmt_checkTime
        /// </summary>
        public string cmt_checkTime
        {
            get { return _cmt_checktime; }
            set { _cmt_checktime = value; }
        }
        #endregion

        #region cmt_checkRemark
        string _cmt_checkremark;
        /// <summary>
        /// cmt_checkRemark
        /// </summary>
        public string cmt_checkRemark
        {
            get { return _cmt_checkremark; }
            set { _cmt_checkremark = value; }
        }
        #endregion

    }
}
