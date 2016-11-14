using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Abstract info of exhibition
    /// </summary>
    public class ExhibitAbs
    {
        /// <summary>
        /// md5 16 of e_name
        /// </summary>
        public string e_md { get; set; }
        /// <summary>
        /// exhibit name
        /// </summary>
        public string e_name { get; set; }
        /// <summary>
        /// count of exhibition companies
        /// </summary>
        public int e_count { get; set; }
        /// <summary>
        /// 展厅
        /// </summary>
        public string e_hall { get; set; }
        /// <summary>
        /// 展会所属行业
        /// </summary>
        public string e_trade { get; set; }
        /// <summary>
        /// 展会日期
        /// </summary>
        public string e_date { get; set; } = "-";
        /// <summary>
        /// 展会所在展厅的展位
        /// </summary>
        public string e_booth { get; set; }
    }

    /// <summary>
    /// Detail of exhibition
    /// </summary>
    public class ExhibitDtl
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; } = "";
        /// <summary>
        /// 参展地区
        /// </summary>
        public string area { get; set; } = "";
        /// <summary>
        /// 展会行业
        /// </summary>
        public string trade { get; set; } = "";
        public string name { get; set; } = "";
        /// <summary>
        /// 展厅名
        /// </summary>
        public string hall { get; set; } = "";
        ///// <summary>
        ///// 展会文档路径
        ///// </summary>
        //public string file { get; set; }
    }

    public class ExhibitCompany
    {
        public string oc_name { get; set; } = "";
        public string oc_code { get; set; } = "";
        public string oc_addr { get; set; } = "";
        public string oc_tel { get; set; } = "";
        public string oc_fax { get; set; } = "";
        public string oc_mail { get; set; } = "";
        public string oc_site { get; set; } = "";
        public string oc_area { get; set; } = "";
    }

    public class Trade_Intelli_Tip
    {
        public IList<string> fwd_names = new List<string>();
        public IList<string> exh_names = new List<string>();
        public Dictionary<string, string> gb_trades = new Dictionary<string, string>();
        public Dictionary<string, string> pro_trades = new Dictionary<string, string>();
    }


    public class Resp_Exhibit_List
    {
        public List<ES_Exhibit> exhibits { get; set; }
        public long count { get; set; }
        public Exhibit_Agg aggs { get; set; }
        public static Resp_Company_List Default { get { return new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = 0 }; } }

        public string cost { get; set; }
    }

    public class Exhibit_Agg
    {
        /// <summary>
        /// 按省份统计
        /// </summary>
        public List<Agg_Monad> provinces { get; set; } = new List<Agg_Monad>();

        /// <summary>
        /// 按日期统计
        /// </summary>
        public List<Agg_Monad> dates { get; set; } = new List<Agg_Monad>();

        /// <summary>
        /// 按行业统计
        /// </summary>
        public List<Agg_Monad> trades { get; set; } = new List<Agg_Monad>();
    }
}
