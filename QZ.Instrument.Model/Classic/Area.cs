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
    public class Province
    {
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

        public List<City> children { get; set; }
    }

    public class City
    {
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
    }
}
