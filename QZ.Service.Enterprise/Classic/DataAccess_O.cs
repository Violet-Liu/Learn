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
    public delegate Answer K<T, Answer>(Func<T, Answer> k);
    [Obsolete("use the class 'DataAccess'")]
    public class DataAccess_O
    {
        #region test snippet
        static K<T, Answer> Wrap<T, U, Answer>(Func<Func<T, K<U, Answer>>, K<T, Answer>> p)
        {
            return func => p(x => y => func(x))(func);
        }
        public static bool CompanyBlackList_Exist(string oc_code) =>
            Wrap<QZOrgCompanyAppAccess, string, bool>(x => y => y(new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key)))
                (access => access.CompanyBlackList_Exist_ByCode(oc_code, Sp_Name_Set.CompanyBlackList_Select_ByCode));
        #endregion

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
                    #region debug
                    Util.Log_Info(nameof(func), Location.Internal, e.Message, "database error");
                    #endregion
                    return exception;
                }
            }
        }

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
        #endregion

        #region company evaluation
        public static CompanyEvaluateInfo CompanyEvaluate_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyEvaluate_Select(oc_code, Sp_Name_Set.CompanyEvaluate_Select));

        public static int CompanyEvaluate_Update(CompanyEvaluateInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.CompanyEvaluate_Update(info, Sp_Name_Set.CompanyEvaluate_Update));

        #endregion


        [Obsolete("reference to function \"OrgCompanyDetails_Select(string oc_code)\"")]
        public static OrgCompanyDtlInfo OrgCompanyDtl_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtl_Select(oc_code, Sp_Name_Set.OrgCompanyDtl_Select));

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
        [Obsolete]
        public static List<OrgCompanyDtlMgrInfo> OrgCompanyDtlMgr_Select(string oc_code) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.OrgCompanyDtlMgr_Select(oc_code, Sp_Name_Set.OrgCompanyDtlMgr_Select));


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

        public static List<OrgCompanyDtl_EvtInfo> OrgCompanyDtl_Evt_Select(DatabaseSearchModel<OrgCompanyDtl_EvtInfo> search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, 
                         access => access.OrgCompanyDtl_Evt_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.OrgCompanyDtl_Evt_Select));

        public static List<OrgCompanyGsxtBgsxInfo> OrgCompanyGsxtBgsx_Select(DatabaseSearchModel<OrgCompanyGsxtBgsxInfo> search, string oc_area) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key,
                         access => access.OrgCompanyGsxtBgsx_Select(oc_area, search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.OrgCompanyGsxtBgsx_Select));
                         

        public static List<Company_Icpl> Company_Icpl_Select(DatabaseSearchModel search) =>
            Dispose_Wrap(Constants.QZOrgCompany_Db_Key, access => access.Company_Icpl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Icpl_Select));

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

        public static int FavoriteLog_Insert(FavoriteLogInfo info) =>
            Dispose_Wrap(Constants.QZOrgCompanyApp_Db_Key, access => access.FavoriteLog_Insert(info, Sp_Name_Set.FavoriteLog_Insert), -2);

        /// <summary>
        /// disable the status of a company favorited by user
        /// </summary>
        /// <param name="oc_code"></param>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public static int FavoriteLog_Disable(string oc_code, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.FavoriteLog_Disable(oc_code, u_id, Sp_Name_Set.FavoriteLog_Disable);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(FavoriteLog_Disable), Location.Internal, e.Message, "failed to insert data into database");
                    #endregion
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns>insert successfully, return 1; if exception comes out, return -2; othercase, return value little than 0</returns>
        public static int FavoriteViewTrace_Insert(FavoriteViewTraceInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.FavoriteViewTrace_Insert(info, Sp_Name_Set.FavoriteViewTrace_Insert);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(FavoriteViewTrace_Insert), Location.Internal, e.Message, "failed to insert data into database");
                    #endregion
                    return -2;
                }
            }
        }
        public static int TopicUserTrace_Status_Turnoff(string t_id, int u_id, string t_type)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.TopicUserTrace_Status_Turnoff(t_id, u_id, t_type, Sp_Name_Set.TopicUserTrace_Status_Turnoff);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }
        public static int TopicUsersTrace_All_TurnOff(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.TopicUsersTrace_All_TurnOff(u_id, Sp_Name_Set.TopicUsersTrace_All_TurnOff);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// whether user does up for a company
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="oc_code"></param>
        /// <returns>true, if user does up for the company</returns>
        public static Topic_Up_Down Company_User_Up_Down(int u_id, string oc_code)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_User_Up_Down(u_id, oc_code, Sp_Name_Set.Company_Impression_Select);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_User_Up_Down), Location.Internal, e.Message, "failed to select data from database");
                    #endregion
                    return Topic_Up_Down.None;
                }
            }
        }

        public static int Company_Topic_Insert(CompanyTieziTopicInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_Insert(info, Sp_Name_Set.Company_Topic_Insert);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_Insert), Location.Internal, e.Message, "failed to insert data into database");
                    #endregion
                    return -1;
                }
            }
        }
        public static Topic_Dtl CompanyTopic_FromId_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.CompanyTopic_FromId_Get(t_id, Sp_Name_Set.CompanyTopic_FromId_Get);
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }

        public static int Company_Reply_Insert(CompanyTieziReplyInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Reply_Insert(info, Sp_Name_Set.Company_Reply_Insert);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Reply_Insert), Location.Internal, e.Message, "failed to insert data into database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int TopicUsersTrace_Insert(TopicUsersTraceInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.TopicUsersTrace_Insert(info, Sp_Name_Set.TopicUsersTrace_Insert);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(TopicUsersTrace_Insert), Location.Internal, e.Message, "failed insert database");
                    #endregion
                    return -1;
                }
            }
        }
        /// <summary>
        /// Refresh users trace of the topic when one user do reply operation under below one topic
        /// </summary>
        /// <param name="t_id">topic id(not reply id)</param>
        /// <param name="t_type">topic type(company "0", community"1")</param>
        /// <param name="u_id">user id who do the reply operation</param>
        /// <returns></returns>
        public static int TopicUserTrace_Refresh(string t_id, string t_type, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.TopicUsersTrace_Refresh(t_id, t_type, u_id);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(TopicUserTrace_Refresh), Location.Internal, e.Message, "failed refresh data database");
                    #endregion
                    return -1;
                }
            }
        }

        /// <summary>
        /// bulk insert image uris into database and then return the count of these images which were inserted successfully
        /// </summary>
        /// <param name="info"></param>
        /// <param name="uris"></param>
        /// <returns></returns>
        public static int CompanyTieziImage_Bulk_Insert(CompanyTieziImageInfo info, List<string> uris)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                return access.CompanyTieziImage_Bulk_Insert(info, uris, Sp_Name_Set.CompanyTieziImage_Insert);
            }
        }

        public static List<Topic_Abs> Company_Topics_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topics_Get(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Topics_Select, out count);
                }
                catch(Exception e)
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
        public static int Company_Topic_ReplyCount_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_ReplyCount_Get(t_id, Sp_Name_Set.Company_Topic_ReplyCount_Get);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_ReplyCount_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return 0;
                }
            }
        }
        /// <summary>
        /// up and down counts of a specified topic
        /// </summary>
        /// 1 -> up, 2 -> down
        /// <param name="t_id"></param>
        /// <returns></returns>
        public static Tuple<int, int> Company_Topic_UpDown_Count_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_UpDown_Count_Get(t_id, Sp_Name_Set.Company_Topic_UpDown_Count_Select);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_UpDown_Count_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return new Tuple<int, int>(0, 0);
                }
            }
        }

        /// <summary>
        /// up_down_flag of one user's manner on a specified topic
        /// </summary>
        /// <param name="t_id"></param>
        /// <param name="u_id"></param>
        /// <returns></returns>
        public static Tuple<int, int> Company_Topic_UpDown_Flag_Get(int t_id, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_UpDown_Flag_Get(t_id, u_id, Sp_Name_Set.Company_Topic_UpDown_Count_Select);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_UpDown_Flag_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return new Tuple<int, int>(0, 0);
                }
            }
        }


        #region voting for company topic
        public static int Company_Topic_Up2Down(CompanyLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_Up2Down(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_Up2Down), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int Company_Topic_Down2Up(CompanyLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_Down2Up(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_Down2Up), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int Company_Topic_UpDown_Vote(CompanyLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Topic_UpDown_Vote(info, Sp_Name_Set.Company_Topic_UpDown_Vote);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_UpDown_Vote), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        #endregion

        #region voting for company
        public static int Company_Up2Down(LikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Up2Down(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_Up2Down), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int Company_Down2Up(LikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Down2Up(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_Down2Up), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int Company_UpDown_Vote(LikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_UpDown_Vote(info, Sp_Name_Set.Company_UpDown_Vote);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Company_Topic_UpDown_Vote), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        #endregion
        
        public static int Company_Correct(CompanyInfoCorrectInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Company_Correct(info, Sp_Name_Set.Company_Correct);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }


        public static List<Reply_Dtl> Replies_Dtl_Select(DatabaseSearchModel search)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Replies_Dtl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Company_Replies_Select);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Replies_Dtl_Select), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }

        #region user
        public static User_Mini_Info User_FromId_Select(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_FromId_Select(u_id);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FromId_Select), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }
        public static List<History_Query> History_Query(DatabaseSearchModel search, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.History_Queries_Get(search, u_id, Sp_Name_Set.Table_PageSelect);
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }
        public static int Query_Delete(int u_id, string oc_name)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Query_Delete(u_id, oc_name, Sp_Name_Set.History_Query_FromUidOcName_Delete);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }
        public static int Query_Drop(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Query_Drop(u_id, Sp_Name_Set.History_Query_FromUid_Delete);
                }
                catch(Exception e)
                {
                    return -1;
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
                catch(Exception e)
                {
                    count = 0;
                    return null;
                }
            }
        }
        public static List<Oc_Notice> FavoriteTraces_Get(DatabaseSearchModel search, out int count)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.FavoriteTraces_Get(search, Sp_Name_Set.FavoriteTraces_Get, out count);
                }
                catch(Exception e)
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
                catch(Exception e)
                {
                    count = 0;
                    return null;
                }
            }
        }



        #region browse
        public static List<Browse_Log> Browses_Get(DatabaseSearchModel search, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Browses_Get(search, u_id, Sp_Name_Set.Table_PageSelect);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public static int Browses_Delete(int u_id, string oc_name)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Browses_Delete(u_id, oc_name, Sp_Name_Set.Browse_FromUidOcName_Delete);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }
        public static int Browses_Drop(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Browses_Drop(u_id, Sp_Name_Set.Browse_FromUid_Delete);
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        #endregion

        public static bool TopicTrace_Status_Get(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.TopicTrace_Status_Get(u_id, Sp_Name_Set.TopicTrace_Status_Get);
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }
        public static UserInfo User_FromName_Select_2(string u_name)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_FromName_Select_2(u_name);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FromName_Select_2), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }
        public static UserInfo User_FromId_Select_2(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_FromId_Select_2(u_id);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FromId_Select_2), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }
        public static UserInfo User_FromEmail_Select_2(string u_email)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_FromEmail_Select_2(u_email);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FromEmail_Select_2), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }

        public static FavoriteViewTraceInfo FavoriteViewTrace_FromUidOccode_Get(string oc_code, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.FavoriteViewTrace_FromUidOccode_Get(oc_code, u_id, Sp_Name_Set.FavoriteViewTrace_FromUidOccode_Get);
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }

        public static UserInfo User_FromPhoneNum_Select_2(string u_phone)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_FromPhoneNum_Select_2(u_phone);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FromPhoneNum_Select_2), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }

        public static int User_Insert_2(UserInfo user)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_Insert_2(user);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_Insert_2), Location.Internal, e.Message, "failed insert data into database");
                    #endregion
                    return -1;
                }
            }
        }

        public static int User_Update(UserInfo user)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_Update(user);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_Update), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return -1;
                }
            }
        }

        public static bool User_FirstLogin_Exist(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.SysLog_App_Db_Key))
            {
                try
                {
                    return access.User_FirstLogin_Exist(u_id);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FirstLogin_Exist), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return true;
                }
            }   
        }

        public static int User_FirstLogin_Insert(Users_AppFirstLoginLogsInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.SysLog_App_Db_Key))
            {
                try
                {
                    return access.User_FirstLogin_Insert(info);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_FirstLogin_Insert), Location.Internal, e.Message, "failed insert data into database");
                    #endregion
                    return -1;
                }
            }
        }

        public static int ShortMsg_Insert(User_SMSLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.SysLog_Db_Key))
            {
                try
                {
                    return access.ShortMsg_Insert(info);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(ShortMsg_Insert), Location.Internal, e.Message, "failed insert data into database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int User_PwdReset_Log_Insert(Users_PwdFoundLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_ULogs_Db_Key))
            {
                try
                {
                    return access.User_PwdReset_Log_Insert(info);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(User_PwdReset_Log_Insert), Location.Internal, e.Message, "failed insert data into database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int UserAppendInf_Set(int u_id, string field, string value)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.UserAppendInf_Set(u_id, field, value);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(UserAppendInf_Set), Location.Internal, e.Message, "failed database sp");
                    #endregion
                    return -1;
                }
            }
        }

        public static MailServersInfo MailServers_SelectRand()
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    return access.MailServers_SelectRand();
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(MailServers_SelectRand), Location.Internal, e.Message, "failed database sp");
                    #endregion
                    return null;
                }
            }
        }

        public static int Users_MailLogs_Insert(Users_MailLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_ULogs_Db_Key))
            {
                try
                {
                    return access.Users_MailLogs_Insert(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Users_MailLogs_Insert), Location.Internal, e.Message, "failed database sp");
                    #endregion
                    return -1;
                }
            }
        }


        public static User_Append_Info UserAppendInf_Select(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.UserAppendInf_Select(u_id);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(UserAppendInf_Select), Location.Internal, e.Message, "failed database sp");
                    #endregion
                    return null;
                }
            }
        }

        public static int UserNameUpdateLog_Count_Get(int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.UserNameUpdateLog_Count_Get(u_id);
                }
                catch(Exception e)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u_id"></param>
        /// <param name="limit"></param>
        /// <returns>true, can update; false, can not update</returns>
        public static bool UserNameUpdate_Flag_Get(int u_id, int limit)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.UserNameUpdate_Flag_Get(u_id, limit);
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public static int UnameUpdate_Log_Insert(User_UpdateNameLog log)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_User_Db_Key))
            {
                try
                {
                    return access.User_UpdateNameLog(log);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(UnameUpdate_Log_Insert), Location.Internal, e.Message, "failed database sp");
                    #endregion
                    return -1;
                }
            }
        }
        #endregion

        #region community
        public static List<Reply_Dtl> Cm_Replies_Dtl_Select(DatabaseSearchModel search)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Cm_Replies_Dtl_Select(search.Column, search.Where, search.Order, search.PageIndex, search.PageSize, Sp_Name_Set.Community_Replies_Select);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Cm_Replies_Dtl_Select), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }
        public static int Community_Topic_Insert(AppTieziTopicInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_Insert(info, Sp_Name_Set.Community_Topic_Insert);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_Insert), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return -1;
                }
            }
        }

        public static int CommunityTieziImage_Bulk_Insert(AppTieziImageInfo img, List<string> uris)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                return access.CommunityTieziImage_Bulk_Insert(img, uris, Sp_Name_Set.CommunityTieziImage_Insert);
            }
        }
        public static int Community_Reply_Insert(AppTeiziReplyInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Reply_Insert(info, Sp_Name_Set.CommunityReply_Insert);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Reply_Insert), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return -1;
                }
            }
            
        }

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
                    #region debug
                    Util.Log_Info(nameof(Community_Topics_Dtl_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    count = 0;
                    return null;
                }
            }
        }
        public static Cm_Topic_Dtl Community_Topic_Dtl_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_Dtl_Get(t_id, Sp_Name_Set.Community_Topic_Dtl_Select);
                }
                catch
                {
                    return null;
                }
            }
        }
        public static int Community_Topic_ReplyCount_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_ReplyCount_Get(t_id, Sp_Name_Set.Community_Topic_ReplyCount_Get);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_ReplyCount_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return 0;
                }
            }
        }
        public static Tuple<int, int> Community_Topic_UpDown_Count_Get(int t_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_UpDown_Count_Get(t_id, Sp_Name_Set.Community_Topic_UpDown_Count_Get);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_UpDown_Count_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return new Tuple<int, int>(0, 0);
                }
            }
        }
        public static Tuple<int, int> Community_Topic_UpDown_Flag_Get(int t_id, int u_id)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_UpDown_Flag_Get(t_id, u_id, Sp_Name_Set.Community_Topic_UpDown_Flag_Get);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_UpDown_Flag_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return new Tuple<int, int>(0, 0);
                }
            }
        }

        public static List<int> CommunityReply_GroupBy_TidUid_Get(int u_id, int pg_index, int pg_size)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.CommunityReply_GroupBy_TidUid_Get(u_id, pg_index, pg_size, Sp_Name_Set.CommunityReply_GroupBy_TidUid_Get);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(CommunityReply_GroupBy_TidUid_Get), Location.Internal, e.Message, "failed get data from database");
                    #endregion
                    return null;
                }
            }
        }

        public static int Community_Topic_UpDown_Vote(AppLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_UpDown_Vote(info, Sp_Name_Set.Community_Topic_UpDown_Vote);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_UpDown_Vote), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        public static int Community_Topic_Up2Down(AppLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_Up2Down(info);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_Up2Down), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }

        public static int Community_Topic_Down2Up(AppLikeOrNotLogInfo info)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZOrgCompanyApp_Db_Key))
            {
                try
                {
                    return access.Community_Topic_Down2Up(info);
                }
                catch (Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(Community_Topic_Down2Up), Location.Internal, e.Message, "failed operating database");
                    #endregion
                    return -1;
                }
            }
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pg_uid">value: "Corp_HotSearch"</param>
        /// <returns></returns>
        public static CMSPagesInfo CMSPagesInfo_FromUid_Get(string pg_uid)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    return access.CMSPagesInfo_FromUid_Get(pg_uid, Sp_Name_Set.CMSPagesInfo_FromUid_Get);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(CMSPagesInfo_FromUid_Get), Location.Internal, e.Message, "database error");
                    #endregion
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blk_pg_id"></param>
        /// <param name="blk_name">value: "CorpHot"</param>
        /// <returns></returns>
        public static CMSBlocksInfo CMSBlocks_FromPgidName_Get(int blk_pg_id, string blk_name)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    return access.CMSBlocks_Selectbyblk_pg_id(blk_pg_id).FirstOrDefault(b => b.blk_name == blk_name);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(CMSBlocks_FromPgidName_Get), Location.Internal, e.Message, "database error");
                    #endregion
                    return null;
                }
            }
        }

        public static CMSItemsInfo CMSItems_FromBlkid_Select(int blk_id, int pg_size)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    var search = new DatabaseSearchModel().SetPageSize(pg_size).SetOrder(" n_date desc ").SetWhere($"n_blk_id={blk_id}");
                    return null /*access.CMSItems_FromBlkid_Select()*/;
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(CMSItems_FromBlkid_Select), Location.Internal, e.Message, "database error");
                    #endregion
                    return null;
                }
            }
        }

        public static List<CMSItemsInfo> CMSItems_FromPgid_Select(int pg_id, int pg_size)
        {
            using (var access = new QZOrgCompanyAppAccess(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    var search = new DatabaseSearchModel().SetPageSize(pg_size).SetOrder(" n_date desc ").SetWhere($"n_pg_id={pg_id}");
                    return access.CMSItems_FromPgid_Select(search, Sp_Name_Set.CMSItems_FromPgid_Select);
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }
    }
}