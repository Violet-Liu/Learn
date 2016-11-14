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
    public class SearchHistoryInfo : IDisposable
    {
        #region IDisposable 接口实现

        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~SearchHistoryInfo()
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

        #endregion IDisposable 接口实现

        #region 自动编号[sh_id]

        private int _sh_id;

        /// <summary>
        /// 自动编号[sh_id]
        /// </summary>
        public int sh_id
        {
            get { return _sh_id; }
            set { _sh_id = value; }
        }

        #endregion 自动编号[sh_id]

        #region 企业名称[sh_oc_name]

        private string _sh_oc_name;

        /// <summary>
        /// 企业名称[sh_oc_name]
        /// </summary>
        public string sh_oc_name
        {
            get { return _sh_oc_name; }
            set { _sh_oc_name = value; }
        }

        #endregion 企业名称[sh_oc_name]

        #region 地区[sh_oc_area]

        private string _sh_oc_area;

        /// <summary>
        /// 地区[sh_oc_area]
        /// </summary>
        public string sh_oc_area
        {
            get { return _sh_oc_area; }
            set { _sh_oc_area = value; }
        }

        #endregion 地区[sh_oc_area]

        #region 注册资金下限[sh_od_regMLower]

        private decimal _sh_od_regmlower;

        /// <summary>
        /// 注册资金下限[sh_od_regMLower]
        /// </summary>
        public decimal sh_od_regMLower
        {
            get { return _sh_od_regmlower; }
            set { _sh_od_regmlower = value; }
        }

        #endregion 注册资金下限[sh_od_regMLower]

        #region 注册资金上限[sh_od_regMUpper]

        private decimal _sh_od_regmupper;

        /// <summary>
        /// 注册资金上限[sh_od_regMUpper]
        /// </summary>
        public decimal sh_od_regMUpper
        {
            get { return _sh_od_regmupper; }
            set { _sh_od_regmupper = value; }
        }

        #endregion 注册资金上限[sh_od_regMUpper]

        #region 企业类型[sh_od_regType]

        private string _sh_od_regtype;

        /// <summary>
        /// 企业类型[sh_od_regType]
        /// </summary>
        public string sh_od_regType
        {
            get { return _sh_od_regtype; }
            set { _sh_od_regtype = value; }
        }

        #endregion 企业类型[sh_od_regType]

        #region 机构代码[sh_oc_code]

        private string _sh_oc_code;

        /// <summary>
        /// 机构代码[sh_oc_code]
        /// </summary>
        public string sh_oc_code
        {
            get { return _sh_oc_code; }
            set { _sh_oc_code = value; }
        }

        #endregion 机构代码[sh_oc_code]

        #region 注册码[sh_oc_number]

        private string _sh_oc_number;

        /// <summary>
        /// 注册码[sh_oc_number]
        /// </summary>
        public string sh_oc_number
        {
            get { return _sh_oc_number; }
            set { _sh_oc_number = value; }
        }

        #endregion 注册码[sh_oc_number]

        #region 法人[sh_od_faRen]

        private string _sh_od_faren;

        /// <summary>
        /// 法人[sh_od_faRen]
        /// </summary>
        public string sh_od_faRen
        {
            get { return _sh_od_faren; }
            set { _sh_od_faren = value; }
        }

        #endregion 法人[sh_od_faRen]

        /// <summary>
        /// 股东
        /// </summary>
        public string sh_od_gd
        {
            get;
            set;
        }

        #region 业务范围[sh_od_bussinessDes]

        private string _sh_od_bussinessdes;

        /// <summary>
        /// 业务范围[sh_od_bussinessDes]
        /// </summary>
        public string sh_od_bussinessDes
        {
            get { return _sh_od_bussinessdes; }
            set { _sh_od_bussinessdes = value; }
        }

        #endregion 业务范围[sh_od_bussinessDes]

        #region 用户名[sh_u_name]

        private string _sh_u_name;

        /// <summary>
        /// 用户名[sh_u_name]
        /// </summary>
        public string sh_u_name
        {
            get { return _sh_u_name; }
            set { _sh_u_name = value; }
        }

        #endregion 用户名[sh_u_name]

        #region 用户ID[sh_u_uid]

        private int _sh_u_uid;

        /// <summary>
        /// 用户ID[sh_u_uid]
        /// </summary>
        public int sh_u_uid
        {
            get { return _sh_u_uid; }
            set { _sh_u_uid = value; }
        }

        #endregion 用户ID[sh_u_uid]

        #region 搜索日期[sh_date]

        private DateTime _sh_date;

        /// <summary>
        /// 搜索日期[sh_date]
        /// </summary>
        public DateTime sh_date
        {
            get { return _sh_date; }
            set { _sh_date = value; }
        }

        #endregion 搜索日期[sh_date]

        #region 搜索类型[sh_searchType]

        private int _sh_searchtype;

        /// <summary>
        /// 搜索类型[sh_searchType]
        /// </summary>
        public int sh_searchType
        {
            get { return _sh_searchtype; }
            set { _sh_searchtype = value; }
        }

        #endregion 搜索类型[sh_searchType]

        #region 排序非正常企业[sh_od_ext]

        private string _sh_od_ext;

        /// <summary>
        /// 排序非正常企业[sh_od_ext]
        /// </summary>
        public string sh_od_ext
        {
            get { return _sh_od_ext; }
            set { _sh_od_ext = value; }
        }

        #endregion 排序非正常企业[sh_od_ext]

        #region 排序，0位默认排序，1为按注册资本倒序排序,2为按注册时间倒序排序[sh_od_orderBy]

        private int _sh_od_orderby;

        /// <summary>
        /// 注册资金排序，1为倒序，0为正序[sh_od_orderBy]
        /// </summary>
        public int sh_od_orderBy
        {
            get { return _sh_od_orderby; }
            set { _sh_od_orderby = value; }
        }

        #endregion 排序，0位默认排序，1为按注册资本倒序排序,2为按注册时间倒序排序[sh_od_orderBy]

        #region 公司地址[sh_oc_address]

        private string _sh_oc_address;

        /// <summary>
        /// 公司地址[sh_oc_address]
        /// </summary>
        public string sh_oc_address
        {
            get { return _sh_oc_address; }
            set { _sh_oc_address = value; }
        }

        #endregion 公司地址[sh_oc_address]

        #region 区域名[sh_oc_areaName]

        private string _sh_oc_areaName;

        /// <summary>
        /// 区域名[sh_oc_areaName]
        /// </summary>
        public string sh_oc_areaName
        {
            get { return _sh_oc_areaName; }
            set { _sh_oc_areaName = value; }
        }

        #endregion 区域名[sh_oc_areaName]
    }
}
