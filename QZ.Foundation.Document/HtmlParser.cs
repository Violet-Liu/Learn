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
    /// HTML文档解析器，生成DOM树
    /// </summary>
    [Serializable]
    public class HtmlParser : IDisposable
    {
        /// <summary>
        /// 传参构造
        /// </summary>
        /// <param name="html">HTML代码</param>
        public HtmlParser(string html)
            : this()
        {
            _html = html;
        }

        /// <summary>
        /// 初始化Parser对象
        /// </summary>
        /// <param name="rmdic">要移除的标记名，请注意小写标记名</param>
        /// <param name="html">HTML源码</param>
        public HtmlParser(Dictionary<string, string> rmdic, string html)
        {
            _html = html;
            _rmTagDic = rmdic;
            _idList = new Dictionary<string, HtmlTag>();
        }

        /// <summary>
        /// 构造，不设置参数实例化本类
        /// </summary>
        public HtmlParser()
        {
            this.InitDic();
        }

        private bool _autoFixImportantTag = true;

        /// <summary>
        /// 设置是否自动修复重要标记
        /// 主要修复Body和根节点
        /// </summary>
        public bool AutoFixImportantTag
        {
            get { return _autoFixImportantTag; }
            set { _autoFixImportantTag = false; }
        }

        /// <summary>
        ///文档流对象 
        /// </summary>
        public HtmlReader reader;

        private string _html;
        /// <summary>
        /// 文档源码
        /// </summary>
        public string HTML
        {
            get { return _html; }
            set { _html = value; }
        }

        private HtmlTag _htmlElement;
        /// <summary>
        /// 文档根节点
        /// </summary>
        public HtmlTag HtmlElemnt
        {
            get { return _htmlElement; }
            set { _htmlElement = value; }
        }

        /// <summary>
        /// 文档标题内容
        /// </summary>
        public string Title
        {
            get { if (TitleTag == null) return string.Empty; return _titleTag.Value; }
        }

        private HtmlTag _titleTag;
        /// <summary>
        /// 文档标题标签对象
        /// </summary>
        public HtmlTag TitleTag
        {
            get
            {
                if (_titleTag == null)
                    _titleTag = QuickSearch("title");
                return _titleTag;
            }
        }

        private HtmlTag _body;
        /// <summary>
        /// 文档正文
        /// </summary>
        public HtmlTag Body
        {
            get
            {
                if (_body == null)
                    _body = GetBody();
                if (_body == null)
                    return _htmlElement;
                return _body;
            }
        }

        private HtmlTag _head;

        /// <summary>
        /// 文档头部
        /// </summary>
        public HtmlTag Head
        {
            get
            {
                if (_head == null)
                    _head = QuickSearch("head");
                return _head;
            }
        }

        private static readonly List<HtmlTag> _emptyHtmlTagList = new List<HtmlTag>();
        /// <summary>
        /// 返回一个空的HtmlTag集合
        /// </summary>
        internal List<HtmlTag> EmptyHtmlTagList
        {
            get
            {
                return _emptyHtmlTagList;
            }
        }

        /// <summary>
        /// 初始化字典
        /// </summary>
        private void InitDic()
        {
            _idList = new Dictionary<string, HtmlTag>();
            _rmTagDic = new Dictionary<string, string>();
        }

        /// <summary>
        /// 文档ID对象集合
        /// </summary>
        private Dictionary<string, HtmlTag> _idList;
        /// <summary>
        /// 移除标签集合
        /// </summary>
        private Dictionary<string, string> _rmTagDic;

        /// <summary>
        /// 移除标签集合
        /// </summary>
        public Dictionary<string, string> RmTagDic
        {
            get { return _rmTagDic; }
        }

        /// <summary>
        /// 设置为爬虫标准移除
        /// 移除标签为
        /// STYLE,SCRIPT,NOSCRIPT,IFRAME,EMBED,OBJECT,PARAM
        /// </summary>
        public void SpiderStandardRemove()
        {
            _rmTagDic.Add("style", string.Empty);
            _rmTagDic.Add("script", string.Empty);
            _rmTagDic.Add("noscript", string.Empty);
            _rmTagDic.Add("iframe", string.Empty);
            _rmTagDic.Add("embed", string.Empty);
            _rmTagDic.Add("object", string.Empty);
            _rmTagDic.Add("param", string.Empty);
        }

        /// <summary>
        /// 清除所有移除标记对象
        /// </summary>
        public void ClearRemoveDic()
        {
            _rmTagDic.Clear();
        }


        /// <summary>
        /// 添加内容移除标签
        /// </summary>
        /// <param name="tagName">小写标签名</param>
        public void AddRemoveTag(string tagName)
        {
            if (!_rmTagDic.ContainsKey(tagName))
                _rmTagDic.Add(tagName, string.Empty);
        }


        /// <summary>
        /// 将HTML源码扫描成HTML DOM
        /// </summary>
        public void Parse()
        {
            if (_html == null)
                throw new ArgumentNullException("HTML", "必须设置HTML源");
            reader = new HtmlReader(_html);
            HtmlTag tag = null;
            HtmlTag prev = null;
            int startIndex = -1;
            while (reader.NEof())
            {

                if (reader.IsTagStartPos())
                {
                    if (tag == null)
                        tag = prev;
                    else
                    {

                        if (reader.GT(startIndex))
                        {
                            tag.Value = reader.GetString(startIndex);
                            startIndex = -1;
                        }
                        else
                        {
                            tag.Value = string.Empty;
                            startIndex = -1;
                        }
                    }
                    prev = tag;
                    tag = new HtmlTag();
                    tag._prevNode = prev;
                    tag._idDic = _idList;
                    tag._document = this;
                    if (prev == null)
                        _htmlElement = tag;
                    else
                        prev._nextNode = tag;
                    tag.FormatterTag();
                    if (HtmlConst.IsNullOrEmpty(tag._tagName))
                        tag = null;
                    else
                    {
                        if (!tag.IsBackTag)
                        {
                            if (tag._tagName == HtmlConst.SCRIPT)
                            {
                                if (startIndex == -1)
                                {
                                    if (reader.NEofWithInc())
                                    {
                                        reader.IncPos();
                                        startIndex = reader.pos;
                                    }
                                }
                                ParseScript();

                            }
                        }
                        if (tag._tagName[0] == HtmlConst.CHT || _rmTagDic.ContainsKey(tag._tagName))
                        {
                            tag = null;
                            if (prev == null)
                                _htmlElement = null;
                            else
                                prev._nextNode = null;

                            startIndex = -1;
                        }

                    }

                }
                else
                {

                    if (tag != null)
                    {
                        if (startIndex == -1)
                            startIndex = reader.pos;
                    }
                }
                reader.IncPos();
                //tmp = c;
            }
            //如果结束标签不为空，修复部分重要标记
            if (tag != null && _autoFixImportantTag)
                tag.FixImportantTag();

        }

        /// <summary>
        /// 解析脚本
        /// </summary>
        private void ParseScript()
        {
            char c;
            bool starwiths = false;
            bool starwithd = false;
            bool isSuppend = false;
            do
            {
                c = reader.CurrChar();
                if (c == HtmlConst.CHTCL)
                {
                    reader.IncPosTwice();
                    continue;
                }
                else if (c == HtmlConst.CHS)
                {
                    if (!starwithd)
                    {
                        if (starwiths)
                        {
                            starwiths = false;
                            isSuppend = false;
                        }
                        else
                        {
                            starwiths = true;
                            isSuppend = true;
                        }
                    }
                }
                else if (c == HtmlConst.CHD)
                {
                    if (!starwiths)
                    {
                        if (starwithd)
                        {
                            starwithd = false;
                            isSuppend = false;
                        }
                        else
                        {
                            starwithd = true;
                            isSuppend = true;
                        }
                    }
                }

                if (isSuppend)
                {
                    reader.IncPos();
                    continue;
                }
                else
                {
                    if (c == HtmlConst.CHLT)
                    {
                        if (reader.NEofWithInc())
                        {
                            char cn = reader.NextChar();
                            if (cn == HtmlConst.CHCL)
                            {
                                if (reader.SpTag(reader.pos + 2, 6) == HtmlConst.SCRIPT)
                                {
                                    reader.minPos();
                                    return;
                                }
                            }
                            else if (cn == HtmlConst.CHT)
                            {
                                if (reader.SpString(reader.pos, 3) == HtmlConst.COMMENTSSTART)
                                {
                                    FormatterComments();
                                }
                            }

                        }
                        reader.IncPos();
                        continue;

                    }
                    else
                    {
                        if (c == HtmlConst.CHCL)
                        {
                            if (reader.NEofWithInc())
                            {
                                char n = reader.NextChar();
                                if (n == HtmlConst.CHCL)
                                {
                                    reader.IncPosTwice();
                                    ParseScriptLComments();
                                    continue;
                                }
                                else if (n == HtmlConst.CHA)
                                {
                                    reader.IncPosTwice();
                                    ParseScriptPComments();
                                }
                                else if (!isSuppend)
                                {
                                    //脚本的正则可以直接由斜杠开始到斜杠结束 如 /[<>']/
                                    reader.IncPos();
                                    FormatterRegex();
                                }
                            }
                        }
                        reader.IncPos();
                        continue;
                    }
                }

            } while (reader.NEof());

        }

        /// <summary>
        /// 格式化正则
        /// </summary>
        internal void FormatterRegex()
        {
            char c = HtmlConst.CHEMPTY;
            int pos = reader.pos - 1;
            //正则必须在一行，如果换行则必须欺骗回溯
            while (reader.NEof())
            {
                c = reader.CurrChar();
                switch (c)
                {
                    case HtmlConst.CHCL:
                        goto LABEL_CHECK;
                    case HtmlConst.CHTCL:
                        reader.IncPosTwice();
                        continue;

                    case HtmlConst.CHLB:
                    case HtmlConst.CHNL:
                        goto LABEL_TRICK;

                    default:
                        break;
                }
                reader.IncPos();
            }

            LABEL_CHECK:
            if (reader.NEofWithInc())
            {
                reader.IncPos();
                do
                {
                    c = reader.CurrChar();
                    if (HtmlConst.IsWhiteSpaceWithoutlineBreak(c) || (c == 'g' || c == 'i' || c == 'm'))
                    {
                        reader.IncPos();
                        continue;
                    }
                    break;
                } while (reader.NEof());
            }


            switch (c)
            {
                case '.':
                case ',':
                case '|':
                case '&':
                case '!':
                case '=':
                case '\r':
                case '\n':
                case ')':
                    return;
                case '<':
                    if (reader.NextChar() == '/')
                    {
                        reader.minPos();
                        return;
                    }
                    break;
                default:
                    break;
            }

            //欺骗回溯
            LABEL_TRICK:
            reader.pos = pos;
            return;


        }

        /*
         检查是否是空白符
         */
        internal void EatWhiteSpaceForRegex()
        {
            char c;
            while (reader.NEof())
            {
                c = reader.CurrChar();
                if (HtmlConst.IsWhiteSpaceWithoutlineBreak(c))
                {
                    reader.IncPos();
                    continue;
                }
                return;
            }
        }


        /// <summary>
        /// 格式化HTML注释 &lt;-- 注释 --&gt;
        /// </summary>
        internal void FormatterComments()
        {
            while (reader.NEof())
            {
                if (reader.CurrChar() == HtmlConst.CHM)
                {
                    if (reader.SpString(reader.pos, 3) == HtmlConst.COMMENTSEND)
                    {
                        reader.IncPosTwice();
                        return;
                    }
                }
                reader.IncPos();
            }
        }

        /// <summary>
        /// 格式化脚本行注释 //注释
        /// </summary>
        private void ParseScriptLComments()
        {
            while (reader.NEof())
            {
                switch (reader.CurrChar())
                {
                    case '\r':
                    case '\n':
                        reader.IncPos();
                        return;
                    case '<':
                        if (reader.NextChar() == HtmlConst.CHCL)
                        {
                            if (reader.SpTag(reader.pos + 2, 6) == HtmlConst.SCRIPT)
                            {
                                reader.minPos();
                                return;
                            }
                        }
                        break;
                }
                reader.IncPos();
            }
        }

        /// <summary>
        /// 格式化脚本段落注释 /*注释*/
        /// </summary>
        private void ParseScriptPComments()
        {
            while (reader.NEof())
            {
                if (reader.CurrChar() == HtmlConst.CHA)
                {

                    if (reader.NextChar() == HtmlConst.CHCL)
                    {
                        reader.IncPos();
                        break;
                    }

                }
                reader.IncPos();
            }
        }


        /// <summary>
        /// 查找一个特殊标记,遇到该标记就返回
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <returns>标签对象 找不到返回NULL</returns>
        private HtmlTag QuickSearch(string tagName)
        {
            HtmlTag tag = _htmlElement;
            while (tag != null)
            {
                if (tag._tagName == tagName)
                    return tag;
                tag = tag._nextNode;

            }
            return null;
        }

        /// <summary>
        /// 得到BODY
        /// </summary>
        /// <returns></returns>
        private HtmlTag GetBody()
        {
            HtmlTag tag = Head;
            if (tag == null)
            {
                return QuickSearch("body");
            }
            else
            {
                while (tag != null)
                {
                    if (tag._tagName == "body")
                        return tag;
                    tag = tag._nextNode;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过标签ID得到节点
        /// </summary>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public HtmlTag GetElementById(string id)
        {
            if (this._htmlElement != null)
            {
                return _htmlElement.GetElementById(id);
            }
            return null;
        }


        /// <summary>
        /// 重新解析文档
        /// 此方法只在文档中节点发生修改时调用
        /// 请不要频繁调用此方法，因为此方法会重组文档然后重新解析，文档越大，开销越大
        /// 如有很多节点需添加或删除，请在删除完所有或添加完所有节点后调用此方法
        /// 而不是删除一个节点调用一次，或增加一个节点调用一次
        /// </summary>
        public void ReParser()
        {

            if (_htmlElement == null)
                throw new Exception("根节点为空，不能重新解析！");
            _html = _htmlElement.ToString();
            Parse();

        }


        #region 静态方法

        /// <summary>
        /// 创建一个HTML标签对象
        /// </summary>
        /// <param name="elementTagName">HTML标签名</param>
        /// <returns>标签对象</returns>
        public static HtmlTag CreateElement(string elementTagName)
        {
            if (elementTagName == null)
                throw new ArgumentNullException("elementTagName", "标签名不能为NULL");
            if (elementTagName.Length == 0)
                throw new ArgumentOutOfRangeException("elementTagName", "标签名不能为空");
            return ParserForElement(CreateElementStr(elementTagName.ToUpper()));

        }

        /// <summary>
        /// 创建一个HTML标签String
        /// </summary>
        /// <param name="elementTagName"></param>
        /// <returns></returns>
        private static string CreateElementStr(string elementTagName)
        {
            if (HtmlConst.IsSelfBackTag(elementTagName))
                return string.Format("<{0} />", elementTagName);
            return string.Format("<{0}></{0}>", elementTagName);
        }

        /// <summary>
        /// 解析一个HTML标签
        /// </summary>
        /// <param name="elementStr"></param>
        /// <returns></returns>
        private static HtmlTag ParserForElement(string elementStr)
        {
            HtmlParser parser = new HtmlParser(elementStr);
            parser._autoFixImportantTag = false;
            parser.Parse();
            return parser.HtmlElemnt;
        }


        #endregion

        #region IDispose方法

        /// <summary>
        /// 终结器, 调用虚拟的Dispose方法
        /// </summary>
        ~HtmlParser()
        {
            Dispose(false);
        }

        /// <summary>
        /// 调用虚拟的Dispose方法, 禁止Finalization（终结操作）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 虚拟的Dispose方法
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
        }
        #endregion


    }
}
