/*
 * Tempelated file which provides methods to handle the next generation's ES searching response.
 * Note: this file is used to do test, and may be deleted later.
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using QZ.Instrument.Model;
namespace QZ.Instrument.Client.Elasticsearch
{
    public class ESResponseHandlerTemp
    {
        /// <summary>
        /// Modify this method to adapt other kind of situation
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Resp_Dishonest_List Dishonest_Handle(ISearchResponse<ES_Dishonest> sr)
        {
            var resp = new Resp_Dishonest_List() { dishonest_list = new List<Dishonest_Abs>(), count = sr.Total, cost = (sr.Took / 1000.0).ToString() };
            var trade_dict = new Dictionary<string, long>();
            var hits = sr.Hits;

            foreach (var hit in hits)
            {
                var r = new Dishonest_Abs();
                var c = hit.Source;
                if (c != null)
                {
                    r.sx_areaname = c.sx_areaname;
                    r.sx_cardnum = c.sx_cardnum;
                    r.sx_casecode = c.sx_casecode;
                    r.sx_court = c.sx_court;
                    r.sx_disrupt = c.sx_disrupt;
                    r.sx_entity = c.sx_entity;
                    r.sx_gistid = c.sx_gistid;
                    r.sx_id = c.sx_id;
                    r.sx_oc_name = c.sx_oc_name;
                    r.sx_performance = c.sx_performance;
                    r.sx_pname = c.sx_pname;
                    r.sx_pubdate = c.sx_pubdate;
                    r.sx_regdate = c.sx_regdate;
                }

                var hl = hit.Highlights;
                foreach (var pair in hl)
                {
                    r.hits.Add(pair.Key, pair.Value.Highlights.FirstOrDefault());
                }
                resp.dishonest_list.Add(r);
            }

            //if (com.pg_index == 1)
            //    ExpUtil.Observation_Insert(ESClient.FunctionScript, Es_Consts.Company_Index + "." + Es_Consts.Company_Type, com.oc_name, scores, weights);

            #region aggregation
            if (sr.Aggregations.Count > 0)
            {
                resp.aggs = new Dishonest_Agg();
                foreach (var agg in sr.Aggregations)
                {
                    var items = ((BucketAggregate)agg.Value).Items;
                    switch (agg.Key)
                    {
                        case "area":
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;

                                resp.aggs.areas.Add(new Agg_Monad(pair.Key, pair.Key, pair.DocCount ?? 0));
                            }
                            break;
                        case "date":
                            foreach (var item in items)
                            {
                                var pair = (DateHistogramBucket)item;
                                resp.aggs.dates.Add(new Agg_Monad($"{pair.Date.Year}({pair.DocCount})", pair.Date.ToString(), pair.DocCount));
                            }
                            break;
                        case "performance":
                            foreach (var item in items)
                            {
                                var pair = (KeyedBucket)item;
                                resp.aggs.performances.Add(new Agg_Monad(pair.Key, pair.Key, pair.DocCount ?? 0));
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
            return resp;
        }
    }

    public class Resp_Dishonest_List
    {
        /// <summary>
        /// list of dishonest infos
        /// </summary>
        public List<Dishonest_Abs> dishonest_list { get; set; }
        /// <summary>
        /// count of all related document
        /// </summary>
        public long count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dishonest_Agg aggs { get; set; }
        
        /// <summary>
        /// Time cost of current search(with unit of second)
        /// </summary>
        public string cost { get; set; }
    }

    public class Dishonest_Abs : ES_Dishonest
    {
        /// <summary>
        /// Highlight hits
        /// Key -> field name; Value -> field value with highlight html tag
        /// </summary>
        public Dictionary<string, string> hits { get; set; } = new Dictionary<string, string>();
    }

    public class Dishonest_Agg
    {
        /// <summary>
        /// 按履行状态统计
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
