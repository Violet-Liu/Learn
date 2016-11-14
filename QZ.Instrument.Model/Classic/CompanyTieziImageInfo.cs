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
    public class CompanyTieziImageInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyTieziImageInfo()
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

        #region cti_id
        int _cti_id;
        /// <summary>
        /// cti_id
        /// </summary>
        public int cti_id
        {
            get { return _cti_id; }
            set { _cti_id = value; }
        }
        #endregion

        #region 公司帖id[cti_tiezi_id]
        int _cti_tiezi_id;
        /// <summary>
        /// 公司帖id[cti_tiezi_id]
        /// </summary>
        public int cti_tiezi_id
        {
            get { return _cti_tiezi_id; }
            set { _cti_tiezi_id = value; }
        }
        #endregion

        #region 公司帖类型，为0表示原帖，为1表示回复[cti_tiezi_type]
        int _cti_tiezi_type;
        /// <summary>
        /// 公司帖类型，为0表示原帖，为1表示回复[cti_tiezi_type]
        /// </summary>
        public int cti_tiezi_type
        {
            get { return _cti_tiezi_type; }
            set { _cti_tiezi_type = value; }
        }
        #endregion

        #region 帖子关联的用户id[cti_uid]
        int _cti_uid;
        /// <summary>
        /// 帖子关联的用户id[cti_uid]
        /// </summary>
        public int cti_uid
        {
            get { return _cti_uid; }
            set { _cti_uid = value; }
        }
        #endregion

        #region 帖子图片的url[cti_url]
        string _cti_url;
        /// <summary>
        /// 帖子图片的url[cti_url]
        /// </summary>
        public string cti_url
        {
            get { return _cti_url; }
            set { _cti_url = value; }
        }
        #endregion

    }
}
