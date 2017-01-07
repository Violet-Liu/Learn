using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyGsxtNbInfo
    {
        #region [id]
        int _id;
        /// <summary>
        /// [id]
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
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

        #region 报告年份[year]
        string _year;
        /// <summary>
        /// 报告年份[year]
        /// </summary>
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        #endregion

        #region 注册号[oc_number]
        string _oc_number;
        /// <summary>
        /// 注册号[oc_number]
        /// </summary>
        public string oc_number
        {
            get { return _oc_number; }
            set { _oc_number = value; }
        }
        #endregion

        #region 企业名称[name]
        string _name;
        /// <summary>
        /// 企业名称[name]
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region 企业联系电话[phone]
        string _phone;
        /// <summary>
        /// 企业联系电话[phone]
        /// </summary>
        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        #endregion

        #region 邮政编码	[postCode]
        string _postcode;
        /// <summary>
        /// 邮政编码	[postCode]
        /// </summary>
        public string postCode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }
        #endregion

        #region 企业通信地址[address]
        string _address;
        /// <summary>
        /// 企业通信地址[address]
        /// </summary>
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }
        #endregion

        #region 电子邮箱[mail]
        string _mail;
        /// <summary>
        /// 电子邮箱[mail]
        /// </summary>
        public string mail
        {
            get { return _mail; }
            set { _mail = value; }
        }
        #endregion

        #region 有限责任公司本年度是否发生股东股权转让(0 否 1 是)[shareTransfer]
        byte _sharetransfer;
        /// <summary>
        /// 有限责任公司本年度是否发生股东股权转让(0 否 1 是)[shareTransfer]
        /// </summary>
        public byte shareTransfer
        {
            get { return _sharetransfer; }
            set { _sharetransfer = value; }
        }
        #endregion

        #region 企业经营状态[runStatus]
        string _runstatus;
        /// <summary>
        /// 企业经营状态[runStatus]
        /// </summary>
        public string runStatus
        {
            get { return _runstatus; }
            set { _runstatus = value; }
        }
        #endregion

        #region 是否有网站或网点 (0 否 1 是)[webSite]
        byte _website;
        /// <summary>
        /// 是否有网站或网点 (0 否 1 是)[webSite]
        /// </summary>
        public byte webSite
        {
            get { return _website; }
            set { _website = value; }
        }
        #endregion

        #region 是否有投资信息或购买其他公司股权 (0 否 1 是)[otherShare]
        byte _othershare;
        /// <summary>
        /// 是否有投资信息或购买其他公司股权 (0 否 1 是)[otherShare]
        /// </summary>
        public byte otherShare
        {
            get { return _othershare; }
            set { _othershare = value; }
        }
        #endregion

        #region 从业人数[numbers]
        string _numbers;
        /// <summary>
        /// 从业人数[numbers]
        /// </summary>
        public string numbers
        {
            get { return _numbers; }
            set { _numbers = value; }
        }
        #endregion

        #region 资产总额[totalAssets]
        string _totalassets;
        /// <summary>
        /// 资产总额[totalAssets]
        /// </summary>
        public string totalAssets
        {
            get { return _totalassets; }
            set { _totalassets = value; }
        }
        #endregion

        #region 所有者权益合计[totalEquity]
        string _totalequity;
        /// <summary>
        /// 所有者权益合计[totalEquity]
        /// </summary>
        public string totalEquity
        {
            get { return _totalequity; }
            set { _totalequity = value; }
        }
        #endregion

        #region 营业总收入[totalIncome]
        string _totalincome;
        /// <summary>
        /// 营业总收入[totalIncome]
        /// </summary>
        public string totalIncome
        {
            get { return _totalincome; }
            set { _totalincome = value; }
        }
        #endregion

        #region 利润总额[toalProfit]
        string _toalprofit;
        /// <summary>
        /// 利润总额[toalProfit]
        /// </summary>
        public string toalProfit
        {
            get { return _toalprofit; }
            set { _toalprofit = value; }
        }
        #endregion

        #region 营业总收入中主营业务收入[mainIncome]
        string _mainincome;
        /// <summary>
        /// 营业总收入中主营业务收入[mainIncome]
        /// </summary>
        public string mainIncome
        {
            get { return _mainincome; }
            set { _mainincome = value; }
        }
        #endregion

        #region 净利润[netProfit]
        string _netprofit;
        /// <summary>
        /// 净利润[netProfit]
        /// </summary>
        public string netProfit
        {
            get { return _netprofit; }
            set { _netprofit = value; }
        }
        #endregion

        #region 纳税总额[totalTax]
        string _totaltax;
        /// <summary>
        /// 纳税总额[totalTax]
        /// </summary>
        public string totalTax
        {
            get { return _totaltax; }
            set { _totaltax = value; }
        }
        #endregion

        #region 负债总额[totalDebt]
        string _totaldebt;
        /// <summary>
        /// 负债总额[totalDebt]
        /// </summary>
        public string totalDebt
        {
            get { return _totaldebt; }
            set { _totaldebt = value; }
        }
        #endregion

        #region createTime
        DateTime _createtime;
        /// <summary>
        /// createTime
        /// </summary>
        public DateTime createTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        #endregion
    }
}
