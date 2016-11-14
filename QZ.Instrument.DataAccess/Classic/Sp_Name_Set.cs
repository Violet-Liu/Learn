/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.DataAccess
{
    public class Sp_Name_Set
    {
        public const string SearchHistory_M_Insert = "Proc_SearchHistory_Insert";
        public const string SearchHistory_S_Insert = "Proc_SearchHistory_Slave_Insert";
        public const string SearchHistoryExt_S_Insert = "Proc_SearchHistoryExt_Slave_Insert";
        public const string SearchHistoryExt_M_Insert = "Proc_SearchHistoryExt_Insert";
        /// <summary>
        /// in fact, this sp will operate on slave table 
        /// </summary>
        public const string AppOrgCompanyLog_Insert = "Proc_AppOrgCompanyLog_Insert";
        public const string District_Code_Get = "Proc_DistrictCode_Selectbydc_a_code";
        public const string User_FromUid_Select = "Proc_Users_Selectbyu_uid";
        public const string CompanyBlackList_Select_ByCode = "Proc_CompanyBlackList_Selectbycbl_oc_code";
        public const string CompanyBlackList_Select_ByName = "Proc_CompanyBlackList_Selectbycbl_oc_name";
        public const string BrowseLog_M_Insert = "Proc_BrowseLog_Insert";
        public const string BrowseLog_S_Insert = "Proc_BrowseLog_Slave_Insert";
        public const string CompanyEvaluate_Select = "Proc_CompanyEvaluate_Selectbyce_oc_code";
        public const string CompanyEvaluate_Update = "Proc_CompanyEvaluate_Update_Byoc_code";
        public const string OrgCompanyDtl_Select = "Proc_OrgCompanyDtl_Selectbyod_oc_code";
        public const string OrgCompanyList_Select = "Proc_OrgCompanyList_Selectbyoc_code";
        public const string OrgCompanyDtlMgr_Select = "Proc_OrgCompanyDtlMgr_Selectbyom_oc_code";
        public const string OrgCompanyGsxtDtlMgr_Select = "Proc_OrgCompanyGsxtDtlMgr_SelectPaged";
        public const string OrgCompanyDtlGD_Select = "Proc_OrgCompanyDtlGD_Selectbyog_oc_code";
        public const string OrgCompanyGsxtDtlGD_Select = "Proc_OrgCompanyGsxtDtlGD_SelectPaged";
        public const string OrgCompanyDtl_Evt_Select = "Proc_OrgCompanyDtl_Evt_SelectPaged";
        public const string OrgCompanyGsxtBgsx_Select = "Proc_OrgCompanyGsxtBgsx_SelectPaged";
        public const string Company_Icpl_Select = "Proc_OrgCompanySite_SelectPaged";
        public const string Company_Correct = "Proc_CompanyInfoCorrect_Insert";
        public const string Company_Annual_Abs_Select = "Proc_SelectOrgCompanyGsxtNbByCodeSimple";
        public const string Company_Annual_Dtl_Select = "Proc_SelectOrgCompanyGsxtNbByCode";
        public const string Company_Favorite_Select = "Proc_FavoriteLog_Selectbyfl_oc_code_and_fl_oc_u_uid";
        public const string FavoriteViewTrace_Select = "Proc_FavoriteViewTrace_Selectbycodeanduid";
        public const string FavoriteViewTrace_Insert = "Proc_FavoriteViewTrace_Insert";
        public const string Company_Impression_Select = "Proc_LikeOrNotLog_Selectbylon_u_uid_and_lon_oc_code_and_lon_type";
        public const string Company_Topic_Insert = "Proc_CompanyTeiziTopic_Insert";
        public const string CompanyTopic_FromId_Get = "Proc_CompanyTeiziTopic_Selectbyctt_id";
        public const string TopicUsersTrace_Insert = "Proc_TopicUsersTrace_Insert";
        public const string CompanyTieziImage_Insert = "Proc_CompanyTieziImage_Insert";
        public const string Area_Get = "Proc_AreaList_SelectWhere";
        public const string Company_Reply_Insert = "Proc_CompanyTeiziReply_Insert";
        public const string Company_Topics_Select = "Proc_CompanyTeiziTopic_SelectPaged";
        public const string Company_Replies_Select = "Proc_CompanyTeiziReply_SelectPaged";
        public const string Community_Replies_Select = "Proc_AppTeiziReply_SelectPaged";
        public const string Company_Images_Select = "Proc_CompanyTieziImage_Selectbytidtype";
        public const string Community_Images_Select = "Proc_AppTieziImage_Selectbytidtype";
        public const string Company_Topic_UpDown_Count_Select = "Proc_CompanyLikeOrNotLog_Selectrowcountbycll_type_and_cll_teizi";
        public const string Company_Topic_UpDown_Flag_Select = "Proc_CompanyLikeOrNotLog_Selectbycll_teizi_and_cll_u_uid_and_cll_type";
        public const string FavoriteLog_Insert = "Proc_FavoriteLog_Insert";
        public const string FavoriteLog_Disable = "Proc_FavoriteLog_Cancelbyfl_oc_code_and_fl_oc_u_uid";
        public const string Company_Topic_ReplyCount_Get = "Proc_CompanyTeiziReply_Count_Get";
        public const string Table_PageSelect = "Proc_BaseBetween_SelectByPageIndex";
        public const string Community_Topic_Insert = "Proc_AppTeiziTopic_Insert";
        public const string CommunityTieziImage_Insert = "Proc_AppTieziImage_Insert";
        public const string CommunityReply_Insert = "Proc_AppTeiziReply_Insert";
        public const string Community_Topics_Select = "Proc_HotAppTeiziTopic_SelectPaged";
        public const string Community_Topic_ReplyCount_Get = "Proc_AppTeiziReply_Count_Get";
        public const string Community_Topic_UpDown_Count_Get = "Proc_AppLikeOrNotLog_Selectrowcountbyall_type_and_all_teizi";
        public const string Community_Topic_UpDown_Flag_Get = "Proc_AppLikeOrNotLog_Selectbyall_teizi_and_all_u_uid_and_all_type";
        public const string CommunityReply_GroupBy_TidUid_Get = "Proc_AppTeiziReplyGroupByTidUid_PageSelect";
        public const string Community_Topic_Dtl_Select = "Proc_AppTeiziTopic_Selectbyatt_id";
        public const string Community_Topic_UpDown_Vote = "Proc_AppLikeOrNotLog_Insert";
        public const string History_Query_FromUidOcName_Delete = "Proc_SearchHistory_Slave_Deletebyocname_uid";
        public const string History_Query_FromUid_Delete = "Proc_SearchHistory_Slave_Deletebysh_u_uid";
        public const string Browse_FromUidOcName_Delete = "Proc_BrowseLog_Slave_Deletebyocname_uid";
        public const string Browse_FromUid_Delete = "Proc_BrowseLog_Slave_Deletebybl_u_uid";
        public const string Favorites_Get = "Proc_FavoriteLog_SelectPaged";
        public const string TopicTrace_Status_Get = "Proc_TopicUsersTrace_Selectbyuid";
        public const string FavoriteTraces_Get = "Proc_Favorite_View_Trace_SelectPaged";
        public const string Notice_Topics_Get = "Proc_TopicUsersTrace_SelectPaged";
        public const string FavoriteViewTrace_FromUidOccode_Get = "Proc_FavoriteViewTrace_Selectbycodeanduid";
        public const string TopicUserTrace_Status_Turnoff = "Proc_TopicUsersTrace_TurnOffStatus";
        public const string TopicUsersTrace_All_TurnOff = "Proc_TopicUsersTrace_TotalTurnOff";
        public const string Company_Topic_UpDown_Vote = "Proc_CompanyLikeOrNotLog_Insert";
        public const string Company_UpDown_Vote = "Proc_LikeOrNotLog_Insert";
        public const string DeRedundent_Page_Select = "Proc_BaseBetween_DeRedundent_Select";
        public const string CMSPagesInfo_FromUid_Get = "Proc_CMSPages_Selectbypg_uid";
        public const string CMSItems_FromPgid_Select = "Proc_CMSItems_SelectPaged";
        public const string Company_Exhibit_PageSelect = "Proc_ExhibitionEnterprise_SelectPaged";
        public const string Exhibit_Detail_Get = "Proc_ExhibitionML_SelectPaged";

        public const string OrgCompanyBrand_FromCode_Select = "Proc_OrgCompanyBrand_SelectByCode";
        public const string OrgCompanyPatent_FromCode_Select = "Proc_OrgCompanyPatent_SelectByCode";
        public const string CommunityReply_GroupByTid_Get = "Proc_AppTeiziReply_GroupByTidSelect";
        public const string Browses_Fresh_Get = "Proc_BrowseLog_Fresh_Select";
        public const string Company_ScoreMark = "Company_ScoreMark";
        public const string Judge_Dtl_Query = "Proc_JudgementDoc_SelectById";
        public const string Dishonest_Dtl_Query = "ProcShixin_SelectById";
        public const string Patent_Dtl_Get = "Proc_CompanyPatent_SelectById";
        public const string Patent_Dtl_FromNoCat_Select = "Proc_CompanyPatent_FromNoCat_Select";
        public const string Brand_Dtl_Get = "Proc_OrgCompanyBrandFull_SelectById";
        public const string Brand_Dtl_Select = "Proc_OrgCompanyBrandExt_FromRegClass_Select";
        public const string Ext_SearchHistory_Drop = "Proc_SearchHistoryExt_Deletebyuid";
        public const string Open_User_Select = "Proc_Users_Socials_SelectSingle";
        public const string Users_Socials_Update = "Proc_Users_Socials_Update";
        public const string UserId_Select = "Proc_Users_Selectu_uidbyu_id";
        public const string Users_Socials_Insert = "Proc_Users_Socials_Insert";
        public const string LoginLog_Insert = "Proc_Users_LoginLogs_Insert";
        public const string OrgCompanyList_Page_Select = "Proc_OrgCompanyList_SelectPaged";
        public const string OrgCompanyDtl_Page_Select = "Proc_OrgCompanyDtl_SelectPaged";
        public const string TopicUserTrace_Reset = "Proc_TopicUsersTrace_ResetCount";
        public const string GB_Trade_AllSelect = "Proc_TradeCategory2012_SelectAll";
        public const string Product_Cat_AllSelect = "Proc_ProductCategory_SelectPaged";
    }
}
