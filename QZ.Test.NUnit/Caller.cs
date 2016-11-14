using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using QZ.Test.Client;
using QZ.Instrument.Model;
using QZ.Instrument.DataAccess;
namespace QZ.Test.NUnit
{
    [TestFixture]
    public class Tester
    {
        #region initialize
        private Simulator _simulator;
        private Action<Action> _action;
        [SetUp]
        public void Init()
        {
            _simulator = new Simulator();
            _action = act => { act(); Console.WriteLine(Global.Instance.Response); };
        }
        #endregion

        #region company
        [Test]
        public void Index() => _simulator.Index();
        [Test]
        public void Company_Trades() => _action(() => _simulator.Company_Trades());
        [Test]
        public void Area() => _action(() => _simulator.Area());
        [Test]
        public void Company_Query() => _action(() => _simulator.Company_Query());
        [Test]
        public void Company_Detail() => _action(() => _simulator.Company_Detail());
        [Test]
        public void Company_Intelli_Tip() => _action(() => _simulator.Company_Intelli_Tip());
        [Test]
        public void Company_Invest() => _action(() => _simulator.Company_Invest());
        [Test]
        public void Company_Report_Send() => _action(() => _simulator.Company_Report_Send());
        [Test]
        public void Company_Map() => _action(() => _simulator.Company_Map());
        [Test]
        public void Company_Stock_Holder() => _action(() => _simulator.Company_Stock_Holder());
        [Test]
        public void Company_Change() => _action(() => _simulator.Company_Change());
        [Test]
        public void Company_Icpl() => _action(() => _simulator.Company_Icpl());
        [Test]
        public void Company_Branch() => _action(() => _simulator.Company_Branch());
        [Test]
        public void Company_Annual() => _action(() => _simulator.Company_Annual());
        [Test]
        public void Company_Annual_Detail() => _action(() => _simulator.Company_Annual_Detail());
        [Test]
        public void Company_Impression() => _action(() => _simulator.Company_Impression());
        [Test]
        public void Company_New_Topic() => _action(() => _simulator.Company_New_Topic());
        [Test]
        public void Company_Reply() => _action(() => _simulator.Company_Reply());
        [Test]
        public void Company_Fresh_Topic() => _action(() => _simulator.Company_Fresh_Topic());
        [Test]
        public void Company_Topic_Query() => _action(() => _simulator.Company_Topic_Query());
        [Test]
        public void Company_Topic_Detail() => _action(() => _simulator.Company_Topic_Detail());
        [Test]
        public void Company_Favorite_Add() => _action(() => _simulator.Company_Favorite_Add());
        [Test]
        public void Company_Favorite_Remove() => _action(() => _simulator.Company_Favorite_Remove());
        [Test]
        public void Query_Hot() => _action(() => _simulator.Query_Hot());
        [Test]
        public void Company_Topic_UpDown_Vote() => _action(() => _simulator.Company_Topic_UpDown_Vote());
        [Test]
        public void Company_UpDown_Vote() => _action(() => _simulator.Company_UpDown_Vote());
        [Test]
        public void Company_Correct() => _action(() => _simulator.Company_Correct());
        [Test]
        public void Brand_Query() => _action(() => _simulator.Brand_Query());
        //[Test]
        //public void ExtQuery_Hot() => _action(() => _simulator.ExtQuery_Hot());
        [Test]
        public void Oc_Brand_Get() => _action(() => _simulator.Oc_Brand_Get());
        [Test]
        public void Oc_Patent_Get() => _action(() => _simulator.Oc_Patent_Get());
        [Test]
        public void Oc_Dishonest_Get() => _action(() => _simulator.Oc_Dishonest_Get());
        [Test]
        public void Oc_Judge_Get() => _action(() => _simulator.Oc_Judge_Get());


        [Test]
        public void CompanyTrade_Search() => _action(() => _simulator.CompanyTrade_Search());

        [Test]
        public void Patent_Dtl() => _action(() => _simulator.Patent_Dtl());
        [Test]
        public void Brand_Dtl() => _action(() => _simulator.Brand_Dtl());
        [Test]
        public void Dishonest_Dtl() => _action(() => _simulator.Dishonest_Dtl());
        [Test]
        public void Judge_Dtl() => _action(() => _simulator.Judge_Dtl());
        [Test]
        public void Patent_Query() => _action(() => _simulator.Patent_Query());
        [Test]
        public void Patent_Universal_Query() => _action(() => _simulator.Patent_Universal_Query());
        [Test]
        public void Judge_Query() => _action(() => _simulator.Judge_Query());
        [Test]
        public void Dishonest_Query() => _action(() => _simulator.Dishonest_Query());
        [Test]
        public void ExtQuery_Hot() => _action(() => _simulator.ExtQuery_Hot());




        #endregion

        #region community
        [Test]
        public void Community_Topic_Add() => _action(() => _simulator.Community_Topic_Add());
        [Test]
        public void Community_Reply() => _action(() => _simulator.Community_Reply());
        [Test]
        public void Community_Topic_Query() => _action(() => _simulator.Community_Topic_Query());
        [Test]
        public void Community_Topic_Detail() => _action(() => _simulator.Community_Topic_Detail());
        [Test]
        public void Community_Topic_UpDown_Vote() => _action(() => _simulator.Community_Topic_UpDown_Vote());
        #endregion

        #region user
        [Test]
        public void Login() => _action(() => _simulator.Login());
        [Test]
        public void Verify_Code_Get() => _action(() => _simulator.Verify_Code_Get());
        [Test]
        public void Register() => _action(() => _simulator.Register());
        [Test]
        public void Pwd_Reset() => _action(() => _simulator.Pwd_Reset());
        [Test]
        public void Face_Reset() => _action(() => _simulator.Face_Reset());
        [Test]
        public void Info_Set() => _action(() => _simulator.Info_Set());
        [Test]
        public void Info_Get() => _action(() => _simulator.Info_Get());
        [Test]
        public void Query() => _action(() => _simulator.Query());
        [Test]
        public void Query_Delete() => _action(() => _simulator.Query_Delete());
        [Test]
        public void Query_Drop() => _action(() => _simulator.Query_Drop());
        [Test]
        public void Browse_Get() => _action(() => _simulator.Browse_Get());
        [Test]
        public void Browse_Delete() => _action(() => _simulator.Browse_Delete());
        [Test]
        public void Browse_Drop() => _action(() => _simulator.Browse_Drop());
        [Test]
        public void ExtQuery_History() => _action(() => _simulator.ExtQuery_History());
        [Test]
        public void Favorites() => _action(() => _simulator.Favorites());
        [Test]
        public void Notices_Get() => _action(() => _simulator.Notices_Get());
        #endregion
        [Test]
        public void Calc()
        {
            double branch_expect = 8918559 / 15727440.0;
            Console.WriteLine(branch_expect);
        }


        [Test]
        public void StringReplace()
        {
            //var path = @"D:\source\forward.txt";

            var list = _simulator.ForwardTrade_Get();

            using (var access = new QZOrgCompanyAppAccess("QZBase"))
            {
                foreach(var l in list)
                {
                    var t = new ForwardTrade();
                    t.ft_date = DateTime.Now;
                    t.ft_name = l;
                    t.ft_hashcode = l.GetHashCode();

                    access.ForwardTrade_Insert(t);
                }
            }

            //var str = "核技术应用市场; 环境监测仪器; 核电设备; 智能电表; 电力建设; 军工; 物联网; 传感器制造; 智能制造装备; IPO";

            //var segs = str.Split(';');
            //foreach (var s in segs)
            //{
            //    var ss = s.Trim();
            //    Console.WriteLine(list.IndexOf(ss));
            //}
        }
    }
    
}
