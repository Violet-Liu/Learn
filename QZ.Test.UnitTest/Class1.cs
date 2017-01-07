using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Xunit;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;
using QZ.Test.Client;
namespace QZ.Test.UnitTest
{
    public class Grosser
    {
        private static Simulator _simulator;
        private static Action<Action> _action;

        [Fact]
        public void TestXmlSerializer()
        {
            //string path = @"F:\Projects\company\QZ.NewSite.CorpInfoService\QZ.Service.Enterprise\Service.config";

            //Services model = path.Deserialize<Services>();
            //Assert.NotEqual(null, model);
            string input = null;
            var s = input.To_Sql_Safe();
            Assert.Equal(null, s);
        }

        [Fact]
        public void DateTime_Test()
        {
            var min = DateTime.MinValue;
            var some = DateTime.Parse("1900-01-01");
            Assert.Equal(1900, some.Year);
            Assert.Equal(1, some.Month);

            
        }

        [Fact]
        public void ListTest()
        {
            var list = new List<string>(0x100);
            var count = list.Count;
            var l = list[0x10];
            Assert.Equal(l, null);
        }

        [Fact]
        public void CompanyTrade_Search()
        {
            _simulator = new Simulator();
            _action = act => { act(); Console.WriteLine(Global.Instance.Response); };
            _action(() => _action(() => _simulator.Favorites()));
        }

        [Fact]
        public void ES_Test()
        {
            ES_Search.Dishonest_Test("深圳市腾讯计算机系统有限公司", 10);
        }
    }
}
