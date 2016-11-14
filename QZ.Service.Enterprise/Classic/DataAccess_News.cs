using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Channels;
using QZ.Foundation.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.DataAccess;
using QZ.Instrument.Utility;
using QZ.Foundation.Model;

namespace QZ.Service.Enterprise
{
    public class DataAccess_News
    {
        /// <summary>
        /// 获得App头部导航条或者获得CMS推送的图片新闻（获得发布过的前topn条数据）
        /// </summary>
        /// <param name="blk_id"></param>
        /// <param name="topn"></param>
        /// <returns></returns>
        public static List<CMSItemsInfo> CMSItems_SelectTopN2(int blk_id, int topn, string andThere = "")
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_Db_Key))
            {
                try
                {
                    // 获得发布过的前topn条数据
                    return access.CMSItems_SelectTopN(blk_id, topn, "n_status = 1" + andThere);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(CMSItems_SelectTopN2), Location.Internal, e.Message, "database error");
                    #endregion
                    return new List<CMSItemsInfo>();
                }
            }
        }

        /// <summary>
        /// 选择信息分类
        /// </summary>
        /// <param name="cat_id">The cat_id.</param>
        /// <returns></returns>
        public static NewsCateInfo NewsCates_Selectbycat_id(int cat_id)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.NewsCates_Selectbycat_id(cat_id);
            }
        }

        public static List<NewsInfo> News_Page_Select(string catId, string page, string pagesize)
        {
            page = (int.Parse(page) < 1) ? "1" : page;
            pagesize = (int.Parse(pagesize) > 30) ? "30" : pagesize;

            // 根据指定的cat_ctrl获取某一新闻分类信息
            NewsCateInfo cate = CacheMarker.NewsCates_FromId_Get(catId);
            if (cate == null)
            {
                return new List<NewsInfo>();
            }
            NewsSearchInfo search = new NewsSearchInfo();
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                try
                {
                    search.n_state = 1;

                    if (cate.cat_inheritAll && !cate.cat_isLast)
                    {
                        search.cat_inheritPath = cate.cat_path;
                    }
                    else
                    {
                        search.n_cat_id = cate.cat_id;
                    }

                    string whereString = search.ToWhereString();

                    //// 图集
                    //if ("photo" == cat_ctrl)
                    //{
                    //    whereString += " and n_type='images'";
                    //}

                    var res = access.News_SelectPaged(GetNewsTable(cate.cat_tableIndex), "*", whereString, search.DefOrder, int.Parse(page), int.Parse(pagesize), out rowcount);
                    return res;
                }
                catch (Exception e)
                {
                    return new List<NewsInfo>();
                }
            }
        }

        /// <summary>
        /// 根据cat_ctrl获得新闻列表
        /// 指定某一个新闻分类，然后获取这个分类下的处于正常状态的新闻列表
        /// </summary>
        /// <returns></returns>
        public static List<NewsInfo> News_SelectPaged(string cat_ctrl, string page, string pagesize)
        {
            page = (int.Parse(page) < 1) ? "1" : page;
            pagesize = (int.Parse(pagesize) > 30) ? "30" : pagesize;

            // 根据指定的cat_ctrl获取某一新闻分类信息
            NewsCateInfo cate = CacheMarker.GetNewsCateByCtrl(cat_ctrl);
            if (cate == null)
            {
                return new List<NewsInfo>();
            }
            NewsSearchInfo search = new NewsSearchInfo();
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                try
                {
                    search.n_state = 1;

                    if (cate.cat_inheritAll && !cate.cat_isLast)
                    {
                        search.cat_inheritPath = cate.cat_path;
                    }
                    else
                    {
                        search.n_cat_id = cate.cat_id;
                    }

                    string whereString = search.ToWhereString();

                    // 图集
                    if ("photo" == cat_ctrl)
                    {
                        whereString += " and n_type='images'";
                    }

                    return access.News_SelectPaged(GetNewsTable(cate.cat_tableIndex), "*", whereString, search.DefOrder, int.Parse(page), int.Parse(pagesize), out rowcount);
                }
                catch(Exception e)
                {
                    #region debug
                    Util.Log_Info(nameof(News_SelectPaged), Location.Internal, e.Message, "database error");
                    #endregion
                    return new List<NewsInfo>();
                }
            }
        }

        public static string GetNewsTable(int tableIndex)
        {
            if (tableIndex > 0)
                return string.Format("News_{0}", tableIndex);

            if (tableIndex == -1)
            {
                return "News_Temp";
            }
            else if (tableIndex == -9)
            {
                return "News_Guide";
            }

            return string.Empty;
        }
        /// <summary>
        /// 获取某一分类下最新新闻
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <returns></returns>
        public static NewsInfo LastNews_Select(string cat_ctrl)
        {
            // 根据指定的cat_ctrl获取某一新闻分类信息
            NewsCateInfo cate = CacheMarker.GetNewsCateByCtrl(cat_ctrl);
            if (cate == null)
            {
                return null;
            }
            NewsSearchInfo search = new NewsSearchInfo();
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                search.n_state = 1;

                if (cate.cat_inheritAll && !cate.cat_isLast)
                {
                    search.cat_inheritPath = cate.cat_path;
                }
                else
                {
                    search.n_cat_id = cate.cat_id;
                }

                string whereString = search.ToWhereString();

                // 图集
                if ("photo" == cat_ctrl)
                {
                    whereString += " and n_type='images'";
                }

                var list = access.News_SelectPaged(GetNewsTable(cate.cat_tableIndex), "*",
                    whereString, search.DefOrder, 1, 1, out rowcount);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
                return null;
            }
        }

        /// <summary>
        /// 根据cat_ctrl,cat_id获得新闻列表
        /// </summary>
        /// <returns></returns>
        public static List<NewsInfo> News_SelectPaged(string cat_ctrl, string cat_id, string page, string pagesize)
        {
            page = (int.Parse(page) < 1) ? "1" : page;
            pagesize = (int.Parse(pagesize) > 30) ? "30" : pagesize;

            NewsCateInfo cate = CacheMarker.GetNewsCatesBycat_id(cat_id);
            NewsSearchInfo search = new NewsSearchInfo();
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                search.n_state = 1;

                if (cate != null && cate.cat_inheritAll && !cate.cat_isLast)
                {
                    search.cat_inheritPath = cate.cat_path;
                }
                else
                {
                    search.n_cat_id = cate.cat_id;
                }

                string whereString = search.ToWhereString();

                // 图集
                if ("photo" == cat_ctrl)
                {
                    whereString += " and n_type='images'";
                }

                return access.News_SelectPaged(GetNewsTable(cate.cat_tableIndex), "*", whereString, search.DefOrder, int.Parse(page), int.Parse(pagesize), out rowcount);
            }
        }

        /// <summary>
        /// 获得某条新闻的详细内容
        /// </summary>
        /// <param name="n_gid"></param>
        /// <returns></returns>
        public static List<NewsContentInfo> GetNewsContent(string n_gid)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                List<NewsContentInfo> list = new List<NewsContentInfo>();
                return access.NewsContent_Selectbync_n_gid(n_gid);
            }
        }
        /// <summary>
        /// 根据文章ID获得文章摘要信息
        /// </summary>
        /// <param name="n_gid"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static NewsInfo News_Selectbyn_gid(string n_gid, string tableName)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.News_Selectbyn_gid(n_gid, tableName);
            }
        }
        /// <summary>
        /// 获得某文章的最新评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="rowcount"></param>
        /// <returns></returns>
        public static List<CommentInfo> Comments_SelectPaged(string sourceId, string page, string limit,
            out int rowcount)
        {
            page = (int.Parse(page) < 1) ? "1" : page;
            limit = (int.Parse(limit) > 30) ? "30" : limit;

            CommentsSearchInfo search = new CommentsSearchInfo();
            search.cmt_sourceId = sourceId.ToSafetyStr();
            search.cmt_status = 1;
            search.page = int.Parse(page);
            search.pagesize = Int32.Parse(limit);

            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_SelectPaged(search.columns, search.ToWhereString(), search.DefOrder, search.page,
                    search.pagesize, true, out rowcount);
            }
        }

        /// <summary>
        /// 加载个人评论
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static List<CommentInfo> Comments_SelectPaged(string userId, string page, string pagesize)
        {
            page = (int.Parse(page) < 1) ? "1" : page;
            pagesize = (int.Parse(pagesize) > 30) ? "30" : pagesize;

            // 设置获取条件
            CommentsSearchInfo search = new CommentsSearchInfo();
            search.cmt_status = 1;
            search.page = int.Parse(page);
            search.pagesize = int.Parse(pagesize);
            search.cmt_createUserID = int.Parse(userId);

            // 根据条件获得评论
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                // 获得评论
                return access.Comments_SelectPaged(search.columns, search.ToWhereString(), search.DefOrder, search.page, search.pagesize, false, out rowcount);
            }
        }
        /// <summary>
        /// 根据文章ID获得该文章的热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="rowcount"></param>
        /// <returns></returns>
        public static List<CommentInfo> RecommendComments_SelectPaged(string sourceId, string page, string limit,
            out int rowcount)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                page = (int.Parse(page) < 1) ? "1" : page;
                limit = (int.Parse(limit) > 30) ? "30" : limit;
                return access.RecommendComments_SelectPaged("*", string.Format("where cmt_sourceId = '{0}'", sourceId),
                    "cmt_id desc", int.Parse(page), int.Parse(limit), out rowcount);
            }
        }

        /// <summary>
        /// 获得评论
        /// </summary>
        /// <param name="cmt_sourceId"></param>
        /// <param name="cmt_uids"></param>
        /// <returns></returns>
        public static List<CommentInfo> Comments_Selectbycmt_uidStrs(string cmt_sourceId, string[] cmt_uids)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_Selectbycmt_uidStrs(cmt_sourceId, cmt_uids.ToArray());
            }
        }
        /// <summary>
        /// 插入评论
        /// </summary>
        /// <param name="cmt"></param>
        /// <param name="cmt_parentUid"></param>
        /// <param name="cmt_sourceUrl"></param>
        /// <returns></returns>
        public static int Comments_Insert(CommentInfo cmt, string cmt_parentUid, string cmt_sourceUrl)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_Insert(cmt, cmt_parentUid, cmt_sourceUrl);
            }
        }

        /// <summary>
        /// 将评论“顶”日志插入数据库
        /// </summary>
        /// <param name="cmtId"></param>
        /// <returns></returns>
        public static int FavorLog_Insert(string cmtId)
        {
            string objType = "CMTAgree";
            string objId = cmtId + "," + DateTime.Now.ToString("yyMMddHH");

            OperationContext context = OperationContext.Current;
            MessageProperties messageProperties = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpointProperty =
            messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpointProperty.Address;
            string clientId = string.Empty;
            string uid = Cipher_Md5.Md5_16_1(string.Join(",", new string[] { DateTime.Today.ToString("yyyy-MM-dd"), objType, objId, clientId }));
            int week = Util.GetWeekOfYear(DateTime.Today);
            string tblName = string.Format("FavorLog_{0}{1}{2}", DateTime.Today.Year, week < 10 ? "0" : "", week);

            using (var access = new DataAccess_QzNews(Constants.SysLog_Db_Key))
            {
                // 将日志插入到数据库
                return access.FavorLog_Insert(uid, objType, objId, string.Empty, string.Empty, clientId, ip, string.Empty, tblName);
            }
        }
        /// <summary>
        /// 评论“顶”操作
        /// </summary>
        /// <param name="cmtId"></param>
        /// <returns></returns>
        public static int Comments_AddAccept(string cmtId)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                // 评论数据加1
                return access.Comments_AddAccept(cmtId, 1);
            }
        }
        /// <summary>
        /// 根据cmt_uid获得评论
        /// </summary>
        /// <param name="cmt_uid"></param>
        /// <returns></returns>
        public static CommentInfo Comments_Selectbycmt_uid(string cmt_uid)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_Selectbycmt_uid(cmt_uid);
            }
        }
        /// <summary>
        /// 根据cmt_uid获得热评
        /// </summary>
        /// <param name="cmt_uid"></param>
        /// <returns></returns>
        public static List<CommentInfo> RecommendComments_Selectbycmt_uid(string cmt_uid)
        {
            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.RecommendComments_SelectPaged("*", string.Format("where cmt_uid = '{0}'", cmt_uid), "", 1, 1, out rowcount);
            }
        }
        /// <summary>
        /// 插入热门评论
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int RecommendComments_Insert(RecommendCommentInfo obj)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.RecommendComments_Insert(obj);
            }
        }
        /// <summary>
        /// 获得某文章的评论数
        /// </summary>
        /// <param name="n_gid"></param>
        /// <returns></returns>
        public static int GetCommentCount(string n_gid)
        {/*
            CommentsSourceUrlInfo comments = null;

            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                comments = access.CommentsSourceUrls_SelectbysourceId(StringExtension.ToSafetyStr(n_gid));
            }

            if (comments != null)
            {
                return comments.cmt_total;
            }
            */
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_CountbysourceId(n_gid);
            }
        }


        /// <summary>
        /// 插入浏览记录
        /// </summary>
        /// <param name="vlog"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int VisitLog_Insert(VisitLogInfo vlog, string tableName)
        {
            using (var logAccess = new DataAccess_QzNews(Constants.SysLog_App_Db_Key))
            {
                return logAccess.VisitLog_Insert(vlog, tableName);
            }
        }

        /// <summary>
        /// 获得新闻分类
        /// </summary>
        /// <returns></returns>
        public static List<NewsCateInfo> NewsCates_SelectPaged(bool isLen = true)
        {
            NewsCateSearchInfo search = new NewsCateSearchInfo();
            if (isLen)
            {
                search.cat_path_len = 2;
            }

            int rowcount = 0;
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.NewsCates_SelectPaged("*", search.ToWhereString(), search.DefOrder, 1, UInt16.MaxValue, out rowcount);
            }
        }

        public static List<CommentInfo> Comments_SelectPaged(string column, string where, string order, int pg_index, int pg_size, bool only4Ids, out int count)
        {
            using (var access = new DataAccess_QzNews(Constants.QZNewSite_News_Db_Key))
            {
                return access.Comments_SelectPaged(column, where, order, pg_index, pg_size, only4Ids, out count);
            }
        }
    }
}