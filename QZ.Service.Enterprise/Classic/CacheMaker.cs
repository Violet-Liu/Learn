using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using QZ.Foundation.Utility;
using QZ.Instrument.Model;
using QZ.Foundation.Document;
using QZ.Instrument.Utility;
using QZ.Foundation.Cache;

namespace QZ.Service.Enterprise
{
    public class CacheMarker
    {
        #region 企业机构
        /// <summary>
        /// 根据pg_uid，blk_name获得对应CMSBlocksInfo对象
        /// </summary>
        /// <param name="pg_uid"></param>
        /// <param name="blk_name"></param>
        /// <returns></returns>
        public static CMSBlocksInfo GetCMSBlocksInfo(string pg_uid, string blk_name)
        {
            Dictionary<string, CMSBlocksInfo> blocks = GetCMSBlocks(pg_uid);
            CMSBlocksInfo block;
            if (blocks.TryGetValue(blk_name, out block))
            {
                return block;
            }
            return null;
        }
        /// <summary>
        /// 缓存 Dictionary<blk_name, CMSBlocksInfo>对象
        /// </summary>
        /// <param name="pg_uid"></param>
        /// <returns></returns>
        public static Dictionary<string, CMSBlocksInfo> GetCMSBlocks(string pg_uid)
        {
            object o = CacheHelper.Cache_Get(Constants.CN_CMSBlocks);
            if ((o != null) && (o is Dictionary<string, CMSBlocksInfo>) && (((Dictionary<string, CMSBlocksInfo>)o).Count > 0))//当缓存过期的时候返回的是一个Count为0的Dictionary<string, CMSBlocksInfo>类型的对象o
            {
                return (Dictionary<string, CMSBlocksInfo>)o;
            }

            Dictionary<string, CMSBlocksInfo> blocks = null;
            List<CMSBlocksInfo> list = DataAccess.CMSBlocks_Selectbyblk_pg_id(pg_uid);
            blocks = new Dictionary<string, CMSBlocksInfo>(list.Count);
            foreach (CMSBlocksInfo item in list)
            {
                string blk_name = item.blk_name;
                if (!blocks.ContainsKey(blk_name))
                    blocks.Add(blk_name, item);
            }

            CacheHelper.Cache_Store(Constants.CN_CMSBlocks, blocks, TimeSpan.FromMinutes(10));

            return blocks;
        }
        /// <summary>
        /// 根据分类缓存获取分类对象
        /// </summary>
        /// <param name="ctrl">ctrl对象</param>
        /// <returns>如果有跟据ctrl匹配到对象，则返回分类对象，否则返回NULL</returns>
        public static NewsCateInfo GetNewsCateByCtrl(string ctrl)
        {
            List<NewsCateInfo> cates = GetCNNewsCates();
            return cates.FirstOrDefault(t => t.cat_ctrl == ctrl);
        }


        /// <summary>
        /// 获取头条下所有分类 Dictionary<cat_ctrl, NewsCateInfo>
        /// </summary>
        /// <returns></returns>
        public static List<NewsCateInfo> GetCNNewsCates()
        {
            object o = CacheHelper.Cache_Get(Constants.CN_NewsCates);
            if ((o != null) && (o is List<NewsCateInfo>) && (((List<NewsCateInfo>)o).Count > 0))
            {
                return (List<NewsCateInfo>)o;
            }
            List<NewsCateInfo> cates = null;
            List<NewsCateInfo> list = DataAccess_News.NewsCates_SelectPaged(false);

            cates = new List<NewsCateInfo>(list.Count);
            foreach (NewsCateInfo item in list)
            {

                if (cates.All(i => i.cat_id != item.cat_id))
                    cates.Add(item);
            }
            CacheHelper.Cache_Store(Constants.CN_NewsCates, cates, TimeSpan.FromMinutes(10));
            return cates;
        }

        /// <summary>
        /// 根据cat_id获取NewsCateInfo
        /// </summary>
        /// <param name="cat_id"></param>
        /// <returns></returns>
        public static NewsCateInfo GetNewsCatesBycat_id(string cat_id)
        {
            var cates = GetNewsCates();
            NewsCateInfo cate;
            if (cates.TryGetValue(cat_id, out cate))
                return cate;
            return null;
        }
        public static NewsCateInfo NewsCates_FromId_Get(string catId)
        {
            string cacheName = "NewsCates_" + catId;

            object o = CacheHelper.Cache_Get(cacheName);
            if (o != null)
            {
                return (NewsCateInfo)o;
            }

            NewsCateInfo cate = DataAccess_News.NewsCates_Selectbycat_id(int.Parse(catId));
            if (cate != null)
            {
                CacheHelper.Cache_Store(cacheName, cate, TimeSpan.FromMinutes(10));
            }

            return cate;
        }

        /// <summary>
        /// 缓存Dictionary<cat_id, NewsCateInfo>对象
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, NewsCateInfo> GetNewsCates()
        {
            object o = CacheHelper.Cache_Get(Constants.CN_NewsCates_All);
            var dict = o as Dictionary<string, NewsCateInfo>;
            if (dict != null && dict.Count > 0)
            {
                return dict;
            }

            Dictionary<string, NewsCateInfo> cates;
            var search = new NewsCateSearchInfo();
            List<NewsCateInfo> list = DataAccess_News.NewsCates_SelectPaged(false);
            cates = new Dictionary<string, NewsCateInfo>(list.Count);
            list.ForEach(t =>
            {
                string cat_id = t.cat_id.ToString();
                if (!cates.ContainsKey(cat_id))
                    cates.Add(cat_id, t);
            });

            CacheHelper.Cache_Store(Constants.CN_NewsCates_All, cates, TimeSpan.FromMinutes(10));
            return cates;
        }

        /// <summary>
        /// 根据文章id获得文章信息
        /// </summary>
        /// <param name="n_gid"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static NewsInfo News_Selectbyn_gid(string n_gid, string tableName)
        {
            object o = CacheHelper.Cache_Get(Constants.CN_News_Info + n_gid + tableName);
            var info = o as NewsInfo;
            if (info != null)
                return info;

            // 从数据库获取，并加入缓存后再返回
            NewsInfo newsInfo = DataAccess_News.News_Selectbyn_gid(n_gid, tableName);
            CacheHelper.Cache_Store(Constants.CN_News_Info + n_gid + tableName, TimeSpan.FromMinutes(10));
            return newsInfo;
        }

        /// <summary>
        /// 根据文章ID获得该文章的热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetRecommendComments(string sourceId, string page, string limit)
        {
            int rowcount = 0;
            List<CommentInfo> cmtLst = DataAccess_News.RecommendComments_SelectPaged(sourceId, page, limit,
                out rowcount);
            List<string> cmtIds = new List<string>();
            foreach (var cmt in cmtLst)
            {
                cmtIds.Add(cmt.cmt_uid);
                if (!string.IsNullOrEmpty(cmt.cmt_parentIds))
                    cmtIds.AddRange(cmt.cmt_parentIds.Split(','));
            }
            cmtIds = cmtIds.Distinct().ToList();

            var jsonSerializer = new JavaScriptSerializer();
            var json = "";

            // 设置没有登录的用户为匿名用户
            List<CommentInfo> comments = DataAccess_News.Comments_Selectbycmt_uidStrs(sourceId, cmtIds.ToArray());
            foreach (CommentInfo comment in comments)
            {
                comment.cmt_createUser = (comment.cmt_createUserID == -1) ? "匿名用户" : comment.cmt_createUser;
                comment.cmt_content = HtmlHelper.DecodeHTMLString(comment.cmt_content, false).Replace("<br/>", "\n");
                comment.cmt_checkRemark = Util.UserFace_Get(comment.cmt_createUserID);//存放用户头像
                comment.cmt_checkTime = Util.Get_Gentle_Time(comment.cmt_createTime);// 存放友好时间
            }
            json = jsonSerializer.Serialize(comments);


            json = "{\"list\":" + json + ",\"ids\":" + jsonSerializer.Serialize(cmtLst) + ",\"page\":" + page + ",\"pageSize\":" + limit + ",\"total\":" + rowcount + ",\"sourceId\":\"" + sourceId + "\"}";

            //CacheHelper.Cache_Store(Constants.CN_RecommendComments + sourceId + page + limit, json, TimeSpan.FromMinutes(10));

            return json;
        }

        /// <summary>
        /// 根据文章ID获得该文章的热门评论（最新接口）
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static string GetRecommendComments2(string sourceId, string page, string limit)
        {
            int rowcount = 0;
            List<CommentInfo> cmtLst = DataAccess_News.RecommendComments_SelectPaged(sourceId, page, limit,
                out rowcount);

            List<string> cmtIds = new List<string>();
            foreach (var cmt in cmtLst)
            {
                cmtIds.Add(cmt.cmt_uid);
                if (!string.IsNullOrEmpty(cmt.cmt_parentIds))
                    cmtIds.AddRange(cmt.cmt_parentIds.Split(','));
            }
            cmtIds = cmtIds.Distinct().ToList();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = "";

            // 设置没有登录的用户为匿名用户
            List<CommentInfo> comments = DataAccess_News.Comments_Selectbycmt_uidStrs(sourceId, cmtIds.ToArray());
            foreach (CommentInfo comment in comments)
            {
                comment.cmt_createUser = (comment.cmt_createUserID == -1) ? "匿名用户" : comment.cmt_createUser;
                comment.cmt_content = HtmlHelper.DecodeHTMLString(comment.cmt_content, false).Replace("<br/>", "\n");
                comment.cmt_checkRemark = Util.UserFace_Get(comment.cmt_createUserID);//存放用户头像
                comment.cmt_checkTime = Util.Get_Gentle_Time(comment.cmt_createTime);// 存放友好时间
            }

            /*
            * 组装成网易盖楼形式
            */
            int idCount = cmtLst.Count; // 楼层数
            List<List<CommentInfo>> newPosts = new List<List<CommentInfo>>();
            for (int index = 0; (comments != null) && index < idCount; index++)
            {
                string cmt_parentIds = cmtLst[index].cmt_parentIds; // 获得所有的父楼层Id
                List<string> idList = new List<string>();
                if (!string.IsNullOrEmpty(cmt_parentIds))
                {
                    idList.AddRange(cmt_parentIds.Split(','));
                }

                idList.Add(cmtLst[index].cmt_uid); // 加上主楼，这样最后一楼为主楼

                Dictionary<string, CommentInfo> cmt = comments.ToDictionary(a => a.cmt_uid);
                List<CommentInfo> result = new List<CommentInfo>();
                CommentInfo comment = null;
                for (int idIndex = 0; idIndex < idList.Count; idIndex++)
                {
                    if (!cmt.TryGetValue(idList[idIndex], out comment))
                    {
                        continue;
                    }

                    result.Add(comment);
                }

                newPosts.Add(result);
            }

            json = "{\"list\":" + jsonSerialiser.Serialize(newPosts) + ",\"page\":" + page + ",\"pageSize\":" + limit + ",\"total\":" + rowcount + ",\"sourceId\":\"" + sourceId + "\"}";

            //CacheHelper.Cache_Store(Constants.CN_RecommendComments_New + sourceId + page + limit, json, TimeSpan.FromMinutes(10));

            return json;
        }
        #endregion

        #region 后台推送

        /// <summary>
        /// 获得CMSItems_n_id
        /// </summary>
        /// <param name="n_id"></param>
        /// <returns></returns>
        public static string CMSItems_n_id(string n_id = "", string PushDebug = "true")
        {
            object o = CacheHelper.Cache_Get(Constants.CMSItems_n_id);
            if (n_id != "")
            {
                CacheHelper.Cache_Store(Constants.CMSItems_n_id, n_id); // 永不过期 
            }

            if (o != null)
            {
                return (string)o;
            }

            return "";
        }

        #endregion

        #region 会员机制
        public static void SetDateVipStatus(VipStatusUserInfo vus)
        {
            if (CacheHelper.Cache_Get(Constants.vipUser_uid_key).IsNotNull())
            {
                var list = (List<VipStatusUserInfo>)CacheHelper.Cache_Get(Constants.vipUser_uid_key);
                if (list.IsNotNull())
                {
                    var info = list.Where(u => u.vip_userId == vus.vip_userId).ToList().FirstOrDefault();
                    if (info != null)
                    {
                        list.Remove(info);
                    }
                    list.Add(vus);
                }
                if (list.IsNotNull() && list.Count > 0)
                {
                    CacheHelper.Cache_Store(Constants.vipUser_uid_key, list, DateTime.Now.AddDays(1));
                }
            }

        }
        #endregion
    }
}