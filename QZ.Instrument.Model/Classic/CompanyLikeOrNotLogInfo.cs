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
    public class CompanyLikeOrNotLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyLikeOrNotLogInfo()
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

        #region 自动编号[cll_id]
        int _cll_id;
        /// <summary>
        /// 自动编号[cll_id]
        /// </summary>
        public int cll_id
        {
            get { return _cll_id; }
            set { _cll_id = value; }
        }
        #endregion

        #region 帖子编号[cll_teizi]
        int _cll_teizi;
        /// <summary>
        /// 帖子编号[cll_teizi]
        /// </summary>
        public int cll_teizi
        {
            get { return _cll_teizi; }
            set { _cll_teizi = value; }
        }
        #endregion

        #region 用户名[cll_u_name]
        string _cll_u_name;
        /// <summary>
        /// 用户名[cll_u_name]
        /// </summary>
        public string cll_u_name
        {
            get { return _cll_u_name; }
            set { _cll_u_name = value; }
        }
        #endregion

        #region 用户ID[cll_u_uid]
        int _cll_u_uid;
        /// <summary>
        /// 用户ID[cll_u_uid]
        /// </summary>
        public int cll_u_uid
        {
            get { return _cll_u_uid; }
            set { _cll_u_uid = value; }
        }
        #endregion

        #region 日期[cll_date]
        DateTime _cll_date;
        /// <summary>
        /// 日期[cll_date]
        /// </summary>
        public DateTime cll_date
        {
            get { return _cll_date; }
            set { _cll_date = value; }
        }
        #endregion

        #region 态度类型[cll_type]
        int _cll_type;
        /// <summary>
        /// 态度类型[cll_type]
        /// </summary>
        public int cll_type
        {
            get { return _cll_type; }
            set { _cll_type = value; }
        }
        #endregion

        #region 是否有效[cll_valid]
        int _cll_valid;
        /// <summary>
        /// cll_valid
        /// </summary>
        public int cll_valid
        {
            get { return _cll_valid; }
            set { _cll_valid = value; }
        }
        #endregion

    }
}
