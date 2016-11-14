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
    public class Simulator
    {
        #region enterprise
        /// <summary>
        /// Call service interface "Index"
        /// </summary>
        public void Index_Async()
        {
            // call service -> process head -> store body data to local buffer
            var task = Client.Index_Async().ContinueWith(resp => resp.Result.Process_Head().Do(body => Global.Instance.Set_Resp_Index(body.ToDecryption().ToObject<Resp_Index>())));
            task.Wait();
        }
        public void Index() => Client.Index().Response_Handle<Resp_Index>();
        public void Area() => Global.Instance.Response = Client.Area().Body;
        public void Company_Trades() => Client.Company_Trades().Response_Handle<Trades>();
        public void Company_Query() => Client.Company_Query().Response_Handle<Resp_Company_List>(); 
        public void Company_Detail() => Client.Company_Detail().Response_Handle<Resp_Company_Detail>();
        public void Company_Intelli_Tip() => Client.Company_Intelli_Tip().Response_Handle<Resp_Intelli_Tip>();
        public void Company_Invest() => Client.Company_Invest().Response_Handle<Resp_Invest>();
        public void Company_Report_Send() => Client.Company_Report_Send().Response_Handle<Response>();
        public void Company_Map() => Client.Company_Map().Response_Handle<Resp_Oc_Map>();
        public void Company_Stock_Holder()=> Client.Company_Stock_Holder().Response_Handle<Resp_Oc_Sh>();
        public void Company_Change() => Client.Company_Change().Response_Handle<Resp_Oc_Change>();
        public void Company_Icpl() => Client.Company_Icpl().Response_Handle<Resp_Icpl>();
        public void Company_Branch() => Client.Company_Branch().Response_Handle<Resp_Oc_Change>();
        public void Company_Annual() => Client.Company_Annual().Response_Handle<Resp_Annual_Abs>();
        public void Company_Annual_Detail() => Client.Company_Annual_Detail().Response_Handle<Resp_Annual_Abs>();
        public void Company_Impression() => Client.Company_Impression().Response_Handle<Company_Impression>();
        public void Company_New_Topic() => Client.Company_New_Topic().Response_Handle<Resp_Binary>();
        public void Company_Reply() => Client.Company_Reply().Response_Handle<Resp_Binary>();
        public void Company_Fresh_Topic() => Client.Company_Fresh_Topic().Response_Handle<Resp_Topics_Abs>();
        public void Company_Topic_Query() => Client.Company_Topic_Query().Response_Handle<Resp_Topics_Abs>();
        public void Company_Topic_Detail() => Client.Company_Topic_Detail().Response_Handle<List<Reply_Dtl>>();
        public void Company_Favorite_Add() => Client.Company_Favorite_Add().Response_Handle<Resp_Binary>();
        public void Company_Favorite_Remove() => Client.Company_Favorite_Remove().Response_Handle<Resp_Binary>();
        public void Query_Hot() => Client.Query_Hot().Response_Handle<List<Query_Hot>>();
        public void Company_Topic_UpDown_Vote() => Client.Company_Topic_UpDown_Vote().Response_Handle<Resp_Binary>();
        public void Company_UpDown_Vote() => Client.Company_UpDown_Vote().Response_Handle<Resp_Binary>();
        public void Company_Correct() => Client.Company_Correct().Response_Handle<Resp_Binary>();
        public void Brand_Query() => Client.Brand_Query().Response_Handle<Resp_Brands>();
        public void Oc_Brand_Get() => Client.Oc_Brand_Get().Response_Handle<Resp_Brands>();
        public void Oc_Patent_Get() => Client.Oc_Patent_Get().Response_Handle<Resp_Patent_Dtls>();
        public void Oc_Dishonest_Get() => Client.Oc_Dishonest_Get().Response_Handle<Resp_Dishonests>();
        public void Oc_Judge_Get() => Client.Oc_Judge_Get().Response_Handle<Resp_Judges>();
        public void CompanyTrade_Search() => Client.CompanyTrade_Search().Response_Handle<Resp_Company_List>();
        public void CompanyTrade_UniversalSearch() => Client.CompanyTrade_UniversalSearch().Response_Handle<Resp_Company_List>();
        public void CompanyTrade_IntelliTips() => Client.CompanyTrade_IntelliTips().Response_Handle<Trade_Intelli_Tip>();
        public void Brand_Dtl() => Client.Brand_Dtl().Response_Handle<Brand_Dtl>();
        public void Dishonest_Dtl() => Client.Dishonest_Dtl().Response_Handle<Dishonest_Dtl>();
        public void Judge_Dtl() => Client.Judge_Dtl().Response_Handle_1<Judge_Dtl>();
        public void Patent_Query() => Client.Patent_Query().Response_Handle<Resp_Patents>();
        public void Patent_Dtl() => Client.Patent_Dtl().Response_Handle<Patent_Dtl>();
        public void Patent_Universal_Query() => Client.Patent_Universal_Query().Response_Handle<Resp_Patents>();
        public void Judge_Query() => Client.Judge_Query().Response_Handle<Resp_Judges>();
        public void Dishonest_Query() => Client.Dishonest_Query().Response_Handle<Resp_Judges>();
        public void ExtQuery_Hot() => Client.ExtQuery_Hot().Response_Handle<List<string>>();

        public void Exhibit_Companies() => Client.Exhibit_Companies().Response_Handle<List<ExhibitCompany>>();
        public void Exhibit_Search() => Client.Exhibit_Search().Response_Handle<Resp_Exhibit_List>();
        #endregion

        #region community
        public void Community_Topic_Add() => Client.Community_Topic_Add().Response_Handle<Resp_Binary>();
        public void Community_Reply() => Client.Community_Reply().Response_Handle<Resp_Binary>();
        public void Community_Topic_Query() => Client.Community_Topic_Query().Response_Handle<Resp_Cm_Topics_Dtl>();
        public void Community_Topic_Detail() => Client.Community_Topic_Detail().Response_Handle<List<Reply_Dtl>>();
        public void Community_Topic_UpDown_Vote() => Client.Community_Topic_UpDown_Vote().Response_Handle<Resp_Binary>();
        #endregion

        #region user
        public void Login() => Client.Login().Response_Handle<Resp_Login>();
        public void Verify_Code_Get() => Client.Verify_Code_Get().Response_Handle<Resp_Binary>();
        public void Register() => Client.Register().Response_Handle<Resp_Binary>();
        public void Pwd_Reset() => Client.Pwd_Reset().Response_Handle<Resp_Binary>();
        public void Face_Reset() => Client.Face_Reset().Response_Handle<Resp_Login>();
        public void Info_Set() => Client.Info_Set().Response_Handle<Resp_User_Info_Set>();
        public void Info_Get() => Client.Info_Get().Response_Handle<User_Append_Info>();
        public void Query() => Client.History_Query().Response_Handle<List<History_Query>>();
        public void Query_Delete() => Client.Query_Delete().Response_Handle<List<History_Query>>();
        public void Query_Drop() => Client.Query_Drop().Response_Handle<List<History_Query>>();
        public void Browse_Get() => Client.Browse_Get().Response_Handle<List<History_Query>>();
        public void Browse_Delete() => Client.Browse_Delete().Response_Handle<List<History_Query>>();
        public void Browse_Drop() => Client.Browse_Drop().Response_Handle<List<History_Query>>();
        public void ExtQuery_History() => Client.ExtQuery_History().Response_Handle<Ext_SearchHistory>();
        public void Favorites() => Client.Favorites().Response_Handle<Resp_Favorites>();
        public void Notices_Get() => Client.Notices_Get().Response_Handle<Resp_Topic_Notice>();
        #endregion


        #region test
        public List<string> ForwardTrade_Get()
        {
            var client = new TradeAnalysis.TradeAnalysisServiceClient();
            return client.GetAllForwardTradeCategory().Select(p => p.Key).ToList();
        }
        #endregion
    }
}
