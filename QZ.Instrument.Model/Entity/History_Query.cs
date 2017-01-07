/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 * Paging select reply records that grouped by u_id and topic_id ordered by max(reply_date) desc
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class History_Query
    {
        public int query_id { get; set; }
        public string oc_code { get; set; }
        /// <summary>
        /// not the really oc_name, but the oc_name user input it into textbox
        /// </summary>
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string oc_area_name { get; set; }
        public string oc_number { get; set; }
        public string query_date { get; set; }
        public string oc_addr { get; set; }
        public string oc_reg_type { get; set; }
        public string oc_art_person { get; set; }
        public string oc_stock_holder { get; set; }
        public string oc_business { get; set; }
        public int q_type { get; set; }
        public int oc_sort { get; set; }
        public string oc_ext { get; set; }
        public string oc_reg_capital_floor { get; set; }
        public string oc_reg_capital_ceiling { get; set; }
    }
    public class Ext_SearchHistory
    {
        public List<string> query_list { get; set; }
        public byte q_type { get; set; }
        public static Ext_SearchHistory Default { get { return new Ext_SearchHistory { query_list = new List<string>() }; } }
    }

    public class Browse_Log
    {
        public int browse_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string browse_date { get; set; }
    }

    public class Favorite_Log
    {
        public int favorite_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string favorite_date { get; set; }
        public int g_gid { get; set; }
        public Favorite_Note favorite_note { get; set; }
    }

    public class Favorite_Group
    {
        public int g_gid { get; set; }
        public int u_uid { get; set; }
        public string g_name { get; set; }
        public int fl_count { get; set; }
    }

    public class Favorite_Note
    {
        public long n_id { get; set; }
        public string note { get; set; }
        public int fl_id { get; set; }
    }


    public class UNSPSC_CNInfo
    {
        public int id { get; set; }
        public string cate { get; set; }
        public string name_en { get; set; }
        public string name_cn { get; set; }
    }

    public class QZEmployInfo
    {
        public int id { get; set; }

        public string ep_code { get; set; }
        /// <summary>
        /// oc_name公司名称[ep_CompanyName]
        /// </summary>
        public string ep_CompanyName { get; set; }

        /// <summary>
        /// 发布时间[ep_Date]
        /// </summary>
        public string ep_Date { get; set; }

        /// <summary>
        /// 更新时间[ep_UpdateTime]
        /// </summary>
        public string ep_UpdateTime { get; set; }

        /// <summary>
        /// 职位名称[ep_Name]
        /// </summary>
        public string ep_Name { get; set; }

        /// <summary>
        /// 职能类别[ep_Type]
        /// </summary>
        public string ep_Type { get; set; }

        /// <summary>
        /// 职能关键字[ep_Keys]
        /// </summary>
        public string ep_Keys { get; set; }

        /// <summary>
        /// 薪资文本[ep_PriceTxt]
        /// </summary>
        public string ep_PriceTxt { get; set; }
        /// <summary>
        /// 最低薪资 面议0[ep_PriceL]
        /// </summary>
        public int ep_PriceL { get; set; }

        // <summary>
        /// 最高薪资 面议0[ep_PriceH]
        /// </summary>
        public int ep_PriceH { get; set; }

        /// <summary>
        /// 工作性质 全职[ep_Property]
        /// </summary>
        public string ep_Property { get; set; }

        /// <summary>
        /// 福利[ep_Welfare]
        /// </summary>
        public string ep_Welfare { get; set; }

        /// <summary>
        /// 年限要求[ep_YearsReq]
        /// </summary>
        public string ep_YearsReq { get; set; }

        /// <summary>
        /// 学历要求[ep_EduReq]
        /// </summary>
        public string ep_EduReq { get; set; }
        /// <summary>
        /// 招聘人数[ep_Count]
        /// </summary>
        public int ep_Count { get; set; }

        /// <summary>
        /// 职位职责[ep_duty]
        /// </summary>
        public string ep_duty { get; set; }

        /// </summary>
        public string ep_Des { get; set; }

        public string ep_City { get; set; }

        /// <summary>
        /// 工作地区[ep_Area]
        /// </summary>
        public string ep_Area { get; set; }

        /// <summary>
        /// 工作详细地址[ep_Addr]
        /// </summary>
        public string ep_Addr { get; set; }

        /// <summary>
        /// 采集网址[ep_Link]
        /// </summary>
        public string ep_Link { get; set; }

        /// <summary>
        /// 公司采集网址[ep_CLink]
        /// </summary>
        public string ep_CLink { get; set; }

        /// <summary>
        /// 采集平台[ep_PlatformName]
        /// </summary>
        public string ep_PlatformName { get; set; }

        /// <summary>
        /// 采集时间[ep_CollectTime]
        /// </summary>
        public string ep_CollectTime { get; set; }

        /// <summary>
        /// 采集ID[ep_CollectID]
        /// </summary>
        public string ep_CollectID { get; set; }

        /// <summary>
        /// 联系电话[ep_Phone]
        /// </summary>
        public string ep_Phone { get; set; }

        /// <summary>
        /// 搜索地区[ep_SearchCity]
        /// </summary>
        public string ep_SearchCity { get; set; }
        /// <summary>
        /// 搜索类目[ep_SearchClass]
        /// </summary>
        public string ep_SearchClass { get; set; }


    }

    public class QZEmployCompanyInfo
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

        #region oc_code[epc_code]
        string _epc_code;
        /// <summary>
        /// oc_code[epc_code]
        /// </summary>
        public string epc_code
        {
            get { return _epc_code; }
            set { _epc_code = value; }
        }
        #endregion

        #region oc_name公司名称[epc_CompanyName]
        string _epc_companyname;
        /// <summary>
        /// oc_name公司名称[epc_CompanyName]
        /// </summary>
        public string epc_CompanyName
        {
            get { return _epc_companyname; }
            set { _epc_companyname = value; }
        }
        #endregion

        #region 公司性质[epc_Nature]
        string _epc_nature;
        /// <summary>
        /// 公司性质[epc_Nature]
        /// </summary>
        public string epc_Nature
        {
            get { return _epc_nature; }
            set { _epc_nature = value; }
        }
        #endregion

        #region 更新时间[epc_UpdateTime]
        DateTime _epc_updatetime;
        /// <summary>
        /// 更新时间[epc_UpdateTime]
        /// </summary>
        public DateTime epc_UpdateTime
        {
            get { return _epc_updatetime; }
            set { _epc_updatetime = value; }
        }
        #endregion

        #region 公司规模[epc_Size]
        string _epc_size;
        /// <summary>
        /// 公司规模[epc_Size]
        /// </summary>
        public string epc_Size
        {
            get { return _epc_size; }
            set { _epc_size = value; }
        }
        #endregion

        #region 公司行业[epc_HY]
        string _epc_hy;
        /// <summary>
        /// 公司行业[epc_HY]
        /// </summary>
        public string epc_HY
        {
            get { return _epc_hy; }
            set { _epc_hy = value; }
        }
        #endregion

        #region 公司地址[epc_Addr]
        string _epc_addr;
        /// <summary>
        /// 公司地址[epc_Addr]
        /// </summary>
        public string epc_Addr
        {
            get { return _epc_addr; }
            set { _epc_addr = value; }
        }
        #endregion

        #region 公司简介[epc_About]
        string _epc_about;
        /// <summary>
        /// 公司简介[epc_About]
        /// </summary>
        public string epc_About
        {
            get { return _epc_about; }
            set { _epc_about = value; }
        }
        #endregion

        #region 公司联系人[epc_Cotacts]
        string _epc_cotacts;
        /// <summary>
        /// 公司联系人[epc_Cotacts]
        /// </summary>
        public string epc_Cotacts
        {
            get { return _epc_cotacts; }
            set { _epc_cotacts = value; }
        }
        #endregion

        #region 公司电话[epc_TEL]
        string _epc_tel;
        /// <summary>
        /// 公司电话[epc_TEL]
        /// </summary>
        public string epc_TEL
        {
            get { return _epc_tel; }
            set { _epc_tel = value; }
        }
        #endregion

        #region 公司邮箱[epc_Email]
        string _epc_email;
        /// <summary>
        /// 公司邮箱[epc_Email]
        /// </summary>
        public string epc_Email
        {
            get { return _epc_email; }
            set { _epc_email = value; }
        }
        #endregion

        #region 公司网站[epc_WebSite]
        string _epc_website;
        /// <summary>
        /// 公司网站[epc_WebSite]
        /// </summary>
        public string epc_WebSite
        {
            get { return _epc_website; }
            set { _epc_website = value; }
        }
        #endregion

        #region 采集平台[epc_PlatformName]
        string _epc_platformname;
        /// <summary>
        /// 采集平台[epc_PlatformName]
        /// </summary>
        public string epc_PlatformName
        {
            get { return _epc_platformname; }
            set { _epc_platformname = value; }
        }
        #endregion

        #region 采集时间[epc_CollectTime]
        DateTime _epc_collecttime;
        /// <summary>
        /// 采集时间[epc_CollectTime]
        /// </summary>
        public DateTime epc_CollectTime
        {
            get { return _epc_collecttime; }
            set { _epc_collecttime = value; }
        }
        #endregion

        #region 采集网址[epc_CLink]
        string _epc_clink;
        /// <summary>
        /// 采集网址[epc_CLink]
        /// </summary>
        public string epc_CLink
        {
            get { return _epc_clink; }
            set { _epc_clink = value; }
        }
        #endregion

        #region Logo路径[epc_Path]
        string _epc_path;
        /// <summary>
        /// Logo路径[epc_Path]
        /// </summary>
        public string epc_Path
        {
            get { return _epc_path; }
            set { _epc_path = value; }
        }
        #endregion

        #region Md5 16位[epc_Md5]
        string _epc_md5;
        /// <summary>
        /// Md5 16位[epc_Md5]
        /// </summary>
        public string epc_Md5
        {
            get { return _epc_md5; }
            set { _epc_md5 = value; }
        }
        #endregion

    }

    public class ZhiXingInfo
    {
        /// <summary>
        /// 标识Id[id]
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 执行人Id[zx_id]
        /// </summary>
        public int zx_id { get; set; }

        /// <summary>
        /// 案号[zx_caseCode]
        /// </summary>
        public string zx_caseCode { get; set; }

        /// <summary>
        /// 状态[zx_caseState]
        /// </summary>
        public string zx_caseState { get; set; }

        /// <summary>
        /// 执行法院名称[zx_execCourtName]
        /// </summary>
        public string zx_execCourtName { get; set; }

        /// <summary>
        /// 执行标的[zx_execMoney]
        /// </summary>
        public string zx_execMoney { get; set; }

        /// <summary>
        /// 身份证号码/组织机构代码[zx_partyCardNum]
        /// </summary>
        public string zx_partyCardNum { get; set; }

        /// <summary>
        /// 被执行人姓名/名称[zx_pname]
        /// </summary>
        public string zx_pname { get; set; }

        /// <summary>
        /// 立案时间[zx_caseCreateTime]
        /// </summary>
        public DateTime zx_caseCreateTime { get; set; }
        /// <summary>
        /// 创建时间[createTime]
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 更新时间[updateTime]
        /// </summary>
        public DateTime updateTime { get; set; }

        /// <summary>
        /// 数据状态 1.可用 0.不可用[status]
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 公司编码[oc_code]
        /// </summary>
        public string oc_code { get; set; }

    }

    public class ExhibitionEnterpriseInfo
    {
        /// <summary>
        /// 自增ID[ee_id]
        /// </summary>
        public int ee_id { get; set; }

        /// <summary>
        /// 数据 唯一MD 企业名称+展会开始时间+展会名称[ee_md]
        /// </summary>
        public string ee_md { get; set; }

        /// <summary>
        /// 公司名[ee_company]
        /// </summary>
        public string ee_company { get; set; }

        /// <summary>
        /// 机构代码[ee_oc_code]
        /// </summary>
        public string ee_oc_code { get; set; }

        /// <summary>
        /// 公司地址[ee_address]
        /// </summary>
        public string ee_address { get; set; }

        /// <summary>
        /// 联系人[ee_contact]
        /// </summary>
        public string ee_contact { get; set; }

        /// <summary>
        /// 联系电话[ee_phone]
        /// </summary>
        public string ee_phone { get; set; }

        /// <summary>
        /// 传真[ee_fax]
        /// </summary>
        public string ee_fax { get; set; }

        /// <summary>
        /// 邮箱[ee_mail]
        /// </summary>
        public string ee_mail { get; set; }

        /// <summary>
        /// 网址[ee_site]
        /// </summary>
        public string ee_site { get; set; }

        /// <summary>
        /// 展厅[ee_exhBooth]
        /// </summary>
        public string ee_exhBooth { get; set; }

        /// <summary>
        /// 会刊年份[ee_year]
        /// </summary>
        public int ee_year { get; set; }

        /// <summary>
        /// 展会开始时间[ee_exhStartTime]
        /// </summary>
        public string ee_exhStartTime { get; set; }

        /// <summary>
        /// 展会名称[ee_exhName]
        /// </summary>
        public string ee_exhName { get; set; }

        /// <summary>
        /// 展会名称MD516[ee_namemd]
        /// </summary>
        public string ee_namemd { get; set; }

        /// <summary>
        /// 展会行业[ee_exhTrade]
        /// </summary>
        public string ee_exhTrade { get; set; }

        /// <summary>
        /// 展会地区[ee_exhArea]
        /// </summary>
        public string ee_exhArea { get; set; }

        /// <summary>
        /// 展馆名称[ee_exhHall]
        /// </summary>
        public string ee_exhHall { get; set; }

        /// <summary>
        /// 会刊个数[ee_exhEntCount]
        /// </summary>
        public int ee_exhEntCount { get; set; }

        /// <summary>
        /// 创建人[ee_createUser]
        /// </summary>
        public string ee_createUser { get; set; }

        /// <summary>
        /// 会刊创建时间[ee_exhCreateTime]
        /// </summary>
        public DateTime ee_exhCreateTime { get; set; }

        /// <summary>
        /// 数据创建时间[ee_createTime]
        /// </summary>
        public DateTime ee_createTime { get; set; }

        /// <summary>
        /// 是否已经导入  0为未导入 1为已导入[ee_imported]
        /// </summary>
        public bool ee_imported { get; set; }
    }

    public class ShixinInfo
    {
        /// <summary>
        /// 自增ID[id]
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 案件id[sx_id]
        /// </summary>
        public int sx_id { get; set; }

        /// <summary>
        /// 被执行人姓名/名称[sx_iname]
        /// </summary>
        public string sx_iname { get; set; }

        /// <summary>
        /// 案号[sx_caseCode]
        /// </summary>
        public string sx_caseCode { get; set; }

        /// <summary>
        /// 身份证号码/组织机构代码[sx_cardNum]
        /// </summary>
        public string sx_cardNum { get; set; }

        /// <summary>
        /// 法定代表人或者负责人姓名[sx_businessEntity]
        /// </summary>
        public string sx_businessEntity { get; set; }

        /// <summary>
        /// 执行法院[sx_courtName]
        /// </summary>
        public string sx_courtName { get; set; }

        /// <summary>
        /// 省份[sx_areaName]
        /// </summary>
        public string sx_areaName { get; set; }

        /// <summary>
        /// sx_partyTypeName
        /// </summary>
        public string sx_partyTypeName { get; set; }

        /// <summary>
        /// 执行依据文号[sx_gistId]
        /// </summary>
        public string sx_gistId { get; set; }

        /// <summary>
        /// 立案时间[sx_regDate]
        /// </summary>
        public DateTime sx_regDate { get; set; }

        /// <summary>
        /// 做出执行依据单位[sx_gistUnit]
        /// </summary>
        public string sx_gistUnit { get; set; }

        /// <summary>
        /// 生效法律文书确定的义务[sx_duty]
        /// </summary>
        public string sx_duty { get; set; }

        /// <summary>
        /// 被执行人的履行情况[sx_performance]
        /// </summary>
        public string sx_performance { get; set; }

        /// <summary>
        /// 失信被执行人行为具体情形[sx_disruptTypeName]
        /// </summary>
        public string sx_disruptTypeName { get; set; }

        /// <summary>
        /// 发布时间[sx_publishDate]
        /// </summary>
        public DateTime sx_publishDate { get; set; }

        /// <summary>
        /// 创建时间[createTime]
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 更新时间[updateTime]
        /// </summary>
        public DateTime updateTime { get; set; }

        /// <summary>
        /// 是否隐藏[isHidden]
        /// </summary>
        public bool isHidden { get; set; }

    }

    public class SystemNoticeInfo
    {
        /// <summary>
        /// s_id
        /// </summary>
        public int s_id { get; set; }

        /// <summary>
        /// 标题[s_title]
        /// </summary>
        public string s_title { get; set; }

        /// <summary>
        /// 系统通知文本包含html等[s_content]
        /// </summary>
        public string s_content { get; set; }

        /// <summary>
        /// 系统通知类型 0系统消息 1... 2...[s_type]
        /// </summary>
        public int s_type { get; set; }

        /// <summary>
        /// 是否屏蔽黑名单用户 0否1是[s_isblack]
        /// </summary>
        public bool s_isblack { get; set; }

        /// <summary>
        /// 发布时间[s_date]
        /// </summary>
        public DateTime s_date { get; set; }

        /// <summary>
        /// 是否有效[s_isvalid]
        /// </summary>
        public bool s_isvalid { get; set; }

        /// <summary>
        /// 用户是否已读
        /// </summary>
        public bool isread { get; set; }
    }

    public class SystemNoticeByUserInfo
    {
        public int id { get; set; }

        /// <summary>
        /// 通知id[s_id]
        /// </summary>
        public int s_id { get; set; }
        /// <summary>
        /// userid
        /// </summary>
        public int userid { get; set; }

        /// <summary>
        /// 是否已读 true已读 false未读[isread]
        /// </summary>
        public bool isread { get; set; }

        /// <summary>
        /// 是否删除 0否1是[isdel]
        /// </summary>
        public bool isdel { get; set; }

        /// <summary>
        /// 创建时间[createtime]
        /// </summary>
        public DateTime createtime { get; set; }

    }



}
