/*
 * 评论（帖子）举报实体类（包括
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class CommentTipOffInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~CommentTipOffInfo()
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

        #region cto_id
        int _cto_id;
        /// <summary>
        /// cto_id
        /// </summary>
        public int cto_id
        {
            get { return _cto_id; }
            set { _cto_id = value; }
        }
        #endregion

        #region 帖子id[cto_tiezi_id]
        int _cto_tiezi_id;
        /// <summary>
        /// 帖子id[cto_tiezi_id]
        /// </summary>
        public int cto_tiezi_id
        {
            get { return _cto_tiezi_id; }
            set { _cto_tiezi_id = value; }
        }
        #endregion

        #region 帖子类型，1：公司贴，2公司贴回复，3app贴，4app贴回复[cto_tiezi_type]
        byte _cto_tiezi_type;
        /// <summary>
        /// 帖子类型，1：公司贴，2公司贴回复，3app贴，4app贴回复[cto_tiezi_type]
        /// </summary>
        public byte cto_tiezi_type
        {
            get { return _cto_tiezi_type; }
            set { _cto_tiezi_type = value; }
        }
        #endregion

        #region 原告或屏蔽人uid[cto_YuanGao_Uid]
        int _cto_yuangao_uid;
        /// <summary>
        /// 原告或屏蔽人uid[cto_YuanGao_Uid]
        /// </summary>
        public int cto_YuanGao_Uid
        {
            get { return _cto_yuangao_uid; }
            set { _cto_yuangao_uid = value; }
        }
        #endregion

        #region 原告或屏蔽人名称[cto_YuanGao_Uname]
        string _cto_yuangao_uname;
        /// <summary>
        /// 原告或屏蔽人名称[cto_YuanGao_Uname]
        /// </summary>
        public string cto_YuanGao_Uname
        {
            get { return _cto_yuangao_uname; }
            set { _cto_yuangao_uname = value; }
        }
        #endregion

        #region 被告uid[cto_BeiGao_Uid]
        int _cto_beigao_uid;
        /// <summary>
        /// 被告uid[cto_BeiGao_Uid]
        /// </summary>
        public int cto_BeiGao_Uid
        {
            get { return _cto_beigao_uid; }
            set { _cto_beigao_uid = value; }
        }
        #endregion

        #region 被告人名称[cto_BeiGao_Uname]
        string _cto_beigao_uname;
        /// <summary>
        /// 被告人名称[cto_BeiGao_Uname]
        /// </summary>
        public string cto_BeiGao_Uname
        {
            get { return _cto_beigao_uname; }
            set { _cto_beigao_uname = value; }
        }
        #endregion

        #region 1：开始举报 2：举报核实 3：处理举报 [cto_status]
        byte _cto_status;
        /// <summary>
        /// 1：开始举报 2：举报核实 3：处理举报 [cto_status]
        /// </summary>
        public byte cto_status
        {
            get { return _cto_status; }
            set { _cto_status = value; }
        }
        #endregion

        #region 举报原因，描述[cto_des]
        string _cto_des;
        /// <summary>
        /// 举报原因，描述[cto_des]
        /// </summary>
        public string cto_des
        {
            get { return _cto_des; }
            set { _cto_des = value; }
        }
        #endregion

        #region 举报时间[cto_time]
        DateTime _cto_time;
        /// <summary>
        /// 举报时间[cto_time]
        /// </summary>
        public DateTime cto_time
        {
            get { return _cto_time; }
            set { _cto_time = value; }
        }
        #endregion

        #region 1:屏蔽，2：取消屏蔽，0：未屏蔽[cto_shield]
        byte _cto_shield;
        /// <summary>
        /// 1:屏蔽，2：取消屏蔽，0：未屏蔽[cto_shield]
        /// </summary>
        public byte cto_shield
        {
            get { return _cto_shield; }
            set { _cto_shield = value; }
        }
        #endregion

    }

}
