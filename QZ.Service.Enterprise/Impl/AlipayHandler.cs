using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using QZ.Instrument.Common;
using QZ.Instrument.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace QZ.Service.Enterprise
{
    public class AlipayHandler
    {
        //callback url
        private static string alipay_notify_url = ConfigurationManager.AppSettings["alipay_notify_url"];
        //cooperation num  
        private static string partner = "2088801713874834";
        //alipay account
        private static string seller_id = "pay@qianzhan.com";
        // appID
        private static string app_id = "2016122804686658";//"2016122804686658";

        private static string ALI_PUBLIC_KEY = AppDomain.CurrentDomain.BaseDirectory + "pem\\alipay_public_key.pem";
        private static string APP_PRIVATE_KEY = AppDomain.CurrentDomain.BaseDirectory + "pem\\rsa_private_key.pem";
        private static string ALIPAY_PUBLIC_KEY = AppDomain.CurrentDomain.BaseDirectory + "pem\\rsa_public_key.pem";

        public static string Signature(VipUserOrderInfo info)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string biz_content = string.Empty;

            StringBuilder builder = new StringBuilder();
            builder.Append("app_id=");
            builder.Append(HttpUtility.UrlEncode(app_id));
            builder.Append("&biz_content=");
            biz_content = "{\"timeout_express\":\"30m\",\"seller_id\":\"";
            biz_content += seller_id;
            biz_content += "\",";
            biz_content += "\"product_code\":\"QUICK_MSECURITY_PAY\",";
            biz_content += "\"total_amount\":\"" + info.mo_money.ToString("f2") + "\",";
            biz_content += "\"subject\":\"" + "企业查询宝会员" + "\",";
            biz_content += "\"body\":\"" + "企业查询宝会员" + "\",";
            biz_content += "\"out_trade_no\":\"" + info.mo_orderid + "\"}";
            biz_content = HttpUtility.UrlEncode(biz_content);
            builder.Append(biz_content);
            builder.Append("&charset=" + HttpUtility.UrlEncode("utf-8"));
            builder.Append("&method=" + HttpUtility.UrlEncode("alipay.trade.app.pay"));
            builder.Append("&notify_url=");
            builder.Append(HttpUtility.UrlEncode(alipay_notify_url));
            builder.Append("&sign_type=" + HttpUtility.UrlEncode("RSA"));
            builder.Append("&timestamp=");
            builder.Append(HttpUtility.UrlEncode(timestamp).Replace("+", " "));
            builder.Append("&version=" + HttpUtility.UrlEncode("1.0"));
            string Signature = AlipaySignature.RSASign(SignatureContent(info, timestamp), APP_PRIVATE_KEY, "utf-8", "RSA");
            builder.Append("&sign=");
            builder.Append(HttpUtility.UrlEncode(Signature));

            return builder.ToString();
        }

        public static string SignatureContent(VipUserOrderInfo info, string timestamp)
        {
            string biz_content = string.Empty;
            StringBuilder builder = new StringBuilder();
            builder.Append("app_id=");
            builder.Append(HttpUtility.UrlEncode(app_id));
            builder.Append("&biz_content=");
            biz_content = "{\"timeout_express\":\"30m\",\"seller_id\":\"";
            biz_content += seller_id;
            biz_content += "\",";
            biz_content += "\"product_code\":\"QUICK_MSECURITY_PAY\",";
            biz_content += "\"total_amount\":\"" + info.mo_money.ToString("f2") + "\",";
            biz_content += "\"subject\":\"" + "企业查询宝会员" + "\",";
            biz_content += "\"body\":\"" + "企业查询宝会员" + "\",";
            biz_content += "\"out_trade_no\":\"" + info.mo_orderid + "\"}";
            builder.Append(biz_content);
            builder.Append("&charset=utf-8");
            builder.Append("&method=alipay.trade.app.pay");
            builder.Append("&notify_url=");
            builder.Append(alipay_notify_url);
            builder.Append("&sign_type=RSA");
            builder.Append("&timestamp=");
            builder.Append(timestamp);
            builder.Append("&version=1.0");
            return builder.ToString();
        }

        public static AlipayTradeQueryResponse alipay_trade_query(string out_trade_no, string trade_no)
        {
            IAopClient client = new DefaultAopClient(
                "https://openapi.alipay.com/gateway.do",
                app_id,
                APP_PRIVATE_KEY,
                "json",
                "1.0",
                "RSA",
                ALI_PUBLIC_KEY,
                "utf-8",
                true);
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "    \"out_trade_no\":\"" + out_trade_no + "\"," +
            "    \"trade_no\":\"" + trade_no + "\"" +
            "  }";
            AlipayTradeQueryResponse response = client.Execute(request);
            return response;
        }

        public static AlipayReturnData GetNotifyData(string Query)
        {
            AlipayReturnData data = new AlipayReturnData();
            //string Query = HttpContext.Current.Request.Url.Query;
            //Query = HttpUtility.UrlDecode(Query);
            //LogHelper.Info("Post过来的参数：" + Query);
            Dictionary<string, string> paras = new Dictionary<string, string>();
            var coll = BuildParams(Query);
            String[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                paras.Add(requestItem[i], coll[requestItem[i]]);
            }
            //验证签名
            if (paras.Keys.Count > 0)
            {
                bool aSignature = AlipaySignature.RSACheckV1(paras, ALI_PUBLIC_KEY, "utf-8");
                if (aSignature)
                {
                    data.app_id = paras.ContainsKey("app_id") ? paras["app_id"] : string.Empty;
                    data.body = paras.ContainsKey("body") ? paras["body"] : string.Empty;
                    data.buyer_id = paras.ContainsKey("buyer_id") ? paras["buyer_id"] : string.Empty;
                    data.gmt_create = paras.ContainsKey("gmt_create") ? paras["gmt_create"] : string.Empty;
                    data.notify_id = paras.ContainsKey("notify_id") ? paras["notify_id"] : string.Empty;
                    data.notify_time = paras.ContainsKey("notify_time") ? paras["notify_time"] : string.Empty;
                    data.notify_type = paras.ContainsKey("notify_type") ? paras["notify_type"] : string.Empty;
                    data.gmt_payment = paras.ContainsKey("gmt_payment") ? paras["gmt_payment"] : string.Empty;
                    data.out_trade_no = paras.ContainsKey("out_trade_no") ? paras["out_trade_no"] : string.Empty;
                    data.seller_id = paras.ContainsKey("seller_id") ? paras["seller_id"] : string.Empty;
                    data.subject = paras.ContainsKey("subject") ? paras["subject"] : string.Empty;
                    data.total_amount = paras.ContainsKey("total_amount") ? paras["total_amount"] : string.Empty;
                    data.trade_no = paras.ContainsKey("trade_no") ? paras["trade_no"] : string.Empty;
                    data.trade_status = paras.ContainsKey("trade_status") ? paras["trade_status"] : string.Empty;
                    data.sign = paras.ContainsKey("sign") ? paras["sign"] : string.Empty;
                    data.sign_type = paras.ContainsKey("sign_type") ? paras["sign_type"] : string.Empty;
                    LogHelper.Info("异步通知验证签名成功");
                }
                else
                {
                    LogHelper.Info("异步通知验证签名失败");
                }
            }
            else
            {
                LogHelper.Info("异步通知的参数为空");
            }

            return data;

        }

        public static Dictionary<string, string> GetRequestPost()
        {
            int i = 0;
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = HttpContext.Current.Request.Form;
            LogHelper.Info(coll.Count.ToString());
            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;
            LogHelper.Info("异步通知参数开始");
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }
            LogHelper.Info("异步通知参数结束");
            return sArray;
        }

        private static NameValueCollection BuildParams(string QureyPara)
        {
            return System.Web.HttpUtility.ParseQueryString(QureyPara);

        }

        public static bool ValidationApp_id(string id)
        {
            return app_id == id;
        }

    }
}