using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Configuration;
using QZ.Foundation.Monad;
using QZ.Foundation.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.Client;
using QZ.Service.Getui;
using System.Collections;
using Aop.Api.Response;
using System.Threading;
using QZ.Instrument.Common;
using System.Net;

namespace QZ.Service.Enterprise
{
    public static class Req_Ext
    {
        /// <summary>
        /// Query and get a list of company info
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Maybe<Resp_Company_List> Company_List_Query(this Company company) =>
            company.ToMaybe().DoWhen(_ => !VisitorStatisticHandler.AuthorizeIp(),
                                     q => q.Pg_Index(1).Pg_Size(10).Input_Cutoff())
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                   q => q.Input(Regex.Replace(q.oc_name, @"[\\%#.]", "", RegexOptions.Compiled)))



                            .Do(q =>
                                {
                                    var i = DataAccess.SearchHistory_Insert(q, q.u_id.ToInt() > 0);
                                })        // Insert Search history table      /* Using Do Operation if it can do */



                           .Where(q => q.pg_index < ConfigurationManager.AppSettings["query_pg_limit"].ToInt())
                           .ShiftWhenOrElse(q => q.q_type == q_type.q_general,
                                        q => ElasticsearchClient.General_Query(q),
                                        q => ElasticsearchClient.Advanced_Query(q))

                           //.Select(c => c.Documents.Select(d => d.To_Resp_Oc_Abs(company)).ToMaybe())

                           .Select(c => new Resp_Company_List() { oc_list = c.Documents.To_Resp_Oc_Abs(company), count = c.Total }.ToMaybe());

        public static Resp_Company_List PrevGen_Company_Search(Company c)
        {
            var resp = c.q_type == q_type.q_general ? ElasticsearchClient.General_Query(c) : ElasticsearchClient.Advanced_Query(c);
            return new Resp_Company_List()
            {
                oc_list = resp.Documents.To_Resp_Oc_Abs(c),
                count = resp.Total
            };
        }

        public static Maybe<Resp_Company_List> Company_List_Recommend(this Company company) =>
            company.ToMaybe().Select(q => ElasticsearchClient.Recommend_Query(q).ToMaybe())
                             .Select(c => ServiceHandler.To_Company_List(c).ToMaybe());

        public static Maybe<Resp_Brands> Brand_Query(this Req_Info_Query brand) =>
            brand.ToMaybe().DoWhen(b => !string.IsNullOrEmpty(b.query_str),
                                   b => b.query_str = (Regex.Replace(b.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                           .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(1), b.u_id.ToInt() > 0))
                           .Where(b => b.pg_index < 50)

                           .ShiftWhenOrElse(b => (string.IsNullOrEmpty(b.cat_s) || b.cat_s == "0") && b.status.ToInt() == 0,
                                            b => ElasticsearchClient.General_Brand_Query(b),
                                            b => ElasticsearchClient.Brand_Query(b))

                           .Select(sp => ServiceHandler.Brand_Query_Handle(sp, brand.pg_size).ToMaybe());

        public static Maybe<ES_Outcome<ES_Brand>> Brand_NewQuery(this Req_Info_Query brand)
        {     
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(brand.cat_s)) dict.Add("type", brand.cat_s);
            if (!string.IsNullOrEmpty(brand.status))
            {
                if (brand.status != "其他")
                    dict.Add("status", brand.status);
                else
                    dict.Add("status", "");
            }
            if (brand.year != 0) dict.Add("date", brand.year.ToString());
            var brands= brand.ToMaybe().DoWhen(b => !string.IsNullOrEmpty(b.query_str),
                                   b => b.query_str = (Regex.Replace(b.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                            .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(1), b.u_id.ToInt() > 0))
                            .Where(b => b.pg_index < 50).ShiftWhenOrElse(b => dict.Count == 0,
                                                                         b => (b.pg_index < 2),
                                                                         b => ES_Induce.Induce(ES_Client.Brand_GSearch(b.query_str, b.pg_size)),
                                                                         b => ES_Induce.Induce(ES_Client.Brand_GSearch(b.query_str, b.pg_index, b.pg_size)),
                                                                         b => ES_Induce.Induce(ES_Client.Brand_FSearch(b.query_str, b.pg_size, dict)),
                                                                         b => ES_Induce.Induce(ES_Client.Brand_FSearch(b.query_str, b.pg_size, dict, b.pg_index)));

            brands.Value.docs.ToList().ForEach(t => t.doc.ob_img = ConfigurationManager.AppSettings["brand_domain"] + t.doc.ob_img);
            return brands;

        }

        public static Maybe<Resp_Patents> Patent_Query(this Req_Info_Query patent) =>
            patent.ToMaybe().DoWhen(p => !string.IsNullOrEmpty(p.query_str),
                                    p => p.query_str = (Regex.Replace(p.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                            .Do(p => DataAccess.SearchHistoryExt_Insert(p.Type_Set(2), p.u_id.ToInt() > 0))
                            .Where(b => b.pg_index < 50)

                           .ShiftWhenOrElse(p => string.IsNullOrEmpty(p.p_type) && p.year == 0,
                                   p => ElasticsearchClient.General_Patent_Query(p),
                                   p => ElasticsearchClient.Patent_Query(p))
                           .Select(c => ServiceHandler.Patent_Query_Handle(c, patent.pg_size).ToMaybe());

        public static Maybe<ES_Outcome<ES_Patent>> Patent_NewQuery(this Req_Info_Query patent)
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(patent.area)) dict.Add("area", patent.area);
            if (!string.IsNullOrEmpty(patent.p_type)) dict.Add("type", patent.p_type);
            if (!string.IsNullOrEmpty(patent.status))
            {
                if (patent.status != "其他")
                    dict.Add("status", patent.status);
                else
                    dict.Add("status", "");
            }
                
            if (patent.year != 0) dict.Add("date", patent.year.ToString());


            return patent.ToMaybe().DoWhen(p => !string.IsNullOrEmpty(p.query_str),
                        p => p.query_str = (Regex.Replace(p.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                    .Do(p => DataAccess.SearchHistoryExt_Insert(p.Type_Set(2), p.u_id.ToInt() > 0))
                    .Where(b => b.pg_index < 50)
                    .ShiftWhenOrElse(p => dict.Count == 0,
                                     p => (p.pg_index < 2),
                                     p => ES_Induce.Induce(ES_Client.Patent_GSearch(p.query_str, p.pg_size)),
                                     p => ES_Induce.Induce(ES_Client.Patent_GSearch(p.query_str, p.pg_index, p.pg_size)),
                                     p => ES_Induce.Induce(ES_Client.Patent_FSearch(p.query_str, p.pg_size, dict)),
                                     p => ES_Induce.Induce(ES_Client.Patent_FSearch(p.query_str, p.pg_size, dict, p.pg_index)));
        }

        public static Maybe<Resp_Patents> Patent_Related_Query(this Req_Info_Query patent) =>
                patent.ToMaybe().DoWhen(p => !string.IsNullOrEmpty(p.query_str),
                                        p => p.query_str = (Regex.Replace(p.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                                .Do(p => DataAccess.SearchHistoryExt_Insert(p.Type_Set(2), p.u_id.ToInt() > 0))
                                .Where(b => b.pg_index < 50)

                               .Select(b => ElasticsearchClient.Patent_Related_Query(b).ToMaybe())
                               .Select(c => ServiceHandler.Patent_Query_Handle(c).ToMaybe());

        public static Maybe<Resp_Patents> Patent_Universal_Query(this Req_Info_Query patent) =>
            patent.ToMaybe().Select(p => ElasticsearchClient.Patent_Intelli_Query(p).ToMaybe()).Select(c => ServiceHandler.Patent_Universal_Query_Handle(c).ToMaybe());

        public static Maybe<ES_Outcome<ES_Judge>> Judge_NewQuery(this Req_Info_Query judge)
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(judge.status))
            {
                if (judge.status != "其他")
                    dict.Add("status", judge.status);
                else
                    dict.Add("status", "");
            }
            if (judge.year != 0) dict.Add("date", judge.year.ToString());
            return judge.ToMaybe().DoWhen(j => !string.IsNullOrEmpty(j.query_str),
                                   j => j.query_str = (Regex.Replace(j.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                           .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(5), b.u_id.ToInt() > 0))
                           .Where(j => j.pg_index < 50)
                           .ShiftWhenOrElse(j => dict.Count == 0,
                                            j => (j.pg_index < 2),
                                            j => ES_Induce.Induce(ES_Client.Judge_GSearch(j.query_str, j.pg_size)),
                                            j => ES_Induce.Induce(ES_Client.Judge_GSearch(j.query_str, j.pg_size, j.pg_index)),
                                            j => ES_Induce.Induce(ES_Client.Judge_FSearch(j.query_str, j.pg_size, dict)),
                                            j => ES_Induce.Induce(ES_Client.Judge_FSearch(j.query_str, j.pg_size, dict, j.pg_index)));

        }

        public static Maybe<Resp_Judges> Judge_Query(this Req_Info_Query judge) =>
                judge.ToMaybe().DoWhen(j => !string.IsNullOrEmpty(j.query_str),
                                       j => j.query_str = (Regex.Replace(j.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                               .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(5), b.u_id.ToInt() > 0))
                               .Where(j => j.pg_index < 50)
                               .Select(j => ElasticsearchClient.Judge_Query(j).ToMaybe())
                               .Select(c => ServiceHandler.Judge_Query_Handle(c).ToMaybe());
        //c.Documents.Select(d => d.To_Judge_Abs()).ToList().ToMaybe())
        //               .Select(list => new Resp_Judges() { judge_list = list, count = 100 }.ToMaybe());

        //public static Patent_Dtl Patent_Dtl_Get(this Req_Query_Dtl p)
        //{
        //    var 
        //}
        public static Judge_Dtl Judge_Dtl_Query(this Req_Query_Dtl judge) => DataAccess.Judge_Dtl_Query(judge.s_id);

        public static Maybe<ES_Outcome<ES_Dishonest>> Dishonest_NewQuery(this Req_Info_Query dishonest)
        {
            var dict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(dishonest.area)) dict.Add("area", dishonest.area);
            if (!string.IsNullOrEmpty(dishonest.status))
            {
                if (dishonest.status != "其他")
                    dict.Add("status", dishonest.status);
                else
                    dict.Add("status", "");
            }
            if (dishonest.year != 0) dict.Add("date", dishonest.year.ToString());
            return dishonest.ToMaybe().DoWhen(d => !string.IsNullOrEmpty(d.query_str),
                                   d => d.query_str = (Regex.Replace(d.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                           .Do(d => DataAccess.SearchHistoryExt_Insert(d.Type_Set(6), d.u_id.ToInt() > 0))
                           .Where(d => d.pg_index < 50)
                           .ShiftWhenOrElse(d => dict.Count == 0,
                                            d => (d.pg_index < 2),
                                            d => ES_Induce.Induce(ES_Client.Dishonest_GSearch(d.query_str, d.pg_size)),
                                            d => ES_Induce.Induce(ES_Client.Dishonest_GSearch(d.query_str, d.pg_size, d.pg_index)),
                                            d => ES_Induce.Induce(ES_Client.Dishonest_FSearch(d.query_str, d.pg_size, dict)),
                                            d => ES_Induce.Induce(ES_Client.Dishonest_FSearch(d.query_str, d.pg_size, dict, d.pg_index)));

        }



        public static Maybe<Resp_Dishonests> Dishonest_Query(this Req_Info_Query dishonest) =>
            dishonest.ToMaybe().DoWhen(d => !string.IsNullOrEmpty(d.query_str),
                                       d => d.query_str = (Regex.Replace(d.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
                               .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(6), b.u_id.ToInt() > 0))
                               .Where(d => d.pg_index < 15)
                               .ShiftWhenOrElse(d => string.IsNullOrEmpty(d.area),
                                                d => ElasticsearchClient.General_Dishonest_Query(d),
                                                d => ElasticsearchClient.Dishonest_Query(d))
                               .Select(c => ServiceHandler.Dishonest_Query_Handle(c, dishonest.pg_size).ToMaybe());

        public static Resp_Patent_Dtls Patents_Get(this Req_Oc_Mini c)
        {
            if (string.IsNullOrWhiteSpace(c.oc_code))
                return Resp_Patent_Dtls.Default;
            var list = DataAccess.OrgCompanyPatent_Page_Select(new DatabaseSearchModel().SetPageIndex(c.pg_index).SetTable("OrgCompanyPatent")
                .SetPageSize(c.pg_size).SetWhere($"oc_code='{c.oc_code}'").SetOrder(" Patent_LastUpdate desc "));
            return list == null ? Resp_Patent_Dtls.Default : new Resp_Patent_Dtls() { patent_list = list.Select(l => l.To_Patent_Dtl()).ToList() };
        }
        public static Resp_Copyrights Copyrights_Get(this Req_Oc_Mini c)
        {
            var sw_tuple = DataAccess.Software_Get(new DatabaseSearchModel().SetPageIndex(c.pg_index).SetTable("SoftwareCopyright").SetPageSize(c.pg_size)
                .SetWhere($"sc_oc_code='{c.oc_code}'").SetOrder(" sc_successDate desc "));
            var p_tuple = DataAccess.Product_Get(new DatabaseSearchModel().SetPageIndex(c.pg_index).SetTable("ProductCopyright").SetPageSize(c.pg_size)
                .SetWhere($"pc_oc_code='{c.oc_code}'").SetOrder(" pc_firstPublicationDate desc "));

            var crs = new Resp_Copyrights();
            if (sw_tuple == null)
            {
                crs.sw_list = new List<Software_Abs>();
            }
            else
            {
                crs.sw_list = sw_tuple.Item1;
                crs.sw_count = sw_tuple.Item2;
            }
            if (p_tuple == null)
                crs.p_list = new List<Product_Abs>();
            else
            {
                crs.p_list = p_tuple.Item1;
                crs.p_count = p_tuple.Item2;
            }
            return crs;
        }

        public static Resp_Judges Judges_Get(this Req_Oc_Mini c)
        {
            var judges = ES_Induce.Induce(ES_Client.Judge_GSearch(c.oc_code, c.pg_index, c.pg_size));
            //var judge = new Req_Info_Query() { oc_code = c.oc_code, q_sort = 2, pg_size = c.pg_size,pg_index=c.pg_index };
            //var search_resp = ElasticsearchClient.JudgeByCode_Query(judge);
            var docs = judges.docs;
            var list = docs.Select(d => d.doc.To_Judge_Abs()).ToList();
            return new Resp_Judges() { judge_list = list, count = judges.total };
            //judge.ToMaybe().DoWhen(j => !string.IsNullOrEmpty(j.query_str),
            //                       j => j.query_str = (Regex.Replace(j.query_str, @"[\\%#.]", "", RegexOptions.Compiled)))
            //               .Do(b => DataAccess.SearchHistoryExt_Insert(b.Type_Set(5), b.u_id.ToInt() > 0))
            //               .Where(j => j.pg_index < 15)
            //               .Select(j => ElasticsearchClient.Judge_Query(j).ToMaybe())
            //               .Select(c => ServiceHandler.Judge_Query_Handle(c).ToMaybe());
            //var list = DataAccess.OrgCompanyJudge_Page_Select(new DatabaseSearchModel().SetPageIndex(c.pg_index).SetPageSize(c.pg_size).SetTable("JudgementDocCombine")
            //    .SetWhere($"oc_code='{c.oc_code}'").SetOrder(" jd_docDate desc "));
        }
        public static Resp_Dishonests Dishonest_Get(this Req_Oc_Mini c)
        {
            var list = DataAccess.OrgCompanyDishonest_Page_Select(new DatabaseSearchModel().SetPageIndex(c.pg_index).SetTable("Shixin")
                .SetPageSize(c.pg_size).SetWhere($"oc_code='{c.oc_code}'").SetWhere("isHidden=0").SetOrder(" sx_publishDate desc"));
            return list == null ? Resp_Dishonests.Default : new Resp_Dishonests() { dishonest_list = list.Select(l => l.To_Dishonest_Abs()).ToList() };
        }
        public static Resp_Brands Brand_Page_Select(this Req_Oc_Mini company)
        {
            //var brand= ES_Induce.Induce(ES_Client.Brand_SearchByCode(company.oc_code, company.pg_size, company.pg_index));
            var list = DataAccess.OrgCompanyBrand_Page_Select(new DatabaseSearchModel().SetPageIndex(company.pg_index).SetPageSize(company.pg_size).SetTable("OrgCompanyBrand")
                .SetWhere($" ob_oc_code = '{company.oc_code}'").SetOrder(" ob_applicationDate desc "));
            return list == null ? Resp_Brands.Default : new Resp_Brands() { brand_list = list.Select(l => l.To_Brand_Abs()).ToList() };
            //return brand;
        }
        public static Resp_Brand_Dtls Brands_Get(this Req_Oc_Mini company)
        {
            var list = DataAccess.OrgCompanyBrand_Select(company.oc_code);
            return list == null ? Resp_Brand_Dtls.Default : new Resp_Brand_Dtls() { brand_list = list.Select(l => l.To_Brand_Dtl()).ToList() };
        }
        public static Resp_Binary Company_ScoreMark(this Req_Oc_Score score) =>
            DataAccess.Company_ScoreMark(score) > 0
            ? new Resp_Binary() { status = true, remark = "评分成功" }
            : new Resp_Binary() { status = false, remark = "评分失败" };

        public static Maybe<Resp_Company_Detail> Company_Detail_Query(this Company company)
        {


            var u_id = company.u_id.ToInt();
            var c_Mb = company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))     // where clause make sure this company doesn't exist in company black list
                .DoWhen(c => c.quick_flag,                              // if user jump to company detail page directly(quickly), log the search operation.
                        c => DataAccess.SearchHistory_Insert(c, u_id > 0))


                .Do(c =>
                {
                    var e = DataAccess.CompanyEvaluate_Select(c.oc_code);       // ready to update visit count of this company
                    if (e == null)                                               // company evaluate info has not existed, create a new record
                    {
                        e = new CompanyEvaluateInfo().Set_Oc_Area(c.oc_area).Set_Oc_Code(c.oc_code).Set_Oc_Name(c.oc_name);
                    }
                    e.ce_visitNum++;
                    DataAccess.CompanyEvaluate_Update(e);
                });

            //todo: update view info of company change trace  
            if (u_id > 0 && DataAccess.Company_Favorite_Exist(u_id, company.oc_code))
            {
                if (DataAccess.FavoriteViewTrace_Time_Status_Update(u_id, company.oc_code, false) == -1)   // -1 means no record found in database, so it needs to insert a record into record
                {
                    var info = new FavoriteViewTraceInfo() { fvt_oc_code = company.oc_code, fvt_u_uid = u_id, fvt_status = true, fvt_viewtime = DateTime.Now };
                    DataAccess.FavoriteViewTrace_Insert(info);
                }
            }

            // get main company detail infos
            var dtl_lst_Mb = c_Mb.Select(c => DataAccess.OrgCompanyDetails_Select(c.oc_code).ToMaybe());        // main body and orgnization info
            var mgr_Mb = c_Mb.Select(c => DataAccess.Company_Member_Select(c.oc_code, c.oc_area).ToMaybe()); // member info
            var detail_Mb = dtl_lst_Mb.Select(dl => dl.To_Company_Detail().ToMaybe());

            #region oc_tel and trade
            if (detail_Mb.HasValue && company.oc_area.Length > 1)
            {
                if (!(company.oc_area.StartsWith("71") || company.oc_area.StartsWith("81") || company.oc_area.StartsWith("72") || company.oc_area.StartsWith("00")))
                {
                    var dicts = DataAccess.OrgCompany_Tel_Get(new DatabaseSearchModel().SetTable($"OrgCompanyGsxtNb_{company.oc_area.Substring(0, 2)}").SetWhere($"oc_code='{company.oc_code}'").SetOrder(" id "));
                    if (dicts != null && dicts.Item1.Count > 0)
                    {
                        var dict = dicts.Item1;
                        var tel = dict.First().Value;
                        var year = dict.First().Key;
                        foreach (var pair in dict)
                        {
                            if (pair.Key.CompareTo(year) > 0)
                                tel = pair.Value;
                        }
                        var tels = tel.Tel_Get();
                        string district = null;
                        if (tels.Count > 0)
                        {
                            district = DataAccess.District_Code_Get(company.oc_area);
                            detail_Mb.Value.tel_list = tels.Select(t => Tel_Handle2(t, district)).ToList();
                            //detail_Mb.Value.tel_list = tels.Select(t => t.Split('-').FirstOrDefault()?.Length < 9 ? district + "-" + t : t).ToList();
                        }
                        else
                            detail_Mb.Value.tel_list = tels;

                    }
                    if (dicts != null && dicts.Item2.Count > 0)
                    {
                        var dict = dicts.Item2;
                        var email = dict.First().Value;
                        var year = dict.First().Key;
                        foreach (var pair in dict)
                        {
                            if (pair.Key.CompareTo(year) > 0)
                                email = pair.Value;
                        }

                        if (email.Email_Get())
                        {
                            detail_Mb.Value.email = email;
                        }
                    }
                }

                //var comMirror = ESClient.SelectByCode(company.oc_code).FirstOrDefault();
                //if (comMirror != null)
                //{
                //    foreach (var code in comMirror.gb_codes)
                //    {
                //        var name = ResponseAdaptor.GBName_Get(comMirror.gb_cat, code);
                //        if (name != "" && !detail_Mb.Value.gb_trades.ContainsValue(name))
                //            detail_Mb.Value.gb_trades.Add(code, name);
                //    }
                //}
            }
            #endregion

            CompanyStatisticsInfo statisinfo = DataAccess.CompanyStatistics_Get(c_Mb.Value.oc_code);
            var dicstatistics = new List<statistic>();

            #region Members
            var member = new statistic { key = "成员信息", count = mgr_Mb.HasValue ? mgr_Mb.Value.Count : 0 };
            dicstatistics.Add(member);

            var stock = new statistic { key = "股东信息", count = statisinfo.IsNotNull() ? statisinfo.gudongxinxi : 0 };
            dicstatistics.Add(stock);

            var cha = new statistic { key = "变更信息", count = statisinfo.IsNotNull() ? statisinfo.biangengxinxi : 0 };
            dicstatistics.Add(cha);

            var trades = c_Mb.Value.Company_Trades_Get();
            var trade = new statistic { key = "所属行业", count = trades.IsNotNull() && trades.gb_trades.IsNotNull() ? trades.gb_trades.Count : 0 };
            dicstatistics.Add(trade);

            var investobj = new statistic { key = "对外投资", count = statisinfo.IsNotNull() ? statisinfo.duiwaitouzi : 0 };
            dicstatistics.Add(investobj);

            var annualobj = new statistic { key = "企业年报", count = statisinfo.IsNotNull() ? statisinfo.nianbao : 0 };
            dicstatistics.Add(annualobj);

            int certcount;
            var certifics = new Req_Business_State { oc_code = c_Mb.Value.oc_code, pg_index = 1, pg_size = 2 }
                                .Company_CetificateList_Get(out certcount);
            var certobj = new statistic { key = "企业证书", count = certcount };
            dicstatistics.Add(certobj);

            var regsobj = new statistic { key = "厂商信息", count = statisinfo.IsNotNull() ? statisinfo.changshangbianmaxinxi : 0 };
            dicstatistics.Add(regsobj);

            var invsobj = new statistic { key = "产品信息", count = statisinfo.IsNotNull() ? statisinfo.shangpintiaomaxinxi : 0 };
            dicstatistics.Add(invsobj);

            var empobj = new statistic { key = "企业招聘", count = statisinfo.IsNotNull() ? statisinfo.zhaopin : 0 };
            dicstatistics.Add(empobj);

            var icplobj = new statistic { key = "备案信息", count = statisinfo.IsNotNull() ? statisinfo.yuminbeian : 0 };
            dicstatistics.Add(icplobj);

            var brandobj = new statistic { key = "商标信息", count = statisinfo.IsNotNull() ? statisinfo.shangbiaoxinxi : 0 };
            dicstatistics.Add(brandobj);

            var patentobj = new statistic { key = "专利信息", count = statisinfo.IsNotNull() ? statisinfo.zhuanlixinxi : 0 };
            dicstatistics.Add(patentobj);

            var softwareobj = new statistic { key = "软件著作权", count = statisinfo.IsNotNull() ? statisinfo.ruanjianzhuzuoquan : 0 };
            dicstatistics.Add(softwareobj);

            var productobj = new statistic { key = "作品著作权", count = statisinfo.IsNotNull() ? statisinfo.zuopinzhuzuoquan : 0 };
            dicstatistics.Add(productobj);

            var judgobj = new statistic { key = "诉讼信息", count = statisinfo.IsNotNull() ? statisinfo.panjuewenshu : 0 };
            dicstatistics.Add(judgobj);

            var dishonestobj = new statistic { key = "失信信息", count = statisinfo.IsNotNull() ? statisinfo.shixinren : 0 };
            dicstatistics.Add(dishonestobj);

            var executeobj = new statistic { key = "被执行人", count = statisinfo.IsNotNull() ? statisinfo.beizhixingren : 0 };
            dicstatistics.Add(executeobj);

            var exhibitionobj = new statistic { key = "参展信息", count = statisinfo.IsNotNull() ? statisinfo.zhuanhuihuikan : 0 };
            dicstatistics.Add(exhibitionobj);

            var fenzhi = new statistic { key = "分支机构", count = statisinfo.IsNotNull() ? statisinfo.fenzhi : 0 };
            dicstatistics.Add(fenzhi);
            #endregion

            return detail_Mb.Do(d => d.oc_member_list = mgr_Mb.HasValue ? mgr_Mb.Value : new List<Company_Member>())
                        .Do(d => d.statistic_count = dicstatistics);
        }


        public static Trade_Intelli_Tip Company_Trades_Get(this Company c)
        {
            var tip = new Trade_Intelli_Tip();
            var comMirror = ESClient.SelectByCode(c.oc_code).FirstOrDefault();
            if (comMirror != null)
            {
                foreach (var code in comMirror.gb_codes)
                {
                    var name = ResponseAdaptor.GBName_Get(comMirror.gb_cat, code);
                    if (name != "" && !tip.gb_trades.ContainsValue(name))
                        tip.gb_trades.Add(code, name);
                }

                tip.fwd_names = comMirror.fwd_names;
                tip.exh_names = comMirror.exh_names;
            }
            return tip;
        }

        private static string Tel_Handle1(string tel, string district)
        {
            var index = tel.IndexOf('-');
            if (index > -1)
            {
                if (index < 5)  // contains district code
                {
                    return tel;
                }
                else            // do not contain district code, maybe contain a branch code
                    return district + "-" + tel;
            }
            else if (tel.Length > 10)
            {
                return tel;     // is cell phone
            }
            else
                return district + "-" + tel;
        }

        private static string Tel_Handle2(string tel, string district)
        {
            var prefix = district + "-";
            if (tel.StartsWith(prefix))
                return tel;
            if (tel.StartsWith(district))
                return prefix + tel.Substring(district.Length);
            if (tel.Length > 9)
                return tel;
            return district + "-" + tel;
        }

        public static Maybe<Resp_Intelli_Tip> Company_Intelli_Tip(this Req_Intelli_Tip tip) =>
            tip.ToMaybe().DoWhen(t => t.prefix_list != null,
                                 t => t.prefix_list.Select(s => s.Length > 64 ? s.Substring(0, 64) : s).ToList())
                         .DoWhen(t => t.input.Length > 64,
                                 t => t.input = t.input.Substring(0, 64))
                         .DoWhen(t => !VisitorStatisticHandler.AuthorizeIp(),
                                 t =>
                                 {
                                     t.prefix_list?.Clear();
                                     if (t.input.Length > 2)
                                         t.input = t.input.Substring(0, 2);
                                 })
                         .Select(t => CompanyNameIndex_Proxy.Prefix_Get(t.prefix_list, t.input, 500)
                                    .Where(x => (!x.Value.Substring(9).StartsWith("71")) && (!x.Value.Substring(9).StartsWith("81")))
                                    .Select(x => new Company_Mini_Info() { oc_area = x.Value.Substring(9), oc_code = x.Value.Substring(0, 9), oc_name = x.Key }).ToMaybe())
                         .Select(e => new Resp_Intelli_Tip() { tip_list = e.ToList() }.ToMaybe());


        public static Maybe<Resp_Invest> Company_Invest(this Company_Mini_Info company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByName(c.oc_name))     // where clause make sure this company doesn't exist in company black list
                             .Select(c => CompanyMap_Proxy.Company_Invest_Get(c.oc_name)
                                            .Select(x => new Company_Mini_Info() { oc_name = x.name, oc_area = x.areaCode, oc_code = x.code }).ToMaybe())
                             .Select(e => new Resp_Invest() { invest_list = e.ToList() }.ToMaybe());

        private static Resp_Oc_Map To_Resp_Oc_Map(this JsonResult j, Req_Oc_Map map) =>
            new Resp_Oc_Map()
            {
                map_html = MapReader.ToJsonWhitFastJsonStatic(map.map_name, j),
                map_node_count = j.nodes,
                map_link_count = j.links,
                map_depth = j.maxDimension
            };


        public static Maybe<Resp_Oc_Map> Company_Map(this Req_Oc_Map map) =>
            map.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByName(c.oc_name))
                         .Do(c => c.oc_name = c.oc_name.Replace('（', '(').Replace('）', ')'))
                         .DoWhen(c => c.map_dimession < 1,
                                 c => c.map_dimession = 3)
                         .DoWhen(c => c.map_dimession > 4,
                                 c => c.map_dimession = 4)
                         .Select(c => CompanyMap_Proxy.Company_Map_Get(c.oc_name, c.map_dimession, 0, c.flag).To_Resp_Oc_Map(c).ToMaybe());


        public static Maybe<Resp_Oc_Sh> Company_Stock_Holder(this Company_Mini_Info company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                             .ShiftWhenOrElse(c => c.oc_area.StartsWith("4403"),
                                              c => Company_Sh_Compensate_0(DataAccess.Company_Sh_Get(c.oc_code, c.oc_area)),
                                              c => Company_Sh_Compensate_1(DataAccess.Company_Sh_Get(c.oc_code, c.oc_area).MoneyRatio_Compensate()))
                             .Select(list => new Resp_Oc_Sh() { sh_list = list }.ToMaybe())
                             .DoWhen(t => t.sh_list.IsNotNull() && t.sh_list.Count > 0, t => t.sh_list.ForEach(b =>
                             {
                                 b.sh_money = Convert.ToDecimal(Util.InvestMoneyHandle(b.sh_money.ToString()));
                             }
                             ));


        /// <summary>
        /// Fill oc_code and oc_area field infos of a given stock holders (companies )
        /// </summary>
        /// <param name="sh_list"></param>
        /// <returns></returns>
        private static List<Company_Sh> Company_Sh_Compensate_0(List<Company_Sh> sh_list)
        {
            var company_list = CompanyNameIndex_Proxy.Company_Mini_Info_Get(sh_list.Select_Where(sh => sh.sh_name.Replace("（", "(").Replace("）", ")"), sh => !sh.sh_type.Trim().StartsWith("自然人")));
            sh_list.ForEach(sh =>
            {
                if (sh.sh_name.Trim().Length >= 5)
                {
                    var d = company_list.FirstOrDefault(c => c.Key == sh.sh_name.Replace("（", "(").Replace("）", ")"));
                    sh.oc_code = d.Value?.Substring(0, 9) ?? string.Empty;
                    sh.oc_area = d.Value?.Substring(9) ?? string.Empty;
                }
            });
            return sh_list.OrderByDescending(l => l.sh_money_ratio).ToList();
        }

        private static List<Company_Sh> Company_Sh_Compensate_1(this List<Company_Sh> sh_list)
        {
            var company_list = CompanyNameIndex_Proxy.Company_Mini_Info_Get(sh_list.Select_Where(sh => sh.sh_name.Replace("（", "(").Replace("）", ")"), sh => sh.sh_name.Trim().Length > 6));

            var list = new List<Company_Sh>();
            foreach (var sh in sh_list)
            {
                if (sh.sh_status != 4)
                {
                    if (sh.sh_name.Trim().Length > 6)
                    {
                        var d = company_list.FirstOrDefault(c => c.Key == sh.sh_name.Replace("（", "(").Replace("）", ")"));
                        sh.oc_code = d.Value?.Substring(0, 9) ?? string.Empty;
                        sh.oc_area = d.Value?.Substring(9) ?? string.Empty;
                    }
                    list.Add(sh);
                }
            }
            return list.OrderByDescending(l => l.sh_money).ToList();
        }

        public static Maybe<List<Oc_Change>> Company_Change(this Req_Oc_Mini company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                             .Where(c => !c.oc_area.StartsWith("71") && !c.oc_area.StartsWith("81"))    // HK, taiwan exclude
                             .ShiftWhenOrElse(c => c.oc_area == "4403",
                                              c => Company_Change_Get_1(c),
                                              c => Company_Change_Get_0(c));


        private static List<Oc_Change> Company_Change_Get_0(Req_Oc_Mini m)
        {
            var changes = new List<Oc_Change>();
            var search = new DatabaseSearchModel<OrgCompanyGsxtBgsxInfo>().SetWhereClause($" oc_code = '{m.oc_code}' ")
                .SetOrderField(c => c.bgrq).Ascend(false).SetPageIndex(m.pg_index).SetPageSize(m.pg_size);
            var list = DataAccess.OrgCompanyGsxtBgsx_Select(search, m.oc_area.Substring(0, 2));
            if (list != null && list.Count > 0)
            {
                string date = "";
                foreach (var info in list)
                {
                    date = (info.bgrq != null) ? info.bgrq.ToString("yyyy-MM-dd") : "";
                    var d = changes.FirstOrDefault(c => c.date.Equals(date));
                    if (d != null)
                    {
                        d.item_list.Add(new Change_Item()
                        {
                            item_name = info.bgsx,
                            item_pre = info.bgq,
                            item_post = info.bgh
                        });
                    }
                    else
                    {
                        changes.Add(new Oc_Change()
                        {
                            date = date,
                            item_list = new List<Change_Item>()
                                    {
                                        new Change_Item()
                                        {
                                            item_name = info.bgsx,
                                            item_pre = info.bgq,
                                            item_post = info.bgh
                                        }
                                    }
                        });
                    }
                }
            }
            return changes;
        }

        /// <summary>
        /// special for company located in shenzhen city
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static List<Oc_Change> Company_Change_Get_1(Req_Oc_Mini m)
        {
            var changes = new List<Oc_Change>();
            var search = new DatabaseSearchModel<OrgCompanyDtl_EvtInfo>().SetPageIndex(m.pg_index).SetPageSize(m.pg_size)
                .SetWhereClause($"oe_oc_code='{m.oc_code}'").Ascend(false).SetOrderField(t => t.oe_id);
            var list = DataAccess.OrgCompanyDtl_Evt_Select(search);
            foreach (var l in list)
            {
                List<OrgCompanyGsxtBgsxInfo> bgsx = SplitOrgCompanyGsxtBgsx(l);

                if ((bgsx != null) && (bgsx.Count > 0))
                {
                    string date = "";
                    foreach (var info in bgsx)
                    {
                        date = (info.bgrq != null) ? info.bgrq.ToString("yyyy-MM-dd") : "";
                        var d = changes.FirstOrDefault(c => c.date.Equals(date));
                        if (d != null)
                        {
                            d.item_list.Add(new Change_Item()
                            {
                                item_name = info.bgsx,
                                item_pre = info.bgq,
                                item_post = info.bgh
                            });
                        }
                        else
                        {
                            changes.Add(new Oc_Change()
                            {
                                date = date,
                                item_list = new List<Change_Item>()
                                    {
                                        new Change_Item()
                                        {
                                            item_name = info.bgsx,
                                            item_pre = info.bgq,
                                            item_post = info.bgh
                                        }
                                    }
                            });
                        }
                    }
                }
            }
            return changes;
        }
        private static List<OrgCompanyGsxtBgsxInfo> SplitOrgCompanyGsxtBgsx(OrgCompanyDtl_EvtInfo info)
        {
            List<OrgCompanyGsxtBgsxInfo> bgsx = new List<OrgCompanyGsxtBgsxInfo>();
            //var reg = new Regex(@"^\s+变更前(.+)[：](.*)变更后(.+)[：](.*)$", RegexOptions.Singleline | RegexOptions.Compiled);

            var regx = new Regex(@"^\s+变更([前后])(.*?)[：:](.*?)$", RegexOptions.Multiline | RegexOptions.Compiled);
            var mt = regx.Matches(info.oe_dtl).Cast<Match>().Select(x => new { name = x.Groups[2].Value, qh = x.Groups[1].Value, value = x.Groups[3].Value });
            var cpy = mt.GroupBy(x => x.name);
            foreach (var x1 in cpy)
            {
                OrgCompanyGsxtBgsxInfo bgsxInfo = new OrgCompanyGsxtBgsxInfo();
                bgsxInfo.bgsx = x1.Key;
                bgsxInfo.bgq = x1.FirstOrDefault(c => c.qh == "前")?.value?.TrimStart();
                bgsxInfo.bgh = x1.FirstOrDefault(c => c.qh == "后")?.value?.TrimStart();
                bgsxInfo.bgrq = Convert.ToDateTime(info.oe_date); // 变更日期
                bgsx.Add(bgsxInfo);
            }



            //string[] sxs = info.oe_dtl.Split(new string[] { "变更前" }, StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 0; i < sxs.Length; i++)
            //{
            //    OrgCompanyGsxtBgsxInfo bgsxInfo = new OrgCompanyGsxtBgsxInfo();
            //    if (!string.IsNullOrEmpty(info.oe_date))
            //    {
            //        bgsxInfo.bgrq = Convert.ToDateTime(info.oe_date); // 变更日期
            //    }
            //    string[] sx = sxs[i].Split(new string[] { "变更后" }, StringSplitOptions.RemoveEmptyEntries);
            //    if (sx.Length == 2)
            //    {
            //        for (int j = 0; j < 2; j++)
            //        {
            //            string[] strs = sx[j].Replace("\t", string.Empty).Split(new string[] { ":", "：" }, StringSplitOptions.RemoveEmptyEntries);

            //            if (j == 0)
            //            {
            //                bgsxInfo.bgsx = strs[0] ?? ""; // 变更类别
            //                bgsxInfo.bgq = strs[1] ?? ""; // 变更前

            //            }
            //            if ((j == 1))
            //            {
            //                bgsxInfo.bgh = strs[1] ?? ""; // 变更后
            //            }
            //        }

            //        bgsx.Add(bgsxInfo);
            //    }

            //}

            return bgsx;
        }

        public static Maybe<List<Company_Icpl>> Company_Icpl(this Req_Oc_Mini company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                             .Select(c =>
                             {
                                 var search = new DatabaseSearchModel().SetWhere($"ocs_oc_code = '{c.oc_code}'").SetPageIndex(c.pg_index).SetPageSize(c.pg_size).SetOrder("ocs_id desc");
                                 return DataAccess.Company_Icpl_Select(search).ToMaybe();
                             });

        public static Maybe<List<Company_Mini_Info>> Company_Branch(this Req_Oc_Mini company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                             .Select(c =>
                             {
                                 var list = CompanyNameIndex_Proxy.Company_Mini_Info_Get(c.oc_name.Replace("（", "(").Replace("）", ")"), Calibrater.Pg_Index_Check(c.pg_index), Calibrater.Pg_Size_Check(c.pg_size));
                                 return list.Select(x => new Company_Mini_Info()
                                 {
                                     oc_name = x.Key,
                                     oc_code = x.Value.Substring(0, 9),
                                     oc_area = x.Value.Substring(9)
                                 }).ToList().ToMaybe();
                             });

        public static Maybe<List<Company_Annual_Abs>> Company_Annual(this Req_Oc_Annual company) =>
            company.ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                             .Select(c => DataAccess.Company_Annual_Abs_Select(c.oc_code, c.oc_area, c.year).ToMaybe());

        public static Maybe<Company_Annual_Dtl> Company_Annual_Detail(this Req_Oc_Annual company) =>
            company.ToMaybe().Select(c => DataAccess.Company_Annual_Dtl_Select(c.oc_code, c.oc_area, c.year).ToMaybe());

        public static Company_Impression Company_Impression(this Req_Oc_Mini company)
        {
            var u_id = company.u_id.ToInt();
            var eval = DataAccess.CompanyEvaluate_Select(company.oc_code);
            var impress = new Company_Impression();
            impress.up_down_flag = DataAccess.Company_User_Up_Down(u_id, company.oc_code);    // whether user does up for this company
            impress.favorite_flag = DataAccess.Company_Favorite_Exist(u_id, company.oc_code);
            if (eval != null)                               // favorite and up and topic about this company
            {
                impress.favorite_count = eval.ce_FavorNum;
                impress.up_count = eval.ce_likeNum;
                impress.topic_count = eval.ce_tucaoNum;
                impress.down_count = eval.ce_notLikeNum;
            }
            return impress;
        }

        public static Resp_Binary Company_New_Topic(this Req_Oc_Comment topic)
        {
            var state = new Comment_State();
            var content = topic.topic_content.To_Sql_Safe();    // handle chars of content
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content.Length > 1000)
                    return new Resp_Binary() { remark = "发帖内容不能超过1000字" };
                Task<File_Upload_Info> task = null;   // declare a task for uploading images

                if (topic.pic_list == null || topic.pic_list.Count < 1)  // no image data
                    state.File_State = File_Upload_State.None;
                else if (topic.pic_list.Count > 10)      // too many images
                    state.File_State = File_Upload_State.Count_Err;
                else        // ready to upload images
                    task = Task.Run(() => Pics_Upload(topic.pic_list, topic.u_id));     // create a new task to do pictures uploading                    

                if (state.File_State != File_Upload_State.Count_Err)    // if image count is large than 10, preventing to post the topic
                {
                    // ready to insert topic data into database
                    var u_id = topic.u_id.ToInt();
                    var i = new CompanyTieziTopicInfo()
                    {
                        ctt_oc_code = topic.oc_code,
                        ctt_oc_name = topic.oc_name,
                        ctt_u_name = topic.u_name,
                        ctt_u_uid = u_id,
                        ctt_content = content,
                        ctt_date = DateTime.Now,
                        ctt_oc_area = topic.oc_area,
                        ctt_status = 1,
                        ctt_tag = topic.topic_tag,
                    };
                    var t_id = DataAccess.Company_Topic_Insert(i);
                    if (t_id > 0)  // insert into database successfully
                    {


                        state.T_R_State = TopicReply_State.Sucess;

                        #region topic user trace
                        var trace = new TopicUsersTraceInfo() { tut_status = true, tut_t_count = -1, tut_t_id = t_id.ToString(), tut_t_type = "0", tut_uid = u_id };
                        DataAccess.TopicUsersTrace_Insert(trace);
                        #endregion


                        #region insert image records into database
                        if (task != null)
                        {
                            var img = new CompanyTieziImageInfo() { cti_tiezi_id = t_id, cti_tiezi_type = (int)Comment_Type.topic, cti_uid = u_id };
                            try
                            {
                                task.Wait();
                                if (task.Result.State == File_Upload_State.Success)
                                    state.Count = DataAccess.CompanyTieziImage_Bulk_Insert(img, task.Result.Uris);
                            }
                            catch (AggregateException e)
                            {
                                //foreach (var ex in e.InnerExceptions)
                                //    Util.Log_Info(nameof(Company_New_Topic), Location.Internal, ex.Message, $"uploading image task exception: {ex.GetType()}\t{ex.Source}");
                            }
                        }
                        #endregion
                    }
                    else
                        state.T_R_State = TopicReply_State.Db_Insert_Err;
                }
            }
            else
                state.T_R_State = TopicReply_State.Content_Empty;
            return state.To_Resp_Binary();
        }

        private static File_Upload_Info Pics_Upload(List<string> pics, string u_id)
        {
            var i = new File_Upload_Info();

            i.Uris = new List<string>();
            foreach (var pic in pics)
            {
                var data = Convert.FromBase64String(pic);
                if (data.Length > 1024 * 1024)          // check size of each image
                    continue;

                var ext = FileHelper.Ext_Get(data);     // check ext format of each image
                if (string.IsNullOrEmpty(ext))
                    continue;


                var info = Upload_Proxy.Image_Upload(u_id, DateTime.Now.ToString("yyyyMMddhhmmss"), ext, data, true);
                if (info != null)
                {
                    i.Uris.Add(info.FullUrl);

                }
            }

            i.State = i.Uris.Any() ? File_Upload_State.Success : File_Upload_State.Upload_Err;
            return i;
        }

        public static Resp_Binary Company_Reply(this Req_Oc_Comment cmt)
        {
            var state = new Comment_State();
            var content = cmt.reply_content.To_Sql_Safe();    // handle chars of content

            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content.Length > 1000)
                    return new Resp_Binary() { remark = "内容不能超过1000字" };
                Task<File_Upload_Info> task = null;   // declare a task for uploading images

                if (cmt.pic_list == null || cmt.pic_list.Count < 1)  // no image data
                    state.File_State = File_Upload_State.None;
                else if (cmt.pic_list.Count > 10)      // too many images
                    state.File_State = File_Upload_State.Count_Err;
                else        // ready to upload images
                    task = Task.Run(() => Pics_Upload(cmt.pic_list, cmt.u_id));     // create a new task to do pictures uploading                    

                if (state.File_State != File_Upload_State.Count_Err)    // if image count is large than 10, preventing to post the topic
                {
                    // ready to insert reply data into database
                    var u_id = cmt.u_id.ToInt();
                    var i = new CompanyTieziReplyInfo()
                    {
                        ctr_teizi = cmt.topic_id,
                        ctr_u_name = cmt.u_name,
                        ctr_u_uid = u_id,
                        ctr_content = content,
                        ctr_date = DateTime.Now,
                        //ctr_status = 1
                    };
                    var t_id = DataAccess.Company_Reply_Insert(i);
                    if (t_id > 0)  // insert into database successfully
                    {
                        state.T_R_State = TopicReply_State.Sucess;

                        #region topic user trace
                        DataAccess.TopicUserTrace_Refresh(cmt.topic_id.ToString(), "0", u_id);
                        #endregion

                        #region insert image records into database
                        if (task != null)
                        {
                            var img = new CompanyTieziImageInfo() { cti_tiezi_id = t_id, cti_tiezi_type = (int)Comment_Type.reply, cti_uid = u_id };
                            try
                            {
                                task.Wait();
                                if (task.Result.State == File_Upload_State.Success)
                                    state.Count = DataAccess.CompanyTieziImage_Bulk_Insert(img, task.Result.Uris);
                            }
                            catch (AggregateException e)
                            {
                                foreach (var ex in e.InnerExceptions)
                                    Util.Log_Info(nameof(Company_Reply), Location.Internal, ex.Message, $"uploading image task exception: {ex.GetType()}\t{ex.Source}");
                            }
                        }
                        #endregion

                        //#region push message to app
                        //List<string> clientidlst = DataAccess.TopicUserTrace_GetClientId(cmt.topic_id, 0);
                        //string topic_content = DataAccess.CompanyTopic_Content_Getbyid(cmt.topic_id);
                        //if (clientidlst.Count > 0 && !string.IsNullOrEmpty(topic_content))
                        //{
                        //    MessageInfo mess = new MessageInfo();
                        //    string title = "你在公司的评论有新消息";
                        //    mess.type = MessageType.CompanyReply;

                        //    mess.content = topic_content;
                        //    mess.title = title;
                        //    mess.topicid = cmt.topic_id.ToString();
                        //    mess.u_id = cmt.u_id;
                        //    mess.u_name = cmt.u_name;
                        //    mess.replycontent = cmt.reply_content;

                        //    string content1 = mess.ToJson();
                        //    string begintime = DateTime.Now.ToString();
                        //    string endtime = DateTime.Now.AddMinutes(60).ToString();
                        //    PushService.PushMessageToList(title, topic_content, "", "", "2", content1, false, false, true, begintime, endtime, clientidlst);
                        //}
                        //#endregion
                    }
                    else
                        state.T_R_State = TopicReply_State.Db_Insert_Err;
                }
            }
            else
                state.T_R_State = TopicReply_State.Content_Empty;
            return state.To_Resp_Binary();
        }

        public static Maybe<Resp_Topics_Abs> Company_Fresh_Topic(this Req_Oc_Mini company)
        {
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(company.pg_index).SetPageSize(company.pg_size).SetOrder(" ctt_date desc ").SetWhere("ctt_status= 1");
            return company.ToMaybe().Select(c => DataAccess.Company_Topics_Get(search, out count).ToMaybe().Do(list => list.ForEach(l =>
            {
                l.topic_gentle_time = Util.Get_Gentle_Time(l.topic_date);
                l.u_face = Util.UserFace_Get(l.u_id.ToInt());
            }))
            .Select<Resp_Topics_Abs>(list => new Resp_Topics_Abs() { topic_list = list, count = count }));
        }

        public static Resp_Topics_Dtl Company_Topic_Query(this Req_Oc_Mini company)
        {
            var u_id = company.u_id.ToInt();
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(company.pg_index).SetPageSize(company.pg_size).SetOrder(" ctt_date desc ").SetWhere("ctt_status= 1").SetWhere($"ctt_oc_code='{company.oc_code}'");
            //List<Topic_Dtl> topics = null;
            var topics = DataAccess.Company_Topics_Dtl_Get(search, out count);
            if (topics.Count > 0)
            {
                var result = Parallel.ForEach(topics, t => Oc_Topic_Compensate(t, u_id));
                //topics.Sort(new Topic_Dtl_Comparer());
                topics = topics.Where(t => !t.t_shield).ToList();

            }

            return new Resp_Topics_Dtl() { topic_list = topics, count = count };
        }


        private static void Oc_Topic_Compensate(Topic_Dtl t, int u_id)
        {
            var tip = DataAccess.Cmt_TipOff_Select(t.topic_id, 1, u_id);
            if (tip != null && tip.cto_shield == 1)
            {
                t.t_shield = true;
                return;
            }

            var search = new DatabaseSearchModel();
            t.reply_count = DataAccess.Company_Topic_ReplyCount_Get(t.topic_id);
            //var replies = DataAccess.Replies_Dtl_Select(search.SetPageSize(50).SetOrder(" ctr_date ").SetWhere($"ctr_teizi={t.topic_id}").SetWhere("ctr_status=1"));
            //t.reply_list = replies != null ? Replies_Compensate(replies) : new List<Reply_Dtl>();

            t.u_face = Util.UserFace_Get(t.u_face, t.u_id);
            t.topic_gentle_time = Util.Get_Gentle_Time(t.topic_date);
            t.topic_content = t.topic_content.De_Sql_Safe();
            var count_tuple = DataAccess.Company_Topic_UpDown_Count_Get(t.topic_id);
            t.down_count = count_tuple.Item2;
            t.up_count = count_tuple.Item1;
            if (u_id > 0)
            {
                var tuple = DataAccess.Company_Topic_UpDown_Flag_Get(t.topic_id, u_id);
                if (tuple.Item1 == 1)
                    t.up_down_flag = Topic_Up_Down.Up;
                else if (tuple.Item2 == 1)
                    t.up_down_flag = Topic_Up_Down.Down;
                else
                    t.up_down_flag = Topic_Up_Down.None;
            }
            else
                t.up_down_flag = Topic_Up_Down.None;
            t.pic_list = DataAccess.Oc_Comment_Imgs_Select(t.topic_id, 0);


        }
        public static Resp_Binary Company_Topic_UpDown_Vote(this Req_Topic_Vote vote)
        {
            var info = new CompanyLikeOrNotLogInfo() { cll_date = DateTime.Now, cll_teizi = vote.topic_id, cll_u_name = vote.u_name, cll_u_uid = vote.u_id.ToInt(), cll_valid = 1 };
            var tuple = DataAccess.Company_Topic_UpDown_Flag_Get(vote.topic_id, vote.u_id.ToInt());
            if (tuple.Item1 == 1)   // already up
            {
                if (vote.op_type == 2)
                {
                    // up -> down
                    if (DataAccess.Company_Topic_Up2Down(info) > 0)
                        return new Resp_Binary() { remark = "点踩成功", status = true };
                    return new Resp_Binary() { remark = "点踩失败", status = false };
                }
            }
            else if (tuple.Item2 == 1)  // already down
            {
                if (vote.op_type == 1)
                {
                    // down -> up
                    if (DataAccess.Company_Topic_Down2Up(info) > 0)
                    {
                        //int count = 0;
                        //var search = new DatabaseSearchModel().SetPageIndex(1).SetPageSize(1).SetOrder(" ctt_date desc ").SetWhere("ctt_status= 1").SetWhere($"ctt_id={vote.topic_id}");
                        //List<Topic_Dtl> dtl = DataAccess.Company_Topics_Dtl_Get(search,out count);

                        //if (dtl.IsNotNull() && dtl.Count> 0&&dtl[0].u_id.ToInt()>0)
                        //{
                        //    string clientId = DataAccess.User_ClientID_Getbyu_id(dtl[0].u_id.ToInt());
                        //    if (!string.IsNullOrWhiteSpace(clientId))
                        //    {
                        //        MessageInfo message = new MessageInfo();
                        //        message.type = MessageType.CompanyUp;
                        //        message.u_id = vote.u_id;
                        //        message.u_name = vote.u_name;
                        //        message.title = "你发布的公司评论有了新的点赞";
                        //        message.content = dtl[0].topic_content;
                        //        string begintime = DateTime.Now.ToString();
                        //        string endtime = DateTime.Now.AddMinutes(60).ToString();
                        //        PushService.PushMessageToSingle("2", message.title, message.ToJson(), begintime, endtime, clientId);
                        //    }

                        //}
                        return new Resp_Binary() { remark = "点赞成功", status = true };
                    }
                    return new Resp_Binary() { remark = "点赞失败", status = false };
                }
            }
            else                        // had not voted
            {
                info.cll_type = vote.op_type;
                var action = vote.op_type == 1 ? "点赞" : "点踩";
                if (DataAccess.Company_Topic_UpDown_Vote(info) > 0)
                    return new Resp_Binary() { remark = action + "成功", status = true };
                return new Resp_Binary() { remark = action + "失败", status = false };
            }
            return new Resp_Binary() { remark = "操作成功", status = true };
        }

        public static Resp_Binary Company_UpDown_Vote(this Req_Topic_Vote vote)
        {
            var u_id = vote.u_id.ToInt();
            var info = new LikeOrNotLogInfo() { lon_date = DateTime.Now, lon_oc_code = vote.oc_code, lon_oc_area = vote.oc_area, lon_oc_name = vote.oc_name, lon_u_name = vote.u_name, lon_u_uid = u_id, lon_valid = 1 };
            var updown = DataAccess.Company_User_Up_Down(vote.u_id.ToInt(), vote.oc_code);
            if (updown == Topic_Up_Down.Up)   // already up
            {
                if (vote.op_type == 2)
                {
                    // up -> down
                    if (DataAccess.Company_Up2Down(info) > 0)
                        return new Resp_Binary() { remark = "点踩成功", status = true };
                    return new Resp_Binary() { remark = "点踩失败", status = false };
                }
            }
            else if (updown == Topic_Up_Down.Down)  // already down
            {
                if (vote.op_type == 1)
                {
                    // down -> up
                    if (DataAccess.Company_Down2Up(info) > 0)
                        return new Resp_Binary() { remark = "点赞成功", status = true };
                    return new Resp_Binary() { remark = "点赞失败", status = false };
                }
            }
            else                        // had not voted
            {
                info.lon_type = vote.op_type;
                var action = vote.op_type == 1 ? "点赞" : "点踩";
                if (DataAccess.Company_UpDown_Vote(info) > 0)
                    return new Resp_Binary() { remark = action + "成功", status = true };
                return new Resp_Binary() { remark = action + "失败", status = false };
            }
            return new Resp_Binary() { remark = "操作成功", status = true };
        }

        public static Maybe<Resp_Binary> Company_Correct(this Req_Oc_Correct correct) =>
            correct.ToMaybe().Where(c => !string.IsNullOrWhiteSpace(c.crect_content)).Select(c => new CompanyInfoCorrectInfo()
            {
                cic_content = c.crect_content.To_Sql_Safe(),
                cic_date = DateTime.Now,
                cic_oc_code = c.oc_code,
                cic_oc_name = c.oc_name,
                cic_phone = c.u_tel,
                cic_type = (int)c.crect_type,
                cic_u_name = c.u_name,
                cic_u_uid = c.u_id.ToInt()
            }.ToMaybe())
            .ShiftWhenOrElse(info => DataAccess.Company_Correct(info) > 0,
                                _ => new Resp_Binary() { status = true, remark = "提交成功" },
                                _ => new Resp_Binary() { remark = "提交失败" });

        public static Maybe<List<Reply_Dtl>> Company_Topic_Detail(this Req_Topic_Dtl topic)
        {
            var search = new DatabaseSearchModel();
            var replies = DataAccess.Replies_Dtl_Select(search.SetPageSize(topic.pg_size).SetPageIndex(topic.pg_index).SetOrder(" ctr_date desc ").SetWhere($"ctr_teizi={topic.topic_id}").SetWhere("ctr_status=1"));
            if (replies != null)
                Replies_Compensate(replies);
            return replies;
        }
        private static List<Reply_Dtl> Replies_Compensate(List<Reply_Dtl> replies)
        {
            var search = new DatabaseSearchModel();
            foreach (var r in replies)
            {
                r.u_face = Util.UserFace_Get(r.u_id.ToInt());
                r.reply_content = r.reply_content.De_Sql_Safe();
                r.reply_gentle_time = Util.Get_Gentle_Time(r.reply_date);
                r.pic_list = DataAccess.Oc_Comment_Imgs_Select(r.reply_id, 1);
            }
            return replies;
        }

        public static List<Favorite_Log> Favorites_GetByUidAndGuid(this Req_Favorite_Group favorite)
        {
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(favorite.pg_index).SetPageSize(favorite.pg_size)
                .SetWhere($"fl_u_uid={favorite.u_id.ToInt()}").SetWhere($"fl_g_gid={favorite.g_id.ToInt()}").SetWhere("fl_valid=1")
                .SetOrder(" fl_id desc ");
            var list = DataAccess.Favorites_Get(search, out count);
            list.ForEach(t => t.favorite_note = DataAccess.FavoriteNote_Top(t.favorite_id));
            DataAccess.FavoriteGroup_SetCount(favorite.g_id.ToInt(), count);
            if (list == null)
                list = new List<Favorite_Log>();
            return list;
        }

        public static List<Favorite_Log> UnFavorites_Get(this Req_Favorite_Group favorite)
        {
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(favorite.pg_index).SetPageSize(favorite.pg_size)
                .SetWhere($"fl_u_uid={favorite.u_id.ToInt()}").SetWhere($" isnull(fl_g_gid,'0')='0'").SetWhere("fl_valid=1")
                .SetOrder(" fl_id desc ");
            var list = DataAccess.Favorites_Get(search, out count);
            list.ForEach(t => t.favorite_note = DataAccess.FavoriteNote_Top(t.favorite_id));
            if (list == null)
                list = new List<Favorite_Log>();
            return list;
        }

        public static Resp_Binary Favorite_Into_Group(this Req_FavoriteIntoGroup req) =>
            req.ToMaybe().FuncWhen(t => t.g_id.ToInt() > 0,
                a => a.ToMaybe().Select<int>(g => DataAccess.Favorite_Into_Group(String.Join(",", g.fl_ids), g.g_id.ToInt(), g.fl_ids.Count))
                .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "添加成功", status = true },
                                         _ => new Resp_Binary() { remark = "添加失败", status = false }).Value).Value;

        public static Resp_Binary Favorite_Note_Add(this Req_FavoriteNote req)
        {
            var obj = req.ToMaybe().Where(t => t.fl_id.ToInt() > 0 && !string.IsNullOrWhiteSpace(t.note));
            if (obj.IsNotNull())
                return obj.Select<int>(g => DataAccess.Favorite_Note_Create(g.fl_id.ToInt(), g.note))
                .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "创建成功", status = true },
                                         _ => new Resp_Binary() { remark = "创建失败", status = false }).Value;
            else
                return new Resp_Binary() { remark = "创建失败", status = false };
        }
        public static Resp_Binary Favorite_Note_UP(this Req_FavoriteNote req) =>
           req.ToMaybe().FuncWhen(t => t.n_id.ToLong() > 0 && !string.IsNullOrWhiteSpace(t.note), a => a.ToMaybe().Select<int>(g => DataAccess.Favorite_Note_Update(g.n_id.ToLong(), g.note))
                .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "修改成功", status = true },
                                         _ => new Resp_Binary() { remark = "修改失败", status = false }).Value).Value;
        public static List<Favorite_Note> Favorite_Note_SelectPaged(this Req_FavoriteNote req)
        {
            var search = new DatabaseSearchModel().SetWhere($"fl_id={req.fl_id.ToInt()}").SetOrder("create_date desc").SetPageIndex(req.pg_index).SetPageSize(req.pg_size);
            var obj = req.ToMaybe().Where(t => t.fl_id.ToInt() > 0);
            if (obj.IsNotNull())
            {
                var lst = obj.Select<List<Favorite_Note>>(g => DataAccess.FavoriteNote_SelectPaged(search));
                return lst.Value;
            }
            else
                return new List<Favorite_Note>();
        }

        public static Maybe<Resp_Binary> Favorite_Group_Update(this Req_Favorite_Add group) =>
            group.ToMaybe().Where(g => !string.IsNullOrWhiteSpace(g.g_name) && g.g_id.ToInt() > 0).Select<Favorite_Group>(t => new Favorite_Group()
            {
                g_gid = t.g_id.ToInt(),
                g_name = t.g_name,
                u_uid = t.u_id.ToInt(),
                fl_count = 0
            })
                .Select<int>(g => DataAccess.FavoriteGroup_UpdateName(g))
                .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "修改成功", status = true },
                                 _ => new Resp_Binary() { remark = "修改失败", status = false });

        public static Maybe<Resp_Binary> Favorite_Group_Del(this Req_Favorite_Add group) =>
            group.ToMaybe().Where(t => t.g_id.ToInt() > 0)
            .Select<int>(g => DataAccess.FavoriteGroup_Del(group.g_id.ToInt()))
            .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "删除成功", status = true },
                                         _ => new Resp_Binary() { remark = "删除失败", status = false });

        public static Maybe<Resp_Binary> Favorite_Group_Add(this Req_Favorite_Add group) =>
            group.ToMaybe().Where(req => group.u_id.ToInt() > 0 && !string.IsNullOrWhiteSpace(group.g_name))
                .Select(t => new Favorite_Group()
                {
                    u_uid = t.u_id.ToInt(),
                    g_name = t.g_name,
                    fl_count = 0
                }.ToMaybe())
            .Select<int>(info => DataAccess.FavoriteGroup_Insert(info))
            .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "新建成功", status = true },
                                         _ => new Resp_Binary() { remark = "新建失败", status = false });


        public static Maybe<Resp_Binary> Company_Favorite_Add(this Req_Favorite_Add req_favorite) =>
            req_favorite.ToMaybe().ShiftWhenOrElse(req => req.q_action == 0,
                                        t => t.ToMaybe().FuncWhen(ta => ta.g_id.ToInt() > 0,
                                                        tb => DataAccess.FavoriteGroup_UpdateCount(true, tb.g_id.ToInt())),
                                        s => s.ToMaybe().FuncWhen(sa => sa.u_id.ToInt() > 0,
                                                        sb => DataAccess.FavoriteGroup_Insert(new Favorite_Group()
                                                        {
                                                            u_uid = sb.u_id.ToInt(),
                                                            g_name = sb.g_name,
                                                            fl_count = 1
                                                        })))
                                   .Select<int>(t => DataAccess.Favorite_Into_Group(req_favorite.u_id.ToInt(), req_favorite.oc_code, req_favorite.g_id.ToInt()))
                .ShiftWhenOrElse(i => i > 0,
                                 _ => new Resp_Binary() { remark = "收藏成功", status = true },
                                 _ => new Resp_Binary() { remark = "收藏失败", status = false });



        public static Maybe<Resp_Binary> Company_Favorite_Add(this Req_Oc_Mini company) =>
            company.ToMaybe().Select(c =>
                    new FavoriteLogInfo()
                    {
                        fl_date = DateTime.Now,
                        fl_oc_area = c.oc_area,
                        fl_oc_code = c.oc_code,
                        fl_oc_name = c.oc_name.Replace("<font color=\"red\">", "").Replace("</font>", ""),
                        fl_u_name = c.u_name,
                        fl_u_uid = c.u_id.ToInt(),
                        fl_valid = 1,
                        fl_g_gid = 0
                    }.ToMaybe()
                )
                .Select<int>(info => DataAccess.FavoriteLog_Insert(info))
                .ShiftWhenOrElse(i => i > 0,
                                 _ => new Resp_Binary() { remark = "收藏成功", status = true },
                                 _ => new Resp_Binary() { remark = "收藏失败", status = false });

        public static Maybe<Resp_Binary> Company_Favorite_NewRemove(this Maybe<Req_Oc_Mini> company) =>
            company.DoWhen(c => DataAccess.FavoriteGroup_UpdateCount(false, DataAccess.Company_Favorite_GetByUidAndOccode(c.u_id.ToInt(), c.oc_code)) > 0,
                f => DataAccess.Company_Favorite_GetByUidAndOccode(f.u_id.ToInt(), f.oc_code))
            .ShiftWhenOrElse(c => DataAccess.FavoriteLog_Disable(c.oc_code, c.u_id.ToInt()) > 0,
                                    _ => new Resp_Binary() { remark = "取消收藏成功", status = true },
                                    _ => new Resp_Binary() { remark = "取消收藏失败", status = true });
        public static Maybe<Resp_Binary> Company_Favorite_Remove(this Maybe<Req_Oc_Mini> company) =>
            company.ShiftWhenOrElse(c => DataAccess.FavoriteLog_Disable(c.oc_code, c.u_id.ToInt()) > 0,
                                    _ => new Resp_Binary() { remark = "取消收藏成功", status = true },
                                    _ => new Resp_Binary() { remark = "取消收藏失败", status = true });


        public static Resp_Binary Company_Report_Send(this Req_Oc_Mini company)
        {
            var lst = DataAccess.OrgCompanyList_Select(company.oc_code);
            if (lst == null || lst.oc_id <= 0)
                return new Resp_Binary() { status = false, remark = "发送失败" };

            company.Export_Pdf(lst);
            return new Resp_Binary() { status = true, remark = "发送成功" };
        }

        private async static void Export_Pdf(this Req_Oc_Mini company, OrgCompanyListInfo lst)
        {
            await Task.Run(() =>
            {

                Thread.Sleep(500);
                string file_name = $"{lst.oc_name}-企业信用报告-企业查询宝";
                string key = "pdf_interval_key" + company.oc_code;

                string baseUrl = System.AppDomain.CurrentDomain.BaseDirectory;//debug后面有\
                    string filename = file_name + "-" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                string dirpath = baseUrl + "exportpdf\\";

                if (!Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }

                if (!File.Exists(dirpath + filename))
                {
                    var ms = new MemoryStream();
                    PDFReport.ExportPDF(company.oc_area, company.oc_code, ms);
                    byte[] bytes = GetPrivateField<byte[]>(ms, "_buffer");
                    int len = GetPrivateField<int>(ms, "_length");

                    Directory.CreateDirectory(dirpath);
                    var files = Directory.GetFiles(dirpath, $"{file_name}*.pdf");
                    if (files.IsNotNull() && files.Count() > 0)
                    {
                        files.ToList().ForEach(t => File.Delete(t));
                    }

                    using (FileStream fs2 = new FileStream(dirpath + filename, FileMode.Create))
                    {
                        fs2.Write(bytes, 0, bytes.Length);
                    }

                    MemoryStream msCpy = new MemoryStream(bytes, 0, len);
                    ServiceHandler.SendReportMail(company.oc_code, filename, company.u_email, company.u_name, company.u_id, lst, msCpy);
                    bytes = null;
                }
                else
                {
                    if (!IsFileInUse(dirpath + filename))
                    {
                        using (FileStream fs = new FileStream(dirpath + filename, FileMode.Open))
                        {
                            int fslen = (int)fs.Length;
                            byte[] bytes = new Byte[fslen];
                            int r = fs.Read(bytes, 0, fslen);

                            MemoryStream msCpy = new MemoryStream(bytes, 0, fslen);
                            ServiceHandler.SendReportMail(company.oc_code, filename, company.u_email, company.u_name, company.u_id, lst, msCpy);
                            bytes = null;
                        }
                    }
                }
            });
        }

        private static bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            if (File.Exists(fileName))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    inUse = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                return inUse;           //true表示正在使用,false没有使用
            }
            else
            {
                return false;           //文件不存在则一定没有被使用
            }
        }

        public static T GetPrivateField<T>(object instance, string fieldname)
        {
            System.Reflection.BindingFlags flag = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic; Type type = instance.GetType();
            System.Reflection.FieldInfo field = type.GetField(fieldname, flag); return (T)field.GetValue(instance);
        }
        #region user

        #region login
        public static Resp_Login User_Login(this User_Login login)
        {
            var result = new Resp_Login() { state = Login_State.Name_Err };  /* initial state name_err, corresponding to no user searched from database */
            var login_log = new Users_LoginLogs_Info();
            
            // todo finish inserting login log into database
            var user_Mb = login.ToMaybe().Select<UserInfo>(l =>                         // select user from database
                {
                    if (l.x_type == X_Type.Phone)
                        return DataAccess.User_FromPhoneNum_Select_2(l.u_x);
                    if (l.x_type == X_Type.Name)
                        return DataAccess.User_FromName_Select(l.u_x);
                    return DataAccess.User_FromEmail_Select_2(l.u_x);
                })


                .DoWhenButNone(u => u.u_pwd != Cipher_Md5.Md5_16_1(login.u_pwd), _ => result.state = Login_State.Pwd_Err)      /* pwd dismatch */
                .DoWhenButNone(u => u.u_status == (int)Users_State.Prohibit || u.u_status == (int)Users_State.Closed, _ => result.state = Login_State.State_Err)  /* status error */
                .DoWhenButNone(u => u.u_status == (int)Users_State.ADBlack, _ => result.state = Login_State.ADBlack_Err)
                .DoWhen(u => !DataAccess.User_FirstLogin_Exist(u.u_id),       // to check whether logins at the first time, if do, present some experience
                        u => User_FirstLogin_Insert(u).User_Exp_Boost(u))
                .Do(u => User_Update(u))    /* update user info */
                .Do(u =>
                    {
                        result.state = Login_State.Success; result.u_id = u.u_uid.ToString(); result.u_name = u.u_name;
                        result.u_face = Util.UserFace_Get(u.u_uid);
                    });
            var vip = DataAccess.VipStatusUser_Selectbyvip_id(user_Mb.Value.u_id);
            if (vip.IsNotNull())
            {
                double days = (vip.vip_vaildate - DateTime.Now).TotalDays;
                if (days > 0)
                {
                    result.isVip = vip.vip_isVaild;
                    result.vipremainDays = (vip.vip_vaildate - DateTime.Now).TotalDays.ToString();
                }
            }
            return result;
        }

        private static string User_FirstLogin_Insert(UserInfo u)
        {
            var info = new Users_AppFirstLoginLogsInfo();
            info.ul_u_uid = u.u_id;
            info.ul_u_name = u.u_name;
            info.ul_createTime = DateTime.Now;
            return DataAccess.User_FirstLogin_Insert(info) > 0 ? "true" : "false";
        }

        private static void User_Exp_Boost(this string state, UserInfo u)
        {
            if (state == "true")
                u.u_total_exp += 100;
        }
        private static void User_Update(UserInfo u)
        {
            u.u_prevLoginTime = u.u_curLoginTime;
            u.u_curLoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            u.u_login_num++;
            if (!string.IsNullOrEmpty(u.u_face) && u.u_face.ToLower().Contains("qianzhan.com"))
                u.u_face = string.Empty;
            DataAccess.User_Update(u);
        }
        #endregion

        #region user portrait
        public static Response Face_Set(this Req_Portrait portrait)
        {
            var head = new Response_Head() { Action = Message_Action.Logic_Err, Text = "上传失败" };    /* in fact, here the situation is "user do no exist" */
            var body = new Resp_Login();

            var user_Mb = portrait.ToMaybe().Select<UserInfo>(p => DataAccess.User_FromId_Select_2(p.u_id.ToInt()));

            var upload_Ei = user_Mb.Do(u => Portrait_Upload(portrait.img_s, Portrait_Type.small, portrait.u_id).ToMaybe())
                                   .Select(u => Portrait_Upload(portrait.img, Portrait_Type.large, portrait.u_id).ToMaybe())
                                   .Where(ei => ei.HasRight)


                                   .Do(ei => DataAccess.User_Update(user_Mb.Value.SetUserFace(string.Empty)))
                                   .Do(ei => { body.u_face = ei.Right; head.Action = Message_Action.None; head.Text = string.Empty; });



            return new Response(head.ToJson().ToEncryption(EncryptType.PT), body.ToJson().ToEncryption(EncryptType.PT));
        }

        private static Either<string, string> Portrait_Upload(string image, Portrait_Type type, string u_id)
        {
            var data = Convert.FromBase64String(image);
            if (data.Length > 1024 * 500)          // check size of each image
                return new Either<string, string>().SetLeft("图片大小不能超过500K");

            var ext = FileHelper.Ext_Get(data);     // check ext format of each image
            if (string.IsNullOrEmpty(ext))
                return new Either<string, string>().SetLeft("图片格式错误");

            var upload = Upload_Proxy.Portrait_Upload(u_id, string.Empty, data, type, true);
            if (upload.Length > 0)
                return new Either<string, string>().SetRight(upload[0].FullUrl);

            return new Either<string, string>().SetLeft("上传失败");
        }
        #endregion

        #region user info binding
        public static Resp_User_Info_Set Info_Set(this Req_User_Info info, Request_Head head)
        {
            switch (info.type)
            {
                case User_Info_Type.name:
                    return Uname_Set_Execute(info, head);
                case User_Info_Type.email:
                    return Email_Set_Execute(info);
                case User_Info_Type.position:
                    return Info_Set_Execute(info, "uai_position");
                case User_Info_Type.company:
                    return Info_Set_Execute(info, "uai_company");
                case User_Info_Type.business:
                    return Info_Set_Execute(info, "uai_business");
                case User_Info_Type.bus_favor:
                    return Info_Set_Execute(info, "uai_b_favorite");
                case User_Info_Type.pos_favor:
                    return Info_Set_Execute(info, "uai_p_favorite");
                case User_Info_Type.per_sign:
                    return Info_Set_Execute(info, "uai_sign");
                default:
                    return Resp_User_Info_Set.Default;
            }
        }

        private static Resp_User_Info_Set Info_Set_Execute(Req_User_Info info, string field)
        {
            try
            {
                if (DataAccess.UserAppendInf_Set(info.u_id.ToInt(), field, info.value) > -1)
                    return new Resp_User_Info_Set() { status = true, remark = "设置成功" };
                return new Resp_User_Info_Set() { status = false, remark = "设置失败" };
            }
            catch (Exception e)
            {
                return Resp_User_Info_Set.Default;
            }
        }

        private static Resp_User_Info_Set Email_Set_Execute(Req_User_Info info)
        {
            var resp = new Resp_User_Info_Set() { remark = "绑定失败" };
            info.ToMaybe().DoWhenButNone(i => !Judge.IsEmail(i.value), _ => resp.remark = "邮箱错误")
                          .DoWhenButNone(i => DataAccess.User_FromEmail_Select_2(i.value) != null, _ => resp.remark = "邮箱已被使用")

                          .Select<UserInfo>(i => DataAccess.User_FromId_Select_2(i.u_id.ToInt()))   /* current object is switched to user*/
                          .DoWhenOrElse(u => u.u_status_email == 0,
                                        u =>
                                        {
                                            if (u.u_email != info.value)
                                            {
                                                u.u_email = info.value;
                                                DataAccess.User_Update(u);
                                            }
                                            ServiceHandler.MailNotice_Send(u.u_id, u.u_name, info.value, string.Empty, string.Empty);
                                            resp.remark = "设置成功，请进入邮箱进行激活"; resp.status = true;
                                        },
                                        _ => resp.remark = "邮箱已被绑定");

            return resp;
        }
        private static Resp_User_Info_Set Uname_Set_Execute(Req_User_Info info, Request_Head head)
        {
            var u_id = info.u_id.ToInt();
            var resp = new Resp_User_Info_Set();
            var info_alias = info.ToMaybe().DoWhenButNone(i => Judge.IsPhoneNum(i.value), _ => resp.remark = "用户名不能为手机号")
                                           .DoWhenButNone(i => Instrument.Utility.Util.Length_Get(i.value) > 30, _ => resp.remark = "用户名太长")
                                           .Do(i => ServiceHandler.UserName_Validate(i.value).ToMaybe().Do(j => resp.remark = j));

            if (string.IsNullOrEmpty(resp.remark))   /* passing filters above */
            {
                resp.remark = "修改失败";   /* initial state */
                DataAccess.UserNameUpdate_Flag_Get(u_id, int.Parse(ConfigurationManager.AppSettings["u_name_limit"])).ToMaybe()
                    .DoWhenButNone(b => !b, _ => resp.remark = "修改次数已用完")
                    .Do(_ => DataAccess.User_FromId_Select_2(u_id).ToMaybe().Do(u =>
                        {
                            var old_name = u.u_name;
                            u.u_name = info.value;
                            if (DataAccess.User_Update(u) > 0)
                            {
                                UnameUpdate_Log_Insert(u, head, old_name);
                                resp.remark = "修改成功";
                                resp.status = true;
                                resp.ext = new Token(head.Cookie, info.u_id, info.value).Compose(t => Cipher_Md5.Md5_16(t)).Induce();
                            }
                            else
                                resp.remark = "修改失败";
                        }));

            }
            return resp;
        }

        private static int UnameUpdate_Log_Insert(UserInfo user, Request_Head head, string old_name)
        {
            var log = new User_UpdateNameLog();
            log.clientIp = Util.Get_RemoteIp();
            log.createdate = DateTime.Now;
            log.namefront = old_name;
            log.nameback = user.u_name;
            log.userid = user.u_uid;
            log.version = head.App_Ver;
            log.platform = head.Platform == Platform.Android ? "android" : "ios";

            return DataAccess.UnameUpdate_Log_Insert(log);
        }
        public static User_Append_Info Info_Get(this Req_User_Info info)
        {
            var u_id = info.u_id.ToInt();
            User_Append_Info u_append = null;
            var u_append_Mb = DataAccess.UserAppendInf_Select(u_id).ToMaybe();
            var user = DataAccess.User_FromId_Select(u_id);
            var count = DataAccess.UserNameUpdateLog_Count_Get(u_id);
            var total = int.Parse(ConfigurationManager.AppSettings["u_name_limit"]);
            var rest_count = total > count ? total - count : 0;
            if (u_append_Mb.HasValue)
            {
                u_append = u_append_Mb.Value;
                if (user != null)
                {
                    u_append.u_name = user.u_name;
                    u_append.u_face = Util.UserFace_Get(u_id);
                    u_append.u_email = user.u_email;
                    u_append.u_email_status = user.u_email_status;
                }
            }
            else if (user != null)
            {
                u_append = new User_Append_Info() { u_email = user.u_email, u_email_status = user.u_email_status, u_face = Util.UserFace_Get(u_id), u_name = user.u_name };
            }
            else
                u_append = new User_Append_Info();

            u_append.u_name_count = rest_count;
            return u_append;
        }
        #endregion
        public static Resp_OpenUser_Login OpenUser_FirstLogin(this Req_Open_Login login)
        {
            var dtl = new Resp_OpenUser_Login();
            var user = ServiceHandler.New_OpenUser_Create(login);
            var uid = DataAccess.User_Insert_2(user);
            if (uid == -2)
            {
                // 如果昵称已经存在，则默认修改昵称
                user.u_name = user.u_name + DateTime.Now.ToString("yyyyMMdd");//"_QianZhan";
                uid = DataAccess.User_Insert_2(user);
                // 如果还重复，则提醒修改昵称
                if (uid == -2)
                {
                    dtl.remark = "绑定登录失败,昵称已被占用，请换一个";
                    return dtl;
                }
            }
            var new_uid = DataAccess.UserId_Select(uid);
            if (new_uid < 1)
            {
                dtl.remark = "绑定登录失败,用户ID获取失败";
                return dtl;
            }

            Users_SocialInfo u = new Users_SocialInfo();
            u.us_attentionsNum = login.us_attentionsNum;
            u.us_contentsNum = login.us_contentsNum;
            u.us_fansNum = login.us_fansNum;
            u.us_favorsNum = login.us_favorsNum;
            u.us_gender = login.us_gender;
            u.us_headImg = login.us_headImg;
            u.us_location = login.us_location;
            u.us_siteurl = login.us_siteurl;
            u.us_name = login.us_name;
            u.us_nick = login.us_nick;
            u.us_type = login.us_type;
            u.us_uid = login.us_uid;
            u.us_verified = login.us_verified;
            u.us_lastLogin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            u.us_loginNum = 1;
            u.us_code = string.Empty;
            u.us_openkey = string.Empty;
            u.us_sync2 = true;
            u.us_createTime = DateTime.Now;
            u.us_u_uid = new_uid;
            var index = DataAccess.Users_Socials_Insert(u);
            if (index < 1)
            {
                dtl.remark = "绑定登录失败";
                return dtl;
            }

            var webClient = new System.Net.WebClient();
            try
            {
                var imgData = webClient.DownloadData(login.us_headImg);
                var ext = FileHelper.Ext_Get(imgData);
                if (!string.IsNullOrEmpty(ext))
                {
                    var upload = Upload_Proxy.Portrait_Upload(new_uid.ToString(), string.Empty, imgData, Portrait_Type.large, true);
                    if (upload.Length > 0)
                        dtl.u_face = upload[0].FullUrl;
                }
            }
            catch
            { }
            dtl.status = true;
            dtl.u_id = new_uid;
            dtl.u_name = user.u_name;
            return dtl;
        }

        #region query history
        public static List<History_Query> History_Query(this Req_Cm_Topic query)
        {
            var search = new DatabaseSearchModel().SetPageIndex(query.pg_index).SetPageSize(query.pg_size).SetOrder(" max(sh_id) desc ").SetWhere($"sh_u_uid={query.u_id}").SetWhere("sh_searchType = 1")
                .SetWhere("sh_oc_name is not null and sh_oc_name <> '' GROUP BY sh_oc_name")     // distince sh_oc_name
                .SetColumn("sh_oc_name, max(sh_id) as max_id, max(sh_date) as max_date, max(sh_oc_code) as max_oc_code, max(sh_oc_area) as max_oc_area");
            //var search = new DatabaseSearchModel().SetPageIndex(query.pg_index).SetPageSize(query.pg_size).SetOrder(" max_id desc ").SetWhere($"sh_u_uid={query.u_id}").SetWhere("sh_searchType = 1")
            //    .SetWhere("sh_oc_name is not null GROUP BY sh_oc_name")     // distince sh_oc_name
            //    .SetColumn("sh_oc_name, max(sh_id) as max_id, max(sh_date) as max_date, max(sh_oc_code) as max_oc_code, max(sh_oc_area) as max_oc_area");
            //var search = new DatabaseSearchModel().SetPageIndex(query.pg_index).SetPageSize(query.pg_size).SetOrder(" sh_id desc ").SetWhere($"sh_u_uid={query.u_id}").SetWhere("sh_searchType = 1")
            //    .SetWhere("sh_oc_name is not null and sh_oc_name <> ''");
            var list = DataAccess.History_Query(search, query.u_id.ToInt());
            //if (list != null && list.Count > 0)
            //{
            //    var lst = new List<History_Query>();
            //    foreach (var l in list)
            //    {
            //        if (!lst.Exists(q => q.oc_name.Equals(l.oc_name)))
            //            lst.Add(l);
            //    }
            //    return lst;
            //}
            return list;
        }

        public static Resp_Binary Query_Delete(this Req_Query del) =>
            DataAccess.Query_Delete(del.u_id.ToInt(), del.oc_name) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };

        public static Resp_Binary Query_Drop(this Req_Query del) =>
            DataAccess.Query_Drop(del.u_id.ToInt()) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };

        public static Maybe<Ext_SearchHistory> Ext_SearchHistory_Get(this Maybe<Req_Info_Query> query) =>
            query.Select<DatabaseSearchModel, List<string>>(q => new DatabaseSearchModel().SetPageIndex(q.pg_index).SetPageSize(q.pg_size)
                                                                                          .SetOrder(" sh_id desc ").SetWhere($"sh_uid={q.u_id}").SetWhere($"sh_type={q.q_type}"),
                                                           (q, s) => DataAccess.Ext_SearchHistory_Page_Select(s, q.u_id.ToInt()))
                 .Select<Ext_SearchHistory>(list =>
                    {
                        if (list != null && list.Count > 0)
                        {
                            var lst = new List<string>();
                            foreach (var l in list)
                            {
                                if (!lst.Exists(q => q.Equals(l)))
                                    lst.Add(l);
                            }
                            return new Ext_SearchHistory() { query_list = lst, q_type = query.Value.q_type };
                        }
                        return new Ext_SearchHistory() { query_list = new List<string>(), q_type = query.Value.q_type };
                    });

        public static Resp_Binary Ext_SearchHistory_Drop(this Req_Info_Query drop) =>
            DataAccess.Ext_SearchHistory_Drop(drop.u_id.ToInt(), drop.q_type) > 0
            ? new Resp_Binary() { status = true, remark = "删除成功" }
            : new Resp_Binary() { remark = "删除失败" };
        #endregion

        #region browse
        public static List<Browse_Log> Browses_Get(this Req_Cm_Topic browse)
        {
            var search = new DatabaseSearchModel().SetPageIndex(browse.pg_index).SetPageSize(browse.pg_size).SetOrder(" max(bl_id) desc ").SetWhere($"bl_u_uid={browse.u_id}")//.SetWhere("sh_searchType = 1")
                .SetWhere("isnull(bl_oc_name,'')<>'' GROUP BY bl_oc_name")     // distince bl_oc_name
                .SetColumn("bl_oc_name, max(bl_id) as max_id, max(bl_date) as max_date, max(bl_oc_code) as max_oc_code, max(bl_oc_area) as max_oc_area");
            //var search = new DatabaseSearchModel().SetPageIndex(browse.pg_index).SetPageSize(browse.pg_size).SetOrder(" bl_id desc ").SetWhere($"bl_u_uid={browse.u_id}")
            //    .SetWhere("bl_oc_name is not null");
            var list = DataAccess.Browses_Get(search, browse.u_id.ToInt());

            //if (list != null && list.Count > 0)
            //{
            //    var lst = new List<Browse_Log>();
            //    foreach (var l in list)
            //    {
            //        if (!lst.Exists(q => q.oc_name.Equals(l.oc_name)))
            //            lst.Add(l);
            //    }
            //    return lst;
            //}
            return list;
        }

        public static Resp_Binary Browse_Delete(this Req_Browse browse) =>
            DataAccess.Browses_Delete(browse.u_id.ToInt(), browse.oc_name) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };

        public static Resp_Binary Browse_Drop(this Req_Browse browse) =>
            DataAccess.Browses_Drop(browse.u_id.ToInt()) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };
        #endregion

        #region company favorites
        public static Resp_NewFavorites Favorites_NewGet(this Req_Cm_Topic favorite)
        {
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(favorite.pg_index).SetPageSize(favorite.pg_size).SetWhere($"fl_u_uid={favorite.u_id}").SetWhere("fl_valid=1").SetOrder(" fl_id desc ");
            var grouplist = new List<Favorite_Group>();
            //查询某个分组（不包含全部分组）,不返回grouplist，减少传输
            if (favorite.group_id != 0)
                search = search.SetWhere($"fl_g_gid={favorite.group_id}");
            else
            {
                DataAccess.FavoriteGroups_Selectbyu_uid(favorite.u_id.ToInt()).ToMaybe()
                            //.DoWhen(gs => !gs.Exists(g => g.g_name.Equals("全部")),
                            //action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "全部", fl_count = 0 }))
                            .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "竞品", fl_count = 0 }))
                             .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "客户", fl_count = 0 }))
                             .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "供应商", fl_count = 0 }))
                             .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "经销商", fl_count = 0 }))
                             .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "其他", fl_count = 0 }))
                             .DoWhen(gs => gs.Count == 0,
                             action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = favorite.u_id.ToInt(), g_name = "关注", fl_count = 0 }));


                grouplist = DataAccess.FavoriteGroups_Selectbyu_uid(favorite.u_id.ToInt());
            }

            var list = DataAccess.Favorites_Get(search, out count);
            list.ForEach(t => t.favorite_note = DataAccess.FavoriteNote_Top(t.favorite_id));
            if (list == null)
                list = new List<Favorite_Log>();
            return new Resp_NewFavorites() { favorite_list = list, favorite_group = grouplist, count = count };
        }

        public static Resp_Favorites Favorites_Get(this Req_Cm_Topic favorite)
        {
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(favorite.pg_index).SetPageSize(favorite.pg_size).SetWhere($"fl_u_uid={favorite.u_id}").SetWhere("fl_valid=1").SetOrder(" fl_id desc ");
            var list = DataAccess.Favorites_Get(search, out count);


            if (list == null)
                list = new List<Favorite_Log>();
            return new Resp_Favorites() { favorite_list = list, count = count };
        }
        #endregion

        #region personal notice
        public static Resp_Binary Notice_Status(this Req_User_Info user) => new Resp_Binary() { status = DataAccess.TopicTrace_Status_Get(user.u_id.ToInt()) };

        public static Resp_Oc_Notice Notice_Companies(this Req_Cm_Topic user)
        {
            int count = 0;
            // attention: fvt_status = 1 denotes that favorite is normal and no matter the favorite trace is read or isn't read
            //            IsRead = 1 denotes that favorite is unread. These two points should have an `or` relation
            var search = new DatabaseSearchModel().SetPageIndex(user.pg_index).SetPageSize(user.pg_size).SetWhere($"fl_u_uid={user.u_id}").SetWhere($"(fvt_status=1 or IsRead = 1)").SetOrder(" ct_createtime desc ");
            var list = DataAccess.FavoriteTraces_Get(search, out count);
            var resp = new Resp_Oc_Notice() { count = count };
            resp.notice_list = list == null ? new List<Oc_Notice>() : list;
            return resp;
        }
        public static Resp_Binary Notice_Company_Remove(this Req_Oc_Mini user)
        {
            bool remove_flag = false;
            var u_id = user.u_id.ToInt();
            var r = DataAccess.FavoriteViewTrace_Time_Status_Update(u_id, user.oc_code, true);
            if (r == -1)   // -1 means no record found in database, so it needs to insert a record into record
            {
                var info = new FavoriteViewTraceInfo() { fvt_oc_code = user.oc_code, fvt_u_uid = u_id, fvt_status = false, fvt_viewtime = DateTime.Now };
                remove_flag = DataAccess.FavoriteViewTrace_Insert(info) > 0;
            }
            else if (r > 0)
                remove_flag = true;

            return new Resp_Binary() { status = remove_flag, remark = remove_flag ? "删除成功" : "删除失败" };
        }
        public static Resp_Binary Notice_Topic_Remove(this Req_Topic_Vote user) =>
            DataAccess.TopicUserTrace_Status_Turnoff(user.topic_id.ToString(), user.u_id.ToInt(), user.topic_type.ToString()) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };

        public static Resp_Binary Notice_Topic_Drop(this Req_Topic_Vote user) =>
            DataAccess.TopicUsersTrace_All_TurnOff(user.u_id.ToInt()) > 0
            ? new Resp_Binary() { remark = "删除成功", status = true }
            : new Resp_Binary() { remark = "删除失败" };

        public static Resp_Topic_Notice Notice_Topics(this Req_Cm_Topic user)
        {
            var u_id = user.u_id.ToInt();
            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(user.pg_index).SetPageSize(user.pg_size).SetOrder(" tut_t_id desc ").SetWhere("tut_status=1").SetWhere($"tut_uid={user.u_id}").SetWhere("tut_t_count>-1");
            var list = DataAccess.Notice_Topics_Get(search, out count);



            if (list != null)
            {
                Parallel.ForEach(list, n => Topic_Notice_Compensate(n, u_id));
            }
            var resp = new Resp_Topic_Notice() { count = count };
            resp.notice_list = list == null ? new List<Topic_Notice>() : list;
            return resp;
        }
        private static void Topic_Notice_Compensate(Topic_Notice notice, int u_id)
        {
            if (notice.topic_type == 0)
                Oc_Topic_Notice_Compensate(notice, u_id);
            else
                Cm_Topic_Notice_Compensate(notice, u_id);
        }

        /// <summary>
        /// compensate the company topic notice
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="u_id">current user id</param>
        private static void Oc_Topic_Notice_Compensate(Topic_Notice notice, int u_id)
        {
            var topic = DataAccess.CompanyTopic_FromId_Get(notice.topic_id);



            if (topic != null)   // not equal to null
            {


                notice.u_id = topic.u_id;
                notice.u_name = topic.u_name;
                notice.u_face = Util.UserFace_Get(topic.u_face, notice.u_id);
                notice.topic_date = topic.topic_date;
                notice.topic_gentle_time = Util.Get_Gentle_Time(notice.topic_date);
                notice.oc_area = topic.oc_area;
                notice.oc_code = topic.oc_code;
                notice.oc_name = topic.oc_name;
                notice.notice_status = topic.status;
                if (topic.status)
                {
                    notice.topic_tag = topic.topic_tag;
                    notice.topic_content = topic.topic_content;
                    notice.reply_count = DataAccess.Company_Topic_ReplyCount_Get(notice.topic_id);
                    var count_tuple = DataAccess.Company_Topic_UpDown_Count_Get(notice.topic_id);
                    notice.down_count = count_tuple.Item2;
                    notice.up_count = count_tuple.Item1;
                    var tuple = DataAccess.Company_Topic_UpDown_Flag_Get(notice.topic_id, u_id);
                    if (tuple.Item1 == 1)
                        notice.up_down_flag = Topic_Up_Down.Up;
                    else if (tuple.Item2 == 1)
                        notice.up_down_flag = Topic_Up_Down.Down;
                    else
                        notice.up_down_flag = Topic_Up_Down.None;
                    notice.pic_list = DataAccess.Oc_Comment_Imgs_Select(notice.topic_id, 0);
                }
                else
                {
                    notice.topic_content = "帖子已被删除";
                }
            }
        }
        /// <summary>
        /// compensate community topic notice
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="u_id"></param>
        private static void Cm_Topic_Notice_Compensate(Topic_Notice notice, int u_id)
        {
            var topic = DataAccess.Community_Topic_Dtl_Get(notice.topic_id);
            if (topic != null)   // not equal to null
            {
                notice.u_id = topic.u_id;
                notice.u_name = topic.u_name;
                notice.u_face = Util.UserFace_Get(topic.u_face, notice.u_id);
                notice.topic_date = topic.topic_date;
                notice.topic_gentle_time = Util.Get_Gentle_Time(notice.topic_date);
                notice.notice_status = topic.status;
                if (topic.status)
                {
                    notice.topic_tag = topic.topic_tag;
                    notice.topic_content = topic.topic_content;
                    notice.reply_count = DataAccess.Community_Topic_ReplyCount_Get(notice.topic_id);
                    var count_tuple = DataAccess.Community_Topic_UpDown_Count_Get(notice.topic_id);
                    notice.down_count = count_tuple.Item2;
                    notice.up_count = count_tuple.Item1;
                    var tuple = DataAccess.Community_Topic_UpDown_Flag_Get(notice.topic_id, u_id);
                    if (tuple.Item1 == 1)
                        notice.up_down_flag = Topic_Up_Down.Up;
                    else if (tuple.Item2 == 1)
                        notice.up_down_flag = Topic_Up_Down.Down;
                    else
                        notice.up_down_flag = Topic_Up_Down.None;
                    notice.pic_list = DataAccess.Cm_Comment_Imgs_Select(notice.topic_id, 0);
                }
                else
                {
                    notice.topic_content = "帖子已被删除";
                }
            }
        }
        #endregion

        public static Resp_OpenUser_Login DominoForExistedOpenLogin(this Users_SocialInfo u, Req_Open_Login login)
        {
            u.us_loginNum = u.us_loginNum + 1;
            u.us_attentionsNum = login.us_attentionsNum;
            u.us_contentsNum = login.us_contentsNum;
            u.us_fansNum = login.us_fansNum;
            u.us_favorsNum = login.us_favorsNum;
            u.us_gender = login.us_gender;
            u.us_headImg = login.us_headImg;
            u.us_location = login.us_location;
            u.us_siteurl = login.us_siteurl;
            u.us_name = login.us_name;
            u.us_nick = login.us_nick;
            u.us_type = login.us_type;
            u.us_verified = login.us_verified;
            u.us_lastLogin = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            DataAccess.Users_Socials_Update(u);
            var user = DataAccess.User_FromId_Select_2(u.us_u_uid);
            var dtl = new Resp_OpenUser_Login();
            dtl.u_id = u.us_u_uid;
            dtl.u_name = user.u_name;
            dtl.status = true;
            if (!string.IsNullOrEmpty(user.u_face))
            {
                // 第三方头像地址
                if (!user.u_face.ToLower().Contains("qianzhan.com"))
                {
                    dtl.u_face = user.u_face;
                }
                // 本地头像地址
                else
                {
                    user.u_face = string.Empty;
                    DataAccess.User_Update(user);
                }
            }
            return dtl;
        }
        #endregion

        #region community
        public static Resp_Binary Community_New_Topic(this Req_Cm_Comment topic)
        {
            var state = new Comment_State();
            var content = topic.topic_content.To_Sql_Safe();    // handle chars of content
            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content.Length > 1000)
                    return new Resp_Binary() { remark = "发帖内容不能超过1000字" };
                Task<File_Upload_Info> task = null;   // declare a task for uploading images

                if (topic.pic_list == null || topic.pic_list.Count < 1)  // no image data
                    state.File_State = File_Upload_State.None;
                else if (topic.pic_list.Count > 10)      // too many images
                    state.File_State = File_Upload_State.Count_Err;
                else        // ready to upload images
                    task = Task.Run(() => Pics_Upload(topic.pic_list, topic.u_id));     // create a new task to do pictures uploading                    

                if (state.File_State != File_Upload_State.Count_Err)    // if image count is large than 10, preventing to post the topic
                {
                    // ready to insert topic data into database
                    var u_id = topic.u_id.ToInt();
                    var i = new AppTieziTopicInfo()
                    {
                        att_u_name = topic.u_name,
                        att_u_uid = u_id,
                        att_content = content,
                        att_date = DateTime.Now,
                        att_status = 1,
                        att_tag = (int)topic.topic_tag
                    };
                    var t_id = DataAccess.Community_Topic_Insert(i);
                    if (t_id > 0)  // insert into database successfully
                    {
                        state.T_R_State = TopicReply_State.Sucess;

                        #region topic user trace
                        var trace = new TopicUsersTraceInfo() { tut_status = true, tut_t_count = -1, tut_t_id = t_id.ToString(), tut_t_type = "1", tut_uid = u_id };
                        DataAccess.TopicUsersTrace_Insert(trace);
                        #endregion

                        #region insert image records into database
                        if (task != null)
                        {
                            var img = new AppTieziImageInfo() { ati_tiezi_id = t_id, ati_tiezi_type = (int)Comment_Type.topic, ati_uid = u_id };
                            try
                            {
                                task.Wait();
                                if (task.Result.State == File_Upload_State.Success)
                                {
                                    state.Count = DataAccess.CommunityTieziImage_Bulk_Insert(img, task.Result.Uris);
                                    state.File_State = File_Upload_State.Success;
                                }
                            }
                            catch (AggregateException e)
                            {
                                foreach (var ex in e.InnerExceptions)
                                    Util.Log_Info(nameof(Community_New_Topic), Location.Internal, ex.Message, $"uploading image task exception: {ex.GetType()}\t{ex.Source}");
                            }
                        }
                        #endregion
                    }
                    else
                        state.T_R_State = TopicReply_State.Db_Insert_Err;
                }
            }
            else
                state.T_R_State = TopicReply_State.Content_Empty;


            return state.To_Resp_Binary();
        }


        public static Resp_Binary Community_New_Reply(this Req_Cm_Comment cmt)
        {
            var state = new Comment_State();
            var content = cmt.reply_content.To_Sql_Safe();    // handle chars of content

            if (!string.IsNullOrWhiteSpace(content))
            {
                if (content.Length > 1000)
                    return new Resp_Binary() { remark = "内容不能超过1000字" };
                Task<File_Upload_Info> task = null;   // declare a task for uploading images

                if (cmt.pic_list == null || cmt.pic_list.Count < 1)  // no image data
                    state.File_State = File_Upload_State.None;
                else if (cmt.pic_list.Count > 10)      // too many images
                    state.File_State = File_Upload_State.Count_Err;
                else        // ready to upload images
                    task = Task.Run(() => Pics_Upload(cmt.pic_list, cmt.u_id));     // create a new task to do pictures uploading                    

                if (state.File_State != File_Upload_State.Count_Err)    // if image count is large than 10, preventing to post the topic
                {
                    // ready to insert reply data into database
                    var u_id = cmt.u_id.ToInt();
                    var i = new AppTeiziReplyInfo()
                    {
                        atr_teizi = cmt.topic_id,
                        atr_u_name = cmt.u_name,
                        atr_u_uid = u_id,
                        atr_content = content,
                        atr_date = DateTime.Now,
                        //ctr_status = 1
                    };
                    var r_id = DataAccess.Community_Reply_Insert(i);
                    if (r_id > 0)  // insert into database successfully
                    {
                        state.T_R_State = TopicReply_State.Sucess;

                        #region topic user trace
                        DataAccess.TopicUserTrace_Refresh(cmt.topic_id.ToString(), "1", u_id);
                        #endregion

                        #region insert image records into database
                        if (task != null)
                        {
                            var img = new AppTieziImageInfo() { ati_tiezi_id = r_id, ati_tiezi_type = (int)Comment_Type.reply, ati_uid = u_id };
                            try
                            {
                                task.Wait();
                                if (task.Result.State == File_Upload_State.Success)
                                    state.Count = DataAccess.CommunityTieziImage_Bulk_Insert(img, task.Result.Uris);
                            }
                            catch (AggregateException e)
                            {
                                foreach (var ex in e.InnerExceptions)
                                    Util.Log_Info(nameof(Community_New_Reply), Location.Internal, ex.Message, $"uploading image task exception: {ex.GetType()}\t{ex.Source}");
                            }
                        }
                        #endregion

                        //#region push message to app

                        List<string> clientidlst = DataAccess.TopicUserTrace_GetClientId(cmt.topic_id, 1);
                        string topic_content = DataAccess.Topic_Content_GetByid(cmt.topic_id);
                        if (clientidlst.Count > 0 && !string.IsNullOrEmpty(topic_content))
                        {
                            MessageInfo mess = new MessageInfo();
                            string title = "你关注的帖子有新消息";
                            mess.type = MessageType.CommunityReply;

                            mess.content = topic_content;
                            mess.title = title;
                            mess.topicid = cmt.topic_id.ToString();
                            mess.u_id = cmt.u_id;
                            mess.u_name = cmt.u_name;
                            mess.replycontent = cmt.reply_content;

                            string content1 = mess.ToJson();
                            string begintime = DateTime.Now.ToString();
                            string endtime = DateTime.Now.AddMinutes(60).ToString();
                            PushService.PushMessageToList(title, topic_content, "", "", "2", content1, false, false, true, begintime, endtime, clientidlst);
                        }

                        //#endregion
                    }
                    else
                        state.T_R_State = TopicReply_State.Db_Insert_Err;
                }
            }
            else
                state.T_R_State = TopicReply_State.Content_Empty;
            return state.To_Resp_Binary();
        }

        public static Resp_Cm_Topics_Dtl Community_Topic_Query(this Req_Cm_Topic user)
        {
            if (user.op_type == 2)
                return Community_Topic_ReplyByMe_Query(user);



            int count = 0;
            var search = new DatabaseSearchModel().SetPageIndex(user.pg_index).SetPageSize(user.pg_size).SetOrder(" att_date desc ").SetWhere("att_status= 1");
            if (user.op_type == 1)
            {


                search.SetWhere("att_u_uid = " + user.u_id);
            }
            var u_id = user.u_id.ToInt();
            var topics = DataAccess.Community_Topics_Dtl_Get(search, out count);
            if (topics == null)
            {
                topics = new List<Cm_Topic_Dtl>();
            }
            else if (topics.Count > 0)
            {

                var result = Parallel.ForEach(topics, t => Cm_Topic_Compensate(t, u_id));
                //topics.Sort(new Topic_Dtl_Comparer());  
                topics = topics.Where(t => !t.t_shield).ToList();

            }



            return new Resp_Cm_Topics_Dtl() { topic_list = topics, count = count };
        }
        public static Resp_Cm_Topics_Dtl Community_Topics_Hot(this Req_Cm_Topic hot)
        {
            var tids = DataAccess.CommunityReply_GroupByTid_Get(ConfigurationManager.AppSettings["cm_topic_hot_limit"].ToInt());
            var list = tids.Select(tid => new Cm_Topic_Dtl() { topic_id = tid }).ToList();
            list.ForEach(t => CommunityTopic_FromId_Get(t, hot.u_id.ToInt()));
            return new Resp_Cm_Topics_Dtl() { topic_list = list };
        }

        private static void Cm_Topic_Compensate(Cm_Topic_Dtl t, int u_id)
        {
            var tip = DataAccess.Cmt_TipOff_Select(t.topic_id, 3, u_id);
            if (tip != null && tip.cto_shield == 1)
            {
                t.t_shield = true;
                return;
            }

            var search = new DatabaseSearchModel();
            t.reply_count = DataAccess.Community_Topic_ReplyCount_Get(t.topic_id);
            //var replies = DataAccess.Replies_Dtl_Select(search.SetPageSize(50).SetOrder(" ctr_date ").SetWhere($"ctr_teizi={t.topic_id}").SetWhere("ctr_status=1"));
            //t.reply_list = replies != null ? Replies_Compensate(replies) : new List<Reply_Dtl>();

            t.u_face = Util.UserFace_Get(t.u_face, t.u_id);
            t.topic_gentle_time = Util.Get_Gentle_Time(t.topic_date);
            t.topic_content = t.topic_content.De_Sql_Safe();
            var count_tuple = DataAccess.Community_Topic_UpDown_Count_Get(t.topic_id);
            t.down_count = count_tuple.Item2;
            t.up_count = count_tuple.Item1;
            if (u_id > 0)   // current user exists, make sure the user's attitude on this topic
            {
                var tuple = DataAccess.Community_Topic_UpDown_Flag_Get(t.topic_id, u_id);


                if (tuple.Item1 == 1)
                    t.up_down_flag = Topic_Up_Down.Up;
                else if (tuple.Item2 == 1)
                    t.up_down_flag = Topic_Up_Down.Down;
                else
                    t.up_down_flag = Topic_Up_Down.None;
            }
            else
                t.up_down_flag = Topic_Up_Down.None;

            // get the topic's image uris
            t.pic_list = DataAccess.Cm_Comment_Imgs_Select(t.topic_id, 0);

        }

        private static Resp_Cm_Topics_Dtl Community_Topic_ReplyByMe_Query(this Req_Cm_Topic topic)
        {
            #region debug
            Util.Log_Info(nameof(Community_Topic_ReplyByMe_Query), Location.Enter, string.Empty, string.Empty);
            #endregion
            var u_id = topic.u_id.ToInt();  // current app user id
            var topic_ids = DataAccess.CommunityReply_GroupBy_TidUid_Get(u_id, topic.pg_index, topic.pg_size);

            if (topic_ids != null && topic_ids.Count > 0)
            {


                var topic_dtls = topic_ids.Select(id => new Cm_Topic_Dtl() { topic_id = id }).ToList();
                Parallel.ForEach(topic_dtls, t => CommunityTopic_FromId_Get(t, u_id));
                return new Resp_Cm_Topics_Dtl() { topic_list = topic_dtls.ToList() };
            }
            else
                return Resp_Cm_Topics_Dtl.Default;
        }
        private static void CommunityTopic_FromId_Get(Cm_Topic_Dtl topic, int u_id)
        {
            var t = DataAccess.Community_Topic_Dtl_Get(topic.topic_id);
            if (t != null)
            {


                topic.u_face = Util.UserFace_Get(t.u_face, t.u_id);
                topic.topic_date = t.topic_date;
                topic.topic_gentle_time = Util.Get_Gentle_Time(t.topic_date);
                topic.u_id = t.u_id;
                topic.u_name = t.u_name;
                topic.status = t.status;
                if (t.status)
                {
                    topic.topic_tag = t.topic_tag;
                    topic.topic_content = t.topic_content;
                    topic.reply_count = DataAccess.Community_Topic_ReplyCount_Get(topic.topic_id);

                    var count_tuple = DataAccess.Community_Topic_UpDown_Count_Get(topic.topic_id);
                    topic.down_count = count_tuple.Item2;
                    topic.up_count = count_tuple.Item1;

                    var tuple = DataAccess.Community_Topic_UpDown_Flag_Get(t.topic_id, u_id/*topic.u_id.ToInt()*/);
                    if (tuple.Item1 == 1)
                        topic.up_down_flag = Topic_Up_Down.Up;
                    else if (tuple.Item2 == 1)
                        topic.up_down_flag = Topic_Up_Down.Down;
                    else
                        topic.up_down_flag = Topic_Up_Down.None;

                    topic.pic_list = DataAccess.Cm_Comment_Imgs_Select(topic.topic_id, 0);

                }
                else
                    topic.topic_content = "帖子已删除";
            }
        }

        public static Maybe<List<Reply_Dtl>> Community_Topic_Detail(this Req_Topic_Dtl topic)
        {
            var search = new DatabaseSearchModel();
            var replies = DataAccess.Cm_Replies_Dtl_Select(search.SetPageSize(topic.pg_size).SetPageIndex(topic.pg_index).SetOrder(" atr_date desc ").SetWhere($"atr_teizi={topic.topic_id}").SetWhere("atr_status=1"));
            if (replies != null)
                Cm_Replies_Compensate(replies);
            return replies;
        }

        private static List<Reply_Dtl> Cm_Replies_Compensate(List<Reply_Dtl> replies)
        {
            var search = new DatabaseSearchModel();
            foreach (var r in replies)
            {
                r.u_face = Util.UserFace_Get(r.u_id.ToInt());
                r.reply_content = r.reply_content.De_Sql_Safe();
                r.reply_gentle_time = Util.Get_Gentle_Time(r.reply_date);
                r.pic_list = DataAccess.Cm_Comment_Imgs_Select(r.reply_id, 1);
            }
            return replies;
        }

        public static Resp_Binary Community_Topic_UpDown_Vote(this Req_Topic_Vote vote)
        {
            var action = vote.op_type == 1 ? "点赞" : "点踩";
            var info = new AppLikeOrNotLogInfo() { all_date = DateTime.Now, all_teizi = vote.topic_id, all_u_name = vote.u_name, all_u_uid = vote.u_id.ToInt(), all_valid = 1 };
            var tuple = DataAccess.Community_Topic_UpDown_Flag_Get(vote.topic_id, vote.u_id.ToInt());
            if (tuple.Item1 == 1)   // already up
            {
                if (vote.op_type == 2)
                {
                    // up -> down
                    if (DataAccess.Community_Topic_Up2Down(info) > 0)
                    {

                        return new Resp_Binary() { remark = "点踩成功", status = true };
                    }
                    return new Resp_Binary() { remark = "点踩失败", status = false };
                }
            }
            else if (tuple.Item2 == 1)  // already down
            {
                if (vote.op_type == 1)
                {
                    // down -> up
                    if (DataAccess.Community_Topic_Down2Up(info) > 0)
                    {
                        Cm_Topic_Dtl dtl = DataAccess.Community_Topic_Dtl_Get(vote.topic_id);
                        if (dtl.IsNotNull() && dtl.u_id.ToInt() > 0)
                        {
                            string clientId = DataAccess.User_ClientID_Getbyu_id(dtl.u_id.ToInt());
                            if (!string.IsNullOrWhiteSpace(clientId))
                            {
                                MessageInfo message = new MessageInfo();
                                message.type = MessageType.CommunityUp;
                                message.u_id = vote.u_id;
                                message.u_name = vote.u_name;
                                message.title = "你发布的帖子有了新的点赞";
                                message.content = dtl.topic_content;
                                string begintime = DateTime.Now.ToString();
                                string endtime = DateTime.Now.AddMinutes(60).ToString();
                                PushService.PushMessageToSingle("2", message.title, message.ToJson(), begintime, endtime, clientId);
                            }

                        }
                        return new Resp_Binary() { remark = "点赞成功", status = true };
                    }
                    return new Resp_Binary() { remark = "点赞失败", status = false };
                }
            }
            else                        // had not voted
            {
                info.all_type = vote.op_type;

                if (DataAccess.Community_Topic_UpDown_Vote(info) > 0)
                    return new Resp_Binary() { remark = action + "成功", status = true };
                return new Resp_Binary() { remark = action + "失败", status = false };
            }

            return new Resp_Binary() { remark = $"当前已经{action}", status = true };
        }

        public static Maybe<Resp_Binary> Company_Report_Collect(this Req_ReportsReq req) =>
            req.ToMaybe().Where(t => !string.IsNullOrEmpty(req.phone) && !string.IsNullOrEmpty(req.contact) && !string.IsNullOrEmpty(req.content))
                .DoWhenOrElse(b => b.type == 0, w => w.oc_name = "信用评估报告_" + w.oc_name, e => e.oc_name = "尽调报告_" + e.oc_name)
                .Select<int>(t => DataAccess.Company_Report_Collect(t))
                .ShiftWhenOrElse(b => b > 0, w => new Resp_Binary { remark = "提交成功", status = true }, e => new Resp_Binary { remark = "提交失败", status = false });

        public static Resp_Binary Company_Claim_Submit(this Req_Claim req)
        {
            var emailReg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            var contactsReg = new Regex(@"^[\u4e00-\u9fa5]{2,4}|[a-zA-Z0-9]{4,20}$");
            var phoneReg = new Regex(@"^\d{11}$");
            var isadd = false;

            if (req.cc_oc_code.IsNotNull() && req.cc_oc_code.Length != 9)
                return new Resp_Binary { remark = "认证公司异常", status = false };
            if (req.cc_e_mail.IsNotNull() && !emailReg.Match(req.cc_e_mail).Success)
                return new Resp_Binary { remark = "邮箱不合法，请重新输入", status = false };
            if (req.contact.IsNotNull() && !contactsReg.Match(req.contact).Success)
                return new Resp_Binary { remark = "联系人未输入或输入有误", status = false };
            if (string.IsNullOrWhiteSpace(req.ccc_mobile))
                return new Resp_Binary { remark = "手机号未输入", status = false };
            if (!phoneReg.Match(req.ccc_mobile).Success)
                return new Resp_Binary { remark = "手机号不合法", status = false };
            var i = ServiceHandler.Code_Verify(req.ccc_mobile, req.verify_code);
            if (i < 1)
                return i.ToMaybe().ShiftWhenOrElse(b => i < 0, r => new Resp_Binary { remark = "验证码过期", status = false }, d => new Resp_Binary { remark = "验证码错误", status = false }).Value;

            var info = DataAccess.ClaimCompany_Selectbycc_oc_code(req.cc_oc_code).FirstOrDefault();

            if (!info.IsNotNull() && info.cc_status == 3)
                isadd = true;
            else
                return new Resp_Binary { status = false, remark = "已经提交资料，请先删除重新提交新的资料" };
            ClaimCompanyInfo obj = new ClaimCompanyInfo();
            if (isadd)
            {
                var comMirror = DataAccess.OrgCompanyList_Select(req.cc_oc_code);
                if (comMirror != null)
                {
                    obj.cc_oc_name = comMirror.oc_name;
                    obj.cc_u_uid = req.u_id.ToInt();
                    obj.cc_status = bool.Parse(SiteConfigHelper.GetSiteConfig(SiteConfigHelper.IsClaimStatus)) ? 2 : 1;
                    obj.cc_e_mail = obj.cc_e_mail.NullToString();
                    obj.cc_createTime = DateTime.Now;
                    obj.cc_createUser = req.u_name;
                    obj.cc_isvalid = true;
                    obj.cc_checkTime = DateTime.Now;
                    obj.cc_checkUser = "";
                    obj.cc_zj_data = req.images.IsNotNull() ? "[" + string.Join("", req.images.Select(t => "{\"url\":\"" + t.ToString() + "\"}" + ",")).TrimEnd(',') + "]" : "[]";
                    var result = DataAccess.Claim_Company_Insert(obj);

                    if (result > 0)
                        return new Resp_Binary { remark = "提交成功", status = true };
                    else
                        return new Resp_Binary { remark = "提交失败", status = false };
                }
            }
            return new Resp_Binary { remark = "提交失败", status = false };
        }

        public static Resp_Common Vip_Order_Submit(this Req_VipOrder req,Either<Response,Request_Head> pre_Ei)
        {
            if (req.type != 1 && req.type != 2 && req.type != 3 || string.IsNullOrWhiteSpace(req.mobile))
                return new Resp_Common { remark = "请填写联系方式", status = false };
            var ordername = "";
            var money = 30;
            var pay_mode = "支付宝";
            switch(req.type)
            {
                case 1:
                    ordername = "一个月30元";
                    money = 30;
                    break;
                case 2:
                    ordername = "三个月84元";
                    money = 84;
                    break;
                case 3:
                    ordername = "一年300元";
                    money = 300;
                    break;
            }

            switch (req.pay_type)
            {
                case 1:
                    pay_mode = "支付宝";
                    break;
                case 2:
                    pay_mode = "微信";
                    break;
            }


            VipUserOrderInfo item = new VipUserOrderInfo();
            item.mo_orderid = "hy" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            item.mo_ordername = ordername;
            item.mo_money = Convert.ToDecimal(0.01);
            item.mo_tradeNo = "";
            item.mo_paySuccess = false;
            item.mo_payType = pay_mode;
            item.mo_payTime = "";
            item.mo_payInfo = "";
            item.mo_state = (int)ApiOrderStateEnums.待支付;
            item.mo_remark = "";
            item.mo_userid = req.u_id.ToInt();
            item.mo_userName = req.u_name;
            if (pre_Ei.Right.Platform == Platform.Android)
                item.mo_platformType = 2;
            else if (pre_Ei.Right.Platform == Platform.Iphone)
                item.mo_platformType = 3;
            else if (pre_Ei.Right.Platform == Platform.Ipad)
                item.mo_platformType = 4;
            else
                item.mo_platformType = 1;
            item.mo_createTime = DateTime.Now;
            item.mo_ip = "";
            item.mo_mobile = req.mobile;
            var result = DataAccess.VipUserOrder_Insert(item);
            if(result>0)
            {
                string signature = AlipayHandler.Signature(item);
                return new Resp_Common { remark = "提交成功", status = true, addition = signature };
            }
            return new Resp_Common { remark = "提交失败", status = false };
        }

        public static Resp_Common Vip_Order_Notify(this Req_VipOrder req)
        {
            AlipayTradeQueryResponse result = AlipayHandler.alipay_trade_query(req.out_trade_no, req.trade_no);
            if (result.Code.Equals("10000") && (result.TradeStatus.Equals("TRADE_SUCCESS") || result.TradeStatus.Equals("TRADE_FINISHED")))
            {
                var type = req.out_trade_no.Substring(0, 2);
                if (type == "hy")
                {
                    var orderInfo = DataAccess.VipUserOrder_Selectbymo_orderid(req.out_trade_no);
                    if (orderInfo.IsNotNull())
                    {
                        if (UpdatePayState(req.trade_no, orderInfo))
                        {
                            
                            var user = DataAccess.VipStatusUser_Selectbyvip_id(req.u_id.ToInt());

                            return new Resp_Common { status = true, addition = (user.vip_vaildate - DateTime.Now).TotalDays.ToString() };
                        }
                        else
                            return new Resp_Common { status = false };
                    }
                    else
                    {
                        return new Resp_Common { remark = "订单不存在", status = false };
                    }
                }
                else if (type == "qy")
                {
                    var orderInfo = DataAccess.ExcelCompanyOrder_Selectbyeco_orderid(req.out_trade_no);
                    if (orderInfo.IsNotNull())
                    {
                    }
                    else
                        return new Resp_Common { remark = "订单不存在", status = false };
                }
            }
            else
            {
                return new Resp_Common { remark = "交易状态有误", status = false };
            }

            return new Resp_Common { status = false };
        }

        private static bool UpdatePayState(string trade_no, VipUserOrderInfo orderInfo)
        {
            var updateInfo = orderInfo.ToMaybe().Where(t => t.mo_state == (int)ApiOrderStateEnums.待支付)
                                            .Select<VipUserOrderInfo>(t =>
                                            {
                                                t.mo_state = (int)ApiOrderStateEnums.已支付;
                                                t.mo_paySuccess = true;
                                                t.mo_tradeNo = trade_no;
                                                t.mo_payTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                return t;
                                            });
            var result = updateInfo.HasValue ? DataAccess.VipUserOrder_Update(updateInfo.Value) > 0 : false;
            result = UpdatePayState(orderInfo, result);

            return result;
        }

        private static bool UpdatePayState(VipUserOrderInfo orderInfo,bool result)
        {
            #region update user status
            if (result)
            {
                var vus = DataAccess.VipStatusUser_Selectbyvip_id(orderInfo.mo_userid);
                if (vus.IsNotNull())
                {
                    vus.vip_status = true;
                    vus.vip_mobile = orderInfo.mo_mobile;
                    vus.vip_isSMS = false;
                    if (DateTime.Now > vus.vip_vaildate)
                    {
                        if (orderInfo.mo_money.ToString("f2") == "30.00")
                        {
                            vus.vip_vaildate = DateTime.Now.AddMonths(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "0.01")
                        {
                            vus.vip_vaildate = DateTime.Now.AddMonths(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "84.00")
                        {
                            vus.vip_vaildate = DateTime.Now.AddMonths(3);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "300.00")
                        {
                            vus.vip_vaildate = DateTime.Now.AddYears(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "500.00")
                        {
                            vus.vip_vaildate = DateTime.Now.AddYears(2);
                        }
                    }
                    else
                    {
                        if (orderInfo.mo_money.ToString("f2") == "30.00")
                        {
                            vus.vip_vaildate = vus.vip_vaildate.AddMonths(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "0.01")
                        {
                            vus.vip_vaildate = vus.vip_vaildate.AddMonths(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "84.00")
                        {
                            vus.vip_vaildate = vus.vip_vaildate.AddMonths(3);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "300.00")
                        {
                            vus.vip_vaildate = vus.vip_vaildate.AddYears(1);
                        }
                        else if (orderInfo.mo_money.ToString("f2") == "500.00")
                        {
                            vus.vip_vaildate = vus.vip_vaildate.AddYears(2);
                        }
                    }
                    result = DataAccess.VipStatusUser_Update(vus) > 0;
                }
                else
                {
                    vus = new VipStatusUserInfo();
                    vus.vip_userId = orderInfo.mo_userid;
                    vus.vip_isVaild = true;
                    vus.vip_status = true;
                    vus.vip_mobile = orderInfo.mo_mobile;
                    vus.vip_isSMS = false;
                    if (orderInfo.mo_money.ToString("f2") == "30.00")
                    {
                        vus.vip_vaildate = DateTime.Now.AddMonths(1);
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "0.01")
                    {
                        vus.vip_vaildate = DateTime.Now.AddMonths(1);
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "84.00")
                    {
                        vus.vip_vaildate = DateTime.Now.AddMonths(3);
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "300.00")
                    {
                        vus.vip_vaildate = DateTime.Now.AddYears(1);
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "500.00")
                    {
                        vus.vip_vaildate = DateTime.Now.AddYears(2);
                    }

                    result = DataAccess.VipStatusUser_Insert(vus) > 0;
                }

                #region send ms
                if (result)
                {
                    CacheMarker.SetDateVipStatus(vus);
                    var datestr = "一个月";
                    if (orderInfo.mo_money.ToString("f2") == "30.00")
                    {
                        datestr = "一个月";
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "0.01")
                    {
                        datestr = "一个月";
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "34.00")
                    {
                        datestr = "三个月";
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "300.00")
                    {
                        datestr = "一年";
                    }
                    else if (orderInfo.mo_money.ToString("f2") == "500.00")
                    {
                        datestr = "二年";
                    }
                    string message = string.Format("尊敬的[企业查询宝]用户, 您的帐号{0}充值{1}会员成功。", orderInfo.mo_ordername, datestr);
                    ShortMsg_Proxy.ShortMsg_Send("企业查询宝", "企业查询宝充值会员", vus.vip_mobile, message);
                }
                #endregion
            }
            return result;
            #endregion
        }

        public static string Vip_Order_AliPayNotify(string form)
        {
            AlipayReturnData notifyData = AlipayHandler.GetNotifyData(form);
            if (notifyData.trade_status == "TRADE_SUCCESS" || notifyData.trade_status == "TRADE_FINISHED")
            {
                LogHelper.Info("异步通知验证支付宝端订单是否存在成功");
                AlipayTradeQueryResponse result = AlipayHandler.alipay_trade_query(notifyData.out_trade_no, notifyData.trade_no);
                if (result.Code.Equals("10000") && (result.TradeStatus.Equals("TRADE_SUCCESS") || result.TradeStatus.Equals("TRADE_FINISHED")))
                {
                    if (AlipayHandler.ValidationApp_id(notifyData.app_id) && notifyData.buyer_id == result.BuyerUserId)
                    {
                        LogHelper.Info("异步通知验证appid是否一致成功");
                        LogHelper.Info("异步通知验证seller_id是否一致成功");

                        var orderInfo = DataAccess.VipUserOrder_Selectbymo_orderid(notifyData.out_trade_no);
                        if (orderInfo == null)
                        {
                            return "FAILED";
                        }
                        LogHelper.Info("异步通知验证数据库订单是否存在成功");
                        if (UpdatePayState(notifyData.trade_no, orderInfo))
                        {
                            return "SUCCESS";
                        }
                        else
                        {
                            return "FAILED";
                        }
                    }
                }
            }

            return "FAILED";
        }

        //public static Resp_Binary Company_Query_VipExport(this Company com)
        //{
        //    if (!com.email.Email_Get())
        //        return new Resp_Binary { remark = "邮箱格式不正确", status = false };
        //    if (!com.phone.Phone_Get())
        //        return new Resp_Binary { remark = "手机格式不正确", status = false };
        //    if (com.end - com.start > 500)
        //        return new Resp_Binary { remark = "超出最大限制5000", status = false };

        //}
        #endregion
    }
}