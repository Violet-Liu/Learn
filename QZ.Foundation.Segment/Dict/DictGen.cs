using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QZ.Foundation.Segment.Dict
{
    /// <summary>
    /// 词典生成器
    /// 考虑到不同的领域有独特的规则，所以此类方法设计为实例方法
    /// 目前先考虑前瞻行业的词典生成
    /// </summary>
    public class DictGen
    {
        public const int MAX_WORD_LEN = 4;

        private string _text;

        // 先知词典
        // key为词条，value表示是否出现过
        private IDictionary<string, bool> _preDict = new Dictionary<string, bool>
        {
            ["互联网+"] = false
        };

        public DictGen(string text)
        {
            if (text == null || text.Length < 1) throw new ArgumentNullException("text can not be null or empty string");
            _text = text;
        }

        public DictGen(List<string> list)
        {
            if (list == null || list.Count < 1) throw new ArgumentNullException("string list must has one item at least");
            bool first = true;
            var sb = new StringBuilder(list.Count * 8);
            foreach(var l in list)
            {
                if(first)
                {
                    first = false;
                    sb.Append(l);
                }
                else
                {
                    sb.Append("|").Append(l);
                }
            }
            _text = sb.ToString();
        }

        public void GenDict2File(string path)
        {
            if (!File.Exists(path))
                File.Create(path);

            int len = _text.Length;
            int count = 0;
            for(int i = 0; i < len; i++)
            {
                var c = HandleOnlyPunctuate(_text[i]);

                // j表示候选词长度
                for(int j = 1; i + j <= len && j <= MAX_WORD_LEN; j++)
                {
                    //if()
                }
            }
        }

        private char HandleOnlyPunctuate(char c)
        {
            if (c == '‘' || c == '’')
                return '\'';
            if (c == '；')
                return ';';
            if (c == '“' || c == '”')
                return '"';
            if (c == '、')
                return '/';
            if (c == '：')
                return ':';
            return c;
        }
    }
}
