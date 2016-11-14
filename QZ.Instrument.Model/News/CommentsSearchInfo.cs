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
    public class CommentsSearchInfo
    {
        private int _page = 1;
        /// <summary>
        /// 评论所在的页码
        /// </summary>
        public int page
        {
            get { return _page; }
            set { _page = value; }
        }

        private int _pagesize = 10;
        /// <summary>
        /// 页最大评论数
        /// </summary>
        public int pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }

        public string columns = "*";
        private string _defOrder = " cmt_createTime desc ";
        /// <summary>
        /// 默认排序
        /// </summary>
        public string DefOrder
        {
            get { return _defOrder; }
            set { _defOrder = value; }
        }
        public string cmt_title { get; set; }
        public int? cmt_accept { get; set; }
        public int? cmt_status { get; set; }
        public int? cmt_createUserID { get; set; }
        public string cmt_createUser { get; set; }
        public string cmt_sourceId { get; set; }
        public string cmt_ip { get; set; }
        public string cmt_createTime_start { get; set; }
        public string cmt_createTime_end { get; set; }

        public string ToWhereString()
        {
            List<string> where = new List<string>();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(cmt_title))
            {
                where.Add("cmt_title='" + cmt_title.ToSafetyStr() + "'");
            }
            if (!string.IsNullOrEmpty(cmt_ip))
            {
                where.Add("cmt_ip = '" + cmt_ip.ToSafetyStr() + "'");
            }
            if (!string.IsNullOrEmpty(cmt_sourceId))
            {
                where.Add("cmt_sourceId='" + cmt_sourceId.ToSafetyStr() + "'");
            }
            if (!string.IsNullOrEmpty(cmt_createUser))
            {
                where.Add("cmt_createUser='" + cmt_createUser.ToSafetyStr() + "'");
            }
            if (!string.IsNullOrEmpty(cmt_createTime_start))
            {
                where.Add("cmt_createTime >='" + cmt_createTime_start.ToSafetyStr() + "'");
            }
            if (!string.IsNullOrEmpty(cmt_createTime_end))
            {
                where.Add("cmt_createTime <='" + cmt_createTime_end.ToSafetyStr() + "'");
            }

            if (cmt_accept != null)
            {
                where.Add("cmt_accept=" + cmt_accept.ToString());
            }
            if (cmt_status != null)
            {
                where.Add("cmt_status=" + cmt_status.ToString());
            }
            if (cmt_createUserID != null)
            {
                where.Add("cmt_createUserID=" + cmt_createUserID.ToString());

            }

            if (where.Count > 0)
            {
                return string.Concat(str, " where ", string.Join(" and ", where.ToArray()));
            }

            return str;
        }
    }
}
