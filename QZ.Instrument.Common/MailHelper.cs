using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using QZ.Foundation.Utility;

namespace QZ.Instrument.Common
{
    /// <summary>
    /// 发送邮件委托
    /// </summary>
    /// <param name="from"></param>
    /// <param name="fromName"></param>
    /// <param name="to"></param>
    /// <param name="toName"></param>
    /// <param name="cc"></param>
    /// <param name="bcc"></param>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="IsBodyHtml"></param>
    /// <param name="attachs"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    public delegate void SendEmailDelegate(string from, string fromName, string to, string toName, string[] cc, string[] bcc, string title, string body, bool IsBodyHtml, List<Attachment> attachs, out string error);






    /// <summary>
    /// 一些常用的字符串函数
    /// </summary>
    public class MailClass
    {

        public delegate void OnSendComplete(MailInfo info);

        /// <summary>
        /// mail info
        /// </summary>
        public class MailInfo
        {
            /// <summary>
            /// 邮件发送的用户ID
            /// </summary>
            public string userId { get; set; }
            /// <summary>
            /// mail type
            /// </summary>
            public string ml_type { get; set; }
            public string from { get; set; }
            public string fromName { get; set; }
            public string to { get; set; }
            public string toName { get; set; }
            public string[] cc { get; set; }
            public string[] bcc { get; set; }
            public string title { get; set; }
            public string body { get; set; }
            public bool isBodyHtml { get; set; }
            public List<Attachment> attachs { get; set; }

            public string error { get; set; }
            public OnSendComplete sendComplete { get; set; }
        }


        #region 公共属性

        public string SMTP_Host { get; set; }
        public int SMTP_Port { get; set; }
        public bool SMTP_EnableSSL { get; set; }
        public string SMTP_UserName { get; set; }
        public string SMTP_Password { get; set; }
        public Encoding MailEcoding { get; set; }

        #endregion





        /// <summary>
        /// 
        /// </summary>
        public SmtpClient smtp = new SmtpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        public MailClass(string host, int port, bool enableSSL, string userName, string userPwd)
        {
            this.InitSMTP(host, port, enableSSL, userName, userPwd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        public MailClass(string host, string userName, string userPwd)
        {
            this.InitSMTP(host, 25, false, userName, userPwd);
        }

        /// <summary>
        /// 
        /// </summary>
        public MailClass()
        {
            this.InitSMTP("", 25, false, "", "");
        }

        /// <summary>
        /// 初始SMTP服务
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="enableSSL"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        private void InitSMTP(string host, int port, bool enableSSL, string userName, string userPwd)
        {
            this.SMTP_Host = host;
            this.SMTP_UserName = userName;
            this.SMTP_Password = userPwd;
            this.SMTP_EnableSSL = enableSSL;
            this.SMTP_Port = port;
            this.MailEcoding = Encoding.GetEncoding("GB2312");

            smtp.Host = host;
            smtp.Port = port;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = enableSSL;
            if (userName != string.Empty)
            {
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(userName, userPwd);

                ////基本权限
                //mm.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); 
                //mm.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", userName);
                //mm.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", userPwd);
            }

            /*
             * 主机名改成IP地址
            Type type = typeof(SmtpClient);
            SmtpClient client = new SmtpClient();
            FieldInfo fi = type.GetField("localHostName", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
            string strLocalHostName = "";
            strLocalHostName = fi.GetValue(client).ToString();
            strLocalHostName = System.Net.Dns.GetHostAddresses(strLocalHostName)[0].ToString();
            fi.SetValue(client, strLocalHostName);
            */
        }

        /// <summary>
        /// 初始化抄送密送
        /// </summary>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <param name="mm">The mm.</param>
        private void InitCCBCC(string[] cc, string[] bcc, ref MailMessage mm)
        {
            if (cc != null && cc.Length > 0)
            {
                foreach (string s in cc)
                {
                    string[] ss = s.Split(',');
                    if (ss.Length > 1)
                    {
                        mm.CC.Add(new MailAddress(ss[0], ss[1], this.MailEcoding));
                    }
                    else
                    {
                        mm.CC.Add(new MailAddress(ss[0]));
                    }
                }
            }
            if (bcc != null && bcc.Length > 0)
            {
                foreach (string s in bcc)
                {
                    string[] ss = s.Split(',');
                    if (ss.Length > 1)
                    {
                        mm.Bcc.Add(new MailAddress(ss[0], ss[1], this.MailEcoding));
                    }
                    else
                    {
                        mm.Bcc.Add(new MailAddress(ss[0]));
                    }
                }
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="cc">抄送</param>
        /// <param name="bcc">密送</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <param name="IsBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        public void SendMail(string from, string to, string[] cc, string[] bcc, string title, string body, bool IsBodyHtml, List<Attachment> attachs, out string error)
        {
            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(from);

            mm.ReplyTo = new MailAddress(from);
            mm.To.Add(new MailAddress(to));

            this.InitCCBCC(cc, bcc, ref mm);

            mm.Subject = title;
            mm.SubjectEncoding = this.MailEcoding;
            mm.Body = body;
            mm.BodyEncoding = this.MailEcoding;
            mm.IsBodyHtml = IsBodyHtml;

            if (attachs != null && attachs.Count > 0)
            {
                foreach (Attachment item in attachs)
                {
                    mm.Attachments.Add(item);
                }
            }

            SendMail(mm, out error);
        }

        /// <summary>
        /// 发送邮件  HTML邮件营销专用
        /// </summary>
        /// <param name="from"></param>
        /// <param name="displayName"></param>
        /// <param name="to"></param>
        /// <param name="todisplayName"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        public void SendMail(string from, string displayName, string to, string todisplayName, string title, string body, bool isBodyHtml)
        {
            MailMessage mm = new MailMessage();

            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(from, displayName);
            mm.ReplyTo = new MailAddress(from, displayName);
            mm.To.Add(new MailAddress(to, todisplayName));
            this.InitCCBCC(null, null, ref mm);
            mm.Subject = title;
            mm.SubjectEncoding = this.MailEcoding;
            mm.Body = body;
            mm.IsBodyHtml = isBodyHtml;

            smtp.Send(mm);

        }

        /// <summary>
        /// 发送有附件的邮件，邮件营销专用
        /// </summary>
        /// <param name="from"></param>
        /// <param name="displayName"></param>
        /// <param name="to"></param>
        /// <param name="todisplayName"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="attachmentList"></param>
        public void SendMail(string from, string displayName, string to, string todisplayName, string title, string body, bool isBodyHtml, IEnumerable<Attachment> attachmentList)
        {
            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(from, displayName);
            mm.ReplyTo = new MailAddress(from, displayName);
            mm.To.Add(new MailAddress(to, todisplayName));
            this.InitCCBCC(null, null, ref mm);
            mm.Subject = title;
            mm.SubjectEncoding = this.MailEcoding;
            mm.Body = body;
            mm.IsBodyHtml = isBodyHtml;
            foreach (Attachment att in attachmentList)
            {
                mm.Attachments.Add(att);
            }
            smtp.Send(mm);
        }


        /// <summary>
        /// 异步发送邮件
        /// </summary>
        /// <param name="mailInfo"></param>
        public void SendMailAsync(MailInfo mailInfo)
        {
            System.Threading.Thread t = new System.Threading.Thread(InternalSendMailAsync);
            t.IsBackground = true;
            t.Name = "send mail";
            t.Start(mailInfo);
        }

        private void InternalSendMailAsync(object o)
        {

            MailInfo mailInfo = null;

            try
            {

                mailInfo = (MailInfo)o;
                string err;
                SendMail(
                    mailInfo.from
                    , mailInfo.fromName
                    , mailInfo.to
                    , mailInfo.toName
                    , mailInfo.cc
                    , mailInfo.bcc, mailInfo.title, mailInfo.body, mailInfo.isBodyHtml, mailInfo.attachs, out err);

                if (err == null)
                    err = string.Empty;
                mailInfo.error = err;


            }
            catch (Exception ex)
            {
                if (mailInfo != null)
                {
                    mailInfo.error = ex.Message;
                }
            }
            finally
            {
                if (mailInfo != null)
                {
                    if (mailInfo.sendComplete != null)
                    {
                        try
                        {
                            mailInfo.sendComplete(mailInfo);
                        }
                        catch { }
                    }
                }
            }
        }


        /// <summary>
        /// 邮件超链接验证
        /// </summary>
        /// <param name="mailgid">邮件GID</param>
        /// <param name="mail">邮箱地址</param>
        /// <param name="type">连接类型</param>
        /// <param name="targetUrl">目标连接地址</param>
        /// <returns>加密后的验证对象</returns>
        public static string AnchorLinkValidationMD516(string mailgid, string mail, string type, string targetUrl)
        {
            return Cipher_Md5.Md5_16_1(string.Format("FORWARD ORGMAIL VALIDATION {0},{1},{2},{3}", mailgid, mail, type, targetUrl));
        }



        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="fromName">From name.</param>
        /// <param name="to">To.</param>
        /// <param name="toName">To name.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <param name="IsBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        public void SendMail(string from, string fromName, string to, string toName, string[] cc, string[] bcc, string title, string body, bool IsBodyHtml, List<Attachment> attachs, out string error)
        {
            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            if (fromName == string.Empty)
            {
                mm.From = new MailAddress(from);
            }
            else
            {
                mm.From = new MailAddress(from, fromName, this.MailEcoding);
            }
            if (fromName == string.Empty)
            {
                mm.ReplyTo = new MailAddress(from);
            }
            else
            {
                mm.ReplyTo = new MailAddress(from, fromName, this.MailEcoding);
            }
            if (toName == string.Empty)
            {
                mm.To.Add(new MailAddress(to));
            }
            else
            {
                mm.To.Add(new MailAddress(to, toName, this.MailEcoding));
            }

            this.InitCCBCC(cc, bcc, ref mm);

            mm.Subject = title;
            mm.SubjectEncoding = this.MailEcoding;
            mm.Body = body;
            mm.BodyEncoding = this.MailEcoding;
            mm.IsBodyHtml = IsBodyHtml;
            if (attachs != null && attachs.Count > 0)
            {
                foreach (Attachment item in attachs)
                {
                    mm.Attachments.Add(item);
                }
            }

            SendMail(mm, out error);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="fromName">From name.</param>
        /// <param name="to">To.</param>
        /// <param name="toName">To name.</param>
        /// <param name="replyTo">The reply to.</param>
        /// <param name="replyToName">Name of the reply to.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <param name="title">The title.</param>
        /// <param name="body">The body.</param>
        /// <param name="IsBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        public void SendMail(string from, string fromName, string to, string toName, string replyTo, string replyToName, string[] cc, string[] bcc, string title, string body, bool IsBodyHtml, List<Attachment> attachs, out string error)
        {
            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            if (fromName == string.Empty)
            {
                mm.From = new MailAddress(from);
            }
            else
            {
                mm.From = new MailAddress(from, fromName, this.MailEcoding);
            }
            if (replyToName == string.Empty)
            {
                if (replyTo != string.Empty)
                {
                    mm.ReplyTo = new MailAddress(replyTo);
                }
            }
            else
            {
                mm.ReplyTo = new MailAddress(replyTo, replyToName, this.MailEcoding);
            }
            if (toName == string.Empty)
            {
                mm.To.Add(new MailAddress(to));
            }
            else
            {
                mm.To.Add(new MailAddress(to, toName, this.MailEcoding));
            }

            this.InitCCBCC(cc, bcc, ref mm);

            mm.Subject = title;
            mm.SubjectEncoding = this.MailEcoding;
            mm.Body = body;
            mm.BodyEncoding = this.MailEcoding;
            mm.IsBodyHtml = IsBodyHtml;
            if (attachs != null && attachs.Count > 0)
            {
                foreach (Attachment item in attachs)
                {
                    mm.Attachments.Add(item);
                }
            }

            SendMail(mm, out error);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mm"></param>
        /// <param name="error"></param>
        public void SendMail(MailMessage mm, out string error)
        {
            try
            {
                //基本权限
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendemailaddress", mm.From.ToString());
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpaccountname", this.SMTP_UserName);
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", this.SMTP_Host);
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", this.SMTP_UserName);
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", this.SMTP_Password);
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", this.SMTP_EnableSSL.ToString());
                //mm.Headers.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", this.SMTP_Port.ToString());

                smtp.Send(mm);
                error = string.Empty;
            }
            catch (Exception exp)
            {
                error = exp.Message + "<br/>" + exp.Source + "<br/>" + exp.StackTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //private bool SendMail(MailMessage mm)
        //{
        //    mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可

        //    mm.From = new MailAddress("邮箱帐号@163.com", "真有意思", Encoding.GetEncoding(936));
        //    //收件方看到的邮件来源；
        //    //第一个参数是发信人邮件地址
        //    //第二参数是发信人显示的名称
        //    //第三个参数是 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码
        //    //936是简体中文的codepage值注：上面的邮件来源，一定要和你登录邮箱的帐号一致，否则会认证失败

        //    mm.ReplyTo = new MailAddress("test_box@gmail.com", "我的接收邮箱", Encoding.GetEncoding(936));
        //    //ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信
        //    //上面后两个参数的意义， 同 From 的意义mm.CC.Add("a@163.com,b@163.com,c@163.com");
        //    //邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开

        //    //当然也可以用全地址，如下：
        //    mm.CC.Add(new MailAddress("a@163.com", "抄送者A", Encoding.GetEncoding(936)));
        //    mm.CC.Add(new MailAddress("b@163.com", "抄送者B", Encoding.GetEncoding(936)));
        //    mm.CC.Add(new MailAddress("c@163.com", "抄送者C", Encoding.GetEncoding(936)));

        //    mm.Bcc.Add("d@163.com,e@163.com");
        //    //邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开

        //    //当然也可以用全地址，如下：
        //    mm.Bcc.Add(new MailAddress("d@163.com", "密送者D", Encoding.GetEncoding(936)));
        //    mm.Bcc.Add(new MailAddress("e@163.com", "密送者E", Encoding.GetEncoding(936))); mm.Sender = new MailAddress("xxx@xxx.com", "邮件发送者", Encoding.GetEncoding(936));
        //    //可以任意设置，此信息包含在邮件头中，但并不会验证有效性，也不会显示给收件人
        //    //说实话，我不知道有啥实际作用，大家可不理会，也可不写此项mm.To.Add("g@163.com,h@163.com");
        //    //邮件的接收者，支持群发，多个地址之间用 半角逗号 分开

        //    //当然也可以用全地址添加
        //    mm.To.Add(new MailAddress("g@163.com", "接收者g", Encoding.GetEncoding(936)));
        //    mm.To.Add(new MailAddress("h@163.com", "接收者h", Encoding.GetEncoding(936))); 
        //    mm.Subject = "这是邮件标题"; //邮件标题
        //    mm.SubjectEncoding = Encoding.GetEncoding(936);
        //    // 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
        //    // 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用mm.IsBodyHtml = true; //邮件正文是否是HTML格式

        //    mm.BodyEncoding = Encoding.GetEncoding(936);
        //    //邮件正文的编码， 设置不正确， 接收者会收到乱码

        //    mm.Body = "<font color=\"red\">邮件测试，呵呵</font>";
        //    //邮件正文mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );
        //    //添加附件，第二个参数，表示附件的文件类型，可以不用指定
        //    //可以添加多个附件
        //    mm.Attachments.Add(new Attachment(@"d:b.doc"));

        //    smtp.Send(mm); //发送邮件，如果不返回异常， 则大功告成了。

        //    return true;
        //}
    }
}
