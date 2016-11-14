using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Client
{
    public class ESAnalyzer
    {
        public List<FieldQuery> NaiveQueryAnalyze(string input)
        {
            var len = input.Length;
            var list = new List<FieldQuery>();

            // Firstly, it thinks that input is composed by pure alphanumbers
            bool alphanumber = true;
            // Once input had been verified to be not a alphanumbers, then it thinks that input is composed by ascii chars
            bool ascii = true;
            // Or else, input is composed by UTF8

            bool @at = false;   // if contains char '@'
            bool dot = false;   // if contains char dot

            foreach(var c in input)
            {
                if (!@at && c == '@')
                    @at = true;

                if (!dot && c == '.')
                    dot = true;

                if (alphanumber && !char.IsLetterOrDigit(c))
                {
                    alphanumber = false;
                    
                }
                else if (c > 127)
                {
                    ascii = false;
                    break;
                }
            }

            // !!! Following statements sequence is very important!

            
            list.Add(new FieldQuery("oc_brands") { Type = MatchType.MatchPhrase, Boost = 3 });

            if (ascii)
            {
                if (alphanumber)
                {
                    list.Add(new FieldQuery("oc_sites") { Type = MatchType.MatchPhrase });
                    if (len > 2)
                        list.Add(new FieldQuery("oc_name.py_oc_name") { Type = MatchType.Prefix });
                    if (len > 7)
                    {
                        list.Add(new FieldQuery("oc_code"));
                        list.Add(new FieldQuery("oc_number"));
                        list.Add(new FieldQuery("oc_creditcode"));
                    }
                    return list;
                }

                if (@at)
                {
                    list.Add(new FieldQuery("oc_mails") { Type = MatchType.MatchPhrase });
                    return list;
                }
                return list;
            }

            list.Add(new FieldQuery("oc_name.oc_name_raw"));
            list.Add(new FieldQuery("od_faren"));
            list.Add(new FieldQuery("oc_members") { Type=MatchType.MatchPhrase});
            list.Add(new FieldQuery("od_gds") { Type = MatchType.MatchPhrase});
            list.Add(new FieldQuery("oc_name") { Type=MatchType.MatchPhrase, NonStrict = true });
            list.Add(new FieldQuery("oc_areaname") { Type = MatchType.MatchPhrase });
            list.Add(new FieldQuery("oc_business") { Type = MatchType.Match });
            return list;
        }

        public void AdvancedAnalyze(string input)
        {

        }
    }

    /// <summary>
    /// Single field query metadata, which used to compose <seealso cref="QueryDescriptor"/>
    /// </summary>
    public class FieldQuery
    {
        public MatchType Type { get; set; } = MatchType.Term;
        public string Field { get; set; }
        public double Boost { get; set; }
        /// <summary>
        /// if match should be strict
        /// </summary>
        public bool NonStrict { get; set; }

        public FieldQuery(string field) { Field = field; }

    }
    public enum MatchType
    {
        MatchPhrase,
        Match,
        Prefix,
        Query,
        Term
    }
}
