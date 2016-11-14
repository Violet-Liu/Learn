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
    public class CompanyEvaluateInfo : IDisposable
    {
        #region IDisposable 接口实现

        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CompanyEvaluateInfo()
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

        #endregion IDisposable 接口实现

        #region 自动编号[ce_id]

        private int _ce_id;

        /// <summary>
        /// 自动编号[ce_id]
        /// </summary>
        public int ce_id
        {
            get { return _ce_id; }
            set { _ce_id = value; }
        }

        #endregion 自动编号[ce_id]

        #region 机构名称[ce_oc_name]

        private string _ce_oc_name;

        /// <summary>
        /// 机构名称[ce_oc_name]
        /// </summary>
        public string ce_oc_name
        {
            get { return _ce_oc_name; }
            set { _ce_oc_name = value; }
        }

        #endregion 机构名称[ce_oc_name]

        #region 机构代码[ce_oc_code]

        private string _ce_oc_code;

        /// <summary>
        /// 机构代码[ce_oc_code]
        /// </summary>
        public string ce_oc_code
        {
            get { return _ce_oc_code; }
            set { _ce_oc_code = value; }
        }

        #endregion 机构代码[ce_oc_code]

        #region 地区代码[ce_oc_area]

        private string _ce_oc_area;

        /// <summary>
        /// 地区代码[ce_oc_area]
        /// </summary>
        public string ce_oc_area
        {
            get { return _ce_oc_area; }
            set { _ce_oc_area = value; }
        }

        #endregion 地区代码[ce_oc_area]

        #region 吐槽数[ce_tucaoNum]

        private int _ce_tucaonum;

        /// <summary>
        /// 吐槽数[ce_tucaoNum]
        /// </summary>
        public int ce_tucaoNum
        {
            get { return _ce_tucaonum; }
            set { _ce_tucaonum = value; }
        }

        #endregion 吐槽数[ce_tucaoNum]

        #region 点赞数[ce_likeNum]

        private int _ce_likenum;

        /// <summary>
        /// 点赞数[ce_likeNum]
        /// </summary>
        public int ce_likeNum
        {
            get { return _ce_likenum; }
            set { _ce_likenum = value; }
        }

        #endregion 点赞数[ce_likeNum]

        #region 点踩数[ce_notLikeNum]

        private int _ce_notlikenum;

        /// <summary>
        /// 点踩数[ce_notLikeNum]
        /// </summary>
        public int ce_notLikeNum
        {
            get { return _ce_notlikenum; }
            set { _ce_notlikenum = value; }
        }

        #endregion 点踩数[ce_notLikeNum]

        #region 浏览数[ce_visitNum]

        private int _ce_visitnum;

        /// <summary>
        /// 浏览数[ce_visitNum]
        /// </summary>
        public int ce_visitNum
        {
            get { return _ce_visitnum; }
            set { _ce_visitnum = value; }
        }

        #endregion 浏览数[ce_visitNum]

        #region 收藏数[ce_FavorNum]

        private int _ce_favornum;

        /// <summary>
        /// 收藏数[ce_FavorNum]
        /// </summary>
        public int ce_FavorNum
        {
            get { return _ce_favornum; }
            set { _ce_favornum = value; }
        }

        #endregion 收藏数[ce_FavorNum]

        public CompanyEvaluateInfo Set_Oc_Code(string oc_code) => Fluent.Assign_0(this, e => e.ce_oc_code = oc_code);
        public CompanyEvaluateInfo Set_Oc_Area(string oc_area) => Fluent.Assign_0(this, e => e.ce_oc_area = oc_area);
        public CompanyEvaluateInfo Set_Oc_Name(string oc_name) => Fluent.Assign_0(this, e => e.ce_oc_name = oc_name);
    }
}
