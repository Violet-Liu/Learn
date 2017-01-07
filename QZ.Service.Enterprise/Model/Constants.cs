/**
 * `Constants` contains some constants value used in Server end
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Service.Enterprise
{
    public class Constants
    {
        #region app setting key in config file
        // C-Client / S-Server
        // config name of initial AES cipher key
        public const string C_Dyn_0 = "C_Dyn_0";
        public const string C_Dyn = "C_Dyn";
        public const string S_Dyn = "S_Dyn";
        public const string S_Tok = "S_Tok";
        public const string S_Msg = "S_Msg";
        #endregion

        #region file path
        public const string Service_Path = @"~/Service.config";
        #endregion

        #region database connection key
        public const string QZBase_Db_Key = "QZBase";
        public const string QZBase166_Db_Key = "QZBase166";
        public const string QZBase159_Db_Key = "QZBase159";
        public const string QZOrgCompany_Db_Key = "QZOrgCompany";
        public const string QZOrgCompanyApp_Db_Key = "QZOrgCompanyApp";
        public const string CompanyStatisticsInfoTwo = "CompanyStatisticsInfoTwo";
        public const string QZOrgCompanyAppLog_Db_Key = "QZOrgCompanyAppLog";
        public const string QZOrgCompanyGsxt_Db_Key = "QZOrgCompanyGsxt";
        public const string QZOrgCompanyExtension = "QZOrgCompanyExtension";
        public const string QZBrand_Db_Key = "QZBrand";
        public const string QZProperty_Db_Key = "QZProperty";
        public const string QZNewSite_User_Db_Key = "QZNewSite_User";
        public const string SysLog_App_Db_Key = "SysLog_App";
        public const string SysLog_Db_Key = "SysLog";
        public const string QZNewSite_Db_Key = "QZNewSite";
        public const string QZNewSite_ULogs_Db_Key = "QZNewSite_ULogs";
        public const string QZNewSite_News_Db_Key = "QZNewSite_News";
        public const string QZPatent = "QZPatent";
        public const string QZCourt = "QZCourt";
        public const string QZOrgCertificate = "QZOrgCertificate";
        public const string QZOrgGS1 = "QZOrgGS1";
        public const string FavoriteGroup = "FavoriteGroup";
        public const string QZEmploy = "QZEmploy";
        public const string QZProperty = "QZProperty";
        #endregion

        #region user operation
        public const string Op_Cmt_TipOff = "举报评论";
        public const string Op_Ent_Query = "搜索企业";    
        public const string Op_Map_Get = "获取公司图谱";
        public const string Op_Oc_Topic_Add = "发表公司帖";
        public const string Op_Oc_Reply_Add = "发表公司帖回复";
        public const string Op_Company_Correct = "公司纠错";
        public const string Op_Brand_Query = "搜索商标";
        public const string Op_Patent_Query = "搜索专利";
        public const string Op_Exhibit_Query = "搜索展会";
        public const string Op_Judge_Query = "搜索判决";
        public const string Op_Dishonest_Query = "搜索失信";
        public const string Op_Oc_Updown_Vote = "公司点赞/点踩";
        public const string Op_Oc_Topic_Updown_Vote = "公司帖点赞/点踩";
        public const string Op_Oc_Report_Send = "发送公司报告";
        public const string Op_Oc_Favorite_Add = "添加公司收藏";
        public const string Op_Oc_Favorite_Remove = "删除单条公司收藏";
        public const string Op_Query_History_Remove = "删除单条搜索记录";
        public const string Op_Query_History_Drop = "删除用户所有搜索记录";
        public const string Op_Oc_Browse_Remove = "删除单条浏览记录";
        public const string Op_Oc_Browse_Drop = "删除用户所有浏览记录";
        public const string Op_Cm_Topic_Add = "发表社区帖";
        public const string Op_Cm_Reply_Add = "发表社区帖回复";
        public const string Op_Cm_Topic_Updown_Vote = "社区帖点赞/点踩";
        public const string Op_Trade_Search = "按行业查询";
        public const string Op_Exhibit_List = "获取公司参展列表";
        [Obsolete]
        public const string Op_Company_Score_Mark = "";
        public const string Favorite_Group_Insert = "添加收藏分组";
        public const string Favorite_Group_Del = "删除用户分组";
        public const string Favorite_Group_Update = "修改分组名";
        public const string op_AliPay_VipOrder_Submit = "提交支付宝订单信息返回预付单及签名";
        public const string op_WX_VipOrder_Submit = "提交微信订单信息返回预付单及签名";
        public const string op_Alipay_VipOrder_Notify = "提交订单信息支付成功同步回调修改订单状态";
        #endregion

        #region servcie config name
        public const string Topic_Img_Key = "Upload_TopicPic_Path";
        #endregion

        #region news_catagory
        public const string OrgCompany_Areas = "OrgCompany_Areas";//缓存全部的地区
        public const string CN_NewsCates = "NewsCates"; // 新闻大分类
        public const string NewsCate_Corp = "NewsCateCorp";
        public const string CN_NewsCates_All = "AllNewsCates";  // 所有新闻分类
        public const string CN_News_Info = "NewsInfo";  // 文章信息
        public const string CN_CMSBlocks = "CMSBlocks"; // CMSBlock
        public const string CMSItems_n_id = "CMSItems_n_id"; // 后台推送某条新闻的ID

        #endregion

        #region vip 
        public const string vipUser_uid_key = "orgCompanyMvc_vipUser_uid_all";
        #endregion
        public const string Trades_Cache_Id = "Trades_Cache_Id";


        public static Dictionary<string, string> Company_Field_Comment_Map = new Dictionary<string, string>
        {
            ["oc_brands"] = "商标",
            ["oc_gds"] = "股东",
            ["oc_members"] = "成员",
            ["oc_faren"] = "法人"
        };

        public static IDictionary<string, string> AreaMap = new Dictionary<string, string>
        {
            ["11"] = "北京",
            ["12"] = "天津",
            ["13"] = "河北",
            ["14"] = "山西",
            ["15"] = "内蒙古",
            ["21"] = "辽宁",
            ["22"] = "吉林",
            ["23"] = "黑龙江",
            ["31"] = "上海",
            ["32"] = "江苏",
            ["33"] = "浙江",
            ["34"] = "安徽",
            ["35"] = "福建",
            ["36"] = "江西",
            ["37"] = "山东",
            ["41"] = "河南",
            ["42"] = "湖北",
            ["43"] = "湖南",
            ["44"] = "广东",
            ["45"] = "广西",
            ["46"] = "海南",
            ["50"] = "重庆",
            ["51"] = "四川",
            ["52"] = "贵州",
            ["53"] = "云南",
            ["54"] = "西藏",
            ["61"] = "陕西",
            ["62"] = "甘肃",
            ["63"] = "青海",
            ["64"] = "宁夏",
            ["65"] = "新疆",
            ["71"] = "台湾",
            ["81"] = "香港",
            ["82"] = "澳门"
        };

        public static IDictionary<string, string> CompanyStatusMap = new Dictionary<string, string>
        {
            ["2"] = "存续",
            ["3"] = "迁出",
            ["4"] = "吊销",
            ["5"] = "注销",
            ["6"] = "停业",
            ["7"] = "解散",
            ["8"] = "清算",
            ["1"] = "在业",
            ["0"] = "未知"
        };


    }
}
