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
    public class VisitLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~VisitLogInfo()
        {
            Dispose(false);
        }

        /// <summary>
        /// 调用虚拟的Dispose方法, 禁止Finalization（终结操作）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 虚拟的Dispose方法
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }
        #endregion

        /// <summary>
        /// vl_id
        /// </summary>
        public int vl_id { get; set; }

        /// <summary>
        /// vl_cookieId
        /// </summary>
        public string vl_gid { get; set; }

        /// <summary>
        /// vl_cookieId
        /// </summary>
        public string vl_type { get; set; }

        /// <summary>
        /// vl_cateId
        /// </summary>
        public string vl_cateId { get; set; }

        /// <summary>
        /// vl_cookieId
        /// </summary>
        public string vl_cookieId { get; set; }

        /// <summary>
        /// vl_userName
        /// </summary>
        public string vl_userName { get; set; }

        /// <summary>
        /// vl_referrer
        /// </summary>
        public string vl_referrer { get; set; }

        /// <summary>
        /// vl_url
        /// </summary>
        public string vl_url { get; set; }

        /// <summary>
        /// vl_totalTime
        /// </summary>
        public int vl_totalTime { get; set; }

        /// <summary>
        /// vl_ip
        /// </summary>
        public string vl_ip { get; set; }

        /// <summary>
        /// remote_addr 实际IP地址
        /// </summary>
        public string vl_remoteAddr { get; set; }

        /// <summary>
        /// vl_ipArea
        /// </summary>
        public string vl_ipArea { get; set; }

        /// <summary>
        /// vl_ipCountry
        /// </summary>
        public string vl_ipCountry { get; set; }

        /// <summary>
        /// vl_osName
        /// </summary>
        public string vl_osName { get; set; }

        /// <summary>
        /// vl_browser
        /// </summary>
        public string vl_browser { get; set; }

        /// <summary>
        /// vl_screenSize
        /// </summary>
        public string vl_screenSize { get; set; }

        /// <summary>
        /// vl_spiderName
        /// </summary>
        public string vl_spiderName { get; set; }

        /// <summary>
        /// vl_createDate
        /// </summary>
        public DateTime vl_createTime { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string vl_host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
    }
}
