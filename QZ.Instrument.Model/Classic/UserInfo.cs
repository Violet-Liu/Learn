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
    public class UserInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~UserInfo()
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
        int _u_id;
        /// <summary>
        /// u_id
        /// </summary>
        public int u_id
        {
            get { return _u_id; }
            set { _u_id = value; }
        }
        #endregion

        #region 用户编号
        int _u_uid;
        /// <summary>
        /// u_uid
        /// </summary>
        public int u_uid
        {
            get { return _u_uid; }
            set { _u_uid = value; }
        }
        #endregion

        #region 注册类型【0本地，1QQ，2新浪】
        byte _u_type;
        /// <summary>
        /// u_type
        /// </summary>
        public byte u_type
        {
            get { return _u_type; }
            set { _u_type = value; }
        }
        #endregion

        #region 用户名
        string _u_name;
        /// <summary>
        /// u_name
        /// </summary>
        public string u_name
        {
            get { return _u_name; }
            set { _u_name = value; }
        }
        #endregion

        #region 注册邮箱
        string _u_email;
        /// <summary>
        /// u_email
        /// </summary>
        public string u_email
        {
            get { return _u_email; }
            set { _u_email = value; }
        }
        #endregion

        #region 手机号码
        string _u_mobile;
        /// <summary>
        /// u_mobile
        /// </summary>
        public string u_mobile
        {
            get { return _u_mobile; }
            set { _u_mobile = value; }
        }
        #endregion

        #region 注册密码
        string _u_pwd;
        /// <summary>
        /// u_pwd
        /// </summary>
        public string u_pwd
        {
            get { return _u_pwd; }
            set { _u_pwd = value; }
        }
        #endregion

        #region 用户状态【0=注册、1=正常、2=禁止、3=关闭】
        int _u_status;
        /// <summary>
        /// u_status
        /// </summary>
        public int u_status
        {
            get { return _u_status; }
            set { _u_status = value; }
        }
        #endregion

        #region 邮箱验证状态
        byte _u_status_email;
        /// <summary>
        /// u_status_email
        /// </summary>
        public byte u_status_email
        {
            get { return _u_status_email; }
            set { _u_status_email = value; }
        }
        #endregion

        #region 手机验证状态
        byte _u_status_mobile;
        /// <summary>
        /// u_status_mobile
        /// </summary>
        public byte u_status_mobile
        {
            get { return _u_status_mobile; }
            set { _u_status_mobile = value; }
        }
        #endregion

        #region 身份验证状态
        byte _u_status_verify;
        /// <summary>
        /// u_status_verify
        /// </summary>
        public byte u_status_verify
        {
            get { return _u_status_verify; }
            set { _u_status_verify = value; }
        }
        #endregion

        #region 注册性别
        byte _u_regsex;
        /// <summary>
        /// u_regsex
        /// </summary>
        public byte u_regsex
        {
            get { return _u_regsex; }
            set { _u_regsex = value; }
        }
        #endregion

        #region 用户头像1
        string _u_face;
        /// <summary>
        /// u_face
        /// </summary>
        public string u_face
        {
            get { return _u_face; }
            set { _u_face = value; }
        }
        #endregion

        #region 用户头像2
        string _u_face2;
        /// <summary>
        /// u_face2
        /// </summary>
        public string u_face2
        {
            get { return _u_face2; }
            set { _u_face2 = value; }
        }
        #endregion

        #region 用户头像3
        string _u_face3;
        /// <summary>
        /// u_face3
        /// </summary>
        public string u_face3
        {
            get { return _u_face3; }
            set { _u_face3 = value; }
        }
        #endregion

        #region 个性签名文字
        string _u_signature;
        /// <summary>
        /// u_signature
        /// </summary>
        public string u_signature
        {
            get { return _u_signature; }
            set { _u_signature = value; }
        }
        #endregion

        #region 个性签名图片
        string _u_signatureimg;
        /// <summary>
        /// u_signatureImg
        /// </summary>
        public string u_signatureImg
        {
            get { return _u_signatureimg; }
            set { _u_signatureimg = value; }
        }
        #endregion

        #region 注册时间
        DateTime _u_regtime;
        /// <summary>
        /// u_regTime
        /// </summary>
        public DateTime u_regTime
        {
            get { return _u_regtime; }
            set { _u_regtime = value; }
        }
        #endregion

        #region 上次登录时间
        string _u_prevlogintime;
        /// <summary>
        /// u_prevLoginTime
        /// </summary>
        public string u_prevLoginTime
        {
            get { return _u_prevlogintime; }
            set { _u_prevlogintime = value; }
        }
        #endregion

        #region 本次登录时间
        string _u_curlogintime;
        /// <summary>
        /// u_curLoginTime
        /// </summary>
        public string u_curLoginTime
        {
            get { return _u_curlogintime; }
            set { _u_curlogintime = value; }
        }
        #endregion

        #region 累计登录次数
        int _u_login_num;
        /// <summary>
        /// u_login_num
        /// </summary>
        public int u_login_num
        {
            get { return _u_login_num; }
            set { _u_login_num = value; }
        }
        #endregion

        #region 累计登录时长
        int _u_login_duration;
        /// <summary>
        /// u_login_duration
        /// </summary>
        public int u_login_duration
        {
            get { return _u_login_duration; }
            set { _u_login_duration = value; }
        }
        #endregion

        #region 金钱数
        int _u_total_money;
        /// <summary>
        /// u_total_money
        /// </summary>
        public int u_total_money
        {
            get { return _u_total_money; }
            set { _u_total_money = value; }
        }
        #endregion

        #region 名望值
        int _u_total_exp;
        /// <summary>
        /// u_total_exp
        /// </summary>
        public int u_total_exp
        {
            get { return _u_total_exp; }
            set { _u_total_exp = value; }
        }
        #endregion

        #region 用户等级
        byte _u_grade;
        /// <summary>
        /// u_grade
        /// </summary>
		public byte u_grade
        {
            get { return _u_grade; }
            set { _u_grade = value; }
        }
        #endregion

        #region 用户生日
        string _u_birthday;
        /// <summary>
        /// u_birthday
        /// </summary>
        public string u_birthday
        {
            get { return _u_birthday; }
            set { _u_birthday = value; }
        }
        #endregion

        #region 星座
        string _u_astro;
        /// <summary>
        /// u_astro
        /// </summary>
        public string u_astro
        {
            get { return _u_astro; }
            set { _u_astro = value; }
        }
        #endregion

        #region 职业
        string _u_profession;
        /// <summary>
        /// u_profession
        /// </summary>
        public string u_profession
        {
            get { return _u_profession; }
            set { _u_profession = value; }
        }
        #endregion

        #region 身高
        int _u_height;
        /// <summary>
        /// u_height
        /// </summary>
        public int u_height
        {
            get { return _u_height; }
            set { _u_height = value; }
        }
        #endregion

        #region 体重
        int _u_weight;
        /// <summary>
        /// u_weight
        /// </summary>
        public int u_weight
        {
            get { return _u_weight; }
            set { _u_weight = value; }
        }
        #endregion

        #region 居住国家
        string _u_live_country;
        /// <summary>
        /// u_live_country
        /// </summary>
        public string u_live_country
        {
            get { return _u_live_country; }
            set { _u_live_country = value; }
        }
        #endregion

        #region 居住城市
        string _u_live_city;
        /// <summary>
        /// u_live_city
        /// </summary>
        public string u_live_city
        {
            get { return _u_live_city; }
            set { _u_live_city = value; }
        }
        #endregion

        #region u_home_country
        string _u_home_country;
        /// <summary>
        /// u_home_country
        /// </summary>
        public string u_home_country
        {
            get { return _u_home_country; }
            set { _u_home_country = value; }
        }
        #endregion

        #region u_home_city
        string _u_home_city;
        /// <summary>
        /// u_home_city
        /// </summary>
        public string u_home_city
        {
            get { return _u_home_city; }
            set { _u_home_city = value; }
        }
        #endregion

        #region 爱好
        string _u_interest;
        /// <summary>
        /// u_interest
        /// </summary>
        public string u_interest
        {
            get { return _u_interest; }
            set { _u_interest = value; }
        }
        #endregion

        #region 微博地址    
        string _u_weibo;
        /// <summary>
        /// u_weibo
        /// </summary>
        public string u_weibo
        {
            get { return _u_weibo; }
            set { _u_weibo = value; }
        }
        #endregion

        #region 累计发帖数
        int _u_total_tiezi;
        /// <summary>
        /// u_total_tiezi
        /// </summary>
        public int u_total_tiezi
        {
            get { return _u_total_tiezi; }
            set { _u_total_tiezi = value; }
        }
        #endregion

        #region 累计回复数
        int _u_total_huifu;
        /// <summary>
        /// u_total_huifu
        /// </summary>
        public int u_total_huifu
        {
            get { return _u_total_huifu; }
            set { _u_total_huifu = value; }
        }
        #endregion

        #region 累计评赏数
        int _u_total_shang;
        /// <summary>
        /// u_total_shang
        /// </summary>
        public int u_total_shang
        {
            get { return _u_total_shang; }
            set { _u_total_shang = value; }
        }
        #endregion

        #region 累计评赏正数
        int _u_total_shangqz;
        /// <summary>
        /// u_total_shangQZ
        /// </summary>
        public int u_total_shangQZ
        {
            get { return _u_total_shangqz; }
            set { _u_total_shangqz = value; }
        }
        #endregion

        #region 累计评赏负数
        int _u_total_shangqf;
        /// <summary>
        /// u_total_shangQF
        /// </summary>
        public int u_total_shangQF
        {
            get { return _u_total_shangqf; }
            set { _u_total_shangqf = value; }
        }
        #endregion

        #region 累计评赏经验
        int _u_total_shangjy;
        /// <summary>
        /// u_total_shangJY
        /// </summary>
        public int u_total_shangJY
        {
            get { return _u_total_shangjy; }
            set { _u_total_shangjy = value; }
        }
        #endregion

        #region 累计评论数
        int _u_total_pinglun;
        /// <summary>
        /// u_total_pinglun
        /// </summary>
        public int u_total_pinglun
        {
            get { return _u_total_pinglun; }
            set { _u_total_pinglun = value; }
        }
        #endregion

        #region 行为分表ID
        int _u_tableid;
        /// <summary>
        /// u_tableId
        /// </summary>
        public int u_tableId
        {
            get { return _u_tableid; }
            set { _u_tableid = value; }
        }
        #endregion

        #region 今日已赏数
        int _u_today_shangF;
        /// <summary>
        /// u_today_shangF
        /// </summary>
        public int u_today_shangF
        {
            get { return _u_today_shangF; }
            set { _u_today_shangF = value; }
        }
        #endregion

        #region 今日已用经验数
        int _u_today_shangJY;
        /// <summary>
        /// u_today_shangJY
        /// </summary>
        public int u_today_shangJY
        {
            get { return _u_today_shangJY; }
            set { _u_today_shangJY = value; }
        }
        #endregion


        #region extension
        public UserInfo SetUserFace(string u_face)
        {
            this.u_face = u_face;
            return this;
        }
        #endregion
    }
}
