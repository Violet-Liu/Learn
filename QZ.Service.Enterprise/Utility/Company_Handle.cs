using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

using QZ.Foundation.Monad;
using QZ.Foundation.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.Client;

namespace QZ.Service.Enterprise
{
    public static class Company_Handle
    {
        #region company trade
        /// <summary>
        /// Trade tree seeds grow up with child or grand child nodes
        /// If there is a big cascade relationship, trade tree should grow recursively(e.g. method below should be used)
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        /// <remarks><b>This method thinks the trade infos are stored in database with some order</b></remarks>
        public static void TradeTree_Grow(List<Trade> seed)
        {
            var gb_trades = DataAccess.TradeCategory_AllSelect();
            foreach (var t in gb_trades)
            {
                var len = t.ts_id.Length;
                if (len == 2)
                {
                    var parent = seed.FirstOrDefault(s => s.code == t.ts_tc_id);
                    if (parent != null)     // we think here parent can not be null, otherwise please check common sense of GB trade
                        parent.trades.Add(new Trade(t.ts_id, t.ts_name));
                }
                else if (len == 3)
                {
                    var first = seed.FirstOrDefault(s => s.code == t.ts_tc_id); // first's level = 1
                    if (first != null)
                    {
                        var second = first.trades.FirstOrDefault(trade => t.ts_id.StartsWith(trade.code));  // second's level = 2
                        if (second == null)
                        {
                            //second = new Trade(t.ts_id.Substring(0, 2), first.name);
                            //first.trades.Add(second);
                            continue;
                        }
                        second.trades.Add(new Trade(t.ts_id, t.ts_name));
                    }
                }
                else if (len == 4)
                {
                    var first = seed.FirstOrDefault(s => s.code == t.ts_tc_id);
                    if (first != null)
                    {
                        var second = first.trades.FirstOrDefault(tr => t.ts_id.StartsWith(tr.code));
                        if (second != null) // here we think the second's can not be null
                        {
                            var third = second.trades.FirstOrDefault(trade => t.ts_id.StartsWith(trade.code));
                            if (third == null)
                            {
                                continue;
                                //third = new Trade(t.ts_id.Substring(0, 3), second.name);
                                //second.trades.Add(third);
                            }
                            third.trades.Add(new Trade(t.ts_id, t.ts_name));
                        }
                    }                        
                }
            }
        }

        /// <summary>
        /// Grow a trade tree recursively.
        /// This method is adpated in the case of growing a tree with a big cascade relationshiip
        /// </summary>
        /// <param name="seed"></param>
        public static void TradeTree_RecursiveGrow(List<Trade> seed)
        {
            var gb_trades = DataAccess.TradeCategory_AllSelect();
            foreach(var t in gb_trades) 
                TradeTree_RecursiveAdd(seed.FirstOrDefault(s => s.code == t.ts_tc_id), t);            
        }

        public static void TradeTree_RecursiveAdd(Trade trade, TradeEntity t)
        {
            if (trade == null)
                return;

            if (trade.level == t.ts_id.Length - 2)    // 目标节点 t 的 候选 parents 所在的 trade
                trade.trades.FirstOrDefault(tr => t.ts_id.StartsWith(tr.code))?.trades?.Add(new Trade(t.ts_id, t.ts_name));
            else
                TradeTree_RecursiveAdd(trade.trades.FirstOrDefault(tr => t.ts_id.StartsWith(tr.code)), t);
        }

        public static List<ProCat> ProCatTree_Create(List<ProductEntity> proentities)
        {
            var pros = new List<ProCat>(95);
            foreach(var p in proentities)
            {
                if(p.pc_path.Length == 2)   // 第一层级
                {
                    var pc = new ProCat(p.pc_path, p.pc_name);
                    pros.Add(pc);
                }
                else if(p.pc_path.Length == 4)  // 第二层级
                {
                    var pc = new ProCat(p.pc_path, p.pc_name, true);
                    var prefix = p.pc_path.Substring(0, 2);
                    for(int i = pros.Count - 1; i > -1; i--)
                    {
                        if(pros[i].code == prefix)
                        {
                            pros[i].pros.Add(pc);
                            break;
                        }
                    }
                }
            }
            return pros;
        }

        #endregion

        public static Maybe<Resp_Company_List> CompanyList_Query(this Company company) => 
            company.ToMaybe()
                   .DoWhen(_ => !VisitorStatisticHandler.AuthorizeIp(),     // ip checking to provent from catching info illegelly
                           q => q.Pg_Index(1).Pg_Size(10).Input_Cutoff())
                   .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                   q => q.Input(Regex.Replace(q.oc_name, @"[\\%#.]", "", RegexOptions.Compiled)))
                   .Do(q => DataAccess.SearchHistory_Insert(q, q.u_id.ToInt() > 0))        // Insert Search history table      /* Using Do Operation if it can do */
                   .Where(q => q.pg_index < ConfigurationManager.AppSettings["query_pg_limit"].ToInt())
                   .ShiftWhenOrElse(q => q.v == 1,
                        q => NextGen_Company_Search(q),
                        q => Req_Ext.PrevGen_Company_Search(q))
                    .Do(q=>q.oc_list.ForEach(u=>u.oc_reg_capital=Util.InvestMoneyHandle(u.oc_reg_capital)));

        

        private static Resp_Company_List NextGen_Company_Search(Company c)
        {
            Nest.ISearchResponse<Instrument.Client.ES_Company> resp;
            if(c.q_type == q_type.q_general)
            {
                resp = Company.Filter_Flag_Get(c) ? ESClient.Company_General_Filter_Search(c) : ESClient.Company_General_Search(c);
            }
            else
            {
                if (c.oc_trade == "00")
                    c.oc_trade = "";
                resp = ESClient.Company_Advanced_Search(c);
            }
            return ResponseAdaptor.Search2CompanyList(resp, c);
        }

        public static Resp_Binary Cmt_TipOff_Handle(this Req_Cmt_TipOff to)
        {
            var accuseId = to.accuse_uid.ToInt();
            
            var tip = DataAccess.Cmt_TipOff_Select(to.cmt_id, to.cmt_type, accuseId);
            if (tip == null)
            {
                var t = new CommentTipOffInfo();
                t.cto_BeiGao_Uid = to.accused_uid.ToInt();
                t.cto_BeiGao_Uname = to.accused_uname ?? "";
                t.cto_des = to.to_des;
                t.cto_status = 1;
                t.cto_tiezi_id = to.cmt_id;
                t.cto_tiezi_type = to.cmt_type;
                t.cto_time = DateTime.Now;
                t.cto_YuanGao_Uid = accuseId;
                t.cto_YuanGao_Uname = to.accuse_uname ?? "";
                DataAccess.Cmt_TipOff_Insert(t);
            }
            else
            {
                tip.cto_YuanGao_Uname = to.accuse_uname;
                tip.cto_BeiGao_Uid = to.accused_uid.ToInt();
                tip.cto_BeiGao_Uname = to.accused_uname;
                tip.cto_des = to.to_des;
                tip.cto_status = 1;
                //tip.cto_tiezi_id = to.cmt_id;
                //tip.cto_tiezi_type = to.cmt_type;
                tip.cto_time = DateTime.Now;
                //tip.cto_YuanGao_Uid = accuseId;
                tip.cto_YuanGao_Uname = to.accuse_uname ?? "";
                DataAccess.Cmt_TipOff_Update(tip);
            }
            return new Resp_Binary() { status = true, remark = "举报成功" };
        }

        public static Resp_Binary Cmt_Shield_Handle(this Req_Cmt_Shield shield)
        {
            var accuseId = shield.u_id.ToInt();

            var tip = DataAccess.Cmt_TipOff_Select(shield.cmt_id, shield.cmt_type, accuseId);
            if(tip != null && tip.cto_shield == 0)
            {
                tip.cto_shield = 1;
                DataAccess.Cmt_TipOff_Update(tip);
            }
            else
            {
                var t = new CommentTipOffInfo();
                //t.cto_BeiGao_Uid = to.accused_uid.ToInt();
                t.cto_BeiGao_Uname = "";
                t.cto_des = "";
                //t.cto_status = 0;
                t.cto_tiezi_id = shield.cmt_id;
                t.cto_tiezi_type = shield.cmt_type;
                t.cto_time = DateTime.Parse("1900-01-01");
                t.cto_YuanGao_Uid = accuseId;
                t.cto_YuanGao_Uname = shield.u_name ?? "";
                t.cto_shield = 1;
                DataAccess.Cmt_TipOff_Insert(t);
            }
            return new Resp_Binary() { status = true, remark = "屏蔽成功" };
        }

        public static List<ExhibitAbs> Company_Exhibit_PageSelect(this Req_Oc_Mini mini) =>
            DataAccess.Company_ExhibitAbs_PageSelect(new DatabaseSearchModel().SetWhere($"ee_oc_code = '{mini.oc_code}'")
                                                                           .SetPageIndex(mini.pg_index)
                                                                           .SetOrder(" ee_exhStartTime desc")
                                                                           .SetPageSize(mini.pg_size));

        public static List<ExhibitCompany> Exhibit_Companies_Get(this Req_Exhibit_Dtl e)
        {
            var companies = DataAccess.Exhibit_Companies_Get(new DatabaseSearchModel().SetWhere($" ee_namemd = '{e.e_md}'").SetWhere(" len(ee_company) > 6").SetOrder(" ee_id ").SetPageIndex(e.pg_index).SetPageSize(e.pg_size));
            var model = new DatabaseSearchModel().SetPageSize(e.pg_size).SetOrder(" oc_id ");

            foreach(var c in companies)
            {
                model.SetOrWhere($" oc_code='{c.oc_code}'");
            }

            var list = DataAccess.OrgCompanyList_Page_Select(model);
            foreach(var l in list)
            {
                for(int i = 0; i < companies.Count; i++)
                {
                    if (companies[i].oc_code == l.oc_code)
                    {
                        companies[i].oc_area = l.oc_area;
                        break;
                    }
                }
            }
            return companies;
        }

        public static Trade_Intelli_Tip AnalysisResult2TradeTip(this AnalysesResult ar)
        {
            var tip = new Trade_Intelli_Tip();
            if (ar.exhibitonTagList != null)
                tip.exh_names = ar.exhibitonTagList.Select(pair => pair.Key).ToList();
            if (ar.tradeList != null)
            {
                foreach(var s in ar.tradeList)
                {
                    var segs = s.Key.Split('\t');
                    if (!tip.gb_trades.ContainsKey(segs[0]))
                        tip.gb_trades[segs[0]] = segs[1];
                }
            }
            if(ar.productList != null)
            {
                foreach(var s in ar.productList)
                {
                    var segs = s.Key.Split('\t');
                    if (!tip.pro_trades.ContainsKey(segs[0]))
                        tip.pro_trades[segs[0]] = segs[1];
                }
            }
            if (ar.forwardTradeList != null)
                tip.fwd_names = ar.forwardTradeList.Select(pair => pair.Key).ToList();

            if (tip.exh_names.Count == 0)
            {
                tip.exh_names.Add("暂无数据");
                tip.exh_names.Add("暂无数据");
            }
            if (tip.pro_trades.Count == 0)
            {
                tip.pro_trades.Add("01", "暂无数据");
                tip.pro_trades.Add("01", "暂无数据");
            }
            if (tip.gb_trades.Count == 0)
            {
                tip.gb_trades.Add("01", "暂无数据");
                tip.gb_trades.Add("01", "暂无数据");
            }
            if (tip.fwd_names.Count == 0)
            {
                tip.fwd_names.Add("暂无数据");
                tip.fwd_names.Add("暂无数据");
            }

            return tip;
        }

        public static List<CertificationInfo> Company_CetificateList_Get(this Req_Business_State req,out int count) =>
            DataAccess.Certificatelst_Get(new DatabaseSearchModel().SetWhere($" ci_oc_code='{req.oc_code}'").SetOrder("ci_expiredDate DESC ").SetPageIndex(req.pg_index).SetPageSize(req.pg_size),out count);

        public static List<CertificationInfo> Company_CetificateDtl_Get(int ci_id) =>
            DataAccess.CertificateDtl_Get(ci_id);

        public static List<OrgGS1RegListInfo> Company_RegList_Get(this Req_Business_State req,out int count) =>
           DataAccess.Reglst_Get(new DatabaseSearchModel().SetWhere($" ori_oc_code='{req.oc_code}'").SetOrder("ori_code").SetPageIndex(req.pg_index).SetPageSize(req.pg_size),out count);

        public static List<OrgGS1ItemInfo> Company_InvList_Get(this Req_Business_State req,out int count) =>
           DataAccess.Invlst_Get(new DatabaseSearchModel().SetWhere($" ogs_oc_code='{req.oc_code}'").SetOrder("ogs_ori_code").SetPageIndex(req.pg_index).SetPageSize(req.pg_size),out count);

        public static List<QZEmployInfo> Company_Emoloyes_Get(this Req_Business_State req, out int count)=>
            DataAccess.QZEmploy_SelectPaged(new DatabaseSearchModel().SetWhere($"ep_code='{req.oc_code}'").SetOrder("ep_Date desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);
        public static List<OrgCompanySiteInfo> OrgCompanySite_SelectPaged(this Req_Business_State req, out int count)=>
            DataAccess.OrgCompanySite_SelectPaged(new DatabaseSearchModel().SetWhere($"ocs_oc_code='{req.oc_code}'").SetOrder("ocs_id desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);

        public static List<ZhiXingInfo> ZhiXing_SelectPaged(this Req_Business_State req, out int count) =>
            DataAccess.ZhiXing_SelectPaged(new DatabaseSearchModel().SetWhere($"oc_code='{req.oc_code}'").SetOrder("zx_caseCreateTime desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);
      

        public static List<ExhibitionEnterpriseInfo> ExhibitionEnterprise_SelectPaged(this Req_Business_State req, out int count)=>
             DataAccess.ExhibitionEnterprise_SelectPaged(new DatabaseSearchModel().SetWhere($"ee_oc_code='{req.oc_code}'").SetOrder("ee_exhCreateTime desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);

        public static List<ClaimCompanyInfo> ClaimCompany_SelectPaged(this Req_myClaim req, out int count) =>
            DataAccess.ClaimCompany_SelectPaged(new DatabaseSearchModel().SetWhere($" cc_u_uid='{req.u_id}'").SetWhere("cc_isvalid=1").SetOrder("cc_createTime desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);

        public static List<VipUserOrderInfo> VipUserOrder_SelectPaged(this Req_myClaim req, out int count) =>
            DataAccess.VipUserOrder_SelectPaged(new DatabaseSearchModel().SetWhere($"mo_userid={req.u_id.ToInt()}").SetOrder("mo_state desc,mo_createTime desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);

        public static List<ExcelCompanyOrderInfo> ExcelCompanyOrder_SelectPaged(this Req_myClaim req, out int count) =>
            DataAccess.ExcelCompanyOrder_SelectPaged(new DatabaseSearchModel().SetWhere($"eco_userid={req.u_id.ToInt()}").SetOrder("eco_state desc,eco_createTime desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size), out count);


    }
}