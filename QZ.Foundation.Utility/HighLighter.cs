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

namespace QZ.Foundation.Utility
{
    public class HighLighter
    {
        private static string pre_tag = "<font color=\"red\">";
        private static string post_tag = "</font>";

        public static string HighLight(string src, string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(src))
                return src;

            var temp = src;
            var set = new HashSet<char>();
            foreach(var key in keyword)
            {
                if (!set.Contains(key))
                    set.Add(key);
            }
            var sb = new StringBuilder();
            foreach(var ch in src)
            {
                if (set.Contains(ch))
                    sb.Append(pre_tag).Append(ch).Append(post_tag);
                else
                    sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}
