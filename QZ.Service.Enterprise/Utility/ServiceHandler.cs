using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web.Hosting;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using Nest;
using QZ.Instrument.Model;
using QZ.Foundation.Model;
using QZ.Instrument.Utility;
using QZ.Foundation.Utility;
using QZ.Instrument.Common;
using QZ.Foundation.Cache;
namespace QZ.Service.Enterprise
{
    public class ServiceHandler
    {
        public static bool ShortMsg_Send(Request_Head head, User_Register u, string v_code)
        {
            try
            {
                var prefix = head.Platform == 0 ? "[企业查询宝iOS版]" : "[企业查询宝android版]";
                string msg = null;
                if (u.op_type == Verify_Code_Type.user_register)
                    msg = $"尊敬的{prefix}注册用户：您的手机注册验证码为{v_code},如不是本人操作请忽略。";
                else
                    msg = $"尊敬的{prefix}用户：您的手机验证码为{v_code},如不是本人操作请忽略。";

                var result = ShortMsg_Proxy.ShortMsg_Send("企业查询宝", "前瞻", u.u_tel, msg);
                return result.Successed;
            }
            catch(Exception e)
            {
                #region debug
                Util.Log_Info(nameof(ShortMsg_Send), Location.Internal, e.Message, "failed to send message to user's phone");
                #endregion
                return false;
            }
        }

        public static string UserName_Validate(string u_name)
        {
            string nick1 = u_name.ToPure();
            if (u_name != nick1)
            {
                return "用户名包含限制的字符“" + nick1 + "”";
            }

            if (Regex.IsMatch(u_name, "前(.*?)瞻"))
            {
                return "用户名不得包含“前瞻”";
            }

            int len = Instrument.Utility.Util.Length_Get(u_name);
            if (len <= 3)
            {
                if (len == u_name.Length)
                    return "用户名不得少于4个字母、数字或字符的组合！";
                else
                    return "用户名不得少于2个汉字或单汉字与单字符的组合！";
            }

            if (Regex.IsMatch(u_name, "[\\~\\!\\@\\#\\$\\%\\^\\&\\*\\(\\)\\+\\|\\=\\\\\\{\\}\\'\"\\;\\`\\:\\<\\>\\/\\?\\[\\]\\.]+"))
            {
                //return "用户名中不得包含“~!@#$%^&*()+|={}[]'`\";:<>.\\/?”这些字符！";
                return "用户名包含非法字符";
            }

            return string.Empty;
        }

        public static int Code_Verify(string u_tel, string v_code)
        {
            object obj = CacheHelper.Cache_Get(u_tel);
            if(obj != null)
            {
                string code = obj.ToString();
                return v_code.Equals(code) ? 1 : 0;
            }

            return -1;      // verify code is expired
        }

        public static UserInfo To_UserInfo(Request_Head head, User_Register u)
        {
            var user = new UserInfo();
            user.u_id = 0;
            user.u_uid = 0;
            user.u_type = (byte)Login_Type.Local;

            user.u_mobile = u.u_tel;

            user.u_status = (int)Users_State.Register;
            user.u_status_email = 0; // 表示未验证
            user.u_status_mobile = 1;
            user.u_status_verify = 0;

            user.u_face = string.Empty;
            user.u_face2 = string.Empty;
            user.u_face3 = head.Platform == Platform.Android ? "android" : "ios"; // 存放标记
            user.u_signature = string.Empty;
            user.u_signatureImg = string.Empty;
            user.u_regTime = DateTime.Now;
            user.u_prevLoginTime = string.Empty;
            user.u_curLoginTime = string.Empty;
            user.u_login_num = 0;
            user.u_login_duration = 0;
            user.u_total_money = 0;
            user.u_total_exp = 0;
            user.u_grade = (int)User_Level.normal;
            user.u_birthday = string.Empty;
            user.u_astro = string.Empty;
            user.u_profession = string.Empty;
            user.u_height = 0;
            user.u_weight = 0;
            user.u_live_country = string.Empty;
            user.u_live_city = string.Empty;
            user.u_home_country = string.Empty;
            user.u_home_city = string.Empty;
            user.u_interest = string.Empty;
            user.u_weibo = string.Empty;
            user.u_total_tiezi = 0;
            user.u_total_huifu = 0;
            user.u_total_shang = 0;
            user.u_total_shangQZ = 0;
            user.u_total_shangQF = 0;
            user.u_total_shangJY = 0;
            user.u_total_pinglun = 0;
            user.u_tableId = 0;
            user.u_today_shangF = 0;
            user.u_today_shangJY = 0;

            user.u_email = string.Empty;
            user.u_name = u.u_name;
            user.u_pwd = Cipher_Md5.Md5_16_1(u.u_pwd);
            user.u_regsex = 2;//默认性别保密
            return user;
        }

        public static int User_PwdReset_Log_Insert(string u_tel)
        {
            Users_PwdFoundLogInfo logInfo = new Users_PwdFoundLogInfo
            {
                pl_createTime = DateTime.Now,
                pl_expireTime =

                    DateTime.Now.AddHours(24),
                pl_remark = string.Format("用户修改密码成功!(IP:{0},时间:{1})", Util.Get_RemoteIp(), DateTime.Now),
                pl_status = 0
            };

            logInfo.pl_uid = Cipher_Md5.Md5_16_1(u_tel + logInfo.pl_expireTime.ToString() + logInfo.pl_status.ToString());
            logInfo.pl_url = "http://user.qianzhan.com/account/resetpwd?email=" + u_tel + "&gid=" + logInfo.pl_uid;
            logInfo.pl_to = u_tel;

            logInfo.pl_execTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return DataAccess.User_PwdReset_Log_Insert(logInfo);
        }

        public static void MailNotice_Send(int u_id, string u_name, string u_email, string temppwd, string template)
        {
            bool isReg = !string.IsNullOrEmpty(temppwd);

            MailServersInfo mailServer = DataAccess.MailServers_SelectRand();

            string ml_title = "企业查询宝用户邮件认证";
            string ml_type = "绑定邮箱认证";

            MailClass mc = new MailClass(mailServer.ms_smtp, mailServer.ms_port, mailServer.ms_ssl, mailServer.ms_loginName, mailServer.ms_loginPwd);

            string bodyContent = string.Empty;
            var guid = Guid.NewGuid();
            string code = guid.ToString().Substring(0, 4);
            CacheHelper.Cache_Store("Email_" + u_id, code, TimeSpan.FromMinutes(10));
            bodyContent = string.Format("{0},您好：请点击如下链接，以完成您帐号的邮箱绑定，如无法点击请复制地址到浏览器中:<div style=\"font-Size:15px\">{1}</div>", u_email, "http://qiye.qianzhan.com/usercenter/SendEmail?code=" + code);

            //记录邮箱日志
            MailClass.MailInfo mailInfo = new MailClass.MailInfo()
            {
                from = mailServer.ms_account,
                fromName = System.Configuration.ConfigurationManager.AppSettings["mailFromName"],
                to = u_email,
                toName = u_name,
                title = ml_title,
                body = bodyContent,
                isBodyHtml = true,
                userId = u_id.ToString(),
                ml_type = ml_type,
                sendComplete = new MailClass.OnSendComplete(OnSendComplete)
            };
            mc.SendMailAsync(mailInfo);

            ////记录邮箱日志
            //var maillog = new Users_MailLogInfo()
            //{
            //    ml_uid = Guid.NewGuid().ToString().Replace("-", "").Trim().Substring(0, 16),
            //    ml_type = ml_type,
            //    ml_to = u_email,
            //    ml_toName = u_name,
            //    ml_cc = string.Empty,
            //    ml_title = ml_title,
            //    ml_content = bodyContent,
            //    ml_resend = 0,
            //    ml_createTime = DateTime.Now,
            //    ml_createUser = u_id.ToString(),
            //    ml_from = mailServer.ms_account,
            //    ml_fromName = System.Configuration.ConfigurationManager.AppSettings["MailFromName"]
            //};
            //mc.SendMailAsync(mailInfo);
        }

        private static void OnSendComplete(MailClass.MailInfo mailInfo)
        {
            var maillog = new Users_MailLogInfo()
            {
                ml_uid = Guid.NewGuid().ToString().Replace("-", "").Trim().Substring(0, 16),
                ml_type = mailInfo.ml_type,
                ml_to = mailInfo.to,
                ml_toName = mailInfo.toName,
                ml_cc = string.Empty,
                ml_title = mailInfo.title,
                ml_content = mailInfo.body,
                ml_resend = 0,
                ml_createTime = DateTime.Now,
                ml_createUser = mailInfo.userId,
                ml_from = mailInfo.from,
                ml_fromName = mailInfo.fromName,
                ml_state = string.IsNullOrEmpty(mailInfo.error) ? 1 : 0,
                ml_resendRemark = mailInfo.error
            };
            DataAccess.Users_MailLogs_Insert(maillog);
        }

        public static string GetDetailPKURL(string n_type, string n_catId, string n_gid)
        {
            string[] ss = n_type.Split('_');

            switch (ss[0])
            {
                case "military":
                    return string.Format("http://mil.qianzhan.com/detail/{0}.html", n_gid);
                case "ent":
                    return string.Format("http://ent.qianzhan.com/detail/{0}.html", n_gid);
                case "health":
                    return string.Format("http://jk.qianzhan.com/detail/{0}.html", n_gid);
                default:
                    return string.Format("http://www.qianzhan.com/survey/detail/{1}/{0}.html", n_gid, n_catId);
            }
        }

        public static string GetDetailURL(string n_type, string n_catId, string n_gid, int page, bool preview)
        {

            //头条前瞻
            if (n_type != null && n_type.Length > 0 && n_type.StartsWith("_t_"))
            {
                return GetTPDetailUrl(n_type, n_catId, n_gid, page, preview);
            }
            else if (n_type.EndsWith("_pk"))
            {
                return GetDetailPKURL(n_type, n_catId, n_gid);
            }

            //StringBuilder sb = new StringBuilder();
            List<string> lst = new List<string>(4);


            // 2013-11-15
            switch (n_type)
            {
                /*最新链接规划*/
                case "military":
                    return string.Format("http://mil.qianzhan.com/detail/{0}.html", n_gid);
                case "ent":
                    return string.Format("http://ent.qianzhan.com/detail/{0}.html", n_gid);
                case "ent_star":
                    return string.Format("http://ent.qianzhan.com/ent/star/{0}.html", n_gid);
                case "ent_original":
                    return string.Format("http://ent.qianzhan.com/ent/original/{0}.html", n_gid);
                case "health":
                    return string.Format("http://jk.qianzhan.com/detail/{0}.html", n_gid);
                /*最新链接结束*/

                case "capital":
                    return string.Format("http://ipo.qianzhan.com/detail/{0}.html", n_gid);
                case "baike":
                    return string.Format("http://baike.qianzhan.com/detail/bk_{0}.html", n_gid);
                case "daohang":
                    return string.Format("http://daohang.qianzhan.com/detail/dh_{0}.html", n_gid);
                case "game":
                    return string.Format("http://yx.qianzhan.com/game/page/{0}.html", n_gid);
                case "bbs_article":
                    return string.Format("http://bbs.qianzhan.com/detail/{0}.html", n_gid);
                case "zhinan":
                    return string.Format("http://zhinan.qianzhan.com/detail/{0}.html", n_gid);

                case "report":
                    lst.Add("http://bg.qianzhan.com");
                    break;
                case "report_bgtj":
                    lst.Add("http://bg.qianzhan.com");
                    break;
                //case "capital":
                //    lst.Add("http://ipo.qianzhan.com");
                //    break;
                case "indynews_qiwen":
                case "qnews":
                    lst.Add("http://qiwen.qianzhan.com");
                    break;
                case "meeting":
                    lst.Add("http://meeting.qianzhan.com");
                    break;

                //case "health":
                //    lst.Add("http://jk.qianzhan.com");
                //    break;

                default:
                    if (n_catId == "exhdetail")
                    {
                        return string.Format("http://meeting.qianzhan.com/detail/exh_{0}.html", n_gid);
                    }
                    else
                    {
                        lst.Add("http://www.qianzhan.com");
                    }
                    break;
            }

            //if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["WebSiteDomain"]))
            //    lst.Add(System.Configuration.ConfigurationManager.AppSettings["WebSiteDomain"]);

            //主要是区别专题 123#its
            string[] cats = n_catId.Split('#');
            n_catId = cats[0];
            if (n_catId == "146")
            {
                #region 特殊URL处理
                bool bFit = true;
                if (n_gid.StartsWith("130219-8f77e287"))//资质证书
                {
                    lst.Clear();
                    lst.Add("http://bg.qianzhan.com/report/guide/certificate.html");
                    if (n_gid.IndexOf("#") != -1)
                    {
                        string[] ss = n_gid.Split('#');
                        lst.Add("#" + ss[1]);
                    }
                }
                else if (n_gid.StartsWith("20120116-c009e37a6b0e7620"))//权威引用
                {
                    lst.Clear();
                    lst.Add("http://bg.qianzhan.com/report/guide/authority.html");
                    if (n_gid.IndexOf("#") != -1)
                    {
                        string[] ss = n_gid.Split('#');
                        lst.Add("#" + ss[1]);
                    }
                }
                else if (n_gid.StartsWith("150915-e3393c8a"))//软件专利
                {
                    lst.Clear();
                    lst.Add("http://bg.qianzhan.com/report/guide/ruanjian.html");
                    if (n_gid.IndexOf("#") != -1)
                    {
                        string[] ss = n_gid.Split('#');
                        lst.Add("#" + ss[1]);
                    }
                }
                else if (n_gid.StartsWith("20111214-93c731d1855829be"))//客户评价
                {
                    lst.Clear();
                    if (n_gid.IndexOf("#") != -1)
                    {
                        lst.Add("http://bg.qianzhan.com/report/pingjia/p");
                        string[] ss = n_gid.Split('#');
                        lst.Add(ss[1] + ".html");
                    }
                    else
                    {
                        lst.Add("http://bg.qianzhan.com/report/guide/pingjia.html");
                    }
                }
                else
                {
                    bFit = false;
                }
                if (bFit)
                {
                    return string.Join("", lst.ToArray());
                }
                #endregion
            }
            if (preview)
            {
                lst.Add("/preview");
            }
            string[] types = n_type.Split('_'); // analyst_news, investment_news
            if (types[0] != string.Empty)
            {
                lst.Add("/");
                lst.Add(types[0]);
            }
            switch (n_type)
            {
                case "analyst":
                case "people":
                    lst.Add("/profile");
                    break;
                case "ent_star":
                    lst.Add("/star");
                    break;
                case "busischool":
                    lst.Add("/content");
                    break;
                case "investment":
                case "investment_ent":
                    lst.Add("/invdetail");
                    break;
                default:
                    if (cats.Length > 1 && cats[1] != string.Empty)
                    {
                        lst.Add("/");
                        lst.Add(cats[1]);
                    }
                    else
                    {
                        if (n_catId != "exhdetail") // 展会详细
                        {
                            lst.Add("/detail");
                        }
                    }
                    break;
            }

            if (n_catId != string.Empty && n_type != "report" && n_type != "monthly" && n_type != "busischool")
            {
                lst.Add("/");
                lst.Add(n_catId);
            }
            lst.Add("/");
            string anchor = string.Empty;
            if (n_gid.IndexOf("#") == -1)
            {
                lst.Add(n_gid);
            }
            else
            {
                string[] ss = n_gid.Split('#');
                anchor = ss[1];
                lst.Add(ss[0]);
            }
            if (page > 1)
            {
                lst.Add("_");
                lst.Add(page.ToString());
            }
            else if (page == -1)
            {
                lst.Add("_{0}");
            }
            lst.Add(".html");
            if (anchor != string.Empty)
            {
                lst.Add("#");
                lst.Add(anchor);
            }

            return string.Join("", lst.ToArray());
        }

        public static string GetTPDetailUrl(string n_type, string n_catId, string n_gid, int page, bool preview)
        {
            List<string> lst = new List<string>(5);
            lst.Add("http://t.qianzhan.com");
            string[] cats = n_catId.Split('#');
            n_catId = cats[0];
            if (preview)
            {
                lst.Add("/preview");
            }

            string[] types = n_type.Split('_'); // _t_tangcuyu
            if (types.Length > 2)
            {
                lst.Add("/");
                lst.Add(types[2]);
                //lst.Add("/");
            }
            else
            {
                lst.Add("/");
                lst.Add(types[0]);
            }

            lst.Add("/detail/");

            //if (cats.Length > 1 && cats[1] != string.Empty)
            //{
            //    lst.Add("/");
            //    lst.Add(cats[1]);
            //}
            //else {
            //    lst.Add("/");
            //    lst.Add(n_catId);
            //}

            string anchor = string.Empty;
            if (n_gid.IndexOf("#") == -1)
            {
                lst.Add(n_gid);
            }
            else
            {
                string[] ss = n_gid.Split('#');
                anchor = ss[1];
                lst.Add(ss[0]);
            }
            if (page > 1)
            {
                lst.Add("_");
                lst.Add(page.ToString());
            }
            else if (page == -1)
            {
                lst.Add("_{0}");
            }
            lst.Add(".html");
            if (anchor != string.Empty)
            {
                lst.Add("#");
                lst.Add(anchor);
            }
            return string.Join("", lst.ToArray());


        }

        public static Resp_Brands Brand_Query_Handle(ISearchResponse<OrgCompanyBrand> response, int pg_size)
        {
            var documents = response.Documents; //

            var hits = response.Hits;
            foreach(var hit in hits)
            {
                var hl = hit.Highlights;
                foreach(var pair in hl)
                {
                    if(pair.Key == "ob_name")
                    {
                        var content = pair.Value.Highlights.FirstOrDefault();
                        var doc = documents.FirstOrDefault(d => d.ob_name == hit.Source.ob_name);
                        if (!string.IsNullOrEmpty(content))
                            doc.ob_name = content;
                    }
                }
            }

            var list = documents/*.Take(pg_size)*/.Select(d => d.To_Brand_Abs()).ToList();

            var dict = new Dictionary<string, long>();
            //dict.Add("0", 0);
            if (response.Aggregations.ContainsKey("term_class"))
            {
                var agg = (BucketAggregate)response.Aggregations["term_class"];

                foreach (var i in agg.Items)
                {
                    var pair = (KeyedBucket)i;
                    dict.Add(pair.Key, pair.DocCount ?? 0);
                }

            }
            return new Resp_Brands() { brand_list = list, class_agg = dict, count =  response.Total };
        }

        public static Resp_Patents Patent_Query_Handle(ISearchResponse<CompanyPatent> response, int pg_size)
        {
            var documents = response.Documents; //

            var hits = response.Hits;
            foreach (var hit in hits)
            {
                var hl = hit.Highlights;
                foreach (var pair in hl)
                {
                    if (pair.Key == "patent_Name")
                    {
                        var content = pair.Value.Highlights.FirstOrDefault();
                        var doc = documents.FirstOrDefault(d => d.Patent_Name == hit.Source.Patent_Name);
                        if (!string.IsNullOrEmpty(content))
                            doc.Patent_Name = content;
                    }
                }
            }

            var list = documents/*.Take(pg_size)*/.Select(d => d.To_Patent_Abs()).ToList();
            var type_dict = new Dictionary<string, long>();
            //type_dict.Add("不限类型", 0);
            var year_dict = new Dictionary<string, long>();
            //var year_list = new List<YearCount>();
            //year_dict.Add("不限年份", 0);
            //long count = 0;
            if (response.Aggregations.ContainsKey("term_type"))
            {
                var agg = (BucketAggregate)response.Aggregations["term_type"];

                foreach (var i in agg.Items)
                {
                    var pair = (KeyedBucket)i;
                    type_dict.Add(pair.Key, pair.DocCount ?? 0);
                }
                //count = type_dict.Sum(di => di.Value);
            }
            if(response.Aggregations.ContainsKey("term_year"))
            {
                var agg = (BucketAggregate)response.Aggregations["term_year"];

                foreach (var i in agg.Items)
                {
                    var pair = (KeyedBucket)i;
                    year_dict.Add(pair.Key, pair.DocCount ?? 0);
                    //year_list.Add(new YearCount(pair.Key, pair.DocCount ?? 0));
                }
                //year_list.Sort(new YearCountComparer());
                //count2 = year_list.Sum(di => di.Value);
            }
            return new Resp_Patents() { patent_list = list, type_agg = type_dict, year_agg = year_dict, count = response.Total };
        }
        
        public static Resp_Patents Patent_Query_Handle(IMultiSearchResponse response)
        {
            var resp_patent = response.GetResponse<CompanyPatent>("patent");
            var resp_application = response.GetResponse<PatentApplicant>("application");
            var resp_designer = response.GetResponse<PatentDesigner>("designer");
            return Resp_Patents.Default;
        }

        public static Resp_Patents Patent_Universal_Query_Handle(ISearchResponse<CompanyPatent> response)
        {
            return Resp_Patents.Default;
        }

        public static Resp_Judges Judge_Query_Handle(ISearchResponse<WenshuIndex> response)
        {
            var documents = response.Documents; //

            var hits = response.Hits;
            foreach (var hit in hits)
            {
                var hl = hit.Highlights;
                foreach (var pair in hl)
                {
                    if (pair.Key == "jd_title")
                    {
                        var content = pair.Value.Highlights.FirstOrDefault();
                        var doc = documents.FirstOrDefault(d => d.jd_title == hit.Source.jd_title);
                        if (!string.IsNullOrEmpty(content))
                            doc.jd_title = content;
                    }
                }
            }

            var list = documents.Select(d => d.To_Judge_Abs()).ToList();
            return new Resp_Judges() { judge_list = list, count = response.Total };
        }

        public static Resp_Company_List To_Company_List(ISearchResponse<OrgCompanyCombine> search)
        {
            var resp = new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>(), count = 3 };
            var docs = search.Documents;
            foreach(var c in docs)
            {
                if (Private_Util.Normal_Filter(c.od_ext))
                {
                    var r = new Resp_Oc_Abs();
                    r.flag = c.od_CreateTime.Year != 1900;
                    r.oc_addr = c.oc_address ?? "--";
                    r.oc_area = c.oc_area;
                    r.oc_code = c.oc_code;
                    r.oc_art_person = c.od_faRen ?? string.Empty;
                    r.oc_issue_time = c.oc_issuetime.ToString("yyyy-MM-dd") ?? "--";
                    r.oc_name_hl = c.oc_name;
                    r.oc_name = c.oc_name;
                    r.oc_reg_capital = c.od_regMoney ?? "--";
                    r.oe_status = c.oc_issuetime < DateTime.Now;
                    r.oc_type = c.oc_companytype ?? "--";
                    r.oc_status = r.flag ? Private_Util.Operation_Status_Get(c.od_ext) : "未知";
                    resp.oc_list.Add(r);
                }
                if(resp.oc_list.Count == 3)
                {
                    break;
                }
            }
            return resp;
        }
        public static Resp_Dishonests Dishonest_Query_Handle(ISearchResponse<ShixinIndex> response, int pg_size)
        {
            var documents = response.Documents; //

            var hits = response.Hits;
            foreach (var hit in hits)
            {
                var hl = hit.Highlights;
                foreach (var pair in hl)
                {
                    //if (pair.Key == "sx_businessEntity")
                    //{
                    //    var content = pair.Value.Highlights.FirstOrDefault();
                    //    var doc = documents.FirstOrDefault(d => d.sx_businessEntity == hit.Source.sx_businessEntity);
                    //    if (!string.IsNullOrEmpty(content))
                    //        doc.sx_businessEntity = content;
                    //}
                    if(pair.Key == "sx_iname")
                    {
                        var content = pair.Value.Highlights.FirstOrDefault();
                        var doc = documents.FirstOrDefault(d => d.sx_iname == hit.Source.sx_iname);
                        if (!string.IsNullOrEmpty(content))
                            doc.sx_iname = content;
                    }
                }
            }

            var list = documents/*.Take(pg_size)*/.Select(d => d.To_Dishonest_Abs()).ToList();
            var dict = new Dictionary<string, long>();
            //long count = 0;
            if (response.Aggregations.ContainsKey("term_area"))
            {
                var agg = (BucketAggregate)response.Aggregations["term_area"];

                foreach (var i in agg.Items)
                {
                    var pair = (KeyedBucket)i;
                    dict.Add(pair.Key, pair.DocCount ?? 0);
                }
                //count = dict.Sum(di => di.Value);
            }

            return new Resp_Dishonests() { dishonest_list = list, area_agg = dict, count = response.Total };
        }

        public static UserInfo New_OpenUser_Create(Req_Open_Login login)
        {
            var user = new UserInfo();
            user.u_name = login.us_nick;
            user.u_pwd = string.Empty;
            user.u_regsex = Byte.Parse(login.us_gender);
            user.u_email = string.Empty;
            user.u_id = 0;
            user.u_uid = 0;
            user.u_type = login.us_type;
            user.u_mobile = string.Empty;
            user.u_status = (int)Users_State.Register;
            user.u_status_email = 0; // 表示未验证
            user.u_status_mobile = 0;
            user.u_status_verify = 0;
            // user.u_face = userSocialInfo.us_headImg;
            user.u_face = string.Empty;
            user.u_face2 = string.Empty;
            user.u_face3 = string.Empty;
            user.u_signature = string.Empty;
            user.u_signatureImg = string.Empty;
            user.u_regTime = DateTime.Now;
            user.u_prevLoginTime = string.Empty;
            user.u_curLoginTime = string.Empty;
            user.u_login_num = 0;
            user.u_login_duration = 0;
            user.u_total_money = 0;
            user.u_total_exp = 0;
            user.u_grade = (int)User_Level.normal;
            user.u_birthday = string.Empty;
            user.u_astro = string.Empty;
            user.u_profession = string.Empty;
            user.u_height = 0;
            user.u_weight = 0;
            user.u_live_country = string.Empty;
            user.u_live_city = string.Empty;
            user.u_home_country = string.Empty;
            user.u_home_city = string.Empty;
            user.u_interest = string.Empty;
            user.u_weibo = string.Empty;
            user.u_total_tiezi = 0;
            user.u_total_huifu = 0;
            user.u_total_shang = 0;
            user.u_total_shangQZ = 0;
            user.u_total_shangQF = 0;
            user.u_total_shangJY = 0;
            user.u_total_pinglun = 0;
            user.u_tableId = 0;
            user.u_today_shangF = 0;
            user.u_today_shangJY = 0;

            return user;
        }

        public static void LoginLog_Insert(Resp_OpenUser_Login login, Req_Open_Login openlog)
        {
            var agent = WebOperationContext.Current.IncomingRequest.UserAgent;
            var ua = UserAgentCache.CreateOrGetCacheItem(agent);
            var log = new Users_LoginLogs_Info();
            log.ul_type = openlog.us_type;
            log.ul_createTime = DateTime.Now;
            log.ul_ip = Util.Get_RemoteIp();
            log.ul_os = ua.GetPlatform();
            log.ul_browser = ua.GetBrowser();
            log.ul_clientId = openlog.us_uid;
            log.ul_userAgent = agent;
            log.ul_u_uid = login.u_id;
            log.ul_u_name = openlog.us_nick; // 登陆名
            log.ul_status = 1;          // 1 -> NORMAL; 0 -> EXCEPTION
            log.ul_error = "正常";
            DataAccess.LoginLog_Insert(log);
        }

        private static DatabaseSearchModel ExtQuery_Hot_SearchModel_Create(string where_f, string column_f, string table, int size, int max_val)
        {
            var l = new HashSet<int>();
            var rand = new Random();
            var sb = new StringBuilder();
            var cur = rand.Next(1000, max_val);

            return new DatabaseSearchModel().SetColumn($" {column_f} ").SetTable($" {table} ").SetWhere($"{where_f} < {cur}").SetOrder($" {where_f} desc ").SetPageSize(size);
        }
        public static string Brand_Query_Hot(int size)
        {
            if (size > 0)
            {
                var s = ExtQuery_Hot_SearchModel_Create("ob_id", "ob_name", "OrgCompanyBrand", size * 2, 10000);
                var list = DataAccess.Brand_Query_Hot(s);
                var lst = new List<string>();
                if(list != null && list.Count > 0)
                {
                    foreach(var l in list)
                    {
                        if (string.IsNullOrEmpty(l))
                            continue;
                        if (lst.Exists(li => li.Equals(l)))
                            continue;

                        lst.Add(l);
                        if (lst.Count == size)
                            break;
                    }
                }
                return (lst.Count != 0 ? lst : new List<string>() { "迪斯尼", "皇冠假日酒店" }).ToJson();
            }
            else
                return (new List<string>() { "迪斯尼", "皇冠假日酒店" }).ToJson();
        }

        public static string Patent_Query_Hot(int size)
        {
            if (size > 0)
            {
                var s = ExtQuery_Hot_SearchModel_Create("ID", "Patent_Name", "OrgCompanyPatent", size * 2, 1000000);
                var list = DataAccess.Patent_Query_Hot(s);
                var lst = new List<string>();
                if (list != null && list.Count > 0)
                {
                    foreach (var l in list)
                    {
                        if (string.IsNullOrEmpty(l))
                            continue;
                        if (lst.Exists(li => li.Equals(l)))
                            continue;

                        lst.Add(l);
                        if (lst.Count == size)
                            break;
                    }
                }
                return (lst.Count != 0 ? lst : new List<string>() { "一种多功能环保锅炉", "热交换器", "一种硅改性氢氧化铝干胶及其制备方法" }).ToJson();
            }
            else
                return (new List<string>() { "一种多功能环保锅炉", "热交换器", "一种硅改性氢氧化铝干胶及其制备方法" }).ToJson();
        }


        public static string Exhibit_Query_Hot(int size)
        {
            if (size < 1 || size > 5) size = 5;

            var rand = new Random(DateTime.Now.Millisecond);
            var model = new DatabaseSearchModel().SetPageSize(500).SetPageIndex(rand.Next(10)).SetColumn(" max(ee_exhName) as e_name ")
                .SetWhere(" ee_namemd <> '' group by ee_namemd ").SetOrder(" ee_namemd ").SetTable("ExhibitionEnterprise");
            var list = DataAccess.Exhibit_Query_Hot(model);

            var e_names = new List<string>(size);
            if (list.Count > 300)
            {
                var firstKey = rand.Next(list.Count);
                var keys = new List<int>(size);
                keys.Add(firstKey);
                for (int i = 0; i < size - 1; i++)
                {
                    var key = rand.Next(list.Count);
                    while (keys.Contains(key))
                    {
                        key = rand.Next(list.Count);
                    }
                    keys.Add(key);
                }
                for(int i = 0; i < size; i++)
                {
                    var s = list[keys[i]];
                    //int index = 0;
                    int end = 0;
                    for(int j = 1; j < s.Length; j++)
                    {
                        //if(s[j] > 127 && s[j - 1] < 127)
                        //{
                        //    if(j < 8)
                        //    {
                        //        index = j;
                        //        if (s[j] == '年' || s[j] == '届')
                        //        {
                        //            index++;
                        //        }
                        //        break;
                        //    }
                        //    break;
                        //}
                        if(s[j] == '会' || s[j] == '览' || s[j] == '展')
                        {
                            if(j+1 < s.Length && s[j+1] == ' ')
                            {
                                end = j;
                                break;
                            }
                        } 
                    }
                    if(end > 0)
                    {
                        e_names.Add(s.Substring(0, end + 1));
                    }
                    else
                    {
                        e_names.Add(s);
                    }
                }
            }
            else if(list.Count >= size)
            {
                for (int i = 0; i < size; i++)
                {
                    var s = list[i];
                    int index = 0;
                    for (int j = 1; j < s.Length; j++)
                    {
                        if (s[j] > 127 && s[j - 1] < 127)
                        {
                            if (j < 8)
                            {
                                index = j;
                                if (s[j] == '年' || s[j] == '届')
                                {
                                    index++;
                                }
                                break;
                            }
                            break;
                        }
                    }
                    if (index > 0)
                    {
                        e_names.Add(s.Substring(index));
                    }
                    else
                    {
                        e_names.Add(s);
                    }
                }
            }
            else
            {
                e_names.Add("中国深圳国际科学生活博览会");
                e_names.Add("中国国际五金展览会");
            }
            return e_names.ToJson();
        }

        public static string Dishonest_Query_Hot(int size)
        {
            if (size > 0)
            {
                var s = ExtQuery_Hot_SearchModel_Create("sx_id", "sx_iname", "Shixin", size, 10000);
                var list = DataAccess.Dishonest_Query_Hot(s);
                return (list ?? new List<string>() { "李静飞", "余荣宁", "曹操" }).ToJson();
            }
            else
                return (new List<string>() { "贾玉", "张飞", "宋江" }).ToJson();
        }

        public static string Trade_Universal_Query_Hot(int size)
        {
            if (size < 1 || size > 5) size = 5;

            var list = new List<string>(size);
            var rand = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < size; i++)
            {
                var idx1 = rand.Next(Datas.Trades.Count);
                try
                {
                    var trade1 = Datas.Trades[idx1];
                    var idx2 = rand.Next(trade1.trades.Count);
                    var trade2 = trade1.trades[idx2];
                    if (trade2.trades != null && trade2.trades.Count > 0)
                    {
                        var idx3 = rand.Next(trade2.trades.Count);
                        var trade3 = trade2.trades[idx3];
                        if (trade3.trades != null && trade3.trades.Count > 0)
                        {
                            var idx4 = rand.Next(trade3.trades.Count);
                            var trade4 = trade2.trades[idx4];
                            list.Add(trade4.name);
                        }
                        else
                        {
                            list.Add(trade3.name);
                        }
                    }
                    else
                    {
                        list.Add(trade2.name);
                    }
                }
                catch(Exception)
                { }
            }
            return list.ToJson();
        }

        public static string Judge_Query_Hot_1(int size)
        {
            if (size > 0)
            {
                var rand = new Random();
                var id = rand.Next(100, 100000);
                var dict = new List<string>();
                List<string> list = null;
                var s = new DatabaseSearchModel().SetColumn("jd_docTitle").SetTable("JudgementDocCombine").SetPageSize(size).SetOrder(" id desc ").SetWhere($"id > {id}");
                list = DataAccess.Judge_Query_Hot(s);

                if (list != null && list.Count > 0)
                {
                    foreach (var l in list)
                    {
                        var index = l.IndexOf("案");
                        if (index > 2)
                        {
                            dict.Add(l.Substring(0, index - 1));
                        }
                        else
                        {
                            var j = l.IndexOf("审");
                            if (j > 2)
                            {
                                dict.Add(l.Substring(0, j - 1));
                            }
                        }
                    }
                    return dict.ToJson();
                }
            }

            return (new List<string>()).ToJson();
        }
        public static string Judge_Query_Hot(int size)
        {
            if(size > 0)
            {
                var rand = new Random();
                var id = rand.Next(100, 100000);
                var s = new DatabaseSearchModel().SetColumn("jd_docTitle").SetTable("JudgementDocCombine").SetPageSize(size*2).SetOrder(" id desc ").SetWhere("oc_code <> '' ").SetWhere($"id > {id}");
                var list = DataAccess.Judge_Query_Hot(s);
                if(list != null)
                {
                    var lst = new List<string>();
                    foreach(var l in list)
                    {
                        var m = Regex.Match(l, @"(\w+)有限");
                        if (m.Success)
                        {
                            var raw_str = m.Groups[0].Value;
                            if (raw_str.Contains("与"))
                                continue;
                            var mo = raw_str.Substring(0, raw_str.Length - 2);
                            if (lst.Exists(t => t.Contains(mo)))
                                continue;
                            lst.Add(mo + " 裁判书");
                        }
                        else
                            lst.Add(l);
                        if (lst.Count == size)
                            break;
                    }
                    return lst.ToJson();
                }
            }
            return (new List<string>()).ToJson();
        }


        public static void SendReportMail(string oc_code, string pdfPath, string toMail, string toName, string uId, OrgCompanyListInfo listInfo, MemoryStream ms)
        {
            // 邮件服务器ms_smtp|ms_port|ms_loginName|ms_loginPwd|ms_ssl
            //key="MailServer" value="mail.qianzhan.com|25|tech@qianzhan.com|tech2018|0"
            string mailServerInfo = ConfigurationManager.AppSettings["MailServer"];
            string[] mailServerArr = mailServerInfo.Split('|');
            if (mailServerArr != null && mailServerArr.Length >= 5)
            {
                // 从配置文件获得邮件服务器信息
                MailServersInfo mailServer = new MailServersInfo()
                {
                    ms_account = mailServerArr[0],
                    ms_smtp = mailServerArr[1],
                    ms_port = int.Parse(mailServerArr[2]),
                    ms_loginName = mailServerArr[3],
                    ms_loginPwd = mailServerArr[4],
                    ms_ssl = (mailServerArr[5] == "1") ? true : false
                };
                MailClass mc = new MailClass(mailServer.ms_smtp, mailServer.ms_port, mailServer.ms_ssl, mailServer.ms_loginName, mailServer.ms_loginPwd);
                string templatePath = "/Templates/mail/ReportPDF.html";
                string bodyContent = string.Empty;
                try
                {
                    using (StreamReader sr = System.IO.File.OpenText(HostingEnvironment.MapPath(templatePath)))
                    {
                        bodyContent = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch { }
                string companyName = listInfo.oc_name;
                string regNum = listInfo.oc_number;
                //string companyState = listInfo.oc_status ? "注销" : "正常";
                string updateDate = listInfo.oc_updatetime.ToString("yyyy-MM-dd");
                bodyContent = bodyContent.Replace("{companyName}", companyName).Replace("{regNum}", regNum).Replace("{updateDate}", updateDate);

                string ml_title = "企业信息报告-" + companyName;



                //string error;
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, pdfPath);
                List<System.Net.Mail.Attachment> attachs = new List<System.Net.Mail.Attachment>();
                // QZ.Common.SendEmailDelegate sed = new QZ.Common.SendEmailDelegate(mc.SendMail);
                attachs.Add(attach);

                MailClass.MailInfo mi = new MailClass.MailInfo()
                {
                    attachs = attachs,
                    from = mailServer.ms_account,
                    fromName = System.Configuration.ConfigurationManager.AppSettings["MailFromName"],
                    to = toMail,
                    toName = toName,
                    title = ml_title,
                    body = bodyContent,
                    isBodyHtml = true,
                    ml_type = "企业信息报告",
                    userId = uId,
                    sendComplete = new MailClass.OnSendComplete(OnReportSendComplete)
                };

                mc.SendMailAsync(mi);

            }
        }

        private static void OnReportSendComplete(MailClass.MailInfo info)
        {
            try
            {
                //close the stream
                if (info.attachs != null && info.attachs.Count > 0)
                {
                    foreach (System.Net.Mail.Attachment att in info.attachs)
                    {
                        if (att.ContentStream != null)
                        {
                            att.ContentStream.Dispose();
                        }
                    }
                }

                string error = string.IsNullOrEmpty(info.error) ? string.Empty : info.error;
                //记录邮箱日志
                var maillog = new Users_MailLogInfo()
                {
                    ml_uid = Guid.NewGuid().ToString().Replace("-", "").Trim().Substring(0, 16),
                    ml_type = info.ml_type,
                    ml_to = info.to,
                    ml_toName = info.toName,
                    ml_cc = string.Empty,
                    ml_title = info.title,
                    ml_content = info.body,
                    ml_resend = 0,
                    ml_createTime = DateTime.Now,
                    ml_createUser = info.userId,
                    ml_from = info.from,
                    ml_fromName = info.fromName,
                    ml_resendRemark = error,
                    ml_state = error.Length > 0 ? 0 : 1

                };


                DataAccess.Users_MailLogs_Insert(maillog);



            }
            catch { }
        }

        public static Resp_Company_List Resp_Oc_Abs_Get(List<OrgCompanyListInfo> list, List<OrgCompanyDtlInfo> dtls)
        {
            var resp = new Resp_Company_List();
            resp.oc_list = new List<Resp_Oc_Abs>();
            foreach(var l in list)
            {
                var dtl = dtls.FirstOrDefault(t => t.od_oc_code == l.oc_code);
                var r = new Resp_Oc_Abs();
                r.flag = dtl != null ? dtl.od_CreateTime.Year != 1900 : false;
                r.oc_addr = l.oc_address;
                r.oc_area = l.oc_area;
                r.oc_code = l.oc_code;
                r.oc_art_person = dtl?.od_faRen ?? string.Empty;
                r.oc_issue_time = l.oc_issuetime.ToString("yyyy-MM-dd");
                r.oc_name = l.oc_name;
                r.oc_name_hl = l.oc_name;
                r.oc_reg_capital = dtl?.od_regMoney ?? string.Empty;
                r.oe_status = l.oc_issuetime < DateTime.Now;
                r.oc_type = l.oc_companytype;
                r.oc_status = Util.BusinessStatus_Get(dtl?.od_ext ?? "登记");

                resp.oc_list.Add(r);
            }
            return resp;
        }
    }
}
