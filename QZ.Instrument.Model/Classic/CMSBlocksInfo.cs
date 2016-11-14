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
    public class CMSBlocksInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CMSBlocksInfo()
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

        public int blk_id
        {
            get;
            set;
        }
        public string blk_code
        {
            get;
            set;
        }
        public string blk_name
        {
            get;
            set;
        }
        public string blk_sampleImg
        {
            get;
            set;
        }
        public string blk_brief
        {
            get;
            set;
        }
        public int blk_totalItems
        {
            get;
            set;
        }
        public int blk_minNum
        {
            get;
            set;
        }
        public string blk_url
        {
            get;
            set;
        }
        public int blk_pg_id
        {
            get;
            set;
        }
        public string blk_source
        {
            get;
            set;
        }
        public bool blk_saveAll
        {
            get;
            set;
        }
        public string blk_imgSize
        {
            get;
            set;
        }
    }
}
