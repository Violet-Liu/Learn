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
    /// HTML标签对象
    /// </summary>
    [Serializable]
    public class HtmlTag
    {

        #region 属性


        internal HtmlParser _document;
        /// <summary>
        /// HtmlParser 文档对象
        /// </summary>
        public HtmlParser Document
        {
            get { return _document; }
            internal set { _document = value; }
        }

        internal string _tagName;
        /// <summary>
        /// 标签名
        /// </summary>
        public string TagName
        {
            get { return _tagName; }
            set
            {
                _tagName = value;
                if (!_isBakcTag)
                {
                    if (_backTag != null)
                        _backTag._tagName = value;
                }
            }

        }

        private int _startIndex;
        /// <summary>
        /// 开始索引
        /// </summary>
        public int StartIndex
        {
            get { return _startIndex; }
            internal set { _startIndex = value; }
        }

        private int _endIndex;
        /// <summary>
        /// 结束索引
        /// </summary>
        public int EndIndex
        {
            get { return _endIndex; }
            internal set { _endIndex = value; }
        }

        private bool _isBakcTag;
        /// <summary>
        /// 是否是结束标签
        /// </summary>
        public bool IsBackTag
        {
            get { return _isBakcTag; }
            internal set { _isBakcTag = value; }
        }

        private string _tagAttributeStr;
        /// <summary>
        /// 属性字符串
        /// 将属性集合按照HTML的方式返回如：
        ///  HREF="http://www.xxx.com" ID="XXX"
        /// </summary>
        public string TagAttributeStr
        {
            get
            {
                _tagAttributeStr = GetAttributeStr(this);
                return _tagAttributeStr;
            }
        }

        /// <summary>
        /// 检查是否有属性
        /// </summary>
        public bool HasAttribute
        {
            get
            {
                if (_attrDic == null)
                    return false;
                return _attrDic.Count == 0;
            }
        }


        private Dictionary<string, string> _attrDic;
        /// <summary>
        /// 属性字典
        /// 当一个标签没有属性时返回NULL
        /// </summary>
        public Dictionary<string, string> AttributeDic
        {
            get { return _attrDic; }

        }

        private string _innerHtml;
        /// <summary>
        /// 获取此节点下的HTML，直接从文档中获取，性能高
        /// 要获取更改的HTML请直接调用InnerHTMLRebuild属性
        /// </summary>
        public string InnerHTML
        {
            get
            {
                if (_innerHtml == null) InnerHtml();
                return _innerHtml;
            }
            //set { _innerHtml = value; }
        }

        private string _outHtml;
        /// <summary>
        /// 获取OUTHTML，直接从原始文档获取，性能高
        /// 如果以对文档进行修改，请直接调用Tostring方法
        /// </summary>
        public string OutHTML
        {
            get
            {
                if (_outHtml == null)
                    OutHtml();
                return _outHtml;
            }
        }

        private string _innerHtmlReBuild;
        /// <summary>
        /// 获取格式化的InnerHTML，仅在要获取更改过的文档或者有添加或者设定移除标记时用
        /// 性能一般，如无特殊要求，请使用InnerHTML属性
        /// </summary>
        public string InnerHTMLReBuild
        {
            get
            {
                InnerHtmlRebuild();
                return _innerHtmlReBuild;
            }
        }

        private string _value;
        /// <summary>
        /// 值对象
        /// 文本对象向左第一个标记为值的标签对象，如
        /// &lt;p&gt;&lt;b&gt;测试&lt;/b&gt;值是/b的&lt;/p&gt;
        /// &lt;/b&gt;标签对象的值为：值是/b的
        /// 而&lt;p&gt;的值为空字符，如要获取&lt;p&gt;的值，请使用InnerText属性
        /// </summary>
        public string Value
        {
            get
            {
                if (_value == null)
                    return string.Empty;

                return _value;
            }
            set { _value = value; }
        }

        private string _allValue;
        /// <summary>
        /// 所有值 此节点下的所有不包括子节点的值
        /// </summary>
        public string AllValue
        {
            get
            {
                if (string.IsNullOrEmpty(_allValue))
                    SearchAllValue();
                return _allValue;
            }
        }

        private string _innerText;
        /// <summary>
        /// 获取此节点下的文本
        /// </summary>
        public string InnerText
        {
            get
            {
                if (_innerText == null)
                    InnerText2();
                return _innerText;
            }

        }

        internal HtmlTag _prevNode;
        /// <summary>
        /// 上一个节点
        /// </summary>
        public HtmlTag PrevNode
        {
            get { return _prevNode; }
        }

        internal HtmlTag _nextNode;
        /// <summary>
        /// 下一个节点
        /// </summary>
        public HtmlTag NextNode
        {
            get { return _nextNode; }
        }

        private HtmlTag _nextSibling;
        /// <summary>
        /// 下一个兄弟节点
        /// </summary>
        public HtmlTag NextSibling
        {
            get { SearchNextSibling(); return _nextSibling; }
        }



        private HtmlTag _previousSibling;
        /// <summary>
        /// 上一个兄弟节点
        /// </summary>
        public HtmlTag PreviousSibling
        {
            get { SearchPreviousSibling(); return _previousSibling; }
        }

        private HtmlTag _parentNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public HtmlTag ParentNode
        {
            get
            {
                if (_parentNode == null)
                    ParentNode2();
                return _parentNode;
            }
        }

        private List<HtmlTag> _childNode;
        /// <summary>
        /// 子节点
        /// </summary>
        public List<HtmlTag> ChildNodes
        {
            get
            {
                if (_childNode == null)
                    ChildNodes2();
                return _childNode;
            }

        }

        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public HtmlTag this[int index]
        {
            get
            {
                if (ChildNodes.Count < 1)
                    return null;
                return ChildNodes[index];
            }
        }

        /// <summary>
        /// 是否存在子节点
        /// </summary>
        public bool HasChildNodes
        {
            get
            {
                return ChildNodes.Count > 0;
            }
        }

        private List<HtmlTag> _all;

        /// <summary>
        /// 所有子节点 包括子节点的子节点
        /// </summary>
        public List<HtmlTag> All
        {
            get { if (_all == null) AllElements(); return _all; }
        }

        private HtmlTag _backTag;
        /// <summary>
        /// 对应结束标记对象
        /// </summary>
        public HtmlTag BackTag
        {
            get { return _backTag; }

        }

        private HtmlTag _startNode;
        /// <summary>
        /// 对应标记开始对象
        /// </summary>
        public HtmlTag StartNode
        {
            get { return _startNode; }
        }

        internal Dictionary<string, HtmlTag> _idDic;
        /// <summary>
        /// 文档ID对象集合
        /// </summary>
        public Dictionary<string, HtmlTag> IdTagDic
        {
            get { return _idDic; }
        }

        /// <summary>
        /// 最后一个子节点
        /// </summary>
        private HtmlTag _lastChild;

        /// <summary>
        /// 标签样式
        /// </summary>
        private StyleParser _style;
        /// <summary>
        /// 获取标签样式数据
        /// </summary>
        public StyleParser Style
        {
            get
            {
                if (_style == null)
                {
                    this.ParseStyle();
                }
                return _style;
            }
            set { _style = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 同一命名空间可直接实例化
        /// </summary>
        internal HtmlTag()
        {

        }

        #region 文档解析部分


        /// <summary>
        /// 检查标签
        /// </summary>
        /// <returns>是否要继续下一步</returns>
        internal bool FormatterCheck()
        {
            switch (_document.reader.CurrChar())
            {
                case '/':
                    _document.reader.pos++;
                    _isBakcTag = true;
                    _backTag = this;
                    break;
                case '!':
                    if (_document.reader.SpString(_document.reader.pos, 3) == HtmlConst.COMMENTSSTART)
                    {
                        _tagName = HtmlConst.T;
                        _isBakcTag = false;
                        _startNode = this;
                        _document.reader.IncPos(3);
                        _document.FormatterComments();
                        return false;
                    }
                    break;
                default:
                    _startNode = this;
                    _isBakcTag = false;
                    break;
            }
            return true;
        }

        /// <summary>
        /// 格式化标签名
        /// </summary>
        internal void FormatterTagName()
        {
            int start = _document.reader.pos;
            do
            {
                if (HtmlConst.IsTagNameEndSymbol(_document.reader.CurrChar()))
                {
                    _tagName = _document.reader.GetStringToUpper(start);
                    break;
                }
                _document.reader.IncPos();

            } while (_document.reader.NEof());
        }

        /// <summary>
        /// 格式化标签属性
        /// </summary>
        internal void FormatterAttributes()
        {
            //以双引开始
            bool starwiths = false;
            //以单引开始
            bool starwithd = false;
            //是否因属性被挂起
            bool isattrnedSupend = false;
            //键值对
            bool valStart = false;
            bool firstSupend = false;
            char attrStarWith = HtmlConst.CHEMPTY;
            int keySIndex = -1;
            int keyLen = 0;
            int valSIndex = -1;
            char c;
            do
            {
                c = _document.reader.CurrChar();
                //如果标记没有结束，检查标记

                //如果没有被挂起
                if (!isattrnedSupend)
                {
                    if (valSIndex == -1 && HtmlConst.IsWhiteSpace(c))
                    {
                        _document.reader.IncPos();
                        continue;
                    }

                    if (c == HtmlConst.CHGT)
                        break;
                    if (c == HtmlConst.CHE)
                    {

                        valStart = true;
                        _document.reader.IncPos();
                        continue;
                    }
                    if (!valStart)
                    {
                        if (keySIndex == -1)
                            keySIndex = _document.reader.pos;
                        keyLen++;
                        _document.reader.IncPos();
                        continue;
                    }



                }

                switch (c)
                {
                    case '\'':
                        //以单引号开始的属性
                        if (!starwiths)
                        {
                            if (starwithd)
                            {
                                starwithd = false;
                                if (attrStarWith == HtmlConst.CHS)
                                {

                                    isattrnedSupend = false;

                                    valStart = false;
                                    firstSupend = false;
                                    if (keyLen > 0 && _document.reader.GT(valSIndex))
                                    {
                                        AddAttribute(_document.reader.GetStringToUpper(keySIndex, keyLen),
                                            _document.reader.GetString(valSIndex));
                                    }
                                    keySIndex = -1;
                                    keyLen = 0;
                                    valSIndex = -1;

                                }


                            }
                            else
                            {

                                if (!firstSupend)
                                {
                                    firstSupend = true;
                                    attrStarWith = HtmlConst.CHS;
                                }

                                starwithd = true;
                                isattrnedSupend = true;


                            }
                        }

                        break;
                    case '"':
                        //以双引号开始的属性
                        if (!starwithd)
                        {
                            if (starwiths)
                            {
                                starwiths = false;
                                if (attrStarWith == HtmlConst.CHD)
                                {

                                    isattrnedSupend = false;

                                    valStart = false;
                                    firstSupend = false;
                                    if (keyLen > 0 && _document.reader.GT(valSIndex))
                                    {
                                        AddAttribute(_document.reader.GetStringToUpper(keySIndex, keyLen),
                                            _document.reader.GetString(valSIndex));
                                    }
                                    keySIndex = -1;
                                    keyLen = 0;
                                    valSIndex = -1;

                                }

                            }
                            else
                            {
                                if (!firstSupend)
                                {
                                    firstSupend = true;
                                    attrStarWith = HtmlConst.CHD;
                                }

                                starwiths = true;
                                isattrnedSupend = true;

                            }
                        }
                        break;
                    default:
                        //处理特殊属性 没有双引号 也没有单引号
                        if (valStart)
                        {
                            if (firstSupend)
                            {
                                if (valSIndex == -1)
                                    valSIndex = _document.reader.pos;


                            }
                            else
                            {

                                if (valSIndex > -1 && HtmlConst.IsAttrEndSymbol(c))
                                {
                                    valStart = false;

                                    if (keyLen > 0 && _document.reader.GT(valSIndex))
                                    {
                                        AddAttribute(_document.reader.GetStringToUpper(keySIndex, keyLen),
                                            _document.reader.GetString(valSIndex));
                                    }
                                    keySIndex = -1;
                                    keyLen = 0;
                                    valSIndex = -1;


                                }
                                else
                                {
                                    if (valSIndex == -1)
                                        valSIndex = _document.reader.pos;

                                }
                            }
                        }
                        break;
                }

                _document.reader.IncPos();

            } while (_document.reader.NEof());
            if (keyLen > 0 && _document.reader.GT(valSIndex))
            {
                AddAttribute(_document.reader.GetStringToUpper(keySIndex, keyLen),
                    _document.reader.GetString(valSIndex));
            }
        }

        /// <summary>
        /// 检查标签名和属性之间的空格
        /// </summary>
        /// <returns></returns>
        internal bool CheckWhiteSpaceBtwNameAttr()
        {
            while (_document.reader.NEof())
            {

                if (HtmlConst.IsWhiteSpace(_document.reader.CurrChar()))
                {
                    _document.reader.IncPos();
                }
                else
                {
                    if (_document.reader.CurrChar() == HtmlConst.CHGT)
                    {
                        return false;
                    }
                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// 格式化一个HTML标签对象
        /// </summary>
        internal void FormatterTag()
        {

            _startIndex = _document.reader.pos;
            _document.reader.IncPos();
            if (!_document.reader.NEof())
                return;
            if (!FormatterCheck())
                return;
            FormatterTagName();
            if (CheckWhiteSpaceBtwNameAttr())
            {
                FormatterAttributes();
            }
            _endIndex = _document.reader.pos;
            if (_isBakcTag)
                StarNode();

        }

        /// <summary>
        /// 得到结束标记的开始标记对象
        /// </summary>
        private void StarNode()
        {

            HtmlTag backNode = this;
            while (backNode != null)
            {
                if (backNode._backTag == null)
                {
                    if (!backNode._isBakcTag && backNode._tagName == _tagName)
                    {
                        backNode._backTag = this;
                        _startNode = backNode;
                        break;
                    }
                }
                if (backNode._startNode == null)
                    backNode = backNode._prevNode;
                else
                    backNode = backNode._startNode._prevNode;
            }

        }

        /// <summary>
        /// 解析样式
        /// </summary>
        private void ParseStyle()
        {
            _style = new StyleParser(this);
            _style.ParseTagStyle();
        }

        #endregion

        #region 功能性属性部分

        /// <summary>
        /// 获取下一级兄弟节点
        /// </summary>
        private void SearchNextSibling()
        {

            HtmlTag backtag = GetbackTag();
            if (backtag == null || backtag._nextNode == null)
                return;
            if (ParentNode == null)
            {
                _nextSibling = backtag._nextNode;
                return;
            }

            HtmlTag nextbacktag = backtag._nextNode.GetbackTag();
            if (nextbacktag == null)
                return;

            if (nextbacktag._startIndex >= _parentNode._backTag._startIndex)
                return;
            _nextSibling = backtag._nextNode;

        }

        /// <summary>
        /// 得到结束标记，包含特殊情况
        /// </summary>
        /// <returns></returns>
        private HtmlTag GetbackTag()
        {
            HtmlTag t = _backTag;
            if (_backTag == null)
            {
                if (HtmlConst.IsSelfBackTag(_tagName) || HtmlConst.IsNoEndTag(_tagName))
                    t = this;
            }
            return t;
        }

        /// <summary>
        /// 获取上一级兄弟节点
        /// </summary>
        private void SearchPreviousSibling()
        {
            if (_startNode == null || _startNode._prevNode == null)
                return;
            if (ParentNode == null)
            {
                _previousSibling = _startNode._prevNode;
                return;
            }

            if (_startNode._prevNode._startNode._startIndex <= _parentNode._startNode._startIndex)
                return;
            _previousSibling = _startNode._prevNode._startNode;
        }


        /// <summary>
        /// 通过名字得到属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <returns>存在该属性返回属性值，不存在返回string.Empty</returns>
        public string GetAttribute(string key)
        {
            if (_attrDic == null)
                return string.Empty;
            if (_attrDic.ContainsKey(key))
                return _attrDic[key];
            return string.Empty;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="key">小写属性名</param>
        /// <param name="value">值</param>
        public void AddAttribute(string key, string value)
        {
            if (_attrDic == null)
                _attrDic = new Dictionary<string, string>();
            if (!_attrDic.ContainsKey(key))
            {
                _attrDic.Add(key, value);
                if (key == HtmlConst.ID)
                    if (!_idDic.ContainsKey(value))
                        _idDic.Add(value, this);

            }
        }

        /// <summary>
        /// 设置一个属性值,不存在该属性时执行添加
        /// </summary>
        /// <param name="key">键名 小写属性</param>
        /// <param name="value">值</param>
        public void SetAttribute(string key, string value)
        {
            if (_attrDic == null)
            {
                AddAttribute(key, value);
                return;
            }
            if (_attrDic.ContainsKey(key))
            {
                if (key == HtmlConst.ID)
                {
                    if (_idDic.ContainsKey(_attrDic[key]))
                    {
                        _idDic.Remove(_attrDic[key]);
                        _idDic.Add(value, this);
                    }
                }
                _attrDic[key] = value;
            }
            else
                AddAttribute(key, value);
        }

        /// <summary>
        /// 移除一个属性
        /// </summary>
        /// <param name="key">小写属性名</param>
        public void RemoveAttribute(string key)
        {
            if (_attrDic != null)
                if (_attrDic.ContainsKey(key))
                {
                    if (key == HtmlConst.ID)
                    {
                        if (_idDic.ContainsKey(_attrDic[key]))
                            _idDic.Remove(_attrDic[key]);
                    }
                    _attrDic.Remove(key);
                }
        }

        /// <summary>
        /// 移除所有属性
        /// </summary>
        public void RemoveAllAttributes()
        {
            _attrDic = null;
        }

        /// <summary>
        /// 获取内部InnerHtml
        /// 如果有对文档进行操作，移除节点或者增加节点，此方法会导致出错的结果，
        /// 请尝试调用InnerHTMLRebuild属性
        /// </summary>
        /// <returns></returns>
        private void InnerHtml()
        {
            if (_backTag == null || _nextNode == null)
            {
                return;
            }
            if (_backTag._startIndex > _document.HTML.Length)
                throw new InvalidOperationException("HTML文档已经被改变，请使用InnerHTMLREBUILD属性获取InnerHtml");

            _innerHtml = _document.reader.GetString(_startNode._endIndex + 1, _backTag._startIndex - _startNode._endIndex - 1).Trim();

        }

        /// <summary>
        /// 获取包括自身的HTML
        /// </summary>
        private void OutHtml()
        {
            if (_backTag == null || _nextNode == null)
            {
                return;
            }
            if (_backTag._startIndex > _document.HTML.Length)
                throw new InvalidOperationException("HTML文档已经被改变，请使用ToString方法获取OutHtml");

            _outHtml = _document.reader.GetString(_startNode._startIndex, _backTag._endIndex + 1 - _startNode._startIndex).Trim();
        }

        /// <summary>
        /// 获取文本值
        /// </summary>
        /// <returns>标记不全时返回NULL</returns>
        private void InnerText2()
        {
            if (_nextNode == null)
            {
                _innerText = _value;
                return;
            }
            if (_backTag == null)
                return;
            StringBuilder sb = new StringBuilder();

            HtmlTag tag = this;
            while (tag != null)
            {

                if (tag._startIndex < _backTag._startIndex)
                {
                    if (tag._value != null)
                        sb.Append(tag._value);
                    //if (tag._backTag != null)
                    //    if (tag._backTag._value != null)
                    //        sb.Append(tag._backTag._value);
                }
                else
                    break;
                tag = tag.NextNode;
            }

            //if (_backTag != null)
            //{
            //    if(_backTag.Value!=null)
            //     sb.Append(_backTag.Value);
            //}
            _innerText = sb.ToString().Trim();
        }

        /// <summary>
        /// 获取InnerText的长度
        /// </summary>
        /// <returns></returns>
        public int InnerTextLength()
        {
            if (_innerText != null)
                return _innerText.Length;


            if (_nextNode == null)
            {
                return Value.Length;
            }
            if (_backTag == null)
                return 0;
            int count = 0;
            HtmlTag tag = this;
            while (tag != null)
            {

                if (tag._startIndex < _backTag._startIndex)
                {
                    if (tag._value != null)
                        count += tag._value.Length;
                    //if (tag._backTag != null)
                    //    if (tag._backTag._value != null)
                    //        sb.Append(tag._backTag._value);
                }
                else
                    break;
                tag = tag._nextNode;
            }
            return count;
        }

        /// <summary>
        /// 检查对象是否在移除字典中
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        private HtmlTag CheckRemove(HtmlTag tag)
        {
            if (_document.RmTagDic.ContainsKey(tag._tagName))
            {
                if (tag._backTag == null)
                    return tag._nextNode;
                else
                    return tag._backTag._nextNode;
            }
            return tag;
        }

        /// <summary>
        /// 得到子节点
        /// </summary>
        /// <returns>标签有误时返回NULL</returns>
        private void ChildNodes2()
        {

            if (_backTag == null)
            {
                //if (_nextNode!=null&&HtmlConst.IsNoEndTag(_tagName))
                //{
                //    NotEndChildNodes();
                //    return;
                //}
                _childNode = _document.EmptyHtmlTagList;
                return;
            }
            if (_nextNode == null)
            {
                _childNode = _document.EmptyHtmlTagList;
                return;
            }

            List<HtmlTag> child = new List<HtmlTag>();
            HtmlTag tag = this._nextNode;

            while (tag != null)
            {
                if (!tag._isBakcTag)
                {
                    if (tag._endIndex < _backTag._startIndex)
                    {
                        tag._parentNode = this;

                        child.Add(tag);
                    }
                    else
                        break;
                }
                if (tag._backTag == null)
                    tag = tag._nextNode;
                else
                    tag = tag._backTag._nextNode;

            }

            _childNode = child;
        }

        /// <summary>
        /// 找到没有结束的的LI和P的子节点
        /// </summary>
        private void NotEndChildNodes()
        {

            if (_tagName == "li")
            {
                FindNotEndLiChildNodes();
                return;
            }
            else if (_tagName == "p")
            {
                FindNotEndPChildNodes();
            }
            _childNode = _document.EmptyHtmlTagList;
        }

        /// <summary>
        /// 找到LI没有结束标记的子节点
        /// </summary>
        private void FindNotEndLiChildNodes()
        {
            HtmlTag tag = _nextNode;
            List<HtmlTag> list = new List<HtmlTag>();
            string pName = GetParentTagName();
            while (tag != null)
            {
                if (IsLiCloseTagName(tag._tagName, pName))
                {
                    break;
                }
                if (tag._isBakcTag)
                {
                    tag = tag._nextNode;
                    continue;
                }
                tag._parentNode = this;
                list.Add(tag);
                tag = tag._nextNode;
            }
            _childNode = list;
        }

        /// <summary>
        /// 是否是符合LI关闭的标签名
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="pName"></param>
        /// <returns></returns>
        private bool IsLiCloseTagName(string tagName, string pName)
        {
            return tagName == "li" || tagName == "ul" || tagName == pName;
        }

        /// <summary>
        /// 是否是P标记关闭的标签名
        /// </summary>
        /// <param name="tagName">当前标签名</param>
        /// <param name="pName">P标记的父级标签</param>
        /// <returns></returns>
        private bool IsPColoseTagName(string tagName, string pName)
        {
            return tagName == "p" || tagName == pName;
        }

        /// <summary>
        /// 得到父节点的标签名
        /// </summary>
        /// <returns></returns>
        private string GetParentTagName()
        {
            if (ParentNode == null)
                return string.Empty;
            return ParentNode._tagName;
        }

        /// <summary>
        /// 找到没有结束的P标记的标签名
        /// </summary>
        private void FindNotEndPChildNodes()
        {
            HtmlTag tag = _nextNode;
            List<HtmlTag> list = new List<HtmlTag>();
            string pName = GetParentTagName();
            while (tag != null)
            {
                if (IsPColoseTagName(tag._tagName, pName))
                {
                    break;
                }
                if (tag._isBakcTag)
                {
                    tag = tag._nextNode;
                    continue;
                }
                list.Add(tag);
                tag = tag._nextNode;

            }
            _childNode = list;
        }




        /// <summary>
        /// 找到父节点
        /// </summary>
        private void ParentNode2()
        {
            if (_prevNode == null)
                return;

            HtmlTag tag = this._prevNode;
            int endidx = 0;
            if (_backTag == null)
                endidx = _endIndex;
            else
                endidx = _backTag._endIndex;
            while (tag != null)
            {
                if (!tag._isBakcTag)
                {
                    if (tag._backTag == null)
                    {
                        tag = tag._prevNode;
                        continue;
                    }
                    if (tag._backTag._startIndex > endidx)
                    {
                        _parentNode = tag;
                        break;
                    }

                }
                tag = tag._prevNode;

            }
        }

        /// <summary>
        /// 得到所有子节点
        /// </summary>
        private void AllElements()
        {

            if (_nextNode == null || _backTag == null)
            {
                _all = _document.EmptyHtmlTagList;
                return;
            }
            List<HtmlTag> list = new List<HtmlTag>();

            HtmlTag tag = _nextNode;
            while (tag != null)
            {
                if (!tag._isBakcTag)
                {
                    if (tag._endIndex < _backTag._endIndex)
                        list.Add(tag);
                    else
                        break;
                }
                tag = tag._nextNode;
            }
            _all = list;

        }


        /// <summary>
        /// 搜索值
        /// </summary>
        private void SearchAllValue()
        {
            if (_nextNode == null || _backTag == null)
            {
                _allValue = _value.Trim();
                return;
            }
            if (ChildNodes.Count == 0)
            {
                _allValue = _value.Trim();
                return;
            }

            StringBuilder sb = new StringBuilder(_value);
            foreach (HtmlTag tag in _childNode)
            {
                if (tag.BackTag == null)
                    sb.Append(tag._value);
                else
                    sb.Append(tag._backTag._value);
            }

            _allValue = sb.ToString().Trim();
        }


        /// <summary>
        /// HTML标签重组
        /// </summary>
        /// <returns>标签或文档对象的HTML代码</returns>
        public override string ToString()
        {

            if (_backTag == null || _nextNode == null)
            {
                if (_attrDic != null)
                {
                    if (HtmlConst.IsSelfBackTag(_tagName))
                        return string.Format("<{0}{1}/>{2}", _tagName, GetAttributeStr(this), _value);
                    else
                        return string.Format("<{0}{1}>{2}</{0}>", _tagName, GetAttributeStr(this), _value);
                }
                else
                {
                    if (HtmlConst.IsSelfBackTag(_tagName))
                        return string.Format("<{0} />{1}", _tagName, _value);
                    else
                        return string.Format("<{0}>{1}</{0}>", _tagName, _value);
                }

            }

            StringBuilder sb = new StringBuilder();
            if (_backTag != null)
                sb.Append(string.Format("<{0}{1}>{2}", _tagName, GetAttributeStr(this), _value));
            HtmlTag tag = this._nextNode;
            while (tag != null)
            {

                if (tag._endIndex > _backTag._startIndex)
                    break;
                if (tag._attrDic != null)
                {
                    if (HtmlConst.IsSelfBackTag(tag._tagName))
                        sb.Append(string.Format("<{0}{1}/>{2}", tag._tagName, GetAttributeStr(tag), tag._value));
                    else
                    {

                        if (tag._isBakcTag)
                            sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}{1}>{2}", tag._tagName, GetAttributeStr(tag), tag._value));


                    }
                }
                else
                {

                    if (tag._isBakcTag)
                        sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                    else
                    {
                        if (HtmlConst.IsSelfBackTag(tag._tagName))
                            sb.Append(string.Format("<{0} />{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}>{1}", tag._tagName, tag._value));
                    }

                }

                tag = tag._nextNode;
            }
            if (_backTag != null)
                sb.Append(string.Format("</{0}>", _tagName));
            return sb.ToString();

        }


        /// <summary>
        /// 得到安全的重组的HTML String 移除所有HTML事件 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string ToStringSafe(HtmlTag root)
        {
            if (root._backTag == null || root._nextNode == null)
            {
                if (root._attrDic != null)
                {
                    if (HtmlConst.IsSelfBackTag(root._tagName))
                        return string.Format("<{0}{1}/>{2}", root._tagName, GetSafeAttributeStr(root), root._value);
                    else
                        return string.Format("<{0}{1}>{2}</{0}>", root._tagName, GetSafeAttributeStr(root), root._value);
                }
                else
                {
                    if (HtmlConst.IsSelfBackTag(root._tagName))
                        return string.Format("<{0} />{1}", root._tagName, root._value);
                    else
                        return string.Format("<{0}>{1}</{0}>", root._tagName, root._value);
                }

            }

            StringBuilder sb = new StringBuilder(root._document.HTML.Length + 0x100);
            if (root._backTag != null)
                sb.Append(string.Format("<{0}{1}>{2}", root._tagName, GetSafeAttributeStr(root), root._value));
            HtmlTag tag = root._nextNode;
            while (tag != null)
            {

                if (tag._endIndex > root._backTag._startIndex)
                    break;
                if (tag._attrDic != null)
                {
                    if (HtmlConst.IsSelfBackTag(tag._tagName))
                        sb.Append(string.Format("<{0}{1}/>{2}", tag._tagName, GetSafeAttributeStr(tag), tag._value));
                    else
                    {

                        if (tag._isBakcTag)
                            sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}{1}>{2}", tag._tagName, GetSafeAttributeStr(tag), tag._value));


                    }
                }
                else
                {

                    if (tag._isBakcTag)
                        sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                    else
                    {
                        if (HtmlConst.IsSelfBackTag(tag._tagName))
                            sb.Append(string.Format("<{0} />{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}>{1}", tag._tagName, tag._value));
                    }

                }

                tag = tag._nextNode;
            }
            if (root._backTag != null)
                sb.Append(string.Format("</{0}>", root._tagName));
            return sb.ToString();
        }

        /// <summary>
        /// 得到安全的HTMLSTR 移除所有的ON开头的属性 移除 javascript:的相关项
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetSafeAttributeStr(HtmlTag tag)
        {
            if (tag._attrDic == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (string key in tag._attrDic.Keys)
            {
                if (key.StartsWith("ON"))
                    continue;
                string val = tag._attrDic[key];
                if (val.StartsWith("javascript:", StringComparison.CurrentCultureIgnoreCase))
                    continue;
                sb.Append(string.Format(" {0}=\"{1}\"", key, tag._attrDic[key]));
            }
            return sb.ToString();

        }

        /// <summary>
        /// 重建InnerHTML
        /// </summary>
        /// <returns></returns>
        private void InnerHtmlRebuild()
        {
            if (_backTag == null || _nextNode == null)
            {
                _innerHtmlReBuild = _value;
                return;
            }

            HtmlTag tag = this._nextNode;
            StringBuilder sb = new StringBuilder(_value);
            while (tag != null)
            {

                if (tag._endIndex > _backTag._startIndex)
                    break;
                if (tag._attrDic != null)
                {
                    if (HtmlConst.IsSelfBackTag(tag._tagName))
                        sb.Append(string.Format("<{0}{1}/>{2}", tag._tagName, GetAttributeStr(tag), tag._value));
                    else
                    {

                        if (tag._isBakcTag)
                            sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}{1}>{2}", tag._tagName, GetAttributeStr(tag), tag._value));


                    }
                }
                else
                {

                    if (tag._isBakcTag)
                        sb.Append(string.Format("</{0}>{1}", tag._tagName, tag._value));
                    else
                    {
                        if (HtmlConst.IsSelfBackTag(tag._tagName))
                            sb.Append(string.Format("<{0} />{1}", tag._tagName, tag._value));
                        else
                            sb.Append(string.Format("<{0}>{1}", tag._tagName, tag._value));
                    }

                }

                tag = tag._nextNode;
            }
            _innerHtmlReBuild = sb.ToString();

        }

        /// <summary>
        /// 得到属性字符串
        /// </summary>
        /// <returns>得到属性字符串</returns>
        public string GetAttributeStr(HtmlTag tag)
        {
            if (tag._attrDic == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (string key in tag._attrDic.Keys)
            {
                //if (key[0] == '/')
                //    continue;
                sb.Append(string.Format(" {0}=\"{1}\"", key, tag._attrDic[key]));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 得到当前节点对象,如果当前对象时结束标记，返回其起始标记
        /// </summary>
        /// <returns></returns>
        private HtmlTag GetThisElement()
        {
            if (_isBakcTag)
                return _startNode;
            return this;
        }

        /// <summary>
        /// 如果重要标记未结束 对重要标记进行修复,
        /// 如Body和跟节点
        /// </summary>
        internal void FixImportantTag()
        {
            HtmlTag htmlTag = null;
            StringBuilder sb = null;
            if (_document.Body != null)
            {
                if (_document.Body.BackTag == null)
                {
                    sb = new StringBuilder(_document.HTML);
                    htmlTag = new HtmlTag();
                    htmlTag._isBakcTag = true;
                    htmlTag._prevNode = this;
                    htmlTag._idDic = _idDic;
                    htmlTag._startIndex = _endIndex + 1;
                    htmlTag._startNode = _document.Body;
                    htmlTag._backTag = htmlTag;
                    htmlTag._document = _document;
                    htmlTag._document.HTML = sb.Append(string.Format("</{0}>", _document.Body.TagName)).ToString();
                    htmlTag._endIndex = htmlTag._document.HTML.Length - 1;
                    htmlTag._value = string.Empty;
                    htmlTag._tagName = _document.Body.TagName;
                    _document.Body._backTag = htmlTag;

                }
            }

            if (_document.HtmlElemnt != null)
            {
                if (_document.HtmlElemnt.BackTag == null)
                {

                    if (htmlTag == null)
                    {
                        sb = new StringBuilder(_document.HTML);
                        htmlTag = new HtmlTag();
                        htmlTag._isBakcTag = true;
                        htmlTag._prevNode = this;
                        htmlTag._idDic = _idDic;
                        htmlTag._startIndex = _endIndex + 1;
                        htmlTag._startNode = _document.HtmlElemnt;
                        htmlTag._backTag = htmlTag;
                        htmlTag._document = _document;
                        htmlTag._document.HTML = sb.Append(string.Format("</{0}>", _document.HtmlElemnt.TagName)).ToString();
                        htmlTag._endIndex = htmlTag._document.HTML.Length - 1;
                        htmlTag._value = string.Empty;
                        htmlTag._tagName = _document.HtmlElemnt.TagName;
                        _document.HtmlElemnt._backTag = htmlTag;
                    }
                    else
                    {
                        HtmlTag el = new HtmlTag();
                        sb = new StringBuilder(htmlTag._document.HTML);
                        el._isBakcTag = true;
                        el._prevNode = htmlTag;
                        el._idDic = _idDic;
                        el._startIndex = htmlTag._endIndex + 1;
                        el._startNode = _document.HtmlElemnt;
                        el._backTag = el;
                        el._document = _document;
                        el._document.HTML = sb.Append(string.Format("</{0}>", _document.HtmlElemnt.TagName)).ToString();
                        el._endIndex = htmlTag._document.HTML.Length - 1;
                        el._value = string.Empty;
                        el._tagName = _document.HtmlElemnt.TagName;
                        htmlTag._nextNode = el;
                        _document.HtmlElemnt._backTag = el;
                    }
                }
            }


        }

        /// <summary>
        /// 获取当前节点值的解码字符 将HTML实体字符转换为字符
        /// </summary>
        /// <param name="skipWhiteSpace">在解码时是否跳过空格</param>
        /// <returns>解码后的结果</returns>
        public string DecodeValue(bool skipWhiteSpace)
        {
            return HtmlHelper.DecodeHTMLString(this._value, skipWhiteSpace);
        }

        #endregion

        #region 文档操作部分

        /// <summary>
        /// 移除此标记 如果此标记有子节点，子节点也将移除
        /// 如果只移除当前节点，保留子节点，请调用：RemoveTagButKeepChildren方法
        /// 调用此方法对文档作出修改后请调用ToString获取文档或者调用InnerHTMLRebuild 不用调用InnerHTML否则会出错
        /// 如果出现异常请尝试调用HtmlParser的ReParser方法,该方法会重新解析编辑过后的文档
        /// </summary>
        public void RemoveTag()
        {
            //子节点相关准备移除工作
            ChildNodesRemovePrepare();
            CheckIdAttributeForRemove();
            //更改上一级节点的下一级节点
            if (_prevNode != null)
            {
                if (_backTag != null)
                    _prevNode._nextNode = _backTag._nextNode;
                else
                    _prevNode._nextNode = _nextNode;
            }
            //更改下一级节点的上一个节点为此节点上一节点
            if (_nextNode != null)
            {
                if (_backTag != null)
                {
                    if (_backTag._nextNode != null)
                        _backTag._nextNode._prevNode = _startNode._prevNode;
                    else
                        _nextNode._prevNode = _prevNode;
                }
                else
                {
                    _nextNode._prevNode = _prevNode;
                }
            }
            //重置父节点的子节点集合  使在下一次搜索时重组
            if (_parentNode != null)
            {
                _parentNode._childNode = null;
                _parentNode._innerText = null;
                _parentNode._allValue = null;
                _parentNode._all = null;
            }
            //将结束标签的值转移到开始标签的上一标签下面
            if (_backTag != null)
            {
                if (_backTag.Value.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    if (_startNode._prevNode != null)
                    {
                        sb.Append(_startNode.Value);
                        sb.Append(_backTag._value);
                        _startNode._prevNode._value = sb.ToString();
                    }

                }
            }

            //this = null;
        }

        /// <summary>
        /// 移除该节点下的子节点
        /// </summary>
        public void RemoveChildNodes()
        {
            HtmlTag tag = GetThisElement();
            if (tag._nextNode == null || tag._backTag == null)
                return;
            if (ChildNodes.Count == 0)
                return;
            ChildNodesRemovePrepare();
            //将子节点的结束标记的值转移到此节点的开始节点
            StringBuilder sb = new StringBuilder(tag._value);
            foreach (HtmlTag child in _childNode)
            {
                if (child._backTag != null)
                    sb.Append(child._backTag._value);
            }
            tag._value = sb.ToString();
            tag._nextNode = tag._backTag;
            tag._backTag._prevNode = tag;
            tag._all = null;
            tag._allValue = null;
            tag._childNode = null;
            tag._innerText = null;
            if (ParentNode != null)
            {
                _parentNode._all = null;
                _parentNode._allValue = null;
            }

            //子节点相关准备移除工作
            ChildNodesRemovePrepare();
        }

        /// <summary>
        /// 移除子节点的时候子节点的属性是否有ID属性，有则移除
        /// </summary>
        private void ChildNodesRemovePrepare()
        {
            RemoveIdAttribute(All);
        }


        /// <summary>
        /// 检查一个集合中的ID属性，存在则移除，用于删除节点
        /// </summary>
        /// <param name="tagList"></param>
        private void RemoveIdAttribute(List<HtmlTag> tagList)
        {
            foreach (HtmlTag t in tagList)
            {
                t.CheckIdAttributeForRemove();
            }
        }

        /// <summary>
        /// 如果该节点需要移除，调用此方法检查是否包含ID属性，有则移除
        /// </summary>
        private void CheckIdAttributeForRemove()
        {
            if (_attrDic != null)
            {
                if (_attrDic.ContainsKey(HtmlConst.ID))
                {
                    string id = _attrDic[HtmlConst.ID];
                    if (_idDic.ContainsKey(id))
                    {
                        if (_idDic[id] == this)
                            _idDic.Remove(id);
                    }
                }
            }
        }

        /// <summary>
        /// 在移除父级节点时重置子节点父节点
        /// </summary>
        private void RestChildNodesParent()
        {
            if (ChildNodes.Count > 0)
            {
                foreach (HtmlTag tag in _childNode)
                {
                    tag._parentNode = null;
                }
            }
        }

        /// <summary>
        /// 移除此节点，如果该标签是父节点，那么子节点将保留
        /// 如需移除此节点和此节点的子节点，请调用RemoveTag
        /// 调用此方法对文档作出修改后请调用ToString获取文档或者调用InnerHTMLRebuild 不用调用InnerHTML否则会出错
        /// 如果出现异常请尝试调用HtmlParser的ReParser方法,该方法会重新解析编辑过后的文档
        /// </summary>
        public void RemoveTagButKeepChildren()
        {
            RemoveStartTag();
            RemoveBackTag();
            //重置父节点的子节点集合  使在下一次搜索时重组
            if (_parentNode != null)
            {
                _parentNode._childNode = null;
                _parentNode._innerText = null;
                _parentNode._allValue = null;
                _parentNode._all = null;
            }
            //重置子节点的数据
            RestChildNodesParent();
        }

        /// <summary>
        /// 只移除该节点，节点的值全部转移到上一个节点
        /// </summary>
        public void RemoveTagButKeepChildrenAndValue()
        {
            RemoveStartTagButTransferValue();
            RemoveBackTag();
            //重置父节点的子节点集合  使在下一次搜索时重组
            if (_parentNode != null)
            {
                _parentNode._childNode = null;
                _parentNode._innerText = null;
                _parentNode._allValue = null;
                _parentNode._all = null;
            }
            //重置子节点的数据
            RestChildNodesParent();
        }

        /// <summary>
        /// 移除开始标记
        /// </summary>
        private void RemoveStartTag()
        {
            CheckIdAttributeForRemove();
            RestChildNodesParent();
            //上级节点的下一节点更改为此节点下一节点
            if (_prevNode != null)
            {
                if (_backTag == null)
                    _prevNode._nextNode = _nextNode;
                else
                {
                    if (_nextNode != null)
                    {
                        //当下一级节点=该节点结束标记时
                        if (_nextNode._startIndex == _backTag._startIndex)
                        {
                            _prevNode._nextNode = _backTag._nextNode;
                        }
                        else
                            _prevNode._nextNode = _nextNode;
                    }
                    else
                        _prevNode._nextNode = _nextNode;
                }
            }
            if (_nextNode != null)
            {
                if (_backTag == null)
                {
                    _nextNode._prevNode = _prevNode;
                }
                else
                {
                    //当下一节点=该节点结束标记时 下级节点=开始标记的上级节点
                    if (_backTag._startIndex == _nextNode._startIndex)
                    {
                        _nextNode._prevNode = _startNode._prevNode;
                    }
                    else
                        _nextNode._prevNode = _prevNode;

                }
            }
        }

        /// <summary>
        /// 移除节点但把此节点的值转移到前一个节点
        /// </summary>
        private void RemoveStartTagButTransferValue()
        {
            CheckIdAttributeForRemove();
            RestChildNodesParent();
            //上级节点的下一节点更改为此节点下一节点
            if (_prevNode != null)
            {
                if (_backTag == null)
                    _prevNode._nextNode = _nextNode;
                else
                {
                    if (_nextNode != null)
                    {
                        //当下一级节点=该节点结束标记时
                        if (_nextNode._startIndex == _backTag._startIndex)
                        {
                            _prevNode._nextNode = _backTag._nextNode;
                        }
                        else
                            _prevNode._nextNode = _nextNode;
                    }
                    else
                        _prevNode._nextNode = _nextNode;
                }
                _prevNode.Value += Value;
            }
            if (_nextNode != null)
            {
                if (_backTag == null)
                {
                    _nextNode._prevNode = _prevNode;
                }
                else
                {
                    //当下一节点=该节点结束标记时 下级节点=开始标记的上级节点
                    if (_backTag._startIndex == _nextNode._startIndex)
                    {
                        _nextNode._prevNode = _startNode._prevNode;
                    }
                    else
                        _nextNode._prevNode = _prevNode;

                }

            }
        }

        /// <summary>
        /// 移除结束标记
        /// </summary>
        private void RemoveBackTag()
        {
            if (_backTag == null)
                return;
            //当结束标记的上一级节点=开始标记
            if (_backTag._prevNode != null)
            {
                if (_backTag._prevNode._startIndex != _startNode._startIndex)
                {
                    _backTag._prevNode._nextNode = _backTag._nextNode;
                }

            }

            if (_backTag._nextNode != null)
            {

                if (_backTag._prevNode._startIndex == _startNode._startIndex)
                {

                    _backTag._nextNode._prevNode = _startNode._prevNode;
                }
                else
                    _backTag._nextNode._prevNode = _backTag._prevNode;
            }
            //转移结束标记的值
            if (_backTag.Value.Trim().Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                if (_backTag._prevNode._startIndex == _startNode._startIndex)
                {
                    if (_backTag._startNode._prevNode != null)
                    {
                        sb.Append(_backTag._startNode._prevNode.Value);
                        sb.Append(_backTag.Value);
                        _backTag._startNode._prevNode._value = sb.ToString();
                    }
                }
                else
                {
                    if (_backTag._prevNode != null)
                    {
                        sb.Append(_backTag._prevNode.Value);
                        sb.Append(_backTag.Value);
                        _backTag._prevNode._value = sb.ToString();
                    }
                }
            }

        }

        /// <summary>
        /// 插入节点
        /// 插入的节点跟节点必须规范
        /// 添加节点后如需使用InnerHTML属性，请使用InnerHTMLRebuild属性获取重组的HTMLStr
        /// </summary>
        /// <param name="tag">要插入的标签</param>
        private void AppendHtmlTag(HtmlTag tag)
        {
            int length = tag._document.HTML.Length;

            //整理_document.reader.pos和EndIndex

            JoinIdDic(tag._idDic);
            HtmlTag next = tag;
            while (true)
            {
                next._startIndex += _endIndex;
                next._endIndex += _endIndex;
                next._parentNode = null;
                next._idDic = _idDic;
                next._document = _document;
                next._all = null;
                next._allValue = null;
                next._childNode = null;
                next._innerHtml = null;
                next._innerText = null;
                next._outHtml = null;
                next._tagAttributeStr = null;

                if (next._nextNode == null)
                    break;
                next = next._nextNode;

            }
            //上一节点设置为当前节点
            tag._prevNode = this;

            //最后一个节点的下一节点设置为当前下一个节点
            next._nextNode = _nextNode;
            //更新此节点下所有的_document.reader.pos和EndIndex
            HtmlTag thisNext = _nextNode;
            while (thisNext != null)
            {

                thisNext._startIndex += length;
                thisNext._endIndex += length;
                thisNext = thisNext._nextNode;
            }

            //当前节点下一级节点的前一级节点为当前节点的最后一个节点
            _nextNode._prevNode = next;
            //设置下一节点为当前当前节点
            _nextNode = tag;

            //_nextNode._prevNode = tag;
            //重置属性
            _all = null;
            _childNode = null;
            _innerText = null;
            _allValue = null;
            if (ParentNode != null)
            {
                _parentNode._innerText = null;
                _parentNode._childNode = null;
                _parentNode._all = null;
                _parentNode._allValue = null;
                _parentNode = null;
            }

        }

        /// <summary>
        /// 最后一个子节点，没有子节点返回空
        /// </summary>
        /// <returns></returns>
        public HtmlTag LastChild()
        {
            if (_lastChild != null)
                return _lastChild;
            if (ChildNodes.Count == 0)
                return null;
            return _childNode[_childNode.Count - 1];
        }

        /// <summary>
        /// 第一个子节点,没有子节点返回空
        /// </summary>
        /// <returns></returns>
        public HtmlTag FirstChild()
        {
            if (ChildNodes.Count == 0)
                return null;
            return _childNode[0];
        }

        /// <summary>
        /// 合并ID对象字典
        /// </summary>
        /// <param name="dic">ID对象字典</param>
        private void JoinIdDic(Dictionary<string, HtmlTag> dic)
        {
            if (dic == null)
                return;
            foreach (string key in dic.Keys)
            {
                if (!_idDic.ContainsKey(key))
                    _idDic.Add(key, dic[key]);
            }
        }

        /// <summary>
        /// 在该节点的基础上插入子节点
        /// </summary>
        /// <param name="tag">
        /// 要插入的节点
        /// </param>
        public void AppendChild(HtmlTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag", "标签对象不能为NULL");
            if (IsPartOfThisDocument(tag))
            {
                AppendChild(tag.ToString());
                return;
            }


            if (_isBakcTag)
            {
                HtmlTag last = LastChild();
                if (last == null)
                    _startNode.AppendHtmlTag(tag);
                else
                    last.AppendAfterThis(tag);
            }
            else
            {
                HtmlTag last = LastChild();
                if (last == null)
                    AppendHtmlTag(tag);
                else
                    last.AppendAfterThis(tag);
            }
            _lastChild = tag;
            tag._parentNode = this;
            if (tag._backTag != null)
                tag._backTag._parentNode = this;

        }

        /// <summary>
        /// 在该节点的基础上插入子节点
        /// </summary>
        /// <param name="htmltagStr">规范的HTML代码</param>
        public void AppendChild(string htmltagStr)
        {
            AppendChild(CreateHtmlDocument(htmltagStr));

        }

        /// <summary>
        /// 添加子节点不设置一个唯一标识
        /// </summary>
        /// <param name="htmlTagStr">规范的HTMl代码</param>
        /// <param name="timeTicks">当前时间戳</param>
        private void AppendChild(string htmlTagStr, string timeTicks)
        {
            AppendChild(CreateHtmlDocument(htmlTagStr, timeTicks));
        }

        /// <summary>
        /// 在该节点之后插入节点
        /// 添加节点后如需使用InnerHTML,请直接调用InnerHTMLRebuild
        /// 根节点不能掉用此方法，否则会引发异常
        /// </summary>
        /// <param name="tag">要插入的节点</param>
        public void AppendAfterThis(HtmlTag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag", "标签对象不能为NULL");

            if (IsPartOfThisDocument(tag))
            {
                AppendAfterThis(tag.ToString());
                return;
            }
            if (_isBakcTag)
                AppendHtmlTag(tag);
            else
                if (_backTag != null)
                _backTag.AppendHtmlTag(tag);
            else
                AppendChild(tag);


        }

        /// <summary>
        /// 在该节点之后插入节点
        /// 添加节点后如需使用InnerHTML,请直接调用InnerHTMLRebuild
        /// 根节点不能掉用此方法，否则会引发异常
        /// </summary>
        /// <param name="htmlTagStr">规范的HTML代码</param>
        public void AppendAfterThis(string htmlTagStr)
        {

            AppendAfterThis(CreateHtmlDocument(htmlTagStr));

        }

        /// <summary>
        /// 在该节点之后插入节点
        /// </summary>
        /// <param name="htmlTagStr">规范的HTML代码</param>
        /// <param name="timeTicks">当前时间戳</param>
        private void AppendAfterThis(string htmlTagStr, string timeTicks)
        {
            AppendAfterThis(CreateHtmlDocument(htmlTagStr, timeTicks));
        }

        /// <summary>
        /// 在该节点之前插入节点
        /// 添加节点后如需使用InnerHTML,请直接调用InnerHTMLRebuild
        /// 根节点不能掉用此方法，否则会引发异常
        /// </summary>
        /// <param name="tag">要插入的节点</param>
        public void AppendBeforeThis(HtmlTag tag)
        {
            if (tag == null)
                throw new System.ArgumentNullException("tag", "不能为NULL");

            if (IsPartOfThisDocument(tag))
            {
                AppendBeforeThis(tag.ToString());
                return;
            }

            if (_isBakcTag)
            {
                if (_startNode._prevNode != null)
                    _startNode._prevNode.AppendHtmlTag(tag);
                else
                    throw new Exception("不能在根节点之前插入");
            }
            else
                if (_prevNode != null)
                _prevNode.AppendHtmlTag(tag);
            else
                throw new Exception("不能在根节点之前插入");

        }

        /// <summary>
        /// 在该节点之前插入节点
        /// 添加节点后如需使用InnerHTML,请直接调用InnerHTMLRebuild
        /// 根节点不能掉用此方法，否则会引发异常
        /// </summary>
        /// <param name="htmlTagStr">规范的HTML代码</param>
        public void AppendBeforeThis(string htmlTagStr)
        {
            AppendBeforeThis(CreateHtmlDocument(htmlTagStr));
        }

        private void AppendBeforeThis(string htmlTagStr, string timeTicks)
        {
            AppendBeforeThis(CreateHtmlDocument(htmlTagStr, timeTicks));
        }

        /// <summary>
        /// 为该节点添加父级节点
        /// </summary>
        /// <param name="tag">标签对象，该对象必须包含结束标记,该对象只能使用一次</param>
        /// <returns>更改后的当前节点，请务必接收更改后的节点</returns>
        public HtmlTag AppendParent(HtmlTag tag)
        {


            if (tag == null)
                throw new ArgumentNullException("tag", "标签不能为空");
            if (tag._backTag == null || tag._startNode == null)
                throw new ArgumentOutOfRangeException("tag", "标签对象不完整！不能做为设置此节点的父节点的对象");
            if (IsPartOfThisDocument(tag))
            {
                return AppendParent(tag.ToString());

            }
            if (_prevNode == null)
                throw new ArgumentOutOfRangeException("this", "不能为根节点");

            bool needNewIdTT = false;
            string timeTicks = this.GetAttribute(HtmlConst.ID);
            if (timeTicks.Length == 0)
            {
                needNewIdTT = true;
                timeTicks = GetNewId("T");
            }
            else
            {
                if (GetElementById(timeTicks) != this)
                    throw new ArgumentOutOfRangeException("此文档对象ID和此对象重复：" + GetElementById(timeTicks).ToString());
            }
            //将当前节点添加到此节点
            if (tag.ChildNodes.Count > 0)
                tag._backTag._prevNode.AppendAfterThis(this.ToString(), timeTicks);
            else
                tag._backTag._prevNode.AppendChild(this.ToString(), timeTicks);
            //将此节点添加到当前节点前一节点的后面
            string finalStr = tag.ToString();

            if (ParentNode != null)
            {
                if (_parentNode._startIndex == _prevNode._startIndex)
                {
                    AppendBeforeThis(finalStr);

                }
                else
                {
                    _prevNode.AppendAfterThis(finalStr);
                }
            }
            else
            {
                _prevNode.AppendAfterThis(finalStr);
            }
            HtmlTag thisTag = GetElementById(timeTicks);
            if (needNewIdTT)
                RemoveIdAttribute(thisTag);
            this.RemoveTag();
            return thisTag;

            //tag._backTag._nextNode.RemoveTag();
        }

        /// <summary>
        /// 移除ID属性
        /// </summary>
        /// <param name="tag"></param>
        private void RemoveIdAttribute(HtmlTag tag)
        {
            tag.RemoveAttribute(HtmlConst.ID);
        }
        /// <summary>
        /// 添加ＩＤ属性
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="id"></param>
        private void AddIdAttribute(HtmlTag tag, string id)
        {
            tag.AddAttribute(HtmlConst.ID, id);
        }

        /// <summary>
        /// 试图通过ID获取一个对象，然后移除该对象ID
        /// </summary>
        /// <param name="id"></param>
        private void TryGetAddRemoveIdAttribute(HtmlTag tag, string id)
        {
            if (GetElementById(id) != null)
            {
                tag = GetElementById(id);
                RemoveIdAttribute(tag);

            }
        }

        private string TryCheckIdObject(HtmlTag tag)
        {
            if (tag._attrDic != null)
            {
                if (tag._attrDic.ContainsKey(HtmlConst.ID))
                    return tag.GetAttribute(HtmlConst.ID);
            }
            return null;
        }

        /// <summary>
        /// 得到一个跟时间有关的新ID
        /// </summary>
        /// <param name="pre">新ID前缀</param>
        /// <returns></returns>
        private string GetNewId(string pre)
        {
            return string.Format("{0}_{1}", pre, Guid.NewGuid());
        }

        /// <summary>
        /// 为该节点添加父级节点
        /// </summary>
        /// <param name="htmlTagStr">规范的HTML代码</param>
        /// <returns>更改后的当前节点,如需继续更改此节点；请务必接收此返回节点</returns>
        public HtmlTag AppendParent(string htmlTagStr)
        {
            return AppendParent(CreateHtmlDocument(htmlTagStr));
        }

        /// <summary>
        /// 根据HTML代码创建HTML文档对象
        /// </summary>
        /// <param name="htmlTagStr"></param>
        /// <returns></returns>
        private HtmlTag CreateHtmlDocument(string htmlTagStr)
        {
            if (htmlTagStr == null)
                throw new System.ArgumentNullException("htmlTagStr", "不能为NULL");
            HtmlParser hp = new HtmlParser(htmlTagStr);
            hp.AutoFixImportantTag = false;
            hp.Parse();
            return hp.HtmlElemnt;
        }

        /// <summary>
        /// 根据HTML代码创建HTML文档对象
        /// </summary>
        /// <param name="htmlTagStr"></param>
        /// <param name="timeTicks">时间戳，用来设置一个唯一标示</param>
        /// <returns></returns>
        private HtmlTag CreateHtmlDocument(string htmlTagStr, string timeTicks)
        {
            HtmlTag tag = CreateHtmlDocument(htmlTagStr);
            tag.SetAttribute(HtmlConst.ID, timeTicks);
            return tag;
        }

        /// <summary>
        /// 检查一个标签是否是否和此标签是同一个文档
        /// </summary>
        /// <param name="tag">要对比的标签</param>
        /// <returns></returns>
        public bool IsPartOfThisDocument(HtmlTag tag)
        {
            if (tag == null)
                return false;
            if (tag._document == _document)
                return true;
            return false;
        }

        #endregion

        #region 文档搜索部分


        /// <summary>
        /// 通过ID得到一个HTML对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回ID对应对象 找不到时返回NULL</returns>
        public HtmlTag GetElementById(string id)
        {
            if (_idDic.ContainsKey(id))
                return _idDic[id];
            return null;
        }

        /// <summary>
        /// 搜索此标签下的标签名为此标签名的标签集合
        /// </summary>
        /// <param name="tagName">小写的HTML标签</param>
        /// <returns>标签集合 当此标记没有回标记时返回NULL</returns>
        public List<HtmlTag> GetElementsByTagName(string tagName)
        {

            if (_backTag == null)
                return _document.EmptyHtmlTagList;
            List<HtmlTag> list = new List<HtmlTag>();
            HtmlTag tag = GetThisElement();
            while (tag != null)
            {
                if (tag._isBakcTag)
                {
                    tag = tag._nextNode;
                    continue;
                }

                if (tag._startIndex > _backTag._endIndex)
                    break;
                if (tag._tagName == tagName)
                    list.Add(tag);
                tag = tag._nextNode;
            }

            return list;

        }

        /// <summary>
        /// 得到有特定属性和特定属性值的标签对象集合
        /// </summary>
        /// <param name="attrName">小写属性名</param>
        /// <param name="attrValue">属性值</param>
        /// <returns>符合要求的对象集合</returns>
        public List<HtmlTag> GetElementsByAttribute(string attrName, string attrValue)
        {

            List<HtmlTag> list = new List<HtmlTag>();
            HtmlTag tag = GetThisElement();
            while (tag != null)
            {
                if (tag._isBakcTag)
                {
                    tag = tag._nextNode;
                    continue;
                }
                if (tag._startIndex > _backTag._endIndex)
                    break;
                if (tag._attrDic != null)
                {
                    if (tag._attrDic.ContainsKey(attrName))
                    {
                        if (tag._attrDic[attrName] == attrValue)
                            list.Add(tag);
                    }
                }
                tag = tag._nextNode;

            }
            return list;
        }

        /// <summary>
        /// 得到有特定属性的标签集合
        /// </summary>
        /// <param name="attrName">小写属性名</param>
        /// <returns>符合要求的对象集合</returns>
        public List<HtmlTag> GetElementsByAttributeName(string attrName)
        {
            List<HtmlTag> list = new List<HtmlTag>();
            HtmlTag tag = GetThisElement();
            while (tag != null)
            {
                if (tag._isBakcTag)
                {
                    tag = tag._nextNode;
                    continue;
                }
                if (tag._startIndex > _backTag._endIndex)
                    break;
                if (tag._attrDic != null)
                {
                    if (tag._attrDic.ContainsKey(attrName))
                    {
                        list.Add(tag);
                    }
                }
                tag = tag._nextNode;
            }
            return list;
        }

        /// <summary>
        /// 用Linq表达式搜索节点,为了高效，请尽量缩小搜索范围
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="range">搜索范围</param>
        /// <returns>搜索结果集合</returns>
        public IEnumerable<HtmlTag> Search(Func<HtmlTag, bool> predicate, TagSearchRange range)
        {
            if (range == TagSearchRange.ChildNodes)
                return ChildNodes.Where(predicate);
            if (range == TagSearchRange.AllElements)
                return All.Where(predicate);
            if (range == TagSearchRange.Body)
                return SearchTag(predicate, _document.Body);
            if (range == TagSearchRange.Head)
                return SearchTag(predicate, _document.Head);
            if (range == TagSearchRange.Document)
                return SearchTag(predicate, _document.HtmlElemnt);
            throw new ArgumentOutOfRangeException("range", string.Format("未处理参数：{0}", range.ToString()));

        }

        /// <summary>
        /// 搜索某一节点的数据
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="tag">要搜索的标签</param>
        /// <returns>结果集</returns>
        private IEnumerable<HtmlTag> SearchTag(Func<HtmlTag, bool> predicate, HtmlTag tag)
        {
            if (tag == null)
                return _document.EmptyHtmlTagList;
            return tag.All.Where(predicate);
        }

        #endregion

        #endregion

    }

    /// <summary>
    /// 搜索范围
    /// </summary>
    public enum TagSearchRange
    {
        /// <summary>
        /// 当前节点的子节点
        /// </summary>
        ChildNodes = 1,

        /// <summary>
        /// 当前节点下的所有元素
        /// </summary>
        AllElements = 2,

        /// <summary>
        /// 文档头
        /// </summary>
        Head = 3,

        /// <summary>
        /// 文档Body部分
        /// </summary>
        Body = 4,

        /// <summary>
        /// 当前文档
        /// </summary>
        Document = 5
    }
}
