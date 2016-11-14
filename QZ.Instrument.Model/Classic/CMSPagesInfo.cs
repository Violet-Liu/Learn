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
    public class CMSPagesInfo
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CMSPagesInfo()
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

        public int pg_id
        {
            get;
            set;
        }
        public string pg_uid
        {
            get;
            set;
        }
        public string pg_name
        {
            get;
            set;
        }
        public string pg_level
        {
            get;
            set;
        }
        public string pg_releaseTemplate
        {
            get;
            set;
        }
        public string pg_releasePath
        {
            get;
            set;
        }
        public string pg_releaseUrl
        {
            get;
            set;
        }
        public string pg_previewTemplate
        {
            get;
            set;
        }
        public string pg_previewPath
        {
            get;
            set;
        }
        public string pg_previewUrl
        {
            get;
            set;
        }
        public string pg_lastReleaseTime
        {
            get;
            set;
        }
        public string pg_lastReleaseUser
        {
            get;
            set;
        }
        public DateTime pg_createTime
        {
            get;
            set;
        }
        public string pg_createUser
        {
            get;
            set;
        }
        public string pg_managers
        {
            get;
            set;
        }
        public string pg_lang
        {
            get;
            set;
        }
    }
}
