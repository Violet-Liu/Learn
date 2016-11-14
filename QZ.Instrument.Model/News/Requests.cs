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
using System.Runtime.Serialization;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// 存放一般POST的数据
    /// </summary>
    [DataContract(Namespace = "http://www.qianzhan.com/")]
    public class RequestData
    {
        [DataMember]
        public string v1 { get; set; }

        [DataMember]
        public string details { get; set; }
    }

    /// <summary>
    /// 存放浏览文章与分享日志参数
    /// </summary>
    [DataContract(Namespace = "http://www.qianzhan.com/")]
    public class NewsContentInfo_RD
    {
        [DataMember]
        public string vl_type { get; set; }

        [DataMember]
        public string vl_cateId { get; set; }

        [DataMember]
        public string vl_userName { get; set; }

        [DataMember]
        public string vl_userId { get; set; }

        [DataMember]
        public string vl_screenSize { get; set; }

        [DataMember]
        public string n_gid { get; set; }

        [DataMember]
        public string vl_spiderName { get; set; } // 分享平台（记录分享日志）


    }

    /// <summary>
    /// 存放评论信息
    /// </summary>
    [DataContract(Namespace = "http://www.qianzhan.com/")]
    public class CommentsData
    {
        [DataMember]
        public string puid { get; set; }

        [DataMember]
        public string srcid { get; set; }

        [DataMember]
        public string content { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string url { get; set; }


        [DataMember]
        public string us_id { get; set; }

        [DataMember]
        public string us_name { get; set; }

        [DataMember]
        public string cmt_sourceCateId { get; set; }

        [DataMember]
        public string cmt_platform { get; set; }

        [DataMember]
        public string cmt_device { get; set; }

        [DataMember]
        public string cmt_title { get; set; }

    }
    /// <summary>
    /// 存放图片数据
    /// </summary>
    public class ImgsInfo
    {
        [DataMember]
        public string v1 { get; set; }

        public string src { get; set; }
        public string alt { get; set; }
    }
}
