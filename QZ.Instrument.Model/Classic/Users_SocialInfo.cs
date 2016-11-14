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
    /// <summary>
    /// 社会化接入管理
    /// </summary>
    public class Users_SocialInfo
    {

        #region us_id
        int _us_id;
        /// <summary>
        /// us_id
        /// </summary>
        public int us_id
        {
            get { return _us_id; }
            set { _us_id = value; }
        }
        #endregion

        #region us_u_uid
        int _us_u_uid;
        /// <summary>
        /// us_u_uid
        /// </summary>
        public int us_u_uid
        {
            get { return _us_u_uid; }
            set { _us_u_uid = value; }
        }
        #endregion

        #region us_type
        byte _us_type;
        /// <summary>
        /// us_type
        /// </summary>
        public byte us_type
        {
            get { return _us_type; }
            set { _us_type = value; }
        }
        #endregion

        #region us_uid
        string _us_uid;
        /// <summary>
        /// us_uid
        /// </summary>
        public string us_uid
        {
            get { return _us_uid; }
            set { _us_uid = value; }
        }
        #endregion

        #region us_nick
        string _us_nick;
        /// <summary>
        /// us_nick
        /// </summary>
        public string us_nick
        {
            get { return _us_nick; }
            set { _us_nick = value; }
        }
        #endregion

        #region us_name
        string _us_name;
        /// <summary>
        /// us_name
        /// </summary>
        public string us_name
        {
            get { return _us_name; }
            set { _us_name = value; }
        }
        #endregion

        #region us_location
        string _us_location;
        /// <summary>
        /// us_location
        /// </summary>
        public string us_location
        {
            get { return _us_location; }
            set { _us_location = value; }
        }
        #endregion

        #region us_siteurl
        string _us_siteurl;
        /// <summary>
        /// us_siteurl
        /// </summary>
        public string us_siteurl
        {
            get { return _us_siteurl; }
            set { _us_siteurl = value; }
        }
        #endregion

        #region us_headImg
        string _us_headimg;
        /// <summary>
        /// us_headImg
        /// </summary>
        public string us_headImg
        {
            get { return _us_headimg; }
            set { _us_headimg = value; }
        }
        #endregion

        #region u_headImg_large
        /// <summary>
        /// 用户大头像
        /// </summary>
        public string u_headImg_large { get; set; }
        #endregion

        #region us_gender
        string _us_gender;
        /// <summary>
        /// us_gender
        /// </summary>
        public string us_gender
        {
            get { return _us_gender; }
            set { _us_gender = value; }
        }
        #endregion

        #region us_fansNum
        int _us_fansnum;
        /// <summary>
        /// us_fansNum
        /// </summary>
        public int us_fansNum
        {
            get { return _us_fansnum; }
            set { _us_fansnum = value; }
        }
        #endregion

        #region us_attentionsNum
        int _us_attentionsnum;
        /// <summary>
        /// us_attentionsNum
        /// </summary>
        public int us_attentionsNum
        {
            get { return _us_attentionsnum; }
            set { _us_attentionsnum = value; }
        }
        #endregion

        #region us_favorsNum
        int _us_favorsnum;
        /// <summary>
        /// us_favorsNum
        /// </summary>
        public int us_favorsNum
        {
            get { return _us_favorsnum; }
            set { _us_favorsnum = value; }
        }
        #endregion

        #region us_contentsNum
        int _us_contentsnum;
        /// <summary>
        /// us_contentsNum
        /// </summary>
        public int us_contentsNum
        {
            get { return _us_contentsnum; }
            set { _us_contentsnum = value; }
        }
        #endregion

        #region us_verified
        bool _us_verified;
        /// <summary>
        /// us_verified
        /// </summary>
        public bool us_verified
        {
            get { return _us_verified; }
            set { _us_verified = value; }
        }
        #endregion

        #region us_code
        string _us_code;
        /// <summary>
        /// us_code
        /// </summary>
        public string us_code
        {
            get { return _us_code; }
            set { _us_code = value; }
        }
        #endregion

        #region us_openkey
        string _us_openkey;
        /// <summary>
        /// us_openkey
        /// </summary>
        public string us_openkey
        {
            get { return _us_openkey; }
            set { _us_openkey = value; }
        }
        #endregion

        #region us_sync2
        bool _us_sync2;
        /// <summary>
        /// us_sync2
        /// </summary>
        public bool us_sync2
        {
            get { return _us_sync2; }
            set { _us_sync2 = value; }
        }
        #endregion

        #region us_createTime
        DateTime _us_createtime;
        /// <summary>
        /// us_createTime
        /// </summary>
        public DateTime us_createTime
        {
            get { return _us_createtime; }
            set { _us_createtime = value; }
        }
        #endregion

        #region us_lastLogin
        string _us_lastlogin;
        /// <summary>
        /// us_lastLogin
        /// </summary>
        public string us_lastLogin
        {
            get { return _us_lastlogin; }
            set { _us_lastlogin = value; }
        }
        #endregion

        #region us_loginNum
        int _us_loginnum;
        /// <summary>
        /// us_loginNum
        /// </summary>
        public int us_loginNum
        {
            get { return _us_loginnum; }
            set { _us_loginnum = value; }
        }
        #endregion

    }
}
