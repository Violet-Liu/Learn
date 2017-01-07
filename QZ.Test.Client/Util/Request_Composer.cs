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
using QZ.Instrument.Utility;
using QZ.Instrument.Model;

namespace QZ.Test.Client
{
    public class Request_Composer
    {
        public static string Compose(string request_body_json) =>
            new Request(Request_Head.Request_Heads.First().Set_Token(Global.Instance.Token_1).ToJson().ToEncryption(), request_body_json.ToEncryption()).ToJson();

        public static string Index()
        {
            var head = Request_Head.Request_Heads.First().ToJson();
            var body = new Req_Index() { u_name= "来咯哦哦",u_ifda="2" }.ToJson();
            var request = new Request(head.ToEncryption(Constants.C_Dyn_Key_0), body.ToEncryption(Constants.C_Dyn_Key_0)).ToJson();
            return request;
        }

        public static string Company_Query()
        {
            var head = Request_Head.Request_Heads.First().Set_Token(Global.Instance.Token_1).ToJson();
            var body = Company.Query_Tests[2].ToJson();
            var request = new Request(head.ToEncryption(), body.ToEncryption()).ToJson();
            return request;
        }
        public static string Company_Detail()
        {
            var head = Request_Head.Request_Heads.First().Set_Token(Global.Instance.Token_1).ToJson();
            var body = Company.Detail_Tests[0].ToJson();
            var request = new Request(head.ToEncryption(), body.ToEncryption()).ToJson();
            return request;
        }
        public static string Company_Intelli_Tip()
        {
            var head = Request_Head.Request_Heads.First().Set_Token(Global.Instance.Token_0).ToJson();
            var body = Req_Intelli_Tip.Tests[0].ToJson();
            var request = new Request(head.ToEncryption(), body.ToEncryption()).ToJson();
            return request;
        }

        public static string Company_Invest()
        {
            var head = Request_Head.Request_Heads.First().Set_Token(Global.Instance.Token_0).ToJson();
            var body = Company_Mini_Info.Default.ToJson();
            var request = new Request(head.ToEncryption(), body.ToEncryption()).ToJson();
            return request;
        }
        public static string Company_Report_Send() => Compose(Req_Oc_Mini.Report_Send.ToJson());
        public static string Company_Map() => Compose(Req_Oc_Map.Default[1].ToJson());
        public static string Company_Stock_Holder() => Compose(Company_Mini_Info.Default.ToJson());
        public static string Company_Change() => Compose(Req_Oc_Mini.Defaults[1].ToJson());
        public static string Company_Icpl() => Compose(Req_Oc_Mini.Defaults[0].ToJson());
        public static string Company_Branch() => Compose(Req_Oc_Mini.Defaults[0].ToJson());
        public static string Company_Annual() => Compose(Req_Oc_Annual.Default[0].ToJson());
        public static string Company_Annual_Detail() => Compose(Req_Oc_Annual.Default[1].ToJson());
        public static string Company_Impression() => Compose(Req_Oc_Mini.Defaults[0].ToJson());
        public static string Company_New_Topic() => Compose(Req_Oc_Comment.Default[0].ToJson());
        public static string Company_Reply() => Compose(Req_Oc_Comment.Default[1].ToJson());
        public static string Company_Fresh_Topic() => Compose(Req_Oc_Mini.Defaults[2].ToJson());
        public static string Company_Topic_Query() => Compose(Req_Oc_Mini.Defaults[0].ToJson());
        public static string Company_Topic_Detail() => Compose(Req_Topic_Dtl.Defaults[0].ToJson());
        public static string Company_Favorite_Add() => Compose(Req_Oc_Mini.Defaults[0].ToJson());
        public static string Company_Favorite_Remove() => Compose(new { u_id = "30161", oc_code = "335535875" }.ToJson());
        public static string Company_Topic_UpDown_Vote() => Compose(Req_Topic_Vote.Defaults[0].ToJson());
        public static string Company_UpDown_Vote() => Compose(Req_Topic_Vote.Defaults[2].ToJson());
        public static string Company_Correct() => Compose(Req_Oc_Correct.Default.ToJson());
        public static string Brand_Query() => Compose(Req_Info_Query.Defaults[0].ToJson());
        public static string Oc_Brand_Get() => Compose(Req_Oc_Mini.Oc_Brand_Get.ToJson());
        public static string Oc_Patent_Get() => Compose(Req_Oc_Mini.Oc_Patent_Get.ToJson());
        public static string Oc_Dishonest_Get() => Compose(Req_Oc_Mini.Oc_Dishonest_Get.ToJson());
        public static string Oc_Judge_Get() => Compose(Req_Oc_Mini.Oc_Judge_Get.ToJson());
        public static string CompanyTrade_Search() => Compose(Req_TradeSearch.Defaults[1].ToJson());
        public static string CompanyTrade_UniversalSearch() => Compose(Req_Trade_UniversalSearch.Default().ToJson());
        public static string CompanyTrade_IntelliTips() => Compose(Req_Intelli_Tip.Tests[0].ToJson());
        public static string Brand_Dtl() => Compose(Req_Brand_Dtl.Default.ToJson());
        public static string Dishonest_Dtl() => Compose(Req_Query_Dtl.Dishonest_Dtl.ToJson());
        public static string Judge_Dtl() => Compose(Req_Query_Dtl.Judge_Dtl.ToJson());
        public static string Patent_Query() => Compose(Req_Info_Query.Defaults[2].ToJson());
        public static string Patent_Dtl() => Compose(Req_Patent_Dtl.Default.ToJson());
        public static string Judge_Query() => Compose(Req_Info_Query.Judge_Query.ToJson());
        public static string Dishonest_Query() => Compose(Req_Info_Query.Defaults[4].ToJson());
        public static string Company_Search4Exhibit() => Compose(new { q_sort = 2, pg_index = 1, query_str="", pg_size = 2,u_id="31370",u_name="xiexie" }.ToJson());
        public static string Exhibit_Companies() => Compose(Req_Exhibit_Dtl.Default().ToJson());
        public static string Exhibit_Search() => Compose(Req_Info_Query.Defaults[4].ToJson());

        public static string Vip_Order_Submit() => Compose(new { u_id = "30924", u_name = "dddd", type = 1, pay_type = 1, mobile = "13725569662" }.ToJson());

        public static string Vip_Order_Notify() => Compose(new { u_id = "30924", u_name = "dddd", trade_no = "2017010521001004620232853791", out_trade_no = "hy20170105191003939" }.ToJson());

        #region community
        public static string Community_Topic_Add() => Compose(Req_Cm_Comment.Defaults[2].ToJson());
        public static string Community_Reply() => Compose(Req_Cm_Comment.Defaults[1].ToJson());
        public static string Community_Topic_Query() => Compose(Req_Cm_Topic.Defaults[0].ToJson());
        public static string Community_Topic_Detail() => Compose(Req_Topic_Dtl.Defaults[1].ToJson());
        public static string Community_Topic_UpDown_Vote() => Compose(Req_Topic_Vote.Defaults[4].ToJson());
        public static string Company_CetificationList() => Compose(new { oc_code = "708461136", pg_index = 0, pg_size = 10 }.ToJson());
        #endregion

        #region user
        public static string Login() =>
            new Request(Request_Head.Request_Heads.First().ToJson().ToEncryption(), User_Login.Defaults[1].ToJson().ToEncryption()).ToJson();
        public static string Verify_Code_Get() =>
            new Request(Request_Head.Request_Heads.First().ToJson().ToEncryption(), User_Register.Defaults[0].ToJson().ToEncryption()).ToJson();
        public static string Register() => Compose(User_Register.Defaults[0].ToJson());
        public static string Pwd_Reset() => Compose(User_Register.Defaults[1].ToJson());
        public static string Face_Reset() => Compose(Req_Portrait.Default.ToJson());
        public static string Info_Set() => Compose(Req_User_Info.Defaults[0].ToJson());
        public static string Info_Get() => Compose(Req_User_Info.Defaults[1].ToJson());
        public static string History_Query() => Compose(Req_Cm_Topic.Defaults[0].ToJson());
        public static string Query_Delete() => Compose(Req_Query.Default.ToJson());
        public static string Query_Drop() => Compose(Req_Query.Default.ToJson());
        public static string Browse_Get() => Compose(Req_Query.Default.ToJson());
        public static string Browse_Delete() => Compose(Req_Browse.Default.ToJson());
        public static string Browse_Drop() => Compose(Req_Browse.Default.ToJson());
        public static string ExtQuery_History() => Compose(Req_Info_Query.ExtQuery_History.ToJson());
        public static string FavoritesNew() => Compose(new { u_id = "30161", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Favorites() => Compose(Req_Cm_Topic.Favorites.ToJson());
        public static string Notices_Get() => Compose(Req_Cm_Topic.Notices_Get.ToJson());
        public static string SysNotices_Get() => Compose(new { u_id = "30740", pg_index = 1, pg_size = 10 }.ToJson());
        
        public static string Favorite_Group_Insert() => Compose(new { g_name = "测试添加", u_id = "30740", u_name = "" }.ToJson());
        public static string Favorite_Group_Del() => Compose(new { u_id = "30740", u_name = "", g_id = "8" }.ToJson());
        public static string Favorite_Group_Update() => Compose(new { u_id = "30924", u_name = "", g_id = "36", g_name = "客户12112" }.ToJson());
        public static string Company_Favorite_NewAdd() => Compose(new { oc_code = "100000059", oc_name = "中钢贸易有限公司", oc_area = "1101", u_id = "30740", u_name = "的额而发", g_name = "能吃", q_action = 1 }.ToJson());
        public static string Favorites_GetbyId() => Compose(new { u_id = "30161", g_id = "2", pg_index = 0, pg_size = 10 }.ToJson());
        public static string UnGroupedFavorites_Get() => Compose(new { u_id = "30161", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Favorite_Into_Group() => Compose(new { g_gid = "2", u_id = "30161", fl_ids=new List<int>(){ 450, 449, 448, 447 } }.ToJson());
        public static string Company_LinkCach() => Compose(new { oc_code = "71526726X", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Company_Executes() => Compose(new { oc_code = "579662213", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Company_RegList() => Compose(new { oc_code = "579662213", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Company_InvList() => Compose(new { oc_code = "767430989", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Company_Employs()=> Compose(new { oc_code = "394076699", pg_index = 0, pg_size = 10 }.ToJson());
        public static string Favorite_Note_Add() => Compose(new { fl_id = "8", note = "喜唰唰" }.ToJson());
        public static string Favorite_Note_UP() => Compose(new { n_id = "3", note = "dudu" }.ToJson());
        public static string Favorite_Note_Get() => Compose(new { fl_id = "465", pg_index = 1, pg_size = 10 }.ToJson());
        public static string SysNotice_Single_Del() => Compose(new { u_id = "3011", s_id = "12" }.ToJson());
        public static string Process_SysNoticeDtl_Get() => Compose(new { u_id = "3011", s_id = "13" }.ToJson());
        
        //Compose(Req_Favorite_Add.group_add.ToJson());
        #endregion
    }
}
