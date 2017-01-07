using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QZ.Service.Enterprise
{
    public static class SiteConfigHelper
    {
        //登录搜索最高页数
        public const string UserSearchPage = "UserSearchPage";

        //Vip登录搜索最高页数
        public const string VipUserSearchPage = "VipUserSearchPage";

        //没有登录搜索最高页数
        public const string NoUserSearchPage = "NoUserSearchPage";

        //登录商标搜索最高页数
        public const string BrandPage = "BrandPage";
        //没有登录商标搜索最高页数
        public const string NoBrandPage = "NoBrandPage";


        //批量查询截取长度
        public const string ExtractContentLength = "ExtractContentLength";


        //导出展会信息限制次数
        public const string ExcelExhibitionLogCount = "ExcelExhibitionLogCount";

        /// <summary>
        /// 企业信息导出每天限制次数
        /// </summary>
        public const string ExcelCompanyLogCount = "ExcelCompanyLogCount";
        /// <summary>
        /// 信用报告导出每天限制次数
        /// </summary>
        public const string CreditReportLogCount = "CreditReportLogCount";


        //是否缓存新闻当天对象
        public const string IsNewsCache = "IsNewsCache";

        //小工具key 解码code用
        public const string OrgCodeKey = "OrgCodeKey";

        /// <summary>
        /// 是否认领通过审核
        /// </summary>
        public const string IsClaimStatus = "IsClaimStatus";

        /// <summary>
        /// 读取可配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSiteConfig(string key)
        {
            string msg = "";
            SiteConfig configSection = null;
            if (HttpRuntime.Cache["OrgCompanyMvc_SiteConfig"] == null)
            {
                var ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "Config\\Site.config";

                if (!string.IsNullOrEmpty(ConfigFile))
                //这里的ConfigFile是你要读取的Xml文件的路径，如果为空，则使用默认的app.config文件
                {
                    try
                    {
                        var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = ConfigFile };
                        var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        configSection = (SiteConfig)config.GetSection("SiteConfig");
                        msg = configSection.KeyValues[key].Value;
                        //添加缓存依赖
                        System.Web.Caching.CacheDependency cdy = new System.Web.Caching.CacheDependency(ConfigFile);
                        //添加缓存项
                        HttpRuntime.Cache.Insert(
                                "OrgCompanyMvc_SiteConfig",
                                configSection,
                                cdy,
                                DateTime.MaxValue,
                                System.Web.Caching.Cache.NoSlidingExpiration
                               );
                    }
                    catch (Exception ex)
                    {
                        msg = "配置有误";

                    }
                }
            }
            else
            {
                configSection = (SiteConfig)HttpRuntime.Cache["OrgCompanyMvc_SiteConfig"];
                msg = configSection.KeyValues[key].Value;
            }

            if (configSection == null)
            {
                msg = "配置有误";
            }
            return msg;
        }
    }
}