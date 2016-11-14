using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZ.Service.Enterprise
{
    /// <summary>
    /// 图谱html模板缓存类
    /// </summary>
    public class MapHtmlCache
    {
        //加载锁
        static object ro;

        /// <summary>
        /// htmlCache
        /// </summary>
        static MapHtmlCache()
        {
            ro = new object();
            maps = new List<KeyValuePair<string, string>>(0x10);
        }

        /// <summary>
        /// 模板集合
        /// </summary>
        static List<KeyValuePair<string, string>> maps;


        /// <summary>
        /// 根据模板名称获取图谱HTML模板
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMap(string name)
        {
            string html;
            if (InternalTryFind(name, out html))
                return html;
            return InternalLoadByName(name);
        }

        /// <summary>
        /// 尝试根据名称找到模板
        /// </summary>
        /// <param name="name"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        private static bool InternalTryFind(string name, out string html)
        {

            int len = maps.Count;
            if (len < 1)
            {
                html = string.Empty;
                return false;
            }
            int i = 0;
            while (i < len)
            {
                if (maps[i].Key == name)
                {
                    html = maps[i].Value;
                    return true;
                }
                i++;
            }
            html = string.Empty;
            return false;

        }

        /// <summary>
        /// 加载一个模板
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string InternalLoadByName(string name)
        {
            lock (ro)
            {
                string html;
                if (InternalTryFind(name, out html))
                    return html;
                html = System.IO.File.ReadAllText(
                 string.Format("{0}{1}",
                 AppDomain.CurrentDomain.BaseDirectory,
                 System.Configuration.ConfigurationManager.AppSettings[name].TrimStart('\\')));
                maps.Add(new KeyValuePair<string, string>(name, html));
                return html;
            }
        }




    }
}