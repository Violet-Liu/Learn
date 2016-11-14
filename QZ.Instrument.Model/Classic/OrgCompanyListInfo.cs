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
    public class OrgCompanyListInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyListInfo()
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

        #region oc_id
        int _oc_id;
        /// <summary>
        /// oc_id
        /// </summary>
        public int oc_id
        {
            get { return _oc_id; }
            set { _oc_id = value; }
        }
        #endregion

        #region 9位唯一编码[oc_code]
        string _oc_code;
        /// <summary>
        /// 9位唯一编码[oc_code]
        /// </summary>
        public string oc_code
        {
            get { return _oc_code; }
            set { _oc_code = value; }
        }
        #endregion

        #region 登记号[oc_number]
        string _oc_number;
        /// <summary>
        /// 登记号[oc_number]
        /// </summary>
        public string oc_number
        {
            get { return _oc_number; }
            set { _oc_number = value; }
        }
        #endregion

        #region 区域代码[oc_area]
        string _oc_area;
        /// <summary>
        /// 区域代码[oc_area]
        /// </summary>
        public string oc_area
        {
            get { return _oc_area; }
            set { _oc_area = value; }
        }
        #endregion

        #region 区域名[oc_areaName]
        string _oc_areaname;
        /// <summary>
        /// 区域名[oc_areaName]
        /// </summary>
        public string oc_areaName
        {
            get { return _oc_areaname; }
            set { _oc_areaname = value; }
        }
        #endregion

        #region 登记注册机构名[oc_regOrgName]
        string _oc_regorgname;
        /// <summary>
        /// 登记注册机构名[oc_regOrgName]
        /// </summary>
        public string oc_regOrgName
        {
            get { return _oc_regorgname; }
            set { _oc_regorgname = value; }
        }
        #endregion

        #region 公司名（组织机构名）[oc_name]
        string _oc_name;
        /// <summary>
        /// 公司名（组织机构名）[oc_name]
        /// </summary>
        public string oc_name
        {
            get { return _oc_name; }
            set { _oc_name = value; }
        }
        #endregion

        #region 公司（组织机构）地址[oc_address]
        string _oc_address;
        /// <summary>
        /// 公司（组织机构）地址[oc_address]
        /// </summary>
        public string oc_address
        {
            get { return _oc_address; }
            set { _oc_address = value; }
        }
        #endregion

        #region 创建人[oc_createUser]
        string _oc_createuser;
        /// <summary>
        /// 创建人[oc_createUser]
        /// </summary>
        public string oc_createUser
        {
            get { return _oc_createuser; }
            set { _oc_createuser = value; }
        }
        #endregion

        #region 创建时间[oc_createTime]
        DateTime _oc_createtime;
        /// <summary>
        /// 创建时间[oc_createTime]
        /// </summary>
        public DateTime oc_createTime
        {
            get { return _oc_createtime; }
            set { _oc_createtime = value; }
        }
        #endregion

        #region oc_status
        bool _oc_status;
        /// <summary>
        /// oc_status
        /// </summary>
        public bool oc_status
        {
            get { return _oc_status; }
            set { _oc_status = value; }
        }
        #endregion

        #region oc_updatetime
        DateTime _oc_updatetime;
        /// <summary>
        /// oc_updatetime
        /// </summary>
        public DateTime oc_updatetime
        {
            get { return _oc_updatetime; }
            set { _oc_updatetime = value; }
        }
        #endregion

        #region oc_issuetime
        DateTime _oc_issuetime;
        /// <summary>
        /// oc_issuetime
        /// </summary>
        public DateTime oc_issuetime
        {
            get { return _oc_issuetime; }
            set { _oc_issuetime = value; }
        }
        #endregion

        #region oc_invalidtime
        DateTime _oc_invalidtime;
        /// <summary>
        /// oc_invalidtime
        /// </summary>
        public DateTime oc_invalidtime
        {
            get { return _oc_invalidtime; }
            set { _oc_invalidtime = value; }
        }
        #endregion

        #region oc_companytype
        string _oc_companytype;
        /// <summary>
        /// oc_companytype
        /// </summary>
        public string oc_companytype
        {
            get { return _oc_companytype; }
            set { _oc_companytype = value; }
        }
        #endregion

    }
}
