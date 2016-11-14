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
    /// <summary>
    /// 新闻查询信息，用于查询指定新闻信息
    /// </summary>
    public class NewsSearchInfo
    {
        public string OrderByDate = "n_date desc";

        public string OrderBySort = "n_sort";

        public string DefOrder = "n_date desc";

        public string OrderBySortDesc = "n_sort desc, n_date desc";
        /// <summary>
        /// 新闻分类ID
        /// </summary>
        public int? n_cat_id { get; set; }

        public string n_cat_ids { get; set; }
        /// <summary>
        /// 新闻的继承路径，即，新闻路径是以其所属新闻分类路径为开头
        /// </summary>
        public string cat_inheritPath { get; set; }
        /// <summary>
        /// 状态1表示正常，为3表示关闭
        /// </summary>
        public int? n_state { get; set; }

        public int? n_right { get; set; }

        public int? n_ready3 { get; set; }

        public List<string> n_gids { get; set; }

        public string n_title { get; set; }

        public string n_area_prefix { get; set; }

        public string n_analyst { get; set; }

        public List<string> n_analyst_keys { get; set; }

        public string n_analyst_prefix { get; set; }

        public string n_createUser { get; set; }

        public string n_createTime_begin { get; set; }

        public string n_createTime_end { get; set; }

        public string n_date_begin { get; set; }

        public string n_date_end { get; set; }

        public string n_sourceId { get; set; }

        public string n_group { get; set; }

        public string n_group_like { get; set; }

        public string n_gid_not { get; set; }

        public bool n_tradeRoot { get; set; }
        public int? n_tradeId { get; set; }

        /// <summary>
        /// 行业路径
        /// </summary>
        public string t_path { get; set; }

        public string ToWhereString()
        {
            List<string> where = new List<string>();

            if (!string.IsNullOrEmpty(cat_inheritPath))
            {
                where.Add(string.Format("n_cat_id in (select cat_id from NewsCates where left(cat_path,{0})='{1}')", cat_inheritPath.Length, cat_inheritPath));
            }
            else
            {
                if (n_cat_id != null)
                {
                    where.Add(string.Format("n_cat_id = {0}", n_cat_id.Value));
                }
                else if (!string.IsNullOrEmpty(n_cat_ids))
                {
                    where.Add(string.Format("n_cat_id in ({0})", n_cat_ids));
                }
            }

            if (n_gids != null && n_gids.Count > 0)
            {
                where.Add(string.Format("n_gid in ('{0}')", string.Join("','", n_gids.ToArray())));
            }

            if (!string.IsNullOrEmpty(n_gid_not))
            {
                where.Add(string.Format("n_gid <> '{0}'", n_gid_not));
            }

            if (!string.IsNullOrEmpty(n_group))
            {
                where.Add(string.Format("n_group = '{0}'", n_group.ToSafetyStr()));
            }
            else if (!string.IsNullOrEmpty(n_group_like))
            {
                where.Add(string.Format("n_group like '%{0}%'", n_group_like.ToSafetyStr()));
            }

            if (n_state != null)
            {
                where.Add(string.Format("n_state = {0}", n_state.Value));
            }

            if (n_ready3 != null)
            {
                where.Add(string.Format("n_ready3 = {0}", n_ready3.Value));
            }

            if (n_right != null)
            {
                where.Add(string.Format("n_right = {0}", n_right.Value));
            }

            if (!string.IsNullOrEmpty(n_date_begin))
            {
                DateTime dt = Convert.ToDateTime(n_date_begin);
                where.Add(string.Format("n_date >= '{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (!string.IsNullOrEmpty(n_date_end))
            {
                DateTime dt = Convert.ToDateTime(n_date_end);
                if (n_date_end.Length == 10 || n_date_end.EndsWith("00:00"))
                {
                    where.Add(string.Format("n_date <= '{0}'", dt.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")));
                }
                else
                {
                    where.Add(string.Format("n_date <= '{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }

            if (!string.IsNullOrEmpty(n_createTime_begin))
            {
                where.Add(string.Format("n_createTime >= '{0}'", n_createTime_begin.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(n_createTime_end))
            {
                DateTime dt = Convert.ToDateTime(n_createTime_end);
                where.Add(string.Format("n_createTime <= '{0}'", dt.AddDays(1)));
            }

            if (!string.IsNullOrEmpty(n_createUser))
            {
                where.Add(string.Format("n_createUser ='{0}'", n_createUser.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(n_title))
            {
                where.Add(string.Format("n_title like '%{0}%'", n_title.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(n_area_prefix))
            {
                where.Add(string.Format("n_ready1 like '{0}%'", n_area_prefix.ToSafetyStr()));
            }

            if (n_tradeId != null)
            {
                if (this.n_tradeRoot)
                    where.Add(string.Format("n_ready2 like '%{0}'", this.n_tradeId.Value));
                else
                {
                    where.Add(string.Format("n_ready2 like '%{0}%'", this.n_tradeId.Value));
                }
            }
            if (!string.IsNullOrEmpty(n_analyst))
            {
                where.Add(string.Format("n_analyst like '%={0}%'", n_analyst.ToSafetyStr()));
            }

            // 多个keys绑定查询
            if (n_analyst_keys != null && n_analyst_keys.Count > 1)
            {
                StringBuilder sb = new StringBuilder(64);
                sb.Append("(");
                for (int i = 0; i < n_analyst_keys.Count; i++)
                {
                    if (i > 0)
                        sb.Append(" OR ");
                    sb.Append(string.Format("charindex('={0}', n_analyst)>0", n_analyst_keys[i].ToSafetyStr()));
                }
                sb.Append(")");
                where.Add(sb.ToString());
            }

            if (!string.IsNullOrEmpty(n_analyst_prefix))
            {
                where.Add(string.Format("n_analyst like '{0}%'", n_analyst_prefix.ToSafetyStr()));
            }

            if (!string.IsNullOrEmpty(n_sourceId))
            {
                where.Add(string.Format("n_sourceId like '{0}%'", n_sourceId));
            }

            if (!string.IsNullOrEmpty(t_path))
            {
                where.Add(string.Format("dbo.func_Contains(REPLACE(SUBSTRING(n_ready2,1,CHARINDEX(';', n_ready2)), ';', ''), " +
                    "STUFF((SELECT ',' + CAST(t_id AS VARCHAR(10)) FROM QZNewSite.dbo.QZTrades WHERE t_path LIKE '{0}%' ORDER BY t_path FOR XML PATH('')),1,1,'')) = 1", t_path.ToSafetyStr()));
            }

            if (where.Count > 0)
            {
                return string.Concat("where ", string.Join(" and ", where.ToArray()));
            }

            return string.Empty;
        }
    }
}
