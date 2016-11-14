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

namespace QZ.Instrument.Utility
{
    public class UserAgentCache
    {

        /// <summary>
        /// 最大65536
        /// </summary>
        public const int MAX_CACHE_ITEM = 0x10000;

        /// <summary>
        /// 字典
        /// </summary>
        public static Dictionary<string, UserAgent> dic;

        /// <summary>
        /// 锁对象
        /// </summary>
        static object o;

        static UserAgent Empty;

        /// <summary>
        /// 缓存对象
        /// </summary>
        static UserAgentCache()
        {
            o = new object();
            dic = new Dictionary<string, UserAgent>(MAX_CACHE_ITEM);
            Empty = new UserAgent(string.Empty);

        }


        /// <summary>
        /// 获取UserAgent
        /// </summary>
        /// <param name="rawUserAgentString"></param>
        /// <returns></returns>
        public static UserAgent GetUserAgentItem(string rawUserAgentString)
        {
            UserAgent item;
            if (dic.TryGetValue(rawUserAgentString, out item))
            {
                return item;
            }
            return null;
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="rawUserAgentString"></param>
        /// <param name="item"></param>
        public static void InsertCache(string rawUserAgentString, UserAgent item)
        {
            if (dic.Count >= MAX_CACHE_ITEM)
                return;
            if (dic.ContainsKey(rawUserAgentString))
                return;
            lock (o)
            {
                if (dic.Count >= MAX_CACHE_ITEM)
                    return;
                if (dic.ContainsKey(rawUserAgentString))
                    return;
                dic.Add(rawUserAgentString, item);
            }

        }

        /// <summary>
        /// 创建或者获取缓存中的数据
        /// </summary>
        /// <param name="rawUserAgentString"></param>
        /// <returns></returns>
        public static UserAgent CreateOrGetCacheItem(string rawUserAgentString)
        {
            if (rawUserAgentString == null || rawUserAgentString.Length < 1)
                return Empty;
            UserAgent item;
            if (dic.TryGetValue(rawUserAgentString, out item))
                return item;
            item = new UserAgent(rawUserAgentString);
            UserAgent.Scan(item);
            InsertCache(rawUserAgentString, item);
            return item;
        }







    }
}
