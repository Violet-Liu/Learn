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
using System.Text.RegularExpressions;

namespace QZ.Foundation.Utility
{
    public class Judge
    {
        public static bool IsPhoneNum(string str)
        {
            var regex = new Regex("^[1][34578][0-9]{9}$", RegexOptions.Compiled);
            var match = regex.Match(str);
            return match.Success;
        }

        public static bool IsEmail(string email) => Regex.IsMatch(email, @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$");
        
    }
}
