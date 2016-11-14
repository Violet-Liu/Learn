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
    public class CompanyTieziTopicInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyTieziTopicInfo()
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

        #region 自动编号[ctt_id]
        int _ctt_id;
        /// <summary>
        /// 自动编号[ctt_id]
        /// </summary>
        public int ctt_id
        {
            get { return _ctt_id; }
            set { _ctt_id = value; }
        }
        #endregion

        #region 机构代码[ctt_oc_code]
        string _ctt_oc_code;
        /// <summary>
        /// 机构代码[ctt_oc_code]
        /// </summary>
        public string ctt_oc_code
        {
            get { return _ctt_oc_code; }
            set { _ctt_oc_code = value; }
        }
        #endregion

        #region 机构名称[ctt_oc_name]
        string _ctt_oc_name;
        /// <summary>
        /// 机构名称[ctt_oc_name]
        /// </summary>
        public string ctt_oc_name
        {
            get { return _ctt_oc_name; }
            set { _ctt_oc_name = value; }
        }
        #endregion

        #region 用户名[ctt_u_name]
        string _ctt_u_name;
        /// <summary>
        /// 用户名[ctt_u_name]
        /// </summary>
        public string ctt_u_name
        {
            get { return _ctt_u_name; }
            set { _ctt_u_name = value; }
        }
        #endregion

        #region 用户ID[ctt_u_uid]
        int _ctt_u_uid;
        /// <summary>
        /// 用户ID[ctt_u_uid]
        /// </summary>
        public int ctt_u_uid
        {
            get { return _ctt_u_uid; }
            set { _ctt_u_uid = value; }
        }
        #endregion

        #region 帖子主题[ctt_content]
        string _ctt_content;
        /// <summary>
        /// 帖子主题[ctt_content]
        /// </summary>
        public string ctt_content
        {
            get { return _ctt_content; }
            set { _ctt_content = value; }
        }
        #endregion

        #region 发表日期[ctt_date]
        DateTime _ctt_date;
        /// <summary>
        /// 发表日期[ctt_date]
        /// </summary>
        public DateTime ctt_date
        {
            get { return _ctt_date; }
            set { _ctt_date = value; }
        }
        #endregion

        #region 地区代码[ctt_oc_area]
        string _ctt_oc_area;
        /// <summary>
        /// 地区代码[ctt_oc_area]
        /// </summary>
        public string ctt_oc_area
        {
            get { return _ctt_oc_area; }
            set { _ctt_oc_area = value; }
        }
        #endregion

        #region 头像[ctt_u_face]
        string _ctt_u_face;
        /// <summary>
        /// 头像[ctt_u_face]
        /// </summary>
        public string ctt_u_face
        {
            get { return _ctt_u_face; }
            set { _ctt_u_face = value; }
        }
        #endregion

        #region 状态[ctt_status]
        int _ctt_status;
        /// <summary>
        /// 状态[ctt_status]
        /// </summary>
        public int ctt_status
        {
            get { return _ctt_status; }
            set { _ctt_status = value; }
        }
        #endregion

        /// <summary>
        /// 帖子主题[replys]
        /// </summary>
        public List<CompanyTieziReplyInfo> replys;

        /// <summary>
        /// 点赞数
        /// </summary>
        public int likeCount;

        /// <summary>
        /// 当前用户是否已经点赞
        /// </summary>
        public bool isLike;



        #region 友好时间[FriendlyTime]
        string _FriendlyTime;
        /// <summary>
        ///友好时间[FriendlyTime]
        /// </summary>
        public string FriendlyTime
        {
            get { return _FriendlyTime; }
            set { _FriendlyTime = value; }
        }
        #endregion
        //public string ctt_title { get; set; }
        public string ctt_tag { get; set; }
        //public List<TopicImage> list { get; set; }
        public List<string> list2 { get; set; }
    }
}
