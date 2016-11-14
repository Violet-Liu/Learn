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
    public class FavoriteLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~FavoriteLogInfo()
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

        #region 自动编号[fl_id]
        int _fl_id;
        /// <summary>
        /// 自动编号[fl_id]
        /// </summary>
        public int fl_id
        {
            get { return _fl_id; }
            set { _fl_id = value; }
        }
        #endregion

        #region 机构名称[fl_oc_name]
        string _fl_oc_name;
        /// <summary>
        /// 机构名称[fl_oc_name]
        /// </summary>
        public string fl_oc_name
        {
            get { return _fl_oc_name; }
            set { _fl_oc_name = value; }
        }
        #endregion

        #region 机构代码[fl_oc_code]
        string _fl_oc_code;
        /// <summary>
        /// 机构代码[fl_oc_code]
        /// </summary>
        public string fl_oc_code
        {
            get { return _fl_oc_code; }
            set { _fl_oc_code = value; }
        }
        #endregion

        #region 用户名[fl_u_name]
        string _fl_u_name;
        /// <summary>
        /// 用户名[fl_u_name]
        /// </summary>
        public string fl_u_name
        {
            get { return _fl_u_name; }
            set { _fl_u_name = value; }
        }
        #endregion

        #region 用户ID[fl_u_uid]
        int _fl_u_uid;
        /// <summary>
        /// 用户ID[fl_u_uid]
        /// </summary>
        public int fl_u_uid
        {
            get { return _fl_u_uid; }
            set { _fl_u_uid = value; }
        }
        #endregion

        #region 收藏日期[fl_date]
        DateTime _fl_date;
        /// <summary>
        /// 收藏日期[fl_date]
        /// </summary>
        public DateTime fl_date
        {
            get { return _fl_date; }
            set { _fl_date = value; }
        }
        #endregion

        #region 是否有效[fl_valid]
        int _fl_valid;
        /// <summary>
        /// 是否有效[fl_valid]
        /// </summary>
        public int fl_valid
        {
            get { return _fl_valid; }
            set { _fl_valid = value; }
        }
        #endregion

        #region 地区编码[fl_oc_area]
        string _fl_oc_area;
        /// <summary>
        /// 地区编码[fl_oc_area]
        /// </summary>
        public string fl_oc_area
        {
            get { return _fl_oc_area; }
            set { _fl_oc_area = value; }
        }
        #endregion

    }
}
