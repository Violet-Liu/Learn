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
    public class OrgCompanyGsxtBgsxInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~OrgCompanyGsxtBgsxInfo()
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

        #region id
        int _id;
        /// <summary>
        /// id
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region oc_code
        string _oc_code;
        /// <summary>
        /// oc_code
        /// </summary>
        public string oc_code
        {
            get { return _oc_code; }
            set { _oc_code = value; }
        }
        #endregion

        #region 变更事项[bgsx]
        string _bgsx;
        /// <summary>
        /// 变更事项[bgsx]
        /// </summary>
        public string bgsx
        {
            get { return _bgsx; }
            set { _bgsx = value; }
        }
        #endregion

        #region 变更前内容[bgq]
        string _bgq;
        /// <summary>
        /// 变更前内容[bgq]
        /// </summary>
        public string bgq
        {
            get { return _bgq; }
            set { _bgq = value; }
        }
        #endregion

        #region 变更后内容[bgh]
        string _bgh;
        /// <summary>
        /// 变更后内容[bgh]
        /// </summary>
        public string bgh
        {
            get { return _bgh; }
            set { _bgh = value; }
        }
        #endregion

        #region 变更日期[bgrq]
        DateTime _bgrq;
        /// <summary>
        /// 变更日期[bgrq]
        /// </summary>
        public DateTime bgrq
        {
            get { return _bgrq; }
            set { _bgrq = value; }
        }
        #endregion

    }
}
