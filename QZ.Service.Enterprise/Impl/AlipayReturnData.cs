using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZ.Service.Enterprise
{
    public class AlipayReturnData
    {
        public string app_id { get; set; }

        public string body { get; set; }

        public string buyer_id { get; set; }

        public string gmt_create { get; set; }

        public string notify_id { get; set; }

        public string notify_time { get; set; }

        public string notify_type { get; set; }

        public string gmt_payment { get; set; }

        public string out_trade_no { get; set; }

        public string seller_id { get; set; }

        public string subject { get; set; }

        public string total_amount { get; set; }

        public string trade_no { get; set; }

        public string trade_status { get; set; }

        public string sign { get; set; }

        public string sign_type { get; set; }
    }
}