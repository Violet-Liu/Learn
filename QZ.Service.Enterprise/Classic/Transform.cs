using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using QZ.Instrument.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;

namespace QZ.Service.Enterprise
{
    public static class Transform
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static AppOrgCompanyLogInfo To_AppOrgCompanyLog(this Request_Head h)
        {
            var l = new AppOrgCompanyLogInfo();
            l.cl_browser = UserAgentCache.CreateOrGetCacheItem(WebOperationContext.Current.IncomingRequest.UserAgent).GetBrowser();
            l.cl_cookieId = h.Cookie;
            l.cl_date = DateTime.Now;
            l.cl_screenSize = h.Screen_Size;

            l.cl_appVer = h.App_Ver;
            if (h.Platform == Platform.Android)
            {
                l.cl_osName = "Android";
            }
            else if (h.Platform == Platform.Iphone)
                l.cl_osName = "IOS";

            return l;
        }

        public static BrowseLogInfo To_BrowseLog(this Company c)
        {
            var log = new BrowseLogInfo();
            log.bl_date = DateTime.Now;
            log.bl_oc_code = c.oc_code;
            log.bl_oc_area = c.oc_area;
            log.bl_oc_name = c.oc_name;
            log.bl_u_uid = c.u_id.ToInt();
            log.bl_u_name = c.u_name;
            return log;
        }
    }
}