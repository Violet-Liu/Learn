/**
 * This class providers many kinds of functions which transform many kinds of internal result types to the last service response, something like `Adaptor`
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.Client;

namespace QZ.Service.Enterprise
{
    public class ResponseAdaptor
    {
        public static Resp_Company_List Search2CompanyList(ISearchResponse<ES_Company> sr, Company com)
        {
            var company_list = new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = sr.Total, cost = (sr.Took/1000.0).ToString() };
            var trade_dict = new Dictionary<string, long>();
            var hits = sr.Hits;
            var weights = new double[com.pg_size];
            var scores = new double[com.pg_size];
            int i = 0;
            foreach (var hit in hits)
            {
                var r = new Resp_Oc_Abs();
                var c = hit.Source;
                if (c != null)
                {
                    r.flag = c.od_createtime.Year != 1900;
                    r.oc_addr = c.oc_address ?? "";
                    r.oc_area = c.oc_area;
                    r.oc_code = c.oc_code;
                    r.oc_art_person = c.od_faren ?? string.Empty;
                    r.oc_issue_time = c.oc_issuetime.ToString("yyyy-MM-dd") ?? "";
                    r.oc_name = c.oc_name;
                    r.oc_name_hl = c.oc_name;
                    r.oc_reg_capital = c.od_regmoney ?? "";
                    r.oe_status = c.oc_issuetime < DateTime.Now;
                    r.oc_type = c.oc_companytype ?? "";
                    r.oc_status = Datas.CompanyStatus.ContainsKey(c.oc_status) ? Datas.CompanyStatus[c.oc_status] : "未知";
                    scores[i] = hit.Score;
                    weights[i++] = c.oc_weight;
                }

                r.gb_trades = new List<string>(10);
                foreach (var code in c.gb_codes)
                {
                    var name = GBName_Get(c.gb_cat, code);
                    if (name != "")
                        r.gb_trades.Add(name);
                }
                
                var hl = hit.Highlights;
                foreach (var pair in hl)
                {
                    r.hits.Add(pair.Key, pair.Value.Highlights.FirstOrDefault());
                }
                company_list.oc_list.Add(r);
            }

            //if (com.pg_index == 1)
            //    ExpUtil.Observation_Insert(ESClient.FunctionScript, Es_Consts.Company_Index + "." + Es_Consts.Company_Type, com.oc_name, scores, weights);

            #region aggregation
            if (sr.Aggregations.Count > 0)
            {
                company_list.aggs = new Company_Agg();
                foreach (var agg in sr.Aggregations)
                {
                    var items = ((BucketAggregate)agg.Value).Items;
                    switch (agg.Key)
                    {
                        case "area":  
                            foreach(var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                if (Constants.AreaMap.ContainsKey(pair.Key))
                                    company_list.aggs.areas.Add(new Agg_Monad($"{Constants.AreaMap[pair.Key]}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                        case "date":
                            foreach(var item in items)
                            {
                                var pair = (DateHistogramBucket)item;
                                company_list.aggs.dates.Add(new Agg_Monad($"{pair.Date.Year}({pair.DocCount})", pair.Date.ToString(), pair.DocCount));
                            }
                            break;
                        case "regm":
                            foreach (var item in items)
                            {
                                var pair = (RangeBucket)item;
                                company_list.aggs.regms.Add(RangeBucket2AggMonad(pair));
                            }
                            break;
                        case "status":
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                company_list.aggs.statuses.Add(new Agg_Monad($"{Constants.CompanyStatusMap[pair.Key]}({pair.DocCount ?? 0})", pair.Key, pair.DocCount??0));
                            }
                            break;
                        case "cat":  // 行业分类
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                if (!string.IsNullOrEmpty(pair.Key))
                                    company_list.aggs.trades.Add(new Agg_Monad($"{Instrument.Model.Constants.Primary_Trades[pair.Key]}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                        case "type":
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                company_list.aggs.types.Add(new Agg_Monad($"{pair.Key}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                    }
                }
            }
            #endregion
            //if (sr.Aggregations.ContainsKey("1"))
            //{
            //    var agg = (BucketAggregate)sr.Aggregations["fst"];

            //    foreach (var i in agg.Items)
            //    {
            //        var pair = (KeyedBucket)i;
            //        type_dict.Add(pair.Key, pair.DocCount ?? 0);
            //    }
            //    //count = type_dict.Sum(di => di.Value);
            //}
            //DataAccess.ErrorLog_Insert(Constructor.Create_TestLog(company_list.aggs.areas.FirstOrDefault().ToJson(), com.ToJson(), com.u_id + com.u_name));
            return company_list;
        }

        public static string GBName_Get(string gb_cat, string gb_code)
        {
            var trade = Datas.Trades.FirstOrDefault(t => t.code == gb_cat);
            if (trade != null)
            {
                string name = trade.name;
                var lvl = gb_code.Length;
                if(lvl >= 2)
                {
                    var prefix = gb_code.Substring(0, 2);
                    var trade2 = trade.trades.FirstOrDefault(t => t.code == prefix);
                    if(trade2 != null && lvl >= 3)
                    {
                        name = trade2.name;
                        var prefix3 = gb_code.Substring(0, 3);
                        var trade3 = trade2.trades.FirstOrDefault(t => t.code == prefix3);
                        if(lvl >= 4 && trade3 != null)
                        {
                            name = trade3.name;
                            var trade4 = trade3.trades.First(t => t.code == gb_code);
                            if (trade4 != null)
                                name = trade4.name;
                        }
                    }
                }
                return name;
            }
            return "";
        }

        private static Agg_Monad RangeBucket2AggMonad(RangeBucket r)
        {
            var a = new Agg_Monad();
            string l = null;
            if (r.From != null)
            {
                if (r.From > 1000)
                    l = "1000万元以上";
                else if (r.From > 500)
                    l = "500-1000万元";
                else if (r.From > 50)
                    l = "50-500万元";
                else if (r.From > 10)
                    l = "10-50万元";
            }
            else
                l = "10万元以下";
            a.label = l;
            a.count = r.DocCount;
            a.value = r.Key;
            return a;
        }

        public static Resp_Company_List TradeSearch2CompanyList(ISearchResponse<ES_Company> sr)
        {
            var company_list = new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = sr.Total };
            foreach(var c in sr.Documents)
            {
                var r = new Resp_Oc_Abs();
                if (c != null)
                {
                    r.flag = c.od_createtime.Year != 1900;
                    r.oc_addr = c.oc_address ?? "";
                    r.oc_area = c.oc_area;
                    r.oc_code = c.oc_code;
                    r.oc_art_person = c.od_faren ?? string.Empty;
                    r.oc_issue_time = c.oc_issuetime.ToString("yyyy-MM-dd") ?? "";
                    r.oc_name = c.oc_name;
                    r.oc_name_hl = c.oc_name;
                    r.oc_reg_capital = c.od_regmoney ?? "";
                    r.oe_status = c.oc_issuetime < DateTime.Now;
                    r.oc_type = c.oc_companytype ?? "";
                    r.oc_status = Datas.CompanyStatus.ContainsKey(c.oc_status) ? Datas.CompanyStatus[c.oc_status] : "未知";
                }
                company_list.oc_list.Add(r);
            }
            return company_list;
        }

        public static Resp_Exhibit_List ExhibitSearch2List(ISearchResponse<ES_Exhibit> sr)
        {
            var exhibit_list = new Resp_Exhibit_List() { count = sr.Total, exhibits = new List<ES_Exhibit>(sr.Documents.Count()), cost = (sr.Took / 1000.0).ToString() };
            foreach (var hit in sr.Hits)
            {
                var c = hit.Source;

                if (c != null)
                {
                    var hl = hit.Highlights;
                    foreach (var pair in hl)
                    {
                        c.hits.Add(pair.Key, pair.Value.Highlights.FirstOrDefault());
                    }
                    exhibit_list.exhibits.Add(c);
                }
            }
            // 统计
            if (sr.Aggregations.Count > 0)
            {
                exhibit_list.aggs = new Exhibit_Agg();
                foreach (var agg in sr.Aggregations)
                {
                    var items = ((BucketAggregate)agg.Value).Items;
                    switch (agg.Key)
                    {
                        case "trade":  // 行业分类
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                if (!string.IsNullOrEmpty(pair.Key))
                                    exhibit_list.aggs.trades.Add(new Agg_Monad($"{pair.Key}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                        case "date":  // 行业分类
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                if (!string.IsNullOrEmpty(pair.Key))
                                    exhibit_list.aggs.dates.Add(new Agg_Monad($"{pair.Key}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                        case "province":  // 行业分类
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                if (!string.IsNullOrEmpty(pair.Key))
                                    exhibit_list.aggs.provinces.Add(new Agg_Monad($"{pair.Key}({pair.DocCount ?? 0})", pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                    }
                }
            }
            return exhibit_list;
        }
    }
}