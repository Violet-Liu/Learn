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
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using QZ.Instrument.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;
namespace QZ.Instrument.DataAccess
{
    public class DataAccess_QzNews : AccessBase, IDisposable
    {
        #region IDisposable 接口实现
        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~DataAccess_QzNews()
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

        private string _key_0;
        /// <summary>
        /// 数据库连接
        /// </summary>
        Database _db_0;
        public Database Db_0
        {
            get
            {
                if (_db_0 == null)
                    _db_0 = DatabaseFactory.CreateDatabase(_key_0);
                return _db_0;
            }
        }

        public DataAccess_QzNews(string key)
        {
            this._key_0 = key;
        }

        /// <summary>
        /// 选择信息分类
        /// </summary>
        /// <param name="cat_id">The cat_id.</param>
        /// <returns></returns>
        public NewsCateInfo NewsCates_Selectbycat_id(int cat_id)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_NewsCates_Selectbycat_id");
            Db_0.AddInParameter(dbCommandWrapper, "@cat_id", DbType.Int32, cat_id);

            try
            {
                NewsCateInfo obj = null;
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    if (reader.Read())
                    {
                        obj = getNewsCateInfo(reader);
                    }
                }

                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 获取最新N条信息
        /// </summary>
        /// <param name="n_blk_id">The n_blk_id.</param>
        /// <param name="topn">The topn.</param>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public List<CMSItemsInfo> CMSItems_SelectTopN(int n_blk_id, int topn, string where)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_CMSItems_SelectTopN");
            Db_0.AddInParameter(dbCommandWrapper, "@n_blk_id", DbType.Int32, n_blk_id);
            Db_0.AddInParameter(dbCommandWrapper, "@topN", DbType.Int32, topn);
            Db_0.AddInParameter(dbCommandWrapper, "@where", DbType.String, where);

            List<CMSItemsInfo> lst = new List<CMSItemsInfo>();
            using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
            {
                while (reader.Read())
                {
                    lst.Add(getCMSItemsInfo(reader));
                }
                reader.NextResult();
            }

            return lst;
        }
        private CMSItemsInfo getCMSItemsInfo(IDataReader reader)
        {
            CMSItemsInfo obj = new CMSItemsInfo();
            obj.n_id = (int)reader["n_id"];
            obj.n_title = reader["n_title"].ToString();
            obj.n_subtitle = reader["n_subtitle"].ToString();
            obj.n_brief = reader["n_brief"].ToString();
            obj.n_date = reader["n_date"].ToString();
            obj.n_imageUrl = reader["n_imageUrl"].ToString();
            obj.n_linkUrl = reader["n_linkUrl"].ToString();
            obj.n_target = reader["n_target"].ToString();
            obj.n_cssClass = reader["n_cssClass"].ToString();
            obj.n_iconType = (int)reader["n_iconType"];
            obj.n_status = (int)reader["n_status"];
            obj.n_publish = (bool)reader["n_publish"];
            obj.n_order = (int)reader["n_order"];
            obj.n_publisher = reader["n_publisher"].ToString();
            obj.n_publishTime = (DateTime)reader["n_publishTime"];
            obj.n_allowTime = reader["n_allowTime"].ToString();
            obj.n_blk_id = (int)reader["n_blk_id"];
            obj.n_pg_id = (int)reader["n_pg_id"];
            obj.n_sourceClassId = reader["n_sourceClassId"].ToString();
            obj.n_sourceId = reader["n_sourceId"].ToString();
            obj.n_type = (int)reader["n_type"];
            DateTime dt = DateTime.Now;
            if (DateTime.TryParse(obj.n_date, out dt))
                obj.DistanceNow = Util.DateStringFromNow(dt);
            else
                obj.DistanceNow = obj.n_date;
            return obj;
        }
        /// <summary>
        /// 分页选择信息
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="where">The where.</param>
        /// <param name="order">The order.</param>
        /// <param name="page">The page.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="rowcount">The rowcount.</param>
        /// <returns></returns>
        public List<NewsInfo> News_SelectPaged(string tableName, string columns, string where, string order, int page, int pagesize, out int rowcount)
        {
            //this.News_CreateTable(tableName);
            rowcount = 0;
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_BaseBetween_SelectByPageIndex");
            Db_0.AddInParameter(dbCommandWrapper, "@Columns", DbType.String, columns);
            Db_0.AddInParameter(dbCommandWrapper, "@tableName", DbType.String, tableName);
            Db_0.AddInParameter(dbCommandWrapper, "@Where", DbType.String, where);
            Db_0.AddInParameter(dbCommandWrapper, "@Order", DbType.String, order);
            Db_0.AddInParameter(dbCommandWrapper, "@Page", DbType.Int32, page);
            Db_0.AddInParameter(dbCommandWrapper, "@pageSize", DbType.Int32, pagesize);
            Db_0.AddOutParameter(dbCommandWrapper, "@rowCount", DbType.Int32, 4);

            List<NewsInfo> lst = new List<NewsInfo>();
            try
            {
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    rowcount = -1;
                    while (reader.Read())
                    {
                        lst.Add(getNewsInfo(reader));
                    }
                    reader.NextResult();
                    rowcount = (int)dbCommandWrapper.Parameters["@rowCount"].Value;
                }

                return lst;
            }
            catch (Exception e)
            {
                if (rowcount == 0)
                {
                    return lst;
                }
                throw new Exception(e.Message);
            }
        }
        private NewsInfo getNewsInfo(IDataReader reader)
        {
            NewsInfo obj = new NewsInfo();
            obj.n_id = (int)reader["n_id"];
            obj.n_cat_id = (int)reader["n_cat_id"];
            obj.n_type = reader["n_type"].ToString();
            obj.n_gid = reader["n_gid"].ToString();
            obj.n_title = reader["n_title"].ToString();
            obj.n_linkUrl = reader["n_linkUrl"].ToString();
            obj.n_summary = reader["n_summary"].ToString();
            obj.n_tags = reader["n_tags"].ToString();
            obj.n_source = reader["n_source"].ToString();
            obj.n_date = reader["n_date"].ToString();
            DateTime dt = DateTime.Now;
            if (DateTime.TryParse(obj.n_date, out dt))
                obj.DistanceNow = Util.DateStringFromNow(dt);
            else
                obj.DistanceNow = obj.n_date;
            obj.n_css = reader["n_css"].ToString();
            obj.n_icon = reader["n_icon"].ToString();
            obj.n_listType = (int)reader["n_listType"];
            obj.n_pages = (int)reader["n_pages"];
            obj.n_hits = (int)reader["n_hits"];
            obj.n_diggs = (int)reader["n_diggs"];
            obj.n_favors = (int)reader["n_favors"];
            obj.n_pic1 = reader["n_pic1"].ToString();
            obj.n_pic2 = reader["n_pic2"].ToString();
            obj.n_state = (int)reader["n_state"];
            obj.n_right = (int)reader["n_right"];
            obj.n_sort = (int)reader["n_sort"];
            obj.n_createUser = reader["n_createUser"].ToString();
            obj.n_createTime = (DateTime)reader["n_createTime"];
            obj.n_analyst = reader["n_analyst"].ToString();
            obj.n_ready1 = reader["n_ready1"].ToString();
            obj.n_ready2 = reader["n_ready2"].ToString();
            obj.n_ready3 = (int)reader["n_ready3"];
            obj.n_sourceId = reader["n_sourceId"].ToString();
            obj.n_sourceUrl = reader["n_sourceUrl"].ToString();

            try
            {
                obj.n_group = reader["n_group"].ToString();
                obj.n_sTitle = reader["n_sTitle"].ToString();
                obj.n_authors = reader["n_authors"].ToString();
            }
            catch
            {
                obj.n_sTitle = string.Empty;
                obj.n_authors = string.Empty;
            }

            if (obj.n_sTitle == string.Empty)
                obj.n_sTitle = obj.n_title;

            return obj;
        }
        /// <summary>
        /// Newses the content_ selectbync_n_gid.
        /// </summary>
        /// <param name="nc_n_gid">The nc_n_gid.</param>
        /// <returns></returns>
        public List<NewsContentInfo> NewsContent_Selectbync_n_gid(string nc_n_gid)
        {
            string SQL = string.Format(@"Select * From NewsContent_{0} WHERE nc_n_gid=@nc_n_gid Order by nc_order,nc_id", getContentTableBy(nc_n_gid));

            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(SQL);

            Db_0.AddInParameter(dbCommandWrapper, "@nc_n_gid", DbType.String, nc_n_gid);

            try
            {

                List<NewsContentInfo> lst = new List<NewsContentInfo>();
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    while (reader.Read())
                    {
                        NewsContentInfo obj = new NewsContentInfo();
                        obj.nc_id = (int)reader["nc_id"];
                        obj.nc_title = reader["nc_title"].ToString();
                        obj.nc_simg_url = reader["nc_simg_url"].ToString();
                        obj.nc_bimg_url = reader["nc_bimg_url"].ToString();
                        obj.nc_content = reader["nc_content"].ToString();
                        obj.nc_createTime = (DateTime)reader["nc_createTime"];
                        obj.nc_shortcreateTime= ((DateTime)reader["nc_createTime"]).ToShortDateString();
                        obj.nc_createUser = reader["nc_createUser"].ToString();
                        obj.nc_order = (int)reader["nc_order"];
                        obj.nc_n_gid = reader["nc_n_gid"].ToString();
                        lst.Add(obj);
                    }
                    reader.NextResult();
                }

                return lst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private string getContentTableBy(string n_gid)
        {
            if (n_gid.Length > 20)
                return n_gid.Substring(0, 6);

            return "20" + n_gid.Substring(0, 4);
        }


        /// <summary>
        /// 插入访问日志
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public int VisitLog_Insert(VisitLogInfo obj, string tableName)
        {
            string SQL = string.Format(@"INSERT INTO {0}(
                        vl_type,
                        vl_cateId,
                        vl_gid,
	                    vl_cookieId,
	                    vl_userName,
	                    vl_referrer,
	                    vl_url,
	                    vl_totalTime,
	                    vl_ip,
	                    vl_osName,
	                    vl_browser,
	                    vl_screenSize,
	                    vl_spiderName,
	                    vl_createTime,
	                    vl_host,
	                    vl_remoteAddr
                    ) VALUES(
                        @vl_type,
                        @vl_cateId,
                        @vl_gid,
	                    @vl_cookieId,
	                    @vl_userName,
	                    @vl_referrer,
	                    @vl_url,
	                    @vl_totalTime,
	                    @vl_ip,
	                    @vl_osName,
	                    @vl_browser,
	                    @vl_screenSize,
	                    @vl_spiderName,
	                    getdate(),
	                    @vl_host,
	                    @vl_remoteAddr
                    ); set @vl_id=@@identity", tableName);

            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(SQL);

            Db_0.AddOutParameter(dbCommandWrapper, "@vl_id", DbType.Int32, 4);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_type", DbType.String, obj.vl_type);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_cateId", DbType.String, obj.vl_cateId ?? string.Empty);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_gid", DbType.String, obj.vl_gid);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_cookieId", DbType.String, obj.vl_cookieId);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_userName", DbType.String, obj.vl_userName);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_referrer", DbType.String, obj.vl_referrer);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_url", DbType.String, obj.vl_url);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_totalTime", DbType.Int32, obj.vl_totalTime);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_ip", DbType.String, obj.vl_ip);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_osName", DbType.String, obj.vl_osName);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_browser", DbType.String, obj.vl_browser);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_screenSize", DbType.String, obj.vl_screenSize);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_spiderName", DbType.String, obj.vl_spiderName);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_host", DbType.String, obj.vl_host);
            Db_0.AddInParameter(dbCommandWrapper, "@vl_remoteAddr", DbType.String, obj.vl_remoteAddr);

            try
            {
                Db_0.ExecuteNonQuery(dbCommandWrapper);
                int vl_id = (int)dbCommandWrapper.Parameters["@vl_id"].Value;

                return vl_id;
            }
            catch (Exception e)
            {
                // 创建日志表
                this.createVisitLog(tableName);

                return -1;
                //throw new Exception(e.Message);
            }
            /*using(DataAccess access = new DataAccess())
            {
                return access.tbVisitLog_Insert( obj );
            }*/
        }
        private void createVisitLog(string tableName)
        {
            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(string.Format("SELECT 1 FROM sysobjects WHERE id = OBJECT_ID('{0}') AND type = 'U'", tableName));

            object rtn = Db_0.ExecuteScalar(dbCommandWrapper);
            if (rtn == null)//创建表
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format(@"create table {0} (
                   vl_id            int                   identity(1,1) not null,
                   vl_type          nvarchar(20)          not null,
                   vl_cateId        varchar(32)           not null,
                   vl_gid           nvarchar(50)          not null,
                   vl_cookieId          varchar(32)          not null,
                   vl_userName          varchar(30)          not null,
                   vl_referrer          varchar(1000)        not null, 
                   vl_url               varchar(250)         not null,
                   vl_totalTime         int                  not null,
                   vl_ip                varchar(50)          not null,
                   vl_ipArea            nvarchar(50)         null,
                   vl_ipCountry         nvarchar(50)         null,
                   vl_osName            varchar(100)         not null,
                   vl_browser           varchar(100)         not null,
                   vl_screenSize        varchar(30)          not null,
                   vl_spiderName        varchar(50)          not null,
                   vl_createTime        datetime             not null,
                   vl_remoteAddr        varchar(20)          null,
                   vl_host          varchar(50)           null)", tableName));

                //sb.Append(string.Format("constraint PK_{0} primary key (vl_id))", tableName));
                //sb.Append(string.Format("create unique index IX_ueo_guid on {0} (ueo_guid ASC)", tableName));
                //sb.Append(string.Format("create index IX_uol_user on {0} (uol_user ASC)", tableName));

                dbCommandWrapper = Db_0.GetSqlStringCommand(sb.ToString());
                Db_0.ExecuteNonQuery(dbCommandWrapper);
            }
        }
        /// <summary>
        /// Comments_s the select paged.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="where">The where.</param>
        /// <param name="order">The order.</param>
        /// <param name="page">The page.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="only4Ids">if set to <c>true</c> [only4 ids].</param>
        /// <param name="rowcount">The rowcount.</param>
        /// <returns></returns>
        public List<CommentInfo> Comments_SelectPaged(string columns, string where, string order, int page, int pagesize, bool only4Ids, out int rowcount)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_SelectPaged");
            Db_0.AddInParameter(dbCommandWrapper, "@columns", DbType.String, columns);
            Db_0.AddInParameter(dbCommandWrapper, "@where", DbType.String, where);
            Db_0.AddInParameter(dbCommandWrapper, "@order", DbType.String, order);
            Db_0.AddInParameter(dbCommandWrapper, "@page", DbType.Int32, page);
            Db_0.AddInParameter(dbCommandWrapper, "@pageSize", DbType.Int32, pagesize);
            Db_0.AddOutParameter(dbCommandWrapper, "@rowCount", DbType.Int32, 4);

            try
            {

                List<CommentInfo> lst = new List<CommentInfo>();
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    while (reader.Read())
                    {
                        CommentInfo obj = getCommentInfo(reader, only4Ids);

                        lst.Add(obj);
                    }
                    reader.NextResult();
                    rowcount = (int)dbCommandWrapper.Parameters["@rowCount"].Value;
                }


                return lst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private CommentInfo getCommentInfo(IDataReader reader, bool only4Ids)
        {
            CommentInfo obj = new CommentInfo();

            obj.cmt_id = (int)reader["cmt_id"];
            obj.cmt_uid = reader["cmt_uid"].ToString();
            obj.cmt_sourceId = reader["cmt_sourceId"].ToString();
            obj.cmt_parentIds = reader["cmt_parentIds"].ToString();

            if (!only4Ids)
            {
                obj.cmt_sourceType = reader["cmt_sourceType"].ToString();
                obj.cmt_title = reader["cmt_title"].ToString();
                obj.cmt_content = reader["cmt_content"].ToString();
                obj.cmt_accept = (int)reader["cmt_accept"];
                obj.cmt_status = (int)reader["cmt_status"];
                obj.cmt_ip = reader["cmt_ip"].ToString();
                obj.cmt_ipArea = reader["cmt_ipArea"].ToString();
                obj.cmt_createUserID = (int)reader["cmt_createUserID"];
                obj.cmt_createUser = reader["cmt_createUser"].ToString();
                obj.cmt_createTime = (DateTime)reader["cmt_createTime"];
                obj.cmt_checkUser = reader["cmt_checkUser"].ToString();
                obj.cmt_checkTime = reader["cmt_checkTime"].ToString();
                obj.cmt_checkRemark = reader["cmt_checkRemark"].ToString();

                obj.cmt_platform = reader["cmt_platform"].ToString(); //平台：PC Mobile APP
                obj.cmt_sourceCateId = reader["cmt_sourceCateId"].ToString(); //评论对象源分类ID
                obj.cmt_device = reader["cmt_device"].ToString(); // 浏览器或操作系统名
                obj.cmt_down = (int)reader["cmt_down"]; // 踩字段
            }
            return obj;
        }

        public int Comments_CountbysourceId(string cmt_sourceid)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_CountbysourceId");
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceId", DbType.String, cmt_sourceid);

            try
            {
                return (int)Db_0.ExecuteScalar(dbCommandWrapper);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 分页选择信息分类
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="where">The where.</param>
        /// <param name="order">The order.</param>
        /// <param name="page">The page.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="rowcount">The rowcount.</param>
        /// <returns></returns>
        public List<NewsCateInfo> NewsCates_SelectPaged(string columns, string where, string order, int page, int pagesize, out int rowcount)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_NewsCates_SelectPaged");
            Db_0.AddInParameter(dbCommandWrapper, "@columns", DbType.String, columns);
            Db_0.AddInParameter(dbCommandWrapper, "@where", DbType.String, where);
            Db_0.AddInParameter(dbCommandWrapper, "@order", DbType.String, order);
            Db_0.AddInParameter(dbCommandWrapper, "@page", DbType.Int32, page);
            Db_0.AddInParameter(dbCommandWrapper, "@pageSize", DbType.Int32, pagesize);
            Db_0.AddOutParameter(dbCommandWrapper, "@rowCount", DbType.Int32, 4);

            try
            {

                List<NewsCateInfo> lst = new List<NewsCateInfo>();
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    while (reader.Read())
                    {
                        lst.Add(getNewsCateInfo(reader));
                    }
                    reader.NextResult();
                    rowcount = (int)dbCommandWrapper.Parameters["@rowCount"].Value;
                }

                return lst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private NewsCateInfo getNewsCateInfo(IDataReader reader)
        {
            NewsCateInfo obj = new NewsCateInfo();

            obj.cat_id = (int)reader["cat_id"];
            obj.cat_uid = reader["cat_uid"].ToString();
            obj.cat_path = reader["cat_path"].ToString();
            obj.cat_name = reader["cat_name"].ToString();
            obj.cat_brief = reader["cat_brief"].ToString();
            obj.cat_url = reader["cat_url"].ToString();
            obj.cat_manager = reader["cat_manager"].ToString();
            obj.cat_status = (int)reader["cat_status"];
            obj.cat_totalNews = (int)reader["cat_totalNews"];
            obj.cat_inheritAll = (bool)reader["cat_inheritAll"];
            obj.cat_createUser = reader["cat_createUser"].ToString();
            obj.cat_createTime = (DateTime)reader["cat_createTime"];
            obj.cat_tableIndex = (int)reader["cat_tableIndex"];
            obj.cat_isLast = (bool)reader["cat_isLast"];
            obj.cat_totalUnchecked = (int)reader["cat_totalUnchecked"];
            obj.cat_listType = (int)reader["cat_listType"];
            obj.cat_ctrl = reader["cat_ctrl"].ToString();
            obj.cat_lang = reader["cat_lang"].ToString();
            obj.cat_tradeInList = reader["cat_tradeInList"].ToString();
            obj.cat_title = reader["cat_title"].ToString();

            return obj;
        }
        public int RecommendComments_Insert(RecommendCommentInfo obj)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_RecommendComments_Insert");
            Db_0.AddOutParameter(dbCommandWrapper, "@rcmt_id", DbType.Int32, 4);
            Db_0.AddInParameter(dbCommandWrapper, "@rcmt_createUser", DbType.String, obj.rcmt_createUser);
            Db_0.AddInParameter(dbCommandWrapper, "@rcmt_createTime", DbType.DateTime, obj.rcmt_createTime);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_id", DbType.Int32, obj.cmt_id);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_uid", DbType.String, obj.cmt_uid);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceType", DbType.String, obj.cmt_sourceType);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceId", DbType.String, obj.cmt_sourceId);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_title", DbType.String, obj.cmt_title);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_content", DbType.String, obj.cmt_content);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_parentIds", DbType.String, obj.cmt_parentIds);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_accept", DbType.Int32, obj.cmt_accept);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_status", DbType.Int32, obj.cmt_status);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_ip", DbType.String, obj.cmt_ip);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_ipArea", DbType.String, obj.cmt_ipArea);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createUserID", DbType.Int32, obj.cmt_createUserID);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createUser", DbType.String, obj.cmt_createUser);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createTime", DbType.DateTime, obj.cmt_createTime);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkUser", DbType.String, obj.cmt_checkUser);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkTime", DbType.String, obj.cmt_checkTime);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkRemark", DbType.String, obj.cmt_checkRemark);

            try
            {

                int _returnValue = Db_0.ExecuteNonQuery(dbCommandWrapper);
                int rcmt_id = (int)dbCommandWrapper.Parameters["@rcmt_id"].Value;

                return _returnValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CommentInfo> RecommendComments_SelectPaged(string columns, string where, string order, int page, int pagesize, out int rowcount)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_RecommendComments_SelectPaged");
            Db_0.AddInParameter(dbCommandWrapper, "@columns", DbType.String, columns);
            Db_0.AddInParameter(dbCommandWrapper, "@where", DbType.String, where);
            Db_0.AddInParameter(dbCommandWrapper, "@order", DbType.String, order);
            Db_0.AddInParameter(dbCommandWrapper, "@page", DbType.Int32, page);
            Db_0.AddInParameter(dbCommandWrapper, "@pageSize", DbType.Int32, pagesize);
            Db_0.AddOutParameter(dbCommandWrapper, "@rowCount", DbType.Int32, 4);

            try
            {

                List<CommentInfo> lst = new List<CommentInfo>();
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    while (reader.Read())
                    {
                        CommentInfo obj = new CommentInfo();
                        obj.cmt_id = (int)reader["cmt_id"];
                        obj.cmt_uid = reader["cmt_uid"].ToString();
                        obj.cmt_sourceType = reader["cmt_sourceType"].ToString();
                        obj.cmt_sourceId = reader["cmt_sourceId"].ToString();
                        obj.cmt_title = reader["cmt_title"].ToString();
                        obj.cmt_content = reader["cmt_content"].ToString();
                        obj.cmt_parentIds = reader["cmt_parentIds"].ToString();
                        obj.cmt_accept = (int)reader["cmt_accept"];
                        obj.cmt_status = (int)reader["cmt_status"];
                        obj.cmt_ip = reader["cmt_ip"].ToString();
                        obj.cmt_ipArea = reader["cmt_ipArea"].ToString();
                        obj.cmt_createUserID = (int)reader["cmt_createUserID"];
                        obj.cmt_createUser = reader["cmt_createUser"].ToString();
                        obj.cmt_createTime = (DateTime)reader["cmt_createTime"];
                        obj.cmt_checkUser = reader["cmt_checkUser"].ToString();
                        obj.cmt_checkTime = reader["cmt_checkTime"].ToString();
                        obj.cmt_checkRemark = reader["cmt_checkRemark"].ToString();
                        lst.Add(obj);
                    }
                    reader.NextResult();
                    rowcount = (int)dbCommandWrapper.Parameters["@rowCount"].Value;
                }


                return lst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CommentInfo Comments_Selectbycmt_uid(string cmt_uid)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_Selectbycmt_uid");
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_uid", DbType.String, cmt_uid);

            try
            {

                CommentInfo obj = null;
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    if (reader.Read())
                    {
                        obj = getCommentInfo(reader, false);
                    }
                }

                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int Comments_AddAccept(string cmt_uid, int cmt_accept)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_AddAccept");
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_uid", DbType.String, cmt_uid);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_accept", DbType.Int32, cmt_accept);

            try
            {

                int _returnValue = Db_0.ExecuteNonQuery(dbCommandWrapper);

                return _returnValue;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int Comments_Insert(CommentInfo obj, string cmt_parentUid, string cmt_sourceUrl)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_Insert_V2");
            Db_0.AddOutParameter(dbCommandWrapper, "@cmt_id", DbType.Int32, 4);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_uid", DbType.String, obj.cmt_uid);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceType", DbType.String, obj.cmt_sourceType);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceId", DbType.String, obj.cmt_sourceId);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_title", DbType.String, obj.cmt_title);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_content", DbType.String, obj.cmt_content);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_parentIds", DbType.String, obj.cmt_parentIds);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_accept", DbType.Int32, obj.cmt_accept);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_status", DbType.Int32, obj.cmt_status);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_ip", DbType.String, obj.cmt_ip);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_ipArea", DbType.String, obj.cmt_ipArea);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createUserID", DbType.Int32, obj.cmt_createUserID);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createUser", DbType.String, obj.cmt_createUser);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_createTime", DbType.DateTime, obj.cmt_createTime);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkUser", DbType.String, obj.cmt_checkUser);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkTime", DbType.String, obj.cmt_checkTime);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_checkRemark", DbType.String, obj.cmt_checkRemark);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_parentUid", DbType.String, cmt_parentUid);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceUrl", DbType.String, cmt_sourceUrl);

            //
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceCateId", DbType.String, obj.cmt_sourceCateId);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_platform", DbType.String, obj.cmt_platform);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_device", DbType.String, obj.cmt_device);

            try
            {
                Db_0.ExecuteNonQuery(dbCommandWrapper);
                int cmt_id = (int)dbCommandWrapper.Parameters["@cmt_id"].Value;

                return cmt_id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CommentInfo> Comments_Selectbycmt_uidStrs(string cmt_sourceid, string[] cmt_uids)
        {
            DbCommand dbCommandWrapper = Db_0.GetStoredProcCommand("Proc_Comments_Selectbycmt_uidStrs");
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_sourceId", DbType.String, cmt_sourceid);
            Db_0.AddInParameter(dbCommandWrapper, "@cmt_uids", DbType.String, string.Format("'{0}'", string.Join("','", cmt_uids)));

            try
            {

                List<CommentInfo> lst = new List<CommentInfo>();
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    while (reader.Read())
                    {
                        lst.Add(getCommentInfo(reader, false));
                    }
                    reader.NextResult();
                }

                return lst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public NewsInfo News_Selectbyn_gid(string n_gid, string tableName)
        {
            string SQL = string.Format(@"Select t1.*,t2.n_editor from {0} t1 left join NewsEditors t2 on t1.n_createUser=t2.n_user WHERE t1.n_gid=@n_gid", tableName);

            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(SQL);
            Db_0.AddInParameter(dbCommandWrapper, "@n_gid", DbType.String, n_gid);

            try
            {
                NewsInfo obj = null;
                using (IDataReader reader = Db_0.ExecuteReader(dbCommandWrapper))
                {
                    if (reader.Read())
                    {
                        obj = getNewsInfo(reader);
                        try
                        {
                            obj.n_editor = reader["n_editor"].ToString();
                        }
                        catch { }
                    }
                }

                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int FavorLog_Insert(string uid, string objType, string objId, string objName, string objCate, string clientId, string ip, string url, string tableName)
        {
            // 创建日志表
            this.createFavorLog(tableName);

            string SQL = string.Format(@"INSERT INTO {0}(
                        [fl_uid]
                        ,[fl_date]
                        ,[fl_clientId]
                        ,[fl_type]
                        ,[fl_objId]
                        ,[fl_objCate]
                        ,[fl_objName]
                        ,[fl_url]
                        ,[fl_ip]
                        ,[fl_createTime]
                    ) VALUES(
                        @fl_uid
                        ,@fl_date
                        ,@fl_clientId
                        ,@fl_type
                        ,@fl_objId
                        ,@fl_objCate
                        ,@fl_objName
                        ,@fl_url
                        ,@fl_ip
                        ,@fl_createTime
                    ); set @fl_id=@@identity", tableName);

            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(SQL);

            Db_0.AddOutParameter(dbCommandWrapper, "@fl_id", DbType.Int32, 4);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_uid", DbType.String, uid);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_date", DbType.String, DateTime.Today.ToString("yyyy-MM-dd"));
            Db_0.AddInParameter(dbCommandWrapper, "@fl_clientId", DbType.String, clientId);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_type", DbType.String, objType);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_objId", DbType.String, objId);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_objCate", DbType.String, objCate);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_objName", DbType.String, objName);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_url", DbType.String, url);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_ip", DbType.String, ip);
            Db_0.AddInParameter(dbCommandWrapper, "@fl_createTime", DbType.DateTime, DateTime.Now);

            try
            {
                Db_0.ExecuteNonQuery(dbCommandWrapper);
                int fl_id = (int)dbCommandWrapper.Parameters["@fl_id"].Value;

                return fl_id;
            }
            catch
            {
                return -1;
                //throw new Exception(e.Message);
            }
        }
        private void createFavorLog(string tableName)
        {
            DbCommand dbCommandWrapper = Db_0.GetSqlStringCommand(string.Format("SELECT 1 FROM sysobjects WHERE id = OBJECT_ID('{0}') AND type = 'U'", tableName));

            object rtn = Db_0.ExecuteScalar(dbCommandWrapper);
            if (rtn == null)//创建表
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(string.Format(@"create table {0} (
	                [fl_id] [int] IDENTITY(1,1) NOT NULL,
	                [fl_uid] [varchar](32) NOT NULL,
	                [fl_date] [varchar](10) NOT NULL,
	                [fl_clientId] [varchar](50) NOT NULL,
	                [fl_type] [nvarchar](20) NOT NULL,
	                [fl_objId] [varchar](32) NOT NULL,
	                [fl_objCate] [nvarchar](50) NOT NULL,
	                [fl_objName] [nvarchar](50) NULL,
	                [fl_url] [varchar](150) NULL,
	                [fl_ip] [varchar](20) NULL,
	                [fl_createTime] [datetime] NOT NULL
                 )", tableName));

                //sb.Append(string.Format("constraint PK_{0} primary key (afl_id))", tableName));
                sb.Append(string.Format("create unique index IX_fl_uid on {0} (fl_uid ASC)", tableName));
                sb.Append(string.Format("create index IX_fl_date on {0} (fl_date ASC)", tableName));

                dbCommandWrapper = Db_0.GetSqlStringCommand(sb.ToString());
                Db_0.ExecuteNonQuery(dbCommandWrapper);
            }
        }
    }
}
