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

        public static Response Exhibit_Companies() => client.Exhibit_Companies(Request_Composer.Exhibit_Companies()).ToObject<Response>();
        public static Response Exhibit_Search() => client.Exhibit_Search(Request_Composer.Exhibit_Search()).ToObject<Response>();
        #endregion

        #region community
        public static Response Community_Topic_Add() => client.Community_Topic_Add(Request_Composer.Community_Topic_Add()).ToObject<Response>();
        public static Response Community_Reply() => client.Community_Reply(Request_Composer.Community_Reply()).ToObject<Response>();
        public static Response Community_Topic_Query() => client.Community_Topic_Query(Request_Composer.Community_Topic_Query()).ToObject<Response>();
        public static Response Community_Topic_Detail() => client.Community_Topic_Detail(Request_Composer.Community_Topic_Detail()).ToObject<Response>();
        public static Response Community_Topic_UpDown_Vote() => client.Community_Topic_UpDown_Vote(Request_Composer.Community_Topic_UpDown_Vote()).ToObject<Response>();
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
        public static Response Notices_Get() => client.Notices_Get(Request_Composer.Notices_Get()).ToObject<Response>();
        #endregion
    }
}
