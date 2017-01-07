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
    public class FavoriteViewTraceInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~FavoriteViewTraceInfo()
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

        #region fvt_id
        int _fvt_id;
        /// <summary>
        /// fvt_id
        /// </summary>
        public int fvt_id
        {
            get { return _fvt_id; }
            set { _fvt_id = value; }
        }
        #endregion

        #region 公司机构代码[fvt_oc_code]
        string _fvt_oc_code;
        /// <summary>
        /// 公司机构代码[fvt_oc_code]
        /// </summary>
        public string fvt_oc_code
        {
            get { return _fvt_oc_code; }
            set { _fvt_oc_code = value; }
        }
        #endregion

        #region 用户id[fvt_u_uid]
        int _fvt_u_uid;
        /// <summary>
        /// 用户id[fvt_u_uid]
        /// </summary>
        public int fvt_u_uid
        {
            get { return _fvt_u_uid; }
            set { _fvt_u_uid = value; }
        }
        #endregion

        #region 用户浏览时间[fvt_viewtime]
        DateTime _fvt_viewtime;
        /// <summary>
        /// 用户浏览时间[fvt_viewtime]
        /// </summary>
        public DateTime fvt_viewtime
        {
            get { return _fvt_viewtime; }
            set { _fvt_viewtime = value; }
        }
        #endregion

        #region 状态，1表示正常，2表示非正常，除非公司更新时间大于浏览时间，否则非正常情况下，不显示这条数据[fvt_status]
        bool _fvt_status;
        /// <summary>
        /// 状态，1表示正常，2表示非正常，除非公司更新时间大于浏览时间，否则非正常情况下，不显示这条数据[fvt_status]
        /// </summary>
        public bool fvt_status
        {
            get { return _fvt_status; }
            set { _fvt_status = value; }
        }
        #endregion

    }

    public class CompanyStatisticsInfo
    {
        public int id { get; set; }
        public string updatetime { get; set; }
        public string oc_code { get; set; }

        public int gudongxinxi { get; set; }

        public int zhuyaorenyuan { get; set; }

        public int biangengxinxi { get; set; }

        public int nianbao { get; set; }

        public int shangbiaoxinxi { get; set; }
        public int zhuanlixinxi { get; set; }

        public int ruanjianzhuzuoquan { get; set; }
        public int zuopinzhuzuoquan { get; set; }
        public int yuminbeian { get; set; }
        public int shangpintiaomaxinxi { get; set; }
        public int changshangbianmaxinxi { get; set; }
        public int renjianwei { get; set; }
        public int xiaofangju { get; set; }
        public int panjuewenshu { get; set; }
        public int fayuangonggao { get; set; }
        public int beizhixingren { get; set; }
        public int shixinren { get; set; }
        public int zhaopin { get; set; }
        public int zhuanhuihuikan { get; set; }
        public int duiwaitouzi { get; set; }
        public int fenzhi { get; set; }
        public int ext1 { get; set; }
        public int ext2 { get; set; }
        public int ext3 { get; set; }
        public int ext4 { get; set; }
        public int ext5 { get; set; }
        public int ext6 { get; set; }
        public int ext7 { get; set; }
        public int ext8 { get; set; }
        public int ext9 { get; set; }
        public int ext10 { get; set; }

    }

}
