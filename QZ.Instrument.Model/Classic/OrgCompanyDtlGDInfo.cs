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
    public class OrgCompanyDtlGDInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyDtlGDInfo()
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

        #region 机构登记号[og_oc_code]
        string _og_oc_code;
        /// <summary>
        /// 机构登记号[og_oc_code]
        /// </summary>
        public string og_oc_code
        {
            get { return _og_oc_code; }
            set { _og_oc_code = value; }
        }
        #endregion

        #region 股东名称[og_name]
        string _og_name;
        /// <summary>
        /// 股东名称[og_name]
        /// </summary>
        public string og_name
        {
            get { return _og_name; }
            set { _og_name = value; }
        }
        #endregion

        #region 出资[og_money]
        decimal _og_money;
        /// <summary>
        /// 出资[og_money]
        /// </summary>
        public decimal og_money
        {
            get { return _og_money; }
            set { _og_money = value; }
        }
        #endregion

        #region 出资比例[og_BL]
        decimal _og_bl;
        /// <summary>
        /// 出资比例[og_BL]
        /// </summary>
        public decimal og_BL
        {
            get { return _og_bl; }
            set { _og_bl = value; }
        }
        #endregion

        #region 资金单位[og_unit]
        string _og_unit;
        /// <summary>
        /// 资金单位[og_unit]
        /// </summary>
        public string og_unit
        {
            get { return _og_unit; }
            set { _og_unit = value; }
        }
        #endregion

        #region 股东属性[og_pro]
        string _og_pro;
        /// <summary>
        /// 股东属性[og_pro]
        /// </summary>
        public string og_pro
        {
            get { return _og_pro; }
            set { _og_pro = value; }
        }
        #endregion

        #region 股东类别[og_type]
        string _og_type;
        /// <summary>
        /// 股东类别[og_type]
        /// </summary>
        public string og_type
        {
            get { return _og_type; }
            set { _og_type = value; }
        }
        #endregion

    }
}
