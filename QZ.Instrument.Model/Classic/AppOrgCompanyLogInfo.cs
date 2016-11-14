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
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    public class AppOrgCompanyLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AppOrgCompanyLogInfo()
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

        #region 自动编号[cl_id]
        int _cl_id;
        /// <summary>
        /// 自动编号[cl_id]
        /// </summary>
        public int cl_id
        {
            get { return _cl_id; }
            set { _cl_id = value; }
        }
        #endregion

        #region 操作[cl_action]
        string _cl_action;
        /// <summary>
        /// 操作[cl_action]
        /// </summary>
        public string cl_action
        {
            get { return _cl_action; }
            set { _cl_action = value; }
        }
        #endregion

        #region 用户名[cl_u_name]
        /// <summary>
        /// 用户名[cl_u_name]
        /// </summary>
        public string cl_u_name { get; set; } = string.Empty;

        #endregion

        #region 用户ID[cl_u_uid]
        /// <summary>
        /// 用户ID[cl_u_uid]
        /// </summary>
        public int cl_u_uid { get; set; }
        #endregion

        #region 创建日期[cl_date]
        DateTime _cl_date;
        /// <summary>
        /// 创建日期[cl_date]
        /// </summary>
        public DateTime cl_date
        {
            get { return _cl_date; }
            set { _cl_date = value; }
        }
        #endregion

        #region 操作系统[cl_osName]
        string _cl_osname;
        /// <summary>
        /// 操作系统[cl_osName]
        /// </summary>
        public string cl_osName
        {
            get { return _cl_osname; }
            set { _cl_osname = value; }
        }
        #endregion

        #region IP[cl_ip]
        string _cl_ip;
        /// <summary>
        /// IP[cl_ip]
        /// </summary>
        public string cl_ip
        {
            get { return _cl_ip; }
            set { _cl_ip = value; }
        }
        #endregion

        #region 浏览器[cl_browser]
        string _cl_browser;
        /// <summary>
        /// 浏览器[cl_browser]
        /// </summary>
        public string cl_browser
        {
            get { return _cl_browser; }
            set { _cl_browser = value; }
        }
        #endregion

        #region 屏幕尺寸[cl_screenSize]
        string _cl_screensize;
        /// <summary>
        /// 屏幕尺寸[cl_screenSize]
        /// </summary>
        public string cl_screenSize
        {
            get { return _cl_screensize; }
            set { _cl_screensize = value; }
        }
        #endregion

        #region cookieid[cl_cookieId]
        string _cl_cookieid;
        /// <summary>
        /// cookieid[cl_cookieId]
        /// </summary>
        public string cl_cookieId
        {
            get { return _cl_cookieid; }
            set { _cl_cookieid = value; }
        }
        #endregion

        #region cl_appVer
        string _cl_appver;
        /// <summary>
        /// cl_appVer
        /// </summary>
        public string cl_appVer
        {
            get { return _cl_appver; }
            set { _cl_appver = value; }
        }
        #endregion

        public AppOrgCompanyLogInfo Set_Uid(string u_id) => Fluent.Assign_0(this, info => info.cl_u_uid = u_id.ToInt());
        public AppOrgCompanyLogInfo Set_Uname(string u_name) => Fluent.Assign_0(this, info => info.cl_u_name = u_name);
        public AppOrgCompanyLogInfo Set_Action(string action) => Fluent.Assign_0(this, info => info.cl_action = action);
    }
}
