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
    public class OrgCompanyDtlInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyDtlInfo()
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

        #region 自增主键[od_id]
        int _od_id;
        /// <summary>
        /// 自增主键[od_id]
        /// </summary>
        public int od_id
        {
            get { return _od_id; }
            set { _od_id = value; }
        }
        #endregion

        #region 机构号[od_oc_code]
        string _od_oc_code;
        /// <summary>
        /// 机构号[od_oc_code]
        /// </summary>
        public string od_oc_code
        {
            get { return _od_oc_code; }
            set { _od_oc_code = value; }
        }
        #endregion

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
        /// <summary>
        /// 注册资本查询
        /// </summary>
        public decimal od_regM
        { get; set; }
        /// <summary>
        /// 实际资产查询
        /// </summary>
        public decimal od_factM { get; set; }
        #region 实收资本[od_factMoney]
        string _od_factmoney;
        /// <summary>
        /// 实收资本[od_factMoney]
        /// </summary>
        public string od_factMoney
        {
            get { return _od_factmoney; }
            set { _od_factmoney = value; }
        }
        #endregion

        #region 注册日期[od_regDate]
        string _od_regdate;
        /// <summary>
        /// 注册日期[od_regDate]
        /// </summary>
        public string od_regDate
        {
            get { return _od_regdate; }
            set { _od_regdate = value; }
        }
        #endregion

        #region 开始经营时间[od_bussinessS]
        string _od_bussinesss;
        /// <summary>
        /// 开始经营时间[od_bussinessS]
        /// </summary>
        public string od_bussinessS
        {
            get { return _od_bussinesss; }
            set { _od_bussinesss = value; }
        }
        #endregion

        #region 截止经营时间[od_bussinessE]
        string _od_bussinesse;
        /// <summary>
        /// 截止经营时间[od_bussinessE]
        /// </summary>
        public string od_bussinessE
        {
            get { return _od_bussinesse; }
            set { _od_bussinesse = value; }
        }
        #endregion

        #region 核实日期[od_chkDate]
        string _od_chkdate;
        /// <summary>
        /// 核实日期[od_chkDate]
        /// </summary>
        public string od_chkDate
        {
            get { return _od_chkdate; }
            set { _od_chkdate = value; }
        }
        #endregion

        #region 最近年检[od_yearChk]
        string _od_yearchk;
        /// <summary>
        /// 最近年检[od_yearChk]
        /// </summary>
        public string od_yearChk
        {
            get { return _od_yearchk; }
            set { _od_yearchk = value; }
        }
        #endregion

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
        /// <summary>
        /// 公司名称
        /// </summary>
        public string oc_name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string oc_address { get; set; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string oc_number { get; set; }
        /// <summary>
        /// 抓取状态
        /// </summary>
        public int oc_status { get; set; }
    }
}
