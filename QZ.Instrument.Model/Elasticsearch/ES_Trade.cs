using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class ES_Trade
    {
        /// <summary>
        /// 行业记录唯一编号，trd_oc_code+'|'+trade_weight
        /// </summary>
        public string trade_id { get; set; }
        public string trd_oc_code { get; set; }
        public string qz_name { get; set; }
        //public int qz_weight { get; set; }
        public string exb_name { get; set; }
        //public int exb_weight { get; set; }
        /// <summary>
        /// 国标行业分类编号
        /// </summary>
        public string gb_code { get; set; }
        public string gb_name { get; set; }
        /// <summary>
        /// 从10 到 1
        /// </summary>
        public int trade_weight { get; set; }
        public string pro_code { get; set; }
        public string pro_name { get; set; }
        //public int pro_weight { get; set; }
    }
}
