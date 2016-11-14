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
    /// <summary>
    /// App端评论类型，与CommentInfo类相比，少了一些不需要的字段
    /// </summary>
    public class CommentInfoApp
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CommentInfoApp()
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

        #region cmt_id
        int _cmt_id;
        /// <summary>
        /// cmt_id
        /// </summary>
        public int cmt_id
        {
            get { return _cmt_id; }
            set { _cmt_id = value; }
        }
        #endregion

        #region 格式，例如：1208N10001 前面四位是所在分表，按年月来，后面6位数据是序号[cmt_uid]
        string _cmt_uid;
        /// <summary>
        /// 格式，例如：1208N10001 前面四位是所在分表，按年月来，后面6位数据是序号[cmt_uid]
        /// </summary>
        public string cmt_uid
        {
            get { return _cmt_uid; }
            set { _cmt_uid = value; }
        }
        #endregion

        #region 评论对象类型，例如：指南，新闻[cmt_sourceType]
        string _cmt_sourcetype;
        /// <summary>
        /// 评论对象类型，例如：指南，新闻[cmt_sourceType]
        /// </summary>
        public string cmt_sourceType
        {
            get { return _cmt_sourcetype; }
            set { _cmt_sourcetype = value; }
        }
        #endregion

        #region 评论源唯一编号[cmt_sourceId]
        string _cmt_sourceid;
        /// <summary>
        /// 评论源唯一编号[cmt_sourceId]
        /// </summary>
        public string cmt_sourceId
        {
            get { return _cmt_sourceid; }
            set { _cmt_sourceid = value; }
        }
        #endregion

        #region 评论内容[cmt_content]
        string _cmt_content;
        /// <summary>
        /// 评论内容[cmt_content]
        /// </summary>
        public string cmt_content
        {
            get { return _cmt_content; }
            set { _cmt_content = value; }
        }
        #endregion

        #region 文章地址
        string _cmt_sourceUrl;

        /// <summary>
        /// 文章地址
        /// </summary>
        public string cmt_sourceUrl
        {
            get { return _cmt_sourceUrl; }
            set { _cmt_sourceUrl = value; }
        }
        #endregion

        #region 作者
        /// <summary>
        /// 作者
        /// </summary>
        string _n_authors;
        public string n_authors
        {
            get { return _n_authors; }
            set { _n_authors = value; }
        }
        #endregion

        #region 信息日期[n_date]
        string _n_date;
        /// <summary>
        /// 信息日期[n_date]
        /// </summary>
        public string n_date
        {
            get { return _n_date; }
            set { _n_date = value; }
        }
        #endregion

        #region 标题[n_title]
        string _n_title;
        /// <summary>
        /// 标题[n_title]
        /// </summary>
        public string n_title
        {
            get { return _n_title; }
            set { _n_title = value; }
        }
        #endregion

        #region 评论源分类Id
        public string cmt_sourceCateId
        {
            get;
            set;
        }
        #endregion
    }
}
