using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class VipUserOrderInfo
    {
        /// <summary>
        /// 主键id[mo_id]
        /// </summary>
        public int mo_id { get; set; }

        /// <summary>
        /// 订单编号唯一[mo_orderid]
        /// </summary>
        public string mo_orderid { get; set; }

        /// <summary>
        /// 订单名称[mo_ordername]
        /// </summary>
        public string mo_ordername { get; set; }

        /// <summary>
        /// 重置金额[mo_money]
        /// </summary>
        public decimal mo_money { get; set; }

        /// <summary>
        /// 支付交易码[mo_tradeNo]
        /// </summary>
        public string mo_tradeNo { get; set; }

        /// <summary>
        /// 是否支付成功[mo_paySuccess]
        /// </summary>
        public bool mo_paySuccess { get; set; }

        /// <summary>
        /// 支付类型（微信、支付宝）[mo_payType]
        /// </summary>
        public string mo_payType { get; set; }

        /// <summary>
        /// 支付时间[mo_payTime]
        /// </summary>
        public string mo_payTime { get; set; }

        /// <summary>
        /// 支付信息[mo_payInfo]
        /// </summary>
        public string mo_payInfo { get; set; }

        /// <summary>
        /// 支付状态[mo_state]
        /// </summary>
        public int mo_state { get; set; }

        /// <summary>
        /// 支付备注[mo_remark]
        /// </summary>
        public string mo_remark { get; set; }

        /// <summary>
        /// 用户id[mo_userid]
        /// </summary>
        public int mo_userid { get; set; }

        /// <summary>
        /// 用户名称[mo_userName]
        /// </summary>
        public string mo_userName { get; set; }

        /// <summary>
        /// 创建时间[mo_createTime]
        /// </summary>
        public DateTime mo_createTime { get; set; }

        /// <summary>
        /// mo_ip
        /// </summary>
        public string mo_ip { get; set; }

        /// <summary>
        /// 平台类型 0web  [mo_platformType] 2:android 3:ios
        /// </summary>
        public int mo_platformType { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string mo_mobile { get; set; }

    }
}
