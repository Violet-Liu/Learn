using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.Model
{
    public class ES_Exhibit
    {
        public int e_showid { get; set; }
        public int e_year { get; set; }
        public string e_name { get; set; }
        public string e_trade { get; set; }
        public string e_hall { get; set; }
        public DateTime e_start { get; set; }
        public int e_count { get; set; }
        public string e_namemd { get; set; }

        
    }
}
