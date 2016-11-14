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
    public class ShixinIndex
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        public int sx_id { get; set; }

        /// <summary>
        /// 被执行人姓名/名称
        /// </summary>
        public string sx_iname { get; set; }

        /// <summary>
        /// 案号
        /// </summary>
        public string sx_caseCode { get; set; }

        /// <summary>
        /// 身份证号码/组织机构代码
        /// </summary>
        public string sx_cardNum { get; set; }

        /// <summary>
        /// 法定代表人或者负责人姓名
        /// </summary>
        public string sx_businessEntity { get; set; }

        /// <summary>
        /// 执行法院
        /// </summary>
        public string sx_courtName { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string sx_areaName { get; set; }

        /// <summary>
        /// 执行依据文号
        /// </summary>
        public string sx_gistId { get; set; }

        /// <summary>
        /// 立案时间
        /// </summary>
        public DateTime sx_regDate { get; set; }

        /// <summary>
        /// 被执行人的履行情况
        /// </summary>
        public string sx_performance { get; set; }

        /// <summary>
        /// 失信被执行人行为具体情形
        /// </summary>
        public string sx_disruptTypeName { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime sx_publishDate { get; set; }
    }
}
