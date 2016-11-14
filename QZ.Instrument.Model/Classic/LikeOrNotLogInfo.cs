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
    public class LikeOrNotLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~LikeOrNotLogInfo()
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

        #region 自动编号[lon_id]
        int _lon_id;
        /// <summary>
        /// 自动编号[lon_id]
        /// </summary>
        public int lon_id
        {
            get { return _lon_id; }
            set { _lon_id = value; }
        }
        #endregion

        #region 用户名[lon_u_name]
        string _lon_u_name;
        /// <summary>
        /// 用户名[lon_u_name]
        /// </summary>
        public string lon_u_name
        {
            get { return _lon_u_name; }
            set { _lon_u_name = value; }
        }
        #endregion

        #region 用户ID[lon_u_uid]
        int _lon_u_uid;
        /// <summary>
        /// 用户ID[lon_u_uid]
        /// </summary>
        public int lon_u_uid
        {
            get { return _lon_u_uid; }
            set { _lon_u_uid = value; }
        }
        #endregion

        #region 机构名称[lon_oc_name]
        string _lon_oc_name;
        /// <summary>
        /// 机构名称[lon_oc_name]
        /// </summary>
        public string lon_oc_name
        {
            get { return _lon_oc_name; }
            set { _lon_oc_name = value; }
        }
        #endregion

        #region 机构代码[lon_oc_code]
        string _lon_oc_code;
        /// <summary>
        /// 机构代码[lon_oc_code]
        /// </summary>
        public string lon_oc_code
        {
            get { return _lon_oc_code; }
            set { _lon_oc_code = value; }
        }
        #endregion

        #region 日期[lon_date]
        DateTime _lon_date;
        /// <summary>
        /// 日期[lon_date]
        /// </summary>
        public DateTime lon_date
        {
            get { return _lon_date; }
            set { _lon_date = value; }
        }
        #endregion

        #region 是否有效[lon_valid]
        int _lon_valid;
        /// <summary>
        /// 是否有效[lon_valid]
        /// </summary>
        public int lon_valid
        {
            get { return _lon_valid; }
            set { _lon_valid = value; }
        }
        #endregion

        #region 地区编码[lon_oc_area]
        string _lon_oc_area;
        /// <summary>
        /// 地区编码[lon_oc_area]
        /// </summary>
        public string lon_oc_area
        {
            get { return _lon_oc_area; }
            set { _lon_oc_area = value; }
        }
        #endregion

        #region 评论类型[lon_type]
        int _lon_type;
        /// <summary>
        /// 评论类型[lon_type]
        /// </summary>
        public int lon_type
        {
            get { return _lon_type; }
            set { _lon_type = value; }
        }
        #endregion

    }
}
