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
    public class User_UpdateNameLog : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~User_UpdateNameLog()
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


        #region 自动编号
        int _id;
        /// <summary>
        /// u_id
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region 用户id
        public int userid { get; set; }

        #endregion

        #region  用户名前
        public string namefront { get; set; }
        #endregion

        #region  用户名后
        public string nameback { get; set; }

        #endregion

        #region 客户端ip
        public string clientIp { get; set; }

        #endregion

        #region 版本号
        public string version { get; set; }

        #endregion

        #region 平台类型
        public string platform { get; set; }

        #endregion

        #region 创建时间

        public DateTime createdate { get; set; }
        #endregion


    }
}
