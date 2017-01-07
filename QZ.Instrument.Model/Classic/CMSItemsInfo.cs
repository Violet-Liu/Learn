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
    public class CMSItemsInfo
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CMSItemsInfo()
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

        public int n_id
        {
            get;
            set;
        }
        public string n_title
        {
            get;
            set;
        }
        public string n_subtitle
        {
            get;
            set;
        }
        public string n_brief
        {
            get;
            set;
        }
        public string n_date
        {
            get;
            set;
        }
        public string n_imageUrl
        {
            get;
            set;
        }
        public string n_linkUrl
        {
            get;
            set;
        }
        public string n_target
        {
            get;
            set;
        }
        public string n_cssClass
        {
            get;
            set;
        }
        public int n_iconType
        {
            get;
            set;
        }
        public int n_status
        {
            get;
            set;
        }
        public bool n_publish
        {
            get;
            set;
        }
        public int n_order
        {
            get;
            set;
        }
        public string n_publisher
        {
            get;
            set;
        }
        public DateTime n_publishTime
        {
            get;
            set;
        }

        public string n_allowTime
        {
            get;
            set;
        }
        public int n_blk_id
        {
            get;
            set;
        }
        public int n_pg_id
        {
            get;
            set;
        }
        public string n_sourceClassId
        {
            get;
            set;
        }
        public string n_sourceId
        {
            get;
            set;
        }
        public int n_type
        {
            get;
            set;
        }

        public string blk_name
        {
            get;
            set;
        }
        public string pg_name
        {
            get;
            set;
        }

        public string DistanceNow { get; set; }

    }
}
