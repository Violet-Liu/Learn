using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyExtensionDataInfo
    {
        /// <summary>
        /// 自增ID[oc_id]
        /// </summary>
        public int oc_id { get; set; }

        /// <summary>
        /// 机构代码[oc_code]
        /// </summary>
        public string oc_code { get; set; }

        /// <summary>
        /// 数据类型，程序中应该要有对类型的路由比如（10 为行业分类，20为产品分类，30为展会标签，40为前瞻行业，50为联系电话）[oc_type]
        /// </summary>
        public int oc_type { get; set; }

        /// <summary>
        /// 数据类型名称[oc_typeName]
        /// </summary>
        public string oc_typeName { get; set; }

        /// <summary>
        /// 唯一标识[oc_gid]
        /// </summary>
        public Guid oc_gid { get; set; }

        /// <summary>
        /// 数据创建时间[oc_createTime]
        /// </summary>
        public DateTime oc_createTime { get; set; }

        /// <summary>
        /// 创建人uid[oc_create_e_id]
        /// </summary>
        public int oc_create_e_id { get; set; }

        /// <summary>
        /// 创建人名称[oc_create_e_name]
        /// </summary>
        public string oc_create_e_name { get; set; }

        /// <summary>
        /// 数据状态，1为有效，2为被锁定，3为被标记无用，4为已删除[oc_status]
        /// </summary>
        public byte oc_status { get; set; }

        /// <summary>
        /// 扩展数据来源[oc_sources]
        /// </summary>
        public string oc_sources { get; set; }

        /// <summary>
        /// 数据备注[oc_remark]
        /// </summary>
        public string oc_remark { get; set; }

        /// <summary>
        /// 数据内容[oc_data]
        /// </summary>
        public string oc_data { get; set; }

        /// <summary>
        /// 图片集json
        /// </summary>
        public string album_data { get; set; }
    }
}
