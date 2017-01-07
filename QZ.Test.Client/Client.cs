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
    public class Client
    {
        private static readonly Enterprise client = new Enterprise();

        #region company
        public static Task<Response> Index_Async()
        {
            return client.IndexAsync(Request_Composer.Index()).ContinueWith(t => t.Result.ToObject<Response>());
        }

        public static Response Index()
        {
            return client.Index(Request_Composer.Index()).ToObject<Response>();
        }
        public static Response Company_Trades() => client.Company_Trades().ToObject<Response>();

        public static Response Area()
        {
            return client.Area().ToObject<Response>();
        }
        public static Response Company_Query()
        {
            return client.Company_Query(Request_Composer.Company_Query()).ToObject<Response>();
        }

        public static Response Company_Detail()
        {
            return client.Company_Detail(Request_Composer.Company_Detail()).ToObject<Response>();
        }

        public static Response Company_Intelli_Tip()
        {
            return client.Company_Intelli_Tip(Request_Composer.Company_Intelli_Tip()).ToObject<Response>();
        }
        public static Response Company_Invest()
        {
            return client.Company_Invest(Request_Composer.Company_Invest()).ToObject<Response>();
        }
        public static Response Company_Report_Send() => client.Company_Report_Send(Request_Composer.Company_Report_Send()).ToObject<Response>();


        public static Response Company_Map() =>
            client.Company_Map(Request_Composer.Company_Map()).ToObject<Response>();
        public static Response Company_Stock_Holder() =>
            client.Company_Stock_Holder(Request_Composer.Company_Stock_Holder()).ToObject<Response>();

        public static Response Company_Change() =>
            client.Company_Change(Request_Composer.Company_Change()).ToObject<Response>();

        public static Response Company_Icpl() =>
            client.Company_Icpl(Request_Composer.Company_Icpl()).ToObject<Response>();

        public static Response Company_Branch() =>
            client.Company_Branch(Request_Composer.Company_Branch()).ToObject<Response>();

        public static Response Company_Annual() =>
            client.Company_Annual(Request_Composer.Company_Annual()).ToObject<Response>();

        public static Response Company_Annual_Detail() =>
            client.Company_Annual_Detail(Request_Composer.Company_Annual_Detail()).ToObject<Response>();

        public static Response Company_Impression() =>
            client.Company_Impression(Request_Composer.Company_Impression()).ToObject<Response>();

        public static Response Company_New_Topic() =>
            client.Company_New_Topic(Request_Composer.Company_New_Topic()).ToObject<Response>();

        public static Response Company_Reply() =>
            client.Company_Reply(Request_Composer.Company_Reply()).ToObject<Response>();

        public static Response Company_Fresh_Topic() =>
            client.Company_Fresh_Topic(Request_Composer.Company_Fresh_Topic()).ToObject<Response>();
        public static Response Company_Topic_Query() =>
            client.Company_Topic_Query(Request_Composer.Company_Topic_Query()).ToObject<Response>();
        public static Response Company_Topic_Detail() =>
            client.Company_Topic_Detail(Request_Composer.Company_Topic_Detail()).ToObject<Response>();
        public static Response Company_Favorite_Add() =>
            client.Company_Favorite_Add(Request_Composer.Company_Favorite_Add()).ToObject<Response>();
        public static Response Company_Favorite_NewAdd() =>
            client.Company_Favorite_NewAdd(Request_Composer.Company_Favorite_NewAdd()).ToObject<Response>();
        public static Response Company_Favorite_Remove() =>
            client.Company_Favorite_Remove(Request_Composer.Company_Favorite_Remove()).ToObject<Response>();

        public static Response Query_Hot() => client.Query_Hot().ToObject<Response>();
        public static Response Company_Topic_UpDown_Vote() => client.Company_Topic_UpDown_Vote(Request_Composer.Company_Topic_UpDown_Vote()).ToObject<Response>();
        public static Response Company_UpDown_Vote() => client.Company_UpDown_Vote(Request_Composer.Company_UpDown_Vote()).ToObject<Response>();
        public static Response Company_Correct() => client.Company_Correct(Request_Composer.Company_Correct()).ToObject<Response>();
        public static Response Brand_Query() => client.Brand_Query(Request_Composer.Brand_Query()).ToObject<Response>();
        public static Response Oc_Brand_Get() => client.Oc_Brand_Get(Request_Composer.Oc_Brand_Get()).ToObject<Response>();
        public static Response Oc_Patent_Get() => client.Oc_Patent_Get(Request_Composer.Oc_Patent_Get()).ToObject<Response>();
        public static Response Oc_Dishonest_Get() => client.Oc_Dishonest_Get(Request_Composer.Oc_Dishonest_Get()).ToObject<Response>();
        public static Response Oc_Judge_Get() => client.Oc_Judge_Get(Request_Composer.Oc_Judge_Get()).ToObject<Response>();
        public static Response CompanyTrade_Search() => client.CompanyTrade_Search(Request_Composer.CompanyTrade_Search()).ToObject<Response>();
        public static Response CompanyTrade_UniversalSearch() => client.CompanyTrade_UniversalSearch(Request_Composer.CompanyTrade_UniversalSearch()).ToObject<Response>();
        public static Response CompanyTrade_IntelliTips() => client.CompanyTrade_IntelliTips(Request_Composer.CompanyTrade_IntelliTips()).ToObject<Response>();
        public static Response Brand_Dtl() => client.Brand_Dtl(Request_Composer.Brand_Dtl()).ToObject<Response>();
        public static Response Dishonest_Dtl() => client.Dishonest_Dtl(Request_Composer.Dishonest_Dtl()).ToObject<Response>();
        public static Response Judge_Dtl() => client.Judge_Dtl(Request_Composer.Judge_Dtl()).ToObject<Response>();
        public static Response Patent_Query() => client.Patent_Query(Request_Composer.Patent_Query()).ToObject<Response>();
        public static Response Patent_Dtl() => client.Patent_Dtl(Request_Composer.Patent_Dtl()).ToObject<Response>();
        public static Response Patent_Universal_Query() => client.Patent_Universal_Query(Request_Composer.Patent_Query()).ToObject<Response>();
        public static Response Judge_Query() => client.Judge_Query(Request_Composer.Judge_Query()).ToObject<Response>();
        public static Response Dishonest_Query() => client.Dishonest_Query(Request_Composer.Dishonest_Query()).ToObject<Response>();
        public static Response ExtQuery_Hot() => client.ExtQuery_Hot().ToObject<Response>();
        public static Response Company_Search4Exhibit() => client.Company_Search4Exhibit(Request_Composer.Company_Search4Exhibit()).ToObject<Response>();
       
        public static Response Exhibit_Companies() => client.Exhibit_Companies(Request_Composer.Exhibit_Companies()).ToObject<Response>();
        public static Response Exhibit_Search() => client.Exhibit_Search(Request_Composer.Exhibit_Search()).ToObject<Response>();

        public static Response Vip_Order_Submit() => client.Vip_Order_Submit(Request_Composer.Vip_Order_Submit()).ToObject<Response>();

        public static Response Vip_Order_Notify() => client.Vip_Order_Notify(Request_Composer.Vip_Order_Notify()).ToObject<Response>();

        public static String test() => client.test();
        #endregion

        #region community
        public static Response Community_Topic_Add() => client.Community_Topic_Add(Request_Composer.Community_Topic_Add()).ToObject<Response>();
        public static Response Community_Reply() => client.Community_Reply(Request_Composer.Community_Reply()).ToObject<Response>();
        public static Response Community_Topic_Query() => client.Community_Topic_Query(Request_Composer.Community_Topic_Query()).ToObject<Response>();
        public static Response Community_Topic_Detail() => client.Community_Topic_Detail(Request_Composer.Community_Topic_Detail()).ToObject<Response>();
        public static Response Community_Topic_UpDown_Vote() => client.Community_Topic_UpDown_Vote(Request_Composer.Community_Topic_UpDown_Vote()).ToObject<Response>();
        public static Response Company_CetificationList()=>client.Company_CetificationList(Request_Composer.Company_CetificationList()).ToObject<Response>();
        #endregion

        #region user
        public static Response Login() =>
            client.Login(Request_Composer.Login()).ToObject<Response>();
        public static Response Verify_Code_Get() =>
            client.Verify_Code_Get(Request_Composer.Verify_Code_Get()).ToObject<Response>();
        public static Response Register() =>
            client.Register(Request_Composer.Register()).ToObject<Response>();
        public static Response Pwd_Reset() =>
            client.Pwd_Reset(Request_Composer.Pwd_Reset()).ToObject<Response>();
        public static Response Face_Reset() =>
            client.Face_Reset(Request_Composer.Face_Reset()).ToObject<Response>();
        public static Response Info_Set() => client.Info_Set(Request_Composer.Info_Set()).ToObject<Response>();
        public static Response Info_Get() => client.Info_Get(Request_Composer.Info_Get()).ToObject<Response>();
        public static Response History_Query() => client.History_Query(Request_Composer.History_Query()).ToObject<Response>();
        public static Response Query_Delete() => client.Query_Delete(Request_Composer.Query_Delete()).ToObject<Response>();
        public static Response Query_Drop() => client.Query_Drop(Request_Composer.Query_Drop()).ToObject<Response>();
        public static Response Browse_Get() => client.Browse_Get(Request_Composer.Browse_Get()).ToObject<Response>();
        public static Response Browse_Delete() => client.Browse_Delete(Request_Composer.Browse_Delete()).ToObject<Response>();
        public static Response Browse_Drop() => client.Browse_Drop(Request_Composer.Browse_Drop()).ToObject<Response>();
        public static Response ExtQuery_History() => client.ExtQuery_History(Request_Composer.ExtQuery_History()).ToObject<Response>();
        public static Response Favorites() => client.Favorites(Request_Composer.Favorites()).ToObject<Response>();
        public static Response FavoritesNew() => client.FavoritesNew(Request_Composer.FavoritesNew()).ToObject<Response>();
        public static Response Notices_Get() => client.Notices_Get(Request_Composer.Notices_Get()).ToObject<Response>();
        public static Response SysNotices_Get() => client.SysNotices_Get(Request_Composer.SysNotices_Get()).ToObject<Response>();
        
        public static Response Favorite_Group_Insert() => client.Favorite_Group_Insert(Request_Composer.Favorite_Group_Insert()).ToObject<Response>();
        public static Response Favorite_Group_Del() => client.Favorite_Group_Del(Request_Composer.Favorite_Group_Del()).ToObject<Response>();
        public static Response Favorite_Group_Update()=>client.Favorite_Group_Update(Request_Composer.Favorite_Group_Update()).ToObject<Response>();
        public static Response FavoriteGroups_Get() => client.FavoriteGroups_Get().ToObject<Response>();
        public static Response Favorites_GetbyId()=>client.Favorites_GetbyId(Request_Composer.Favorites_GetbyId()).ToObject<Response>();
        public static Response UnGroupedFavorites_Get()=>client.UnGroupedFavorites_Get(Request_Composer.UnGroupedFavorites_Get()).ToObject<Response>();
        public static Response Favorite_Into_Group()=>client.Favorite_Into_Group(Request_Composer.Favorite_Into_Group()).ToObject<Response>();
        public static Response Favorite_Out_Group() => client.Favorite_Out_Group().ToObject<Response>();
        public static Response Company_CertificateDtl()=>client.Company_CertificateDtl().ToObject<Response>();
        public static Response Company_InvDtl() => client.Company_InvDtl().ToObject<Response>();
        public static Response Company_JobDtl() => client.Company_JobDtl().ToObject<Response>();
        public static Response Company_ExecuteDtl() => client.Company_ExecuteDtl().ToObject<Response>();
        
        public static Response Company_LinkCach() => client.Company_LinkCach(Request_Composer.Company_LinkCach()).ToObject<Response>();
        public static Response Company_Executes() => client.Company_Executes(Request_Composer.Company_Executes()).ToObject<Response>();
        public static Response Company_RegList()=>client.Company_RegList(Request_Composer.Company_RegList()).ToObject<Response>();
        public static Response Company_InvList() => client.Company_InvList(Request_Composer.Company_InvList()).ToObject<Response>();
        public static Response Company_Employs() => client.Company_Employs(Request_Composer.Company_Employs()).ToObject<Response>();
        public static Response Favorite_Note_Add() => client.Favorite_Note_Add(Request_Composer.Favorite_Note_Add()).ToObject<Response>();
        public static Response Favorite_Note_UP() => client.Favorite_Note_UP(Request_Composer.Favorite_Note_UP()).ToObject<Response>();

        public static Response Favorite_Note_Get() => client.Favorite_Note_Get(Request_Composer.Favorite_Note_Get()).ToObject<Response>();
        public static Response Favorite_Note_Del() => client.Favorite_Note_Del().ToObject<Response>();
        public static Response SysNotice_All_Del() => client.SysNotice_All_Del().ToObject<Response>();
        public static Response SysNotice_Single_Del() => client.SysNotice_Single_Del(Request_Composer.SysNotice_Single_Del()).ToObject<Response>();
        public static Response Process_SysNoticeDtl_Get() => client.Process_SysNoticeDtl_Get(Request_Composer.Process_SysNoticeDtl_Get()).ToObject<Response>();
        
        public static Response Brand_NewQuery(Req_Info_Query req) => client.Brand_NewQuery(Request_Composer.Compose(req.ToJson())).ToObject<Response>();
        public static Response Patent_NewQuery(Req_Info_Query req) => client.Patent_NewQuery(Request_Composer.Compose(req.ToJson())).ToObject<Response>();
        public static Response Judge_NewQuery(Req_Info_Query req) => client.Judge_NewQuery(Request_Composer.Compose(req.ToJson())).ToObject<Response>();
        public static Response Dishonest_NewQuery(Req_Info_Query req) => client.Dishonest_NewQuery(Request_Composer.Compose(req.ToJson())).ToObject<Response>();


        //public static Response Company_RegList()=>client.Company_RegList(Request_Composer.Company_RegList()).ToObject<Response>();
        #endregion
    }
}
