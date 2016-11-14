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
    public class User_SMSLogInfo : IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~User_SMSLogInfo()
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
        int _sms_id;
        /// <summary>
        /// 自动增长列
        /// </summary>
        public int Sms_id
        {
            get { return _sms_id; }
            set { _sms_id = value; }
        }
        /// <summary>
        /// 客户端类型 电脑：“PC”，手机：“MB”
        /// </summary>
        byte _sms_type;

        public byte Sms_type
        {
            get { return _sms_type; }
            set { _sms_type = value; }
        }
        string _sms_code;
        /// <summary>
        /// 发送的验证码
        /// </summary>
        public string Sms_code
        {
            get { return _sms_code; }
            set { _sms_code = value; }
        }
        byte _sms_success;
        /// <summary>
        /// 发送是否成功  0：失败，1：成功
        /// </summary>
        public byte Sms_success
        {
            get { return _sms_success; }
            set { _sms_success = value; }
        }
        DateTime _sms_time;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Sms_time
        {
            get { return _sms_time; }
            set { _sms_time = value; }
        }
        string _sms_phone;
        /// <summary>
        /// 发送的手机号码
        /// </summary>
        public string Sms_phone
        {
            get { return _sms_phone; }
            set { _sms_phone = value; }
        }
        byte _sms_purpose;
        /// <summary>
        /// 目的:"找回密码"，“注册”
        /// </summary>
        public byte Sms_purpose
        {
            get { return _sms_purpose; }
            set { _sms_purpose = value; }
        }
    }
}
