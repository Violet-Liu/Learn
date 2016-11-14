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

namespace QZ.Foundation.Document
{
    /// <summary>
    /// 文档读取器
    /// </summary>
    [Serializable]
    public struct HtmlReader
    {

        /// <summary>
        /// HTML源
        /// </summary>
        internal string htmlStr;

        /// <summary>
        /// 当前位置
        /// </summary>
        public int pos;

        /// <summary>
        /// 流长度
        /// </summary>
        internal int length;

        /// <summary>
        /// 创建一个HTMLReader
        /// </summary>
        /// <param name="htmlStr">HTML源</param>
        public HtmlReader(string htmlStr)
        {
            this.htmlStr = htmlStr;
            pos = 0;
            length = htmlStr.Length;
        }

        /// <summary>
        /// 转大写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal char ToUpper(char c)
        {

            return (c < 0x7b && c > 0x60) ? (char)(c - 0x20) : c;

        }

        /// <summary>
        /// 转小写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal char ToLower(char c)
        {

            return (c < 0x5b && c > 0x40) ? (char)(c + 0x20) : c;

        }

        /// <summary>
        /// 从HTML里复制一段字符
        /// </summary>
        /// <param name="startIndex">开始字符串</param>
        /// <param name="len">长度</param>
        /// <returns>字符串</returns>

        internal string GetString(int startIndex, int len)
        {
            return new string(htmlStr.ToCharArray(startIndex, len));
        }

        /// <summary>
        /// 从HTML某个位置开始复制到当前位置
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        internal string GetString(int startIndex)
        {
            return new string(htmlStr.ToCharArray(startIndex, pos - startIndex));
        }


        /// <summary>
        /// 得到一个大写String
        /// </summary>
        /// <param name="startIndex">开始字符串</param>
        /// <param name="len">结束字符串</param>
        /// <returns></returns>
        public string GetStringToUpper(int startIndex, int len)
        {

            char[] arr = new char[len];
            for (int i = 0; i < len; i++)
            {
                arr[i] = ToLower(htmlStr[startIndex + i]);
            }
            return new string(arr);
        }


        /// <summary>
        /// 从HTML中复制一段String
        /// </summary>
        /// <param name="startIndex">开始复制的位置</param>
        /// <returns></returns>
        public string GetStringToUpper(int startIndex)
        {
            return GetStringToUpper(startIndex, pos - startIndex);
        }


        /// <summary>
        /// 获取一段字符
        /// </summary>
        /// <param name="index">开始索引</param>
        /// <param name="len">获取长度</param>
        /// <returns>字符串</returns>
        internal string SpTag(int index, int len)
        {
            return index + len < this.length ? this.GetStringToUpper(index, len) : string.Empty;

        }

        /// <summary>
        /// 获取一段字符
        /// </summary>
        /// <param name="index">开始获取索引</param>
        /// <param name="len">获取长度</param>
        /// <returns>获取字符串</returns>
        internal string SpString(int index, int len)
        {
            return index + len < length ? this.GetString(index, len) : string.Empty;

        }

        /// <summary>
        /// 当前字符
        /// </summary>
        /// <returns></returns>
        public char CurrChar()
        {
            return htmlStr[pos];
        }

        /// <summary>
        /// 下一个字符
        /// </summary>
        /// <returns></returns>
        public char NextChar()
        {
            return this.NEofWithInc() ? this.htmlStr[pos + 1] : HtmlConst.CHEMPTY;

        }

        /// <summary>
        /// 是否结束
        /// </summary>
        /// <returns></returns>
        public bool NEof()
        {
            return pos < length;
        }



        /// <summary>
        /// 是否在下一个位置结束
        /// </summary>
        /// <returns></returns>
        public bool NEofWithInc()
        {
            return pos + 1 < length;
        }

        /// <summary>
        /// 比较一个索引位置是否比当前位置小
        /// </summary>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public bool GT(int startIndex)
        {
            return startIndex > -1 ? pos > startIndex : false;

        }

        /// <summary>
        /// 是否是标签开始的位置
        /// </summary>
        /// <returns></returns>
        public bool IsTagStartPos()
        {
            return htmlStr[pos] < 0x7B ? htmlStr[pos] == HtmlConst.CHLT && IsLetter(NextChar()) : false;
        }

        /// <summary>
        /// 是否是字母或者 '/'
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal bool IsLetter(char c)
        {
            return c == HtmlConst.CHCL || ((c < 0x7b && c > 0x60) || (c < 0x5b && c > 0x40)) || c == HtmlConst.CHT || c == HtmlConst.CHQ;
        }


        /// <summary>
        /// 是否在某个位置结束
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool NEofWith(int pos)
        {
            return pos < length;
        }

        /// <summary>
        /// 前进
        /// </summary>
        public void IncPos()
        {
            pos++;
        }

        /// <summary>
        /// 后退一步
        /// </summary>
        public void minPos()
        {
            pos--;
        }

        /// <summary>
        /// 前进指定步
        /// </summary>
        /// <param name="inc"></param>
        public void IncPos(int inc)
        {
            pos += inc;
        }

        /// <summary>
        /// 前进两位
        /// </summary>
        public void IncPosTwice()
        {
            pos += 2;
        }

    }
}
