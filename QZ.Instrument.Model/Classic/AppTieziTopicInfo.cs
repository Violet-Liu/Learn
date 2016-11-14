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
    public class AppTieziTopicInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AppTieziTopicInfo()
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

        #region 自动编号[att_id]
        int _att_id;
        /// <summary>
        /// 自动编号[att_id]
        /// </summary>
        public int att_id
        {
            get { return _att_id; }
            set { _att_id = value; }
        }
        #endregion

        #region 用户名[att_u_name]
        string _att_u_name;
        /// <summary>
        /// 用户名[att_u_name]
        /// </summary>
        public string att_u_name
        {
            get { return _att_u_name; }
            set { _att_u_name = value; }
        }
        #endregion

        #region 用户ID[att_u_uid]
        int _att_u_uid;
        /// <summary>
        /// 用户ID[att_u_uid]
        /// </summary>
        public int att_u_uid
        {
            get { return _att_u_uid; }
            set { _att_u_uid = value; }
        }
        #endregion

        #region 帖子主题[att_content]
        string _att_content;
        /// <summary>
        /// 帖子主题[att_content]
        /// </summary>
        public string att_content
        {
            get { return _att_content; }
            set { _att_content = value; }
        }
        #endregion

        #region 发表日期[att_date]
        DateTime _att_date;
        /// <summary>
        /// 发表日期[att_date]
        /// </summary>
        public DateTime att_date
        {
            get { return _att_date; }
            set { _att_date = value; }
        }
        #endregion

        #region 头像[att_u_face]
        string _att_u_face;
        /// <summary>
        /// 地区代码[att_u_face]
        /// </summary>
        public string att_u_face
        {
            get { return _att_u_face; }
            set { _att_u_face = value; }
        }
        #endregion

        #region 状态[att_status]
        int _att_status;
        /// <summary>
        /// 状态[att_status]
        /// </summary>
        public int att_status
        {
            get { return _att_status; }
            set { _att_status = value; }
        }
        #endregion


        /// <summary>
        /// 帖子主题[replys]
        /// </summary>
        //public List<AppTeiziReplyInfo> replys;

        /// <summary>
        /// 点赞数
        /// </summary>
        public int likeCount;

        /// <summary>
        /// 当前用户是否已经点赞
        /// </summary>
        public bool isLike;

        /// <summary>
        /// 回复数量
        /// </summary>
        public int replyCount;

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

        /// <summary>
        /// image uris
        /// </summary>
        public List<string> list { get; set; }
        public int att_tag { get; set; }
        public List<string> list2 { get; set; }
    }
}
