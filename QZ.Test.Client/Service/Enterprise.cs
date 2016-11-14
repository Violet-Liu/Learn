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
using QZ.Instrument.Model;

namespace QZ.Test.Client
{
    class Enterprise : ClientBase
    {
        //public string Index(string request)
        //{
        //    var channel = CreateChannel(Constants.Enterprise_Uri);
        //    return channel.Index(request);
        //}

        public Task<string> IndexAsync(string request)
        {
            return PostAsync(Constants.Index_Uri, request);
        }

        public string Index(string request)
        {
            return Post(Constants.Index_Uri, request);
        }

        public string Area()
        {
            return Get(Constants.Area_Uri);
        }
        public string Company_Trades() => Get(Constants.Company_Trades_Uri);

        public string Company_Query(string request)
        {
            return Post(Constants.Company_Query_Uri, request);
        }
        public string Company_Detail(string request)
        {
            return Post(Constants.Company_Detail_Uri, request);
        }
        public string Company_Intelli_Tip(string request)
        {
            return Post(Constants.Company_Intelli_Tip_Uri, request);
        }

        public string Company_Invest(string request) => Post(Constants.Company_Invest_Uri, request);
        public string Company_Report_Send(string request) => Post(Constants.Company_Report_Send_Uri, request);
        public string Company_Map(string request) => Post(Constants.Company_Map_Uri, request);
        public string Company_Stock_Holder(string request) => Post(Constants.Company_Stock_Holder_Uri, request);

        public string Company_Change(string request) => Post(Constants.Company_Change_Uri, request);

        public string Company_Icpl(string request) => Post(Constants.Company_Icpl_Uri, request);
        public string Company_Branch(string request) => Post(Constants.Company_Branch_Uri, request);
        public string Company_Annual(string request) => Post(Constants.Company_Annual_Uri, request);

        public string Company_Annual_Detail(string request) => Post(Constants.Company_Annual_Detail_Uri, request);
        public string Company_Impression(string request) => Post(Constants.Company_Impression_Uri, request);
        public string Company_New_Topic(string request) => Post(Constants.Company_New_Topic_Uri, request);

        public string Company_Reply(string request) => Post(Constants.Company_Reply_Uri, request);
        public string Company_Fresh_Topic(string request) => Post(Constants.Company_Fresh_Topic_Uri, request);
        public string Company_Topic_Query(string request) => Post(Constants.Company_Topic_Query_Uri, request);
        public string Company_Topic_Detail(string request) => Post(Constants.Company_Topic_Detail_Uri, request);
        public string Company_Favorite_Add(string request) => Post(Constants.Company_Favorite_Add_Uri, request);
        public string Company_Favorite_Remove(string request) => Post(Constants.Company_Favorite_Remove_Uri, request);
        public string Query_Hot() => Get(Constants.Query_Hot_Uri);
        public string Company_Topic_UpDown_Vote(string request) => Post(Constants.Company_Topic_UpDown_Vote_Uri, request);
        public string Company_UpDown_Vote(string request) => Post(Constants.Company_UpDown_Vote_Uri, request);
        public string Company_Correct(string request) => Post(Constants.Company_Correct_Uri, request);
        public string Brand_Query(string request) => Post(Constants.Brand_Query_Uri, request);
        public string Oc_Brand_Get(string request) => Post(Constants.Oc_Brand_Get_Uri, request);
        public string Oc_Patent_Get(string request) => Post(Constants.Oc_Patent_Get_Uri, request);
        public string Oc_Dishonest_Get(string request) => Post(Constants.Oc_Dishonest_Get_Uri, request);
        public string Oc_Judge_Get(string request) => Post(Constants.Oc_Judge_Get_Uri, request);
        public string CompanyTrade_Search(string request) => Post(Constants.CompanyTrade_Search_Uri, request);
        public string CompanyTrade_IntelliTips(string request) => Post(Constants.CompanyTrade_IntelliTips_Uri, request);
        public string CompanyTrade_UniversalSearch(string request) => Post(Constants.CompanyTrade_UniversalSearch_Uri, request);
        public string Brand_Dtl(string request) => Post(Constants.Brand_Dtl_Uri, request);
        public string Dishonest_Dtl(string request) => Post(Constants.Dishonest_Dtl_Uri, request);
        public string Judge_Dtl(string request) => Post(Constants.Judge_Dtl_Uri, request);
        public string Patent_Query(string request) => Post(Constants.Patent_Query_Uri, request);
        public string Patent_Dtl(string request) => Post(Constants.Patent_Dtl_Uri, request);
        public string Patent_Universal_Query(string request) => Post(Constants.Patent_Universal_Query_Uri, request);
        public string Judge_Query(string request) => Post(Constants.Judge_Query_Uri, request);
        public string Dishonest_Query(string request) => Post(Constants.Dishonest_Query_Uri, request);
        public string ExtQuery_Hot() => Get(Constants.ExtQuery_Hot_Uri);
        public string Exhibit_Tips(string request) => Post(Constants.Exhibit_Tips_Uri, request);
        public string Exhibit_Companies(string request) => Post(Constants.Exh_Companies_Uri, request);
        public string Exhibit_Search(string request) => Post(Constants.Exh_Search_Uri, request);
        public string Community_Topic_Add(string request) => Post(Constants.Community_Topic_Add_Uri, request);
        public string Community_Reply(string request) => Post(Constants.Community_Reply_Uri, request);
        public string Community_Topic_Query(string request) => Post(Constants.Community_Topic_Query_Uri, request);
        public string Community_Topic_Detail(string request) => Post(Constants.Community_Topic_Detail_Uri, request);
        public string Community_Topic_UpDown_Vote(string request) => Post(Constants.Community_Topic_UpDown_Vote_Uri, request);
        #region
        public string Login(string request) => Post(Constants.Login_Uri, request);
        public string Verify_Code_Get(string request) => Post(Constants.Verify_Code_Get_Uri, request);
        public string Register(string request) => Post(Constants.Register_Uri, request);
        public string Pwd_Reset(string request) => Post(Constants.Pwd_Reset_Uri, request);
        public string Face_Reset(string request) => Post(Constants.Face_Reset_Uri, request);
        public string Info_Set(string request) => Post(Constants.Info_Set_Uri, request);
        public string Info_Get(string request) => Post(Constants.Info_Get_Uri, request);
        public string History_Query(string request) => Post(Constants.History_Query_Uri, request);
        public string Query_Delete(string request) => Post(Constants.Query_Delete_Uri, request);
        public string Query_Drop(string request) => Post(Constants.Query_Drop_Uri, request);
        public string Browse_Get(string request) => Post(Constants.Browse_Get_Uri, request);
        public string Browse_Delete(string request) => Post(Constants.Browse_Delete_Uri, request);
        public string Browse_Drop(string request) => Post(Constants.Browse_Drop_Uri, request);
        public string ExtQuery_History(string request) => Post(Constants.ExtQuery_History_Uri, request);
        public string Favorites(string request) => Post(Constants.Favorites_Uri, request);
        public string Notices_Get(string request) => Post(Constants.Notices_Get_Uri, request);
        #endregion
    }
}
