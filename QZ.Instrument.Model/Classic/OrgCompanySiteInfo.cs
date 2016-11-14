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
    public class OrgCompanySiteInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanySiteInfo()
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

        #region 自增 ID[ocs_id]
        int _ocs_id;
        /// <summary>
        /// 自增 ID[ocs_id]
        /// </summary>
        public int ocs_id
        {
            get { return _ocs_id; }
            set { _ocs_id = value; }
        }
        #endregion

        #region 机构代码[ocs_oc_code]
        string _ocs_oc_code;
        /// <summary>
        /// 机构代码[ocs_oc_code]
        /// </summary>
        public string ocs_oc_code
        {
            get { return _ocs_oc_code; }
            set { _ocs_oc_code = value; }
        }
        #endregion

        #region 备案域名[ocs_domain]
        string _ocs_domain;
        /// <summary>
        /// 备案域名[ocs_domain]
        /// </summary>
        public string ocs_domain
        {
            get { return _ocs_domain; }
            set { _ocs_domain = value; }
        }
        #endregion

        #region 备案单位/个人[ocs_host]
        string _ocs_host;
        /// <summary>
        /// 备案单位/个人[ocs_host]
        /// </summary>
        public string ocs_host
        {
            get { return _ocs_host; }
            set { _ocs_host = value; }
        }
        #endregion

        #region 主体类型[ocs_hostType]
        string _ocs_hosttype;
        /// <summary>
        /// 主体类型[ocs_hostType]
        /// </summary>
        public string ocs_hostType
        {
            get { return _ocs_hosttype; }
            set { _ocs_hosttype = value; }
        }
        #endregion

        #region 备案号[ocs_number]
        string _ocs_number;
        /// <summary>
        /// 备案号[ocs_number]
        /// </summary>
        public string ocs_number
        {
            get { return _ocs_number; }
            set { _ocs_number = value; }
        }
        #endregion

        #region 网站名称[ocs_siteName]
        string _ocs_sitename;
        /// <summary>
        /// 网站名称[ocs_siteName]
        /// </summary>
        public string ocs_siteName
        {
            get { return _ocs_sitename; }
            set { _ocs_sitename = value; }
        }
        #endregion

        #region 网站首页地址[ocs_siteHomePage]
        string _ocs_sitehomepage;
        /// <summary>
        /// 网站首页地址[ocs_siteHomePage]
        /// </summary>
        public string ocs_siteHomePage
        {
            get { return _ocs_sitehomepage; }
            set { _ocs_sitehomepage = value; }
        }
        #endregion

        #region ocs_status
        byte _ocs_status;
        /// <summary>
        /// ocs_status
        /// </summary>
        public byte ocs_status
        {
            get { return _ocs_status; }
            set { _ocs_status = value; }
        }
        #endregion

        #region 备案通过时间[ocs_checkTime]
        DateTime _ocs_checktime;
        /// <summary>
        /// 备案通过时间[ocs_checkTime]
        /// </summary>
        public DateTime ocs_checkTime
        {
            get { return _ocs_checktime; }
            set { _ocs_checktime = value; }
        }
        #endregion

        #region 创建时间[ocs_createTime]
        DateTime _ocs_createtime;
        /// <summary>
        /// 创建时间[ocs_createTime]
        /// </summary>
        public DateTime ocs_createTime
        {
            get { return _ocs_createtime; }
            set { _ocs_createtime = value; }
        }
        #endregion

        #region ocs_ext
        string _ocs_ext;
        /// <summary>
        /// ocs_ext
        /// </summary>
        public string ocs_ext
        {
            get { return _ocs_ext; }
            set { _ocs_ext = value; }
        }
        #endregion


        #region 经营状态[OperatingStatus]
        string _OperatingStatus;
        /// <summary>
        /// 经营状态[OperatingStatus]
        /// </summary>
        public string OperatingStatus
        {
            get { return _OperatingStatus; }
            set { _OperatingStatus = value; }
        }
        #endregion

    }
}
