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
    public class NewsContentInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~NewsContentInfo()
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

        #region 内容ID[nc_id]
        int _nc_id;
        /// <summary>
        /// 内容ID[nc_id]
        /// </summary>
        public int nc_id
        {
            get { return _nc_id; }
            set { _nc_id = value; }
        }
        #endregion

        #region 小标题[nc_title]
        string _nc_title;
        /// <summary>
        /// 小标题[nc_title]
        /// </summary>
        public string nc_title
        {
            get { return _nc_title; }
            set { _nc_title = value; }
        }
        #endregion

        #region nc_simg_url
        string _nc_simg_url;
        /// <summary>
        /// nc_simg_url
        /// </summary>
        public string nc_simg_url
        {
            get { return _nc_simg_url; }
            set { _nc_simg_url = value; }
        }
        #endregion

        #region nc_bimg_url
        string _nc_bimg_url;
        /// <summary>
        /// nc_bimg_url
        /// </summary>
        public string nc_bimg_url
        {
            get { return _nc_bimg_url; }
            set { _nc_bimg_url = value; }
        }
        #endregion

        #region 详细内容[nc_content]
        string _nc_content;
        /// <summary>
        /// 详细内容[nc_content]
        /// </summary>
        public string nc_content
        {
            get { return _nc_content; }
            set { _nc_content = value; }
        }
        #endregion

        #region 创建时间[nc_createTime]
        DateTime _nc_createtime;
        /// <summary>
        /// 创建时间[nc_createTime]
        /// </summary>
        public DateTime nc_createTime
        {
            get { return _nc_createtime; }
            set { _nc_createtime = value; }
        }
        #endregion

        public string nc_shortcreateTime { get; set; }

        #region 创建人[nc_createUser]
        string _nc_createuser;
        /// <summary>
        /// 创建人[nc_createUser]
        /// </summary>
        public string nc_createUser
        {
            get { return _nc_createuser; }
            set { _nc_createuser = value; }
        }
        #endregion

        #region 排序[nc_order]
        int _nc_order;
        /// <summary>
        /// 排序[nc_order]
        /// </summary>
        public int nc_order
        {
            get { return _nc_order; }
            set { _nc_order = value; }
        }
        #endregion

        #region 信息GID[nc_n_gid]
        string _nc_n_gid;
        /// <summary>
        /// 信息GID[nc_n_gid]
        /// </summary>
        public string nc_n_gid
        {
            get { return _nc_n_gid; }
            set { _nc_n_gid = value; }
        }
        #endregion

    }
}
