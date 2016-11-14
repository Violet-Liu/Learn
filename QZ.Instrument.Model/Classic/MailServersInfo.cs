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
    public class MailServersInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~MailServersInfo()
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

        public int ms_id
        {
            get;
            set;
        }
        public string ms_smtp
        {
            get;
            set;
        }
        public string ms_loginName
        {
            get;
            set;
        }
        public string ms_loginPwd
        {
            get;
            set;
        }
        public string ms_account
        {
            get;
            set;
        }
        public bool ms_ssl
        {
            get;
            set;
        }
        public int ms_port
        {
            get;
            set;
        }
        public bool ms_status
        {
            get;
            set;
        }
        public string ms_remark
        {
            get;
            set;
        }
    }
}
