using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using QZ.Instrument.Model;
using QZ.Foundation.Document;
using QZ.Foundation.Utility;
using QZ.Instrument.DataAccess;
using QZ.Instrument.Utility;
using QZ.Foundation.Cache;
using QZ.Foundation.Model;

namespace QZ.Service.Enterprise
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“News”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 News.svc 或 News.svc.cs，然后开始调试。
    public class News : INews
    {
        #region 新闻
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pg_uid">"Corp_Page_News"</param>
        /// <param name="blk_name">"Focus"</param>
        /// <param name="topn">"5"</param>
        /// <returns></returns>
        public Stream CMSItems_SelectPaged(string pg_uid, string blk_name, string topn)
        {

            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            // 设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            string json = "";

            CMSBlocksInfo block = CacheMarker.GetCMSBlocksInfo(pg_uid, blk_name);
            if (block == null)
            {
                return null;
            }
            // 获得发布过的前topn条数据
            List<CMSItemsInfo> cmsItems = new List<CMSItemsInfo>();
            cmsItems = DataAccess_News.CMSItems_SelectTopN2(block.blk_id, int.Parse(topn), ""/*" and n_publish = 1"*/);
            
            var jsonSerialiser = new JavaScriptSerializer();
            json = string.Format("{{\"AppTop\":{0}}}", jsonSerialiser.Serialize(cmsItems));

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 根据cat_ctrl获得新闻列表
        /// 获取图片新闻下方的新闻
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<NewsInfo> News_SelectPaged(string cat_ctrl, string page, string pagesize)
        {

            //var list = DataAccess_News.News_SelectPaged("qiye_show", page, pagesize);
            var list = DataAccess_News.News_Page_Select("538", page, pagesize);
            return list;
        }

        /// <summary>
        /// 获取指定分类下的最新新闻
        /// </summary>
        /// <param name="cat_ctrl">qiye_show</param>
        /// <returns></returns>
        public NewsInfo LastNews_Select(string cat_ctrl)
        {

            var info = DataAccess_News.LastNews_Select("qiye_show");
            return info;
        }

        /// <summary>
        /// 根据cat_ctrl，cat_id获得新闻列表
        /// 获取图片新闻下方的新闻
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <param name="cat_id"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<NewsInfo> News_SelectPagedBycat_id(string cat_ctrl, string cat_id, string page, string pagesize)
        {

            var list = DataAccess_News.News_SelectPaged(cat_ctrl, cat_id, page, pagesize);

            return list;
        }

        /// <summary>
        /// 获得某条新闻的详细内容
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        public Stream NewsContent_Selectbync_n_gid(NewsContentInfo_RD rData)
        {
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.ContentType = "application/json;charset=utf-8";
            string json = "";
            // 获取某条新闻的详细内容
            List<NewsContentInfo> list = DataAccess_News.GetNewsContent(rData.n_gid);
            if (list.Count == 0)
            {
                json = string.Format("{{\"contents\":{0},\"shareUrl\":\"{1}\",\"imgs\":{2}}}", string.Empty,
                    string.Empty, string.Empty);
            }
            else
            {
                string shareUrl = "";   // 分享文章的地址
                NewsCateInfo cateInfo = null;
                string n_gid = "";
                // 获取文章分类的相关字段信息，并拼接得到文章分享地址
                if (list != null && list.Count > 0)
                {
                    // 分类ID
                    string cat_id = rData.vl_cateId.ToSafety();
                    // 根据分类ID获取分类信息
                    cateInfo = CacheMarker.GetNewsCatesBycat_id(cat_id);

                    string cat_ctrl = (cateInfo != null) ? cateInfo.cat_ctrl : "";
                    n_gid = list[0].nc_n_gid;
                    shareUrl = "http://qiye.qianzhan.com/show/detail/" + rData.n_gid + ".html";
                    //ServiceHandler.GetDetailURL(cat_ctrl, cat_id, n_gid, 1, false);
                }

                // 存放图片数据
                List<ImgsInfo> imgs = new List<ImgsInfo>();
                // 自定义文章内容
                list = CustomContent(list, n_gid, "News_" + cateInfo.cat_tableIndex, out imgs, rData.vl_screenSize);

                // 插入浏览记录
                int logIndex = VisitLog_Insert(rData, shareUrl);
                if (logIndex == -1)
                {
                    return null;
                }

                var jsonSerialiser = new JavaScriptSerializer();
                json = string.Format("{{\"contents\":{0},\"shareUrl\":\"{1}\",\"imgs\":{2}}}", jsonSerialiser.Serialize(list), shareUrl, jsonSerialiser.Serialize(imgs));
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="n_gid"></param>
        /// <param name="tableName"></param>
        /// <param name="imgs"></param>
        /// <param name="screen_resolution"></param>
        /// <returns></returns>
        private List<NewsContentInfo> CustomContent(List<NewsContentInfo> list, string n_gid, string tableName,
            out List<ImgsInfo> imgs, string screen_resolution = "")
        {
            // 样式
            string tableStyle = "border-collapse:collapse;text-align:left;";
            string pStyle = "font-size:18px;font-family:'Helvetica Neue','Helvetica','Heiti SC Regular',stheititc,sans-serif;font-color:#000;line-height:1.6;margin-top:19px;padding:0 6.5px"; //text-align:justify;
            if ((screen_resolution == "750x1334") || (screen_resolution == "1080x1920")) // 6、6s
            {
                pStyle = "font-size:20px;font-family:'Helvetica Neue','Helvetica','Heiti SC Regular',stheititc,sans-serif;font-color:#000;line-height:1.6;margin-top:25px;padding:0 7.5px"; //text-align:justify;
            }
            string titleStyle = "font-size:25px;font-weight:bold;line-height:1.25;font-family:'Helvetica Neue','Helvetica','Heiti SC',stheititc,sans-serif;color:#333;padding:0;margin:0;";
            string authorStyle = "font-size:12px;font-weight:400;font-family:'Helvetica Neue','Helvetica','Heiti SC',stheititc,sans-serif;color:#bbb;padding:0;margin:0;line-height:1.25;";

            // 获取文章题名，作者与时间
            string html = "";
            NewsInfo newsInfo = CacheMarker.News_Selectbyn_gid(n_gid, tableName); //DataAccess_News.News_Selectbyn_gid(n_gid, tableName);
            if (newsInfo != null) // 图集不需要将文章标题加入内容中
            {
                if (newsInfo.n_type != "images")
                {
                    string title = newsInfo.n_title;
                    string author = string.IsNullOrEmpty(newsInfo.n_authors.Trim()) ? newsInfo.n_source : newsInfo.n_authors;
                    string date = newsInfo.n_date;
                    html = string.Format("<p style=\"{0}\">{1}</p><p style=\"{2}\">{3} {4}</p>", titleStyle, title, authorStyle, date, author);
                }
                else
                {
                    list.RemoveAll(t => t.nc_order == -1);
                }
            }

            // 存放图片数据
            imgs = new List<ImgsInfo>();
            int first = 0;
            if (list != null)
            {
                foreach (var info in list)
                {
                    // 对新闻内容特殊字符做处理
                    info.nc_content = HtmlHelper.DecodeHTMLString(info.nc_content, false);

                    using (HtmlParser hp = new HtmlParser(string.Format("<q>{0}</q>", info.nc_content)))
                    {
                        hp.Parse();
                        // 链接集合
                        List<HtmlTag> anchorList = hp.HtmlElemnt.GetElementsByTagName("a");
                        foreach (var anchor in anchorList)
                        {
                            anchor.RemoveTagButKeepChildrenAndValue();  // 去掉新闻内容中的a标签
                        }
                        // 图片集合
                        List<HtmlTag> imgList = hp.HtmlElemnt.GetElementsByTagName("img");
                        foreach (var img in imgList)
                        {
                            img.SetAttribute("style", "width:100%;height:auto"); // 设置新闻内容的图片样式
                            img.RemoveAttribute("width"); // 去除原来的width属性
                            img.RemoveAttribute("height"); // 去除原来的height属性

                            string src = img.GetAttribute("src");
                            string alt = img.GetAttribute("alt");
                            ImgsInfo _info = new ImgsInfo();
                            _info.src = src;
                            _info.alt = alt;
                            imgs.Add(_info);
                        }
                        List<HtmlTag> pTags = hp.HtmlElemnt.GetElementsByTagName("p");
                        foreach (var p in pTags)
                        {
                            p.SetAttribute("style", pStyle); // 设置文章段落
                            foreach (var strong in p.GetElementsByTagName("strong"))
                            {
                                strong.SetAttribute("style", "color:#333");
                            }
                        }
                        // 设置表格样式
                        List<HtmlTag> tTags = hp.HtmlElemnt.GetElementsByTagName("table");
                        foreach (var table in tTags)
                        {
                            // 删除文章中的相关推荐
                            if (table.GetAttribute("style") == "float:right;margin-left:5px;border: 1px solid #C4D6EC;")
                            {
                                table.RemoveTag();
                                continue;
                            }

                            table.SetAttribute("style", tableStyle);
                        }

                        if (++first == 1)// 有些文章分成几篇，会出现多个标题情况
                        {
                            if (newsInfo != null && newsInfo.n_type != "images")
                            {
                                info.nc_content = html + hp.HtmlElemnt.InnerHTMLReBuild + "<br />"; // 重新组装新闻内容
                            }
                            else
                            {
                                info.nc_content = html + hp.HtmlElemnt.InnerHTMLReBuild;
                            }
                        }
                        else
                        {
                            if (newsInfo != null && newsInfo.n_type != "images")
                            {
                                info.nc_content = hp.HtmlElemnt.InnerHTMLReBuild + "<br />"; // 重新组装新闻内容
                            }
                            else
                            {
                                info.nc_content = hp.HtmlElemnt.InnerHTMLReBuild;
                            }

                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 插入访问日志或分享日志
        /// </summary>
        /// <param name="rData"></param>
        /// <param name="shareUrl"></param>
        /// <param name="preTableName"></param>
        /// <returns></returns>
        private int VisitLog_Insert(NewsContentInfo_RD rData, string shareUrl, string preTableName = "VisitLog_")
        {
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            VisitLogInfo vlog = new VisitLogInfo();
            IncomingWebRequestContext inContext = context.IncomingRequest;
            UserAgent ua = UserAgentCache.CreateOrGetCacheItem(inContext.UserAgent);
            vlog.vl_browser = ua.GetBrowser();
            vlog.vl_cookieId = string.Empty;
            vlog.vl_createTime = DateTime.Now;
            vlog.vl_gid = rData.n_gid;
            vlog.vl_id = 0;
            vlog.vl_ip = Util.Get_RemoteIp();
            vlog.vl_remoteAddr = vlog.vl_ip;
            vlog.vl_osName = ua.GetPlatform();      // todo 操作系统的获取可能需要重新实现
            //UserAgent.KeyItem ki = ua.Spider();
            vlog.vl_spiderName = rData.vl_spiderName != null ? ShareFlatform(rData.vl_spiderName) : string.Empty;
            vlog.vl_referrer = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString + "/" + rData.n_gid;
            vlog.vl_url = shareUrl;// 拼接成PC端地址
            vlog.vl_cateId = rData.vl_cateId;
            //vlog.vl_screenSize = string.Empty;
            vlog.vl_totalTime = 0;
            vlog.vl_type = rData.vl_type;
            string userName = rData.vl_userId;
            vlog.vl_userName = (userName == "|") ? "" : userName;
            vlog.vl_screenSize = rData.vl_screenSize;
            vlog.vl_host = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.Host; // 主域名
            string tblName = preTableName + vlog.vl_createTime.ToString("yyyyMMdd"); // 插入表的表名
            // 插入浏览记录
            int logIndex = DataAccess_News.VisitLog_Insert(vlog, tblName);

            return logIndex;
        }

        /// <summary>
        /// 文章分享平台
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ShareFlatform(string input)
        {
            string share_flatform = "其他平台";
            switch (input)
            {
                case "sina":
                case "SINA":
                    {
                        share_flatform = "新浪微博";
                        break;
                    }
                case "qq":
                case "QQ":
                    {
                        share_flatform = "QQ好友";
                        break;
                    }
                case "qzone":
                case "QZONE":
                    {
                        share_flatform = "QQ空间";
                        break;
                    }
                case "wxtimeline":
                case "WEIXIN_CIRCLE":
                    {
                        share_flatform = "微信朋友圈";
                        break;
                    }
                case "wxsession":
                case "WEIXIN":
                    {
                        share_flatform = "微信好友";
                        break;
                    }
                default:
                    break;
            }
            return share_flatform;
        }
        #endregion

        #region 评论
        /// <summary>
        /// 获得某文章的所有评论（最新评论在前面）
        /// </summary>
        /// <param name="sourceId">w文章ID</param>
        /// <param name="page">第几页评论</param>
        /// <param name="limit">每页最大评论数</param>
        /// <returns></returns>
        public Stream Comments_SelectPaged(string sourceId, string page, string limit)
        {
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            int rowcount = 0;
            List<CommentInfo> comments = DataAccess_News.Comments_SelectPaged(sourceId, page, limit, out rowcount);

            // 获取评论cmt_uid集合
            List<string> cmt_Uids = new List<string>();
            comments.ForEach(m =>
            {
                cmt_Uids.Add(m.cmt_uid);
                if (!string.IsNullOrEmpty(m.cmt_parentIds))
                    cmt_Uids.AddRange(m.cmt_parentIds.Split(','));
            });
            cmt_Uids = cmt_Uids.Distinct().ToList();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = "";
            List<CommentInfo> commentList = DataAccess_News.Comments_Selectbycmt_uidStrs(sourceId, cmt_Uids.ToArray());
            foreach (var comment in commentList)
            {
                comment.cmt_createUser = (comment.cmt_createUserID == -1) ? "匿名用户" : comment.cmt_createUser;
                comment.cmt_content = HtmlHelper.DecodeHTMLString(comment.cmt_content, false).Replace("<br/>", "\n");   // 解析编码后的危险字符
                comment.cmt_checkRemark = Util.UserFace_Get(comment.cmt_createUserID);   // 存放用户头像
                comment.cmt_checkTime = Util.Get_Gentle_Time(comment.cmt_createTime);    // 存放友好时间
            }
            json = jsonSerialiser.Serialize(commentList);
            json = "{\"list\":" + json + ",\"ids\":" + jsonSerialiser.Serialize(comments) + ",\"page\":" + page + ",\"pageSize\":" + limit + ",\"total\":" + rowcount + ",\"sourceId\":\"" + sourceId + "\"}";

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 获得某文章的所有评论（最新接口）
        /// </summary>
        /// <param name="sourceId">文章Id</param>
        /// <param name="page">评论所在的页码</param>
        /// <param name="limit">页最大评论数</param>
        /// <returns></returns>
        public Stream Comments_SelectPaged2(string sourceId, string page, string limit)
        {
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            // 设置评论搜索条件
            CommentsSearchInfo search = new CommentsSearchInfo();
            search.cmt_sourceId = sourceId.ToSafetyStr();
            search.cmt_status = 1;
            search.page = int.Parse(page);
            search.pagesize = int.Parse(limit);

            // 获取评论
            int rowcount = 0;
            List<CommentInfo> comments = DataAccess_News.Comments_SelectPaged(search.columns, search.ToWhereString(), search.DefOrder,
                    search.page, search.pagesize, true, out rowcount);


            // 评论uid集合
            List<string> cmt_uids = new List<string>();
            foreach (var cmt in comments)
            {
                cmt_uids.Add(cmt.cmt_uid);
                if (!string.IsNullOrEmpty(cmt.cmt_parentIds))
                    cmt_uids.AddRange(cmt.cmt_parentIds.Split(','));
            }
            cmt_uids = cmt_uids.Distinct().ToList();

            var jsonSerializer = new JavaScriptSerializer();
            var json = "";

            // 获取所有评论uid的评论
            List<CommentInfo> commentList = null;
            commentList = DataAccess_News.Comments_Selectbycmt_uidStrs(sourceId, cmt_uids.ToArray());
            foreach (CommentInfo comment in commentList)
            {
                comment.cmt_createUser = (comment.cmt_createUserID == -1) ? "匿名用户" : comment.cmt_createUser;
                comment.cmt_content = HtmlHelper.DecodeHTMLString(comment.cmt_content, false).Replace("<br/>", "\n"); // 解析编码后的危险字符
                comment.cmt_checkRemark = Util.UserFace_Get(comment.cmt_createUserID);//存放用户头像
            }
            json = jsonSerializer.Serialize(comments);
            /*
             * 组装成网易盖楼形式
             */
            int idCount = comments.Count;   // 楼层数
            List<List<CommentInfo>> newPosts = new List<List<CommentInfo>>();
            for (int index = 0; index < idCount; index++)
            {
                string cmt_parentIds = comments[index].cmt_parentIds;   // 获取所有父楼层Id
                List<string> idList = new List<string>();   // 所有楼层id列表
                if (!string.IsNullOrEmpty(cmt_parentIds))
                    idList.AddRange(cmt_parentIds.Split(','));
                // 加上主楼
                idList.Add(comments[index].cmt_uid);

                Dictionary<string, CommentInfo> cmt = commentList.ToDictionary(a => a.cmt_uid);
                List<CommentInfo> result = new List<CommentInfo>();
                CommentInfo info = null;
                for (int idIndex = 0; idIndex < idList.Count; idIndex++)
                {
                    if (!cmt.TryGetValue(idList[idIndex], out info))
                        continue;
                    result.Add(info);
                }
                newPosts.Add(result);
            }
            json = "{\"list\":" + jsonSerializer.Serialize(newPosts) + ",\"page\":" + page + ",\"pageSize\":" + limit + ",\"total\":" + rowcount + ",\"sourceId\":\"" + sourceId + "\"}";

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }



        /// <summary>
        /// 获取热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Stream RecommendComments_SelectPaged(string sourceId, string page, string limit)
        {
            WebOperationContext context = WebOperationContext.Current;
            context.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            string json = CacheMarker.GetRecommendComments(sourceId.ToSafety(), page, limit);

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
        /// <summary>
        /// 获取热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Stream RecommendComments_SelectPaged2(string sourceId, string page, string limit)
        {
            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            //设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            string json = CacheMarker.GetRecommendComments2(sourceId.ToSafety(), page, limit);
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        public Stream CommentAdd(CommentsData rData)
        {
            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            //设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";

            string sourceId = rData.srcid ?? string.Empty;
            string content = rData.content ?? string.Empty;
            int us_id = int.Parse(rData.us_id);
            string json = "";
            if (us_id <= 0)
            {
                json = Util.GetResultStr("请登录后再评论！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }
            string cacheName = "UserBlack_" + us_id.ToString();
            object o = CacheHelper.Cache_Get(cacheName);

            if (o != null)
            {
                try
                {
                    int bl_status = (int)o;
                    if ((bl_status & (int)SiteBlackStatus.评论) > 0)
                        json = Util.GetResultStr("评论失败：禁止评论！");
                    return new MemoryStream(Encoding.UTF8.GetBytes(json));
                }
                catch
                {
                }

            }

            if (sourceId == string.Empty)
            {
                json = Util.GetResultStr("评论失败：参数错误！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            if (content == string.Empty)
            {
                json = Util.GetResultStr("评论失败：请输入评论内容！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            if (content.Length > 800)
            {
                json = Util.GetResultStr("评论失败：评论内容超过800个字！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            // 过滤危险字符，以防脚本攻击
            content = content.ToPure().ToText();
            MatchCollection matches = Regex.Matches(content, "http://");
            if (matches != null && matches.Count > 2)
            {
                json = Util.GetResultStr("评论失败：非法的评论内容！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            int relt = InsertComments(rData);
            if (relt > 0)
            {
                json = Util.GetResultStr("评论成功！", "true");


                try
                {
                    UserInfo user = DataAccess.User_FromId_Select_2(us_id);
                    if (user != null)
                    {
                        user.u_total_exp += 10; // 奖励10个积分

                        DataAccess.User_Update(user); // 更新用户信息
                    }


                }
                catch
                {
                }


                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            json = Util.GetResultStr("评论失败：请稍许再提交！");
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 插入评论
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        private int InsertComments(CommentsData rData)
        {
            string puid = rData.puid ?? string.Empty;
            string sourceId = rData.srcid ?? string.Empty;
            string content = rData.content ?? string.Empty;
            string sourceType = rData.type ?? string.Empty;
            string url = rData.url ?? string.Empty;
            int us_id = int.Parse(rData.us_id);
            string us_name = rData.us_name;

            CommentInfo cmt = new CommentInfo();
            cmt.cmt_accept = 0;
            cmt.cmt_checkRemark = string.Empty;
            cmt.cmt_checkTime = string.Empty;
            cmt.cmt_checkUser = string.Empty;
            cmt.cmt_content = content;
            cmt.cmt_createTime = DateTime.Now;
            cmt.cmt_createUser = us_name;
            cmt.cmt_createUserID = us_id;
            cmt.cmt_id = 0;

            OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
              messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            cmt.cmt_ip = endpointProperty.Address;

            cmt.cmt_ipArea = string.Empty;
            cmt.cmt_parentIds = string.Empty;
            cmt.cmt_sourceId = sourceId;
            cmt.cmt_sourceType = sourceType;
            cmt.cmt_status = 1;
            cmt.cmt_title = string.Empty;
            cmt.cmt_uid = GetCmtUID(sourceId);

            cmt.cmt_sourceCateId = rData.cmt_sourceCateId;
            cmt.cmt_platform = rData.cmt_platform;
            cmt.cmt_device = rData.cmt_device;
            cmt.cmt_title = rData.cmt_title;

            return DataAccess_News.Comments_Insert(cmt, puid, url);
        }

        private string GetCmtUID(string sourceId)
        {
            if (sourceId.Length > 20)
                return sourceId.Substring(2, 4);
            return sourceId.Substring(0, 4);
        }

        /// <summary>
        /// 评论顶操作
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        public Stream DoAgree(RequestData rData)
        {
            // 解析得到评论ID
            var data = rData.details.Split('|');
            string cmt_uid = data[0].ToSafety();

            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            //设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            string json = "";

            if (cmt_uid == string.Empty)
            {
                json = Util.GetResultStr("“顶”失败：参数错误！");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }
            //  
            int result = DataAccess_News.FavorLog_Insert(cmt_uid);
            if (result > 0)
            {
                int relt = DataAccess_News.Comments_AddAccept(cmt_uid);       // 评论记录中评论“支持”字段值增1
                if (relt > 0)
                {
                    json = " { \"success\" : true, \"errors\" :  { \"text\" : \"“顶”成功！\", \"num\" : 1 } }";

                    CommentInfo comment = DataAccess_News.Comments_Selectbycmt_uid(cmt_uid);
                    if ((comment != null) && (comment.cmt_accept > 10)) // 顶超过10设为热评
                    {
                        // 判断是否已设为热评
                        List<CommentInfo> comments = DataAccess_News.RecommendComments_Selectbycmt_uid(cmt_uid);
                        if ((comments == null) || (comments.Count < 1))
                        {
                            // 设为热评
                            RecommendCommentInfo rcomment = new RecommendCommentInfo();
                            rcomment.cmt_accept = comment.cmt_accept;
                            rcomment.cmt_checkRemark = comment.cmt_checkRemark;
                            rcomment.cmt_checkTime = comment.cmt_checkTime;
                            rcomment.cmt_checkUser = comment.cmt_checkUser;
                            rcomment.cmt_content = comment.cmt_content;
                            rcomment.cmt_createTime = comment.cmt_createTime;
                            rcomment.cmt_createUser = comment.cmt_createUser;
                            rcomment.cmt_createUserID = comment.cmt_createUserID;
                            rcomment.cmt_id = comment.cmt_id;
                            rcomment.cmt_ip = comment.cmt_ip;
                            rcomment.cmt_ipArea = comment.cmt_ipArea;
                            rcomment.cmt_parentIds = comment.cmt_parentIds;
                            rcomment.cmt_sourceId = comment.cmt_sourceId;
                            rcomment.cmt_sourceType = comment.cmt_sourceType;
                            rcomment.cmt_status = comment.cmt_status;
                            rcomment.cmt_title = comment.cmt_title;
                            rcomment.cmt_uid = comment.cmt_uid;
                            rcomment.rcmt_createUser = comment.cmt_createUser;
                            rcomment.rcmt_createTime = DateTime.Now;
                            DataAccess_News.RecommendComments_Insert(rcomment);

                        }
                    }

                    return new MemoryStream(Encoding.UTF8.GetBytes(json));
                }

                json = Util.GetResultStr("“顶”失败，请稍候再试");
                return new MemoryStream(Encoding.UTF8.GetBytes(json));
            }

            json = " { \"success\" : true, \"errors\" :  { \"text\" : \"“顶”已成功！\", \"num\" : 0 } }";
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 获得评论总条数
        /// </summary>
        /// <param name="n_gid"></param>
        /// <returns></returns>
        public Stream GetCommentCount(string n_gid)
        {
            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            //设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            string json = "";

            int commentCount = DataAccess_News.GetCommentCount(n_gid);

            json = "{\"commentCount\":\"" + commentCount + "\"}";
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 加载个人评论（个人中心:我的跟帖）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public Stream LoadUserContent(string userId, string page, string pagesize)
        {
            // 得到上下文
            WebOperationContext woc = WebOperationContext.Current;
            // 设置响应格式，消除了返回为string的有反斜杠情况
            woc.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            string json = "";

            // 根据条件获得评论
            List<CommentInfo> cmtLst = DataAccess_News.Comments_SelectPaged(userId, page, pagesize);

            // 在评论里加上作者、发表时间、以及文章标题
            List<CommentInfoApp> cmtLst2 = new List<CommentInfoApp>();
            if (cmtLst != null)
            {
                for (int i = 0; i < cmtLst.Count; i++)
                {
                    var item = cmtLst[i];
                    var infoApp = new CommentInfoApp();
                    infoApp.cmt_content = item.cmt_content;
                    infoApp.cmt_id = item.cmt_id;
                    infoApp.cmt_sourceId = item.cmt_sourceId;
                    infoApp.cmt_sourceType = item.cmt_sourceType;
                    infoApp.cmt_uid = item.cmt_uid;
                    infoApp.cmt_sourceCateId = item.cmt_sourceCateId;

                    int catId = 0;
                    if (!int.TryParse(item.cmt_sourceCateId, out catId))
                        continue;

                    var cate = CacheMarker.GetNewsCatesBycat_id(catId.ToString());
                    if (cate == null)
                        continue;
                    NewsInfo news = CacheMarker.News_Selectbyn_gid(item.cmt_sourceId, "News_" + cate.cat_tableIndex.ToString());
                    if (news != null)
                    {
                        infoApp.n_authors = news.n_authors;
                        infoApp.n_date = news.n_date;
                        infoApp.n_title = news.n_title;
                    }

                    cmtLst2.Add(infoApp);
                }
            }
            var jsonSerialiser = new JavaScriptSerializer();
            json = "{\"list\":" + jsonSerialiser.Serialize(cmtLst2) + "}";
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        #endregion
    }
}
