using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class ExhibitionEnterprise : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~ExhibitionEnterprise()
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

        #region 自增ID[ee_id]
        int _ee_id;
        /// <summary>
        /// 自增ID[ee_id]
        /// </summary>
        public int ee_id
        {
            get { return _ee_id; }
            set { _ee_id = value; }
        }
        #endregion

        #region 数据 唯一MD 企业名称+展会开始时间+展会名称[ee_md]
        string _ee_md;
        /// <summary>
        /// 数据 唯一MD 企业名称+展会开始时间+展会名称[ee_md]
        /// </summary>
        public string ee_md
        {
            get { return _ee_md; }
            set { _ee_md = value; }
        }
        #endregion

        #region 公司名[ee_company]
        string _ee_company;
        /// <summary>
        /// 公司名[ee_company]
        /// </summary>
        public string ee_company
        {
            get { return _ee_company; }
            set { _ee_company = value; }
        }
        #endregion

        #region 机构代码[ee_oc_code]
        string _ee_oc_code;
        /// <summary>
        /// 机构代码[ee_oc_code]
        /// </summary>
        public string ee_oc_code
        {
            get { return _ee_oc_code; }
            set { _ee_oc_code = value; }
        }
        #endregion

        #region 公司地址[ee_address]
        string _ee_address;
        /// <summary>
        /// 公司地址[ee_address]
        /// </summary>
        public string ee_address
        {
            get { return _ee_address; }
            set { _ee_address = value; }
        }
        #endregion

        #region 联系人[ee_contact]
        string _ee_contact;
        /// <summary>
        /// 联系人[ee_contact]
        /// </summary>
        public string ee_contact
        {
            get { return _ee_contact; }
            set { _ee_contact = value; }
        }
        #endregion

        #region 联系电话[ee_phone]
        string _ee_phone;
        /// <summary>
        /// 联系电话[ee_phone]
        /// </summary>
        public string ee_phone
        {
            get { return _ee_phone; }
            set { _ee_phone = value; }
        }
        #endregion

        #region 传真[ee_fax]
        string _ee_fax;
        /// <summary>
        /// 传真[ee_fax]
        /// </summary>
        public string ee_fax
        {
            get { return _ee_fax; }
            set { _ee_fax = value; }
        }
        #endregion

        #region 邮箱[ee_mail]
        string _ee_mail;
        /// <summary>
        /// 邮箱[ee_mail]
        /// </summary>
        public string ee_mail
        {
            get { return _ee_mail; }
            set { _ee_mail = value; }
        }
        #endregion

        #region 网址[ee_site]
        string _ee_site;
        /// <summary>
        /// 网址[ee_site]
        /// </summary>
        public string ee_site
        {
            get { return _ee_site; }
            set { _ee_site = value; }
        }
        #endregion

        #region 展厅[ee_exhBooth]
        string _ee_exhbooth;
        /// <summary>
        /// 展厅[ee_exhBooth]
        /// </summary>
        public string ee_exhBooth
        {
            get { return _ee_exhbooth; }
            set { _ee_exhbooth = value; }
        }
        #endregion

        #region 会刊年份[ee_year]
        int _ee_year;
        /// <summary>
        /// 会刊年份[ee_year]
        /// </summary>
        public int ee_year
        {
            get { return _ee_year; }
            set { _ee_year = value; }
        }
        #endregion

        #region 展会开始时间[ee_exhStartTime]
        string _ee_exhstarttime;
        /// <summary>
        /// 展会开始时间[ee_exhStartTime]
        /// </summary>
        public string ee_exhStartTime
        {
            get { return _ee_exhstarttime; }
            set { _ee_exhstarttime = value; }
        }
        #endregion

        #region 展会名称[ee_exhName]
        string _ee_exhname;
        /// <summary>
        /// 展会名称[ee_exhName]
        /// </summary>
        public string ee_exhName
        {
            get { return _ee_exhname; }
            set { _ee_exhname = value; }
        }
        #endregion

        #region 展会名称MD516[ee_namemd]
        string _ee_namemd;
        /// <summary>
        /// 展会名称MD516[ee_namemd]
        /// </summary>
        public string ee_namemd
        {
            get { return _ee_namemd; }
            set { _ee_namemd = value; }
        }
        #endregion

        #region 展会行业[ee_exhTrade]
        string _ee_exhtrade;
        /// <summary>
        /// 展会行业[ee_exhTrade]
        /// </summary>
        public string ee_exhTrade
        {
            get { return _ee_exhtrade; }
            set { _ee_exhtrade = value; }
        }
        #endregion

        #region 展会地区[ee_exhArea]
        string _ee_exharea;
        /// <summary>
        /// 展会地区[ee_exhArea]
        /// </summary>
        public string ee_exhArea
        {
            get { return _ee_exharea; }
            set { _ee_exharea = value; }
        }
        #endregion

        #region 展馆名称[ee_exhHall]
        string _ee_exhhall;
        /// <summary>
        /// 展馆名称[ee_exhHall]
        /// </summary>
        public string ee_exhHall
        {
            get { return _ee_exhhall; }
            set { _ee_exhhall = value; }
        }
        #endregion

        #region 会刊个数[ee_exhEntCount]
        int _ee_exhentcount;
        /// <summary>
        /// 会刊个数[ee_exhEntCount]
        /// </summary>
        public int ee_exhEntCount
        {
            get { return _ee_exhentcount; }
            set { _ee_exhentcount = value; }
        }
        #endregion

        #region 创建人[ee_createUser]
        string _ee_createuser;
        /// <summary>
        /// 创建人[ee_createUser]
        /// </summary>
        public string ee_createUser
        {
            get { return _ee_createuser; }
            set { _ee_createuser = value; }
        }
        #endregion

        #region 会刊创建时间[ee_exhCreateTime]
        DateTime _ee_exhcreatetime;
        /// <summary>
        /// 会刊创建时间[ee_exhCreateTime]
        /// </summary>
        public DateTime ee_exhCreateTime
        {
            get { return _ee_exhcreatetime; }
            set { _ee_exhcreatetime = value; }
        }
        #endregion

        #region 数据创建时间[ee_createTime]
        DateTime _ee_createtime;
        /// <summary>
        /// 数据创建时间[ee_createTime]
        /// </summary>
        public DateTime ee_createTime
        {
            get { return _ee_createtime; }
            set { _ee_createtime = value; }
        }
        #endregion

        #region 是否已经导入  0为未导入 1为已导入[ee_imported]
        bool _ee_imported;
        /// <summary>
        /// 是否已经导入  0为未导入 1为已导入[ee_imported]
        /// </summary>
        public bool ee_imported
        {
            get { return _ee_imported; }
            set { _ee_imported = value; }
        }
        #endregion

    }

}
