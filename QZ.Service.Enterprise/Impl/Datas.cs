using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QZ.Instrument.Model;

namespace QZ.Service.Enterprise
{
    public class Datas
    {
        private static List<Trade> _trades;
        public static List<Trade> Trades
        {
            get
            {
                // This is not thread-safe, but it's value is always the same. Who cares?
                if(_trades == null)
                {
                    _trades = Instrument.Model.Constants.Primary_Trades.Select(p => new Trade(p.Key, p.Value)).ToList();
                    Company_Handle.TradeTree_Grow(_trades);
                }
                return _trades;
            }
        }

        private static List<ProCat> _pros;
        public static List<ProCat> Pros
        {
            get
            {
                if(_pros == null)
                {
                    var procats = DataAccess.ProductCategory_AllSelect();
                    _pros = Company_Handle.ProCatTree_Create(procats);
                }
                return _pros;
            }
        }

        //private byte CompanyStatus_Get(string od_ext)
        //{
        //    if (od_ext.Contains("存续"))
        //        return 2;
        //    if (od_ext.Contains("迁出"))
        //        return 3;
        //    if (od_ext.Contains("吊销"))
        //        return 4;
        //    if (od_ext.Contains("注销"))
        //        return 5;
        //    if (od_ext.Contains("停业"))
        //        return 6;
        //    if (od_ext.Contains("解散"))
        //        return 7;
        //    if (od_ext.Contains("清算"))
        //        return 8;
        //    return 1;
        //}
        private static IDictionary<byte, string> _companyStatus = new Dictionary<byte, string>()
        {
            [0] = "未知",
            [1] = "在业",
            [2] = "存续",
            [3] = "迁出",
            [4] = "吊销",
            [5] = "注销",
            [6] = "停业",
            [7] = "解散",
            [8] = "清算"
        };
        public static IDictionary<byte, string> CompanyStatus { get { return _companyStatus; } }
    }
}