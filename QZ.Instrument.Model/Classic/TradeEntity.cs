using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class TradeEntity
    {
        public string ts_id { get; set; }
        public string ts_name { get; set; }
        public string ts_tc_id { get; set; }
        //public string pc_path { get; set; }
    }

    /// <summary>
    /// 国家产品分类实体
    /// </summary>
    public class ProductEntity
    {
        /// <summary>
        /// code
        /// </summary>
        public string pc_path { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string pc_name { get; set; }
    }
}
