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
    public class CompanyInfoCorrectInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyInfoCorrectInfo()
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

        #region 自动编号[cic_id]
        int _cic_id;
        /// <summary>
        /// 自动编号[cic_id]
        /// </summary>
        public int cic_id
        {
            get { return _cic_id; }
            set { _cic_id = value; }
        }
        #endregion

        #region 用户名[cic_u_name]
        string _cic_u_name;
        /// <summary>
        /// 用户名[cic_u_name]
        /// </summary>
        public string cic_u_name
        {
            get { return _cic_u_name; }
            set { _cic_u_name = value; }
        }
        #endregion

        #region 用户ID[cic_u_uid]
        int _cic_u_uid;
        /// <summary>
        /// 用户ID[cic_u_uid]
        /// </summary>
        public int cic_u_uid
        {
            get { return _cic_u_uid; }
            set { _cic_u_uid = value; }
        }
        #endregion

        #region 机构代码[cic_oc_code]
        string _cic_oc_code;
        /// <summary>
        /// 机构代码[cic_oc_code]
        /// </summary>
        public string cic_oc_code
        {
            get { return _cic_oc_code; }
            set { _cic_oc_code = value; }
        }
        #endregion

        #region 机构名称[cic_oc_name]
        string _cic_oc_name;
        /// <summary>
        /// 机构名称[cic_oc_name]
        /// </summary>
        public string cic_oc_name
        {
            get { return _cic_oc_name; }
            set { _cic_oc_name = value; }
        }
        #endregion

        #region 纠错类型[cic_type]
        int _cic_type;
        /// <summary>
        /// 纠错类型[cic_type]
        /// </summary>
        public int cic_type
        {
            get { return _cic_type; }
            set { _cic_type = value; }
        }
        #endregion

        #region 纠错人手机[cic_phone]
        string _cic_phone;
        /// <summary>
        /// 纠错人手机[cic_phone]
        /// </summary>
        public string cic_phone
        {
            get { return _cic_phone; }
            set { _cic_phone = value; }
        }
        #endregion

        #region 纠错内容[cic_content]
        string _cic_content;
        /// <summary>
        /// 纠错内容[cic_content]
        /// </summary>
        public string cic_content
        {
            get { return _cic_content; }
            set { _cic_content = value; }
        }
        #endregion

        #region 纠错提交时间[cic_date]
        DateTime _cic_date;
        /// <summary>
        /// 纠错提交时间[cic_date]
        /// </summary>
        public DateTime cic_date
        {
            get { return _cic_date; }
            set { _cic_date = value; }
        }
        #endregion

    }
}
