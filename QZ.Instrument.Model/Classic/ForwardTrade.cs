/*
 * Sha Jianjian
 * 2016-10-26
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// 前瞻行业实体类
    /// </summary>
    public class ForwardTrade
    {
        public int ft_id { get; set; }
        public string ft_name { get; set; }
        public DateTime ft_date { get; set; }
        public int ft_hashcode { get; set; }
    }
}
