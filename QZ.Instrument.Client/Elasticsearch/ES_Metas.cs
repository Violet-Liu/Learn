/*
 * This class shows all meta datas in Elasticsearch, such as index names, index types, fields of types and so on.
 * These infos is helpful when dynamicly constructing query descriptors
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

namespace QZ.Instrument.Client
{
    public class ES_Meta
    {
        #region next generation
        public const string Company_Index = "company_nextgen";
        public const string Company_Type = "company";

        public const string Company_Ext_Index = "company_ext";
        public const string Exhibit_Type = "exhibit";
        public const string Dishonest_Type = "dishonest";
        public const string Brand_Type = "brand";
        public const string Patent_Type = "patent";
        public const string Judge_Type = "judge";
        #endregion
    }

    public class ES_Company_MD
    {
        public static IList<ES_Field> A_Fields { get; private set; }
        public static IList<ES_Field> U_Fields { get; private set; }

        public static IList<string> Agg_Fields { get; private set; }
    }


    public class Brand_Meta
    {
        public static readonly IList<ES_Field> A_Fields = new List<ES_Field>
        {
            new ES_Field("ob_regno"),               // term
            new ES_Field("ob_name.ob_name_raw"),    // term
            new ES_Field("ob_classno"),             // term
            new ES_Field("ob_oc_code")              // term
        };


        public static readonly IList<ES_Field> U_Fields = new List<ES_Field>
        {
            new ES_Field("ob_name.ob_name_raw", boost: 100),            // term
            new ES_Field("ob_proposer.ob_proposer_raw", boost: 100),    // term
            new ES_Field("ob_dlrmc", Analyzer.IK),
            new ES_Field("ob_proposer", Analyzer.IK),
            new ES_Field("ob_name", Analyzer.IK, boost: 20),
            new ES_Field("ob_proposeraddr", Analyzer.IK)
        };
        public static readonly ES_Field Name = new ES_Field("ob_name");
    }

    public class Patent_Meta
    {
        public static readonly IList<ES_Field> A_Fields = new List<ES_Field>
        {
            new ES_Field("oc_code", boost: 10),
            new ES_Field("patent_no", boost: 10),
            new ES_Field("patent_gkh", boost: 10),
            new ES_Field("patent_year", boost: 10),
            new ES_Field("patent_postcode", Analyzer.Standard),
            new ES_Field("patent_flh", Analyzer.Unknown, 5),
            new ES_Field("patent_yxq", Analyzer.Unknown, 5)
        };

        public static readonly IList<ES_Field> U_Fields = new List<ES_Field>
        {
            new ES_Field("patent_name.patent_name_raw", boost: 100),
            new ES_Field("patent_name", Analyzer.IK, boost: 40),
            new ES_Field("oc_name", Analyzer.IK, boost: 20),
            new ES_Field("patent_type", boost: 20),
            new ES_Field("patent_status", boost: 20),
            new ES_Field("patent_sqr", Analyzer.IK, boost: 20),
            new ES_Field("patent_sjr", Analyzer.Unknown, boost: 60),
            new ES_Field("patent_dlr", Analyzer.Unknown, boost: 60),
            new ES_Field("patent_dljg", Analyzer.IK),
            new ES_Field("patent_dljg.patent_dljg_raw", boost: 60)

        };
        public static readonly ES_Field Name = new ES_Field("patent_name");
    }

    public class Judge_Meta
    {
        //public static readonly IList<ES_Field> A_Fields = new List<ES_Field>
        //{
        //    new ES_Field("jd_oc_code")
        //};
        public static readonly ES_Field A_Field = new ES_Field("jd_oc_code");

        public static readonly IList<ES_Field> U_Fields = new List<ES_Field>
        {
            new ES_Field("jd_program", boost: 60),
            new ES_Field("jd_title.name_raw", boost: 100),
            new ES_Field("jd_title", boost: 10),
            new ES_Field("jd_court", Analyzer.IK, 5),
            new ES_Field("jd_num", Analyzer.IK)
        };

        public static readonly ES_Field Name = new ES_Field("jd_title");
    }

    public class Dishonest_Meta
    {
        public static readonly ES_Field A_Field = new ES_Field("sx_cardnum");
 
        public static readonly IList<ES_Field> U_Fields = new List<ES_Field>
        {
            new ES_Field("sx_entity", boost: 50),
            new ES_Field("sx_pname", boost: 50),
            new ES_Field("sx_performance", boost: 20),
            new ES_Field("sx_oc_name.name_raw", boost: 60),
            new ES_Field("sx_oc_name", Analyzer.IK, boost: 10),
            new ES_Field("sx_court", Analyzer.IK),
            new ES_Field("sx_areaname", boost: 30)
        };
        public static readonly ES_Field Name = new ES_Field("sx_oc_name");
    }

    public class ES_Field
    {
        public string name { get; private set; }

        /// <summary>
        /// May be used in future
        /// </summary>
        //public ES_Cat cat { get; private set; }
        public Analyzer ana { get; private set; }

        public int boost { get; private set; }
        public ES_Field(string name, Analyzer ana = Analyzer.Term, int boost = 1)
        {
            this.name = name;
            this.ana = ana;
            this.boost = boost;
        }


        //public ES_Field(string name, ES_Cat cat, Analyzer ana = Analyzer.Term)
        //{
        //    this.name = name;
        //    this.cat = cat;
        //    this.ana = ana;
        //}
    }

    public enum Analyzer
    {
        Unknown = 0,
        /// <summary>
        /// do not segment
        /// </summary>
        Term,
        /// <summary>
        /// segment with ik
        /// </summary>
        IK,
        /// <summary>
        /// segment with delimiter @"[-\|,\s]"
        /// </summary>
        Char,
        /// <summary>
        /// segment with delimiter @"@"
        /// </summary>
        Mail,
        /// <summary>
        /// segment with delimiter @"[-\./\:]"
        /// </summary>
        Url,
        /// <summary>
        /// segment with delimiter @"\p{ASCII}"
        /// </summary>
        Ascii,
        /// <summary>
        /// segment standardly, usually for prefix-match
        /// </summary>
        Standard,
        Pinyin,
        Geo,
    }

    /// <summary>
    /// category of field content
    /// </summary>
    public enum ES_Cat
    {
        Unknown = 0,
        /// <summary>
        /// Number
        /// </summary>
        Num = 1,
        /// <summary>
        /// date
        /// </summary>
        Date = 2,
        /// <summary>
        /// Geo
        /// </summary>
        Geo = 3,

        /// <summary>
        /// In range of Ascii
        /// </summary>
        Ascii = 8,
        /// <summary>
        /// In range of Unicode
        /// </summary>
        Unicode = 16,
        ///// <summary>
        ///// Pinyin of Chinese
        ///// </summary>
        //Pinyin = 32,
        
    }
}
