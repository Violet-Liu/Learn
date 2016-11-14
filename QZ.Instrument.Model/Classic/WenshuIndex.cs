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

namespace QZ.Instrument.Model
{
    public class WenshuIndex
    {
        #region jd_id
        string _jd_id;
        /// <summary>
        /// 文档ID
        /// </summary>
        public string jd_id
        {
            get { return _jd_id; }
            set { _jd_id = value; }
        }
        #endregion

        public Guid jd_guid { get; set; }

        #region jd_title
        string _jd_title;
        /// <summary>
        /// 文档标题，需要索引
        /// </summary>
        public string jd_title
        {
            get { return _jd_title; }
            set { _jd_title = value; }
        }
        #endregion

        #region jd_ch
        string _jd_ch;
        /// <summary>
        /// 判决法院，不需要索引
        /// </summary>
        public string jd_ch
        {
            get { return _jd_ch; }
            set { _jd_ch = value; }
        }
        #endregion

        #region jd_num
        string _jd_num;
        /// <summary>
        /// 案号
        /// </summary>
        public string jd_num
        {
            get { return _jd_num; }
            set { _jd_num = value; }
        }
        #endregion

        #region oc_code
        string _oc_code;
        /// <summary>
        /// jd_title
        /// </summary>
        public string oc_code
        {
            get { return _oc_code; }
            set { _oc_code = value; }
        }
        #endregion

        #region jd_date
        DateTime _jd_date;
        /// <summary>
        /// jd_id
        /// </summary>
        public DateTime jd_date
        {
            get { return _jd_date; }
            set { _jd_date = value; }
        }
        #endregion
    }
}
