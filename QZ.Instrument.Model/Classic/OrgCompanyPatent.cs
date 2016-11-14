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
    public class CompanyPatent
    {

        #region ID[ID]
        int _id;
        /// <summary>
        /// ID[ID]
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region oc_code[oc_code]
        string _oc_code;
        /// <summary>
        /// oc_code[oc_code]
        /// </summary>
        public string oc_code
        {
            get { return _oc_code; }
            set { _oc_code = value; }
        }
        #endregion

        #region oc_name公司名称[oc_name]
        string _oc_name;
        /// <summary>
        /// oc_name公司名称[oc_name]
        /// </summary>
        public string oc_name
        {
            get { return _oc_name; }
            set { _oc_name = value; }
        }
        #endregion

        #region 申请号[Patent_No]
        string _patent_no;
        /// <summary>
        /// 申请号[Patent_No]
        /// </summary>
        public string Patent_No
        {
            get { return _patent_no; }
            set { _patent_no = value; }
        }
        #endregion

        #region 公开号[Patent_gkh]
        string _patent_gkh;
        /// <summary>
        /// 公开号[Patent_gkh]
        /// </summary>
        public string Patent_gkh
        {
            get { return _patent_gkh; }
            set { _patent_gkh = value; }
        }
        #endregion

        #region 发明名称[Patent_Name]
        string _patent_name;
        /// <summary>
        /// 发明名称[Patent_Name]
        /// </summary>
        public string Patent_Name
        {
            get { return _patent_name; }
            set { _patent_name = value; }
        }
        #endregion

        #region 专利类型[Patent_Type]
        string _patent_type;
        /// <summary>
        /// 专利类型[Patent_Type]
        /// </summary>
        public string Patent_Type
        {
            get { return _patent_type; }
            set { _patent_type = value; }
        }
        #endregion

        #region 法律状态[Patent_Status]
        string _patent_status;
        /// <summary>
        /// 法律状态[Patent_Status]
        /// </summary>
        public string Patent_Status
        {
            get { return _patent_status; }
            set { _patent_status = value; }
        }
        #endregion

        #region 申请人[Patent_sqr]
        string _patent_sqr;
        /// <summary>
        /// 申请人[Patent_sqr]
        /// </summary>
        public string Patent_sqr
        {
            get { return _patent_sqr; }
            set { _patent_sqr = value; }
        }
        #endregion

        #region 地址[Patent_Addr]
        string _patent_addr;
        /// <summary>
        /// 地址[Patent_Addr]
        /// </summary>
        public string Patent_Addr
        {
            get { return _patent_addr; }
            set { _patent_addr = value; }
        }
        #endregion


        #region 图片[Patent_img]
        string _patent_img;
        /// <summary>
        /// 图片[Patent_img]
        /// </summary>
        public string Patent_img
        {
            get { return _patent_img; }
            set { _patent_img = value; }
        }
        #endregion

        public DateTime sq_date { get; set; }  /* 申请日 */
        public int patent_year { get; set; }
        public string patent_date { get; set; }
    }

    public class PatentApplicant
    {
        public string aname { get; set; }
    }
    public class PatentDesigner
    {
        public string dname { get; set; }
    }
}
