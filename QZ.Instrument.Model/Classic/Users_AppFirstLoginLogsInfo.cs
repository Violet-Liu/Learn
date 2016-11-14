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
    public class Users_AppFirstLoginLogsInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~Users_AppFirstLoginLogsInfo()
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

        #region 自动编号[ul_id]
        int _ul_id;
        /// <summary>
        /// 自动编号[ul_id]
        /// </summary>
        public int ul_id
        {
            get { return _ul_id; }
            set { _ul_id = value; }
        }
        #endregion

        #region 用户编号[ul_u_uid]
        int _ul_u_uid;
        /// <summary>
        /// 用户编号[ul_u_uid]
        /// </summary>
        public int ul_u_uid
        {
            get { return _ul_u_uid; }
            set { _ul_u_uid = value; }
        }
        #endregion

        #region 用户昵称[ul_u_name]
        string _ul_u_name;
        /// <summary>
        /// 用户昵称[ul_u_name]
        /// </summary>
        public string ul_u_name
        {
            get { return _ul_u_name; }
            set { _ul_u_name = value; }
        }
        #endregion

        #region 创建时间[ul_createTime]
        DateTime _ul_createtime;
        /// <summary>
        /// 创建时间[ul_createTime]
        /// </summary>
        public DateTime ul_createTime
        {
            get { return _ul_createtime; }
            set { _ul_createtime = value; }
        }
        #endregion

    }
}
