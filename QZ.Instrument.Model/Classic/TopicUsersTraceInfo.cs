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
    public class TopicUsersTraceInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~TopicUsersTraceInfo()
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

        #region TopicUsers Trace ID[tut_id]
        int _tut_id;
        /// <summary>
        /// TopicUsers Trace ID[tut_id]
        /// </summary>
        public int tut_id
        {
            get { return _tut_id; }
            set { _tut_id = value; }
        }
        #endregion

        #region Topic ID[tut_t_id]
        string _tut_t_id;
        /// <summary>
        /// Topic ID[tut_t_id]
        /// </summary>
        public string tut_t_id
        {
            get { return _tut_t_id; }
            set { _tut_t_id = value; }
        }
        #endregion

        #region 帖子类型，有社区帖，公司帖等[tut_t_type]
        string _tut_t_type;
        /// <summary>
        /// 帖子类型，有社区帖，公司帖等[tut_t_type]
        /// </summary>
        public string tut_t_type
        {
            get { return _tut_t_type; }
            set { _tut_t_type = value; }
        }
        #endregion

        #region 被推送通知的用户id[tut_uid]
        int _tut_uid;
        /// <summary>
        /// 被推送通知的用户id[tut_uid]
        /// </summary>
        public int tut_uid
        {
            get { return _tut_uid; }
            set { _tut_uid = value; }
        }
        #endregion

        #region tut_t_count
        int _tut_t_count;
        /// <summary>
        /// tut_t_count
        /// </summary>
        public int tut_t_count
        {
            get { return _tut_t_count; }
            set { _tut_t_count = value; }
        }
        #endregion

        #region tut_status
        bool _tut_status;
        /// <summary>
        /// tut_status
        /// </summary>
        public bool tut_status
        {
            get { return _tut_status; }
            set { _tut_status = value; }
        }
        #endregion

    }
}
