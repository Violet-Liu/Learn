/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 * Elasticsearch client which implements data searching.
 * Because there are many times of pattern matching, so F# may be a better choice here.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Nest;
using Elasticsearch.Net;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;
using QZ.Foundation.Monad;
using QZ.Instrument.Global;

namespace QZ.Instrument.Client
{
    public class ElasticsearchClient
    {
        public static ElasticClient Get_Client(string[] uris)
        {
            if (uris.Length < 2)
                return new ElasticClient(new ConnectionSettings(new Uri(uris[0])));

            return new ElasticClient(new ConnectionSettings(new StaticConnectionPool(uris.Select(u => new Uri(u)))));
        }

        public static ElasticClient Get_Client() => Get_Client(new string[] { "http://192.168.1.180:7111", "http://192.168.2.217:7111" });

        //[DebuggerStepThrough]
        public static ISearchResponse<OrgCompanyCombine> General_Query_Obsolute(Company query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Company>();
            var sfd = new SortFieldDescriptor<Company>();
            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))

                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("0"),
                                   q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))

                           .Do(q => qcs.Add(qcd.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                    m => m.Prefix(p => p.Field("oc_area").Value("71"))))))

                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("oc_name.oc_name_raw").Value(q.oc_name))
                                                                        //| m.MatchPhrase(t => t.Field("oc_name").Query(q.oc_name))
                                                                        | m.Match(t => t.Field("_all").Query(q.oc_name).Slop(1).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))))
                           
                           .DoWhen(q => q.oc_sort == oc_sort.oc_reg_capital,
                                         q => sfd.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                           .DoWhen(q => q.oc_sort == oc_sort.oc_issue_time,
                                         q => sfd.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            var resp = client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    //.Query(q => q.FunctionScore(c => c.Query(qq => qcs.Aggregate((a, b) => a & b))
                    //                   .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Inline("_score + doc['oc_weight'].value * 10"))))))
                    .Sort(sd => sd.Field(f => sfd)));
            return resp;
        }

        public static ISearchResponse<OrgCompanyCombine> General_Query(Company query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Company>();
            var sfd = new SortFieldDescriptor<Company>();
            if (query.oc_name.Contains("万") || query.oc_name.Contains("元"))
            {
                query.ToMaybe()
                               //.DoWhen(q => q.pg_index < 1,
                               //        q => q.Pg_Index(1))

                               .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("00"),
                                       q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))

                               .Do(q => qcs.Add(qcd.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                        m => m.Prefix(p => p.Field("oc_area").Value("71"))))))

                               .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                       q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("oc_name.oc_name_raw").Value(query.oc_name))
                                                                        | m.MatchPhrasePrefix(t => t.Field("oc_name").Query(q.oc_name).Slop(1).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))
                                                                        //| m.Match(t => t.Field("oc_name").Query(query.oc_name))
                                                                        ))))
                               .DoWhen(q => !string.IsNullOrEmpty(query.oc_reg_type),
                                   q => qcs.Add(qcd.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And))))
                               .DoWhen(q => q.oc_sort == oc_sort.oc_reg_capital,
                                             q => sfd.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                               .DoWhen(q => q.oc_sort == oc_sort.oc_issue_time,
                                             q => sfd.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending));
            }
            else
            {
                query.ToMaybe()
                               //.DoWhen(q => q.pg_index < 1,
                               //        q => q.Pg_Index(1))

                               .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !q.oc_area.Equals("00"),
                                       q => qcs.Add(qcd.Prefix(d => d.Field("oc_area").Value(q.oc_area))))

                               .Do(q => qcs.Add(qcd.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                        m => m.Prefix(p => p.Field("oc_area").Value("71"))))))

                               .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                       q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("oc_name.oc_name_raw").Value(q.oc_name))
                                                                            | m.MatchPhrasePrefix(t => t.Field("oc_name").Query(q.oc_name).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))
                                                                            | m.Match(t => t.Field("_all").Query(q.oc_name).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))
                                                                            ))))
                               .DoWhen(q => !string.IsNullOrEmpty(query.oc_reg_type),
                                   q => qcs.Add(qcd.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And))))
                               .DoWhen(q => !string.IsNullOrEmpty(q.oc_ext),
                                   q =>
                                   {
                                       MatchQuery mq = new MatchQuery() { Field = "od_ext", Query = "吊销 注销 停业", Operator = Operator.Or };
                                       qcs.Add(!mq);
                                       //qcs.Add(d.Bool(b => b.MustNot(m => m.Match(p => p.Field("od_ext").Query(query.oc_ext).Operator(Operator.And)))));
                                       //qcs.Add(d.Bool(p => p.Filter(t => t.DateRange(dr => dr.Field("od_CreateTime").GreaterThan(DateTime.MinValue)))));
                                   })
                               .DoWhen(q => q.oc_sort == oc_sort.oc_reg_capital,
                                             q => sfd.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                               .DoWhen(q => q.oc_sort == oc_sort.oc_issue_time,
                                             q => sfd.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending));
            }

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            var resp = client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    //.PostFilter(pf => pf.Limit(l => l.Limit(1000)))
                    //.Query(q => qcs.Aggregate((a, b) => a & b))
                    .Query(q => q.FunctionScore(c => c.Query(qq => qcs.Aggregate((a, b) => a & b))
                                       .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Inline("_score + doc['oc_weight'].value * 10"))))))
                    .Sort(sd => sd.Field(f => sfd)));
            return resp;
        }

        public static ISearchResponse<OrgCompanyCombine> General_Function_Query(Company query)
        {
            
            
            var client = Get_Client(DataBus.Elasticsearch_Uris);
            var resp = client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => q.FunctionScore(c => c
                        .Query(qq => qq.Match(t => t.Field("oc_name").Query(query.oc_name)))
                                       .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Inline("_score +  doc['oc_weight'].value * 10"))))))
                    );
            return resp;
        }

        public static ISearchResponse<OrgCompanyCombine> Advanced_Query_Obsolute(Company query)
        {
            var qcs = new List<QueryContainer>();
            var d = new QueryContainerDescriptor<Company>();
            var sfd = new SortFieldDescriptor<Company>();

            query.ToMaybe().Do(q => qcs.Add(d.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                    m => m.Prefix(p => p.Field("oc_area").Value("71"))))))
            #region oc_code
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_code),
                                   q => qcs.Add(d.Term(t => t.Field("oc_code").Value(query.oc_code))))
            #endregion
            #region oc_area
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !query.oc_area.Equals("0"),
                                   q => qcs.Add(d.MatchPhrasePrefix(p => p.Field("oc_area").Query(query.oc_area))))
            #endregion
            #region oc_number
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_number),
                                   q => qcs.Add(d.Term(t => t.Field("oc_number").Value(query.oc_number))))
            #endregion
            #region oc_art_person
                           .DoWhenOrElse(q => !string.IsNullOrEmpty(q.oc_art_person),
                                         q => qcs.Add(d.MatchPhrase(p => p.Field("od_faRen").Query(query.oc_art_person)) |
                                                Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))),
                                         q => qcs.Add(Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))))
            #endregion
            #region oc_name
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                   q => qcs.Add(d.Term(p => p.Field("oc_name.oc_name.raw").Value(query.oc_name)) |
                                                d.Match(p => p.Field("oc_name").Query(query.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
            #region oc_addr
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_addr),
                                   q => qcs.Add(d.Match(p => p.Field("oc_address").Query(query.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_business),
                                   q => qcs.Add(d.Match(p => p.Field("od_bussinessDes").Query(query.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
                           .DoWhen(q => !string.IsNullOrEmpty(query.oc_reg_type),
                                   q => qcs.Add(d.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_ext),
                                   q => qcs.Add(d.Match(p => p.Field("od_ext").Query(query.oc_ext).Operator(Operator.And))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_capital_floor),
                                   q => qcs.Add(d.Range(p => p.Field("od_regM").GreaterThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_floor) ? 0 : Convert.ToDouble(query.oc_reg_capital_floor))
                                                          .LessThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_ceiling) ? Int32.MaxValue : Convert.ToDouble(query.oc_reg_capital_ceiling)))))
                           .DoWhen(q => q.oc_sort == oc_sort.oc_reg_capital,
                                         q => sfd.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                           .DoWhen(q => q.oc_sort == oc_sort.oc_issue_time,
                                         q => sfd.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<OrgCompanyCombine> Advanced_Query(Company query)
        {
            var qcs = new List<QueryContainer>();
            var d = new QueryContainerDescriptor<Company>();
            var sfd = new SortFieldDescriptor<Company>();

            query.ToMaybe().Do(q => qcs.Add(d.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                    m => m.Prefix(p => p.Field("oc_area").Value("71"))))))
            #region oc_code
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_code),
                                   q => qcs.Add(d.Term(t => t.Field("oc_code").Value(query.oc_code))))
            #endregion
            #region oc_area
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !query.oc_area.Equals("00"),
                                   q => qcs.Add(d.Prefix(p => p.Field("oc_area").Value(query.oc_area))))
            #endregion
            #region oc_number
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_number),
                                   q => qcs.Add(d.Term(t => t.Field("oc_number").Value(query.oc_number))))
            #endregion
            #region oc_art_person
                           .DoWhenOrElse(q => !string.IsNullOrEmpty(q.oc_art_person),
                                         q => qcs.Add(d.MatchPhrase(p => p.Field("od_faRen").Query(query.oc_art_person)) |
                                                Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))),
                                         q => qcs.Add(Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))))
            #endregion
            #region oc_name
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                                   q => qcs.Add(d.Term(p => p.Field("oc_name.oc_name.raw").Value(query.oc_name)) |
                                                d.MatchPhrase(p => p.Field("oc_name").Query(query.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
            #region oc_addr
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_addr),
                                   q => qcs.Add(d.Match(p => p.Field("oc_address").Query(query.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_business),
                                   q => qcs.Add(d.Match(p => p.Field("od_bussinessDes").Query(query.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
                           .DoWhen(q => !string.IsNullOrEmpty(query.oc_reg_type),
                                   q => qcs.Add(d.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_ext),
                                   q =>
                                   {
                                       MatchQuery mq = new MatchQuery() { Field = "od_ext", Query = "吊销 注销 停业", Operator = Operator.Or };
                                       qcs.Add(!mq);
                                       //qcs.Add(d.Bool(b => b.MustNot(m => m.Match(p => p.Field("od_ext").Query(query.oc_ext).Operator(Operator.And)))));
                                       //qcs.Add(d.Bool(p => p.Filter(t => t.DateRange(dr => dr.Field("od_CreateTime").GreaterThan(DateTime.MinValue)))));
                                   })
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_reg_capital_floor) || !string.IsNullOrEmpty(q.oc_reg_capital_ceiling),
                                   q => qcs.Add(d.Range(p => p.Field("od_regM").GreaterThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_floor) ? 0 : Convert.ToDouble(query.oc_reg_capital_floor))
                                                          .LessThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_ceiling) ? Int32.MaxValue : Convert.ToDouble(query.oc_reg_capital_ceiling)))))
                           .DoWhen(q => q.oc_sort == oc_sort.oc_reg_capital,
                                         q => sfd.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                           .DoWhen(q => q.oc_sort == oc_sort.oc_issue_time,
                                         q => sfd.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    //.Query(q => qcs.Aggregate((a, b) => a & b))
                    .Query(q => q.FunctionScore(c => c.Query(qq => qcs.Aggregate((a, b) => a & b))
                                       .Functions(f => f.ScriptScore(ss => ss.Script(sc => sc.Inline("_score + doc['oc_weight'].value * 10"))))))
                    .Sort(sd => sd.Field(f => sfd)));
            //return response;
        }

        public static ISearchResponse<OrgCompanyCombine> Recommend_Query(Company query)
        {
            var qcs = new List<QueryContainer>();
            var d = new QueryContainerDescriptor<Company>();
            var sfd = new SortFieldDescriptor<Company>();

            query.ToMaybe().Do(q => qcs.Add(d.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                    m => m.Prefix(p => p.Field("oc_area").Value("71"))))))
            #region oc_code
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_code),
                           //        q => qcs.Add(d.Term(t => t.Field("oc_code").Value(query.oc_code))))
            #endregion
            #region oc_area
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_area) && !query.oc_area.Equals("0"),
                           //        q => qcs.Add(d.Prefix(p => p.Field("oc_area").Value(query.oc_area))))
            #endregion
            #region oc_number
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_number),
                           //        q => qcs.Add(d.Term(t => t.Field("oc_number").Value(query.oc_number))))
            #endregion
            #region oc_art_person
                           //.DoWhenOrElse(q => !string.IsNullOrEmpty(q.oc_art_person),
                           //              q => qcs.Add(d.MatchPhrase(p => p.Field("od_faRen").Query(query.oc_art_person)) |
                           //                     Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))),
                           //              q => qcs.Add(Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => d.Match(p => p.Field("od_gd").Query(q.oc_stock_holder).Operator(Operator.And)))))
            #endregion
            #region oc_name
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_name),
                           //        q => qcs.Add(d.Term(p => p.Field("oc_name.oc_name.raw").Value(query.oc_name)) |
                           //                     d.Match(p => p.Field("oc_name").Query(query.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
            #region oc_addr
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_addr),
                           //        q => qcs.Add(d.Match(p => p.Field("oc_address").Query(query.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
            #endregion
                           //.DoWhen(q => !string.IsNullOrEmpty(q.oc_business),
                           //        q => qcs.Add(d.Match(p => p.Field("od_bussinessDes").Query(query.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))))
                           //.DoWhen(q => !string.IsNullOrEmpty(query.oc_reg_type),
                           //        q => qcs.Add(d.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And))))
                           .Do(q => qcs.Add(d.Bool(b => b.Must(m => m.Match(ma => ma.Field("od_ext").Query("正常存续").Operator(Operator.And)))
                                                         .Must(m => m.Range(p => p.Field("od_regM").GreaterThanOrEquals(Convert.ToDouble(query.oc_reg_capital_floor))
                                                                                                   .LessThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_ceiling) 
                                                                                                       ? Int32.MaxValue : Convert.ToDouble(query.oc_reg_capital_ceiling)))))));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        [DebuggerStepThrough]
        public static ISearchResponse<OrgCompanyCombine> Advanced_Query_0(Company query)
        {
            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyCombine>(s => s
                    .Index(Es_Consts.Enterprise_Idx)
                    .Type(Es_Consts.Enterprise_Type)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => Q_Concat(!string.IsNullOrEmpty(query.oc_reg_capital_floor),
                                      () => q.Range(p => p.Field("od_regM").GreaterThanOrEquals(Convert.ToDouble(query.oc_reg_capital_floor))
                                                          .LessThanOrEquals(string.IsNullOrEmpty(query.oc_reg_capital_ceiling) ? Int32.MaxValue : Convert.ToDouble(query.oc_reg_capital_ceiling))),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_ext),
                                      () => q.Match(p => p.Field("od_ext").Query(query.oc_ext).Operator(Operator.And)),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_reg_type),
                                      () => q.Match(p => p.Field("od_regType").Query(query.oc_reg_type).Operator(Operator.And)),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_business),
                                      () => q.Match(p => p.Field("od_bussinessDes").Query(query.oc_business).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90))),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_addr),
                                      () => q.Match(p => p.Field("oc_address").Query(query.oc_addr).Slop(2).CutoffFrequency(0.08d).MinimumShouldMatch(MinimumShouldMatch.Percentage(90))),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_name),
                                      () => Get(true, () => q.Term(p => p.Field("oc_name.oc_name.raw").Value(query.oc_name))
                                                          | q.Match(p => p.Field("oc_name").Query(query.oc_name).CutoffFrequency(0.08d).Slop(2).MinimumShouldMatch(MinimumShouldMatch.Percentage(90)))),
                                Q_Concat(true,
                                      () => Get(!string.IsNullOrEmpty(query.oc_art_person), () => q.MatchPhrase(p => p.Field("od_faRen").Query(query.oc_art_person)))
                                                | Get(!string.IsNullOrEmpty(query.oc_stock_holder), () => q.Match(p => p.Field("od_gd").Query(query.oc_stock_holder).Operator(Operator.And))),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_number),
                                      () => q.Term(t => t.Field("oc_number").Value(query.oc_number)),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_area) && !query.oc_area.Equals("0"),
                                      () => q.Prefix(p => p.Field("oc_area").Value(query.oc_area)),
                                Q_Concat(!string.IsNullOrEmpty(query.oc_code),
                                      () => q.Term(t => t.Field("oc_code").Value(query.oc_code)),
                                            q.Bool(b => b.MustNot(m => m.Prefix(p => p.Field("oc_area").Value("81")),
                                                                  m => m.Prefix(p => p.Field("oc_area").Value("91"))))
                                            ))))))
                                        )
                                    )
                                )
                            )
                        )
                    .Sort(st => query.oc_sort == oc_sort.oc_reg_capital
                                ? st.Field(f => f.Field("od_regM").UnmappedType(FieldType.Float).Order(SortOrder.Descending))
                                : st.Field(f => f.Field("oc_issuetime").UnmappedType(FieldType.Date).Order(SortOrder.Descending))
                        )
                    );                                                    
        }
        public static QueryContainer Get(bool flag, Func<QueryContainer> func) => flag ? func() : null;

        public static QueryContainer Q_Concat(bool flag, Func<QueryContainer> getLeft,  QueryContainer right) => flag ? getLeft() & right : right;

        public static ISearchResponse<OrgCompanyBrand> Brand_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("ob_name.ob_name_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("ob_name").Query(q.query_str))))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.cat_s),
                                   q => qcs.Add(qcd.Term(t => t.Field("ob_classNo").Value(q.cat_s))))
                           .DoWhen(q => q.status != 0,
                                   q => qcs.Add(qcd.Term(t => t.Field("ob_status").Value(Enum.GetName(typeof(Brand_Status), q.status)))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("ob_applicationDate").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("ob_applicationDate").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyBrand>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Brand_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("ob_name")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<OrgCompanyBrand> General_Brand_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("ob_name.ob_name_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("ob_name").Query(q.query_str))))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("ob_applicationDate").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("ob_applicationDate").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<OrgCompanyBrand>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Brand_Type_Old)
                    .Aggregations(agg => agg.Terms("term_class", t => t.Field(f => f.ob_classNo)))
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("ob_name")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<OrgCompanyBrand> Brand_Agg_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            query.ToMaybe().DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("ob_name.ob_name_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("ob_name").Query(q.query_str))))));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            var response = client.Search<OrgCompanyBrand>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Brand_Type_Old)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Aggregations(agg => agg.Terms("term_class", t => t.Field(f => f.ob_classNo))));

            return response;
        }

        public static ISearchResponse<CompanyPatent> General_Patent_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("patent_Name.patent_Name_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("patent_Name").Query(q.query_str))))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("sq_date").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("sq_date").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<CompanyPatent>(s => s
                    .Index(Es_Consts.Patent_Idx)
                    .Type(Es_Consts.Patent_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Aggregations(agg => agg.Terms("term_type", t => t.Field("patent_Type"))
                                            .Terms("term_year", t => t.Field(f => f.patent_year))
                                            )
                    //.Aggregations(agg => agg.Terms("term_year", t => t.Field(f => f.patent_year)))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("patent_Name")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        /// <summary>
        /// related query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IMultiSearchResponse Patent_Related_Query(Req_Info_Query query) =>
            Get_Client(DataBus.Elasticsearch_Uris).MultiSearch(ms => ms
            .Index(Es_Consts.Patent_Idx)
            .Type(Es_Consts.Patent_Type_Old)
            .Search<PatentApplicant>("applicant", s => s
                                                   .Type("patentapplicant")
                                                   .Query(q => q.Term(t => t.Field("aname").Value(query.query_str)))
                                                   .From(query.pg_size * (query.pg_index - 1)).Size(query.pg_size))
            .Search<PatentDesigner>("designer", s => s
                                                 .Type("patentdesigner")
                                                 .Query(q => q.Term(t => t.Field("dname").Value(query.query_str))))
            .Search<CompanyPatent>("patent", s => s
                                              .Query(q => q.Bool(b => b.Must(m => m.Term(t => t.Field("patent_Name.patent_Name_raw").Value(query.query_str))
                                                            | m.Match(t => t.Field("patent_Name").Query(query.query_str)))))));

        public static ISearchResponse<CompanyPatent> Patent_Intelli_Query(Req_Info_Query query) =>
            Get_Client(DataBus.Elasticsearch_Uris).Search<CompanyPatent>(s => s
                .Index(Es_Consts.Patent_Idx)
                .Type("companypatent,patentapplicant,patentdesigner")
                .Query(q => q.HasChild<PatentApplicant>(c => c.Name("applicant").Type("patentapplicant").Query(cq => cq.Term(t => t.Field("aname").Value(query.query_str))).Boost(3))
                        | q.HasChild<PatentDesigner>(c => c.Name("designer").Type("patentdesigner").Query(cq => cq.Term(t => t.Field("dname").Value(query.query_str))).Boost(3))
                        | q.Bool(b => b.Name("patent").Must(m => m.Term(t => t.Field("patent_Name.patent_Name_raw").Value(query.query_str))
                                                            | m.Match(t => t.Field("patent_Name").Query(query.query_str))))));


        public static ISearchResponse<CompanyPatent> Patent_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("patent_Name.patent_Name_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("patent_Name").Query(q.query_str))))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.p_type),
                                   q => qcs.Add(qcd.Term(t => t.Field("patent_Type").Value(q.p_type))))
                           .DoWhen(q => q.year > 0,
                                   q => qcs.Add(qcd.Term(t => t.Field("patent_year").Value(q.year))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("sq_date").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("sq_date").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<CompanyPatent>(s => s
                    .Index(Es_Consts.Patent_Idx)
                    .Type(Es_Consts.Patent_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("patent_Name")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<WenshuIndex> Judge_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("jd_title_raw").Value(q.query_str))
                                                            | m.Match(t => t.Field("jd_title").Query(q.query_str))))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("jd_date").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("jd_date").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<WenshuIndex>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Judge_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))//"<font color =\"#FF4400\">", "").Replace("</font>", "")
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("jd_title")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<WenshuIndex> JudgeByCode_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.oc_code),
                                   q => qcs.Add(qcd.Term(t => t.Field("oc_code").Value(q.oc_code))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("jd_date").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("jd_date").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<WenshuIndex>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Judge_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<ShixinIndex> General_Dishonest_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("sx_businessEntity").Value(q.query_str))
                                                            | m.Match(t => t.Field("sx_iname").Query(q.query_str))))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("sx_publishDate").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("sx_publishDate").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<ShixinIndex>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Dishonest_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Aggregations(agg => agg.Terms("term_area", t => t.Field(f => f.sx_areaName)))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(/*fs => fs.Field("sx_businessEntity"),*/ fs => fs.Field("sx_iname")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<ShixinIndex> Dishonest_Query(Req_Info_Query query)
        {
            var qcs = new List<QueryContainer>();
            var qcd = new QueryContainerDescriptor<Req_Info_Query>();
            var sfd = new SortFieldDescriptor<Req_Info_Query>();

            query.ToMaybe().DoWhen(q => q.pg_index < 1,
                                   q => q.Pg_Index(1))
                           .DoWhen(q => !string.IsNullOrEmpty(q.query_str),
                                   q => qcs.Add(qcd.Bool(b => b.Must(m => m.Term(t => t.Field("sx_businessEntity").Value(q.query_str))
                                                            | m.Match(t => t.Field("sx_iname").Query(q.query_str))
                                                            | m.MatchPhrase(t => t.Field("sx_iname").Query(q.query_str).Boost(100).Slop(0).MinimumShouldMatch(MinimumShouldMatch.Percentage(100)))))))
                           .DoWhen(q => !string.IsNullOrEmpty(q.area),
                                   q => qcs.Add(qcd.Term(t => t.Field("sx_areaName").Value(q.area))))
                           .DoWhen(q => q.q_sort == 1,
                                   q => sfd.Field("sx_publishDate").UnmappedType(FieldType.Date).Order(SortOrder.Ascending))
                           .DoWhen(q => q.q_sort == 2,
                                   q => sfd.Field("sx_publishDate").UnmappedType(FieldType.Date).Order(SortOrder.Descending));

            var client = Get_Client(DataBus.Elasticsearch_Uris);
            return client.Search<ShixinIndex>(s => s
                    .Index(Es_Consts.Gsxt_Idx)
                    .Type(Es_Consts.Dishonest_Type_Old)
                    .From(query.pg_size * (query.pg_index - 1))
                    .Take(query.pg_size)
                    .Query(q => qcs.Aggregate((a, b) => a & b))
                    .Highlight(h => h.PreTags("<font color=\"#FF4400\">")
                                     .PostTags("</font>")
                                     .Fields(fs => fs.Field("sx_businessEntity"), fs => fs.Field("sx_iname")))
                    .Sort(sd => sd.Field(f => sfd)));
        }

        public static ISearchResponse<OrgCompanyTradeIndex> CompanyTrade_Query(string trade, string oc_name)
        {
            var resp = 
            Get_Client().Search<OrgCompanyTradeIndex>(s => s
                .Index("orgcompanytrade")
                .Type("companytrade")
                .Query(q => q.Nested(n => n.Name("comtrade").Path(p => p.trades).Query(query => query.Term(t => t.Field("trades.qz_name").Value(trade))))
                          & q.Match(m => m.Field("oc_name").Query(oc_name)))
                .Highlight(hl => hl
                    .PreTags("<font color=\"red\">")
                    .PostTags("</font>")
                    .Fields(
                            f => f.Field("oc_name")))
                );
            return resp;
        }
    }
}
