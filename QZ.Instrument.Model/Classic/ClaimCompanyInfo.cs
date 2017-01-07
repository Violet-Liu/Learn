using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class ClaimCompanyInfo
    {
        /// <summary>
        /// cc_id
        /// </summary>
        public int cc_id { get; set; }

        /// <summary>
        /// 认证用户id[cc_u_uid]
        /// </summary>
        public int cc_u_uid { get; set; }

        /// <summary>
        /// 公司机构代码[cc_oc_code]
        /// </summary>
        public string cc_oc_code { get; set; }

        /// <summary>
        /// 公司名称[cc_oc_name]
        /// </summary>
        public string cc_oc_name { get; set; }

        /// <summary>
        /// 联系人[cc_contacts]
        /// </summary>
        public string cc_contacts { get; set; }

        /// <summary>
        /// 手机[cc_mobile]
        /// </summary>
        public string cc_mobile { get; set; }

        /// <summary>
        /// 电子邮箱[cc_e_mail]
        /// </summary>
        public string cc_e_mail { get; set; }

        /// <summary>
        /// 证件照片json[cc_zj_data]
        /// </summary>
        public string cc_zj_data { get; set; }

        /// <summary>
        /// 状态 0 未审核 1待审核 2审核通过 3审核失败[cc_status]
        /// </summary>
        public int cc_status { get; set; }

        /// <summary>
        /// 审核状态消息
        /// </summary>
        public string cc_statusMsg { get; set; }

        /// <summary>
        /// cc_checkUser
        /// </summary>
        public string cc_checkUser { get; set; }

        /// <summary>
        /// cc_checkTime
        /// </summary>
        public DateTime cc_checkTime { get; set; }

        /// <summary>
        /// cc_createUser
        /// </summary>
        public string cc_createUser { get; set; }

        /// <summary>
        /// cc_createTime
        /// </summary>
        public DateTime cc_createTime { get; set; }

        /// <summary>
        /// 是否有效 0否1是[cc_isvalid]
        /// </summary>
        public bool cc_isvalid { get; set; }

        /// <summary>
        /// 绑定的扩展信息
        /// </summary>
        public OrgCompanyExtensionDataInfo ExtensionData { get; set; }
    }
}
