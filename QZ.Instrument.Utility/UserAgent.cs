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
    /// <summary>
    /// 浏览器代理类
    /// </summary>
    public class UserAgent
    {

        /// <summary>
        /// 浏览器代理STRING
        /// </summary>
        public string rawUserAgentString;

        /// <summary>
        /// 实例化浏览器代理类
        /// </summary>
        public UserAgent(string rawUserAgentString)
        {
            this.rawUserAgentString = rawUserAgentString;
            Parse(this.rawUserAgentString, this);
            //UserAgentCache.InsertCache(rawUserAgentString,this);
        }

        /// <summary>
        /// 根键
        /// </summary>
        public KeyItem root;


        /// <summary>
        /// 可能是邮箱的键项 SP=@
        /// </summary>
        public KeyItem MayEmailItem;

        /// <summary>
        /// 可能是蜘蛛的项 SP=+
        /// </summary>
        public KeyItem MaySpiderItem;


        /// <summary>
        /// 浏览器项
        /// </summary>
        public KeyItem BrowserItem;




        /// <summary>
        /// 浏览器名称
        /// </summary>
        public string Browser;


        /// <summary>
        /// 平台
        /// </summary>
        public string Platform;

        /// <summary>
        /// 临时平台
        /// </summary>
        private string tempPlatform;


        /// <summary>
        /// 临时浏览器，比如Chrome 很多用这个内核 IE等
        /// </summary>
        private string tempBrowser;


        /// <summary>
        /// 获取平台
        /// </summary>
        /// <returns></returns>
        public string GetPlatform()
        {
            if (Platform != null)
                return Platform;
            if (tempPlatform != null)
                return tempPlatform;
            if (root != null)
            {
                if (root.key != null)
                {
                    Platform = root.key;
                    return Platform;
                }
            }
            Platform = "unknow platform";
            return Platform;
        }

        /// <summary>
        /// 获取浏览器
        /// </summary>
        /// <returns></returns>
        public string GetBrowser()
        {
            if (Browser != null)
                return Browser;
            if (tempBrowser != null)
            {

                return tempBrowser;
            }

            if (root != null && root.next != null && root.next.key != null)
            {
                Browser = root.next.key;
                return Browser;
            }

            Browser = "unknow browser";
            return Browser;
        }

        /// <summary>
        /// 获取Spider
        /// </summary>
        /// <returns></returns>
        public KeyItem Spider()
        {
            if (MaySpiderItem != null)
                return MaySpiderItem;
            //if (MayEmailItem != null)
            //{
            //    //if (MayEmailItem.key != null && MayEmailItem.key.Length > 0)
            //    //{
            //    //    //if (MayEmailItem.key[0] != MayEmailItem.SpecialSymbol)
            //    //    //    return MayEmailItem;
            //    //    return 
            //    //}
            //    return MayEmailItem;
            //}
            return null;
        }



        /// <summary>
        /// 执行解析
        /// </summary>
        /// <param name="rawUserAgentString"></param>
        /// <param name="ua"></param>
        public static unsafe void Parse(string rawUserAgentString, UserAgent ua)
        {
            if (rawUserAgentString == null || rawUserAgentString.Length == 0)
                return;

            int len = rawUserAgentString.Length;
            int i = 0;
            int startIdx = -1;
            //int endIdx = -1;
            //int endIdx;
            int verStartIdx = -1;

            fixed (char* cha = rawUserAgentString)
            {
                char c;
                KeyItem item = null;
                KeyItem prev = null;
                while (i < len)
                {
                    c = cha[i];
                    #region switch
                    switch (c)
                    {

                        #region VerStartIndex
                        case '/':
                        case ':':

                            //版本号开始
                            i++;
                            verStartIdx = i;
                            continue;
                        #endregion

                        case ' ':

                            if (startIdx < 0)   // 开始索引小于0，表示还未开始，跳转至default处理
                                goto default;

                            if (verStartIdx > 0)    // 遇到版本号后的一个空格，跳转并进行版本号提取
                                goto LABLE_10000;

                            if (i == startIdx)
                                break;

                            if (IsNextDigital(cha, len, i))     // 如果是数字，也表示是verstartindex
                                verStartIdx = i + 1;
                            break;
                        //case '.':
                        //    if (verStartIdx > 0)
                        //        break;
                        //    i++;
                        //    verStartIdx = i;
                        //    continue;

                        //case '@':
                        //    if (item != null)
                        //    {
                        //        item.SpecialSymbol = c;
                        //        ua.MayEmailItem = item;
                        //    }
                        //    break;

                        //case '+':
                        //case '!':

                        //    if (ua.MaySpiderItem != null)
                        //        break;

                        //    if (item != null) 
                        //    {
                        //        item.SpecialSymbol = c;
                        //        ua.MaySpiderItem = item;
                        //    }

                        //    break;


                        case '(':
                        case ';':
                        case ')':
                        case ',':
                            //键,开始，旧键的结束

                            //int j=EatWhitespace(cha,len,i);
                            #region Label_1000，解析键和值
                            LABLE_10000:

                            if (startIdx < 0)
                            {
                                //item = null;
                                //startIdx = i+1;
                                //goto default;
                                break;
                            }
                            //if (startIdx + 1 == i)
                            //{
                            //    //startIdx = i + 1;
                            //    break;
                            //    //break;

                            //}


                            //endIdx = i;
                            if (item != null)
                            {

                                if (verStartIdx > -1)   // 已经定位出版本号起始位置
                                {
                                    item.key = GetString(cha, startIdx, verStartIdx - 1); // 获取当前解析出来的键
                                    item.ver = GetString(cha, verStartIdx, i);          // 获取当前解析出来的值
                                    verStartIdx = -1;   // 复位
                                    startIdx = -1;      // 复位
                                }
                                else
                                {
                                    item.key = GetString(cha, startIdx, i);

                                    startIdx = -1;
                                }
                                prev = item;
                                item = null;
                                //endIdx=-1'
                            }
                            #endregion



                            break;
                        case 'C':
                        case 'c':
                            if (ua.MaySpiderItem != null)
                                goto default;
                            //may spider

                            if (item != null && MatchStr(cha, i, len, "crawler"))
                            {
                                ua.MaySpiderItem = item;
                                item.SpecialSymbol = 'c';
                                i += 7;
                                continue;
                            }
                            goto default;

                        case 'S':
                        case 's':

                            if (ua.MaySpiderItem != null)
                                goto default;
                            //may spider

                            if (item != null && MatchStr(cha, i, len, "spider"))
                            {
                                ua.MaySpiderItem = item;
                                item.SpecialSymbol = 's';
                                i += 6;
                                continue;
                            }



                            goto default;
                        case 'B':
                        case 'b':
                            //may spider

                            if (ua.MaySpiderItem != null)
                                goto default;

                            if (item != null)
                            {

                                if (MatchStr(cha, i, len, "bot"))
                                {
                                    ua.MaySpiderItem = item;
                                    item.SpecialSymbol = 'b';
                                    i += 3;
                                    continue;
                                }
                                else if (MatchStr(cha, i, len, "browser"))
                                {
                                    ua.BrowserItem = item;
                                    item.SpecialSymbol = 'b';
                                    i += 7;
                                    continue;
                                }
                            }

                            goto default;

                        case 'T':
                            if (ua.MaySpiderItem != null)
                                goto default;

                            if (item != null)
                            {
                                if (MatchStr(cha, i, len, "Transcoder"))
                                {
                                    ua.MaySpiderItem = item;
                                    item.SpecialSymbol = 't';
                                    i += 10;
                                    continue;
                                }
                            }

                            goto default;


                        case 'Y':
                        case 'y':

                            if (ua.MaySpiderItem != null)
                                goto default;

                            if (item != null)
                            {

                                if (MatchStr(cha, i, len, "yahoo!"))
                                {
                                    ua.MaySpiderItem = item;
                                    item.SpecialSymbol = 'y';
                                    i += 6;
                                    continue;
                                }
                            }

                            goto default;

                        case 'M':

                            if (ua.BrowserItem != null)
                                goto default;

                            if (item != null && MatchStr(cha, i, len, "MSIE"))
                            {
                                ua.BrowserItem = item;
                                item.SpecialSymbol = 'm';
                                i += 4;
                                continue;

                            }

                            goto default;
                        default:
                            //i= EatWhitespace(cha,len,i);

                            if (item == null)
                            {
                                item = new KeyItem();
                                if (prev == null)
                                    prev = item;
                                else
                                    prev.next = item;

                                if (ua.root == null)
                                    ua.root = prev;

                                if (startIdx < 0)
                                {
                                    startIdx = EatWhitespace(cha, len, i);
                                    if (startIdx != i)
                                    {
                                        i = startIdx;
                                        continue;
                                    }
                                }

                            }


                            break;

                    }
                    #endregion

                    i++;
                }



                if (item != null)
                {

                    if (verStartIdx > -1)
                    {
                        item.key = GetString(cha, startIdx, verStartIdx - 1);
                        item.ver = GetString(cha, verStartIdx, i);
                        //verStartIdx = -1;
                        //startIdx = -1;
                    }
                    else
                    {
                        item.key = GetString(cha, startIdx, i);
                        //startIdx = -1;
                    }
                    //prev = item;
                    //item = null;
                    //endIdx=-1'
                }


            }


        }


        /// <summary>
        /// 从某一个点开始匹配一个字符串
        /// </summary>
        /// <param name="cha">原始字符串指针</param>
        /// <param name="idx">开始检查的位置</param>
        /// <param name="len">原始字符串长度</param>
        /// <param name="str2Match">要匹配的字符串</param>
        /// <returns>是否匹配</returns>
        public static unsafe bool MatchStr(char* cha, int idx, int len, string str2Match)
        {
            //int j = 1;
            int len2Match = str2Match.Length;
            fixed (char* matchStr = str2Match)
            {
                for (int i = 1; i < len2Match; i++)
                {
                    if (matchStr[i] != cha[idx + i])
                        return false;

                }
                return true;
            }


        }


        /// <summary>
        /// 下一个字符是否是数字
        /// </summary>
        /// <param name="cha"></param>
        /// <param name="len"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static unsafe bool IsNextDigital(char* cha, int len, int i)
        {
            if (i + 1 < len)
            {
                int j = cha[i + 1];
                return j > 47 && j < 58;
            }
            return false;
        }


        /// <summary>
        /// 获取一段内容
        /// </summary>
        /// <param name="cha"></param>
        /// <param name="startIdx"></param>
        /// <param name="endIdx"></param>
        /// <returns></returns>
        public static unsafe string GetString(char* cha, int startIdx, int endIdx)
        {
            return Util.To_String_X(cha, startIdx, endIdx);

            //char[] arr=;

            //fixed (char* newArr = new char[endIdx - startIdx])
            //{
            //    int j = 0;
            //    for (int i = startIdx; i < endIdx; i++)
            //    {
            //        newArr[j] = cha[i];
            //        j++;
            //    }
            //    return new string(newArr);
            //}

        }

        /// <summary>
        /// 吞掉键开始的空格
        /// </summary>
        /// <param name="cha"></param>
        /// <param name="len"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static unsafe int EatWhitespace(char* cha, int len, int i)
        {
            //i = i + 1;
            //i = i + 1;

            while (i < len)
            {
                char c = cha[i];
                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '\f':
                    case '　':
                        i++;
                        continue;
                    default:
                        return i;

                }


            }

            return i;
        }


        /// <summary>
        /// 扫描一遍节点,检查平台和浏览器
        /// </summary>
        /// <param name="ua"></param>
        public static void Scan(UserAgent ua)
        {

            KeyItem root = ua.root;
            while (root != null)
            {

                if (ua.Platform == null)
                    KeyItemCheck4Plantform(root, ua);

                if (ua.Browser == null)
                    KeyItemCheck4Browser(root, ua);

                root = root.next;

            }

        }


        /// <summary>
        /// 检查平台
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ua"></param>
        public static void KeyItemCheck4Plantform(KeyItem item, UserAgent ua)
        {
            switch (item.key)
            {
                case "Windows NT":
                    switch (item.ver)
                    {
                        case "10.0":
                        case "6.4":
                            ua.Platform = "Windows 10";
                            break;
                        case "6.3":
                            ua.Platform = "Windows 8.1/Server 2012 R2";
                            break;
                        case "6.2":
                            ua.Platform = "Windows 8/Server 2012";
                            break;
                        case "6.1":
                            ua.Platform = "Windows 7/Server 2008 R2";
                            break;
                        case "6.0":
                            ua.Platform = "Windows Vista/Server 2008";
                            break;
                        case "5.2":
                            ua.Platform = "Windows Server 2003";
                            break;
                        case "5.1":
                            ua.Platform = "Windows xp";
                            break;
                        case "5.0":
                            ua.Platform = "Windows 2000";
                            break;
                        default:
                            ua.Platform = item.ToString();
                            break;
                    }
                    break;
                case "Windows RT":
                    ua.Platform = item.ToString();
                    break;
                case "Windows ME":
                    ua.Platform = "Windows ME";
                    break;
                case "Windows":
                    if (item.ver == "98")
                        ua.Platform = "Windows 98";
                    else
                        ua.tempPlatform = "Windows";
                    break;
                case "iPad":
                    ua.Platform = "iPad";
                    break;

                case "Macintosh":
                    ua.Platform = "MAC";
                    break;

                case "iphone":
                case "iPhone":
                    ua.Platform = "iPhone";
                    break;
                case "iPod":
                    ua.Platform = "iPod";
                    break;
                case "Android":
                    ua.Platform = "Android";
                    break;
                case "X11":
                    ua.tempPlatform = item.next != null ? item.next.key : "X11";
                    break;
                case "Linux":
                case "Linux x86_64":
                    ua.tempPlatform = "Linux";
                    break;

                case "BlackBerry":
                    ua.Platform = "BlackBerry";
                    break;
                case "hpwOS":
                    ua.Platform = "Web OS";
                    break;
                case "SymbianOS":
                    ua.Platform = "Symbian";
                    break;
                //case "Windows Phone OS":
                //    ua.Platform = "Windows Phone";
                //    break;
                case "Windows Phone":
                    ua.Platform = "Windows Phone";
                    break;
                case "SunOS":
                    ua.Platform = "SunOS";
                    break;
                case "Unix":
                    ua.Platform = "Unix";
                    break;
                case "FreeBSD":
                    ua.Platform = "FreeBSD";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 检查浏览器
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ua"></param>
        public static void KeyItemCheck4Browser(KeyItem item, UserAgent ua)
        {
            if (ua.BrowserItem != null && ua.tempBrowser == null)
            {
                if (ua.BrowserItem.key == "MSIE")
                    ua.tempBrowser = ua.BrowserItem.ToString();
                else
                    ua.Browser = ua.BrowserItem.ToString();
                return;
            }

            switch (item.key)
            {

                case "Chrome":
                    ua.tempBrowser = item.ToString();
                    break;
                case "Firefox":
                    ua.Browser = item.ToString();
                    break;
                case "360SE":
                    ua.Browser = item.ToString();
                    break;
                case "SE":
                    ua.Browser = string.Format("Sogou Browser:{0}", item.ver);
                    break;
                case "Maxthon":
                    ua.Browser = item.ToString();
                    break;
                case "TencentTraveler":
                    ua.Browser = item.ToString();
                    break;
                case "The World":
                    ua.Browser = item.ToString();
                    break;
                case "LBBROWSER":
                    ua.Browser = item.ToString();
                    break;
                case "Avant":
                    ua.Browser = item.ToString();
                    break;
                case "Opera":
                    ua.Browser = item.ToString();
                    break;
                case "UCWEB":
                    ua.Browser = item.ToString();
                    break;
                case "AppleWebKit":
                    ua.tempBrowser = item.key;
                    break;
                case "Edge":
                    if (item.next == null && item.ver != null)
                    {
                        ua.Browser = string.Format("Microsoft Edge:{0}", item.ver);
                    }
                    break;



                case "Version":
                    if (ua.Platform == "Android")
                    {
                        ua.Browser = string.Format("Android Browser:{0}", item.ver);
                        break;
                    }
                    if (ua.tempBrowser == "AppleWebKit")
                        ua.Browser = string.Format("{0}:{1}", ua.tempBrowser, item.ver);

                    break;
                case "rv":
                    if (CheckItem(ua, "Trident", "7.0"))
                    {
                        ua.Browser = string.Format("MSIE:{0}", item.ver);
                    }
                    break;


            }



        }

        /// <summary>
        /// 检查是否有匹配的键项
        /// </summary>
        /// <param name="ua"></param>
        /// <param name="key"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        public static bool CheckItem(UserAgent ua, string key, string ver)
        {
            KeyItem root = ua.root;
            while (root != null)
            {
                if (root.key == key && root.ver == ver)
                    return true;
                root = root.next;
            }
            return false;
        }




        /// <summary>
        /// 键的单项链表
        /// </summary>
        public class KeyItem
        {
            /// <summary>
            /// 下一个
            /// </summary>
            public KeyItem next;

            /// <summary>
            /// 键值
            /// </summary>
            public string key;

            /// <summary>
            /// 版本
            /// </summary>
            public string ver;

            /// <summary>
            /// 特殊标志字符
            /// </summary>
            public char SpecialSymbol;

            /// <summary>
            /// 键对象
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                if (ver != null)
                {
                    return string.Format("{0}:{1}", key, ver);
                }
                return key;
                //return base.ToString();
            }


        }



    }
}
