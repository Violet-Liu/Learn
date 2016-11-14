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
    public class CompanyBlackListInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyBlackListInfo()
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

        #region cbl_id
        int _cbl_id;
        /// <summary>
        /// cbl_id
        /// </summary>
        public int cbl_id
        {
            get { return _cbl_id; }
            set { _cbl_id = value; }
        }
        #endregion

        #region 公司代码[cbl_oc_code]
        string _cbl_oc_code;
        /// <summary>
        /// 公司代码[cbl_oc_code]
        /// </summary>
        public string cbl_oc_code
        {
            get { return _cbl_oc_code; }
            set { _cbl_oc_code = value; }
        }
        #endregion

        #region 公司名[cbl_oc_name]
        string _cbl_oc_name;
        /// <summary>
        /// 公司名[cbl_oc_name]
        /// </summary>
        public string cbl_oc_name
        {
            get { return _cbl_oc_name; }
            set { _cbl_oc_name = value; }
        }
        #endregion

        #region 添加到黑名单的操作员[cbl_operator]
        string _cbl_operator;
        /// <summary>
        /// 添加到黑名单的操作员[cbl_operator]
        /// </summary>
        public string cbl_operator
        {
            get { return _cbl_operator; }
            set { _cbl_operator = value; }
        }
        #endregion

        #region 操作时间[cbl_operateTime]
        DateTime _cbl_operatetime;
        /// <summary>
        /// 操作时间[cbl_operateTime]
        /// </summary>
        public DateTime cbl_operateTime
        {
            get { return _cbl_operatetime; }
            set { _cbl_operatetime = value; }
        }
        #endregion

        #region 加入黑名单的原因或其他备注[cbl_comment]
        string _cbl_comment;
        /// <summary>
        /// 加入黑名单的原因或其他备注[cbl_comment]
        /// </summary>
        public string cbl_comment
        {
            get { return _cbl_comment; }
            set { _cbl_comment = value; }
        }
        #endregion

        #region 记录状态，status为1，表示正常处于黑名单，为0，表示暂且停止对公司的屏蔽，此字段用途保留说明[cbl_status]
        bool _cbl_status;
        /// <summary>
        /// 记录状态，status为1，表示正常处于黑名单，为0，表示暂且停止对公司的屏蔽，此字段用途保留说明[cbl_status]
        /// </summary>
        public bool cbl_status
        {
            get { return _cbl_status; }
            set { _cbl_status = value; }
        }
        #endregion

    }
}
