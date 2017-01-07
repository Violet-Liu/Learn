/**
 * Implementation of all company services
 * 
 * In the future, service interfaces in Enterprise.svc will be moved from ServiceImpl into this file
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;

using QZ.Foundation.Cache;
using QZ.Foundation.Utility;
using QZ.Foundation.Model;
using QZ.Instrument.Client;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using System.Text.RegularExpressions;

namespace QZ.Service.Enterprise
{
    public class CompanyImpl
    {
        #region index
        public static Response Process_Index(Request request)
        {
            var pre_Ei = request.Simple_Preprocess(Constants.C_Dyn_0);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var req_body = request.Body_Get<Req_User>(Constants.C_Dyn_0);

            req_body.DoWhen(b => !string.IsNullOrEmpty(b.u_idfa), fu => DataAccess.User_Ifda_Insert(fu.u_idfa));
            req_body.DoWhen(b => b.u_id > 0 && !string.IsNullOrWhiteSpace(b.u_clientID), fu => DataAccess.User_ClientID_Insert(fu.u_id, fu.u_clientID));

            var user_Mb = req_body.Where(u => !string.IsNullOrEmpty(u.u_name))
                                 .Select(u => DataAccess.User_FromName_Select(u.u_name).ToMaybe()); // get user according to the user name


            var token = user_Mb.HasValue        // generate a token value
                ? new Token(pre_Ei.Right, user_Mb.Value.u_id.ToString(), user_Mb.Value.u_name).Compose(t => Cipher_Md5.Md5_16(t)).Induce()
                : new Token(pre_Ei.Right).Compose(t => Cipher_Md5.Md5_16(t)).Induce();

            var head = new Response_Head().ToJson();
            var body = Resp_Index.Resp_Indices.First().SetToken(token).ToJson();

            var response = new Response(head.ToEncryption(EncryptType.PT | EncryptType.ResetKey), body.ToEncryption(EncryptType.AES));//.ToJson();
            return response;
        }

        public static Response Process_Index_Pics()
        {
            var pics_mb = CacheMarker.GetCMSBlocksInfo("App_Header_Pic", "Focus").ToMaybe()     // get info of the block with a path of 'App_Header_Pic.Focus'. Refer to behind website for more details
                                     .Select(block => DataAccess_News.CMSItems_SelectTopN2(block.blk_id, 5, "").ToMaybe())
                                     .Select(items => items.Select(item => new Index_Pic() { href = item.n_linkUrl, img_src = item.n_imageUrl, title = item.n_title }).ToList().ToMaybe());

            var body = pics_mb.HasValue ? pics_mb.Value.ToJson() : new List<Index_Pic>().ToJson();
            var response = Util.Normal_Resp_Create(body, EncryptType.PT);
            return response;
        }
        #endregion

        #region query for company list
        /// <summary>
        /// Query for hot company list which is pushed by natural people via behind website
        /// </summary>
        /// <param name="pg_size"></param>
        /// <returns></returns>
        public static Response Process_Query_Hot(string pg_size)
        {
            var resp_Mb = DataAccess.CMSPagesInfo_FromUid_Get("Corp_HotSearch").ToMaybe()
                .Select(p => DataAccess.CMSItems_FromPgid_Select(p.pg_id, pg_size.ToInt()).ToMaybe())   //
                .Select<List<Query_Hot>>(items =>
                {
                    var hot_list = items.Select(i => Hot_Company_Transfer(i, ConfigurationManager.AppSettings["code_key"])).Where(c => !string.IsNullOrEmpty(c.oc_name)).ToList();
                    if (hot_list.Count < 1)
                        return null;
                    return hot_list;
                });

            var body = resp_Mb.HasValue ? resp_Mb.Value.ToJson() : Resp_Query_Hot.Default.ToJson();
            var response = Util.Normal_Resp_Create(body);

            return response;
        }

        private static Query_Hot Hot_Company_Transfer(CMSItemsInfo info, string code_key)
        {
            var hot = new Query_Hot() { title = info.n_title };
            var fields = info.n_linkUrl.Split('/');
            var len = fields.Length;
            var field = fields[len - 1];
            hot.oc_code = field.Substring(0, field.Length - 5).To_Occode(code_key);

            var c = DataAccess.OrgCompanyList_Select(hot.oc_code);
            if (c != null)
            {
                hot.oc_area = c.oc_area;
                hot.oc_name = c.oc_name;
            }
            return hot;
        }
        #endregion

        public static Response Process_Company_TradeInfos()
        {
            //object o = CacheHelper.Cache_Get(Constants.Trades_Cache_Id);
            //if (o != null)
            //    return (Response)o;

            //var gb_trades = Instrument.Model.Constants.Primary_Trades.Select(p => new Trade(p.Key, p.Value)).ToList();
            //Company_Handle.TradeTree_Grow(gb_trades);
            var trades = new Trades() { gb_trades = Datas.Trades };
            var response = new Response(string.Empty, trades.ToJson().ToGzip());//Util.Normal_Resp_Create(trades.ToJson().ToGzip(), EncryptType.PT);
            //CacheHelper.Cache_Store(Constants.Trades_Cache_Id, response, TimeSpan.FromDays(1));
            return response;
        }

        public static Response Process_Company_ProInfos()
        {
            var trades = new Trades() { pro_trades = Datas.Pros };
            var response = new Response(string.Empty, trades.ToJson().ToGzip());//Util.Normal_Resp_Create(trades.ToJson().ToGzip(), EncryptType.PT);
            //CacheHelper.Cache_Store(Constants.Trades_Cache_Id, response, TimeSpan.FromDays(1));
            return response;
        }

        public static Response Process_Company_FavoriteScan(Request request)
        {
            var pre_mb = request.Preprocess2Maybe(true);
            if (pre_mb.HasValue)
                return pre_mb.Value;

            var req_body = request.GetBody<Req_Cm_Topic>().User_Valid_Check();
            var browses_mb = req_body.Select(q => q.Browses_Get().ToMaybe());
            var favorites_mb = req_body.Select(q => q.Favorites_Get().favorite_list.ToMaybe());

            var browses = browses_mb.HasValue ? browses_mb.Value : null;
            var favorites = favorites_mb.HasValue ? favorites_mb.Value : null;
            var count = req_body.HasValue ? req_body.Value.pg_size : 5;
            var list = ResponseAdaptor.CompanyFavorBrowse_Compose(browses, favorites, count, req_body.Value.u_id.ToInt());

            var response = Util.Normal_Resp_Create(list.ToJson(), EncryptType.AES);
            return response;
        }

        /// <summary>
        /// Search via special trade
        /// <b>Note:</b> this function can not specifilize any search condition but trade code
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Response Process_Company_TradeSearch(Request request) => Company_SearchByTradeCode(request, Es_Consts.Company_GBTrade);
        public static Response Process_Company_ProSearch(Request request) => Company_SearchByTradeCode(request, Es_Consts.Company_ProTrade);

        private static Response Company_SearchByTradeCode(Request request, string fieldName)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var req_body = request.GetBody<Req_TradeSearch>();

            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(req_body.u_id).Set_Uname(req_body.u_name).Set_Action(Constants.Op_Trade_Search + fieldName);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list = ResponseAdaptor.TradeSearch2CompanyList(ESClient.Company_TradeCodeSearch(req_body, fieldName));
            var response = Util.Normal_Resp_Create(list.ToJson(), EncryptType.AES | EncryptType.Gzip);
            return response;
        }

        public static Response Process_Company_FwdTradeSearch(Request request) => Company_SearchByTradeName(request, TradeQueryType.forward);

        public static Response Process_Company_ExhTradeSearch(Request request) => Company_SearchByTradeName(request, TradeQueryType.exhibit);


        private static Response Company_SearchByTradeName(Request request, TradeQueryType type)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var req_body = request.GetBody<Req_TradeSearch>();
            if (string.IsNullOrWhiteSpace(req_body.trd_name))
            {
                var head = new Response_Head();
                head.Action = Message_Action.Logic_Err;
                head.Text = "查询字符串不能为空";
                var headJson = head.ToJson().ToEncryption(EncryptType.PT);
                return new Response(headJson, "");
            }

            string fieldName = type == TradeQueryType.forward ? Es_Consts.Company_FwdTrade : Es_Consts.Company_ExhTrade;

            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(req_body.u_id).Set_Uname(req_body.u_name).Set_Action(Constants.Op_Trade_Search + fieldName);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);



            var query = new Req_Info_Query();
            query.Type_Set((byte)((int)type & 0x11100));
            query.u_id = req_body.u_id;
            query.u_name = req_body.u_name;
            query.query_str = req_body.trd_name;

            DataAccess.SearchHistoryExt_Insert(query, query.u_id.ToInt() > 0);

            var list = ResponseAdaptor.TradeSearch2CompanyList(ESClient.Company_TradeNameSearch(req_body, fieldName));
            var response = Util.Normal_Resp_Create(list.ToJson(), EncryptType.AES | EncryptType.Gzip);
            return response;
        }

        public static Response Process_Company_TradeIntelliTip(Request request)
        {
            var pre_mb = request.Preprocess2Maybe(true, false);
            if (pre_mb.HasValue)
                return pre_mb.Value;
            var head = new Response_Head();

            var res_mb = request
                .GetBody<Req_Intelli_Tip>().ToMaybe()
                .Where(t => !string.IsNullOrWhiteSpace(t.input) && t.input.Trim().Length > 1)
                .Do(p => DataAccess.SearchHistoryExt_Insert(new Req_Info_Query() { u_id = p.u_id, u_name = p.u_name, query_str = p.input }.Type_Set(30), p.u_id.ToInt() > 0))
                .Select<AnalysesResult>(t => CompanyTrade_Proxy.AnalysisAllTrade(t.input, t.pg_size < 1 ? 5 : t.pg_size))
                .Select<Trade_Intelli_Tip>(ar => ar.AnalysisResult2TradeTip());

            var body = res_mb.HasValue ? res_mb.Value.ToJson() : Trade_Intelli_Tip.Default().ToJson();
            
            var response = Util.Normal_Resp_Create(head.ToJson(), body, EncryptType.AES | EncryptType.Gzip);

            return response;
        }

        public static Response Process_Company_UniversalTradeSearch(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var req_body = request.GetBody<Req_Trade_UniversalSearch>();
            var res_mb = req_body.ToMaybe()
                .DoWhen(s => s.pg_index < 1, s => s.pg_index = 1)
                .Select(s => ESClient.Company_TradeUniversalSearch(s).ToMaybe())
                .Select(r => ResponseAdaptor.TradeSearch2CompanyList(r, req_body).ToMaybe());

            var body = res_mb.HasValue ? res_mb.Value : new Resp_Company_List() { oc_list = new List<Resp_Oc_Abs>() };
            var response = Util.Normal_Resp_Create(body.ToJson(), EncryptType.AES | EncryptType.Gzip);
            return response;
        }

        public static Response Process_Company_Search4Exhibit(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            //ServiceImpl.Process_ExtQuery_Hot("31", "5");

            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Exhibit_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var res_mb = request.GetBody<Req_Info_Query>().ToMaybe()
                .DoWhen(s => s.pg_index < 1, s => s.pg_index = 1)
                .DoWhen(b=>!string.IsNullOrEmpty(b.query_str),p => DataAccess.SearchHistoryExt_Insert(p.Type_Set(31), p.u_id.ToInt() > 0))
                .Select(s => ESClient.Exhibit_Search(s).ToMaybe())
                .Select(r => ResponseAdaptor.ExhibitSearch2List(r).ToMaybe());

            var body = res_mb.HasValue ? res_mb.Value : new Resp_Exhibit_List() { exhibits = new List<SearchExhibit>() };
            var response = Util.Normal_Resp_Create(body.ToJson(), EncryptType.AES | EncryptType.Gzip);
            return response;
        }

        public static Response Process_Company_Query(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Company>(); // get request body


            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Ent_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var head = new Response_Head();

            var resp_List_Mb = request_Body.ToMaybe()
                .DoWhenButNone(b => b.q_type == q_type.q_general && string.IsNullOrWhiteSpace(b.oc_name), _ => { head.Action = Message_Action.Logic_Err; head.Text = "请输入非空白字符"; })
                .DoWhenButNone(b => b.q_type != q_type.q_general && Company.Invalid_Get(b), _ => { head.Action = Message_Action.Logic_Err; head.Text = "请输入有效条件"; })
                .Select(b => b.CompanyList_Query());   // process request body



            var body = resp_List_Mb.HasValue ? resp_List_Mb.Value.ToJson() : Resp_Company_List.Default.ToJson();
            var response = Util.Normal_Resp_Create(head.ToJson(), body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }


        public static Response Process_Comment_TipOff(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true, true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Cmt_TipOff>(); // get request body
            if (!string.IsNullOrEmpty(request_Body.accused_uid))
            {
                // create user operation log and insert it into database
                var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.accused_uid).Set_Uname(request_Body.accused_uname).Set_Action(Constants.Op_Ent_Query);
                DataAccess.AppOrgCompanyLog_Insert(op_Log);
            }
            var head = new Response_Head();

            // 二值状态响应的Maybe形式
            var resp_bin_mb = request_Body.ToMaybe()
                .DoWhenButNone(b => string.IsNullOrEmpty(b.accused_uid), _ => { head.Action = Message_Action.Logic_Err; })
                .DoWhenButNone(b => b.cmt_id < 1 || b.cmt_type < 1, _ => head.Action = Message_Action.Logic_Err)
                .DoWhenButNone(b => string.IsNullOrEmpty(b.to_des), _ => { head.Action = Message_Action.Logic_Err; head.Text = "请输入举报原因"; })
                .Select(b => b.Cmt_TipOff_Handle().ToMaybe());   // process request body



            var body = resp_bin_mb.HasValue ? resp_bin_mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Util.Normal_Resp_Create(head.ToJson(), body, EncryptType.PT);

            return response;
        }

        public static Response Process_Comment_Shield(Request request)
        {
            var pre_mb = request.Preprocess2Maybe(true, true);
            if (pre_mb.HasValue)
                return pre_mb.Value;

            var request_Body = request.GetBody<Req_Cmt_Shield>(); // get request body

            var head = new Response_Head();

            var resp_bin_mb = request_Body.ToMaybe()
                .DoWhenButNone(b => string.IsNullOrEmpty(b.u_id), _ => head.Action = Message_Action.Logic_Err)
                .DoWhenButNone(b => b.cmt_id < 1 || b.cmt_type < 1, _ => head.Action = Message_Action.Logic_Err)
                .Select(b => b.Cmt_Shield_Handle().ToMaybe());

            var body = resp_bin_mb.HasValue ? resp_bin_mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Util.Normal_Resp_Create(head.ToJson(), body, EncryptType.PT);



            return response;
        }

        public static Response Process_Company_Exhibit_Participate(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Mini>(); // get request body


            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Exhibit_List);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            //var head = new Response_Head();

            var resp_List_Mb = request_Body.ToMaybe()
                .Where(b => !string.IsNullOrWhiteSpace(b.oc_code))
                .Select<List<ExhibitAbs>>(b => b.Company_Exhibit_PageSelect());   // process request body



            var body = resp_List_Mb.HasValue ? resp_List_Mb.Value.ToJson() : new List<ExhibitAbs>().ToJson();
            var response = Util.Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }

        public static Response Process_Exhibit_Detail(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Exhibit_Dtl>().ToMaybe()
                .Where(b => !string.IsNullOrWhiteSpace(b.e_md))
                .DoWhenButNone(b => string.IsNullOrWhiteSpace(b.u_id), _ => h.Action = Message_Action.Login)
                .Select<ExhibitDtl>(b => DataAccess.Exhibit_Detail(new DatabaseSearchModel().SetWhere($" e_count ={b.e_count} and e_namemd = '{b.e_md}' ")));

            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new ExhibitDtl().ToJson();
            var head = h.ToJson();
            var response = new Response(head.ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES));
            return response;
        }

        public static Response Process_Exhibit_Companies(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Exhibit_Dtl>().ToMaybe()
                .Where(b => !string.IsNullOrWhiteSpace(b.e_md))
                .DoWhenButNone(b => string.IsNullOrWhiteSpace(b.u_id), _ => h.Action = Message_Action.Login)
                .Select<List<ExhibitCompany>>(b => b.Exhibit_Companies_Get());

            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new List<ExhibitCompany>().ToJson();
            var head = h.ToJson();
            var response = new Response(head.ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;
        }

        #region Cetification
        public static Response Process_Company_CetificationList(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<CertificationInfo>, List<Certification>>(b => b.Company_CetificateList_Get(out count),
                (b, s) => s.Select(p => new Certification
                {
                    ci_certNo = p.ci_certNo,
                    ci_certificationProgram = p.ci_certificationProgram,
                    ci_certStatus = p.ci_certStatus,
                    ci_expiredDate = DateTime.Parse(p.ci_expiredDate.ToString("yyyy-MM-dd")),
                    ci_id = p.ci_id.ToString(),
                    ci_oc_code = p.ci_oc_code
                }
            ).ToList()).ShiftWhenOrElse<Resp_Certifications>(t => t != null && t.Count > 0, ta => new Resp_Certifications()
            {
                isSuccess = true,
                errorCode = 200,
                certifications = ta,
                count = count
            },
                            tb => new Resp_Certifications()
                            {
                                errorCode = 100,
                                count = 0
                            });

            var body = resp_mb.Value.ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;

        }

        public static Response Process_Company_CetificationDtl(string ci_id)
        {
            var h = new Response_Head();
            var resp_mb = ci_id.ToMaybe().Where(c => c.ToInt() > 0).Select(action => Company_Handle.Company_CetificateDtl_Get(ci_id.ToInt()).ToMaybe());
            resp_mb.Value.ForEach(t => t.ci_detailInfo = Ci_detailInfoHandle(t.ci_detailInfo));
            var body = resp_mb.HasValue && resp_mb.Value.Count > 0 ? resp_mb.Value[0].ToJson() : new CertificationInfo().ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;
        }

        private static string Ci_detailInfoHandle(string ci_detailInfo)
        {
            var b = ci_detailInfo.IndexOf("table", ci_detailInfo.Length - 2);
            List<int> arr = new List<int>();
            int temp = 0;
            for (int i = 0; i < 100; i++)
            {

                if (temp < 0) break;
                temp = ci_detailInfo.IndexOf("table", temp + 1);
                if (i % 2 == 0)
                    arr.Add(temp);
            }

            int length = " border=\"1\"".Length;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Count - 1; i++)
            {
                if (i == 0)
                    sb.Append(ci_detailInfo.Substring(0, arr[i] + 5));
                else
                {
                    string tem = ci_detailInfo.Substring(arr[i - 1] + 5, arr[i] - arr[i - 1]);
                    sb.Append(tem);
                }
                sb.Append(" border=\"1\"");
            }
            sb.Append(ci_detailInfo.Substring(arr[arr.Count - 2] + 5, ci_detailInfo.Length - arr[arr.Count - 2] - 5));
            return sb.ToString();
        }
        #endregion

        #region company reg
        public static Response Process_Company_RegList(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var h = new Response_Head();
            int count = 0;
            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<OrgGS1RegListInfo>>(b => b.Company_RegList_Get(out count));


            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new List<OrgGS1RegListInfo>().ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;

        }

        public static Response Process_Company_InvList(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var h = new Response_Head();
            int count = 0;
            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<OrgGS1ItemInfo>, List<OrgGS1Item>>(b => b.Company_InvList_Get(out count),
                (b, s) => s.Select(p => new OrgGS1Item
                {
                    ogs_code = p.ogs_code,
                    ogs_itemName = p.ogs_itemName,
                    ogs_brandName = p.ogs_brandName,
                    ogs_id = p.ogs_id.ToString()
                }).ToList());

            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new List<OrgGS1Item>().ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;

        }

        public static Response Process_Company_InvDtl(string ogs_id)
        {
            var h = new Response_Head();
            int count = 0;
            var resp_mb = DataAccess.InvDtl_Get(ogs_id.ToInt()).ToMaybe();    
            var lst = DataAccess.UNSPSC_CN_SelectPaged(" * ", " where 1=1 ", " [id] desc ", 1, 20000, out count);
            var catestr = "";
            if (resp_mb.HasValue)
                resp_mb.DoWhenOrElse(t => t.ogs_itemClassCode.IndexOf("*") > -1 || string.IsNullOrEmpty(t.ogs_itemClassCode)
                                                        , tw => catestr = "无"
                                                        , te => catestr = catestr.ToMaybe().Select<string>(t => lst.Find(e => e.cate == te.ogs_itemClassCode.Substring(0, 2) + "000000").name_cn + ">>")
                                                          .Select<string>(t => t + lst.Find(e => e.cate == te.ogs_itemClassCode.Substring(0, 4) + "0000").name_cn + ">>")
                                                             .Select<string>(t => t + lst.Find(e => e.cate == te.ogs_itemClassCode.Substring(0, 6) + "00").name_cn + ">>")
                                                             .Select<string>(t => t + lst.Find(e => e.cate == te.ogs_itemClassCode).name_cn).Value);

            var body = resp_mb.HasValue ? new Resp_Regs() { catestr = catestr, regInfo = resp_mb.Value }.ToJson() : new Resp_Regs().ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;

        }


        #endregion

        #region employe
        public static Response Process_Employs(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<QZEmployInfo>>(b => b.Company_Emoloyes_Get(out count)).Value
                .Select(p => new {
                    ep_Name = p.ep_Name,
                    ep_PriceL = p.ep_PriceL,
                    ep_PriceH = p.ep_PriceH,
                    ep_PriceTxt = !string.IsNullOrEmpty(p.ep_PriceTxt) ? p.ep_PriceTxt : "面议",
                    ep_City = p.ep_City,
                    ep_Area = p.ep_Area,
                    ep_Date = Convert.ToDateTime(p.ep_Date).ToString("yyyy-MM-dd"),
                    ID = p.id.ToString()
                }).ToList().ToMaybe();




            var body = resp_mb.HasValue ? new { employelist = resp_mb.Value, count = count }.ToJson() : new { employelist = new List<QZEmployInfo>(), count = 0 }.ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;
        }

        public static Response Process_JobDtl_Get(string ogs_id)
        {
            var h = new Response_Head();
            var resp_mb = DataAccess.Job_Get(ogs_id.ToInt()).ToMaybe().DoWhen(b=>b.IsNotNull()&&b.Count>0,t=> {
                if (string.IsNullOrEmpty(t[0].ep_Type))
                    t[0].ep_Type = "不详";
                if (string.IsNullOrEmpty(t[0].ep_Property))
                    t[0].ep_Property = "不详";
                if (string.IsNullOrEmpty(t[0].ep_EduReq))
                    t[0].ep_EduReq = "不详";
                if (string.IsNullOrEmpty(t[0].ep_YearsReq))
                    t[0].ep_YearsReq = "不详";
            });
            
            var body = resp_mb.HasValue && resp_mb.Value.Count > 0 ? resp_mb.Value[0].ToJson() : new QZEmployInfo().ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_ExecuteDtl_Get(string zx_id)
        {
            var h = new Response_Head();
            var resp_mb = DataAccess.ExecuteInfo_Get(zx_id.ToInt()).ToMaybe();

            var body = resp_mb.HasValue && resp_mb.Value.Count > 0 ? resp_mb.Value[0].ToJson() : new ZhiXingInfo().ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_SearchItemSite_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();
            var req_body = request.GetBody<Req_Business_State>();
            var companylist = DataAccess.OrgCompanyList_Select(req_body.oc_code);
            var resp_mb = new List<OrgCompanySiteInfo>();
            if (companylist != null && companylist.oc_id > 0)
                resp_mb = req_body.ToMaybe()
                    .Where(c => !string.IsNullOrEmpty(c.oc_code))
                    .Select<List<OrgCompanySiteInfo>>(b => b.OrgCompanySite_SelectPaged(out count)).Value;

            var body = new { companysites = resp_mb, count = count }.ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_Executes_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<ZhiXingInfo>>(b => b.ZhiXing_SelectPaged(out count)).Value
                .Select(u => new {
                    court = !string.IsNullOrEmpty(u.zx_execCourtName) ? u.zx_execCourtName : "--",
                    name = !string.IsNullOrEmpty(u.zx_pname) ? u.zx_pname : "--",
                    num = !string.IsNullOrEmpty(u.zx_caseCode) ? u.zx_caseCode : "--",
                    date = u.zx_caseCreateTime != null ? u.zx_caseCreateTime.ToString("yyyy-MM-dd") : "--",
                    zx_id = u.zx_id.ToString(),
                    zx_execMoney = !string.IsNullOrEmpty(u.zx_execMoney) ? u.zx_execMoney : "--"
                });


            var body = resp_mb.ToMaybe().HasValue ? new { executes = resp_mb, count = count }.ToJson() : new { executes = new List<ZhiXingInfo>(), count = 0 }.ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_LinkCach_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();

            var resp_mb = request.GetBody<Req_Business_State>().ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<ExhibitionEnterpriseInfo>>(b => b.ExhibitionEnterprise_SelectPaged(out count))
                .DoWhen(b => b.IsNotNull() && b.Count > 0, data => data.ForEach(u => MappingToExhibitionEnterpriseInfo(u, false)));



            var body = resp_mb.ToMaybe().HasValue ? new { exhibitions= resp_mb, count = count }.ToJson() : new { exhibitions = new List<ExhibitionEnterpriseInfo>(), count = 0 }.ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static ExhibitionEnterpriseInfo MappingToExhibitionEnterpriseInfo(ExhibitionEnterpriseInfo info, bool IsSub)
        {
            var phone = info.ee_contact;
            var contact = info.ee_phone;
            if (info.ee_phone.IsNotNull() || info.ee_contact.IsNotNull())
            {
                if (info.ee_phone.IsNotNull())
                {
                    if (Regex.IsMatch(info.ee_phone, @"[\u4e00-\u9fa5]"))
                    {
                        contact = info.ee_phone;
                        phone = info.ee_contact;
                        info.ee_contact = contact;
                        info.ee_phone = phone;
                    }
                }
                else
                {
                    if (!Regex.IsMatch(info.ee_contact, @"[\u4e00-\u9fa5]"))
                    {
                        contact = info.ee_phone;
                        phone = info.ee_contact;
                        info.ee_contact = contact;
                        info.ee_phone = phone;
                    }
                }
            }

            if (IsSub)
            {
                if (info.ee_contact.IsNotNull() && info.ee_contact.Length > 1)
                {
                    info.ee_contact = info.ee_contact.Substring(0, info.ee_contact.Length - 1) + "*";
                }
                if (info.ee_phone.IsNotNull() && info.ee_phone.Length > 5)
                {
                    info.ee_phone = info.ee_phone.Substring(0, info.ee_phone.Length - 5) + "*****";
                }

                if (info.ee_mail.IsNotNull() && info.ee_mail.Length > 5)
                {
                    info.ee_mail = info.ee_mail.Substring(0, info.ee_mail.Length - 5) + "*****";
                }
                if (info.ee_fax.IsNotNull() && info.ee_fax.Length > 5)
                {
                    info.ee_fax = info.ee_fax.Substring(0, info.ee_fax.Length - 5) + "*****";
                }
            }
            return info;
        }


        public static Response Process_Exhibitions_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var h = new Response_Head();
            var req_body = request.GetBody<Req_Business_State>();
            var resp_mb = req_body.ToMaybe()
                .Where(c => !string.IsNullOrEmpty(c.oc_code))
                .Select<List<ExhibitionEnterpriseInfo>>(b => b.ExhibitionEnterprise_SelectPaged(out count))
                .Where(b => b.IsNotNull() && b.Count > 0).Value
                .Select(u => new
                {
                    ee_contact = u.ee_contact,
                    ee_exhName = u.ee_exhName,
                    ee_exhTrade = u.ee_exhTrade,
                    ee_mail = u.ee_mail,
                    ee_site = u.ee_site,
                    ee_namemd = u.ee_namemd
                }).ToList();

            var body = resp_mb.ToMaybe().HasValue ? new { exhibitions = resp_mb, count = count }.ToJson() : new { exhibitions = new List<ExhibitionEnterpriseInfo>(), count = 0 }.ToJson();
            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_Report_Collect(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            var h = new Response_Head();
            var req_body = request.GetBody<Req_ReportsReq>();

            var resp_mb = req_body.Company_Report_Collect();

            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new Resp_Binary { remark = "提交数据不完整", status = false }.ToJson();

            var response = new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;

        }

        public static Response Process_Claim_Submit(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            var h = new Response_Head();
            var resp_mb = request.GetBody<Req_Claim>().Company_Claim_Submit();

            var body = resp_mb.IsNotNull() ? resp_mb.ToJson() : new Resp_Binary { status = false }.ToJson();

            var response= new Response(h.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));
            return response;
        }

        //public static Response Process_Company_VipExport(Request request)
        //{
        //    var pre_Mb = request.Preprocess2Maybe(true);
        //    if (pre_Mb.HasValue)
        //        return pre_Mb.Value;
        //    var h = new Response_Head();
        //    var req_body = request.GetBody<Company>();




        //}
        #endregion

    }
}