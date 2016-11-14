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
    public class Favorite_View_TraceInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~Favorite_View_TraceInfo()
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

        #region ct_oc_code
        string _ct_oc_code;
        /// <summary>
        /// ct_oc_code
        /// </summary>
        public string ct_oc_code
        {
            get { return _ct_oc_code; }
            set { _ct_oc_code = value; }
        }
        #endregion

        #region ct_oc_name
        string _ct_oc_name;
        /// <summary>
        /// ct_oc_name
        /// </summary>
        public string ct_oc_name
        {
            get { return _ct_oc_name; }
            set { _ct_oc_name = value; }
        }
        #endregion

        #region ct_createtime
        DateTime _ct_createtime;
        /// <summary>
        /// ct_createtime
        /// </summary>
        public DateTime ct_createtime
        {
            get { return _ct_createtime; }
            set { _ct_createtime = value; }
        }
        #endregion
        int _ct_lvl;
        public int ct_lvl
        {
            get { return _ct_lvl; }
            set { _ct_lvl = value; }
        }
        #region fl_u_uid
        int _fl_u_uid;
        /// <summary>
        /// fl_u_uid
        /// </summary>
        public int fl_u_uid
        {
            get { return _fl_u_uid; }
            set { _fl_u_uid = value; }
        }
        #endregion

        string _fl_oc_area;
        public string fl_oc_area
        {
            get { return _fl_oc_area; }
            set { _fl_oc_area = value; }
        }

        public bool IsRead { get; set; }
    }
}
