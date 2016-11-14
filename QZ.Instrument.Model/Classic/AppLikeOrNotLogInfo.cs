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
    public class AppLikeOrNotLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AppLikeOrNotLogInfo()
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

        #region 自动编号[all_id]
        int _all_id;
        /// <summary>
        /// 自动编号[all_id]
        /// </summary>
        public int all_id
        {
            get { return _all_id; }
            set { _all_id = value; }
        }
        #endregion

        #region 帖子编号[all_teizi]
        int _all_teizi;
        /// <summary>
        /// 帖子编号[all_teizi]
        /// </summary>
        public int all_teizi
        {
            get { return _all_teizi; }
            set { _all_teizi = value; }
        }
        #endregion

        #region 用户名[all_u_name]
        string _all_u_name;
        /// <summary>
        /// 用户名[all_u_name]
        /// </summary>
        public string all_u_name
        {
            get { return _all_u_name; }
            set { _all_u_name = value; }
        }
        #endregion

        #region 用户ID[all_u_uid]
        int _all_u_uid;
        /// <summary>
        /// 用户ID[all_u_uid]
        /// </summary>
        public int all_u_uid
        {
            get { return _all_u_uid; }
            set { _all_u_uid = value; }
        }
        #endregion

        #region 日期[all_date]
        DateTime _all_date;
        /// <summary>
        /// 日期[all_date]
        /// </summary>
        public DateTime all_date
        {
            get { return _all_date; }
            set { _all_date = value; }
        }
        #endregion

        #region 态度类型[all_type]
        int _all_type;
        /// <summary>
        /// 态度类型[all_type]
        /// </summary>
        public int all_type
        {
            get { return _all_type; }
            set { _all_type = value; }
        }
        #endregion

        #region 是否有效[all_valid]
        int _all_valid;
        /// <summary>
        /// 是否有效[all_valid]
        /// </summary>
        public int all_valid
        {
            get { return _all_valid; }
            set { _all_valid = value; }
        }
        #endregion

    }
}
