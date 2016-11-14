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
    public class OrgCompanyCombine
    {

        #region orgcompanyList



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





        /// <summary>
        /// 证书有效期开始
        /// </summary>
        public DateTime oc_issuetime
        {
            get;
            set;
        }

        /// <summary>
        /// 证书有效期过期
        /// </summary>
        public DateTime oc_invalidtime
        {
            get;
            set;
        }

        /// <summary>
        /// 机构类别
        /// </summary>
        public string oc_companytype
        {
            get;
            set;
        }



        #endregion


        #region orgcompanyDtl

        #region 法人[od_faRen]
        string _od_faren;
        /// <summary>
        /// 法人[od_faRen]
        /// </summary>
        public string od_faRen
        {
            get { return _od_faren; }
            set { _od_faren = value; }
        }
        #endregion

        /// <summary>
        /// 注册资本
        /// </summary>
        public decimal od_regM
        {
            get;
            set;
        }

        #region 注册资本[od_regMoney]
        string _od_regmoney;
        /// <summary>
        /// 注册资本[od_regMoney]
        /// </summary>
        public string od_regMoney
        {
            get { return _od_regmoney; }
            set { _od_regmoney = value; }
        }
        #endregion

        //#region 注册日期[od_regDate]
        //string _od_regdate;
        ///// <summary>
        ///// 注册日期[od_regDate]
        ///// </summary>
        //public string od_regDate
        //{
        //    get { return _od_regdate; }
        //    set { _od_regdate = value; }
        //}
        //#endregion




        #region 注册类型[od_regType]
        string _od_regtype;
        /// <summary>
        /// 注册类型[od_regType]
        /// </summary>
        public string od_regType
        {
            get { return _od_regtype; }
            set { _od_regtype = value; }
        }
        #endregion

        #region 业务简介[od_bussinessDes]
        string _od_bussinessdes;
        /// <summary>
        /// 业务简介[od_bussinessDes]
        /// </summary>
        public string od_bussinessDes
        {
            get { return _od_bussinessdes; }
            set { _od_bussinessdes = value; }
        }
        #endregion

        #region 扩展字段[od_ext]
        string _od_ext;
        /// <summary>
        /// 扩展字段[od_ext]
        /// </summary>
        public string od_ext
        {
            get { return _od_ext; }
            set { _od_ext = value; }
        }
        #endregion

        #region 数据创建时间[od_CreateTime]
        DateTime _od_createtime;
        /// <summary>
        /// 数据创建时间[od_CreateTime]
        /// </summary>
        public DateTime od_CreateTime
        {
            get { return _od_createtime; }
            set { _od_createtime = value; }
        }

        #endregion

        #endregion


        /// <summary>
        /// 股东字符串
        /// </summary>
        public string od_gd
        {
            get; set;
        }

        public double oc_weight { get; set; }
    }
}
