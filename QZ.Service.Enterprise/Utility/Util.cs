using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Xml.Linq;
using System.Configuration;
using QZ.Foundation.Utility;
using QZ.Instrument.Model;
using QZ.Foundation.Model;
using QZ.Instrument.LogClient;
using QZ.Instrument.Global;
using QZ.Foundation.Cache;
using QZ.Foundation.Document;
using QZ.Instrument.Utility;

namespace QZ.Service.Enterprise
{
    public class Util
    {
        private const decimal Max_Content_Similarity = 0.8M;
        private const decimal Max_Similar_Submit_Times = 3;
        static object sms_lock_firstTime = new object();
        static object sms_lock_lastTime = new object();
        static Util()
        {
            _proxy = new Proxy();
        }
        /// <summary>
        /// Get remote(client) ip
        /// </summary>
        /// <returns></returns>
        public static string Get_RemoteIp()
        {
            var endpoint = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint.Address;
        }

        /// <summary>
        /// Set web operation context
        /// </summary>
        /// <returns></returns>
        public static WebOperationContext Set_Context()
        {
            WebOperationContext woc = WebOperationContext.Current;
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return woc;
        }

        private static Proxy _proxy;
        [Conditional("DEBUG")]
        public static void Log_Info(string func, Location location, string message, string analysis)
        {
            var log_M = new Log_M() { Uri = func, Message = message, Analysis = analysis + "\n\n", Location = location };
            _proxy.Log_Info(log_M);
        }
        [Conditional("DEBUG")]
        public static void Log_Error(string func, string message, string analysis)
        {
            var log_M = new Log_M() { Uri = func, Message = message, Analysis = analysis };
            _proxy.Log_Error(log_M);
        }
        [Conditional("DEBUG")]
        public static void Log_Warn(string func, string message, string analysis)
        {
            var log_M = new Log_M() { Uri = func, Message = message, Analysis = analysis };
            _proxy.Log_Warn(log_M);
        }
        public static void Log_Fatal(string func, string message, string analysis)
        {
            var log_M = new Log_M() { Uri = func, Message = message, Analysis = analysis };
            _proxy.Log_Fatal(log_M);
        }


        /// <summary>
        /// 获取友好时间
        /// </summary>
        /// <param name="time">评论时间</param>
        /// <returns>评论友好时间</returns>
        public static string Get_Gentle_Time(DateTime time)
        {
            TimeSpan ts = DateTime.Now - time;
            if (ts.TotalDays >= 365)
            {
                return "很久以前";
            }
            if (ts.TotalDays >= 30)
            {
                return ((int)(ts.TotalDays / 30)).ToString() + "月前";
            }
            if (ts.TotalDays >= 1)
            {
                return ((int)ts.TotalDays).ToString() + "天前";
            }
            if (ts.TotalHours >= 1)
            {
                return ((int)ts.TotalHours).ToString() + "小时前";
            }
            if (ts.TotalMinutes >= 1)
            {
                return ((int)ts.TotalMinutes).ToString() + "分钟前";
            }
            return "刚刚";
        }

        /// <summary>
        /// 获得用户头像
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string UserFace_Get(int u_id)
        {
            /*
             * 先读取数据库头像字段，如果没有，拼出头像
             */
            var user = DataAccess.User_FromId_Select(u_id);
            if (user == null)
            {
                return "";
            }
            // return 第三方头像地址
            if (!string.IsNullOrEmpty(user.u_face) && !user.u_face.Contains("qianzhan.com"))
            {

                return user.u_face;
            }

            // 构造本地服务器头像地址
            // 用户头像
            string userId = user.u_id.ToString();
            int dir0 = u_id / 60000 + 10;
            string dir1 = userId.Substring(userId.Length - 2);

            var domain = DataBus.Metadata_Uris.FirstOrDefault(uri => uri.name.Equals("user_head"))?.value;
            if(!string.IsNullOrEmpty(domain))
                return string.Format("{0}/{1}/{2}/{3}.large.gif", domain, dir0, dir1, userId);//本地版本
            return string.Format("http://face.qianzhan.com/{0}/{1}/{2}.large.gif", dir0, dir1, userId);
        }

        public static string UserFace_Get(string old_u_face, string u_id)
        {
            if (!string.IsNullOrEmpty(old_u_face) && !old_u_face.ToLower().Contains("qianzhan.com"))
                return old_u_face;

            if (u_id.Length < 2)
                return string.Empty;

            int uid = u_id.ToInt();
            // 构造本地服务器头像地址
            // 用户头像
            int dir0 = uid / 60000 + 10;
            string dir1 = u_id.Substring(u_id.Length - 2);

            // 从配置文件读到用户头像域名
            var domain = DataBus.Metadata_Uris.FirstOrDefault(uri => uri.name.Equals("user_head"))?.value;
            if (string.IsNullOrEmpty(domain))
                return string.Format("{0}/{1}/{2}/{3}.large.gif", domain, dir0, dir1, u_id);//本地版本
            return string.Format("http://face.qianzhan.com/{0}/{1}/{2}.large.gif", dir0, dir1, u_id);
        }


        public static string VerifyCode_Get(string phone)
        {

            //验证码key
            string key = phone;
            //发送次数key
            string numKey = "num_" + key;
            //最后一次发送时间key
            string lastTimeKey = "ltime_" + key;
            //第一次发送时间
            string firstTimeKey = "ftime" + key;
            //发送状态key
            string sendStatuKey = "statu_" + key;
            //获取短信验证码测试
            int limitNum = 5;

            #region ignore when in debug mode
            if (ConfigurationManager.AppSettings["verifycode_debug"] != "true")
            {
                #region 第一次获取验证码的时间 ftimeObj

                object ftimeObj = CacheHelper.Cache_Get(firstTimeKey);

                if (ftimeObj == null)
                {
                    lock (sms_lock_firstTime)
                    {
                        ftimeObj = CacheHelper.Cache_Get(firstTimeKey);
                        if (ftimeObj == null)
                        {
                            CacheHelper.Cache_Store(firstTimeKey, DateTime.Now, DateTime.Now.AddMinutes(20));
                        }
                    }
                }
                else
                {
                    // 重新保存第一次发送时间，确保在20分钟内每一次重发送短信相对于第一次的开始时间一致
                    CacheHelper.Cache_Store(firstTimeKey, (DateTime)ftimeObj, DateTime.Now.AddMinutes(20));
                }

                #endregion

                #region 比较获取短信验证码时间间隔 2 分钟 timeObj

                object timeObj = CacheHelper.Cache_Get(lastTimeKey);
                if (timeObj == null)
                {
                    lock (sms_lock_lastTime)
                    {
                        timeObj = CacheHelper.Cache_Get(lastTimeKey);
                        if (timeObj == null)
                        {
                            // 保存最后一次发送验证码时间
                            CacheHelper.Cache_Store(lastTimeKey, DateTime.Now, DateTime.Now.AddMinutes(2));
                        }
                        else
                        {
                            DateTime lastSendTime = (DateTime)timeObj;
                            if (DateTime.Now.AddMinutes(-2) < lastSendTime)
                            {
                                // 时间间隔小于2分钟
                                return "busy";
                            }
                        }
                    }
                }
                else
                {
                    DateTime lastSendTime = (DateTime)timeObj;
                    if (DateTime.Now.AddMinutes(-2) < lastSendTime)
                    {
                        // 时间间隔小于2分钟
                        return "busy";
                    }
                }

                #endregion

                #region 获取短信验证码的次数(相对于最后一次发送短信2小时内累计) 20分钟内超过5次，提示更换手机号码

                //获取短信验证码的次数
                object numObj = CacheHelper.Cache_Get(numKey);
                int num = 1;
                if (numObj != null)
                {
                    num = (int)numObj;
                    //表明是至少在20分钟内尝试的
                    if (ftimeObj != null)
                    {
                        num++;
                        if (num > limitNum)
                        {
                            //如果超过限制次数，续期第一发送短信时间，保存该号码尝试次数并提示要求更换号码再试
                            CacheHelper.Cache_Store(firstTimeKey, (DateTime)ftimeObj, DateTime.Now.AddMinutes(20));
                            //CacheHelper.Cache_Store(lastTimeKey, DateTime.Now, DateTime.Now.AddMinutes(2));
                            CacheHelper.Cache_Store(numKey, num, DateTime.Now.AddHours(2));
                            return string.Empty;
                        }

                    }
                    else
                    {
                        //20分钟外重试并且
                        //2小时内尝试次数超出预期
                        //2小时内尝试超12次
                        if (num >= 12)
                        {
                            //如果超过限制次数 保存该号码尝试次数并提示要求更换号码再试
                            //CacheHelper.Cache_Store(firstTimeKey, (DateTime)ftimeObj, DateTime.Now.AddMinutes(20));
                            //CacheHelper.Cache_Store(lastTimeKey, DateTime.Now, DateTime.Now.AddMinutes(2));
                            CacheHelper.Cache_Store(numKey, num, DateTime.Now.AddHours(2));
                            return string.Empty;
                        }

                        if (num > limitNum)
                        {

                            //20分钟内只给他3次尝试机会
                            num = 3;
                            //CacheHelper.Cache_Store(numKey, 3, DateTime.Now.AddHours(2));
                        }


                    }
                }
                CacheHelper.Cache_Store(numKey, num, DateTime.Now.AddHours(2));

                #endregion

                #region 记录当前手机号的发送状态，在间隔期内不能重复发送 statuObj

                object statuObj = CacheHelper.Cache_Get(sendStatuKey);
                if (statuObj == null)
                {
                    CacheHelper.Cache_Store(sendStatuKey, DateTime.Now, DateTime.Now.AddMinutes(2));
                }
                else
                {
                    //表示当前手机号 正处于2分钟的 获取验证码间隔期，不能重复获取
                    return "already";
                }

                #endregion
            }
            #endregion

            #region 短信验证码 10 分钟内获取的内容一致

            //短信验证码
            object obj = CacheHelper.Cache_Get(key);
            if (obj == null)
            {
                Random r = new Random();
                int ranNum = r.Next(100000, 999999);

                CacheHelper.Cache_Store(key, ranNum, DateTime.Now.AddMinutes(10));

                return ranNum.ToString();
            }
            return obj.ToString();

            #endregion

        }

        public static string GetResultStr(string text, string success = "false")
        {
            string result = string.Format("{{\"success\" : {0},\"errors\": {{ \"text\" : \"{1}\" }}}}", success, text);

            return result;
        }
        public static int GetWeekOfYear(System.DateTime dt)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();
            return gc.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static bool PostFilter(string key, string content)
        {

            object cache = CacheHelper.Cache_Get(key);
            int cacheTimes = 1;
            string cacheContent = "";

            if (cache != null)
            {
                cacheTimes = int.Parse(((object[])cache)[0].ToString());
                cacheContent = ((object[])cache)[1].ToString();
                decimal similarity = TextDiff.Similar(content, cacheContent);
                if (similarity == 1M)
                {
                    return false;
                }
                else if (similarity > Max_Content_Similarity)
                {
                    cacheTimes += 1;
                    if (cacheTimes > Max_Similar_Submit_Times)
                        return false;
                }
                else
                {
                    cacheTimes = 1;
                }
            }

            CacheHelper.Cache_Store(key, new object[] { cacheTimes, content }, TimeSpan.FromHours(5));

            return true;
        }

        public static string BusinessStatus_Get(string od_ext)
        {
            string BusinessStatus = null;
            if (od_ext.Contains("吊销"))
            {
                BusinessStatus = "吊销";
            }
            else if (od_ext.Contains("注销"))
            {
                BusinessStatus = "注销";
            }
            else if (od_ext.Contains("停业"))
            {
                BusinessStatus = "停业";
            }
            else if (od_ext.Contains("清算"))
            {
                BusinessStatus = "清算";
            }
            else if (od_ext.Contains("存续"))
            {
                BusinessStatus = "存续";
            }
            else if (od_ext.Contains("核准成立") || od_ext.Contains("核准设立"))
            {
                BusinessStatus = "核准设立";
            }
            else if (od_ext.Contains("解散"))
            {
                BusinessStatus = "解散";
            }
            else
            {
                BusinessStatus = "正常";
            }
            return BusinessStatus;
        }

        public static Response Normal_Resp_Create(string body, EncryptType en_type = EncryptType.AES) =>       
            new Response(new Response_Head().ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(en_type));//.ToJson();

        public static Response Normal_Resp_Create(string head, string body, EncryptType en_type = EncryptType.AES) =>
            new Response(head.ToEncryption(EncryptType.PT), body.ToEncryption(en_type));//.ToJson();
    }
}
