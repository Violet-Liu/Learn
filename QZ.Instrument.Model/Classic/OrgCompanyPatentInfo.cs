using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyPatentInfo
    {
        #region ID[ID]
        int _id;
        /// <summary>
        /// ID[ID]
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region oc_code[oc_code]
        string _oc_code;
        /// <summary>
        /// oc_code[oc_code]
        /// </summary>
        public string oc_code
        {
            get { return _oc_code; }
            set { _oc_code = value; }
        }
        #endregion

        #region oc_name公司名称[oc_name]
        string _oc_name;
        /// <summary>
        /// oc_name公司名称[oc_name]
        /// </summary>
        public string oc_name
        {
            get { return _oc_name; }
            set { _oc_name = value; }
        }
        #endregion

        #region 申请号[Patent_No]
        string _patent_no;
        /// <summary>
        /// 申请号[Patent_No]
        /// </summary>
        public string Patent_No
        {
            get { return _patent_no; }
            set { _patent_no = value; }
        }
        #endregion

        #region 申请日[Patent_Day]
        string _patent_day;
        /// <summary>
        /// 申请日[Patent_Day]
        /// </summary>
        public string Patent_Day
        {
            get { return _patent_day; }
            set { _patent_day = value; }
        }
        #endregion

        #region 公开号[Patent_gkh]
        string _patent_gkh;
        /// <summary>
        /// 公开号[Patent_gkh]
        /// </summary>
        public string Patent_gkh
        {
            get { return _patent_gkh; }
            set { _patent_gkh = value; }
        }
        #endregion

        #region 公开日[Patent_gkr]
        string _patent_gkr;
        /// <summary>
        /// 公开日[Patent_gkr]
        /// </summary>
        public string Patent_gkr
        {
            get { return _patent_gkr; }
            set { _patent_gkr = value; }
        }
        #endregion

        #region 发明名称[Patent_Name]
        string _patent_name;
        /// <summary>
        /// 发明名称[Patent_Name]
        /// </summary>
        public string Patent_Name
        {
            get { return _patent_name; }
            set { _patent_name = value; }
        }
        #endregion

        #region 发明(设计)人[Patent_sjr]
        string _patent_sjr;
        /// <summary>
        /// 发明(设计)人[Patent_sjr]
        /// </summary>
        public string Patent_sjr
        {
            get { return _patent_sjr; }
            set { _patent_sjr = value; }
        }
        #endregion

        #region 专利类型[Patent_Type]
        string _patent_type;
        /// <summary>
        /// 专利类型[Patent_Type]
        /// </summary>
        public string Patent_Type
        {
            get { return _patent_type; }
            set { _patent_type = value; }
        }
        #endregion

        #region 优先权[Patent_yxq]
        string _patent_yxq;
        /// <summary>
        /// 优先权[Patent_yxq]
        /// </summary>
        public string Patent_yxq
        {
            get { return _patent_yxq; }
            set { _patent_yxq = value; }
        }
        #endregion

        #region 优先权日[Patent_yxqr]
        string _patent_yxqr;
        /// <summary>
        /// 优先权日[Patent_yxqr]
        /// </summary>
        public string Patent_yxqr
        {
            get { return _patent_yxqr; }
            set { _patent_yxqr = value; }
        }
        #endregion

        #region 外观设计珞珈诺分类号[Patent_lknflh]
        string _patent_lknflh;
        /// <summary>
        /// 外观设计珞珈诺分类号[Patent_lknflh]
        /// </summary>
        public string Patent_lknflh
        {
            get { return _patent_lknflh; }
            set { _patent_lknflh = value; }
        }
        #endregion

        #region 代理人[Patent_dlr]
        string _patent_dlr;
        /// <summary>
        /// 代理人[Patent_dlr]
        /// </summary>
        public string Patent_dlr
        {
            get { return _patent_dlr; }
            set { _patent_dlr = value; }
        }
        #endregion

        #region 代理机构[Patent_dljg]
        string _patent_dljg;
        /// <summary>
        /// 代理机构[Patent_dljg]
        /// </summary>
        public string Patent_dljg
        {
            get { return _patent_dljg; }
            set { _patent_dljg = value; }
        }
        #endregion

        #region 法律状态[Patent_Status]
        string _patent_status;
        /// <summary>
        /// 法律状态[Patent_Status]
        /// </summary>
        public string Patent_Status
        {
            get { return _patent_status; }
            set { _patent_status = value; }
        }
        #endregion

        #region 申请人[Patent_sqr]
        string _patent_sqr;
        /// <summary>
        /// 申请人[Patent_sqr]
        /// </summary>
        public string Patent_sqr
        {
            get { return _patent_sqr; }
            set { _patent_sqr = value; }
        }
        #endregion

        #region 地址[Patent_Addr]
        string _patent_addr;
        /// <summary>
        /// 地址[Patent_Addr]
        /// </summary>
        public string Patent_Addr
        {
            get { return _patent_addr; }
            set { _patent_addr = value; }
        }
        #endregion

        #region 国（省）[Patent_City]
        string _patent_city;
        /// <summary>
        /// 国（省）[Patent_City]
        /// </summary>
        public string Patent_City
        {
            get { return _patent_city; }
            set { _patent_city = value; }
        }
        #endregion

        #region 邮编[Patent_PostCode]
        string _patent_postcode;
        /// <summary>
        /// 邮编[Patent_PostCode]
        /// </summary>
        public string Patent_PostCode
        {
            get { return _patent_postcode; }
            set { _patent_postcode = value; }
        }
        #endregion

        #region 主分类号[Patent_zflh]
        string _patent_zflh;
        /// <summary>
        /// 主分类号[Patent_zflh]
        /// </summary>
        public string Patent_zflh
        {
            get { return _patent_zflh; }
            set { _patent_zflh = value; }
        }
        #endregion

        #region 分类号[Patent_flh]
        string _patent_flh;
        /// <summary>
        /// 分类号[Patent_flh]
        /// </summary>
        public string Patent_flh
        {
            get { return _patent_flh; }
            set { _patent_flh = value; }
        }
        #endregion

        #region 颁证日[Patent_bzr]
        string _patent_bzr;
        /// <summary>
        /// 颁证日[Patent_bzr]
        /// </summary>
        public string Patent_bzr
        {
            get { return _patent_bzr; }
            set { _patent_bzr = value; }
        }
        #endregion

        #region 国际申请[Patent_gjsq]
        string _patent_gjsq;
        /// <summary>
        /// 国际申请[Patent_gjsq]
        /// </summary>
        public string Patent_gjsq
        {
            get { return _patent_gjsq; }
            set { _patent_gjsq = value; }
        }
        #endregion

        #region 国际公布[Patent_gjgb]
        string _patent_gjgb;
        /// <summary>
        /// 国际公布[Patent_gjgb]
        /// </summary>
        public string Patent_gjgb
        {
            get { return _patent_gjgb; }
            set { _patent_gjgb = value; }
        }
        #endregion

        #region 进入国家日期[Patent_jrgjrq]
        string _patent_jrgjrq;
        /// <summary>
        /// 进入国家日期[Patent_jrgjrq]
        /// </summary>
        public string Patent_jrgjrq
        {
            get { return _patent_jrgjrq; }
            set { _patent_jrgjrq = value; }
        }
        #endregion

        #region 专利主分类号[Patent_zlflh]
        string _patent_zlflh;
        /// <summary>
        /// 专利主分类号[Patent_zlflh]
        /// </summary>
        public string Patent_zlflh
        {
            get { return _patent_zlflh; }
            set { _patent_zlflh = value; }
        }
        #endregion

        #region 摘要[Patent_zy]
        string _patent_zy;
        /// <summary>
        /// 摘要[Patent_zy]
        /// </summary>
        public string Patent_zy
        {
            get { return _patent_zy; }
            set { _patent_zy = value; }
        }
        #endregion

        #region 主权项[Patent_zqx]
        string _patent_zqx;
        /// <summary>
        /// 主权项[Patent_zqx]
        /// </summary>
        public string Patent_zqx
        {
            get { return _patent_zqx; }
            set { _patent_zqx = value; }
        }
        #endregion

        #region 图片[Patent_img]
        string _patent_img;
        /// <summary>
        /// 图片[Patent_img]
        /// </summary>
        public string Patent_img
        {
            get { return _patent_img; }
            set { _patent_img = value; }
        }
        #endregion

        #region 更新时间[Patent_LastUpdate]
        DateTime _patent_lastupdate;
        /// <summary>
        /// 更新时间[Patent_LastUpdate]
        /// </summary>
        public DateTime Patent_LastUpdate
        {
            get { return _patent_lastupdate; }
            set { _patent_lastupdate = value; }
        }
        #endregion

        #region 下次更新时间[Patent_NextUpdate]
        DateTime _patent_nextupdate;
        /// <summary>
        /// 下次更新时间[Patent_NextUpdate]
        /// </summary>
        public DateTime Patent_NextUpdate
        {
            get { return _patent_nextupdate; }
            set { _patent_nextupdate = value; }
        }
        #endregion

        #region 字典记录[Patent_DicRecord]
        string _patent_dicrecord;
        /// <summary>
        /// 字典记录[Patent_DicRecord]
        /// </summary>
        public string Patent_DicRecord
        {
            get { return _patent_dicrecord; }
            set { _patent_dicrecord = value; }
        }
        #endregion

        #region 其它数据[Patent_OtherData]
        string _patent_otherdata;
        /// <summary>
        /// 其它数据[Patent_OtherData]
        /// </summary>
        public string Patent_OtherData
        {
            get { return _patent_otherdata; }
            set { _patent_otherdata = value; }
        }
        #endregion

        #region 获取年份[GetYear]
        int _getyear;
        /// <summary>
        /// 获取年份[GetYear]
        /// </summary>
        public int GetYear
        {
            get { return _getyear; }
            set { _getyear = value; }
        }
        #endregion

        #region 获取页码[GetPage]
        int _getpage;
        /// <summary>
        /// 获取页码[GetPage]
        /// </summary>
        public int GetPage
        {
            get { return _getpage; }
            set { _getpage = value; }
        }
        #endregion

        #region 获取行号[GetRowNum]
        int _getrownum;
        /// <summary>
        /// 获取行号[GetRowNum]
        /// </summary>
        public int GetRowNum
        {
            get { return _getrownum; }
            set { _getrownum = value; }
        }
        #endregion

        #region [StatusProcess]
        string _statusProcess;
        /// <summary>
        /// 获取行号[StatusProcess]
        /// </summary>
        public string StatusProcess
        {
            get { return _statusProcess; }
            set { _statusProcess = value; }
        }
        #endregion
    }
}
