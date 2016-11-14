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

namespace QZ.Test.Client
{
    public class Constants
    {
        public const string C_Dyn_Key_0 = "0BF71E02907CA25135B36B5328AF36C4";
        public const string C_Dyn_Key = "ST1856A312320D4E0F6385347089EEAC";

        public const string Enterprise_Uri = "http://localhost:9081/enterprise.svc/company/";

        public static readonly string Index_Uri = "http://localhost:9081/enterprise.svc/index";
        public static readonly string Area_Uri = Enterprise_Uri + "area_all";
        public static readonly string Company_Trades_Uri = Enterprise_Uri + "trade_infos";
        public static readonly string Company_Query_Uri = Enterprise_Uri + "query";
       
        public static readonly string Company_Detail_Uri = Enterprise_Uri + "detail";
        public static readonly string Company_Intelli_Tip_Uri = Enterprise_Uri + "intelli_tip";
        public static readonly string Company_Invest_Uri = Enterprise_Uri + "invest";
        public static readonly string Company_Report_Send_Uri = Enterprise_Uri + "report_send";
        public static readonly string Company_Map_Uri = Enterprise_Uri + "map";
        public static readonly string Company_Stock_Holder_Uri = Enterprise_Uri + "stock_holder";
        public static readonly string Company_Change_Uri = Enterprise_Uri + "change";
        public static readonly string Company_Icpl_Uri = Enterprise_Uri + "icpl";
        public static readonly string Company_Branch_Uri = Enterprise_Uri + "branch";
        public static readonly string Company_Annual_Uri = Enterprise_Uri + "annual";
        public static readonly string Company_Annual_Detail_Uri = Enterprise_Uri + "annual_detail";
        public static readonly string Company_Impression_Uri = Enterprise_Uri + "impression";
        public static readonly string Company_New_Topic_Uri = Enterprise_Uri + "new_topic";
        public static readonly string Company_Reply_Uri = Enterprise_Uri + "reply";
        public static readonly string Company_Fresh_Topic_Uri = Enterprise_Uri + "fresh_topic";
        public static readonly string Company_Topic_Query_Uri = Enterprise_Uri + "topic_query";
        public static readonly string Company_Topic_Detail_Uri = Enterprise_Uri + "topic_detail";
        public static readonly string Company_Favorite_Add_Uri = Enterprise_Uri + "favorite_add";
        public static readonly string Company_Favorite_Remove_Uri = Enterprise_Uri + "favorite_remove";
        public static readonly string Query_Hot_Uri = Enterprise_Uri + "query_hot/5";
        public static readonly string Company_Topic_UpDown_Vote_Uri = Enterprise_Uri + "topic/updown_vote";
        public static readonly string Company_UpDown_Vote_Uri = Enterprise_Uri + "updown_vote";
        public static readonly string Company_Correct_Uri = Enterprise_Uri + "correct";
        public static readonly string Brand_Query_Uri = Enterprise_Uri + "brands";
        public static readonly string Oc_Brand_Get_Uri = Enterprise_Uri + "oc_brands";
        public static readonly string Oc_Patent_Get_Uri = Enterprise_Uri + "oc_patents";
        public static readonly string Patent_Query_Uri = Enterprise_Uri + "patents";
        public static readonly string Patent_Dtl_Uri = Enterprise_Uri + "patent_dtl";
        public static readonly string Patent_Universal_Query_Uri = Enterprise_Uri + "u_patents";
        public static readonly string Judge_Query_Uri = Enterprise_Uri + "judges";
        public static readonly string Dishonest_Query_Uri = Enterprise_Uri + "dishonests";
        public static readonly string ExtQuery_Hot_Uri = Enterprise_Uri + "extquery_hot/1/5";
        public static readonly string Exhibit_Tips_Uri = Enterprise_Uri + "trade_tips";
        public static readonly string Exh_Companies_Uri = Enterprise_Uri + "exh_companies";
        public static readonly string Exh_Search_Uri = Enterprise_Uri + "search_exhibit";
        public static readonly string Oc_Dishonest_Get_Uri = Enterprise_Uri + "oc_dishonests";
        public static readonly string Oc_Judge_Get_Uri = Enterprise_Uri + "oc_judges";
        public static readonly string CompanyTrade_Search_Uri = Enterprise_Uri + "trade_search";
        public static readonly string CompanyTrade_IntelliTips_Uri = Enterprise_Uri + "trade_tips";
        public static readonly string CompanyTrade_UniversalSearch_Uri = Enterprise_Uri + "trade_usearch";
        public static readonly string Brand_Dtl_Uri = Enterprise_Uri + "brand_dtl";
        public static readonly string Dishonest_Dtl_Uri = Enterprise_Uri + "dishonest_dtl";
        public static readonly string Judge_Dtl_Uri = Enterprise_Uri + "judge_dtl";
        #region community
        public const string Community_Uri = "http://localhost:9081/community.svc/community/";
        public static readonly string Community_Topic_Add_Uri = Community_Uri + "topic";
        public static readonly string Community_Reply_Uri = Community_Uri + "reply";
        public static readonly string Community_Topic_Query_Uri = Community_Uri + "topic_query";
        public static readonly string Community_Topic_Detail_Uri = Community_Uri + "topic_detail";
        public static readonly string Community_Topic_UpDown_Vote_Uri = Community_Uri + "topic/updown_vote";
        #endregion

        #region user
        public const string User_Uri = "http://localhost:9081/user.svc/user/";
        public static readonly string Login_Uri = User_Uri + "login";
        public static readonly string Verify_Code_Get_Uri = User_Uri + "verify_code";
        public static readonly string Register_Uri = User_Uri + "register";
        public static readonly string Pwd_Reset_Uri = User_Uri + "pwd_reset";
        public static readonly string Face_Reset_Uri = User_Uri + "face_reset";
        public static readonly string Notice_Status_Uri = User_Uri + "notice/status";
        public static readonly string History_Query_Uri = User_Uri + "queries";
        public static readonly string Info_Set_Uri = User_Uri + "info_set";
        public static readonly string Info_Get_Uri = User_Uri + "info_get";
        public static readonly string Query_Delete_Uri = User_Uri + "query_delete";
        public static readonly string Query_Drop_Uri = User_Uri + "query_drop";
        public static readonly string Browse_Get_Uri = User_Uri + "browse";
        public static readonly string Browse_Delete_Uri = User_Uri + "browse_delete";
        public static readonly string Browse_Drop_Uri = User_Uri + "browse_drop";
        public static readonly string Notice_Topic_Drop = User_Uri + "notice/topic_drop";
        public static readonly string ExtQuery_History_Uri = User_Uri + "ext_queries";
        public static readonly string Favorites_Uri = User_Uri + "favorites";
        public static readonly string Notices_Get_Uri = User_Uri + "notice/topics";
        public static readonly string Notice_Topic_Remove = User_Uri + "notice/topic_remove";
        public static readonly string Notice_Company_Remove = User_Uri + "notice/company_remove";
        public static readonly string Notice_Companies = User_Uri + "notice/companies";
        #endregion
    }
}
