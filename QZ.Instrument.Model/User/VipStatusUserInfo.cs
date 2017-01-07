using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class VipStatusUserInfo
    {
        /// <summary>
        /// vip_id
        /// </summary>
        public int vip_id { get; set; }

        /// <summary>
        /// 用户id[vip_userId]
        /// </summary>
        public int vip_userId { get; set; }

        /// <summary>
        /// vip状态是否有效[vip_status]
        /// </summary>
        public bool vip_status { get; set; }

        /// <summary>
        /// vip有效时间[vip_vaildate]
        /// </summary>
        public DateTime vip_vaildate { get; set; }

        /// <summary>
        /// 数据是否有效 可做删除[vip_isVaild]
        /// </summary>
        public bool vip_isVaild { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string vip_mobile { get; set; }
        /// <summary>
        /// 是否短信通知
        /// </summary>
        public bool vip_isSMS { get; set; }
    }
}
