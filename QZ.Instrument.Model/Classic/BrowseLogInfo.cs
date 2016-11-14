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
    public class BrowseLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~BrowseLogInfo()
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

        #region 自动编号[bl_id]
        int _bl_id;
        /// <summary>
        /// 自动编号[bl_id]
        /// </summary>
        public int bl_id
        {
            get { return _bl_id; }
            set { _bl_id = value; }
        }
        #endregion

        #region 机构名称[bl_oc_name]
        string _bl_oc_name;
        /// <summary>
        /// 机构名称[bl_oc_name]
        /// </summary>
        public string bl_oc_name
        {
            get { return _bl_oc_name; }
            set { _bl_oc_name = value; }
        }
        #endregion

        #region 机构代码[bl_oc_code]
        string _bl_oc_code;
        /// <summary>
        /// 机构代码[bl_oc_code]
        /// </summary>
        public string bl_oc_code
        {
            get { return _bl_oc_code; }
            set { _bl_oc_code = value; }
        }
        #endregion

        #region 地区代码[bl_oc_area]
        string _bl_oc_area;
        /// <summary>
        /// 地区代码[bl_oc_area]
        /// </summary>
        public string bl_oc_area
        {
            get { return _bl_oc_area; }
            set { _bl_oc_area = value; }
        }
        #endregion

        #region 用户名[bl_u_name]
        string _bl_u_name;
        /// <summary>
        /// 用户名[bl_u_name]
        /// </summary>
        public string bl_u_name
        {
            get { return _bl_u_name; }
            set { _bl_u_name = value; }
        }
        #endregion

        #region 用户ID[bl_u_uid]
        int _bl_u_uid;
        /// <summary>
        /// 用户ID[bl_u_uid]
        /// </summary>
        public int bl_u_uid
        {
            get { return _bl_u_uid; }
            set { _bl_u_uid = value; }
        }
        #endregion

        #region 浏览日期[bl_date]
        DateTime _bl_date;
        /// <summary>
        /// 浏览日期[bl_date]
        /// </summary>
        public DateTime bl_date
        {
            get { return _bl_date; }
            set { _bl_date = value; }
        }
        #endregion

        #region bl_ip
        string _bl_ip;
        /// <summary>
        /// bl_ip
        /// </summary>
        public string bl_ip
        {
            get { return _bl_ip; }
            set { _bl_ip = value; }
        }
        #endregion

        #region bl_osName
        string _bl_osname;
        /// <summary>
        /// bl_osName
        /// </summary>
        public string bl_osName
        {
            get { return _bl_osname; }
            set { _bl_osname = value; }
        }
        #endregion

        #region bl_appVer
        string _bl_appver;
        /// <summary>
        /// bl_appVer
        /// </summary>
        public string bl_appVer
        {
            get { return _bl_appver; }
            set { _bl_appver = value; }
        }
        #endregion

        public BrowseLogInfo Set_Ip(string ip) => Fluent.Assign_0(this, log => log.bl_ip = ip);
        public BrowseLogInfo Set_Os_Name(string os_name) => Fluent.Assign_0(this, log => log.bl_osName = os_name);
        public BrowseLogInfo Set_App_Ver(string app_ver) => Fluent.Assign_0(this, log => log.bl_appVer = app_ver);
    }
}
