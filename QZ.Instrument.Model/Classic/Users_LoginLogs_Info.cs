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
    public class Users_LoginLogs_Info : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~Users_LoginLogs_Info()
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

        #region ul_id
        int _ul_id;
        /// <summary>
        /// ul_id
        /// </summary>
        public int ul_id
        {
            get { return _ul_id; }
            set { _ul_id = value; }
        }
        #endregion

        #region ul_u_uid
        int _ul_u_uid;
        /// <summary>
        /// ul_u_uid
        /// </summary>
        public int ul_u_uid
        {
            get { return _ul_u_uid; }
            set { _ul_u_uid = value; }
        }
        #endregion

        #region ul_u_name
        string _ul_u_name;
        /// <summary>
        /// ul_u_name
        /// </summary>
        public string ul_u_name
        {
            get { return _ul_u_name; }
            set { _ul_u_name = value; }
        }
        #endregion

        #region ul_type
        int _ul_type;
        /// <summary>
        /// ul_type
        /// </summary>
        public int ul_type
        {
            get { return _ul_type; }
            set { _ul_type = value; }
        }
        #endregion

        #region ul_status
        int _ul_status;
        /// <summary>
        /// ul_status
        /// </summary>
        public int ul_status
        {
            get { return _ul_status; }
            set { _ul_status = value; }
        }
        #endregion

        #region ul_error
        string _ul_error;
        /// <summary>
        /// ul_error
        /// </summary>
        public string ul_error
        {
            get { return _ul_error; }
            set { _ul_error = value; }
        }
        #endregion

        #region ul_createTime
        DateTime _ul_createtime;
        /// <summary>
        /// ul_createTime
        /// </summary>
        public DateTime ul_createTime
        {
            get { return _ul_createtime; }
            set { _ul_createtime = value; }
        }
        #endregion

        #region ul_ip
        string _ul_ip;
        /// <summary>
        /// ul_ip
        /// </summary>
        public string ul_ip
        {
            get { return _ul_ip; }
            set { _ul_ip = value; }
        }
        #endregion

        #region ul_os
        string _ul_os;
        /// <summary>
        /// ul_os
        /// </summary>
        public string ul_os
        {
            get { return _ul_os; }
            set { _ul_os = value; }
        }
        #endregion

        #region ul_browser
        string _ul_browser;
        /// <summary>
        /// ul_browser
        /// </summary>
        public string ul_browser
        {
            get { return _ul_browser; }
            set { _ul_browser = value; }
        }
        #endregion

        #region ul_clientId
        string _ul_clientid;
        /// <summary>
        /// ul_clientId
        /// </summary>
        public string ul_clientId
        {
            get { return _ul_clientid; }
            set { _ul_clientid = value; }
        }
        #endregion

        #region ul_userAgent
        string _ul_useragent;
        /// <summary>
        /// ul_userAgent
        /// </summary>
        public string ul_userAgent
        {
            get { return _ul_useragent; }
            set { _ul_useragent = value; }
        }
        #endregion

    }
}
