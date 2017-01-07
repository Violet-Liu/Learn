using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class ExcelCompanyOrderInfo
    { 

        #region 主键[eco_id]
        int _eco_id;
        /// <summary>
        /// 主键[eco_id]
        /// </summary>
        public int eco_id
        {
            get { return _eco_id; }
            set { _eco_id = value; }
        }
        #endregion

        #region 订单号[eco_orderid]
        string _eco_orderid;
        /// <summary>
        /// 订单号[eco_orderid]
        /// </summary>
        public string eco_orderid
        {
            get { return _eco_orderid; }
            set { _eco_orderid = value; }
        }
        #endregion

        #region 订单名称[eco_ordername]
        string _eco_ordername;
        /// <summary>
        /// 订单名称[eco_ordername]
        /// </summary>
        public string eco_ordername
        {
            get { return _eco_ordername; }
            set { _eco_ordername = value; }
        }
        #endregion

        #region 位运算1公司信息 2成员 4股东[eco_type]
        int _eco_type;
        /// <summary>
        /// 位运算1公司信息 2成员 4股东[eco_type]
        /// </summary>
        public int eco_type
        {
            get { return _eco_type; }
            set { _eco_type = value; }
        }
        #endregion

        #region 金额[eco_money]
        decimal _eco_money;
        /// <summary>
        /// 金额[eco_money]
        /// </summary>
        public decimal eco_money
        {
            get { return _eco_money; }
            set { _eco_money = value; }
        }
        #endregion

        #region 支付交易码[eco_tradeNo]
        string _eco_tradeno;
        /// <summary>
        /// 支付交易码[eco_tradeNo]
        /// </summary>
        public string eco_tradeNo
        {
            get { return _eco_tradeno; }
            set { _eco_tradeno = value; }
        }
        #endregion

        #region 是否支付成功[eco_paySuccess]
        bool _eco_paysuccess;
        /// <summary>
        /// 是否支付成功[eco_paySuccess]
        /// </summary>
        public bool eco_paySuccess
        {
            get { return _eco_paysuccess; }
            set { _eco_paysuccess = value; }
        }
        #endregion

        #region 支付类型[eco_payType]
        string _eco_paytype;
        /// <summary>
        /// 支付类型[eco_payType]
        /// </summary>
        public string eco_payType
        {
            get { return _eco_paytype; }
            set { _eco_paytype = value; }
        }
        #endregion

        #region 支付时间[eco_payTime]
        string _eco_paytime;
        /// <summary>
        /// 支付时间[eco_payTime]
        /// </summary>
        public string eco_payTime
        {
            get { return _eco_paytime; }
            set { _eco_paytime = value; }
        }
        #endregion

        #region 支付状态[eco_state]
        int _eco_state;
        /// <summary>
        /// 支付状态[eco_state]
        /// </summary>
        public int eco_state
        {
            get { return _eco_state; }
            set { _eco_state = value; }
        }
        #endregion

        #region eco_remark
        string _eco_remark;
        /// <summary>
        /// eco_remark
        /// </summary>
        public string eco_remark
        {
            get { return _eco_remark; }
            set { _eco_remark = value; }
        }
        #endregion

        #region 支付用户id[eco_userid]
        int _eco_userid;
        /// <summary>
        /// 支付用户id[eco_userid]
        /// </summary>
        public int eco_userid
        {
            get { return _eco_userid; }
            set { _eco_userid = value; }
        }
        #endregion

        #region 支付用户名称[eco_userName]
        string _eco_username;
        /// <summary>
        /// 支付用户名称[eco_userName]
        /// </summary>
        public string eco_userName
        {
            get { return _eco_username; }
            set { _eco_username = value; }
        }
        #endregion

        #region 创建时间[eco_createTime]
        DateTime _eco_createtime;
        /// <summary>
        /// 创建时间[eco_createTime]
        /// </summary>
        public DateTime eco_createTime
        {
            get { return _eco_createtime; }
            set { _eco_createtime = value; }
        }
        #endregion

        #region eco_ip
        string _eco_ip;
        /// <summary>
        /// eco_ip
        /// </summary>
        public string eco_ip
        {
            get { return _eco_ip; }
            set { _eco_ip = value; }
        }
        #endregion

        #region 平台类型[eco_platformType]
        int _eco_platformtype;
        /// <summary>
        /// 平台类型[eco_platformType]
        /// </summary>
        public int eco_platformType
        {
            get { return _eco_platformtype; }
            set { _eco_platformtype = value; }
        }
        #endregion

        #region 手机号[eco_mobile]
        string _eco_mobile;
        /// <summary>
        /// 手机号[eco_mobile]
        /// </summary>
        public string eco_mobile
        {
            get { return _eco_mobile; }
            set { _eco_mobile = value; }
        }
        #endregion

        #region 邮箱[eco_email]
        string _eco_email;
        /// <summary>
        /// 邮箱[eco_email]
        /// </summary>
        public string eco_email
        {
            get { return _eco_email; }
            set { _eco_email = value; }
        }
        #endregion

        #region 是否删除[eco_isvaild]
        bool _eco_isvaild;
        /// <summary>
        /// 是否删除[eco_isvaild]
        /// </summary>
        public bool eco_isvaild
        {
            get { return _eco_isvaild; }
            set { _eco_isvaild = value; }
        }
        #endregion
    }
}
