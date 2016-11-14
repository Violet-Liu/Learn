using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Segment.PanGu
{
    public class WordAttr
    {
        public string value;
        public WordFstType type;

        public WordAttr() { }
        public WordAttr(string value, WordFstType type)
        {
            this.value = value;
            this.type = type;
        }
        public override string ToString() => value;
    }

    public enum WordFstType
    {
        None = 0,
        English = 1,
        Chinese = 2,
        Numeric = 4,
        Symbol = 8
    }

    public enum WordSndType
    {
        PersonName = 1,
        CompanyName = 2,

    }
}
