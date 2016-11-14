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
    public class NewsCateInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~NewsCateInfo()
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

        #region cat_id
        int _cat_id;
        /// <summary>
        /// cat_id
        /// </summary>
        public int cat_id
        {
            get { return _cat_id; }
            set { _cat_id = value; }
        }
        #endregion

        #region cat_uid
        string _cat_uid;
        /// <summary>
        /// cat_uid
        /// </summary>
        public string cat_uid
        {
            get { return _cat_uid; }
            set { _cat_uid = value; }
        }
        #endregion

        #region cat_path
        string _cat_path;
        /// <summary>
        /// cat_path
        /// </summary>
        public string cat_path
        {
            get { return _cat_path; }
            set { _cat_path = value; }
        }
        #endregion

        #region cat_name
        string _cat_name;
        /// <summary>
        /// cat_name
        /// </summary>
        public string cat_name
        {
            get { return _cat_name; }
            set { _cat_name = value; }
        }
        #endregion

        #region cat_brief
        string _cat_brief;
        /// <summary>
        /// cat_brief
        /// </summary>
        public string cat_brief
        {
            get { return _cat_brief; }
            set { _cat_brief = value; }
        }
        #endregion

        #region cat_url
        string _cat_url;
        /// <summary>
        /// cat_url
        /// </summary>
        public string cat_url
        {
            get { return _cat_url; }
            set { _cat_url = value; }
        }
        #endregion

        #region cat_manager
        string _cat_manager;
        /// <summary>
        /// cat_manager
        /// </summary>
        public string cat_manager
        {
            get { return _cat_manager; }
            set { _cat_manager = value; }
        }
        #endregion

        #region 1=正常，3=已关闭，关闭后不能添加、修改信息[cat_status]
        int _cat_status;
        /// <summary>
        /// 1=正常，3=已关闭，关闭后不能添加、修改信息[cat_status]
        /// </summary>
        public int cat_status
        {
            get { return _cat_status; }
            set { _cat_status = value; }
        }
        #endregion

        #region cat_createUser
        string _cat_createuser;
        /// <summary>
        /// cat_createUser
        /// </summary>
        public string cat_createUser
        {
            get { return _cat_createuser; }
            set { _cat_createuser = value; }
        }
        #endregion

        #region cat_createTime
        DateTime _cat_createtime;
        /// <summary>
        /// cat_createTime
        /// </summary>
        public DateTime cat_createTime
        {
            get { return _cat_createtime; }
            set { _cat_createtime = value; }
        }
        #endregion

        #region cat_inheritAll
        bool _cat_inheritAll;
        /// <summary>
        /// cat_tableIndex
        /// </summary>
        public bool cat_inheritAll
        {
            get { return _cat_inheritAll; }
            set { _cat_inheritAll = value; }
        }
        #endregion

        #region cat_tableIndex
        int _cat_tableindex;
        /// <summary>
        /// cat_tableIndex
        /// </summary>
        public int cat_tableIndex
        {
            get { return _cat_tableindex; }
            set { _cat_tableindex = value; }
        }
        #endregion

        #region cat_isLast
        bool _cat_isLast;
        /// <summary>
        /// _cat_isLast
        /// </summary>
        public bool cat_isLast
        {
            get { return _cat_isLast; }
            set { _cat_isLast = value; }
        }
        #endregion

        #region cat_totalNews
        int _cat_totalNews;
        /// <summary>
        /// cat_totalNews
        /// </summary>
        public int cat_totalNews
        {
            get { return _cat_totalNews; }
            set { _cat_totalNews = value; }
        }
        #endregion

        #region cat_totalUnchecked
        int _cat_totalUnchecked;
        /// <summary>
        /// cat_totalUnchecked
        /// </summary>
        public int cat_totalUnchecked
        {
            get { return _cat_totalUnchecked; }
            set { _cat_totalUnchecked = value; }
        }
        #endregion

        #region cat_listType
        int _cat_listType;
        /// <summary>
        /// cat_listType
        /// </summary>
        public int cat_listType
        {
            get { return _cat_listType; }
            set { _cat_listType = value; }
        }
        #endregion

        #region cat_ctrl
        string _cat_ctrl;
        /// <summary>
        /// _cat_ctrl
        /// </summary>
        public string cat_ctrl
        {
            get { return _cat_ctrl; }
            set { _cat_ctrl = value; }
        }
        #endregion

        #region cat_lang
        string _cat_lang;
        /// <summary>
        /// _cat_lang
        /// </summary>
        public string cat_lang
        {
            get { return _cat_lang; }
            set { _cat_lang = value; }
        }
        #endregion

        /// <summary>
        /// 显示多少级行业在列表中(trade=1,region=2)
        /// </summary>
        public string cat_tradeInList { get; set; }

        #region cat_title
        string _cat_title;
        /// <summary>
        /// _cat_title
        /// </summary>
        public string cat_title
        {
            get { return _cat_title; }
            set { _cat_title = value; }
        }
        #endregion
    }
}
