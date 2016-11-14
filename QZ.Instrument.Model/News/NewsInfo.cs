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
    public class NewsInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~NewsInfo()
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

        #region 自动编号[n_id]
        int _n_id;
        /// <summary>
        /// 自动编号[n_id]
        /// </summary>
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        #endregion

        #region 分类编号[n_cat_id]
        int _n_cat_id;
        /// <summary>
        /// 分类编号[n_cat_id]
        /// </summary>
        public int n_cat_id
        {
            get { return _n_cat_id; }
            set { _n_cat_id = value; }
        }
        #endregion

        #region 新闻类型：text=图文新闻，images=图片集合，video=视频[n_type]
        string _n_type;
        /// <summary>
        /// 新闻类型：text=图文新闻，images=图片集合，video=视频[n_type]
        /// </summary>
        public string n_type
        {
            get { return _n_type; }
            set { _n_type = value; }
        }
        #endregion

        #region 资讯编号(GUID)，格式=20111212-0123456789abcdef[n_gid]
        string _n_gid;
        /// <summary>
        /// 资讯编号(GUID)，格式=20111212-0123456789abcdef[n_gid]
        /// </summary>
        public string n_gid
        {
            get { return _n_gid; }
            set { _n_gid = value; }
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

        #region 信息url(直接跳转到目标地址)[n_linkUrl]
        string _n_linkurl;
        /// <summary>
        /// 信息url(直接跳转到目标地址)[n_linkUrl]
        /// </summary>
        public string n_linkUrl
        {
            get { return _n_linkurl; }
            set { _n_linkurl = value; }
        }
        #endregion

        #region 核心内容[n_summary]
        string _n_summary;
        /// <summary>
        /// 核心内容[n_summary]
        /// </summary>
        public string n_summary
        {
            get { return _n_summary; }
            set { _n_summary = value; }
        }
        #endregion

        #region 信息标签[n_tags]
        string _n_tags;
        /// <summary>
        /// 核心内容[n_tags]
        /// </summary>
        public string n_tags
        {
            get { return _n_tags; }
            set { _n_tags = value; }
        }
        #endregion

        #region 信息来源[n_source]
        string _n_source;
        /// <summary>
        /// 信息来源[n_source]
        /// </summary>
        public string n_source
        {
            get { return _n_source; }
            set { _n_source = value; }
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

        #region css样式[n_css]
        string _n_css;
        /// <summary>
        /// css样式[n_css]
        /// </summary>
        public string n_css
        {
            get { return _n_css; }
            set { _n_css = value; }
        }
        #endregion

        #region 图标类型(信息标题后面：hot、video、new等图片)[n_icon]
        string _n_icon;
        /// <summary>
        /// 图标类型(信息标题后面：hot、video、new等图片)[n_icon]
        /// </summary>
        public string n_icon
        {
            get { return _n_icon; }
            set { _n_icon = value; }
        }
        #endregion

        #region 列表展示形式[n_listType]
        int _n_listType;
        /// <summary>
        /// 内容页数[n_listType]
        /// </summary>
        public int n_listType
        {
            get { return _n_listType; }
            set { _n_listType = value; }
        }
        #endregion

        #region 内容页数[n_pages]
        int _n_pages;
        /// <summary>
        /// 内容页数[n_pages]
        /// </summary>
        public int n_pages
        {
            get { return _n_pages; }
            set { _n_pages = value; }
        }
        #endregion

        #region 浏览次数[n_hits]
        int _n_hits;
        /// <summary>
        /// 浏览次数[n_hits]
        /// </summary>
        public int n_hits
        {
            get { return _n_hits; }
            set { _n_hits = value; }
        }
        #endregion

        #region 用户推荐次数[n_diggs]
        int _n_diggs;
        /// <summary>
        /// 用户推荐次数[n_diggs]
        /// </summary>
        public int n_diggs
        {
            get { return _n_diggs; }
            set { _n_diggs = value; }
        }
        #endregion

        #region 喜欢次数[n_favors]
        int _n_favors;
        /// <summary>
        /// 喜欢次数[n_favors]
        /// </summary>
        public int n_favors
        {
            get { return _n_favors; }
            set { _n_favors = value; }
        }
        #endregion

        #region 图片1[n_pic1]
        string _n_pic1;
        /// <summary>
        /// 图片1[n_pic1]
        /// </summary>
        public string n_pic1
        {
            get { return _n_pic1; }
            set { _n_pic1 = value; }
        }
        #endregion

        #region 图片2[n_pic2]
        string _n_pic2;
        /// <summary>
        /// 图片2[n_pic2]
        /// </summary>
        public string n_pic2
        {
            get { return _n_pic2; }
            set { _n_pic2 = value; }
        }
        #endregion

        #region 审核状态(控制信息发布状态),0=未审核，1=已审核[n_state]
        int _n_state;
        /// <summary>
        /// 审核状态(控制信息发布状态),0=未审核，1=已审核[n_state]
        /// </summary>
        public int n_state
        {
            get { return _n_state; }
            set { _n_state = value; }
        }
        #endregion

        #region 访问权限，0=开放的，1=注册用户可见[n_right]
        int _n_right;
        /// <summary>
        /// 访问权限，0=开放的，1=注册用户可见[n_right]
        /// </summary>
        public int n_right
        {
            get { return _n_right; }
            set { _n_right = value; }
        }
        #endregion

        #region 排序，默认添加后更新为n_id值[n_sort]
        int _n_sort;
        /// <summary>
        /// 排序，默认添加后更新为n_id值[n_sort]
        /// </summary>
        public int n_sort
        {
            get { return _n_sort; }
            set { _n_sort = value; }
        }
        #endregion

        #region 发布人[n_createUser]
        string _n_createuser;
        /// <summary>
        /// 发布人[n_createUser]
        /// </summary>
        public string n_createUser
        {
            get { return _n_createuser; }
            set { _n_createuser = value; }
        }
        #endregion

        #region 发布时间[n_createTime]
        DateTime _n_createtime;
        /// <summary>
        /// 发布时间[n_createTime]
        /// </summary>
        public DateTime n_createTime
        {
            get { return _n_createtime; }
            set { _n_createtime = value; }
        }
        #endregion

        #region 分析师,格式：0=1234=某某某，0=类型，默认为分析师，1=投资名人，中间为编号[n_analyst]
        string _n_analyst;
        /// <summary>
        /// 分析师,格式：0=1234=某某某，0=类型，默认为分析师，1=投资名人，中间为编号[n_analyst]
        /// </summary>
        public string n_analyst
        {
            get { return _n_analyst; }
            set { _n_analyst = value; }
        }
        #endregion

        #region 扩展字段1[n_ready1]
        string _n_ready1;
        /// <summary>
        /// 扩展字段1[n_ready1]
        /// </summary>
        public string n_ready1
        {
            get { return _n_ready1; }
            set { _n_ready1 = value; }
        }
        #endregion

        #region 扩展字段2[n_ready2]
        string _n_ready2;
        /// <summary>
        /// 扩展字段2[n_ready2]
        /// </summary>
        public string n_ready2
        {
            get { return _n_ready2; }
            set { _n_ready2 = value; }
        }
        #endregion

        #region 扩展字段3[n_ready3]
        int _n_ready3;
        /// <summary>
        /// 扩展字段3[n_ready3]
        /// </summary>
        public int n_ready3
        {
            get { return _n_ready3; }
            set { _n_ready3 = value; }
        }
        #endregion

        #region 归属源编号[n_sourceId]
        string _n_sourceId;
        /// <summary>
        /// 归属源编号[_n_sourceId]
        /// </summary>
        public string n_sourceId
        {
            get { return _n_sourceId; }
            set { _n_sourceId = value; }
        }
        #endregion

        #region 编辑[n_editor]
        string _n_editor;
        /// <summary>
        /// 编辑[n_editor]
        /// </summary>
        public string n_editor
        {
            get { return _n_editor; }
            set { _n_editor = value; }
        }
        #endregion

        #region 来源网址[n_sourceUrl]
        string _n_sourceUrl;
        /// <summary>
        /// 来源网址[n_sourceUrl]
        /// </summary>
        public string n_sourceUrl
        {
            get { return _n_sourceUrl; }
            set { _n_sourceUrl = value; }
        }
        #endregion

        #region 同类分组[n_group]
        string _n_group;
        /// <summary>
        /// 同类分组[n_group]
        /// </summary>
        public string n_group
        {
            get { return _n_group; }
            set { _n_group = value; }
        }
        #endregion

        #region 短标题
        /// <summary>
        /// 短标题
        /// </summary>
        string _n_sTitle;
        public string n_sTitle
        {
            get { return _n_sTitle; }
            set { _n_sTitle = value; }
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


    }
}
