using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace QZ.Service.Enterprise
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    [ErrorBehavior]
    [VisitorStaticticBehavior]
    public class Enterprise : IEnterprise
    {
        #region index
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Index(Request request) => CompanyImpl.Process_Index(request);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response Index_Pics() => ServiceImpl.Process_Index_Pics();
        

        /// <summary>
        /// Get area infos in the range of one-timely
        /// </summary>
        /// <returns></returns>
        public Response Area() => ServiceImpl.Process_Area();

        #endregion

        #region company query
        /// <summary>
        /// Query for a company list according to some conditions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Query(Request request) => CompanyImpl.Process_Company_Query(request);

        /// <summary>
        /// recommended company list
        /// In fact, it is just a randomized company query
        /// </summary>
        /// <returns></returns>
        public Response Company_Recommend() => ServiceImpl.Process_Company_Recommend();

        /// <summary>
        /// Intelli tips for company query
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Intelli_Tip(Request request) => ServiceImpl.Process_Company_Intelli_Tip(request);

        /// <summary>
        /// get companies which are scaned by user or added into favorites
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_FavoriteScan(Request request) => CompanyImpl.Process_Company_FavoriteScan(request);
        #endregion

        #region company detail
        /// <summary>
        /// Query for a concrete company detail through oc_code, oc_name, oc_area
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Detail(Request request) => ServiceImpl.Process_Company_Detail(request);
        public Response Company_Trades(Request request) => ServiceImpl.Process_Company_Trades(request);

        public Response Company_Invest(Request request) => ServiceImpl.Process_Company_Invest(request);
        public Response Company_Map(Request request) => ServiceImpl.Process_Company_Map(request);
        public Response Company_Stock_Holder(Request request) => ServiceImpl.Process_Company_Stock_Holder(request);
        public Response Company_Change(Request request) => ServiceImpl.Process_Company_Change(request);
        /// <summary>
        /// Icpl -> Internet Content Provider License
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Icpl(Request request) => ServiceImpl.Process_Company_Icpl(request);
        public Response Company_Branch(Request request) => ServiceImpl.Process_Company_Branch(request);
        public Response Company_Annual(Request request) => ServiceImpl.Process_Company_Annual(request);
        public Response Company_Annual_Detail(Request request) => ServiceImpl.Process_Company_Annual_Detail(request);
        /// <summary>
        /// A simple company detail, used in photo spectrum
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Dtl_Simple(Request request) => ServiceImpl.Process_Company_Dtl_Simple(request);

        /// <summary>
        /// Exhibition info which a give company had participated
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Exhibit_Participate(Request request) => CompanyImpl.Process_Company_Exhibit_Participate(request);
        public Response Exhibit_Detail(Request request) => CompanyImpl.Process_Exhibit_Detail(request);

        public Response Exhibit_Companies(Request request) => CompanyImpl.Process_Exhibit_Companies(request);

        public Response Company_CetificationList(Request request) => CompanyImpl.Process_Company_CetificationList(request);

        public Response Company_CertificateDtl(string ci_id) => CompanyImpl.Process_Company_CetificationDtl(ci_id);

        public Response Company_RegList(Request request) => CompanyImpl.Process_Company_RegList(request);

        public Response Company_InvList(Request request) => CompanyImpl.Process_Company_InvList(request);

        public Response Company_InvDtl(string ogs_id) => CompanyImpl.Process_Company_InvDtl(ogs_id);
        #endregion

        #region company operation i.e. `vote` or `info correct`
        public Response Company_Correct(Request request) => ServiceImpl.Process_Company_Correct(request);
        public Response Company_Favorite_Add(Request request) => ServiceImpl.Process_Company_Favorite_Add(request);
        public Response Company_Favorite_NewAdd(Request request) => ServiceImpl.Process_Company_Favorite_NewAdd(request);
        public Response Company_Favorite_Remove(Request request) => ServiceImpl.Process_Company_Favorite_Remove(request);
        public Response Company_Favorite_NewRemove(Request request) => ServiceImpl.Process_Company_Favorite_NewRemove(request);
        public Response Company_Report_Send(Request request) => ServiceImpl.Process_Company_Report_Send(request);
        public Response Company_Impression(Request request) => ServiceImpl.Process_Company_Impression(request);
        public Response Company_UpDown_Vote(Request request) => ServiceImpl.Process_Company_UpDown_Vote(request);
        #endregion

        #region company topic

        /// <summary>
        /// Fresh company list
        /// </summary>
        /// <returns></returns>
        public Response Company_Fresh() => ServiceImpl.Process_Company_Fresh();

        /// <summary>
        /// Add a new company topic
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_New_Topic(Request request) => ServiceImpl.Process_Company_New_Topic(request);
        public Response Company_Reply(Request request) => ServiceImpl.Process_Company_Reply(request);
        /// <summary>
        /// Fresh company topics
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_Fresh_Topic(Request request) => ServiceImpl.Process_Company_Fresh_Topic(request);
        public Response Company_Topic_Query(Request request) => ServiceImpl.Process_Company_Topic_Query(request);
        public Response Company_Topic_Detail(Request request) => ServiceImpl.Process_Company_Topic_Detail(request);
        public Response Company_Topic_UpDown_Vote(Request request) => ServiceImpl.Process_Company_Topic_UpDown_Vote(request);
        #endregion

        #region personized function
        public Response Comment_TipOff(Request request) => CompanyImpl.Process_Comment_TipOff(request);

        public Response Comment_Shield(Request request) => CompanyImpl.Process_Comment_Shield(request);
        #endregion

        #region hot search
        /// <summary>
        /// Get hot search of companies
        /// </summary>
        /// <param name="pg_size"></param>
        /// <returns></returns>
        public Response Query_Hot(string pg_size) => ServiceImpl.Process_Query_Hot(pg_size);
        /// <summary>
        /// Get hot search for ext-query such as brand, patent, copyright and so on
        /// </summary>
        /// <param name="q_type"></param>
        /// <param name="pg_size"></param>
        /// <returns></returns>
        public Response ExtQuery_Hot(string q_type, string pg_size) => ServiceImpl.Process_ExtQuery_Hot(q_type, pg_size);
        #endregion

        #region company ext i.e. brand patent copyright court...
        public Response Query_Brand(Request request) => ServiceImpl.Process_Query_Brand(request);
        public Response NewQuery_Brand(Request request) => ServiceImpl.Process_NewQuery_Brand(request);
        //public Response ExtQuery_History(Request request) => ServiceImpl.Process_Ext_SearchHistory
        public Response Brand_Dtl(Request request) => ServiceImpl.Process_Brand_Dtl(request);
        public Response Patent_Dtl(Request request) => ServiceImpl.Process_Patent_Dtl(request);
        public Response Patent_NewQuery(Request request) => ServiceImpl.Process_Patent_NewQuery(request);
        public Response Patent_Query(Request request) => ServiceImpl.Process_Patent_Query(request);
        public Response Patent_Universal_Query(Request request) => ServiceImpl.Process_Patent_Universal_Query(request);
        public Response Judge_NewQuery(Request request) => ServiceImpl.Process_Judge_NewQuery(request);
        public Response Judge_Query(Request request) => ServiceImpl.Process_Judge_Query(request);
        public Response Judge_Detail(Request request) => ServiceImpl.Process_Judge_Detail(request);
        public Response Dishonest_Detail(Request request) => ServiceImpl.Process_Dishonest_Dtl(request);
        public Response Dishonest_NewQuery(Request request) => ServiceImpl.Process_Dishonest_NewQuery(request);
        public Response Dishonest_Query(Request request) => ServiceImpl.Process_Dishonest_Query(request);
        public Response Patent_Get(Request request) => ServiceImpl.Process_Patent_Get(request);
        public Response Brand_Get(Request request) => ServiceImpl.Process_Brand_Get(request);
        public Response Copyrights_Get(Request request) => ServiceImpl.Process_Copyrights_Get(request);
        public Response SoftwareCopyright_Detail(Request request) => ServiceImpl.Process_SoftwareCopyright_Detail(request);
        public Response ProductCopyright_Detail(Request request) => ServiceImpl.Process_ProductCopyright_Detail(request);
        public Response Judge_Get(Request request) => ServiceImpl.Process_Judge_Get(request);
        public Response Dishonest_Get(Request request) => ServiceImpl.Process_Dishonest_Get(request);
        #endregion

        #region obsolute
        public Response Company_ScoreMark(Request request) => ServiceImpl.Process_Company_ScoreMark(request);
        public Response Company_ScoreDetail(Request request) => ServiceImpl.Process_Company_ScoreDetail(request);
        #endregion

        #region company trades
        /// <summary>
        /// GB trade infos
        /// </summary>
        /// <returns></returns>
        public Response Company_TradeInfos() => CompanyImpl.Process_Company_TradeInfos();
        /// <summary>
        /// company query by a given GB trade name(actually, is code)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_TradeSearch(Request request) => CompanyImpl.Process_Company_TradeSearch(request);
        public Response Company_ProInfos() => CompanyImpl.Process_Company_ProInfos();
        public Response Company_ProSearch(Request request) => CompanyImpl.Process_Company_ProSearch(request);
        /// <summary>
        /// company query by a given `Forward` trade name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Company_FwdTradeSearch(Request request) => CompanyImpl.Process_Company_FwdTradeSearch(request);
        public Response Company_ExhTradeSearch(Request request) => CompanyImpl.Process_Company_ExhTradeSearch(request);

        public Response Company_TradeIntelliTip(Request request) => CompanyImpl.Process_Company_TradeIntelliTip(request);

        public Response Company_UniversalTradeSearch(Request request) => CompanyImpl.Process_Company_UniversalTradeSearch(request);

        public Response Company_Search4Exhibit(Request request) => CompanyImpl.Process_Company_Search4Exhibit(request);

        public Response Company_Employs(Request request) => CompanyImpl.Process_Employs(request);

        public Response Company_JobDtl(string ogs_id) => CompanyImpl.Process_JobDtl_Get(ogs_id);

        public Response Company_SearchItemSite(Request request) => CompanyImpl.Process_SearchItemSite_Get(request);

        public Response Company_Executes(Request request) => CompanyImpl.Process_Executes_Get(request);

        public Response Company_ExecuteDtl(string zx_id)=> CompanyImpl.Process_ExecuteDtl_Get(zx_id);

        public Response Company_LinkCach(Request request) => CompanyImpl.Process_LinkCach_Get(request);

        public Response Company_Exhibitions(Request request) => CompanyImpl.Process_Exhibitions_Get(request);

        public Response Company_Report_Collect(Request request) => CompanyImpl.Process_Report_Collect(request);

        public Response Company_Claim_Submit(Request request) => CompanyImpl.Process_Claim_Submit(request);

        //public Response Company_Query_VipExport(Request request)=>
        #endregion

    }
}
