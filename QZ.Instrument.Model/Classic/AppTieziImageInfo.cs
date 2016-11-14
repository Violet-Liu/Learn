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
    public class AppTieziImageInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AppTieziImageInfo()
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

        #region ati_id
        int _ati_id;
        /// <summary>
        /// ati_id
        /// </summary>
        public int ati_id
        {
            get { return _ati_id; }
            set { _ati_id = value; }
        }
        #endregion

        #region 帖子id[ati_tiezi_id]
        int _ati_tiezi_id;
        /// <summary>
        /// 帖子id[ati_tiezi_id]
        /// </summary>
        public int ati_tiezi_id
        {
            get { return _ati_tiezi_id; }
            set { _ati_tiezi_id = value; }
        }
        #endregion

        #region 帖子类型,0表示主帖,1表示帖子回复[ati_tiezi_type]
        int _ati_tiezi_type;
        /// <summary>
        /// 帖子类型,0表示主帖,1表示帖子回复[ati_tiezi_type]
        /// </summary>
        public int ati_tiezi_type
        {
            get { return _ati_tiezi_type; }
            set { _ati_tiezi_type = value; }
        }
        #endregion

        #region 帖子图片url[ati_url]
        string _ati_url;
        /// <summary>
        /// 帖子图片url[ati_url]
        /// </summary>
        public string ati_url
        {
            get { return _ati_url; }
            set { _ati_url = value; }
        }
        #endregion

        #region 发表帖子的用户id[ati_uid]
        int _ati_uid;
        /// <summary>
        /// 发表帖子的用户id[ati_uid]
        /// </summary>
        public int ati_uid
        {
            get { return _ati_uid; }
            set { _ati_uid = value; }
        }
        #endregion

    }
}
