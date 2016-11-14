using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class AreaList : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~AreaList()
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

        #region a_id
        int _a_id;
        /// <summary>
        /// a_id
        /// </summary>
        public int a_id
        {
            get { return _a_id; }
            set { _a_id = value; }
        }
        #endregion

        #region a_code
        string _a_code;
        /// <summary>
        /// a_code
        /// </summary>
        public string a_code
        {
            get { return _a_code; }
            set { _a_code = value; }
        }
        #endregion

        #region a_name
        string _a_name;
        /// <summary>
        /// a_name
        /// </summary>
        public string a_name
        {
            get { return _a_name; }
            set { _a_name = value; }
        }
        #endregion

        #region a_shortName
        string _a_shortname;
        /// <summary>
        /// a_shortName
        /// </summary>
        public string a_shortName
        {
            get { return _a_shortname; }
            set { _a_shortname = value; }
        }
        #endregion

        #region a_mapX
        string _a_mapx;
        /// <summary>
        /// a_mapX
        /// </summary>
        public string a_mapX
        {
            get { return _a_mapx; }
            set { _a_mapx = value; }
        }
        #endregion

        #region a_mapY
        string _a_mapy;
        /// <summary>
        /// a_mapY
        /// </summary>
        public string a_mapY
        {
            get { return _a_mapy; }
            set { _a_mapy = value; }
        }
        #endregion

    }

}
