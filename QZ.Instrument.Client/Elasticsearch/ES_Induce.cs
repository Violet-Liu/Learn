/*
 * This class is used to induce a returned search-response from ES to a business entity(such as brand, patent, etc.).
 * Besides the documents, highlighted hits and aggregations are also integrated into those business entities.
 * 
 * Sha Jianjian
 * 2016-11-11
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
namespace QZ.Instrument.Client
{
    public class ES_Induce
    {
        /// <summary>
        /// Induce to documents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hits"></param>
        /// <returns></returns>
        private static IList<ES_Doc<T>> Doc_Induce<T>(IEnumerable<IHit<T>> hits) where T : class =>
            hits.Select(h => new ES_Doc<T>() { doc = h.Source, hits = h.Highlights.ToDictionary(hl => hl.Key, hl => hl.Value.Highlights.FirstOrDefault()) }).ToList();

        /// <summary>
        /// Induce keyed buckets
        /// </summary>
        /// <param name="items"></param>
        /// <param name="combine">whether to combine</param>
        /// <returns></returns>
        private static IList<Agg> Agg_Key_Induce(IEnumerable<IBucket> items)
        {
            var list = new List<Agg>();
            foreach (var item in items)
            {
                var pair = (KeyedBucket)item;
                list.Add(new Agg(pair.Key, pair.DocCount ?? 0));
            }
            return list;
        }

        /// <summary>
        /// Induce date buckets
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private static IList<Agg> Agg_Date_Induce(IEnumerable<IBucket> items)
        {
            var list = new List<Agg>();
            foreach (var item in items)
            {
                var pair = (DateHistogramBucket)item;
                list.Add(new Agg(pair.Date.Year.ToString(), pair.DocCount));
            }
            return list;
        }


        /// <summary>
        /// Induce the obscure search response that returned from ES into a gentel and straightforward business entity
        /// </summary>
        /// <typeparam name="T">ES model type</typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        public static ES_Outcome<T> Induce<T>(ISearchResponse<T> r) where T : class
        {
            var outcome = new ES_Outcome<T>();

            outcome.docs = Doc_Induce(r.Hits);
            outcome.total = r.Total;
            if(r.Aggregations.Count > 0)
            {
                outcome.aggs = new Aggs();
                foreach(var agg in r.Aggregations)
                {
                    var items = ((BucketAggregate)agg.Value).Items;
                    switch (agg.Key)
                    {
                        case "date.int":
                            outcome.aggs.date = Agg_Key_Induce(items);
                            break;
                        case "area":
                            outcome.aggs.area = Agg_Key_Induce(items);
                            break;
                        case "date":
                            outcome.aggs.date = Agg_Date_Induce(items);
                            break;
                        case "status":
                            outcome.aggs.status = Agg_Key_Induce(items);
                            break;
                        case "type":
                            outcome.aggs.type = Agg_Key_Induce(items);
                            break;
                    }
                }
            }
            return outcome;
        }
    }

}
