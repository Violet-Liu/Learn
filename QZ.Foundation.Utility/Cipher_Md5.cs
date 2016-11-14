/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * MD5 encryption helper 
 *
 *
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace QZ.Foundation.Utility
{
    public class Cipher_Md5
    {
        public static string CreateSalt()
        {
            byte[] salt = new byte[8];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string Encrypt(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(CreateSalt() + input)));
        }

        public static string Encrypt16(string input)
        {
            return null;
        }

        /// <summary>
        /// 16 bit MD5 encryption
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>encrypted string</returns>
        public static string Md5_16(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(input))).Replace("-", "").ToLower().Substring(8, 16);
        }

        /// <summary>
        /// 16 bit MD5 encryption. Get the continuous substring from index 9
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5_16_1(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(input))).Replace("-", "").ToLower().Substring(9, 16);
        }
    }
}
