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
    /// 样式解析器
    /// </summary>
    public class StyleParser
    {

        private Dictionary<string, string> _dic;
        private HtmlTag _tag;

        /// <summary>
        /// 实例化一个解析器
        /// </summary>
        public StyleParser()
        {
            this._dic = new Dictionary<string, string>();
        }

        /// <summary>
        /// 实例化一个样式解析器
        /// </summary>
        /// <param name="tag"></param>
        public StyleParser(HtmlTag tag) : this()
        {
            this._tag = tag;
        }

        /// <summary>
        /// 样式索引器,不存在该样式 返回空字符串
        /// </summary>
        /// <param name="key">样式名</param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (_dic != null && _dic.ContainsKey(key))
                    return _dic[key].Trim();
                return string.Empty;
            }
        }

        /// <summary>
        /// 样式集合
        /// </summary>
        public Dictionary<string, string> StyleDic
        {
            get { return _dic; }
        }

        /// <summary>
        /// 解析一个样式标签
        /// </summary>
        public Dictionary<string, string> ParseTagStyle()
        {

            this.ExtendStyleFromParent();
            this.TagNameCheck();

            if (InternalCheck(this._tag))
                return _dic;
            this.TagStyleCheck();
            if (!this._tag.AttributeDic.ContainsKey("style"))
                return _dic;
            ParseStyleString(this._tag.GetAttribute("style"));
            return _dic;
        }

        /// <summary>
        /// 解析样式
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public Dictionary<string, string> ParseStyleString(string style)
        {
            int i = 0;
            char c;
            string key;
            string val;
            int valStartIndex = 0;
            int keyStartIndex = -1;
            while (i < style.Length)
            {
                c = style[i];
                if (c == ':')
                {
                    if (keyStartIndex == -1)
                    {
                        i++;
                        continue;
                    }
                    //key = new string(style.ToCharArray(keyStartIndex, i - keyStartIndex));
                    key = this.GetStringToLower(style, keyStartIndex, i - keyStartIndex);
                    i++;
                    while (i < style.Length)
                    {
                        if (!HtmlConst.IsWhiteSpace(style[i]))
                            break;
                        i++;
                    }
                    int j = i;
                    valStartIndex = i;
                    while (j < style.Length)
                    {
                        if (style[j] == ';')
                        {
                            if (j - i > 0)
                            {
                                //val = new string(style.ToCharArray(i, j - i));
                                val = this.GetStringToLower(style, i, j - i);
                                Insert(key, val);
                            }
                            valStartIndex = 0;
                            keyStartIndex = -1;
                            i = j;
                            break;
                        }
                        j++;
                    }
                    if (valStartIndex > 0)
                    {
                        if (j - i > 0)
                        {
                            //val = new string(style.ToCharArray(i, j - i));
                            val = this.GetStringToLower(style, i, j - i);
                            Insert(key, val);
                        }
                        valStartIndex = 0;
                        keyStartIndex = -1;
                        i = j;
                        break;
                    }

                }
                else
                    if (keyStartIndex == -1 && valStartIndex == 0)
                {
                    if (!HtmlConst.IsWhiteSpace(c))
                        keyStartIndex = i;
                }
                i++;

            }
            return this._dic;
        }


        /// <summary>
        /// 检查某个标签内部的样式
        /// </summary>
        private void TagStyleCheck()
        {
            if (this._tag.AttributeDic.ContainsKey("width"))
                Insert("width", _tag.AttributeDic["width"]);
            if (this._tag.AttributeDic.ContainsKey("height"))
                Insert("height", _tag.AttributeDic["height"]);
            if (this._tag.AttributeDic.ContainsKey("align"))
                Insert("text-align", _tag.AttributeDic["align"]);
            if (this._tag.AttributeDic.ContainsKey("vlign"))
                Insert("vlign", _tag.AttributeDic["vlign"]);
            if (this._tag.AttributeDic.ContainsKey("color"))
                Insert("color", _tag.AttributeDic["color"]);
            if (this._tag.AttributeDic.ContainsKey("size"))
                Insert("font-size", _tag.AttributeDic["size"]);

        }

        /// <summary>
        /// 根据功能性标题设置样式
        /// </summary>
        private void TagNameCheck()
        {
            switch (this._tag.TagName)
            {
                case "strong":
                case "b":
                    this.SetBlod();
                    break;
                case "h1":
                    this.SetFontSize("22pt");
                    this.SetBlod();
                    break;
                case "h2":
                    this.SetFontSize("18pt");
                    this.SetBlod();
                    break;
                case "h3":
                    this.SetFontSize("13.5pt");
                    this.SetBlod();
                    break;
                case "em":
                case "i":
                case "address":
                    this.SetFontItalic();
                    break;
                case "u":
                    this.Insert("text-decoration", "underline");
                    break;
            }
        }

        /// <summary>
        /// 设置为加粗
        /// </summary>
        private void SetBlod()
        {
            this.Insert("font-weight", "blod");
        }

        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="size"></param>
        private void SetFontSize(string size)
        {
            this.Insert("font-size", size);
        }

        /// <summary>
        /// 设置为斜体
        /// </summary>
        private void SetFontItalic()
        {
            this.Insert("font-style", "italic");
        }

        /// <summary>
        /// 向字典里插入一个样式信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public void Insert(string key, string val)
        {
            if (_dic.ContainsKey(key))
                _dic[key] = val;
            else
                _dic.Add(key, val);
        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="lst"></param>
        public void Remove(IEnumerable<string> lst)
        {
            if (_dic == null || _dic.Count < 1)
                return;

            foreach (string key in lst)
            {
                _dic.Remove(key);
            }

        }

        /// <summary>
        /// 移除属性
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _dic.Remove(key);
        }

        /// <summary>
        /// 移除所有属性但保留
        /// </summary>
        /// <param name="keys">要保留的样式键值</param>
        public void RemoveButKeep(List<string> keys)
        {
            if (_dic == null || _dic.Count < 1)
                return;

            IEnumerable<string> dicKey = _dic.Keys.ToArray();
            foreach (string key in dicKey)
            {
                if (!keys.Contains(key))
                {
                    _dic.Remove(key);
                }
            }
        }



        /// <summary>
        /// 检查标签是否不存在样式
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private bool InternalCheck(HtmlTag tag)
        {
            return tag.AttributeDic == null;
            //if (tag.AttributeDic == null)
            //    return true;
            //if (tag.AttributeDic.ContainsKey("STYLE"))
            //    return false;
            //return true;

        }

        /// <summary>
        /// 检查是否有父节点
        /// </summary>
        /// <returns></returns>
        private bool InternalCheckParent()
        {
            return this._tag.ParentNode == null;
        }

        /// <summary>
        /// 继承来自父类的样式
        /// </summary>
        private void ExtendStyleFromParent()
        {
            if (this.InternalCheckParent())
                return;
            CombineStylesFromParent(this._tag.ParentNode.Style.StyleDic);
        }

        /// <summary>
        /// 合并父节点的样式
        /// </summary>
        /// <param name="pstyles"></param>
        private void CombineStylesFromParent(Dictionary<string, string> pstyles)
        {
            foreach (string pkey in pstyles.Keys)
            {
                this.Insert(pkey, pstyles[pkey]);
            }
        }

        /// <summary>
        /// 得到一个小写String
        /// </summary>
        /// <param name="input">字符串</param>
        /// <param name="start">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns>小写String</returns>
        private string GetStringToLower(string input, int start, int length)
        {
            char[] arr = input.ToCharArray(start, length);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = HtmlConst.ToLower(arr[i]);
            }
            return new string(arr);
        }

        /// <summary>
        /// tostring
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_dic != null && _dic.Count > 0)
            {
                StringBuilder sb = new StringBuilder(_dic.Count * 10);
                foreach (string key in _dic.Keys)
                {
                    sb.AppendFormat("{0}:{1};", key, _dic[key]);
                }
                sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
            return string.Empty;
            //return base.ToString();
        }
    }
}
