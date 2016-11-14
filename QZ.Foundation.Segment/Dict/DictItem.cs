using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Segment.Dict
{
    /// <summary>
    /// 词条
    /// </summary>
    public class DictItem : IComparable<DictItem>
    {
        private IDictionary<char, int> _leftWords;
        public IDictionary<char, int> LeftWords { get { return _leftWords; } }
        public int LeftWordsNum { get; private set; }
        private IDictionary<char, int> _rightWords;
        public IDictionary<char, int> RightWords { get { return _rightWords; } }
        public int RightWordsNum { get; private set; }
        public long Frequency { get; set; }
        public double Entropy { get; set; }
        public double Mi { get; set; }
        public string Value { get; private set; }
        /// <summary>
        /// 一次性全局分析后得到的词典中词语总数
        /// </summary>
        public static long N = 0;

        public DictItem(string value)
        {
            Value = value;
            _leftWords = new Dictionary<char, int>();
            _rightWords = new Dictionary<char, int>();
        }

        public DictItem(string value, long frequency) : this(value) { Frequency = frequency; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="isLeft">true -> 左邻，false -> 右邻</param>
        public void AddNeighbourChar(char c, bool isLeft = true)
        {
            if (isLeft)
            {
                if (LeftWords.ContainsKey(c))
                    LeftWords[c] += 1;
                else
                    LeftWords[c] = 1;
                LeftWordsNum++;
            }
            else
            {
                if (RightWords.ContainsKey(c))
                    RightWords[c] += 1;
                else
                    RightWords[c] = 1;
                RightWordsNum++;
            }
        }

        public int CompareTo(DictItem other)
        {
            if (this.Frequency > other.Frequency)
                return -1;
            else if (this.Frequency < other.Frequency)
                return 1;
            else
                return 0;
        }

        public override string ToString() => $"DictItem[value={Value}, frequency={Frequency}, entropy={Entropy}, mi={Mi}]";
    }
}
