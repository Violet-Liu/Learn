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
    public class Users_PwdFoundLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~Users_PwdFoundLogInfo()
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

        #region pl_id
        int _pl_id;
        /// <summary>
        /// pl_id
        /// </summary>
        public int pl_id
        {
            get { return _pl_id; }
            set { _pl_id = value; }
        }
        #endregion

        #region pl_uid
        string _pl_uid;
        /// <summary>
        /// pl_uid
        /// </summary>
        public string pl_uid
        {
            get { return _pl_uid; }
            set { _pl_uid = value; }
        }
        #endregion

        #region pl_to
        string _pl_to;
        /// <summary>
        /// pl_to
        /// </summary>
        public string pl_to
        {
            get { return _pl_to; }
            set { _pl_to = value; }
        }
        #endregion

        #region pl_url
        string _pl_url;
        /// <summary>
        /// pl_url
        /// </summary>
        public string pl_url
        {
            get { return _pl_url; }
            set { _pl_url = value; }
        }
        #endregion

        #region pl_expireTime
        DateTime _pl_expiretime;
        /// <summary>
        /// pl_expireTime
        /// </summary>
        public DateTime pl_expireTime
        {
            get { return _pl_expiretime; }
            set { _pl_expiretime = value; }
        }
        #endregion

        #region pl_status
        int _pl_status;
        /// <summary>
        /// pl_status
        /// </summary>
        public int pl_status
        {
            get { return _pl_status; }
            set { _pl_status = value; }
        }
        #endregion

        #region pl_remark
        string _pl_remark;
        /// <summary>
        /// pl_remark
        /// </summary>
        public string pl_remark
        {
            get { return _pl_remark; }
            set { _pl_remark = value; }
        }
        #endregion

        #region pl_createTime
        DateTime _pl_createtime;
        /// <summary>
        /// pl_createTime
        /// </summary>
        public DateTime pl_createTime
        {
            get { return _pl_createtime; }
            set { _pl_createtime = value; }
        }
        #endregion

        #region pl_execTime
        string _pl_exectime;
        /// <summary>
        /// pl_execTime
        /// </summary>
        public string pl_execTime
        {
            get { return _pl_exectime; }
            set { _pl_exectime = value; }
        }
        #endregion

    }
}
