using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class InvoiceInfo
    {
        #region 自动编号[invoice_id]
        int _invoice_id;
        /// <summary>
        /// 自动编号[invoice_id]
        /// </summary>
        public int invoice_id
        {
            get { return _invoice_id; }
            set { _invoice_id = value; }
        }
        #endregion

        #region 产品名称[invoice_proName]
        string _invoice_proname;
        /// <summary>
        /// 产品名称[invoice_proName]
        /// </summary>
        public string invoice_proName
        {
            get { return _invoice_proname; }
            set { _invoice_proname = value; }
        }
        #endregion

        #region 抬头[invoice_name]
        string _invoice_name;
        /// <summary>
        /// 抬头[invoice_name]
        /// </summary>
        public string invoice_name
        {
            get { return _invoice_name; }
            set { _invoice_name = value; }
        }
        #endregion

        #region 发票类型[invoice_type]
        int _invoice_type;
        /// <summary>
        /// 发票类型[invoice_type]
        /// </summary>
        public int invoice_type
        {
            get { return _invoice_type; }
            set { _invoice_type = value; }
        }
        #endregion

        #region 发票金额[invoice_money]
        decimal _invoice_money;
        /// <summary>
        /// 发票金额[invoice_money]
        /// </summary>
        public decimal invoice_money
        {
            get { return _invoice_money; }
            set { _invoice_money = value; }
        }
        #endregion

        #region 邮寄地址[invoice_address]
        string _invoice_address;
        /// <summary>
        /// 邮寄地址[invoice_address]
        /// </summary>
        public string invoice_address
        {
            get { return _invoice_address; }
            set { _invoice_address = value; }
        }
        #endregion

        #region 邮政编码[invoice_code]
        string _invoice_code;
        /// <summary>
        /// 邮政编码[invoice_code]
        /// </summary>
        public string invoice_code
        {
            get { return _invoice_code; }
            set { _invoice_code = value; }
        }
        #endregion

        #region 联系人[invoice_contacts]
        string _invoice_contacts;
        /// <summary>
        /// 联系人[invoice_contacts]
        /// </summary>
        public string invoice_contacts
        {
            get { return _invoice_contacts; }
            set { _invoice_contacts = value; }
        }
        #endregion

        #region 手机号[invoice_mobile]
        string _invoice_mobile;
        /// <summary>
        /// 手机号[invoice_mobile]
        /// </summary>
        public string invoice_mobile
        {
            get { return _invoice_mobile; }
            set { _invoice_mobile = value; }
        }
        #endregion

        #region 备注[invoice_desc]
        string _invoice_desc;
        /// <summary>
        /// 备注[invoice_desc]
        /// </summary>
        public string invoice_desc
        {
            get { return _invoice_desc; }
            set { _invoice_desc = value; }
        }
        #endregion

        #region 状态[invoice_state]
        int _invoice_state;
        /// <summary>
        /// 状态[invoice_state]
        /// </summary>
        public int invoice_state
        {
            get { return _invoice_state; }
            set { _invoice_state = value; }
        }
        #endregion

        #region 创建人Id[invoice_userId]
        int _invoice_userid;
        /// <summary>
        /// 创建人Id[invoice_userId]
        /// </summary>
        public int invoice_userId
        {
            get { return _invoice_userid; }
            set { _invoice_userid = value; }
        }
        #endregion

        #region 创建人[invoice_user]
        string _invoice_user;
        /// <summary>
        /// 创建人[invoice_user]
        /// </summary>
        public string invoice_user
        {
            get { return _invoice_user; }
            set { _invoice_user = value; }
        }
        #endregion

        #region 创建时间[invoice_createTime]
        DateTime _invoice_createtime;
        /// <summary>
        /// 创建时间[invoice_createTime]
        /// </summary>
        public DateTime invoice_createTime
        {
            get { return _invoice_createtime; }
            set { _invoice_createtime = value; }
        }
        #endregion

        #region 审核时间[invoice_checkTime]
        string _invoice_checktime;
        /// <summary>
        /// 审核时间[invoice_checkTime]
        /// </summary>
        public string invoice_checkTime
        {
            get { return _invoice_checktime; }
            set { _invoice_checktime = value; }
        }
        #endregion

        #region 审核人[invoice_checkUser]
        string _invoice_checkuser;
        /// <summary>
        /// 审核人[invoice_checkUser]
        /// </summary>
        public string invoice_checkUser
        {
            get { return _invoice_checkuser; }
            set { _invoice_checkuser = value; }
        }
        #endregion

        #region 审核备注[invoice_checkRemark]
        string _invoice_checkremark;
        /// <summary>
        /// 审核备注[invoice_checkRemark]
        /// </summary>
        public string invoice_checkRemark
        {
            get { return _invoice_checkremark; }
            set { _invoice_checkremark = value; }
        }
        #endregion
    }
}
