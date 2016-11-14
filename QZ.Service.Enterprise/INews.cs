using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.ServiceModel.Web;
using QZ.Instrument.Model;

namespace QZ.Service.Enterprise
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“INews”。
    [ServiceContract]
    public interface INews
    {
        /// <summary>
        /// 获得App头部导航条或者获得CMS推送的图片新闻
        /// </summary>
        /// <param name="pg_uid"></param>
        /// <param name="blk_name"></param>
        /// <param name="topn">新闻条数</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "NewsCates/{pg_uid}/{blk_name}/{topn}")]
        [Description("获得App头部导航条或者获得CMS推送的图片新闻")]
        Stream CMSItems_SelectPaged(string pg_uid, string blk_name, string topn);

        /// <summary>
        /// 根据cat_ctrl获得新闻列表
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [OperationContract]
        [Description("根据cat_ctrl获得新闻列表")]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "News/{cat_ctrl}/{page}/{pagesize}")]
        List<NewsInfo> News_SelectPaged(string cat_ctrl, string page, string pagesize);

        /// <summary>
        /// 根据cat_ctrl获得新闻列表
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [OperationContract]
        [Description("根据cat_ctrl获得最新新闻")]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "News/Last/{cat_ctrl}")]
        NewsInfo LastNews_Select(string cat_ctrl);

        /// <summary>
        /// 根据cat_ctrl，cat_id获得新闻列表
        /// </summary>
        /// <param name="cat_ctrl"></param>
        /// <param name="cat_id"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [OperationContract]
        [Description("根据cat_ctrl，cat_id获得新闻列表")]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "news/{cat_ctrl}/{cat_id}/{page}/{pagesize}")]
        List<NewsInfo> News_SelectPagedBycat_id(string cat_ctrl, string cat_id, string page, string pagesize);

        /// <summary>
        /// 获得某条新闻的详细内容
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "News/NewsContent")]
        [Description("获得某条新闻的详细内容")]
        Stream NewsContent_Selectbync_n_gid(NewsContentInfo_RD rData);


        /// <summary>
        /// 获得某文章的所有评论（最新评论在前面）
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/allComments/{sourceId}/{page}/{limit}")]
        [Description("获得某文章的所有评论（最新评论在前面）")]
        Stream Comments_SelectPaged(string sourceId, string page, string limit);

        /// <summary>
        /// 获得某文章的所有评论（最新评论在前面,新的接口，返回数据较原来简单）
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/allComments_new/{sourceId}/{page}/{limit}")]
        [Description("获得某文章的所有评论（最新评论在前面）")]
        Stream Comments_SelectPaged2(string sourceId, string page, string limit);

        /// <summary>
        /// 获取热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/recommendComments/{sourceId}/{page}/{limit}")]
        [Description("获取热门评论")]
        Stream RecommendComments_SelectPaged(string sourceId, string page, string limit);

        /// <summary>
        /// 获取热门评论
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/recommendComments_new/{sourceId}/{page}/{limit}")]
        [Description("获取热门评论")]
        Stream RecommendComments_SelectPaged2(string sourceId, string page, string limit);


        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/commentAdd")]
        [Description("发表评论")]
        Stream CommentAdd(CommentsData rData);


        /// <summary>
        /// 评论顶操作
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/doAgree")]
        [Description("评论顶操作")]
        Stream DoAgree(RequestData rData);


        /// <summary>
        /// 获得评论总条数
        /// </summary>
        /// <param name="n_gid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/GetCommentCount/{n_gid}")]
        [Description("获得评论总条数")]
        Stream GetCommentCount(string n_gid);

        /// <summary>
        /// 加载个人评论
        /// </summary>
        /// <param name="n_gid"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "comment/LoadUserContent/{userId}/{page}/{pagesize}")]
        [Description("加载个人评论")]
        Stream LoadUserContent(string userId, string page, string pagesize);
    }
}
