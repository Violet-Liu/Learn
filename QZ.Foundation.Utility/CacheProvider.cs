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
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace QZ.Foundation.Utility
{
    //[Obsolete]
    //public static class CacheProvider
    //{

    //    public static MemcachedClient mc = null;

    //    static CacheProvider()
    //    {
    //        mc = new MemcachedClient("SDCacheProviderConfig/sdmemcached");
    //    }

    //    public static bool Append(string strKey, byte[] data)
    //    {
    //        return mc.Append(strKey, new ArraySegment<byte>(data));
    //    }

    //    //#region CheckAndSet

    //    //public static bool CheckAndSet(string key, object value, ulong cas)
    //    //{
    //    //    return mc.CheckAndSet(key, value, cas);
    //    //}

    //    //public static bool CheckAndSet(string key, byte[] value, int offset, int length, ulong cas)
    //    //{
    //    //    return mc.CheckAndSet(key, value, offset, length, cas);
    //    //}

    //    //public static bool CheckAndSet(string key, object value, ulong cas, TimeSpan validFor)
    //    //{
    //    //    return mc.CheckAndSet(key, value, cas, validFor);
    //    //}

    //    //public static bool CheckAndSet(string key, object value, ulong cas, DateTime expiresAt)
    //    //{
    //    //    return mc.CheckAndSet(key, value, cas, expiresAt);
    //    //}

    //    //public static bool CheckAndSet(string key, byte[] value, int offset, int length, ulong cas, TimeSpan validFor)
    //    //{
    //    //    return mc.CheckAndSet(key, value, offset, length, cas, validFor);
    //    //}

    //    //public static bool CheckAndSet(string key, byte[] value, int offset, int length, ulong cas, DateTime expiresAt)
    //    //{
    //    //    return mc.CheckAndSet(key, value, offset, length, cas, expiresAt);
    //    //}

    //    //#endregion CheckAndSet

    //    //public static long Decrement(string key, uint amount)
    //    //{
    //    //    return mc.Decrement(key, amount);
    //    //}

    //    public static void FlushAll()
    //    {
    //        mc.FlushAll();
    //    }

    //    #region Get *****************************************************

    //    public static object Get(string key)
    //    {
    //        return mc.Get(key);
    //    }
    //    public static IDictionary<string, object> Get(IEnumerable<string> keys)
    //    {
    //        return mc.Get(keys);
    //    }

    //    //public static IDictionary<string, object> Get(IEnumerable<string> keys, out IDictionary<string, ulong> casValues)
    //    //{
    //    //    return mc.Get(keys, out casValues);
    //    //}

    //    public static T Get<T>(string key)
    //    {
    //        return mc.Get<T>(key);
    //    }

    //    #endregion Get

    //    //public static long Increment(string key, uint amount)
    //    //{
    //    //    return mc.Increment(key, amount);
    //    //}

    //    //public static bool Prepend(string key, byte[] data)
    //    //{
    //    //    return mc.Prepend(key, data);
    //    //}

    //    public static bool Remove(string key)
    //    {
    //        return mc.Remove(key);
    //    }

    //    public static ServerStats Stats()
    //    {
    //        return mc.Stats();
    //    }

    //    #region Restore ******************************************

    //    public static bool Store(string key, object value)
    //    {
    //        return mc.Store(StoreMode.Set, key, value);
    //    }


    //    //public static bool Store(string key, byte[] value, int offset, int length)
    //    //{
    //    //    return mc.Store(StoreMode.Set, key, value, offset, length);
    //    //}


    //    public static bool Store(string key, object value, TimeSpan validFor)
    //    {
    //        return mc.Store(StoreMode.Set, key, value, validFor);
    //    }


    //    public static bool Store(string key, object value, DateTime expiresAt)
    //    {
    //        return mc.Store(StoreMode.Set, key, value, expiresAt);
    //    }


    //    //public static bool Store(string key, byte[] value, int offset, int length, TimeSpan validFor)
    //    //{
    //    //    return mc.Store(StoreMode.Set, key, value, offset, length, validFor);
    //    //}


    //    //public static bool Store(string key, byte[] value, int offset, int length, DateTime expiresAt)
    //    //{
    //    //    return mc.Store(StoreMode.Set, key, value, offset, length, expiresAt);
    //    //}




    //    public static bool Store(StoreMode mode, string key, object value)
    //    {
    //        return mc.Store(mode, key, value);
    //    }


    //    //public static bool Store(StoreMode mode, string key, byte[] value, int offset, int length)
    //    //{
    //    //    return mc.Store(mode, key, value, offset, length);
    //    //}


    //    public static bool Store(StoreMode mode, string key, object value, TimeSpan validFor)
    //    {
    //        return mc.Store(mode, key, value, validFor);
    //    }


    //    public static bool Store(StoreMode mode, string key, object value, DateTime expiresAt)
    //    {
    //        return mc.Store(mode, key, value, expiresAt);
    //    }


    //    //public static bool Store(StoreMode mode, string key, byte[] value, int offset, int length, TimeSpan validFor)
    //    //{
    //    //    return mc.Store(mode, key, value, offset, length, validFor);
    //    //}


    //    //public static bool Store(StoreMode mode, string key, byte[] value, int offset, int length, DateTime expiresAt)
    //    //{
    //    //    return mc.Store(mode, key, value, offset, length, expiresAt);
    //    //}
    //    #endregion Restore     


    //}
}
