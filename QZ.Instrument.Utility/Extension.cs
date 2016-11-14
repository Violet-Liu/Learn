using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using fastJSON;
using QZ.Foundation.Monad;

namespace QZ.Instrument.Utility
{
    public static class Extension
    {

        #region to maybe
        public static Maybe<T> ToMaybe<T>(this T t)
        {
            return t;
        }
        #endregion

        #region to either
        public static Either<L, R> ToLeft<L, R>(this L l)
        {
            return Either<L, R>.FromLeft(l);
        }
        public static Either<L, R> ToRight<L, R>(this R r)
        {
            return Either<L, R>.FromRight(r);
        }
        #endregion

        #region to json
        public static string SerializeToJson_F<T>(this T t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }

        /// <summary>
        /// Deserialize from json string to T type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeFromJson_F<T>(this string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
            
        }
        #endregion

        #region json serizlization
        public static string SerializeToJson<T>(this T t)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, t);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        public static T DeserializeFromJson<T>(this string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
            
        }
        public static T ToObject<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        #endregion

        #region xml serialization
        /// <summary>
        /// Deserizlize from xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(this string path)
        {
            if(File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
            return default(T);
        }
        #endregion

        #region to safe sql string
        public static unsafe string To_Sql_Safe(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            int len = input.Length;
            char[] dest = new char[len * 4];
            int i = 0;
            int j = 0;

            fixed(char* srcptr = input)
            {
                fixed(char* destptr = dest)
                {
                    char c;
                    while(i < len)
                    {
                        c = srcptr[i];
                        switch(c)
                        {

                            case '\'':
                                destptr[j++] = '\'';
                                destptr[j++] = '\'';
                                break;
                            case '<':
                                destptr[j++] = '&';
                                destptr[j++] = 'l';
                                destptr[j++] = 't';
                                destptr[j++] = ';';
                                break;
                            case '>':
                                destptr[j++] = '&';
                                destptr[j++] = 'g';
                                destptr[j++] = 't';
                                destptr[j++] = ';';
                                break;
                            default:
                                destptr[j++] = c;

                                break;
                        }
                        i++;
                    }
                }
                return new string(dest, 0, j);
            }
        }

        public static unsafe string De_Sql_Safe(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            int len = s.Length;
            char[] des = new char[len];
            int i = 0;
            int j = 0;

            fixed (char* sptr = s)
            {
                fixed (char* dptr = des)
                {
                    char c = '\0';
                    while (i < len)
                    {
                        c = sptr[i];
                        switch (c)
                        {
                            case '&':

                                if (sptr[i + 1] == 'l' && sptr[i + 2] == 't' && sptr[i + 3] == ';')
                                {
                                    dptr[j++] = '<';
                                    i += 3;
                                }
                                else if (sptr[i + 1] == 'g' && sptr[i + 2] == 't' && sptr[i + 3] == ';')
                                {
                                    dptr[j++] = '>';
                                    i += 3;
                                }
                                else
                                {
                                    dptr[j++] = c;
                                }
                                break;
                            case '\'':
                                if (sptr[i + 1] == '\'')
                                {
                                    dptr[j++] = '\'';
                                    i++;
                                }
                                else
                                {
                                    dptr[j++] = c;
                                }
                                break;
                            default:
                                dptr[j++] = c;
                                break;
                        }
                        i++;
                    }
                }
                return new string(des, 0, j);
            }
        }
        #endregion

        #region to json with fastJson
        /// <summary>
        /// 静态json设定
        /// </summary>
        static JSONParameters jsp = new JSONParameters() { EnableAnonymousTypes = false, UseEscapedUnicode = false, UseOptimizedDatasetSchema = false, UsingGlobalTypes = false, UseExtensions = false };
        /// <summary>
        /// using fastJson
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToJson_Fast<T>(this T t)
        {
            return JSON.ToJSON(t, jsp);
        }

        public static T ToObject_Fast<T>(this string input)
        {
            return JSON.ToObject<T>(input, jsp);
        }
        #endregion

        #region extensional action 
        public static string DoExt(this string s, Action<string> action)
        {
            action(s);
            return s;
        }
        #endregion

        #region to | from gzip
        public static string ToGzip(this string input) => Convert.ToBase64String(Cipher_Gzip.Compress(Encoding.UTF8.GetBytes(input)));

        public static string DeGzip(this string input) => Encoding.UTF8.GetString(Cipher_Gzip.Decompress(Convert.FromBase64String(input)));
        #endregion


        public static string ToPure(this string sTxt)
        {

            string str = "a片|操你|操她|我日|黄片|我靠|管理员|共产党|温家宝|江泽民|胡锦涛|毛泽东|系统|傻逼|妈B|妈逼|犯贱|日你|我操|我草";
            str += "妈个B|操你妈|妈个比|妈个逼|我操你娘|贱货|骚货|你娘的|操你娘|卖淫|傻逼|FUCK|干你娘|贱B|傻B|强奸|迷奸|操你们妈|草你们妈|卖B|妈B|妈逼";
            str += "妓女|一夜情|做爱|shit|毛片|去死|搞死|操死|奸死|日死|www|com|net|cn|bijint|抄袭小日本的网站|口爆|bitch|sb|肏|吐|裸体|口交|丑B|鸡8|法轮功";
            Regex reg = new Regex(str, RegexOptions.IgnoreCase);

            string r = reg.Replace(sTxt, "**");
            return r;
        }

        /// <summary>
        /// private to decrypte oc_code
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string To_Occode(this string input, string key)
        {
            return Encoding.UTF8.GetString(Cipher_Aes.DecryptToBytes(Util.DecodeHexEncodingString(input), key));
        }


        public static string ToSafetyStr(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(str, "[';\"\\r\\n]+", string.Empty), "CHAR<\\d+>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 把HTML代码转换为Text，"\r"转换为<![CDATA[<br>]]>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToText(this string s)
        {
            if ((s != null) && (s.Length > 0))
            {
                s = s.Replace("<", "&lt;");
                s = s.Replace(">", "&gt;");
                s = s.Replace("\r\n", "<br/>");
                s = s.Replace("\r", "<br/>");
                s = s.Replace("\n", "<br/>");
            }
            return s;
        }

        
    }
}
