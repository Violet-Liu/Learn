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
    public class OrgCompanyGsxtDtlGDInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyGsxtDtlGDInfo()
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

        #region og_int
        int _og_int;
        /// <summary>
        /// og_int
        /// </summary>
        public int og_int
        {
            get { return _og_int; }
            set { _og_int = value; }
        }
        #endregion

        #region og_oc_code
        string _og_oc_code;
        /// <summary>
        /// og_oc_code
        /// </summary>
        public string og_oc_code
        {
            get { return _og_oc_code; }
            set { _og_oc_code = value; }
        }
        #endregion

        #region og_name
        string _og_name;
        /// <summary>
        /// og_name
        /// </summary>
        public string og_name
        {
            get { return _og_name; }
            set { _og_name = value; }
        }
        #endregion

        #region og_type
        string _og_type;
        /// <summary>
        /// og_type
        /// </summary>
        public string og_type
        {
            get { return _og_type; }
            set { _og_type = value; }
        }
        #endregion

        #region og_licenseType
        string _og_licensetype;
        /// <summary>
        /// og_licenseType
        /// </summary>
        public string og_licenseType
        {
            get { return _og_licensetype; }
            set { _og_licensetype = value; }
        }
        #endregion

        #region og_licenseNum
        string _og_licensenum;
        /// <summary>
        /// og_licenseNum
        /// </summary>
        public string og_licenseNum
        {
            get { return _og_licensenum; }
            set { _og_licensenum = value; }
        }
        #endregion

        #region og_subscribeAccount
        decimal _og_subscribeaccount;
        /// <summary>
        /// og_subscribeAccount
        /// </summary>
        public decimal og_subscribeAccount
        {
            get { return _og_subscribeaccount; }
            set { _og_subscribeaccount = value; }
        }
        #endregion

        #region og_subscribeForm
        string _og_subscribeform;
        /// <summary>
        /// og_subscribeForm
        /// </summary>
        public string og_subscribeForm
        {
            get { return _og_subscribeform; }
            set { _og_subscribeform = value; }
        }
        #endregion

        #region og_subscribeDate
        string _og_subscribedate;
        /// <summary>
        /// og_subscribeDate
        /// </summary>
        public string og_subscribeDate
        {
            get { return _og_subscribedate; }
            set { _og_subscribedate = value; }
        }
        #endregion

        #region og_realAccount
        decimal _og_realaccount;
        /// <summary>
        /// og_realAccount
        /// </summary>
        public decimal og_realAccount
        {
            get { return _og_realaccount; }
            set { _og_realaccount = value; }
        }
        #endregion

        #region og_realForm
        string _og_realform;
        /// <summary>
        /// og_realForm
        /// </summary>
        public string og_realForm
        {
            get { return _og_realform; }
            set { _og_realform = value; }
        }
        #endregion

        #region og_realDate
        string _og_realdate;
        /// <summary>
        /// og_realDate
        /// </summary>
        public string og_realDate
        {
            get { return _og_realdate; }
            set { _og_realdate = value; }
        }
        #endregion

        #region og_unit
        string _og_unit;
        /// <summary>
        /// og_unit
        /// </summary>
        public string og_unit
        {
            get { return _og_unit; }
            set { _og_unit = value; }
        }
        #endregion

        #region og_createTime
        DateTime _og_createTime;
        /// <summary>
        /// og_createTime
        /// </summary>
        public DateTime og_createTime
        {
            get { return _og_createTime; }
            set { _og_createTime = value; }
        }
        #endregion

        #region og_updateTime
        DateTime _og_updateTime;
        /// <summary>
        /// og_updateTime
        /// </summary>
        public DateTime og_updateTime
        {
            get { return _og_updateTime; }
            set { _og_updateTime = value; }
        }
        #endregion

        #region og_invalidTime
        DateTime _og_invalidtime;
        /// <summary>
        /// og_invalidTime
        /// </summary>
        public DateTime og_invalidTime
        {
            get { return _og_invalidtime; }
            set { _og_invalidtime = value; }
        }
        #endregion

        #region og_status
        byte _og_status;
        /// <summary>
        /// og_status
        /// </summary>
        public byte og_status
        {
            get { return _og_status; }
            set { _og_status = value; }
        }
        #endregion
    }
}
