using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyBrandInfo
    {

        #region ob_id
        int _ob_id;
        /// <summary>
        /// ob_id
        /// </summary>
        public int ob_id
        {
            get { return _ob_id; }
            set { _ob_id = value; }
        }
        #endregion

        #region ob_oc_code
        string _ob_oc_code;
        /// <summary>
        /// ob_oc_code
        /// </summary>
        public string ob_oc_code
        {
            get { return _ob_oc_code; }
            set { _ob_oc_code = value; }
        }
        #endregion

        #region 注册/申请号[ob_regNo]
        string _ob_regno;
        /// <summary>
        /// 注册/申请号[ob_regNo]
        /// </summary>
        public string ob_regNo
        {
            get { return _ob_regno; }
            set { _ob_regno = value; }
        }
        #endregion

        #region 类号[ob_classNo]
        string _ob_classno;
        /// <summary>
        /// 类号[ob_classNo]
        /// </summary>
        public string ob_classNo
        {
            get { return _ob_classno; }
            set { _ob_classno = value; }
        }
        #endregion

        #region 申请日期[ob_applicationDate]
        DateTime _ob_applicationdate;
        /// <summary>
        /// 申请日期[ob_applicationDate]
        /// </summary>
        public DateTime ob_applicationDate
        {
            get { return _ob_applicationdate; }
            set { _ob_applicationdate = value; }
        }
        #endregion

        #region 申请人名称[ob_name]
        string _ob_name;
        /// <summary>
        /// 申请人名称[ob_name]
        /// </summary>
        public string ob_name
        {
            get { return _ob_name; }
            set { _ob_name = value; }
        }
        #endregion

        #region 申请人名称(中文)[ob_proposer]
        string _ob_proposer;
        /// <summary>
        /// 申请人名称(中文)[ob_proposer]
        /// </summary>
        public string ob_proposer
        {
            get { return _ob_proposer; }
            set { _ob_proposer = value; }
        }
        #endregion

        #region 申请人名称(英文)[ob_proposerEn]
        string _ob_proposeren;
        /// <summary>
        /// 申请人名称(英文)[ob_proposerEn]
        /// </summary>
        public string ob_proposerEn
        {
            get { return _ob_proposeren; }
            set { _ob_proposeren = value; }
        }
        #endregion

        #region 申请人地址(中文)[ob_proposerAddr]
        string _ob_proposeraddr;
        /// <summary>
        /// 申请人地址(中文)[ob_proposerAddr]
        /// </summary>
        public string ob_proposerAddr
        {
            get { return _ob_proposeraddr; }
            set { _ob_proposeraddr = value; }
        }
        #endregion

        #region 申请人地址(英文)[ob_proposerAddrEn]
        string _ob_proposeraddren;
        /// <summary>
        /// 申请人地址(英文)[ob_proposerAddrEn]
        /// </summary>
        public string ob_proposerAddrEn
        {
            get { return _ob_proposeraddren; }
            set { _ob_proposeraddren = value; }
        }
        #endregion

        #region 图片路径[ob_img]
        string _ob_img;
        /// <summary>
        /// 图片路径[ob_img]
        /// </summary>
        public string ob_img
        {
            get { return _ob_img; }
            set { _ob_img = value; }
        }
        #endregion

        #region ob_createTime
        DateTime _ob_createtime;
        /// <summary>
        /// ob_createTime
        /// </summary>
        public DateTime ob_createTime
        {
            get { return _ob_createtime; }
            set { _ob_createtime = value; }
        }
        #endregion

        #region ob_updateTime
        DateTime _ob_updatetime;
        /// <summary>
        /// ob_updateTime
        /// </summary>
        public DateTime ob_updateTime
        {
            get { return _ob_updatetime; }
            set { _ob_updatetime = value; }
        }
        #endregion

        #region 初审公告期号[ob_csggqh]
        string _ob_csggqh;
        /// <summary>
        /// 初审公告期号[ob_csggqh]
        /// </summary>
        public string ob_csggqh
        {
            get { return _ob_csggqh; }
            set { _ob_csggqh = value; }
        }
        #endregion

        #region 注册公告期号[ob_zcggqh]
        string _ob_zcggqh;
        /// <summary>
        /// 注册公告期号[ob_zcggqh]
        /// </summary>
        public string ob_zcggqh
        {
            get { return _ob_zcggqh; }
            set { _ob_zcggqh = value; }
        }
        #endregion

        #region 初审公告日期[ob_csggrq]
        string _ob_csggrq;
        /// <summary>
        /// 初审公告日期[ob_csggrq]
        /// </summary>
        public string ob_csggrq
        {
            get { return _ob_csggrq; }
            set { _ob_csggrq = value; }
        }
        #endregion

        #region 注册公告日期[ob_zcggrq]
        string _ob_zcggrq;
        /// <summary>
        /// 注册公告日期[ob_zcggrq]
        /// </summary>
        public string ob_zcggrq
        {
            get { return _ob_zcggrq; }
            set { _ob_zcggrq = value; }
        }
        #endregion

        #region 专用开始期限[ob_zyksqx]
        string _ob_zyksqx;
        /// <summary>
        /// 专用开始期限[ob_zyksqx]
        /// </summary>
        public string ob_zyksqx
        {
            get { return _ob_zyksqx; }
            set { _ob_zyksqx = value; }
        }
        #endregion

        #region 专用结束期限[ob_zyjsqx]
        string _ob_zyjsqx;
        /// <summary>
        /// 专用结束期限[ob_zyjsqx]
        /// </summary>
        public string ob_zyjsqx
        {
            get { return _ob_zyjsqx; }
            set { _ob_zyjsqx = value; }
        }
        #endregion

        #region 后期指定日期[ob_hqzdrq]
        string _ob_hqzdrq;
        /// <summary>
        /// 后期指定日期[ob_hqzdrq]
        /// </summary>
        public string ob_hqzdrq
        {
            get { return _ob_hqzdrq; }
            set { _ob_hqzdrq = value; }
        }
        #endregion

        #region 国际注册日期[ob_gjzcrq]
        string _ob_gjzcrq;
        /// <summary>
        /// 国际注册日期[ob_gjzcrq]
        /// </summary>
        public string ob_gjzcrq
        {
            get { return _ob_gjzcrq; }
            set { _ob_gjzcrq = value; }
        }
        #endregion

        #region 优先权日期[ob_yxqrq]
        string _ob_yxqrq;
        /// <summary>
        /// 优先权日期[ob_yxqrq]
        /// </summary>
        public string ob_yxqrq
        {
            get { return _ob_yxqrq; }
            set { _ob_yxqrq = value; }
        }
        #endregion

        #region 代理人名称[ob_dlrmc]
        string _ob_dlrmc;
        /// <summary>
        /// 代理人名称[ob_dlrmc]
        /// </summary>
        public string ob_dlrmc
        {
            get { return _ob_dlrmc; }
            set { _ob_dlrmc = value; }
        }
        #endregion

        #region 颜色组合[ob_yszh]
        string _ob_yszh;
        /// <summary>
        /// 颜色组合[ob_yszh]
        /// </summary>
        public string ob_yszh
        {
            get { return _ob_yszh; }
            set { _ob_yszh = value; }
        }
        #endregion

        #region 商标类型[ob_sblx]
        string _ob_sblx;
        /// <summary>
        /// 商标类型[ob_sblx]
        /// </summary>
        public string ob_sblx
        {
            get { return _ob_sblx; }
            set { _ob_sblx = value; }
        }
        #endregion

        #region 是否共有商标[ob_sfgysb]
        bool _ob_sfgysb;
        /// <summary>
        /// 是否共有商标[ob_sfgysb]
        /// </summary>
        public bool ob_sfgysb
        {
            get { return _ob_sfgysb; }
            set { _ob_sfgysb = value; }
        }
        #endregion
    }
}
