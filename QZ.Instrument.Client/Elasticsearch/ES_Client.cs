/*
 * Client of ES-Search, which provides many useful methods to search infos, such as brands, patents and so on.
 * Please refer to each method comment for detailed usage
 * 
 * Notations: G -> 'General', F -> 'Filter', U -> 'Unicode', A -> 'Ascii', Ini -> 'Search with Aggregation', NonIni -> 'Search without Aggregation'
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
using Elasticsearch.Net;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;
using QZ.Foundation.Monad;
using QZ.Instrument.Global;

namespace QZ.Instrument.Client
{
    public class ES_Client
    {
        #region get ES client
        private static string[] uris = new string[] { "http://192.168.10.1:9200",
          "http://192.168.10.10:9200",
          "http://192.168.10.11:9200",
          "http://192.168.10.12:9200",
          "http://192.168.10.13:9200" };

        ///<summary>Since there are more than one ES node, why not use <seealso cref="StaticConnectionPool"/> directly?</summary>
        public static ElasticClient Client_Get() => new ElasticClient(new ConnectionSettings(new StaticConnectionPool(uris.Select(u => new Uri(u))/*DataBus.ES_5_0_Uris*/)));
        //public static ElasticClient Client_Get() => new ElasticClient(new ConnectionSettings(new StaticConnectionPool(DataBus.ES_5_0_Uris)));
        #endregion

        #region auxiliary methods
        /// <summary>
        /// Create a unicode-query function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<T>, QueryContainer> UQuery_Create<T>(string keyword, IList<ES_Field> fields) where T : class =>
            q =>
                fields.Select(
                    f => f.ana == Analyzer.Term
                    ? q.Term(t => t.Field(f.name).Value(keyword).Boost(f.boost))
                    : (q.MatchPhrase(m => m.Field(f.name).Query(keyword).Boost(f.boost).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.001).Slop(2))
                       | q.Match(m => m.Field(f.name).Query(keyword).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90))))
                ).Aggregate((a, b) => a | b);

        /// <summary>
        /// Create a ascii-query function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<T>, QueryContainer> AQuery_Create<T>(string keyword, IList<ES_Field> fields) where T : class =>
            q => fields.Select(f => f.ana == Analyzer.Term
                ? q.Term(t => t.Field(f.name).Value(keyword))
                : (f.ana == Analyzer.Standard
                    ? q.Prefix(p => p.Field(f.name).Value(keyword).Boost(f.boost))
                    : q.MatchPhrase(m => m.Field(f.name).Query(keyword).Boost(f.boost)))).Aggregate((a, b) => a | b);

        /// <summary>
        /// Create a query-with-filter function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <param name="qfilters"></param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<T>, QueryContainer> FQuery_Create<T>(string keyword, IList<ES_Field> fields, IEnumerable<QueryContainer> qfilters) where T : class =>
            q => UQuery_Create<T>(keyword, fields)(q) & qfilters.Aggregate((a, b) => a & b);

        /// <summary>
        /// Search templat, services for many kinds of searching, such as brand, patent, and so on.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pg_size"></param>
        /// <param name="des"></param>
        /// <param name="qfun"></param>
        /// <param name="pg_index"></param>
        /// <returns></returns>
        private static ISearchResponse<T> Search_Template<T>(int pg_size, SearchDescriptor<T> des, Func<QueryContainerDescriptor<T>, QueryContainer> qfun, int pg_index = 0) where T : class =>
            Client_Get().Search<T>(des.From(pg_index * pg_size).Take(pg_size).Query(qfun));

        /// <summary>
        /// Filter search template, services for many kinds of searching, such as brand, patent, and so on.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pg_index"></param>
        /// <param name="pg_size"></param>
        /// <param name="des"></param>
        /// <param name="keyfun">function to create key-search-querycontainer</param>
        /// <param name="filterfun">function to create filter-querycontainer</param>
        /// <returns></returns>
        private static ISearchResponse<T> FSearch_Template<T>(int pg_index, int pg_size, SearchDescriptor<T> des,
            Func<QueryContainerDescriptor<T>, QueryContainer> keyfun, Func<QueryContainerDescriptor<T>, QueryContainer> filterfun) where T : class =>
            Client_Get().Search<T>(des.From(pg_index * pg_size).Take(pg_size).Query(q => keyfun(q) & filterfun(q)));
        #endregion

        #region company
        //public static ISearchResponse<ES_Company> Company_GSearch(string keyword, int pg_size) =>
        //    keyword.Any(c => c > 127)
        //    ? 
        #endregion

        #region brand
        /// <summary>
        /// General search from brand.
        /// <list>Note that this method will do aggregation for brand</list>
        /// </summary>
        /// <param name="keyword">keyword of query</param>
        /// <param name="pg_size">page size with a default page index of 0</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Brand> Brand_GSearch(string keyword, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Brand_GU_Ini, UQuery_Create<ES_Brand>(keyword, Brand_Meta.U_Fields))
            : Search_Template(pg_size, ES_Buffer.Brand_GA_Ini, AQuery_Create<ES_Brand>(keyword, Brand_Meta.A_Fields))
            ;

        /// <summary>
        /// General search from brand.
        /// <list>Note that this method will not do aggregation for brand</list>
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pg_index">page index, start from 0</param>
        /// <param name="pg_size">page size</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Brand> Brand_GSearch(string keyword, int pg_index, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Brand_GU_NonIni, UQuery_Create<ES_Brand>(keyword, Brand_Meta.U_Fields), pg_index)
            : Search_Template(pg_size, ES_Buffer.Brand_GA_NonIni, AQuery_Create<ES_Brand>(keyword, Brand_Meta.A_Fields), pg_index)
            ;

        /// <summary>
        /// Filter-searching of brand
        /// </summary>
        /// <param name="keyword">query keyword</param>
        /// <param name="pg_index"></param>
        /// <param name="pg_size"></param>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Brand> Brand_FSearch(string keyword, int pg_size, IDictionary<string, string> dict, int pg_index=0) =>
            keyword.Any(c => c > 127)
            ? FSearch_Template(pg_index, pg_size, ES_Buffer.Brand_GU_NonIni, UQuery_Create<ES_Brand>(keyword, Brand_Meta.U_Fields), Brand_Fill(dict))
            : FSearch_Template(pg_index, pg_size, ES_Buffer.Brand_GA_NonIni, AQuery_Create<ES_Brand>(keyword, Brand_Meta.A_Fields), Brand_Fill(dict))
            ;

        /// <summary>
        /// Fill filter conditions of brand-searching into 'QueryContainer'
        /// </summary>
        /// <remarks>Be aware that keys of this <paramref name="dict"/>must in the value-domain["ob_date", "ob_classno"]</remarks>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<ES_Brand>, QueryContainer> Brand_Fill(IDictionary<string, string> dict) => q =>
            dict.Select(pair =>
                pair.Key == "date"
                ? q.DateRange(d => d.Field("ob_date").GreaterThanOrEquals(pair.Value).LessThan($"{int.Parse(pair.Value) + 1}").Format("yyyy"))
                : q.Term(t => t.Field(string.Format("ob_{0}", pair.Key == "type" ? "classno": pair.Key)).Value(pair.Value))
            ).Aggregate((a, b) => a & b);


        /// <summary>
        /// 根据oc_code得到公司商标
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Brand> Brand_SearchByCode(string oc_code, int pg_size, int pg_index=0) =>
            Search_Template(pg_size, ES_Buffer.Brand_GA_Ini, AQuery_Create<ES_Brand>(oc_code, new List<ES_Field>() { new ES_Field("ob_oc_code") }), pg_index);
        #endregion

        #region patent
        /// <summary>
        /// General search for patent
        /// This method will do aggregation, and usually be invoked at the first time for a specified keyword searching
        /// </summary>
        /// <param name="keyword">keyword of searching</param>
        /// <param name="pg_size">page size, with a default page index of 0</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Patent> Patent_GSearch(string keyword, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Patent_GU_Ini, UQuery_Create<ES_Patent>(keyword, Patent_Meta.U_Fields))
            : Search_Template(pg_size, ES_Buffer.Patent_GA_Ini, AQuery_Create<ES_Patent>(keyword, Patent_Meta.A_Fields))
            ;

        /// <summary>
        /// General search for patent
        /// This method will not do aggregation
        /// </summary>
        /// <param name="keyword">keyword of searching</param>
        /// <param name="pg_index">page index, starting from 0</param>
        /// <param name="pg_size">page size</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Patent> Patent_GSearch(string keyword, int pg_index, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Patent_GU_Ini, UQuery_Create<ES_Patent>(keyword, Patent_Meta.U_Fields), pg_index)
            : Search_Template(pg_size, ES_Buffer.Patent_GA_Ini, AQuery_Create<ES_Patent>(keyword, Patent_Meta.A_Fields), pg_index)
            ;

        /// <summary>
        /// Filter-searching of patent
        /// </summary>
        /// <param name="keyword">query keyword</param>
        /// <param name="pg_index"></param>
        /// <param name="pg_size"></param>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Patent> Patent_FSearch(string keyword, int pg_size, IDictionary<string, string> dict, int pg_index=0) =>
            keyword.Any(c => c > 127)
            ? FSearch_Template(pg_index, pg_size, ES_Buffer.Patent_GU_NonIni, UQuery_Create<ES_Patent>(keyword, Patent_Meta.U_Fields), Patent_Fill(dict))
            : FSearch_Template(pg_index, pg_size, ES_Buffer.Patent_GA_NonIni, AQuery_Create<ES_Patent>(keyword, Patent_Meta.A_Fields), Patent_Fill(dict))
            ;

        /// <summary>
        /// Fill filter conditions of brand-searching into 'QueryContainer'
        /// </summary>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<ES_Patent>, QueryContainer> Patent_Fill(IDictionary<string, string> dict) => q =>
            dict.Select(pair => q.Term(t => t.Field($"patent_{Name_Callibrate(pair.Key)}").Value(pair.Value).Verbatim())
            ).Aggregate((a, b) => a & b);

        private static string Name_Callibrate(string name) => name[0] == 'd' ? "year" : name;

        #endregion

        #region judge
        /// <summary>
        /// General search for judge
        /// This method will do aggregation, and usually be invoked at the first time for a specified keyword searching
        /// </summary>
        /// <param name="keyword">keyword of searching</param>
        /// <param name="pg_size">page size, with a default page index of 0</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Judge> Judge_GSearch(string keyword, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Judge_GU_Ini, UQuery_Create<ES_Judge>(keyword, Judge_Meta.U_Fields))
            : Search_Template(pg_size, ES_Buffer.Judge_GA_Ini, q => q.Term(t => t.Field(Judge_Meta.A_Field.name).Value(keyword)))
            ;

        /// <summary>
        /// General search for judge
        /// This method will not do aggregation
        /// </summary>
        /// <param name="keyword">keyword of searching</param>
        /// <param name="pg_index">page index, starting from 0</param>
        /// <param name="pg_size">page size</param>
        public static ISearchResponse<ES_Judge> Judge_GSearch(string keyword, int pg_index, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Judge_GU_NonIni, UQuery_Create<ES_Judge>(keyword, Judge_Meta.U_Fields), pg_index)
            : Search_Template(pg_size, ES_Buffer.Judge_GA_NonIni, q => q.Term(t => t.Field(Judge_Meta.A_Field.name).Value(keyword)), pg_index)
            ;

        /// <summary>
        /// Filter-searching of judge
        /// </summary>
        /// <param name="keyword">query keyword</param>
        /// <param name="pg_index"></param>
        /// <param name="pg_size"></param>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Judge> Judge_FSearch(string keyword,  int pg_size, IDictionary<string, string> dict, int pg_index=0) =>
            keyword.Any(c => c > 127)
            ? FSearch_Template(pg_index, pg_size, ES_Buffer.Judge_GU_NonIni, UQuery_Create<ES_Judge>(keyword, Judge_Meta.U_Fields), Judge_Fill(dict))
            : FSearch_Template(pg_index, pg_size, ES_Buffer.Judge_GU_NonIni, q => q.Term(t => t.Field(Judge_Meta.A_Field.name).Value(keyword)), Judge_Fill(dict))
            ;

        /// <summary>
        /// Fill filter conditions of judge-searching into 'QueryContainer'
        /// </summary>
        /// <remarks>Be aware that keys of this <paramref name="dict"/>must in the value-domain["ob_date", "ob_classno"]</remarks>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<ES_Judge>, QueryContainer> Judge_Fill(IDictionary<string, string> dict) => q =>
            dict.Select(pair =>
                pair.Key == "date"
                ? q.DateRange(d => d.Field("jd_date").GreaterThanOrEquals(pair.Value).LessThan($"{int.Parse(pair.Value) + 1}").Format("yyyy"))
                : q.Term(t => t.Field("jd_program").Value(pair.Value))
            ).Aggregate((a, b) => a & b);
        #endregion

        #region dishonest
        public static ISearchResponse<ES_Dishonest> Dishonest_GSearch(string keyword, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Dishonest_GU_Ini, UQuery_Create<ES_Dishonest>(keyword, Dishonest_Meta.U_Fields))
            : Search_Template(pg_size, ES_Buffer.Dishonest_GA_Ini, q => q.Term(t => t.Field(Dishonest_Meta.A_Field.name).Value(keyword)))
            ;

        public static ISearchResponse<ES_Dishonest> Dishonest_GSearch(string keyword, int pg_index, int pg_size) =>
            keyword.Any(c => c > 127)
            ? Search_Template(pg_size, ES_Buffer.Dishonest_GU_NonIni, UQuery_Create<ES_Dishonest>(keyword, Dishonest_Meta.U_Fields), pg_index)
            : Search_Template(pg_size, ES_Buffer.Dishonest_GA_NonIni, q => q.Term(t => t.Field(Dishonest_Meta.A_Field.name).Value(keyword)), pg_index)
            ;

        /// <summary>
        /// Filter-searching of judge
        /// </summary>
        /// <param name="keyword">query keyword</param>
        /// <param name="pg_index"></param>
        /// <param name="pg_size"></param>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        public static ISearchResponse<ES_Dishonest> Dishonest_FSearch(string keyword,  int pg_size, IDictionary<string, string> dict, int pg_index=0) =>
            keyword.Any(c => c > 127)
            ? FSearch_Template(pg_index, pg_size, ES_Buffer.Dishonest_GU_NonIni, UQuery_Create<ES_Dishonest>(keyword, Dishonest_Meta.U_Fields), Dishonest_Fill(dict))
            : FSearch_Template(pg_index, pg_size, ES_Buffer.Dishonest_GA_NonIni, q => q.Term(t => t.Field(Dishonest_Meta.A_Field.name).Value(keyword)), Dishonest_Fill(dict))
            ;

        /// <summary>
        /// Fill filter conditions of dishonest-searching into 'QueryContainer'
        /// </summary>
        /// <remarks>Be aware that keys of this <paramref name="dict"/>must in the value-domain["ob_date", "ob_classno"]</remarks>
        /// <param name="dict">dict of filter conditions</param>
        /// <returns></returns>
        private static Func<QueryContainerDescriptor<ES_Dishonest>, QueryContainer> Dishonest_Fill(IDictionary<string, string> dict) => q =>
            dict.Select(pair =>
                pair.Key == "date"
                ? q.DateRange(d => d.Field("sx_pubdate").GreaterThanOrEquals(pair.Value).LessThan($"{int.Parse(pair.Value) + 1}").Format("yyyy"))
                : (pair.Key == "status" 
                    ? q.Term(t => t.Field("sx_performance").Value(pair.Value))
                    : q.Term(t => t.Field("sx_areaname").Value(pair.Value)))
            ).Aggregate((a, b) => a & b);
        #endregion

        //"int total = 0; for (int i = 0; i < doc['oc_brands'].length; ++i) {total += doc['oc_brands'][i].length;} return total;"
        //public static void Script_Search()
        //{
        //    var response = Client_Get().Search<ES_Company>(s => s.Index("company_nextgen").Type("company")
        //        .Query(q => q.F
            
            
            
        //    //.Query(q => q.Match(m => m.Field("oc_name").Query("腾讯")))
        //        .ScriptFields(sfs => sfs.ScriptField("count", sf => sf.Inline("doc['oc_tels'].value.size()").Lang("painless"))));
        //    //return response;
        //}
    }
}
