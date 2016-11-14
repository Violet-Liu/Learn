using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Foundation.Utility.Dyn;
using QZ.Instrument.Utility;

using QZ.Test.Client;
using QZ.Instrument.Model;
using QZ.Instrument.DataAccess;
using QZ.Foundation.Model;

namespace QZ.Test.TopRobot
{
    class Program
    {
        #region initialize
        private static Simulator _simulator;
        private static Action<Action> _action;

        public static void Init()
        {
            _simulator = new Simulator();
            _action = act => { act(); Console.WriteLine(Global.Instance.Response); };
        }
        #endregion
        static void Main(string[] args)
        {
            //var st = "百度-瑞安市";
            //var set = ES_Search.SpanFirst_Test("百度");
            //Console.WriteLine($"span first term: {st}\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.oc_name + " | " + s.oc_code + " | " + s.od_faren);
            //}

            //var set = ES_Search.SpanTerm_Test("百度");
            //Console.WriteLine($"span term: {st}\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.oc_name + " | " + s.oc_code + " | " + s.od_faren);
            //}
            //var set = ES_Search.SpanNear_Test("聚美优品");
            //var set = ES_Search.SpanNot_Test("百度-瑞安市");
            //Console.WriteLine($"span not: {"百度-瑞安市"}\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.oc_name + " | " + s.oc_code + " | " + s.od_faren);
            //}
            //return;
            
            //var set = ES_Search.SpanNot_Test("百度-瑞安市");
            //Console.WriteLine($"span not: {"百度-瑞安市"}\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.oc_name + " | " + s.oc_code + " | " + s.od_faren);
            //}
            //var set = ES_Search.Regexp_Test("百.* ");
            //Console.WriteLine($"Regexp_Test: {"百度.*"}\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.oc_name + " | " + s.oc_code + " | " + s.od_faren);
            //}
            //return;

            //var list = new List<string>
            //{
            //    "00000004K",
            //    "10000018X",
            //    "1M00O018X",
            //    "Z00Z0Z18X",
            //    "722911092",
            //    "MMMMOOX8X"
            //};

            //foreach (var s in list)
            //{
            //    Console.WriteLine("raw: " + s);
            //    long v = ScalaInteger.ToFixValue(s);
            //    Console.WriteLine("translate: " + v);
            //    Console.WriteLine("anti translate: " + ScalaInteger.FromFixValue(v));
            //}
            //Console.WriteLine("--------------------------------");
            //foreach (var s in list)
            //{
            //    Console.WriteLine("raw: " + s);
            //    long v = ScalaInteger.ToVInt(s);
            //    Console.WriteLine("translate: " + v);
            //    Console.WriteLine("anti translate: " + ScalaInteger.FromVInt(v));
            //}
            //return;
            //ES_Search.ScrollSearch();
            //return;
            Init();
            //_action(() => _simulator.Index());
            _action(() => _simulator.CompanyTrade_IntelliTips());
            //_action(() => _simulator.ExtQuery_History());
            return;
            //var list = ES_Search.Advanced_Query();
            //foreach(var l in list)
            //{
            //    Console.WriteLine(l.oc_name + " | " + l.oc_weight);
            //}
            //return;
            

            //Console.WriteLine("\nsearch by PinYin:");
            //var set1 = ES_Search.Dishonest_Search(new Req_Info_Query() { query_str = "dys", pg_index = 1, pg_size = 5 });
            //Console.WriteLine("query string: dys\n_______________________________");
            //foreach (var s in set)
            //{
            //    Console.WriteLine(s.sx_oc_name + " | " + s.sx_pname + " | " + s.sx_cardnum);
            //}

            //Init();

            //Company_Query();
            //CompanyTrade_IntelliTips();

            //CompanyTrade_UniversalSearch();
        }


        

        public static void CompanyTrade_UniversalSearch() => _action(() => _simulator.CompanyTrade_UniversalSearch());
        public static void CompanyTrade_IntelliTips() => _action(() => _simulator.CompanyTrade_IntelliTips());
        public static void Company_Query() => _action(() => _simulator.Company_Query());
    }
}
