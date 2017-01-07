/*
 * ES mapping entity class
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
    public class ES_Company
    {
        /// <summary>
        /// 机构代码
        /// </summary>
        public string oc_code { get; set; }
        public string oc_creditcode { get; set; } = string.Empty;
        /// <summary>
        /// 登记号
        /// </summary>
        public string oc_number { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string oc_area { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string oc_areaname { get; set; }
        /// <summary>
        /// 登记注册机构名
        /// </summary>
        public string oc_regorgname { get; set; }
        public string oc_name { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string oc_address { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public string oc_companytype { get; set; }
        /// <summary>
        /// 公司权重值
        /// </summary>
        public double oc_weight { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime oc_changetime { get; set; }
        /// <summary>
        /// 证书有效期开始时间
        /// </summary>
        public DateTime oc_issuetime { get; set; }
        /// <summary>
        /// 证书过期开始时间
        /// </summary>
        public DateTime oc_invalidtime { get; set; }
        /// <summary>
        /// 股东
        /// </summary>
        public IList<string> od_gds { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public IList<string> oc_members { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string od_faren { get; set; } = string.Empty;
        /// <summary>
        /// 注册资本
        /// </summary>
        public decimal od_regm { get; set; } = 0;
        /// <summary>
        /// 注册资本
        /// </summary>
        public string od_regmoney { get; set; } = string.Empty;
        /// <summary>
        /// 注册类型
        /// </summary>
        public string od_regtype { get; set; } = string.Empty;
        /// <summary>
        /// ext备注
        /// </summary>
        public string od_ext { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态,1 -> 正常，9 -> 非正常，0 -> 未知
        /// </summary>
        public byte oc_status { get; set; }
        /// <summary>
        /// 公司业务
        /// </summary>
        public string od_bussiness { get; set; } = string.Empty;
        public DateTime od_createtime { get; set; } = DateTime.MinValue;

        ///// <summary>
        ///// 公司商标，以 '|' 分隔
        ///// </summary>
        //public string oc_brands { get; set; } = string.Empty;
        ///// <summary>
        ///// 公司专利，以 '|' 分隔
        ///// </summary>
        //public string oc_patents { get; set; } = string.Empty;

        public IList<string> oc_brands { get; set; }
        public IList<string> oc_patents { get; set; }
        public IList<string> oc_sites { get; set; }
        public IList<string> gb_codes { get; set; }
        public IList<string> pro_codes { get; set; }
        public IList<string> fwd_names { get; set; }
        public IList<string> exh_names { get; set; }
        /// <summary>
        /// 国标主分类
        /// </summary>
        public string gb_cat { get; set; }

        public IList<string> oc_tels { get; set; }
        public IList<string> oc_mails { get; set; }
        //public List<Comtrade> trades { get; set; }
    }

    public class ES_Patent
    {
        /// <summary>
        /// 唯一标识，patent_no + '|' + patent_gkh
        /// </summary>
        public string patent_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        /// <summary>
        /// 专利号
        /// </summary>
        public string patent_no { get; set; }
        /// <summary>
        /// 公开号
        /// </summary>
        public string patent_gkh { get; set; }
        public string patent_name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string patent_type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string patent_status { get; set; }
        public string patent_img { get; set; }
        /// <summary>
        /// 申请人（公司）
        /// </summary>
        public List<string> patent_sqr { get; set; }
        /// <summary>
        /// 专利申请人（公司）地址
        /// </summary>
        public string patent_addr { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime patent_date { get; set; }
        /// <summary>
        /// 专利年份
        /// </summary>
        public int patent_year { get; set; }
        /// <summary>
        /// 优先权，以 '[\|;]' 分隔
        /// </summary>
        public List<string> patent_yxq { get; set; }
        /// <summary>
        /// 设计人，以 '[\|;]' 分隔
        /// </summary>
        public List<string> patent_sjr { get; set; }
        /// <summary>
        /// 代理人，以 '[\|;]' 分隔
        /// </summary>
        public List<string> patent_dlr { get; set; }
        /// <summary>
        /// 代理机构
        /// </summary>
        public List<string> patent_dljg { get; set; }
        /// <summary>
        /// 分类号，以 '[\|;]' 分隔
        /// </summary>
        public List<string> patent_flh { get; set; }
        /// <summary>
        /// 申请公司所在地区，外国为国名，中国则一般为省名
        /// </summary>
        public string patent_area { get; set; }
        /// <summary>
        /// 申请公司所在地区邮编，只包括国内
        /// </summary>
        public string patent_postcode { get; set; }
    }

    public class ES_Judge
    {
        public string jd_id { get; set; }
        ///// <summary>
        ///// 判决摘要
        ///// </summary>
        //public string jd_intro { get; set; } = "无";
        /// <summary>
        /// 判决法院
        /// </summary>
        public string jd_court { get; set; } = "未知";

        public string jd_title { get; set; }
        /// <summary>
        /// 案号
        /// </summary>
        public string jd_num { get; set; } = "-";

        public string jd_oc_code { get; set; } = "000000000";
        public DateTime jd_date { get; set; }
        public string jd_program { get; set; } = "-";
    }

    public class ES_Outcome<T> where T : class
    {
        /// <summary>
        /// (just returned) matched documents for a search
        /// </summary>
        public IList<ES_Doc<T>> docs { get; set; }
        
        /// <summary>
        /// time taken for a search(unit: s)
        /// </summary>
        public float took { get; set; }
        /// <summary>
        /// for a concrete search condition, total count of all matched documents
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// aggretation for all matched documents
        /// </summary>
        public Aggs aggs { get; set; }
    }

    public class ES_Dishonest
    {
        public int sx_id { get; set; }
        /// <summary>
        /// 被执行人名称，如果是公司，此字段值为"非自然人"
        /// </summary>
        public string sx_pname { get; set; }
        /// <summary>
        /// 被执行公司名称，如是非企业(即，自然人)，则公司名称为 "无"
        /// </summary>
        public string sx_oc_name { get; set; }
        /// <summary>
        /// 案号
        /// </summary>
        public string sx_casecode { get; set; }
        /// <summary>
        /// 身份证号码/组织机构代码
        /// </summary>
        public string sx_cardnum { get; set; }
        /// <summary>
        /// 法定代表人或者负责人姓名，如果是自然人，则值为 "无"
        /// </summary>
        public string sx_entity { get; set; }
        /// <summary>
        /// 执行法院
        /// </summary>
        public string sx_court { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string sx_areaname { get; set; }
        /// <summary>
        /// 执行依据文号
        /// </summary>
        public string sx_gistid { get; set; }
        /// <summary>
        /// 被执行人的履行情况
        /// </summary>
        public string sx_performance { get; set; }
        /// <summary>
        /// 失信被执行人行为具体情形
        /// </summary>
        public string sx_disrupt { get; set; }
        /// <summary>
        /// 立案时间
        /// </summary>
        public DateTime sx_regdate { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime sx_pubdate { get; set; }
    }

    public class ES_Brand
    {
        public string ob_id { get; set; }
        /// <summary>
        /// Composed by ob_regno|ob_classno
        /// </summary>
        public string ob_regclass { get; set; }
        public string ob_oc_code { get; set; }
        //public string ob_oc_name { get; set; }
        /// <summary>
        /// register number
        /// </summary>
        public string ob_regno { get; set; }
        /// <summary>
        /// class number
        /// </summary>
        public string ob_classno { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ob_date { get; set; }
        public string ob_name { get; set; }
        ///// <summary>
        ///// 分类名称，跟分类号对应
        ///// </summary>
        //public string ob_classname { get; set; }
        public string ob_status { get; set; } = string.Empty;
        /// <summary>
        /// 代理人名称
        /// </summary>
        public string ob_dlrmc { get; set; }
        public string ob_img { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string ob_proposer { get; set; }
        public string ob_proposeraddr { get; set; }
    }

    public class ES_Doc<T>
    {
        public T doc { get; set; }
        public IDictionary<string, string> hits { get; set; }
    }

    public class Aggs
    {
        /// <summary>
        /// area aggregation
        /// </summary>
        public IList<Agg> area { get; set; }
        /// <summary>
        /// date aggregation
        /// </summary>
        public IList<Agg> date { get; set; }
        /// <summary>
        /// status aggregation
        /// </summary>
        public IList<Agg> status { get; set; }
        /// <summary>
        /// type aggregation
        /// </summary>
        public IList<Agg> type { get; set; }
    }

    public class Agg
    {
        ///// <summary>
        ///// 标签，用户UI控件显示
        ///// </summary>
        //public string label { get; set; }
        /// <summary>
        /// 某分类对应的值，用于筛选查询
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 某分类对应的统计数量
        /// </summary>
        public long count { get; set; }
        public Agg(string value, long count)
        {
            this.value = value;
            this.count = count;
        }
    }

    public class Company_Parts
    {
        public string area;
        public string key;
        public string trade;
        public string type;

        //public static IDictionary<ComType, IList<ComType>> Stock
        public static IDictionary<ComType, IList<ComType>> Limit;
    }

    public enum ComType
    {
        /// <summary>
        /// None
        /// </summary>
        N,
        /// <summary>
        /// Stock
        /// </summary>
        S,
        /// <summary>
        /// Limit
        /// </summary>
        L,
        /// <summary>
        /// Company
        /// </summary>
        C
    }
}
