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
    public class OrgCompanyDtl_EvtInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyDtl_EvtInfo()
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

        #region 机构登记号[oe_id]
        int _oe_id;
        /// <summary>
        /// 机构登记号[oe_id]
        /// </summary>
        public int oe_id
        {
            get { return _oe_id; }
            set { _oe_id = value; }
        }
        #endregion

        #region oe_oc_code
        string _oe_oc_code;
        /// <summary>
        /// oe_oc_code
        /// </summary>
        public string oe_oc_code
        {
            get { return _oe_oc_code; }
            set { _oe_oc_code = value; }
        }
        #endregion

        #region 事件日期[oe_date]
        string _oe_date;
        /// <summary>
        /// 事件日期[oe_date]
        /// </summary>
        public string oe_date
        {
            get { return _oe_date; }
            set { _oe_date = value; }
        }
        #endregion

        #region 变更类型[oe_type]
        string _oe_type;
        /// <summary>
        /// 变更类型[oe_type]
        /// </summary>
        public string oe_type
        {
            get { return _oe_type; }
            set { _oe_type = value; }
        }
        #endregion

        #region 变更明细[oe_dtl]
        string _oe_dtl;
        /// <summary>
        /// 变更明细[oe_dtl]
        /// </summary>
        public string oe_dtl
        {
            get { return _oe_dtl; }
            set { _oe_dtl = value; }
        }
        #endregion

    }
}
