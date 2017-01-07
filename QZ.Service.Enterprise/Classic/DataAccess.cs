using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QZ.Foundation.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.DataAccess;

namespace QZ.Service.Enterprise
{
    public class DataAccess
    {
        public static T Dispose_Wrap<T>(string db_key, Func<QZOrgCompanyAppAccess, T> func, T exception = default(T))
        {
            using (var access = new QZOrgCompanyAppAccess(db_key))
            {
                try
                {
                    return func(access);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void ErrorLog_Insert(LogError log) =>
            Dispose_Wrap(Constants.QZOrgCompanyAppLog_Db_Key, access => access.ErrorLog_Insert(log));
        #region search_history
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="flag">user login, true</param>
        /// <returns></returns>
        public static int SearchHistory_Insert(Company c, bool flag) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key,
                         access => flag ? access.SearchHistory_Slave_Insert(c, Sp_Name_Set.SearchHistory_S_Insert) : access.SearchHistory_Master_Insert(c, Sp_Name_Set.SearchHistory_M_Insert));
        public static string District_Code_Get(string oc_area) =>
            Dispose_Wrap(Constants.QZBase_Db_Key, access => access.District_Code_Get(oc_area, Sp_Name_Set.District_Code_Get));


        public static int SearchHistoryExt_Insert(Req_Info_Query query, bool flag) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key,
                access => flag ? access.SearchHistoryExt_Slave_Insert(query, Sp_Name_Set.SearchHistoryExt_S_Insert) : access.SearchHistoryExt_Master_Insert(query, Sp_Name_Set.SearchHistoryExt_M_Insert));
        public static Judge_Dtl Judge_Dtl_Query(string id) =>
            Dispose_Wrap(Constants.QZCourt, access => access.Judge_Dtl_Query(new Guid(id), Sp_Name_Set.Judge_Dtl_Query));
        public static Dishonest_Dtl Dishonest_Dtl_Query(int id) => Dispose_Wrap(Constants.QZCourt, access => access.Dishonest_Dtl_Query(id, Sp_Name_Set.Dishonest_Dtl_Query));
        public static Patent_Dtl Patent_Dtl_Get(int id) => Dispose_Wrap(Constants.QZPatent, access => access.Patent_Dtl_Get(id, Sp_Name_Set.Patent_Dtl_Get));
        public static Patent_Dtl Patent_Dtl_Get(string p_no, string m_cat) => Dispose_Wrap(Constants.QZPatent, access => access.Patent_Dtl_FromNoCat_Get(p_no, m_cat, Sp_Name_Set.Patent_Dtl_FromNoCat_Select));
        public static Brand_Dtl Brand_Dtl_Get(int id) => Dispose_Wrap(Constants.QZBrand_Db_Key, access => access.Brand_Dtl_Get(id, Sp_Name_Set.Brand_Dtl_Get));
        public static Brand_Dtl Brand_Dtl_Get(string regno, string classno) => Dispose_Wrap(Constants.QZBrand_Db_Key, access => access.Brand_Dtl_FromRegClass_Get(regno, classno, Sp_Name_Set.Brand_Dtl_Select));
        #endregion

        #region area
        public static List<Province> Area_Get() =>
            Dispose_Wrap(Constants.QZBase_Db_Key, access => access.Area_Get(Sp_Name_Set.Area_Get));
        #endregion

        #region user app operation log
        public static int AppOrgCompanyLog_Insert(AppOrgCompanyLogInfo log) =>
            Dispose_Wrap(Constants.QZOrgCompanyAppLog_Db_Key, access => access.AppOrgCompanyLog_Insert(log, Sp_Name_Set.AppOrgCompanyLog_Insert));
        #endregion

        #region company blacklist
        public static bool CompanyBlackList_Exist_ByCode(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyBlackList_Exist_ByCode(oc_code, Sp_Name_Set.CompanyBlackList_Select_ByCode));

        public static bool CompanyBlackList_Exist_ByName(string oc_name) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyBlackList_Exist_ByName(oc_name, Sp_Name_Set.CompanyBlackList_Select_ByName));
        #endregion

        #region browse log
        public static int BrowseLog_Insert(BrowseLogInfo log) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key,
                         access => log.bl_u_uid > 0 ? access.BrowseLog_Slave_Insert(log, Sp_Name_Set.BrowseLog_S_Insert) : access.BrowseLog_Master_Insert(log, Sp_Name_Set.BrowseLog_M_Insert));

        public static List<string> Browses_Fresh_Get(int count) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Browses_Fresh_Get(count, Sp_Name_Set.Browses_Fresh_Get));
        #endregion

        #region company evaluation
        public static CompanyEvaluateInfo CompanyEvaluate_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyEvaluate_Select(oc_code, Sp_Name_Set.CompanyEvaluate_Select));

        public static int CompanyEvaluate_Update(CompanyEvaluateInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyEvaluate_Update(info, Sp_Name_Set.CompanyEvaluate_Update));

        #endregion

        public static List<TradeEntity> TradeCategory_AllSelect() =>
            Dispose_Wrap(Constants.QZBase_Db_Key, access => access.TradeCategory_AllSelect(Sp_Name_Set.GB_Trade_AllSelect));

        public static List<ProductEntity> ProductCategory_AllSelect() =>
            Dispose_Wrap(Constants.QZBase_Db_Key, access => access.ProductCategory_AllSelect(Sp_Name_Set.Product_Cat_AllSelect));
        public static List<OrgCompanyListInfo> OrgCompanyList_Page_Select(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyList_Page_Select(s, Sp_Name_Set.OrgCompanyList_Page_Select));
        public static List<OrgCompanyDtlInfo> OrgCompanyDtl_Page_Select(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtl_Page_Select(s, Sp_Name_Set.OrgCompanyDtl_Page_Select));

        public static OrgCompanyDtlInfo OrgCompanyDtl_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtl_Select(oc_code, Sp_Name_Set.OrgCompanyDtl_Select));

        public static OrgCompanyDtlInfo New_OrgCompanyDtl_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtl_Selectbyod_oc_code(oc_code));

        public static Tuple<OrgCompanyDtlInfo, OrgCompanyListInfo> OrgCompanyDetails_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => {
                var dtl = access.OrgCompanyDtl_Select(oc_code, Sp_Name_Set.OrgCompanyDtl_Select);
                var lst = access.OrgCompanyList_Select(oc_code, Sp_Name_Set.OrgCompanyList_Select);
                return new Tuple<OrgCompanyDtlInfo, OrgCompanyListInfo>(dtl, lst);
            });

        public static OrgCompanyListInfo OrgCompanyList_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyList_Select(oc_code, Sp_Name_Set.OrgCompanyList_Select));

        /// <summary>
        /// Get company member infos
        /// It's used when query member infos of a company in shenzhen city
        /// </summary>
        /// <param name="oc_code"></param>
        /// <returns></returns>
        public static List<OrgCompanyDtlMgrInfo> OrgCompanyDtlMgr_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtlMgr_Select(oc_code, Sp_Name_Set.OrgCompanyDtlMgr_Select));
        public static List<OrgCompanyGsxtDtlMgrInfo> OrgCompanyGsxtDtlMgr_Page_Select(DatabaseSearchModel s, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyGsxtDtlMgr_Page_Select(s, oc_area, Sp_Name_Set.OrgCompanyGsxtDtlMgr_Select));
        public static List<OrgCompanyDtlGDInfo> OrgCompanyDtlGD_FromOccode_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtlGD_FromOccode_Select(oc_code, Sp_Name_Set.OrgCompanyDtlGD_Select));
        public static List<OrgCompanyGsxtDtlGDInfo> OrgCompanyGsxtDtlGD_Page_Select(DatabaseSearchModel s, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyGsxtDtlGD_Page_Select(s, oc_area, Sp_Name_Set.OrgCompanyGsxtDtlGD_Select));
        public static List<Resp_Oc_Abs> CompanyList_Buld_Get(List<string> codes) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.CompanyList_Buld_Get(codes, Sp_Name_Set.OrgCompanyList_Select));

        #region company members
        public static List<Company_Member> Company_Member_Select(string oc_code, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access =>
            {
                if (oc_area.StartsWith("4403"))                 // query member infos of a company in shenzhen city
                    return access.CompanyMember_Select(oc_code, Sp_Name_Set.OrgCompanyDtlMgr_Select);

                if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81"))
                {
                    var s = new DatabaseSearchModel<OrgCompanyGsxtDtlMgrInfo>().SetOrderField(m => m.om_id).Ascend(true).SetPageSize(100).SetWhereClause($"om_oc_code = '{oc_code}'");
                    return access.CompanyMember_Select(oc_area.Substring(0, 2), Sp_Name_Set.OrgCompanyGsxtDtlMgr_Select, s);
                }
                return null;
            });
        #endregion

        #region company stock holder
        public static List<Company_Sh> Company_Sh_Get(string oc_code, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access =>
            {
                if (oc_area.StartsWith("4403"))
                    return access.Company_Sh_Select(oc_code, Sp_Name_Set.OrgCompanyDtlGD_Select);
                else if (!oc_area.StartsWith("71") && !oc_area.StartsWith("81"))
                    return access.Company_Sh_Select(oc_area.Substring(0, 2), "*", $"where og_oc_code = '{oc_code}'", "og_int asc", 1, 100, Sp_Name_Set.OrgCompanyGsxtDtlGD_Select);

                return new List<Company_Sh>();
            });
        #endregion
        public static Tuple<Dictionary<string, string>, Dictionary<string, string>> OrgCompany_Tel_Get(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.OrgCompany_Tel_Get(search, Sp_Name_Set.Table_PageSelect));


        public static List<OrgCompanyDtl_EvtInfo> OrgCompanyDtl_Evt_Select(DatabaseSearchModel<OrgCompanyDtl_EvtInfo> search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key,
                         access => access.OrgCompanyDtl_Evt_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.OrgCompanyDtl_Evt_Select));

        public static List<OrgCompanyGsxtBgsxInfo> OrgCompanyGsxtBgsx_Select(DatabaseSearchModel<OrgCompanyGsxtBgsxInfo> search, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key,
                         access => access.OrgCompanyGsxtBgsx_Select(oc_area, search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.OrgCompanyGsxtBgsx_Select));

        public static List<OrgCompanyBrand_Dtl> OrgCompanyBrand_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key,
                access => access.OrgCompanyBrand_Select(oc_code, Sp_Name_Set.OrgCompanyBrand_FromCode_Select));

        public static List<OrgCompanyBrand> OrgCompanyBrand_Page_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZBrand_Db_Key,
                access => access.OrgCompanyBrand_Page_Select(search, Sp_Name_Set.Table_PageSelect));

        public static List<CompanyPatent> OrgCompanyPatent_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZPatent,
                access => access.OrgCompanyPatent_Select(oc_code, Sp_Name_Set.OrgCompanyPatent_FromCode_Select));

        public static List<CompanyPatent> OrgCompanyPatent_Page_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZPatent,
                access => access.OrgCompanyPatent_Page_Select(search, Sp_Name_Set.Table_PageSelect));
        public static int OrgCompanyPatent_GetByoccode(string oc_code) =>
            Dispose_Wrap(Constants.QZPatent,
                access => access.OrgCompanyPatent_GetByoccode(oc_code));

        public static Tuple<List<Software_Abs>, int> Software_Get(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZProperty_Db_Key,
                access => access.Software_Page_Select(search, Sp_Name_Set.Table_PageSelect));
        public static Tuple<List<Product_Abs>, int> Product_Get(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZProperty_Db_Key,
                access => access.Product_Page_Select(search, Sp_Name_Set.Table_PageSelect));

        public static List<WenshuIndex> OrgCompanyJudge_Page_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZCourt,
                access => access.OrgCompanyJudge_Page_Select(search, Sp_Name_Set.Table_PageSelect));
        public static List<ShixinIndex> OrgCompanyDishonest_Page_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZCourt,
                access => access.OrgCompanyDishonest_Page_Select(search, Sp_Name_Set.Table_PageSelect));

        public static int Company_ScoreMark(Req_Oc_Score score) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_ScoreMark(score, Sp_Name_Set.Company_ScoreMark));
        public static List<Company_Icpl> Company_Icpl_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Company_Icpl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Icpl_Select));
        public static List<OrgCompanySiteInfo> OrgCompanySite_SelectPaged(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanySite_SelectPaged(search, Sp_Name_Set.Company_Icpl_Select));
        #region company annual
        public static List<Company_Annual_Abs> Company_Annual_Abs_Select(string oc_code, string oc_area, string year) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.Company_Annual_Abs_Select(oc_code, oc_area, year, Sp_Name_Set.Company_Annual_Abs_Select));

        public static Company_Annual_Dtl Company_Annual_Dtl_Select(string oc_code, string oc_area, string year) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.Company_Annual_Dtl_Select(oc_code, oc_area, year, Sp_Name_Set.Company_Annual_Dtl_Select));
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="oc_code"></param>
        /// <returns></returns>
        public static bool Company_Favorite_Exist(int u_id, string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Favorite_Exist(u_id, oc_code, Sp_Name_Set.Company_Favorite_Select));

        public static int Company_Favorite_GetByUidAndOccode(int u_id, string oc_code)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Favorite_GetByUidAndOccode(u_id, oc_code, Sp_Name_Set.Company_Favorite_Select));

        public static List<Favorite_Log> Company_Favorite_GetByUidAndGid(int u_id, int g_id,out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Favorite_GetByUidAndGid(u_id, g_id, Sp_Name_Set.FavoriteLog_SelectBygid, out count);
                }
                catch (Exception e)
                {
                    #region debug
                    count = 0;
                    //Util.Log_Info(nameof(FavoriteGroup_UpdateCount), Location.Internal, e.Message, "database error");
                    #endregion
                    return new List<Favorite_Log>();
                }
            }          
        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="oc_code"></param>
        /// <returns>if exception comes out, return -2; if update successfully, return 1; if no record found, return -1</returns>
        public static int FavoriteViewTrace_Time_Status_Update(int u_id, string oc_code, bool status_flag) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => {
                var info = access.FavoriteViewTrace_Select(u_id, oc_code, Sp_Name_Set.FavoriteViewTrace_Select);
                if (info != null)
                {
                    info.fvt_viewtime = DateTime.Now;
                    if (status_flag)
                        info.fvt_status = false;
                    return access.FavoriteViewTrace_Update(info);
                }
                return -1;
            }, -2);

        public static List<string> Brand_Query_Hot(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.Brand_Query_Hot(s, Sp_Name_Set.Table_PageSelect));
        public static List<string> Patent_Query_Hot(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZPatent, access => access.Patent_Query_Hot(s, Sp_Name_Set.Table_PageSelect));
        public static List<string> Dishonest_Query_Hot(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZCourt, access => access.Dishonest_Query_Hot(s, Sp_Name_Set.Table_PageSelect));

        public static List<string> Exhibit_Query_Hot(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Exhibit_Query_Hot(s, Sp_Name_Set.Table_PageSelect));

        public static List<string> Judge_Query_Hot(DatabaseSearchModel s) =>
            Dispose_Wrap(Constants.QZCourt, access => access.Judge_Query_Hot(s, Sp_Name_Set.Table_PageSelect));
        public static int FavoriteLog_Insert(FavoriteLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteLog_Insert(info, Sp_Name_Set.FavoriteLog_Insert), -2);

        /// <summary>
        /// disable the status of a company favorited by user
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public static int FavoriteLog_Disable(string oc_code, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteLog_Disable(oc_code, u_id, Sp_Name_Set.FavoriteLog_Disable));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>insert successfully, return 1; if exception comes out, return -2; othercase, return value little than 0</returns>
        public static int FavoriteViewTrace_Insert(FavoriteViewTraceInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteViewTrace_Insert(info, Sp_Name_Set.FavoriteViewTrace_Insert), -2);

        public static int TopicUserTrace_Status_Turnoff(string t_id, int u_id, string t_type) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUserTrace_Status_Turnoff(t_id, u_id, t_type, Sp_Name_Set.TopicUserTrace_Status_Turnoff));

        public static int TopicUsersTrace_All_TurnOff(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUsersTrace_All_TurnOff(u_id, Sp_Name_Set.TopicUsersTrace_All_TurnOff));

        /// <summary>
        /// whether user does up for a company
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="oc_code"></param>
        /// <returns>true, if user does up for the company</returns>
        public static Topic_Up_Down Company_User_Up_Down(int u_id, string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_User_Up_Down(u_id, oc_code, Sp_Name_Set.Company_Impression_Select));

        public static int Company_Topic_Insert(CompanyTieziTopicInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_Insert(info, Sp_Name_Set.Company_Topic_Insert));

        public static Topic_Dtl CompanyTopic_FromId_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyTopic_FromId_Get(t_id, Sp_Name_Set.CompanyTopic_FromId_Get));

        public static int Company_Reply_Insert(CompanyTieziReplyInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Reply_Insert(info, Sp_Name_Set.Company_Reply_Insert));

        public static int TopicUsersTrace_Insert(TopicUsersTraceInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUsersTrace_Insert(info, Sp_Name_Set.TopicUsersTrace_Insert));

        /// <summary>
        /// Refresh users trace of the topic when one user do reply operation under below one topic
        /// </summary>
        /// <param name="t_id">topic id(not reply id)</param>
        /// <param name="t_type">topic type(company "0", community"1")</param>
        /// <param name="u_id">user id who do the reply operation</param>
        /// <returns></returns>
        public static int TopicUserTrace_Refresh(string t_id, string t_type, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUsersTrace_Refresh(t_id, t_type, u_id));

        public static int TopicUserTrace_Reset(string t_id, string t_type, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUserTrace_Reset(t_id, t_type, u_id, Sp_Name_Set.TopicUserTrace_Reset));
        /// <summary>
        /// bulk insert image uris into database and then return the count of these images which were inserted successfully
        /// </summary>
        /// <param name="info"></param>
        /// <param name="uris"></param>
        /// <returns></returns>
        public static int CompanyTieziImage_Bulk_Insert(CompanyTieziImageInfo info, List<string> uris) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyTieziImage_Bulk_Insert(info, uris, Sp_Name_Set.CompanyTieziImage_Insert));

        public static List<Topic_Abs> Company_Topics_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topics_Get(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Topics_Select, out count);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topics_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    count = 0;
                    return null;
                }
            }
        }

        public static List<Topic_Dtl> Company_Topics_Dtl_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topics_Dtl_Get(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Topics_Select, out count);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topics_Dtl_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    count = 0;
                    return null;
                }
            }
        }
        public static List<Favorite_Log> Favorites_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Favorites_Get(search, Sp_Name_Set.Favorites_Get, out count);
                }
                catch (Exception e)
                {
                    count = 0;
                    #region debug
                    Util.Log_Info(nameof(Favorites_Get), Location.Internal, e.Message, "database error");
                    #endregion
                    return null;
                }
            }
        }

        public static bool Favorite_Exsit_byUidAndOccode(string oc_code, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Exsit_byUidAndOccode(oc_code, u_id));
        public static int Company_Topic_ReplyCount_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_ReplyCount_Get(t_id, Sp_Name_Set.Company_Topic_ReplyCount_Get));

        /// <summary>
        /// up and down counts of a specified topic
        /// </summary>
        /// 1 -> up, 2 -> down
        /// <param name="t_id"></param>
        /// <returns></returns>
        public static Tuple<int, int> Company_Topic_UpDown_Count_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_UpDown_Count_Get(t_id, Sp_Name_Set.Company_Topic_UpDown_Count_Select), new Tuple<int, int>(0, 0));

        /// <summary>
        /// up_down_flag of one user's manner on a specified topic
        /// </summary>
        /// <param name="t_id"></param>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public static Tuple<int, int> Company_Topic_UpDown_Flag_Get(int t_id, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_UpDown_Flag_Get(t_id, u_id, Sp_Name_Set.Company_Topic_UpDown_Flag_Select), new Tuple<int, int>(0, 0));

        /// <summary>
        /// get uris of comment images
        /// </summary>
        /// <returns></returns>
        public static List<string> Oc_Comment_Imgs_Select(int t_id, int type) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Oc_Imgs_Select(t_id, type, Sp_Name_Set.Company_Images_Select));

        #region voting for company topic
        public static int Company_Topic_Up2Down(CompanyLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_Up2Down(info));

        public static int Company_Topic_Down2Up(CompanyLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_Down2Up(info));

        public static int Company_Topic_UpDown_Vote(CompanyLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Topic_UpDown_Vote(info, Sp_Name_Set.Company_Topic_UpDown_Vote));

        #endregion

        #region voting for company
        public static int Company_Up2Down(LikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Up2Down(info));
        public static int Company_Down2Up(LikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Down2Up(info));

        public static int Company_UpDown_Vote(LikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_UpDown_Vote(info, Sp_Name_Set.Company_UpDown_Vote));

        #endregion

        /// <summary>
        /// Comment tipoff
        /// </summary>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static int Cmt_TipOff_Insert(CommentTipOffInfo tip) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommentTipOff_Insert(tip));

        public static CommentTipOffInfo Cmt_TipOff_Select(int t_id, byte t_type, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommentTipOff_SelectbyYuanGao_Type(u_id, t_type, t_id));

        public static int Cmt_TipOff_Update(CommentTipOffInfo tip) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommentTipOff_Update(tip));

        public static int Company_Correct(CompanyInfoCorrectInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Company_Correct(info, Sp_Name_Set.Company_Correct));

        public static List<string> Cm_Comment_Imgs_Select(int t_id, int type) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Comment_Imgs_Select(t_id, type, Sp_Name_Set.Community_Images_Select));

        public static List<Reply_Dtl> Replies_Dtl_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Replies_Dtl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Replies_Select));

        #region user
        public static User_Mini_Info User_FromId_Select(int u_id) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_FromId_Select(u_id));
        public static string User_ClientID_Getbyu_id(int u_id) =>
           Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_ClientID_Getbyu_id(u_id));
        
        public static List<History_Query> History_Query(DatabaseSearchModel search, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.SearchHistory_PageSelect(search, u_id, Sp_Name_Set.DeRedundent_Page_Select));

        public static List<string> Ext_SearchHistory_Page_Select(DatabaseSearchModel search, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Ext_SearchHistory_Page_Select(search, u_id, Sp_Name_Set.Table_PageSelect));
        public static int Ext_SearchHistory_Drop(int u_id, byte q_type) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Ext_SearchHistory_Drop(u_id, q_type, Sp_Name_Set.Ext_SearchHistory_Drop));
        public static int Query_Delete(int u_id, string oc_name) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Query_Delete(u_id, oc_name, Sp_Name_Set.History_Query_FromUidOcName_Delete));

        public static int Query_Drop(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Query_Drop(u_id, Sp_Name_Set.History_Query_FromUid_Delete));

        //public static List<Favorite_Log> Favorites_Get(DatabaseSearchModel search, out int count) =>
        //    Dispose_Wrap_1(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorites_Get(search, Sp_Name_Set.Favorites_Get, out count), () => Ex_Func<List<Favorite_Log>>(out count));

        public static List<ExhibitAbs> Company_ExhibitAbs_PageSelect(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Company_ExhibitAbs_PageSelect(search, Sp_Name_Set.Company_Exhibit_PageSelect));
        public static List<ExhibitCompany> Exhibit_Companies_Get(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Exhibit_Companies_Get(search, Sp_Name_Set.Company_Exhibit_PageSelect));

        public static ExhibitDtl Exhibit_Detail(DatabaseSearchModel model) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Exhibit_Detail(model, Sp_Name_Set.Exhibit_Detail_Get));

        public static List<Oc_Notice> FavoriteTraces_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.FavoriteTraces_Get(search, Sp_Name_Set.FavoriteTraces_Get, out count);
                }
                catch (Exception e)
                {
                    count = 0;
                    return null;
                }
            }
        }
        public static List<Topic_Notice> Notice_Topics_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Notice_Topics_Get(search, Sp_Name_Set.Notice_Topics_Get, out count);
                }
                catch (Exception e)
                {
                    count = 0;
                    return null;
                }
            }
        }



        #region browse
        public static List<Browse_Log> Browses_Get(DatabaseSearchModel search, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Browses_Get_1(search, u_id, Sp_Name_Set.DeRedundent_Page_Select));

        public static int Browses_Delete(int u_id, string oc_name) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Browses_Delete(u_id, oc_name, Sp_Name_Set.Browse_FromUidOcName_Delete));

        public static int Browses_Drop(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Browses_Drop(u_id, Sp_Name_Set.Browse_FromUid_Delete));

        #endregion

        public static bool TopicTrace_Status_Get(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicTrace_Status_Get(u_id, Sp_Name_Set.TopicTrace_Status_Get));

        public static UserInfo User_FromName_Select(string u_name) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_FromName_Select_2(u_name));

        public static void User_Ifda_Insert(string u_ifda)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    access.User_Ifda_Insert(u_ifda);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static int User_ClientID_Insert(int u_uid, string u_clientID) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_ClientID_Insert(u_uid, u_clientID));


        public static UserInfo User_FromId_Select_2(int u_id) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_FromId_Select_2(u_id));

        public static UserInfo User_FromEmail_Select_2(string u_email) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_FromEmail_Select_2(u_email));

        public static FavoriteViewTraceInfo FavoriteViewTrace_FromUidOccode_Get(string oc_code, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteViewTrace_FromUidOccode_Get(oc_code, u_id, Sp_Name_Set.FavoriteViewTrace_FromUidOccode_Get));

        public static UserInfo User_FromPhoneNum_Select_2(string u_phone) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_FromPhoneNum_Select_2(u_phone));

        public static int User_Insert_2(UserInfo user) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_Insert_2(user));
        public static int UserId_Select(int u_id) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.UserId_Select(u_id, Sp_Name_Set.UserId_Select));
        public static int User_Update(UserInfo user) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_Update(user));
        public static int LoginLog_Insert(Users_LoginLogs_Info info) =>
            Dispose_Wrap(Constants.QZNewSite_ULogs_Db_Key, access => access.LoginLog_Insert(info, Sp_Name_Set.LoginLog_Insert));
        public static Users_SocialInfo Open_User_Select(byte us_type, string us_uid) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.Open_User_Select(us_type, us_uid, Sp_Name_Set.Open_User_Select));
        public static bool User_FirstLogin_Exist(int u_id) =>
            Dispose_Wrap(Constants.SysLog_Db_Key, access => access.User_FirstLogin_Exist(u_id), true);
        public static int Users_Socials_Insert(Users_SocialInfo u) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.Users_Socials_Insert(u, Sp_Name_Set.Users_Socials_Insert));
        public static int User_FirstLogin_Insert(Users_AppFirstLoginLogsInfo info) =>
            Dispose_Wrap(Constants.SysLog_Db_Key, access => access.User_FirstLogin_Insert(info));
        public static int Users_Socials_Update(Users_SocialInfo ou) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.Users_Socials_Update(ou, Sp_Name_Set.Users_Socials_Update));

        public static int ShortMsg_Insert(User_SMSLogInfo info) =>
            Dispose_Wrap(Constants.SysLog_Db_Key, access => access.ShortMsg_Insert(info));

        public static int User_PwdReset_Log_Insert(Users_PwdFoundLogInfo info) =>
            Dispose_Wrap(Constants.QZNewSite_ULogs_Db_Key, access => access.User_PwdReset_Log_Insert(info));

        public static int UserAppendInf_Set(int u_id, string field, string value) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.UserAppendInf_Set(u_id, field, value), -1);

        public static MailServersInfo MailServers_SelectRand() =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access => access.MailServers_SelectRand());

        public static int Users_MailLogs_Insert(Users_MailLogInfo info) =>
            Dispose_Wrap(Constants.QZNewSite_ULogs_Db_Key, access => access.Users_MailLogs_Insert(info));

        public static User_Append_Info UserAppendInf_Select(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.UserAppendInf_Select(u_id));

        public static int UserNameUpdateLog_Count_Get(int u_id) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.UserNameUpdateLog_Count_Get(u_id));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="limit"></param>
        /// <returns>true, can update; false, can not update</returns>
        public static bool UserNameUpdate_Flag_Get(int u_id, int limit) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.UserNameUpdate_Flag_Get(u_id, limit));

        public static int UnameUpdate_Log_Insert(User_UpdateNameLog log) =>
            Dispose_Wrap(Constants.QZNewSite_User_Db_Key, access => access.User_UpdateNameLog(log));

        #endregion

        #region community
        public static List<Reply_Dtl> Cm_Replies_Dtl_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Cm_Replies_Dtl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Community_Replies_Select));

        public static int Community_Topic_Insert(AppTieziTopicInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_Insert(info, Sp_Name_Set.Community_Topic_Insert));

        public static int CommunityTieziImage_Bulk_Insert(AppTieziImageInfo img, List<string> uris) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommunityTieziImage_Bulk_Insert(img, uris, Sp_Name_Set.CommunityTieziImage_Insert));

        public static int Community_Reply_Insert(AppTeiziReplyInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Reply_Insert(info, Sp_Name_Set.CommunityReply_Insert));

        public static List<Cm_Topic_Dtl> Community_Topics_Dtl_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topics_Dtl_Get(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Community_Topics_Select, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }
        public static List<int> CommunityReply_GroupByTid_Get(int count) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommunityReply_GroupByTid_Get(count, Sp_Name_Set.CommunityReply_GroupByTid_Get));
        public static Cm_Topic_Dtl Community_Topic_Dtl_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_Dtl_Get(t_id, Sp_Name_Set.Community_Topic_Dtl_Select));

        public static int Community_Topic_ReplyCount_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_ReplyCount_Get(t_id, Sp_Name_Set.Community_Topic_ReplyCount_Get));

        public static Tuple<int, int> Community_Topic_UpDown_Count_Get(int t_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_UpDown_Count_Get(t_id, Sp_Name_Set.Community_Topic_UpDown_Count_Get), new Tuple<int, int>(0, 0));

        public static Tuple<int, int> Community_Topic_UpDown_Flag_Get(int t_id, int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_UpDown_Flag_Get(t_id, u_id, Sp_Name_Set.Community_Topic_UpDown_Flag_Get), new Tuple<int, int>(0, 0));

        public static List<int> CommunityReply_GroupBy_TidUid_Get(int u_id, int pg_index, int pg_size) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommunityReply_GroupBy_TidUid_Get(u_id, pg_index, pg_size, Sp_Name_Set.CommunityReply_GroupBy_TidUid_Get));

        public static int Community_Topic_UpDown_Vote(AppLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_UpDown_Vote(info, Sp_Name_Set.Community_Topic_UpDown_Vote));

        public static int Community_Topic_Up2Down(AppLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_Up2Down(info));

        public static int Community_Topic_Down2Up(AppLikeOrNotLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Community_Topic_Down2Up(info));
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pg_uid">value: "Corp_HotSearch"</param>
        /// <returns></returns>
        public static CMSPagesInfo CMSPagesInfo_FromUid_Get(string pg_uid) =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access => access.CMSPagesInfo_FromUid_Get(pg_uid, Sp_Name_Set.CMSPagesInfo_FromUid_Get));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blk_pg_id"></param>
        /// <param name="blk_name">value: "CorpHot"</param>
        /// <returns></returns>
        public static CMSBlocksInfo CMSBlocks_FromPgidName_Get(int blk_pg_id, string blk_name) =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access => access.CMSBlocks_Selectbyblk_pg_id(blk_pg_id).FirstOrDefault(b => b.blk_name == blk_name));

        public static List<CMSItemsInfo> CMSItems_FromPgid_Select(int pg_id, int pg_size) =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access =>
            access.CMSItems_FromPgid_Select(new DatabaseSearchModel().SetPageSize(pg_size).SetOrder(" n_date desc ").SetWhere($"n_pg_id={pg_id}").SetWhere("n_status=1 and n_publish='true'"), Sp_Name_Set.CMSItems_FromPgid_Select));

        public static List<CMSBlocksInfo> CMSBlocks_Selectbyblk_pg_id(string pg_uid) =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access =>
            {
                var page = access.CMSPagesInfo_FromUid_Get(pg_uid, Sp_Name_Set.CMSPagesInfo_FromUid_Get);
                return page == null ? null : access.CMSBlocks_Selectbyblk_pg_id(page.pg_id);
            });

        public static List<CertificationInfo> Certificatelst_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZProperty))
            {
                try
                {
                    return access.Certificatelst_Get(search, Sp_Name_Set.Certificatelst_Get, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<CertificationInfo> CertificateDtl_Get(int ci_id)=>
            Dispose_Wrap(Constants.QZProperty, access=>access.CertificateDtl_Get(ci_id, Sp_Name_Set.CertificateDtl_Get));



        public static List<OrgGS1RegListInfo> Reglst_Get(DatabaseSearchModel search,out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgGS1))
            {
                try
                {
                    return access.OrgGS1RegList_Get(search, Sp_Name_Set.Reglst_Get, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<OrgGS1ItemInfo> Invlst_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgGS1))
            {
                try
                {
                    return access.OrgGS1InvList_Get(search, Sp_Name_Set.Invlst_Get, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static OrgGS1ItemInfo InvDtl_Get(int ogs_id) =>
            Dispose_Wrap(Constants.QZOrgGS1, access => access.OrgGS1Item_Selectbyogs_id(ogs_id));

        public static List<Favorite_Group> FavoriteGroups_Selectbyu_uid(int u_uid)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroups_Selectbyu_uid(u_uid));
            
        public static int FavoriteGroup_Insert(Favorite_Group group)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_Insert(group, Sp_Name_Set.FavoriteGroup_Insert));  
         
        public static int FavoriteGroup_UpdateName(Favorite_Group group)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_UpdateName(group));

        public static int FavoriteGroup_UpdateCount(bool isAdd,int g_gid)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_UpdateCount(isAdd, g_gid));

        public static int FavoriteGroup_SetCount(int g_gid, int count) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_SetCount(g_gid, count));

        public static int FavoriteGroup_SetCount(int g_gid) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_SetCount(g_gid));

        public static int Favorite_Into_Group(string fl_ids, int g_gid,int count)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Into_Group(fl_ids, g_gid,count));

        public static int Favorite_Into_Group(int u_uid, string oc_code, int g_gid) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Into_Group(u_uid, oc_code, g_gid));

        public static int FavoriteGroup_Del(int g_gid) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteGroup_Del(g_gid,Sp_Name_Set.FavoriteGroup_Delete));
        public static int Favorite_Out_Group(int g_gid) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Out_Group(g_gid));

        public static List<UNSPSC_CNInfo> UNSPSC_CN_SelectPaged(string columns, string where, string order, int page, int pagesize, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgGS1))
            {
                try
                {
                    return access.UNSPSC_CN_SelectPaged(columns, where,order,page,pagesize, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<QZEmployInfo> QZEmploy_SelectPaged(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZEmploy))
            {
                try
                {
                    return access.QZEmploy_SelectPaged(search, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<QZEmployInfo> Job_Get(int ogs_id) =>
           Dispose_Wrap(Constants.QZEmploy, access => access.EmployInfo_Get(ogs_id, Sp_Name_Set.Employ_SelectbyID));

       public static List<ZhiXingInfo> ExecuteInfo_Get(int zx_id) =>
           Dispose_Wrap(Constants.QZCourt, access => access.ExecuteInfo_Get(zx_id, Sp_Name_Set.Execute_SelectbyZXID));

        public static List<OrgCompanySiteInfo> OrgCompanySite_SelectPaged(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompany_Db_Key))
            {
                try
                {
                    return access.OrgCompanySite_SelectPaged(search, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<ZhiXingInfo> ZhiXing_SelectPaged(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZCourt))
            {
                try
                {
                    return access.ZhiXing_SelectPaged(search, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }


        public static List<ExhibitionEnterpriseInfo> ExhibitionEnterprise_SelectPaged(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompany_Db_Key))
            {
                try
                {
                    return access.ExhibitionEnterprise_SelectPaged(search, out count);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static int OregCompanyDtl_Evt_GetByoccode(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OregCompanyDtl_Evt_GetByoccode(oc_code));

        public static int OregCompanyDtl_Evt_GetByoccode(string oc_code,string areaprefix) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.OrgCompanyGsxtBgsx_GetByoccode(oc_code,areaprefix));

        public static int Company_Annual_Abs_Count(string oc_code, string oc_areaprefix) =>
            Dispose_Wrap(Constants.QZOrgCompanyGsxt_Db_Key, access => access.Company_Annual_Abs_Count(oc_code, oc_areaprefix));

        public static int Company_Icpl_Count(string oc_code) =>
           Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Company_Icpl_Count(oc_code));

        public static int Software_GetByoccode(string oc_code) =>
           Dispose_Wrap(Constants.QZProperty_Db_Key, access => access.Software_GetByoccode(oc_code));

        public static int Product_GetByoccode(string oc_code) =>
           Dispose_Wrap(Constants.QZProperty_Db_Key, access => access.Product_GetByoccode(oc_code));

        public static int OrgCompanyDishonestt_GetByoccode(string oc_code) =>
           Dispose_Wrap(Constants.QZCourt, access => access.OrgCompanyDishonestt_GetByoccode(oc_code));

        public static int Favorite_Note_Create(int fl_id, string note) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Note_Create(fl_id, note));

        public static int Favorite_Note_Update(long n_id, string note) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Note_Update(n_id, note));

        public static int Favorite_Note_Del(long n_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Favorite_Note_Del(n_id));

        public static List<Favorite_Note> FavoriteNote_SelectPaged(DatabaseSearchModel model) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteNote_SelectPaged(model));

        public static Favorite_Note FavoriteNote_Top(int fl_id) =>
           Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteNote_top(fl_id));

        public static List<SystemNoticeInfo> SystemNotice_SelectPagedByUser(int userid, int startsize, int endsize, out int totalcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.SystemNotice_SelectPagedByUser(userid, startsize, endsize, out totalcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }           
        }

        public static List<SystemNoticeByUserInfo> SystemNoticeByUser_SelectPaged(DatabaseSearchModel model, out int totalcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.SystemNoticeByUser_SelectPaged(model, out totalcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }


        public static List<SystemNoticeInfo> SystemNotice_SelectPaged(DatabaseSearchModel model, out int totalcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.SystemNotice_SelectPaged(model, out totalcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static int SystemNoticeByUser_Insert(SystemNoticeByUserInfo obj)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.SystemNoticeByUser_Insert(obj));

        public static int SystemNoticeByUser_Update(SystemNoticeByUserInfo obj) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.SystemNoticeByUser_Update(obj));

        public static int SystemNoticeAll_Del(int u_id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.SystemNoticeAll_Del(u_id));

        public static CompanyStatisticsInfo CompanyStatistics_Get(string oc_code) =>
            Dispose_Wrap(Constants.CompanyStatisticsInfoTwo, access => access.CompanyStatistics_Get(oc_code));

        public static List<string> TopicUserTrace_GetClientId(int tut_t_id,int tut_t_type) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.TopicUserTrace_GetClientId(tut_t_id, tut_t_type));

        public static string Topic_Content_GetByid(int id)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CommunityTopic_Content_GetByid(id));
        public static string CompanyTopic_Content_Getbyid(int id) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyTopic_Content_Getbyid(id));

        public static int Company_Report_Collect(Req_ReportsReq obj) =>
            Dispose_Wrap(Constants.QZNewSite_Db_Key, access => access.Company_Report_Collect(obj));

        #region claim company
        public static int Claim_Company_Insert(ClaimCompanyInfo obj) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Claim_Company_Insert(obj));

        public static int Claim_Company_Update(ClaimCompanyInfo obj) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.Claim_Company_Update(obj));

        public static int ClaimCompany_Deletebycc_id(int cc_id, bool cc_isvalid) =>
           Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.ClaimCompany_Deletebycc_id(cc_id,cc_isvalid));


        public static ClaimCompanyInfo ClaimCompany_Selectbycc_id(int cc_id) =>
          Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.ClaimCompany_Selectbycc_id(cc_id));

        public static List<ClaimCompanyInfo> ClaimCompany_Selectbycc_u_uid(int cc_u_uid) =>
          Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.ClaimCompany_Selectbycc_u_uid(cc_u_uid));

        public static List<ClaimCompanyInfo> ClaimCompany_Selectbycc_oc_code(string cc_oc_code) =>
          Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.ClaimCompany_Selectbycc_oc_code(cc_oc_code));

        public static List<ClaimCompanyInfo> ClaimCompany_SelectPaged(DatabaseSearchModel model, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.ClaimCompany_SelectPaged(model, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static bool OrgCompanyExtensionData_Insert(OrgCompanyExtensionDataInfo obj, out string guid)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyExtension))
            {
                try
                {
                    return access.OrgCompanyExtensionData_Insert(obj, out guid);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static bool OrgCompanyExtensionData_Update(OrgCompanyExtensionDataInfo obj) =>
          Dispose_Wrap(Constants.QZOrgCompanyExtension, access => access.OrgCompanyExtensionData_Update(obj));

        public static bool OrgCompanyExtensionData_Update_CoverAll(OrgCompanyExtensionDataInfo obj) =>
            Dispose_Wrap(Constants.QZOrgCompanyExtension, access => access.OrgCompanyExtensionData_Update_CoverAll(obj));

        public static List<OrgCompanyExtensionDataInfo> OrgCompanyExtensionData_SelectPaged(DatabaseSearchModel model)=>
            Dispose_Wrap(Constants.QZOrgCompanyExtension, access => access.OrgCompanyExtensionData_SelectPaged(model));

        public static List<OrgCompanyExtensionDataInfo> OrgCompanyExtensionData_Selectbyoc_codeandoc_type(string oc_code, int oc_type)=>
            Dispose_Wrap(Constants.QZOrgCompanyExtension, access => access.OrgCompanyExtensionData_Selectbyoc_codeandoc_type(oc_code, oc_type));

        public static List<OrgCompanyExtensionDataInfo> OrgCompanyClaimCompanyWall_SelectPaged(int startIndex, int endIndex, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyExtension))
            {
                try
                {
                    return access.OrgCompanyClaimCompanyWall_SelectPaged(startIndex, endIndex, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }
        #endregion

        #region vip gallery
        public static int VipUserOrder_Insert(VipUserOrderInfo obj)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipUserOrder_Insert(obj));

        public static VipUserOrderInfo VipUserOrder_Selectbymo_orderid(string mo_orderid) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipUserOrder_Selectbymo_orderid(mo_orderid));

        public static int VipUserOrder_Update(VipUserOrderInfo obj)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipUserOrder_Update(obj));

        public static VipStatusUserInfo VipStatusUser_Selectbyvip_id(int vip_userid)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipStatusUser_Selectbyvip_id(vip_userid));

        public static int VipStatusUser_Update(VipStatusUserInfo obj)=>
             Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipStatusUser_Update(obj));

        public static int VipStatusUser_Insert(VipStatusUserInfo obj)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.VipStatusUser_Insert(obj));

        public static ExcelCompanyOrderInfo ExcelCompanyOrder_Selectbyeco_orderid(string eco_orderid)=>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.ExcelCompanyOrder_Selectbyeco_orderid(eco_orderid));
        #endregion

        #region company info package
        public static OrgCompanyDtlPack4Site OrgCompanySitePackSelect(string oc_code)=>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanySitePackSelect(oc_code));

        public static List<OrgCompanyListInfo> OrgCompanyList_Selectinoc_codes(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyList_Selectinoc_codes(oc_code));

        public static List<OrgCompanyDtlMgrInfo> OrgCompanyDtlMgr_Selectbyom_oc_code(string om_oc_code)=>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtlMgr_Selectbyom_oc_code(om_oc_code));

        public static List<OrgCompanyDtlGDInfo> OrgCompanyDtlGD_SelectPaged(DatabaseSearchModel model)=>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtlGD_SelectPaged(model));

        public static List<OrgCompanyBrandInfo> OrgCompanyBrand_SelectPaged(DatabaseSearchModel model)=>
            Dispose_Wrap(Constants.QZBrand_Db_Key, access => access.OrgCompanyBrand_SelectPaged(model));

        public static List<OrgCompanyPatentInfo> OrgCompanyPatent_SelectPaged(DatabaseSearchModel model)=>
            Dispose_Wrap(Constants.QZPatent, access => access.OrgCompanyPatent_SelectPaged(model));

        public static List<InvoiceInfo> InvoiceInfo_SelectPaged(DatabaseSearchModel model, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.InvoiceInfo_SelectPaged(model, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static int InvoiceInfo_Insert(InvoiceInfo obj) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.InvoiceInfo_Insert(obj));

        public static List<VipUserOrderInfo> VipUserOrder_SelectPaged(DatabaseSearchModel model, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.VipUserOrder_SelectPaged(model, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }

        public static List<ExcelCompanyOrderInfo> ExcelCompanyOrder_SelectPaged(DatabaseSearchModel model, out int rowcount)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.ExcelCompanyOrder_SelectPaged(model, out rowcount);
                }
                catch (Exception e)
                {
                    //count = 0;
                    //return new List<Cm_Topic_Dtl>();
                    throw e;
                }
            }
        }
        #endregion

    }
}
