
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class OrgCompanyGsxtWwInfo
    {

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

        #region year
        string _year;
        /// <summary>
        /// year
        /// </summary>
        public string year
        {
            get { return _year; }
            set { _year = value; }
        }
        #endregion

        #region 类型[type]
        string _type;
        /// <summary>
        /// 类型[type]
        /// </summary>
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

        #region 名称[name]
        string _name;
        /// <summary>
        /// 名称[name]
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region 网址[webSite]
        string _website;
        /// <summary>
        /// 网址[webSite]
        /// </summary>
        public string webSite
        {
            get { return _website; }
            set { _website = value; }
        }
        #endregion
    }
}
