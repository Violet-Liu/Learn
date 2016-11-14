using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Request for query exhibit detail
    /// </summary>
    public class Req_Exhibit_Dtl
    {
        public string u_id;
        public string u_name;
        /// <summary>
        /// md5 16 of exhibition name
        /// </summary>
        public string e_md;
        /// <summary>
        /// count of exhibition magazine
        /// </summary>
        public int e_count;
        public int pg_index;
        public int pg_size;

        public static Req_Exhibit_Dtl Default() => new Req_Exhibit_Dtl() { u_id = "30740", u_name = "",pg_index = 1, pg_size=10, e_md = "9838b0fd98dd06f9" };
    }

    public class Req_Trade_UniversalSearch
    {
        public List<string> fwd_names;
        public List<string> exh_names;
        public List<string> gb_codes;
        public List<string> pro_codes;

        public string u_id;
        public string u_name;
        public int pg_size;
        public int pg_index;

        public static Req_Trade_UniversalSearch Default()
        {
            var s = new Req_Trade_UniversalSearch();
            s.fwd_names = new List<string>();
            s.exh_names = new List<string>();
            s.gb_codes = new List<string>();
            s.pro_codes = new List<string>();

            s.pg_index = 1;
            s.pg_size = 10;
            return s;
        }
    }
}
