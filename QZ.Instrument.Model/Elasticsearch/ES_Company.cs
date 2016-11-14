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
    public class ES_Company
    {
        /// <summary>
        /// 机构代码
        /// </summary>
        public string oc_code { get; set; }
        public string oc_creditcode { get; set; } = string.Empty;
        /// <summary>
        /// 登记号
        /// </summary>
        public string oc_number { get; set; }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string oc_area { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string oc_areaname { get; set; }
        /// <summary>
        /// 登记注册机构名
        /// </summary>
        public string oc_regorgname { get; set; }
        public string oc_name { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string oc_address { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public string oc_companytype { get; set; }
        /// <summary>
        /// 公司权重值
        /// </summary>
        public double oc_weight { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime oc_changetime { get; set; }
        /// <summary>
        /// 证书有效期开始时间
        /// </summary>
        public DateTime oc_issuetime { get; set; }
        /// <summary>
        /// 证书过期开始时间
        /// </summary>
        public DateTime oc_invalidtime { get; set; }
        /// <summary>
        /// 股东
        /// </summary>
        public IList<string> od_gds { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public IList<string> oc_members { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string od_faren { get; set; } = string.Empty;
        /// <summary>
        /// 注册资本
        /// </summary>
        public decimal od_regm { get; set; } = 0;
        /// <summary>
        /// 注册资本
        /// </summary>
        public string od_regmoney { get; set; } = string.Empty;
        /// <summary>
        /// 注册类型
        /// </summary>
        public string od_regtype { get; set; } = string.Empty;
        /// <summary>
        /// ext备注
        /// </summary>
        public string od_ext { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态,1 -> 正常，9 -> 非正常，0 -> 未知
        /// </summary>
        public byte oc_status { get; set; }
        /// <summary>
        /// 公司业务
        /// </summary>
        public string od_bussiness { get; set; } = string.Empty;
        public DateTime od_createtime { get; set; } = DateTime.MinValue;

        ///// <summary>
        ///// 公司商标，以 '|' 分隔
        ///// </summary>
        //public string oc_brands { get; set; } = string.Empty;
        ///// <summary>
        ///// 公司专利，以 '|' 分隔
        ///// </summary>
        //public string oc_patents { get; set; } = string.Empty;

        public IList<string> oc_brands { get; set; }
        public IList<string> oc_patents { get; set; }
        public IList<string> oc_sites { get; set; }
        public IList<string> gb_codes { get; set; }
        public IList<string> pro_codes { get; set; }
        public IList<string> fwd_names { get; set; }
        public IList<string> exh_names { get; set; }
        /// <summary>
        /// 国标主分类
        /// </summary>
        public string gb_cat { get; set; }

        public IList<string> oc_tels { get; set; }
        public IList<string> oc_mails { get; set; }
        //public List<Comtrade> trades { get; set; }
    }

    public class Comtrade
    {
        public string qz_name { get; set; }
        public string exb_name { get; set; }
        public string gb_name { get; set; }
        public string gb_code { get; set; }
        public string p_name { get; set; }
        public string p_code { get; set; }
        public double trd_weight { get; set; }
    }
}
