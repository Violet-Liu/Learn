/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 * Paging select reply records that grouped by u_id and topic_id ordered by max(reply_date) desc
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class History_Query
    {
        public int query_id { get; set; }
        public string oc_code { get; set; }
        /// <summary>
        /// not the really oc_name, but the oc_name user input it into textbox
        /// </summary>
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string oc_area_name { get; set; }
        public string oc_number { get; set; }
        public string query_date { get; set; }
        public string oc_addr { get; set; }
        public string oc_reg_type { get; set; }
        public string oc_art_person { get; set; }
        public string oc_stock_holder { get; set; }
        public string oc_business { get; set; }
        public int q_type { get; set; }
        public int oc_sort { get; set; }
        public string oc_ext { get; set; }
        public string oc_reg_capital_floor { get; set; }
        public string oc_reg_capital_ceiling { get; set; }
    }
    public class Ext_SearchHistory
    {
        public List<string> query_list { get; set; }
        public byte q_type { get; set; }
        public static Ext_SearchHistory Default { get { return new Ext_SearchHistory { query_list = new List<string>() }; } }
    }

    public class Browse_Log
    {
        public int browse_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string browse_date { get; set; }
    }

    public class Favorite_Log
    {
        public int favorite_id { get; set; }
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string oc_area { get; set; }
        public string favorite_date { get; set; }
    }
}
