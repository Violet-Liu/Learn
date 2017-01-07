/*
 * This class buffers some ES query request descriptors. At each time of constructing a request descriptor, one in this buffer can be reused and it just only needs to modify the
 * query description contained into the request descriptor.
 * 
 * Be careful to the notaions of these methods' names, where 'G' usually denotes 'General-Search', 'Ini' denotes the first-page-search while 'NonIni' means scroll-page-search,
 * 'A' represents 'Ascii-Search-Keyword' and 'U' represents 'Unicode-Search-Keyword'.
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
using QZ.Instrument.Model;

namespace QZ.Instrument.Client
{
    public class ES_Buffer
    {
        #region auxiliary methods
        private static HighlightDescriptor<T> HL_Create<T>(HighlightDescriptor<T> hl, IEnumerable<string> names) where T : class => hl
            .PreTags("<font color=\"red\">")
            .PostTags("</font>")
            .Fields(names.Select<string, Func<HighlightFieldDescriptor<T>, IHighlightField>>(n => f => f.Field(n)).ToArray());

        #endregion

        #region company
        public const string Company_FunScript = "_score * (Math.log(doc['oc_weight'].value + 1) * 100 + 1)";
        private static Func<AggregationContainerDescriptor<ES_Brand>, IAggregationContainer> Company_Agg =
            agg => agg.DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1))
                        .Terms("cat", t => t.Field("gb_cat").Size(22))
                        .Terms("area", t => t.Field("prefix_area").Size(32))
                        .Terms("type", t => t.Field("oc_companytype").Size(12))
                        .Terms("status", t => t.Field("oc_status"))
                        .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(100), rs => rs.From(100.1).To(500), rs => rs.From(500.1).To(1000),
                                                rs => rs.From(1000.1)));
        /// <summary>
        /// Initial descriptor of general search
        /// </summary>
        public static SearchDescriptor<ES_Company> Company_GIni = new SearchDescriptor<ES_Company>()
            .Index(ES_Meta.Company_Index).Type(ES_Meta.Company_Type)
            .Aggregations(agg => agg.DateHistogram("date", t => t.Field("oc_issuetime").Interval(DateInterval.Year).MinimumDocumentCount(1))
                                        .Terms("cat", t => t.Field("gb_cat").Size(22))
                                        .Terms("area", t => t.Field("prefix_area").Size(32))
                                        .Terms("type", t => t.Field("oc_companytype").Size(12))
                                        .Terms("status", t => t.Field("oc_status"))
                                        .Range("regm", r => r.Field("od_regm").Ranges(rs => rs.To(100), rs => rs.From(100.1).To(500), rs => rs.From(500.1).To(1000),
                                                                rs => rs.From(1000.1))));
        ///// <summary>
        ///// Roll descriptor of general search
        ///// </summary>
        //public static SearchDescriptor<ES_Company> Company_GRoll = new SearchDescriptor<ES_Company>()
        //    .Index(ES_Meta.Company_Index).Type(ES_Meta.Company_Type)
        //    .Highlight();

        #endregion


        #region brand
        private static Func<AggregationContainerDescriptor<ES_Brand>, IAggregationContainer> Brand_Agg =
            agg => agg.DateHistogram("date", t => t.Field("ob_date").Interval(DateInterval.Year).MinimumDocumentCount(1))
                      .Terms("type", t => t.Field("ob_classno").Size(45))
                      .Terms("status", t => t.Field("ob_status").Size(30))
                      ;

        public static SearchDescriptor<ES_Brand> Brand_GA_Ini = new SearchDescriptor<ES_Brand>()
            .Index(ES_Meta.Brand_Index).Type(ES_Meta.Brand_Type)
            .Highlight(hl => HL_Create(hl, Brand_Meta.A_Fields.Select(f => f.name.Split('.')[0])))
            .Aggregations(Brand_Agg);

        public static SearchDescriptor<ES_Brand> Brand_GU_Ini = new SearchDescriptor<ES_Brand>()
            .Index(ES_Meta.Brand_Index).Type(ES_Meta.Brand_Type)
            .Highlight(hl => HL_Create(hl, Brand_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)))
            .Aggregations(Brand_Agg);

        public static SearchDescriptor<ES_Brand> Brand_GA_NonIni = new SearchDescriptor<ES_Brand>()
            .Index(ES_Meta.Brand_Index).Type(ES_Meta.Brand_Type)
            .Highlight(hl => HL_Create(hl, Brand_Meta.A_Fields.Select(f => f.name.Split('.')[0])));

        public static SearchDescriptor<ES_Brand> Brand_GU_NonIni = new SearchDescriptor<ES_Brand>()
            .Index(ES_Meta.Brand_Index).Type(ES_Meta.Brand_Type)
            .Highlight(hl => HL_Create(hl, Brand_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)));

        #endregion

        #region patent
        private static Func<AggregationContainerDescriptor<ES_Patent>, IAggregationContainer> Patent_Agg =
            agg => agg.Terms("date.int", t => t.Field("patent_year").Size(30))
                      .Terms("status", t => t.Field("patent_status"))
                      .Terms("type", t => t.Field("patent_type"))
                      .Terms("area", t => t.Field("patent_area"));

        public static SearchDescriptor<ES_Patent> Patent_GA_Ini = new SearchDescriptor<ES_Patent>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Patent_Type)
            .Highlight(hl => HL_Create(hl, Patent_Meta.A_Fields.Select(f => f.name.Split('.')[0])))
            .Aggregations(Patent_Agg);

        public static SearchDescriptor<ES_Patent> Patent_GU_Ini = new SearchDescriptor<ES_Patent>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Patent_Type)
            .Highlight(hl => HL_Create(hl, Patent_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)))
            .Aggregations(Patent_Agg);

        public static SearchDescriptor<ES_Patent> Patent_GA_NonIni = new SearchDescriptor<ES_Patent>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Patent_Type)
            .Highlight(hl => HL_Create(hl, Patent_Meta.A_Fields.Select(f => f.name.Split('.')[0])));

        public static SearchDescriptor<ES_Patent> Patent_GU_NonIni = new SearchDescriptor<ES_Patent>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Patent_Type)
            .Highlight(hl => HL_Create(hl, Patent_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)));
        #endregion

        #region judge
        private static Func<AggregationContainerDescriptor<ES_Judge>, IAggregationContainer> Judge_Agg =
            agg => agg.Terms("status", t => t.Field("jd_program"))
                      .DateHistogram("date", t => t.Field("jd_date").Interval(DateInterval.Year).MinimumDocumentCount(1));

        public static SearchDescriptor<ES_Judge> Judge_GA_Ini = new SearchDescriptor<ES_Judge>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Judge_Type)
            .Highlight(hl => hl.PreTags("< font color =\"red\">").PostTags("</font>").Fields(f => f.Field("jd_oc_code")))
            .Aggregations(Judge_Agg);

        public static SearchDescriptor<ES_Judge> Judge_GU_Ini = new SearchDescriptor<ES_Judge>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Judge_Type)
            .Highlight(hl => HL_Create(hl, Judge_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)))
            .Aggregations(Judge_Agg);

        public static SearchDescriptor<ES_Judge> Judge_GU_NonIni = new SearchDescriptor<ES_Judge>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Judge_Type)
            .Highlight(hl => HL_Create(hl, Judge_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)));

        public static SearchDescriptor<ES_Judge> Judge_GA_NonIni = new SearchDescriptor<ES_Judge>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Judge_Type)
            .Highlight(hl => hl.PreTags("< font color =\"red\">").PostTags("</font>").Fields(f => f.Field("jd_oc_code")));
        #endregion

        #region dishonest
        private static Func<AggregationContainerDescriptor<ES_Dishonest>, IAggregationContainer> Dishonest_Agg =
            agg => agg.DateHistogram("date", t => t.Field("sx_pubdate").Interval(DateInterval.Year).MinimumDocumentCount(1))
                      .Terms("status", t => t.Field("sx_performance"))
                      .Terms("area", t => t.Field("sx_areaname").Size(32));

        public static SearchDescriptor<ES_Dishonest> Dishonest_GA_Ini = new SearchDescriptor<ES_Dishonest>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Dishonest_Type)
            .Highlight(hl => hl.PreTags("< font color =\"red\">").PostTags("</font>").Fields(f => f.Field("sx_cardnum")))
            .Aggregations(Dishonest_Agg);

        public static SearchDescriptor<ES_Dishonest> Dishonest_GU_Ini = new SearchDescriptor<ES_Dishonest>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Dishonest_Type)
            .Highlight(hl => HL_Create(hl, Dishonest_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)))
            .Aggregations(Dishonest_Agg);

        public static SearchDescriptor<ES_Dishonest> Dishonest_GA_NonIni = new SearchDescriptor<ES_Dishonest>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Dishonest_Type)
            .Highlight(hl => hl.PreTags("< font color =\"red\">").PostTags("</font>").Fields(f => f.Field("sx_cardnum")));

        public static SearchDescriptor<ES_Dishonest> Dishonest_GU_NonIni = new SearchDescriptor<ES_Dishonest>()
            .Index(ES_Meta.Company_Ext_Index).Type(ES_Meta.Dishonest_Type)
            .Highlight(hl => HL_Create(hl, Dishonest_Meta.U_Fields.Where(f => !f.name.Contains('.')).Select(f => f.name)));

        #endregion
    }
}
