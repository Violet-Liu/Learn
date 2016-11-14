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
    public class OrgCompanyBrand
    {

        #region ob_id
        int _ob_id;
        /// <summary>
        /// ob_id
        /// </summary>
        public int ob_id
        {
            get { return _ob_id; }
            set { _ob_id = value; }
        }
        #endregion

        #region ob_oc_code
        string _ob_oc_code;
        /// <summary>
        /// ob_oc_code
        /// </summary>
        public string ob_oc_code
        {
            get { return _ob_oc_code; }
            set { _ob_oc_code = value; }
        }
        #endregion

        #region ob_regNo
        string _ob_regno;
        /// <summary>
        /// ob_regNo
        /// </summary>
        public string ob_regNo
        {
            get { return _ob_regno; }
            set { _ob_regno = value; }
        }
        #endregion

        #region ob_classNo
        string _ob_classno;
        /// <summary>
        /// ob_classNo
        /// </summary>
        public string ob_classNo
        {
            get { return _ob_classno; }
            set { _ob_classno = value; }
        }
        #endregion

        #region ob_applicationDate
        DateTime _ob_applicationdate;
        /// <summary>
        /// ob_applicationDate
        /// </summary>
        public DateTime ob_applicationDate
        {
            get { return _ob_applicationdate; }
            set { _ob_applicationdate = value; }
        }
        #endregion

        #region ob_name
        string _ob_name;
        /// <summary>
        /// ob_name
        /// </summary>
        public string ob_name
        {
            get { return _ob_name; }
            set { _ob_name = value; }
        }
        #endregion

        #region ob_proposer
        string _ob_proposer;
        /// <summary>
        /// ob_proposer
        /// </summary>
        public string ob_proposer
        {
            get { return _ob_proposer; }
            set { _ob_proposer = value; }
        }
        #endregion

        #region ob_img
        string _ob_img;
        /// <summary>
        /// ob_img
        /// </summary>
        public string ob_img
        {
            get { return _ob_img; }
            set { _ob_img = value; }
        }
        #endregion

        #region ob_csggqh
        string _ob_csggqh;
        /// <summary>
        /// ob_csggqh
        /// </summary>
        public string ob_csggqh
        {
            get { return _ob_csggqh; }
            set { _ob_csggqh = value; }
        }
        #endregion

        #region ob_zcggqh
        string _ob_zcggqh;
        /// <summary>
        /// ob_zcggqh
        /// </summary>
        public string ob_zcggqh
        {
            get { return _ob_zcggqh; }
            set { _ob_zcggqh = value; }
        }
        #endregion

        #region ob_csggrq
        string _ob_csggrq;
        /// <summary>
        /// ob_csggrq
        /// </summary>
        public string ob_csggrq
        {
            get { return _ob_csggrq; }
            set { _ob_csggrq = value; }
        }
        #endregion

        #region ob_zcggrq
        string _ob_zcggrq;
        /// <summary>
        /// ob_zcggrq
        /// </summary>
        public string ob_zcggrq
        {
            get { return _ob_zcggrq; }
            set { _ob_zcggrq = value; }
        }
        #endregion

        #region ob_zyksqx
        string _ob_zyksqx;
        /// <summary>
        /// ob_zyksqx
        /// </summary>
        public string ob_zyksqx
        {
            get { return _ob_zyksqx; }
            set { _ob_zyksqx = value; }
        }
        #endregion

        #region ob_zyjsqx
        string _ob_zyjsqx;
        /// <summary>
        /// ob_zyjsqx
        /// </summary>
        public string ob_zyjsqx
        {
            get { return _ob_zyjsqx; }
            set { _ob_zyjsqx = value; }
        }
        #endregion

        #region ob_dlrmc
        string _ob_dlrmc;
        /// <summary>
        /// ob_dlrmc
        /// </summary>
        public string ob_dlrmc
        {
            get { return _ob_dlrmc; }
            set { _ob_dlrmc = value; }
        }
        #endregion


        public string ob_status { get; set; } = string.Empty;
        public string ob_class_name { get; set; } = string.Empty;
    }
}
