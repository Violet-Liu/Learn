/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility
{
    public static class Extension
    {
        public static List<U> Select_Where<T, U>(this List<T> src, Func<T, U> select, Predicate<T> predicate)
        {
            var dest = new List<U>();
            foreach(var t in src)
            {
                if (predicate(t))
                    dest.Add(select(t));
            }
            return dest;
        }

        /// <summary>
        /// convert a string to an int number. return -1 if failed to convert
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i))
                return i;

            return -1;
        }

        public static long ToLong(this string s)
        {
            long i;
            if (long.TryParse(s, out i))
                return i;
            return -1;
        }
        public static double ToDouble(this string s)
        {
            double d;
            if (double.TryParse(s, out d))
                return d;
            return -1;
        }
        public static List<string> Tel_Get(this string s)
        {
            var matches = Regex.Matches(s, @"^.*\d{7,8}.*$", RegexOptions.Compiled);
            var count = matches.Count;
            var list = new List<string>();
            for(int i = 0; i < count; i++)
            {
                list.Add(matches[i].Value);
            }
            return list;
        }

        public static bool Email_Get(this string s)
        {
            return Regex.IsMatch(s, @"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$", RegexOptions.Compiled);
        }

        public static bool Phone_Get(this string s)
        {
            return Regex.IsMatch(s, @"^\d{11}$");
        }
    }
}
