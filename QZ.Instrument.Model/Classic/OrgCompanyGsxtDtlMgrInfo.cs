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
    public class OrgCompanyGsxtDtlMgrInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyGsxtDtlMgrInfo()
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

        #region om_id
        int _om_id;
        /// <summary>
        /// om_id
        /// </summary>
        public int om_id
        {
            get { return _om_id; }
            set { _om_id = value; }
        }
        #endregion

        #region 机构登记号[om_oc_code]
        string _om_oc_code;
        /// <summary>
        /// 机构登记号[om_oc_code]
        /// </summary>
        public string om_oc_code
        {
            get { return _om_oc_code; }
            set { _om_oc_code = value; }
        }
        #endregion

        #region 姓名[om_name]
        string _om_name;
        /// <summary>
        /// 姓名[om_name]
        /// </summary>
        public string om_name
        {
            get { return _om_name; }
            set { _om_name = value; }
        }
        #endregion

        #region 职位[om_position]
        string _om_position;
        /// <summary>
        /// 职位[om_position]
        /// </summary>
        public string om_position
        {
            get { return _om_position; }
            set { _om_position = value; }
        }
        #endregion

        #region 产生方式[om_type]
        string _om_type;
        /// <summary>
        /// 产生方式[om_type]
        /// </summary>
        public string om_type
        {
            get { return _om_type; }
            set { _om_type = value; }
        }
        #endregion

        DateTime _om_createTime;
        /// <summary>
        /// om_createTime
        /// </summary>
        public DateTime om_createTime
        {
            get { return _om_createTime; }
            set { _om_createTime = value; }
        }

        DateTime _om_updateTime;
        /// <summary>
        /// om_updateTime
        /// </summary>
        public DateTime om_updateTime
        {
            get { return _om_updateTime; }
            set { _om_updateTime = value; }
        }

        #region om_invalidTime
        DateTime _om_invalidtime;
        /// <summary>
        /// om_invalidTime
        /// </summary>
        public DateTime om_invalidTime
        {
            get { return _om_invalidtime; }
            set { _om_invalidtime = value; }
        }
        #endregion

        #region om_status
        byte _om_status;
        /// <summary>
        /// om_status
        /// </summary>
        public byte om_status
        {
            get { return _om_status; }
            set { _om_status = value; }
        }
        #endregion
    }
}
