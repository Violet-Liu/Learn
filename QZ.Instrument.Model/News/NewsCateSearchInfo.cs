/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.Utility;

namespace QZ.Instrument.Model
{
    public class NewsCateSearchInfo
    {
        public string DefOrder = "cat_path";

        public string cat_name { get; set; }
        public string cat_ctrl { get; set; }
        public string cat_path_start { get; set; }
        public int? cat_path_len { get; set; }
        public string cat_manager { get; set; }
        public int? cat_status { get; set; }
        public string cat_lang { get; set; }

        public string ToWhereString()
        {
            List<string> where = new List<string>();

            if (!string.IsNullOrEmpty(cat_ctrl))
            {
                where.Add(string.Format("cat_ctrl = '{0}'", cat_ctrl.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(cat_lang))
            {
                where.Add(string.Format("cat_lang = '{0}'", cat_lang.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(cat_path_start))
            {
                where.Add(string.Format("cat_path like '{0}%'", cat_path_start.ToSafetyStr()));
            }

            if (cat_status != null)
            {
                where.Add(string.Format("cat_status = {0}", cat_status));
            }

            if (cat_path_len != null)
            {
                where.Add(string.Format("len(cat_path) = {0}", cat_path_len));
            }

            if (!string.IsNullOrEmpty(cat_name))
            {
                where.Add(string.Format("cat_name like '%{0}%'", cat_name.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(cat_manager))
            {
                where.Add(string.Format("cat_manager like '%{0}%'", cat_manager.ToSafetyStr()));
            }

            if (where.Count > 0)
            {
                return string.Concat("where ", string.Join(" and ", where.ToArray()));
            }

            return string.Empty;
        }
    }
}
