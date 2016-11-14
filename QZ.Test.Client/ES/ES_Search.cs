using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;

using QZ.Instrument.Model;
using QZ.Instrument.Client;
using QZ.Instrument.Utility;

namespace QZ.Test.Client
{
    public class ES_Search
    {
        private static ES_Outcome<ES_Judge> Dishonest_PTest(string key, int pg_size, int pg_index = 0) =>
            pg_index == 0
            ? ES_Induce.Induce(ES_Client.Judge_GSearch(key, pg_size))
            : ES_Induce.Induce(ES_Client.Judge_GSearch(key, pg_index, pg_size));

        public static void Dishonest_Test(string key, int pg_size, int pg_index = 0)
        {
            var outcom = Dishonest_PTest(key, pg_size, pg_index);
        }

        public static IEnumerable<ES_Company> ScrollSearch()
        {
            var set = ESClient.Company_Search4Export("腾讯", 0);
            var total = set.Values.Count();
            return set.Values;
        }

        public static IEnumerable<ES_Company> SpanFirst_Test(string keyword) => ESClient.SpanFirst_Test(keyword).Documents;

        public static IEnumerable<ES_Company> SpanTerm_Test(string keyword) => ESClient.SpanTerm_Test(keyword).Documents;

        public static IEnumerable<ES_Company> SpanNear_Test(string keyword) => ESClient.SpanNear_Test(keyword).Documents;
        public static IEnumerable<ES_Company> SpanNot_Test(string keyword) => ESClient.SpanNot_Test(keyword).Documents;

        public static IEnumerable<ES_Company> Regexp_Test(string keyword) => ESClient.Regexp_Test(keyword).Documents;
        public static void Company_GeneraiFilterByTrade()
        {
            var resp = ESClient.Company_FilterByTrade_Search("bytrade", "65", "上海元实信息技术有限公司");
        }


        //public static IEnumerable<ES_Company> Advanced_Query()
        //{
        //    var com = new Company() { oc_code = "111111", oc_sort = oc_sort.oc_reg_capital };
        //    var status = 0;
        //    double floor = 0, ceiling = 0;

        //    DateTime start = DateTime.MinValue, end = DateTime.MinValue;
        //    if (!string.IsNullOrEmpty(com.year) && DateTime.TryParse(com.year, out start))
        //    {
        //        end = start.AddYears(1);
        //    }
        //    var s = new SearchDescriptor<ES_Company>();
        //    var qcs = new List<QueryContainer>();
        //    var qcd = new QueryContainerDescriptor<ES_Company>();
        //    //com.ToMaybe()
        //    //   .DoWhen(q => start > DateTime.MinValue, q => qcs.Add(qcd.DateRange(d => d.Field("oc_issuetime").GreaterThanOrEquals(start).LessThan(end))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("00"), q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_type), q => qcs.Add(qcd.Term(p => p.Field("oc_companytype").Value(q.oc_type))))
        //    //   .DoWhen(q => status >= 0, q => qcs.Add(qcd.Term(t => t.Field("oc_status").Value(status))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_trade), q => qcs.Add(q.oc_trade.Length == 1 ? qcd.Term(t => t.Field("gb_cat").Value(q.oc_trade)) : qcd.Prefix(qp => qp.Field("gb_codes").Value(q.oc_trade))))
        //    //   .DoWhen(q => floor > 0 || ceiling > 0,
        //    //                       q => qcs.Add(qcd.Range(p => p.Field("od_regm").GreaterThan(floor).LessThanOrEquals(ceiling <= 0 ? Int32.MaxValue : ceiling))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_code), q => qcs.Add(qcd.Term(t => t.Field("oc_code").Value(com.oc_code))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_number), q => qcs.Add(qcd.Term(t => t.Field("oc_number").Value(com.oc_number))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_art_person), q => qcs.Add(qcd.MatchPhrase(p => p.Field("od_faren").Query(q.oc_art_person))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_stock_holder), q => qcs.Add(qcd.Match(p => p.Field("od_gds").Query(q.oc_stock_holder))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_site), q => qcs.Add(qcd.MatchPhrase(m => m.Field("oc_sites").Query(com.oc_name).Strict())))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_member), q => qcs.Add(qcd.Match(p => p.Field("oc_members").Query(q.oc_member))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_name), q => qcs.Add(qcd.Term(p => p.Field("oc_name.oc_name_raw").Value(q.oc_name)) |
        //    //                                                               qcd.Prefix(m => m.Field("oc_name.py_oc_name").Value(q.oc_name)) |
        //    //                                                               qcd.MatchPhrase(p => p.Field("oc_name").Query(q.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_addr), q => qcs.Add(qcd.Match(p => p.Field("oc_address").Query(q.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_business), q => qcs.Add(qcd.Match(p => p.Field("od_bussiness").Query(q.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_type), q => qcs.Add(qcd.Match(p => p.Field("od_regtype").Query(q.oc_reg_type).Slop(1).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.08d))))

        //    //   //.DoWhen(q => q.oc_status >= 0, q => qcs.Add(qcd.Term(t => t.Field("oc_status").Value(q.oc_status))))
        //    //   //.DoWhen(q => !string.IsNullOrEmpty(q.oc_trade), q => qcs.Add(qcd.Nested(nest => nest.Name("trade").Query(query => query.Prefix(qp => qp.Field("trades.gb_code").Value(q.oc_trade))))))
        //    //   .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_capital_floor) || !string.IsNullOrEmpty(q.oc_reg_capital_ceiling),
        //    //                       q => qcs.Add(qcd.Range(p => p.Field("od_regm").GreaterThanOrEquals(string.IsNullOrEmpty(com.oc_reg_capital_floor) ? 0 : Convert.ToDouble(com.oc_reg_capital_floor))
        //    //                                              .LessThanOrEquals(string.IsNullOrEmpty(com.oc_reg_capital_ceiling) ? Int32.MaxValue : Convert.ToDouble(com.oc_reg_capital_ceiling)))));

        //    s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From(0).Take(100)
        //        .Query(qq => qq
        //            .FunctionScore(c => c
        //                .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score"))))
        //                .Query(q => q.Range(r => r.Field("oc_weight").GreaterThan(1000))))

        //                    //hl
        //                    //.PreTags("<font color=\"red\">")
        //                    //.PostTags("</font>")
        //                    //.Fields(f => f.Field("oc_code"),
        //                    //        f => f.Field("od_faren"),
        //                    //        f => f.Field("od_gds"),
        //                    //        f => f.Field("oc_name"),
        //                    //        f => f.Field("oc_sites"),
        //                    //        f => f.Field("oc_address"),
        //                    //        f => f.Field("oc_members"),
        //                    //        f => f.Field("od_regtype")
        //                    );

        //    s.Sort(st => st.Field(sfd => sfd.Field("oc_weight").Descending()));

        //    var response = Client_Get().Search<ES_Company>(s);
        //    return response.Documents;
        //}
        public static void Company_NestByTrade()
        {
            var resp = ElasticsearchClient.CompanyTrade_Query("精密铝合金结构制造", "昌达铸造");
        }

        public static void Company_Script_Search() => ESClient.Script_Search();

        //    private static QueryContainer Dishonest_AsciiSearch(string keyword, QueryContainerDescriptor<ES_Dishonest> q) =>
        //        q.Term(t => t.Field("sx_cardnum").Value(keyword));

        //    private static QueryContainer Dishonest_UnicodeSearch(string keyword, QueryContainerDescriptor<ES_Dishonest> q) =>
        //        q.Term(t => t.Field("sx_entity").Value(keyword).Boost(5))
        //        | q.Term(t => t.Field("sx_pname").Value(keyword).Boost(5))
        //        | q.Term(t => t.Field("sx_areaname").Value(keyword).Boost(5))
        //        | q.Term(t => t.Field("sx_oc_name.name_raw").Value(keyword).Boost(10))
        //        | (keyword.Length < 5
        //            ? q.MatchPhrase(m => m.Field("sx_oc_name").Query(keyword).Analyzer("urldelimit"))
        //            : q.MatchPhrase(m => m.Field("sx_oc_name").Query(keyword).Slop(0).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).CutoffFrequency(0.001).Boost(3)))
        //        | q.Match(m => m.Field("sx_oc_name").Query(keyword).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(80)).Boost(0.5));

        //    private static HighlightDescriptor<ES_Dishonest> Dishonest_AsciiSearch_HL_Compose(HighlightDescriptor<ES_Dishonest> hl) => hl
        //        .PreTags("<font color=\"red\">")
        //        .PostTags("</font>")
        //        .Fields(f => f.Field("sx_cardnum"),
        //                f => f.Field("sx_oc_name.py_name")
        //                );

        //    private static HighlightDescriptor<ES_Dishonest> Dishonest_UnicodeSearch_HL_Compose(HighlightDescriptor<ES_Dishonest> hl) => hl
        //        .PreTags("<font color=\"red\">")
        //        .PostTags("</font>")
        //        .Fields(f => f.Field("sx_entity"),
        //                f => f.Field("sx_pname"),
        //                f => f.Field("sx_areaname"),
        //                f => f.Field("sx_oc_name")
        //                );

        //    public static ISearchResponse<ES_Dishonest> Dishonest_Search(Req_Info_Query query)
        //    {
        //        bool isAscii = !query.query_str.Any(c => c > 127);
        //        var s = new SearchDescriptor<ES_Dishonest>();
        //        s.Index(Es_Consts.Company_Ext_Type).Type(Es_Consts.Dishonest_Type).From((query.pg_index - 1) * query.pg_size).Take(query.pg_size)
        //            .Query(q => isAscii
        //                ? (q.Term(t => t.Field("sx_cardnum").Value(query.query_str)) | q.MatchPhrasePrefix(mp => mp.Field("sx_oc_name.py_name").Query(query.query_str)))
        //                : Dishonest_UnicodeSearch(query.query_str, q))
        //            //.Explain()
        //            .Highlight(hl => isAscii ? Dishonest_AsciiSearch_HL_Compose(hl) : Dishonest_UnicodeSearch_HL_Compose(hl))
        //            ;
        //        if (query.pg_index == 1)
        //        {
        //            s.Aggregations(agg => agg.DateHistogram("date", t => t.Field("sx_pubdate").Interval(DateInterval.Year).MinimumDocumentCount(1))
        //                                    .Terms("performance", t => t.Field("sx_performance"))
        //                                    .Terms("area", t => t.Field("sx_areaname").Size(32))
        //                                    );

        //        }
        //        var response = Client_Get().Search<ES_Dishonest>(ss => s);
        //        return response;
        //    }

        //    public static Resp_Dishonest_List Dishonest_SearchHandle(Req_Info_Query query) =>
        //        Dishonest_Handle(Dishonest_Search(query));

        //    public static Resp_Dishonest_List Dishonest_Handle(ISearchResponse<ES_Dishonest> sr)
        //    {
        //        var resp = new Resp_Dishonest_List() { dishonest_list = new List<Dishonest_Abs>(), count = sr.Total, cost = (sr.Took / 1000.0).ToString() };
        //        var trade_dict = new Dictionary<string, long>();
        //        var hits = sr.Hits;

        //        foreach (var hit in hits)
        //        {
        //            var r = new Dishonest_Abs();
        //            var c = hit.Source;
        //            if (c != null)
        //            {
        //                r.sx_areaname = c.sx_areaname;
        //                r.sx_cardnum = c.sx_cardnum;
        //                r.sx_casecode = c.sx_casecode;
        //                r.sx_court = c.sx_court;
        //                r.sx_disrupt = c.sx_disrupt;
        //                r.sx_entity = c.sx_entity;
        //                r.sx_gistid = c.sx_gistid;
        //                r.sx_id = c.sx_id;
        //                r.sx_oc_name = c.sx_oc_name;
        //                r.sx_performance = c.sx_performance;
        //                r.sx_pname = c.sx_pname;
        //                r.sx_pubdate = c.sx_pubdate;
        //                r.sx_regdate = c.sx_regdate;
        //            }

        //            var hl = hit.Highlights;
        //            foreach (var pair in hl)
        //            {
        //                r.hits.Add(pair.Key, pair.Value.Highlights.FirstOrDefault());
        //            }
        //            resp.dishonest_list.Add(r);
        //        }

        //        //if (com.pg_index == 1)
        //        //    ExpUtil.Observation_Insert(ESClient.FunctionScript, Es_Consts.Company_Index + "." + Es_Consts.Company_Type, com.oc_name, scores, weights);

        //        #region aggregation
        //        if (sr.Aggregations.Count > 0)
        //        {
        //            resp.aggs = new Dishonest_Agg();
        //            foreach (var agg in sr.Aggregations)
        //            {
        //                var items = ((BucketAggregate)agg.Value).Items;
        //                switch (agg.Key)
        //                {
        //                    case "area":
        //                        foreach (var item in items)
        //                        {
        //                            var pair = (KeyedBucket)item;

        //                            resp.aggs.areas.Add(new Agg_Monad(pair.Key, pair.Key, pair.DocCount ?? 0));
        //                        }
        //                        break;
        //                    case "date":
        //                        foreach (var item in items)
        //                        {
        //                            var pair = (DateHistogramBucket)item;
        //                            resp.aggs.dates.Add(new Agg_Monad($"{pair.Date.Year}({pair.DocCount})", pair.Date.ToString(), pair.DocCount));
        //                        }
        //                        break;
        //                    case "performance":
        //                        foreach (var item in items)
        //                        {
        //                            var pair = (KeyedBucket)item;
        //                            resp.aggs.performances.Add(new Agg_Monad(pair.Key, pair.Key, pair.DocCount ?? 0));
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //        #endregion
        //        //if (sr.Aggregations.ContainsKey("1"))
        //        //{
        //        //    var agg = (BucketAggregate)sr.Aggregations["fst"];

        //        //    foreach (var i in agg.Items)
        //        //    {
        //        //        var pair = (KeyedBucket)i;
        //        //        type_dict.Add(pair.Key, pair.DocCount ?? 0);
        //        //    }
        //        //    //count = type_dict.Sum(di => di.Value);
        //        //}
        //        //DataAccess.ErrorLog_Insert(Constructor.Create_TestLog(company_list.aggs.areas.FirstOrDefault().ToJson(), com.ToJson(), com.u_id + com.u_name));
        //        return resp;
        //    }

        //    public static ElasticClient Client_Get() => Get_Client(new string[] { "http://192.168.10.1:9200",
        //  "http://192.168.10.10:9200",
        //  "http://192.168.10.11:9200",
        //  "http://192.168.10.12:9200",
        //  "http://192.168.10.13:9200" });

        //    public static ElasticClient Get_Client(string[] uris)
        //    {
        //        if (uris.Length < 2)
        //            return new ElasticClient(new ConnectionSettings(new Uri(uris[0])));

        //        return new ElasticClient(new ConnectionSettings(new StaticConnectionPool(uris.Select(u => new Uri(u)))));
        //    }
        //}

        //public class Resp_Dishonest_List
        //{
        //    /// <summary>
        //    /// list of dishonest infos
        //    /// </summary>
        //    public List<Dishonest_Abs> dishonest_list { get; set; }
        //    /// <summary>
        //    /// count of all related document
        //    /// </summary>
        //    public long count { get; set; }

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    public Dishonest_Agg aggs { get; set; }

        //    /// <summary>
        //    /// Time cost of current search(with unit of second)
        //    /// </summary>
        //    public string cost { get; set; }
        //}

        //public class Dishonest_Abs : ES_Dishonest
        //{
        //    /// <summary>
        //    /// Highlight hits
        //    /// Key -> field name; Value -> field value with highlight html tag
        //    /// </summary>
        //    public Dictionary<string, string> hits { get; set; } = new Dictionary<string, string>();
        //}

        public class Dishonest_Agg
        {
            /// <summary>
            /// 按企业状态统计
            /// </summary>
            public List<Agg_Monad> performances { get; set; } = new List<Agg_Monad>();

            /// <summary>
            /// 按发布日期统计
            /// </summary>
            public List<Agg_Monad> dates { get; set; } = new List<Agg_Monad>();
            /// <summary>
            /// 按地区统计
            /// </summary>
            public List<Agg_Monad> areas { get; set; } = new List<Agg_Monad>();
        }
    }
}
