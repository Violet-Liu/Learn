/*
 * ES Search：
 * Priority: Term > Prefix > MatchPhrase > Match
 * For same priority: CompanyName > ArtPerson.
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
using QZ.Instrument.Model;
using QZ.Instrument.Global;

namespace QZ.Instrument.Client
{
    public class ESClient
    {
        public const string FunctionScript = "_score * (Math.log(doc['oc_weight'].value + 1) * 100 + 1)";

        //public static int ScriptHash { get { return FunctionScript.GetHashCode(); } }

        #region get ES client


        ///<summary>Since there are more than one ES node, why not use <seealso cref="StaticConnectionPool"/> directly?</summary>
        public static ElasticClient Client_Get(string[] uris) => new ElasticClient(new ConnectionSettings(new StaticConnectionPool(uris.Select(u => new Uri(u)))));

        public static ElasticClient Client_Get() => Client_Get(DataBus.ES_5_0_0_Uris);
        #endregion


        #region special search
        public static IEnumerable<ES_Company> SelectByCode(string oc_code) => 
            Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index)
                                                    .Type(Es_Consts.Company_Type)
                                                    .Query(q => q.Term(f => f.Field("oc_code").Value(oc_code)))
                                            ).Documents;
        #endregion

        #region company general search
        
        public static ISearchResponse<ES_Company> Company_General_Search(Company com) => Company_General_Template(com, IsPureAscii(com.oc_name));

        public static ISearchResponse<ES_Company> Company_General_Filter_Search(Company com) => Company_General_Filter_Template(com, IsPureAscii(com.oc_name));

        private static bool IsPureAscii(string input)
        {
            foreach (var c in input)
            {
                if (c > 127)
                    return false;
            }
            return true;
        }

        #region search conditions composition
        /// <summary>
        /// Compose matching conditions when general search using ascii chars
        /// </summary>
        /// <param name="com"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static QueryContainer General_AsciiSearch_Compose(string oc_name, QueryContainerDescriptor<ES_Company> q) =>
            oc_name.Length < 8 ? General_Ascii_Short_Compose(oc_name, q) : General_Ascii_Long_Compose(oc_name, q);


        private static QueryContainer General_Ascii_Long_Compose(string oc_name, QueryContainerDescriptor<ES_Company> q) =>
            q.Term(t => t.Field("oc_code").Value(oc_name))
            | q.Term(t => t.Field("oc_number").Value(oc_name))
            | q.Term(t => t.Field("oc_creditcode").Value(oc_name))
            | q.MatchPhrase(m => m.Field("oc_tels").Query(oc_name).Strict())
            | q.MatchPhrase(m => m.Field("oc_mails").Query(oc_name).Strict().Boost(0.01))
            | q.MatchPhrase(m => m.Field("oc_sites").Query(oc_name).Strict())
            | q.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().Boost(3))
            //| q.Term(m => m.Field("oc_members").Value(com.oc_name).Strict().Boost(4))                    // match this field: maybe some member of one company are non-Chinese
            //| q.MatchPhrase(m => m.Field("od_gds").Query(com.oc_name).Strict().Boost(5))        // (as former...)
            | q.Prefix(m => m.Field("oc_name.py_oc_name").Value(oc_name));

        private static QueryContainer General_Ascii_Short_Compose(string oc_name, QueryContainerDescriptor<ES_Company> q) =>
            oc_name.Length < 3 
            ? General_Ascii_HyperShort_Compose(oc_name, q) 
            : General_Ascii_HyperShort_Compose(oc_name, q) 
                | q.Prefix(m => m.Field("oc_name.py_oc_name").Value(oc_name)) 
                | q.MatchPhrase(m => m.Field("oc_mails").Query(oc_name).Strict().Boost(0.01));

        private static QueryContainer General_Ascii_HyperShort_Compose(string oc_name, QueryContainerDescriptor<ES_Company> q) =>
            q.MatchPhrase(m => m.Field("oc_sites").Query(oc_name).Strict())
            | q.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().Boost(3));
            //| q.Term(m => m.Field("oc_members").Value(com.oc_name).Strict().Boost(4))                    // match this field: maybe some member of one company are non-Chinese
            //| q.MatchPhrase(m => m.Field("od_gds").Query(com.oc_name).Strict().Boost(5))        // (as former...)

        private static HighlightDescriptor<ES_Company> General_AsciiSearch_HL_Compose(string oc_name, HighlightDescriptor<ES_Company> hl) =>
            oc_name.Length < 8 ? General_Ascii_HL_Short_Compose(hl) : General_Ascii_HL_Long_Compose(hl);

        private static HighlightDescriptor<ES_Company> General_Ascii_HL_Long_Compose(HighlightDescriptor<ES_Company> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("oc_code"),
                    f => f.Field("oc_number"),
                    f => f.Field("oc_creditcode"),
                    f => f.Field("oc_sites"),
                    f => f.Field("oc_brands"),
                    f => f.Field("oc_tels"),
                    f => f.Field("oc_mails")
                    );


        private static HighlightDescriptor<ES_Company> General_Ascii_HL_Short_Compose(HighlightDescriptor<ES_Company> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("oc_sites"),
                    f => f.Field("oc_brands"),
                    f => f.Field("oc_mails")
                    );


        #region SpanQuery
        public static ISearchResponse<ES_Company> SpanFirst_Test(string key) => SpanQuery_Template(q => SpanFirstQuery(key, q));

        public static ISearchResponse<ES_Company> SpanTerm_Test(string key) => SpanQuery_Template(q => SpanTermQuery(key, q));

        public static ISearchResponse<ES_Company> SpanNear_Test(string key) => SpanQuery_Template(q => SpanNear_Query(key, q));

        public static ISearchResponse<ES_Company> SpanNot_Test(string key) => SpanQuery_Template(q => SpanNot_Query(key.Split('-'), q));

        /// <summary>
        /// Use with caution
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Company> Regexp_Test(string key) => SpanQuery_Template(q => Regexp_Query(key, q));


        // path_hierarchy tokenizer

        private static ISearchResponse<ES_Company> SpanQuery_Template(Func<QueryContainerDescriptor<ES_Company>, QueryContainer> func)
        {
            var s = new SearchDescriptor<ES_Company>();
            s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From(0).Take(20)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                        .Query(q => func(q))));
            var response =
            Client_Get().Search<ES_Company>(ss => s);
            return response;
        }

        private static QueryContainer Regexp_Query(string keyword, QueryContainerDescriptor<ES_Company> q) =>
            q.Regexp(r => r.Field("oc_name").Value(keyword)/*.Flags("INTERSECTION|COMPLEMENT|EMPTY")*/.MaximumDeterminizedStates(200000));

        private static QueryContainer SpanNot_Query(string[] keys, QueryContainerDescriptor<ES_Company> q) =>
            q.SpanNot(s => s.Include(i => i.SpanTerm(st => st.Field("oc_name").Value(keys[0])))
             .Exclude(e => e.SpanNear(sn => sn.Clauses(cs => cs.SpanTerm(st => st.Field("oc_name").Value(keys[1])), cs => cs.SpanTerm(st => st.Field("oc_name").Value(keys[0])))
                                              .Slop(5).InOrder(true))));

        private static QueryContainer SpanNear_Query(string keyword, QueryContainerDescriptor<ES_Company> q) =>
            q.SpanNear(s => s.Clauses(sc => sc.SpanTerm(st => st.Field("oc_name").Value(keyword[0])),
                sc => sc.SpanTerm(st => st.Field("oc_name").Value(keyword[1])),
                sc => sc.SpanTerm(st => st.Field("oc_name").Value(keyword[2])),
                sc => sc.SpanTerm(st => st.Field("oc_name").Value(keyword[3]))).Slop(1).InOrder(true));

        public static QueryContainer SpanFirstQuery(string keyword, QueryContainerDescriptor<ES_Company> q) =>
            q.SpanFirst(s => s.End(1).Match(m => m.SpanTerm(st => st.Field("oc_name").Value(keyword))));

        private static QueryContainer SpanTermQuery(string key, QueryContainerDescriptor<ES_Company> q) =>
            q.SpanTerm(s => s.Field("oc_name").Value(key));
        #endregion

        /// <summary>
        /// Compose matching conditions when general search using unicode chars
        /// </summary>
        /// <param name="com"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        private static QueryContainer General_UnicodeSearch_Compose(string oc_name, QueryContainerDescriptor<ES_Company> q) =>
            q.Term(t => t.Field("oc_name.oc_name_raw").Value(oc_name).Boost(1000))
            //| q.MatchPhrase(m => m.Field("oc_sites").Query(oc_name).Strict())
            | q.Term(t => t.Field("od_faren").Value(oc_name).Boost(600))
            | (q.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).Boost(400)) | q.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().Boost(5)))
            | q.MatchPhrase(m => m.Field("oc_members").Query(oc_name).Strict().Boost(400))
            | q.MatchPhrase(m => m.Field("od_gds").Query(oc_name).Strict().Boost(400))
            | q.MatchPhrase(m => m.Field("oc_areaname").Query(oc_name).Strict().Boost(30))
            | (q.MatchPhrasePrefix(m => m.Field("oc_name").Query(oc_name).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).Boost(400)) | q.Match(m => m.Field("oc_name").Query(oc_name).Boost(0.1)))
            //| (oc_name.Length < 5
            //    ? q.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Strict().Boost(40))
            //    : q.Match(m => m.Field("oc_name").Query(oc_name).Slop(1).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.08d)))
            | q.Match(m => m.Field("oc_business").Query(oc_name))
            | q.MatchPhrase(m => m.Field("oc_address").Query(oc_name).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(85)).CutoffFrequency(0.08d))
            ;

        private static QueryContainer LessCharsQuery(string oc_name, QueryContainerDescriptor<ES_Company> q)
        {
            var len = oc_name.Length;
            if (len < 4)
            {
                return q.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Strict().Boost(5)) 
                    | q.Match(m => m.Field("oc_name").Query(oc_name).Slop(1).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).CutoffFrequency(0.01d).Boost(0.001));
            }
            else
            {
                return
                    q.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Strict().Boost(5))
                        | q.Match(m => m.Field("oc_name").Query(oc_name).Slop(1).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).Boost(0.01));
                //if (!oc_name.Contains("公司"))
                //{
                //    var redundant = oc_name + "公司";
                //    return 
                //    q.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Strict().Boost(5))
                //        | q.Match(m => m.Field("oc_name").Query(redundant).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).Boost(0.01));
                //}
                //else
                //{
                //    var brief = oc_name.Replace("公司", "");
                //    return q.MatchPhrase(m => m.Field("oc_name").Query(brief).Strict())
                //    | q.Match(m => m.Field("oc_name").Query(oc_name).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.08d).Boost(0.1));
                //}
            }
        }

        #region highlight
        private static HighlightDescriptor<ES_Company> General_UnicodeSearch_HL_Compose(HighlightDescriptor<ES_Company> hl, bool hasSZ) =>
            hasSZ
            ? hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(
                    f => f.Field("oc_areaname"),
                    f => f.Field("oc_address"),
                    f => f.Field("oc_name")
                    )
            : hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(
                    f => f.Field("oc_name"),
                    f => f.Field("oc_sites"),
                    f => f.Field("od_faren"),
                    f => f.Field("oc_brands"),
                    f => f.Field("oc_members"),
                    f => f.Field("od_gds"),
                    f => f.Field("oc_areaname"),
                    f => f.Field("oc_address"),
                    f => f.Field("od_regtype")
                    );
        private static HighlightDescriptor<ES_Company> Advanced_UnicodeSearch_HL_Compose(Company com, HighlightDescriptor<ES_Company> hl)
        {
            var fields = new List<Func<HighlightFieldDescriptor<ES_Company>, IHighlightField>>();
            if(!string.IsNullOrEmpty(com.oc_name))
            {
                fields.Add(f => f.Field("oc_name"));
            }
            if(!string.IsNullOrEmpty(com.oc_art_person))
            {
                fields.Add(f => f.Field("od_faren"));
            }
            if(!string.IsNullOrEmpty(com.oc_stock_holder))
            {
                fields.Add(f => f.Field("oc_members"));
                fields.Add(f => f.Field("od_gds"));
            }
            if(!string.IsNullOrEmpty(com.oc_addr))
            {
                fields.Add(f => f.Field("oc_address"));
            }
            if(!string.IsNullOrEmpty(com.oc_reg_type))
            {
                fields.Add(f => f.Field("od_regtype"));
            }
            return hl.PreTags("<font color=\"red\">")
                    .PostTags("</font>")
                    .Fields(
                fields.ToArray()
                            );
        }
        #endregion

        #endregion


        private static DateTime minDate = DateTime.Parse("1949-01-01");

        #region general search template
        private static ISearchResponse<ES_Company> Company_General_Template(Company com, bool asciiSearch)
        {
            var s = new SearchDescriptor<ES_Company>();
            s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((com.pg_index - 1) * com.pg_size).Take(com.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                        .Query(q => asciiSearch ? General_AsciiSearch_Compose(com.oc_name, q) : General_UnicodeSearch_Compose(com.oc_name, q))))
                //.Explain()


                .Highlight(hl => asciiSearch ? General_AsciiSearch_HL_Compose(com.oc_name, hl) : General_UnicodeSearch_HL_Compose(hl, com.oc_name.Contains("深圳")))
                ;
            if (com.pg_index == 1)
            {

                s.Aggregations(agg => agg.DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("cat", t => t.Field("gb_cat").Size(22))
                                        .Terms("area", t => t.Field("prefix_area").Size(32))
                                        .Terms("type", t => t.Field("oc_companytype").Size(12))
                                        .Terms("status", t => t.Field("oc_status"))
                                        .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(100), rs => rs.From(100.1).To(500), rs => rs.From(500.1).To(1000),
                                                                rs => rs.From(1000.1))));

            }

            var response = 
            Client_Get().Search<ES_Company>(ss => s
                //.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((com.pg_index - 1) * com.pg_size).Take(com.pg_size)
                //.Query(qq => qq
                //    .FunctionScore(c => c
                //        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                //        .Query(q => asciiSearch ? General_AsciiSearch_Compose(com, q) : General_UnicodeSearch_Compose(com, q))))
                //.Highlight(hl => asciiSearch ? General_AsciiSearch_HL_Compose(hl) : General_UnicodeSearch_HL_Compose(hl))
                //.Aggregations(agg => agg
                //                .DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1).ExtendedBounds(minDate, DateTime.Now))
                //                .Terms("cat", t => t.Field("gb_cat"))
                //                .Terms("area", t => t.Field("prefix_area"))
                //                .Terms("type", t => t.Field("oc_companytype"))
                //                .Terms("status", t => t.Field("oc_status"))
                //                .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(100), rs => rs.From(100.1).To(500), rs => rs.From(500.1).To(1000),
                //                    rs => rs.From(1000.1))))
                                            );

            return response;
        }
        private static ISearchResponse<ES_Company> Company_General_Filter_Template(Company com, bool asciiSearch)
        {
            var status = com.oc_status.ToInt();
            double floor = 0, ceiling = 0;
            if(!string.IsNullOrEmpty(com.oc_regm))
            {
                var vals = com.oc_regm.Split('-');
                if(vals.Length == 2)
                {
                    floor = vals[0].ToDouble();
                    ceiling = vals[1].ToDouble();
                }
            }
            DateTime start = DateTime.MinValue, end = DateTime.MinValue;
            if(!string.IsNullOrEmpty(com.year) && DateTime.TryParse(com.year, out start))
            {
                end = start.AddYears(1);
            }
            var s = new SearchDescriptor<ES_Company>();
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<ES_Company>();
            com.ToMaybe()
               .DoWhen(q => start > DateTime.MinValue, q => qcs.Add(qcd.DateRange(d => d.Field("oc_issuetime").GreaterThanOrEquals(start).LessThan(end))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("00"), q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_type), q => qcs.Add(qcd.Term(p => p.Field("oc_companytype").Value(q.oc_type))))
               .DoWhen(q => status >= 0, q => qcs.Add(qcd.Term(t => t.Field("oc_status").Value(status))))
               .DoWhen(q => q.oc_trade != null, q => qcs.Add(q.oc_trade.Length <= 1 ? qcd.Term(t => t.Field("gb_cat").Value(q.oc_trade)) : qcd.Prefix(qp => qp.Field("gb_codes").Value(q.oc_trade))))
               .DoWhen(q => floor > 0 || ceiling > 0,
                                   q => qcs.Add(qcd.Range(p => p.Field("od_regm").GreaterThan(floor).LessThanOrEquals(ceiling <= 0 ? Int32.MaxValue : ceiling))))
               .Do(q => qcs.Add(asciiSearch ? General_AsciiSearch_Compose(com.oc_name, qcd) : General_UnicodeSearch_Compose(com.oc_name, qcd)));

            s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((com.pg_index - 1) * com.pg_size).Take(com.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                        .Query(q => qcs.Aggregate((a, b) => a & b))))
                .Highlight(hl => asciiSearch ? General_AsciiSearch_HL_Compose(com.oc_name, hl) : General_UnicodeSearch_HL_Compose(hl, com.oc_name.Contains("深圳")));

            if (com.oc_sort == oc_sort.oc_reg_capital)
            {
                s.Sort(st => st.Field(sfd => sfd.Field("od_regm").UnmappedType(FieldType.Float).Order(SortOrder.Descending)));
            }
            else if (com.oc_sort == oc_sort.oc_issue_time)
            {
                s.Sort(st => st.Field(sfd => sfd.Field("oc_issuetime").UnmappedType(FieldType.Float).Order(SortOrder.Descending)));
            }
            var response = Client_Get().Search<ES_Company>(s);
            return response;
        }
        #endregion

        /// <summary>
        /// General search(e.g. given a unique input box)
        /// Supposing the user input are not all Ascii chars, search through this method
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Company> Company_General_UnicodeSearch(Company com) => Company_General_Template(com, false);

        /// <summary>
        /// General search(e.g. given a unique input box)
        /// Supposing the user input are all Ascii chars, search through this method
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Company> Company_General_AsciiSearch(Company com) => Company_General_Template(com, true);

        /// <summary>
        /// General search with some filter conditions
        /// Supposing the user input are all Ascii chars, search through this method
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Company> Company_General_Filter_AsciiSearch(Company com) => Company_General_Filter_Template(com, true);

        /// <summary>
        /// General search with some filter conditions
        /// Supposing the user input are not all Ascii chars, search through this method
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public static ISearchResponse<ES_Company> Company_General_Filter_UnicodeSearch(Company com) => Company_General_Filter_Template(com, false);

        #endregion

        #region company advanced search
        public static ISearchResponse<ES_Company> Company_Advanced_Search(Company com)
        {
            
            var status = com.oc_status.ToInt();
            double floor = 0, ceiling = 0;
            if (!string.IsNullOrEmpty(com.oc_regm))
            {
                var vals = com.oc_regm.Split('-');
                if (vals.Length == 2)
                {
                    floor = vals[0].ToDouble();
                    ceiling = vals[1].ToDouble();
                }
            }
            DateTime start = DateTime.MinValue, end = DateTime.MinValue;
            if (!string.IsNullOrEmpty(com.year) && DateTime.TryParse(com.year, out start))
            {
                end = start.AddYears(1);
            }
            var s = new SearchDescriptor<ES_Company>();
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<ES_Company>();
            com.ToMaybe()
               .DoWhen(q => start > DateTime.MinValue, q => qcs.Add(qcd.DateRange(d => d.Field("oc_issuetime").GreaterThanOrEquals(start).LessThan(end))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("00"), q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_type), q => qcs.Add(qcd.Term(p => p.Field("oc_companytype").Value(q.oc_type))))
               .DoWhen(q => status >= 0, q => qcs.Add(qcd.Term(t => t.Field("oc_status").Value(status))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_trade), q => qcs.Add(q.oc_trade.Length == 1 ? qcd.Term(t => t.Field("gb_cat").Value(q.oc_trade)) : qcd.Prefix(qp => qp.Field("gb_codes").Value(q.oc_trade))))
               .DoWhen(q => floor > 0 || ceiling > 0,
                                   q => qcs.Add(qcd.Range(p => p.Field("od_regm").GreaterThan(floor).LessThanOrEquals(ceiling <= 0 ? Int32.MaxValue : ceiling))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_code), q => qcs.Add(qcd.Term(t => t.Field("oc_code").Value(com.oc_code))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_number), q => qcs.Add(qcd.Term(t => t.Field("oc_number").Value(com.oc_number))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_art_person), q => qcs.Add(qcd.MatchPhrase(p => p.Field("od_faren").Query(q.oc_art_person))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_stock_holder), q => qcs.Add(qcd.Match(p => p.Field("od_gds").Query(q.oc_stock_holder))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_site), q => qcs.Add(qcd.MatchPhrase(m => m.Field("oc_sites").Query(com.oc_name).Strict())))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_member), q => qcs.Add(qcd.Match(p => p.Field("oc_members").Query(q.oc_member))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_name), q => qcs.Add(qcd.Term(p => p.Field("oc_name.oc_name_raw").Value(q.oc_name)) |
                                                                           qcd.Prefix(m => m.Field("oc_name.py_oc_name").Value(q.oc_name)) |
                                                                           qcd.MatchPhrase(p => p.Field("oc_name").Query(q.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_addr), q => qcs.Add(qcd.Match(p => p.Field("oc_address").Query(q.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_business), q => qcs.Add(qcd.Match(p => p.Field("od_bussiness").Query(q.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_type), q => qcs.Add(qcd.Match(p => p.Field("od_regtype").Query(q.oc_reg_type).Slop(1).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.08d))))

               //.DoWhen(q => q.oc_status >= 0, q => qcs.Add(qcd.Term(t => t.Field("oc_status").Value(q.oc_status))))
               //.DoWhen(q => !string.IsNullOrEmpty(q.oc_trade), q => qcs.Add(qcd.Nested(nest => nest.Name("trade").Query(query => query.Prefix(qp => qp.Field("trades.gb_code").Value(q.oc_trade))))))
               .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_capital_floor) || !string.IsNullOrEmpty(q.oc_reg_capital_ceiling),
                                   q => qcs.Add(qcd.Range(p => p.Field("od_regm").GreaterThanOrEquals(string.IsNullOrEmpty(com.oc_reg_capital_floor) ? 0 : Convert.ToDouble(com.oc_reg_capital_floor))
                                                          .LessThanOrEquals(string.IsNullOrEmpty(com.oc_reg_capital_ceiling) ? Int32.MaxValue : Convert.ToDouble(com.oc_reg_capital_ceiling)))));

            s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((com.pg_index - 1) * com.pg_size).Take(com.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                        .Query(q => qcs.Aggregate((a, b) => a & b))))
                .Highlight(hl => Advanced_UnicodeSearch_HL_Compose(com, hl)
                            //hl
                            //.PreTags("<font color=\"red\">")
                            //.PostTags("</font>")
                            //.Fields(f => f.Field("oc_code"),
                            //        f => f.Field("od_faren"),
                            //        f => f.Field("od_gds"),
                            //        f => f.Field("oc_name"),
                            //        f => f.Field("oc_sites"),
                            //        f => f.Field("oc_address"),
                            //        f => f.Field("oc_members"),
                            //        f => f.Field("od_regtype")
                            );
            if (com.pg_index == 1)
                s.Aggregations(agg => agg.DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("cat", t => t.Field("gb_cat"))
                                        .Terms("area", t => t.Field("prefix_area"))
                                        .Terms("type", t => t.Field("oc_companytype"))
                                        .Terms("status", t => t.Field("oc_status"))
                                        .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(10), rs => rs.From(10.1).To(50), rs => rs.From(50.1).To(500), rs => rs.From(500.1).To(1000),
                                            rs => rs.From(1000.1))));

            if (com.oc_sort == oc_sort.oc_reg_capital)
            {
                s.Sort(st => st.Field(sfd => sfd.Field("od_regm").UnmappedType(FieldType.Float).Order(SortOrder.Descending)));
            }
            else if (com.oc_sort == oc_sort.oc_issue_time)
            {
                s.Sort(st => st.Field(sfd => sfd.Field("oc_issuetime").UnmappedType(FieldType.Float).Order(SortOrder.Descending)));
            }
            var response = Client_Get().Search<ES_Company>(s);
            return response;
        }
        
        #endregion

        #region company search by trade

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="name">child constraint name</param>
        /// <returns></returns>
        [Obsolete]
        public static ISearchResponse<ES_Company> Old_Company_TradeSearch(Req_TradeSearch search)
        {
            var response =
            Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index).Type($"{Es_Consts.Company_Type},{Es_Consts.Trade_Type}").From((search.pg_index - 1) * search.pg_size).Take(search.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value + doc['trades.trd_weight].value * 10"))))
                        .Query(q => q.Nested(n => n.Path(p => p.gb_codes).Query(query => query.Term(t => t.Field("trades.gb_code").Value(search.trd_code)))))
                                    )));
            return response;
        }

        public static ISearchResponse<ES_Company> Company_TradeCodeSearch(Req_TradeSearch search, string fieldName)
        {
            var response =
            Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((search.pg_index - 1) * search.pg_size).Take(search.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value * 5"))))
                        .Query(q => q.Prefix(p => p.Field(fieldName).Value(search.trd_code))))
                                    ));
            return response;
        }

        public static ISearchResponse<ES_Company> Company_TradeNameSearch(Req_TradeSearch search, string fieldName) =>
            Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((search.pg_index - 1) * search.pg_size).Take(search.pg_size)
                .Query(q => q
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value * 5"))))
                        .Query(fq => fq.MatchPhrase(p => p.Field(fieldName).Query(search.trd_name))))
                                    ));

        public static ISearchResponse<ES_Company> Company_FilterByTrade_Search(string childQueryName, string tradeVal, string oc_name)
        {
            var response =
            Client_Get().Search<ES_Company>(s => s.Index("company_nextgen").Type("company,trade").From(0).Take(20)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value"))))
                        .Query(q => q.HasChild<Trade>(ch => ch.Name(childQueryName).Type("trade").Query(cq => cq.Prefix(pre => pre.Field("gb_code").Value(tradeVal))))
                                    &(q.Term(t => t.Field("oc_name.oc_name_raw").Value(oc_name))
                                    | q.MatchPhrase(m => m.Field("oc_sites").Query(oc_name).Strict())
                                    | q.Term(t => t.Field("od_faRen").Value(oc_name))
                                    | q.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().Boost(10))
                                    | q.Term(m => m.Field("oc_members").Value(oc_name).Strict())
                                    | q.MatchPhrase(m => m.Field("od_gds").Query(oc_name).Strict().Boost(5))
                                    | q.MatchPhrase(m => m.Field("oc_areaname").Query(oc_name).Strict())
                                    | q.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90))
                                                        .CutoffFrequency(0.08d))
                                                        )
                                                        )))
                                )
                                    ;
            return response;
        }

        public static ISearchResponse<ES_Company> Company_SearchByTrade(string oc_name, string queryName, string tradeVal)
        {
            var response = Client_Get().Search<ES_Company>(s => s.Index("company_nextgen").Type("company,trade").From(0).Take(20)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value"))))
                        .Query(q => q.HasParent<ES_Company>(ch => ch.Name(queryName).Type("company").Query(cq => cq.Term(t => t.Field("oc_name.oc_name_raw").Value(oc_name))
                                    | cq.MatchPhrase(m => m.Field("oc_sites").Query(oc_name).Strict())
                                    | cq.Term(t => t.Field("od_faren").Value(oc_name))
                                    | cq.MatchPhrase(m => m.Field("oc_brands").Query(oc_name).Strict().Boost(10))
                                    | cq.Term(m => m.Field("oc_members").Value(oc_name).Strict())
                                    | cq.MatchPhrase(m => m.Field("od_gds").Query(oc_name).Strict().Boost(5))
                                    | cq.MatchPhrase(m => m.Field("oc_areaname").Query(oc_name).Strict())
                                    | cq.MatchPhrase(m => m.Field("oc_name").Query(oc_name).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90))
                                                        .CutoffFrequency(0.08d)))
                               .InnerHits(ih => ih.Highlight(hl => hl
                                    .PreTags("<font color=\"red\">")
                                    .PostTags("</font>")
                                    .Fields(
                                            f => f.Field("oc_name"),
                                            f => f.Field("oc_sites"),
                                            f => f.Field("od_faren"),
                                            f => f.Field("oc_brands"),
                                            f => f.Field("oc_members"),
                                            f => f.Field("od_gds"),
                                            f => f.Field("oc_patents"),
                                            f => f.Field("oc_areaname")))))
                        &
                        q.Prefix(pre => pre.Field("gb_code").Value(tradeVal))))

                                                        ));
            return response;
        }

        public static ISearchResponse<ES_Company> Company_TradeUniversalSearch(Req_Trade_UniversalSearch ts)
        {
            var response =
            Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type).From((ts.pg_index - 1) * ts.pg_size).Take(ts.pg_size)
                .Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline("_score + doc['oc_weight'].value * 5"))))
                        .Query(q => UniversalTradeSearch_Compose(ts, q)))
                                    ));
            return response;
        }

        public static ISearchResponse<ES_Exhibit> Exhibit_Search(Req_Info_Query query)
        {
            if(string.IsNullOrEmpty(query.cat_s) && query.q_sort == 0)      // general search
            {
                return ExhibitGeneral_Search(query);
            }
            else        // filter search
            {
                bool isAscii = !query.query_str.Any(c => c > 127);
                var s = new SearchDescriptor<ES_Exhibit>();
                var qcs = new List<QueryContainer>();
                var qcd = new QueryContainerDescriptor<ES_Exhibit>();
                query.ToMaybe()
                   .DoWhen(q => !string.IsNullOrEmpty(q.cat_s), q => qcs.Add(qcd.Term(p => p.Field("e_trade").Value(q.cat_s))))
                   .Do(q => qcs.Add(isAscii 
                    ? Exhibit_AsciiQueryCompose(q.query_str, qcd)
                    : Exhibit_UnicodeQueryCompose(q.query_str, qcd)));

                s.Index(Es_Consts.Company_Ext_Type).Type(Es_Consts.Exhibit_Type).From((query.pg_index - 1) * query.pg_size).Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Highlight(hl => Exhibit_HL_Compose(hl));

                if (query.q_sort == 2)
                {
                    s.Sort(st => st.Field(sfd => sfd.Field("e_start").UnmappedType(FieldType.Date).Order(SortOrder.Descending)));
                }
                else if (query.q_sort == 1)
                {
                    s.Sort(st => st.Field(sfd => sfd.Field("e_start").UnmappedType(FieldType.Date).Order(SortOrder.Ascending)));
                }
                var response = Client_Get().Search<ES_Exhibit>(ss => s);
                return response;
            }
        }

        public static ISearchResponse<ES_Exhibit> ExhibitGeneral_Search(Req_Info_Query ts)
        {
            bool isAscii = !ts.query_str.Any(c => c > 127);

            var s = new SearchDescriptor<ES_Exhibit>();
            s.Index(Es_Consts.Company_Ext_Type).Type(Es_Consts.Exhibit_Type).From((ts.pg_index - 1) * ts.pg_size).Take(ts.pg_size)
                .Query(q => isAscii
                    ? Exhibit_AsciiQueryCompose(ts.query_str, q)
                    : Exhibit_UnicodeQueryCompose(ts.query_str, q))
                //.Explain()
                .Highlight(hl => Exhibit_HL_Compose(hl))
                ;
            if (ts.pg_index == 1)
            {
                s.Aggregations(agg => agg.Terms("date", t => t.Field("e_year").Size(20))
                                        .Terms("trade", t => t.Field("e_trade").Size(45))
                                        .Terms("province", t => t.Field("e_province").Size(30))
                                        );

            }
            var response = Client_Get().Search<ES_Exhibit>(ss => s);
            return response;
        }

        private static HighlightDescriptor<ES_Exhibit> Exhibit_HL_Compose(HighlightDescriptor<ES_Exhibit> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("e_year"),
                    f => f.Field("e_showid"),
                    f => f.Field("e_trade"),
                    f => f.Field("e_name"),
                    f => f.Field("e_hall"),
                    f => f.Field("e_province"),
                    f => f.Field("e_city")
                    );

        private static QueryContainer Exhibit_AsciiQueryCompose(string keyword, QueryContainerDescriptor<ES_Exhibit> q)
        {
            if (keyword.ToInt() > 0)
                return q.Term(t => t.Field("e_showid").Value(keyword)) | q.Term(t => t.Field("e_year").Value(keyword));
            else
                return q.Match(m => m.Field("e_name").Query(keyword)) | q.Match(m => m.Field("e_hall").Query(keyword));
        }

        private static QueryContainer Exhibit_UnicodeQueryCompose(string keyword, QueryContainerDescriptor<ES_Exhibit> q) =>
            q.Term(t => t.Field("e_trade").Value(keyword).Boost(30))
                    | q.Term(t => t.Field("e_name.name_raw").Value(keyword).Boost(100))
                    | q.Term(t => t.Field("e_hall.hall_raw").Boost(50))
                    | q.MatchPhrase(m => m.Field("e_name").Query(keyword).MinimumShouldMatch(MinimumShouldMatch.Percentage(95)).CutoffFrequency(0.01d).Boost(5))
                    | q.Match(m => m.Field("e_name").Query(keyword).Slop(2))
                    | q.Term(t => t.Field("e_province").Value(keyword).Boost(20))
                    | q.Term(t => t.Field("e_city").Value(keyword).Boost(20))
                    | q.Match(m => m.Field("e_hall").Query(keyword).Slop(2));

        private static QueryContainer UniversalTradeSearch_Compose(Req_Trade_UniversalSearch ts, QueryContainerDescriptor<ES_Company> q)
        {
            var qcs = new List<QueryContainer>();
            if(ts.exh_names != null)
            {
                foreach(var s in ts.exh_names)
                {
                    qcs.Add(q.MatchPhrase(m => m.Field("exh_trades").Query(s)));
                }
            }
            if(ts.pro_codes != null)
            {
                foreach(var s in ts.pro_codes)
                {
                    qcs.Add(q.Prefix(p => p.Field("pro_codes").Value(s)));
                }
            }
            if(ts.fwd_names != null)
            {
                foreach(var s in ts.fwd_names)
                {
                    qcs.Add(q.MatchPhrase(m => m.Field("fwd_trades").Query(s)));
                }
            }
            if(ts.gb_codes != null)
            {
                foreach(var s in ts.gb_codes)
                {
                    qcs.Add(q.Prefix(p => p.Field("gb_codes").Value(s)));
                }
            }
            return qcs.Aggregate((a, b) => a | b);
        }
        #endregion

        #region research

        public static ISearchResponse<ES_Company> Script_Search()
        {
            var response = Client_Get().Search<ES_Company>(s => s.Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type)
                .ScriptFields(sfs => sfs.ScriptField("source", sf => sf.Inline("_source.oc_code + ' ' + _source.oc_name").Lang("painless"))));
            return response;
        }


        #endregion


        #region dishonest

        private static QueryContainer Dishonest_AsciiSearch(string keyword, QueryContainerDescriptor<ES_Dishonest> q) =>
            q.Term(t => t.Field("sx_cardnum").Value(keyword));

        private static QueryContainer Dishonest_UnicodeSearch(string keyword, QueryContainerDescriptor<ES_Dishonest> q) =>
            q.Term(t => t.Field("sx_entity").Value(keyword).Boost(5))
            | q.Term(t => t.Field("sx_pname").Value(keyword).Boost(5))
            | q.Term(t => t.Field("sx_areaname").Value(keyword).Boost(5))
            | q.Term(t => t.Field("sx_oc_name.name_raw").Value(keyword).Boost(10))
            | (keyword.Length < 5
                ? q.MatchPhrase(m => m.Field("sx_oc_name").Query(keyword).Analyzer("urldelimit"))
                : q.MatchPhrase(m => m.Field("sx_oc_name").Query(keyword).Slop(0).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).CutoffFrequency(0.001).Boost(3)))
            | q.Match(m => m.Field("sx_oc_name").Query(keyword).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(80)).Boost(0.5));

        private static HighlightDescriptor<ES_Dishonest> Dishonest_AsciiSearch_HL_Compose(HighlightDescriptor<ES_Dishonest> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("sx_cardnum"),
                    f => f.Field("sx_oc_name.py_name")
                    );

        private static HighlightDescriptor<ES_Dishonest> Dishonest_UnicodeSearch_HL_Compose(HighlightDescriptor<ES_Dishonest> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("sx_entity"),
                    f => f.Field("sx_pname"),
                    f => f.Field("sx_areaname"),
                    f => f.Field("sx_oc_name")
                    );

        public static ISearchResponse<ES_Dishonest> Dishonest_Search(Req_Info_Query query)
        {
            bool isAscii = !query.query_str.Any(c => c > 127);
            var s = new SearchDescriptor<ES_Dishonest>();
            s.Index(Es_Consts.Company_Ext_Type).Type(Es_Consts.Dishonest_Type).From((query.pg_index - 1) * query.pg_size).Take(query.pg_size)
                .Query(q => isAscii 
                    ? (q.Term(t => t.Field("sx_cardnum").Value(query.query_str)) | q.MatchPhrasePrefix(mp => mp.Field("sx_oc_name.py_name").Query(query.query_str)))
                    : Dishonest_UnicodeSearch(query.query_str, q))
                //.Explain()
                .Highlight(hl => isAscii ? Dishonest_AsciiSearch_HL_Compose(hl) : Dishonest_UnicodeSearch_HL_Compose(hl))
                ;
            if (query.pg_index == 1)
            {
                s.Aggregations(agg => agg.DateHistogram("date", t => t.Field("sx_pubdate").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("performance", t => t.Field("sx_performance"))
                                        .Terms("area", t => t.Field("sx_areaname").Size(32))
                                        );

            }
            var response = Client_Get().Search<ES_Dishonest>(ss => s);
            return response;
        }
        #endregion

        #region brand

        private static QueryContainer Brand_AsciiSearch(string keyword, QueryContainerDescriptor<ES_Brand> q) =>
            q.Term(t => t.Field("ob_regno").Value(keyword))
            | q.Term(t => t.Field("ob_name.ob_name_raw").Value(keyword))
            | q.Term(t => t.Field("ob_classno").Value(keyword))
            | q.Term(t => t.Field("ob_oc_code").Value(keyword));

        private static HighlightDescriptor<ES_Brand> Brand_AsciiSearch_HL(HighlightDescriptor<ES_Brand> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("ob_regno"),
                    f => f.Field("ob_classno"),
                    f => f.Field("ob_oc_code"),
                    f => f.Field("ob_name")
                    );

        private static QueryContainer Brand_UnicodeSearch(string keyword, QueryContainerDescriptor<ES_Brand> q) =>
            q.Term(t => t.Field("ob_name.ob_name_raw").Value(keyword).Boost(20))
            | q.Term(t => t.Field("ob_proposer.ob_proposer_raw").Value(keyword).Boost(20))
            | q.MatchPhrase(t => t.Field("ob_dlrmc").Query(keyword).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(80)).CutoffFrequency(0.001))
            | q.MatchPhrase(t => t.Field("ob_proposer").Query(keyword).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.001).Boost(10))
            | q.MatchPhrase(t => t.Field("ob_name").Query(keyword).Slop(0).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)).Boost(10))
            | q.MatchPhrase(t => t.Field("ob_proposeraddr").Query(keyword).Slop(0).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)).CutoffFrequency(0.001).Boost(5))
            ;
        private static HighlightDescriptor<ES_Brand> Brand_UnicodeSearch_HL(HighlightDescriptor<ES_Brand> hl) => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(f => f.Field("ob_name"),
                    f => f.Field("ob_proposer"),
                    f => f.Field("ob_dlrmc"),
                    f => f.Field("ob_proposeraddr")
                    );
        public static ISearchResponse<ES_Brand> Brand_Search(Req_Info_Query query)
        {
            bool isAscii = !query.query_str.Any(c => c > 127);

            var s = new SearchDescriptor<ES_Brand>();
            s.Index(Es_Consts.Company_Ext_Type).Type(Es_Consts.Brand_Type).From((query.pg_index - 1) * query.pg_size).Take(query.pg_size)
                .Query(q => isAscii
                    ? Brand_AsciiSearch(query.query_str, q)
                    : Brand_UnicodeSearch(query.query_str, q))
                //.Explain()
                .Highlight(hl => isAscii ? Brand_AsciiSearch_HL(hl) : Brand_UnicodeSearch_HL(hl))
                ;
            if (query.pg_index == 1)
            {
                s.Aggregations(agg => agg.DateHistogram("date", t => t.Field("ob_date").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("class", t => t.Field("ob_classno").Size(45))
                                        .Terms("status", t => t.Field("ob_status").Size(30))
                                        );

            }
            var response = Client_Get().Search<ES_Brand>(ss => s);
            return response;
        }
        #endregion


        #region Export ES Data

        /// <summary>
        /// Search company for exporting.
        /// </summary>
        /// <param name="keyword">Keyword of search</param>
        /// <param name="limit">Total count of documents to be exported. If is 0, then all matched documents will be exported</param>
        /// <returns></returns>
        public static SuperSet<ES_Company> Company_Search4Export(string keyword, int limit)
        {
            if (string.IsNullOrWhiteSpace(keyword) || limit < 0 || (limit > 0 && limit < 10000)) throw new ArgumentException("search keyword can not be empty or search count value can not be minus");
            var client = Client_Get();
            string _scrollId;
            

            if (limit == 0)
            {
                var response = CompanySearch_IniScroll(client, keyword);
                var set = new SuperSet<ES_Company>(10);
                set.Add(response.Documents);
                _scrollId = response.ScrollId;

                while(response.Documents.Count() == 0x800)
                {
                    response = CompanySearch_FollowScroll(client, _scrollId);
                    _scrollId = response.ScrollId;

                    set.Add(response.Documents);
                }
                return set;
            }
            else
            {
                var count = limit >> 20;
                if (limit % 0x800 != 0)
                    count++;

                var set = new SuperSet<ES_Company>(count);
                var response = CompanySearch_IniScroll(client, keyword);
                set.Add(response.Documents);
                _scrollId = response.ScrollId;

                for(int i = 0; i < count -1; i++)
                {
                    response = CompanySearch_FollowScroll(client, _scrollId);
                    _scrollId = response.ScrollId;
                    set.Add(response.Documents);
                }
                return set;
            }
        }

        private static ISearchResponse<ES_Company> CompanySearch_IniScroll(ElasticClient client, string keyword)
        {
            bool ascii = !keyword.Any(c => c > 127);
            var response = client.Search<ES_Company>(s => s.Scroll("1m").Index(Es_Consts.Company_Index).Type(Es_Consts.Company_Type)
                .Take(0x800).Query(qq => qq
                    .FunctionScore(c => c
                        .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Lang("painless").Inline(FunctionScript))))
                        .Query(q => ascii ? General_AsciiSearch_Compose(keyword, q) : General_UnicodeSearch_Compose(keyword, q))))
                //.Explain()


                //.Highlight(hl => ascii ? General_AsciiSearch_HL_Compose(keyword, hl) : General_UnicodeSearch_HL_Compose(hl, keyword.Contains("深圳")))
                .Aggregations(agg => agg//.DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("cat", t => t.Field("gb_cat").Size(22))
                                        .Terms("area", t => t.Field("prefix_area").Size(32))
                                        .Terms("type", t => t.Field("oc_companytype").Size(12))
                                        .Terms("status", t => t.Field("oc_status"))
                                        .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(100), rs => rs.From(100.1).To(500), rs => rs.From(500.1).To(1000),
                                                                rs => rs.From(1000.1))))
                                                                );
            return response;
        }

        private static ISearchResponse<ES_Company> CompanySearch_FollowScroll(ElasticClient client, string scrollId) =>
            client.Scroll<ES_Company>("1m", scrollId);
        
        #endregion


        public class SuperSet<T>
        {
            private List<IEnumerable<T>> _set;
            public int Count { get { return _set.Count; } }
            
            public SuperSet(int capacity)
            {
                if (capacity < 1 || capacity > 0x10000)
                    throw new ArgumentException("capacity must be positive and little than 0x10000");
                _set = new List<IEnumerable<T>>(capacity);
            }

            public void Add(IEnumerable<T> @enum) => _set.Add(@enum);

            public IEnumerable<T> Values
            {
                get
                {
                    for(int i = 0; i < _set.Count; i++)
                    {
                        foreach (var t in _set[i])
                            yield return t;
                    }
                }
            }
        }
    }

}
