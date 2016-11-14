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
    public class CommentInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CommentInfo()
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

        #region 格式，例如：1208N10001 前面四位是所在分表，按年月来，后面6位数据是序号[cmt_uid]
        string _cmt_uid;
        /// <summary>
        /// 格式，例如：1208N10001 前面四位是所在分表，按年月来，后面6位数据是序号[cmt_uid]
        /// </summary>
        public string cmt_uid
        {
            get { return _cmt_uid; }
            set { _cmt_uid = value; }
        }
        #endregion

        #region 评论对象类型，例如：指南，新闻[cmt_sourceType]
        string _cmt_sourcetype;
        /// <summary>
        /// 评论对象类型，例如：指南，新闻[cmt_sourceType]
        /// </summary>
        public string cmt_sourceType
        {
            get { return _cmt_sourcetype; }
            set { _cmt_sourcetype = value; }
        }
        #endregion

        #region 评论源唯一编号[cmt_sourceId]
        string _cmt_sourceid;
        /// <summary>
        /// 评论源唯一编号[cmt_sourceId]
        /// </summary>
        public string cmt_sourceId
        {
            get { return _cmt_sourceid; }
            set { _cmt_sourceid = value; }
        }
        #endregion

        #region 评论标题[cmt_title]
        string _cmt_title;
        /// <summary>
        /// 评论标题[cmt_title]
        /// </summary>
        public string cmt_title
        {
            get { return _cmt_title; }
            set { _cmt_title = value; }
        }
        #endregion

        #region 评论内容[cmt_content]
        string _cmt_content;
        /// <summary>
        /// 评论内容[cmt_content]
        /// </summary>
        public string cmt_content
        {
            get { return _cmt_content; }
            set { _cmt_content = value; }
        }
        #endregion

        #region 所有的父评论编号，例如：1234，1235，1236[cmt_parentIds]
        string _cmt_parentids;
        /// <summary>
        /// 所有的父评论编号，例如：1234，1235，1236[cmt_parentIds]
        /// </summary>
        public string cmt_parentIds
        {
            get { return _cmt_parentids; }
            set { _cmt_parentids = value; }
        }
        #endregion

        #region 被接受次数，相当于“顶”[cmt_accept]
        int _cmt_accept;
        /// <summary>
        /// 被接受次数，相当于“顶”[cmt_accept]
        /// </summary>
        public int cmt_accept
        {
            get { return _cmt_accept; }
            set { _cmt_accept = value; }
        }
        #endregion

        #region 状态：0=未审核，1=审核通过[cmt_status]
        int _cmt_status;
        /// <summary>
        /// 状态：0=未审核，1=审核通过[cmt_status]
        /// </summary>
        public int cmt_status
        {
            get { return _cmt_status; }
            set { _cmt_status = value; }
        }
        #endregion

        #region 用户IP地址[cmt_ip]
        string _cmt_ip;
        /// <summary>
        /// 用户IP地址[cmt_ip]
        /// </summary>
        public string cmt_ip
        {
            get { return _cmt_ip; }
            set { _cmt_ip = value; }
        }
        #endregion

        #region IP所在地区[cmt_ipArea]
        string _cmt_iparea;
        /// <summary>
        /// IP所在地区[cmt_ipArea]
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

        #region 创建人[cmt_createUser]
        string _cmt_createuser;
        /// <summary>
        /// 创建人[cmt_createUser]
        /// </summary>
        public string cmt_createUser
        {
            get { return _cmt_createuser; }
            set { _cmt_createuser = value; }
        }
        #endregion

        #region 创建时间[cmt_createTime]
        DateTime _cmt_createtime;
        /// <summary>
        /// 创建时间[cmt_createTime]
        /// </summary>
        public DateTime cmt_createTime
        {
            get { return _cmt_createtime; }
            set { _cmt_createtime = value; }
        }
        #endregion

        #region 审核人[cmt_checkUser]
        string _cmt_checkuser;
        /// <summary>
        /// 审核人[cmt_checkUser]
        /// </summary>
        public string cmt_checkUser
        {
            get { return _cmt_checkuser; }
            set { _cmt_checkuser = value; }
        }
        #endregion

        #region 审核时间[cmt_checkTime]
        string _cmt_checktime;
        /// <summary>
        /// 审核时间[cmt_checkTime]
        /// </summary>
        public string cmt_checkTime
        {
            get { return _cmt_checktime; }
            set { _cmt_checktime = value; }
        }
        #endregion

        #region 审核备注[cmt_checkRemark]
        string _cmt_checkremark;
        /// <summary>
        /// 审核备注[cmt_checkRemark]
        /// </summary>
        public string cmt_checkRemark
        {
            get { return _cmt_checkremark; }
            set { _cmt_checkremark = value; }
        }
        #endregion


        public string cmt_sourceCateId
        {
            get;
            set;
        }

        public int cmt_down
        {
            get;
            set;
        }

        public string cmt_platform
        {
            get;
            set;
        }

        public string cmt_device
        {
            get;
            set;
        }
    }
}
