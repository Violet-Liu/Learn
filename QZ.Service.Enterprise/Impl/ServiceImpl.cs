using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using QZ.Foundation.Utility;
using QZ.Foundation.Model;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Instrument.Client;
using System.Net.Http;
using QZ.Instrument.Common;

namespace QZ.Service.Enterprise
{
    public class ServiceImpl
    {
        private static Response Normal_Resp_Create(string body, EncryptType en_type = EncryptType.AES)
        {
            var head = new Response_Head().ToJson();
            return new Response(head.ToEncryption(EncryptType.PT), body.ToEncryption(en_type));//.ToJson();
        }

        #region index
        public static Response Process_Index(Request request)
        {
            var pre_Ei = request.Simple_Preprocess(Constants.C_Dyn_0);    // 第一个请求的预处理
            if (pre_Ei.HasLeft)     // 预处理有左值，说明预处理出错，则直接返回左值
                return pre_Ei.Left;

            var user_Mb = request.GetBody<Req_User>(Constants.C_Dyn_0).u_name.ToMaybe().Where(n => !string.IsNullOrEmpty(n)).Select(n => DataAccess.User_FromName_Select(n).ToMaybe()); // get user according to the user name

            var token = user_Mb.HasValue        // generate a token value
                ? new Token(pre_Ei.Right, user_Mb.Value.u_id.ToString(), user_Mb.Value.u_name).Compose(t => Cipher_Md5.Md5_16(t)).Induce()
                : new Token(pre_Ei.Right).Compose(t => Cipher_Md5.Md5_16(t)).Induce();

            var head = new Response_Head().ToJson();
            var body = Resp_Index.Resp_Indices.First().SetToken(token).ToJson();

            var response = new Response(head.ToEncryption(EncryptType.PT | EncryptType.ResetKey), body.ToEncryption(EncryptType.AES));//.ToJson();
            return response;
        }
        #endregion
        public static Response Process_Index_Pics()
        {
            List<Index_Pic> pic_list = null;
            CMSBlocksInfo block = CacheMarker.GetCMSBlocksInfo("App_Header_Pic", "Focus");
            if (block != null)
            {
                // 获得发布过的前topn条数据
                var cmsItems = DataAccess_News.CMSItems_SelectTopN2(block.blk_id, 5, ""/*" and n_publish = 1"*/);
                pic_list = cmsItems.Select(item => new Index_Pic() { href = item.n_linkUrl, img_src = item.n_imageUrl, title = item.n_title }).ToList();
            }
            else
            {
                pic_list = new List<Index_Pic>();
            }
            var body = pic_list.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.PT);
            return response;
        }

        public static Response Process_Query_Hot(string pg_size)
        {

            var code_key = ConfigurationManager.AppSettings["code_key"];
            var resp_Mb = DataAccess.CMSPagesInfo_FromUid_Get("Corp_HotSearch").ToMaybe()
                .Select(p => DataAccess.CMSItems_FromPgid_Select(p.pg_id, pg_size.ToInt()).ToMaybe())

                //.Select(p => DataAccess.CMSBlocks_FromPgidName_Get(p.pg_id, "CorpHot").ToMaybe())
                //.Select(b => DataAccess.CMSItems_FromBlkid_Select(b.blk_id, pg_size).ToMaybe());
                //d5da4886c06c316497be96635c8ac894 -> 华为

                .Select<List<Query_Hot>>(items =>  
                {
                    var hot_list = items.Select(i => Query_Hot_Get(i, code_key)).ToList();
                    if (hot_list.Count < 1)
                        return null;
                    return hot_list;
                });

            var body = resp_Mb.HasValue ? resp_Mb.Value.ToJson() : Resp_Query_Hot.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_ExtQuery_Hot(string q_type, string pg_size)
        {


            string body = null;
            var type = q_type.ToInt();
            var size = pg_size.ToInt();
            switch (type)
            {
                case 1:
                    body = ServiceHandler.Brand_Query_Hot(size);
                    break;
                case 2:
                    body = ServiceHandler.Patent_Query_Hot(size);
                    break;
                case 5:
                    body = ServiceHandler.Judge_Query_Hot(size);
                    break;
                case 6:
                    body = ServiceHandler.Dishonest_Query_Hot(size);
                    break;
                case 30:
                    body = ServiceHandler.Trade_Universal_Query_Hot(size);
                    break;
                case 31:
                    body = ServiceHandler.Exhibit_Query_Hot(size);
                    break;
                default:
                    body = (new List<string>()).ToJson();
                    break;
            }

            var response = Normal_Resp_Create(body);

            return response;
        }

        private static Query_Hot Query_Hot_Get(CMSItemsInfo info, string code_key)
        {
            var hot = new Query_Hot() { title = info.n_title };
            var fields = info.n_linkUrl.Split('/');
            var len = fields.Length;
            var field = fields[len - 1];
            
            if (fields.Contains("show") && fields.Contains("detail"))
            {
                hot.type = 2;
                hot.show_url = field.Split('.')[0];
                hot.cat_id = "538";
            }
            else
            {
                hot.type = 1;
                hot.oc_code = field.Substring(0, field.Length - 5).To_Occode(code_key);
                var c = DataAccess.OrgCompanyList_Select(hot.oc_code);
                if (c != null)
                {
                    hot.oc_area = c.oc_area;
                    hot.oc_name = c.oc_name;
                }
            }


            
            return hot;
        }

        public static Response Process_NewQuery_Brand(Request request)
        {


            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Brand_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Brand_NewQuery();

            var body = list_mb.HasValue ? list_mb.Value.ToJson() : new ES_Outcome<ES_Brand>() { docs = new List<ES_Doc<ES_Brand>>() }.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }

        public static Response Process_Query_Brand(Request request)
        {
  

            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Brand_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Brand_Query();

            var body = list_mb.HasValue ? list_mb.Value.ToJson() : Resp_Brands.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

 

            return response;
        }
        public static Response Process_Patent_NewQuery(Request request)
        {

            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Patent_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Patent_NewQuery();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : new ES_Outcome<ES_Patent>() { docs = new List<ES_Doc<ES_Patent>>() }.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

            return response;
        }

        public static Response Process_Patent_Query(Request request)
        {

            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Patent_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Patent_Query();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : Resp_Patents.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

            return response;
        }

        public static Response Process_Patent_Universal_Query(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var patent_mb = request.GetBody<Req_Info_Query>().Patent_Universal_Query();
            var body = patent_mb.HasValue ? patent_mb.Value.ToJson() : Resp_Patents.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }

        public static Response Process_Judge_NewQuery(Request request)
        {


            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Judge_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Judge_NewQuery();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : new ES_Outcome<ES_Judge>() { docs = new List<ES_Doc<ES_Judge>>() }.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }

        public static Response Process_Judge_Query(Request request)
        {


            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Judge_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Judge_Query();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : Resp_Judges.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }

        public static Response Process_Brand_Dtl(Request request)
        {
 

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_mb = request.GetBody<Req_Brand_Dtl>().ToMaybe();
            var brand_dtl_mb = req_mb.Select(b => DataAccess.Brand_Dtl_Get(b.reg_no, b.cat_no).ToMaybe())
                                     .DoWhen(b => !string.IsNullOrEmpty(b.oc_code),
                                             d => d.Company_Compensate());
            var body = brand_dtl_mb.HasValue ? brand_dtl_mb.Value.ToJson() : Brand_Dtl.Default.ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Patent_Dtl(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_mb = request.GetBody<Req_Patent_Dtl>().ToMaybe();
            var patent_mb = req_mb.Select(p => DataAccess.Patent_Dtl_Get(p.p_no, p.m_cat).ToMaybe())
                                  .Do(p => p.img = ConfigurationManager.AppSettings["patent_domain"] + p.img)
                                  .DoWhen(p => !string.IsNullOrEmpty(p.oc_code) && !p.oc_code.EndsWith("K") && !p.oc_code.EndsWith("T"), p => p.Company_Compensate());
            var body = patent_mb.HasValue ? patent_mb.Value.ToJson() : Patent_Dtl.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Judge_Detail(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var judge_dtl = request.GetBody<Req_Query_Dtl>().Judge_Dtl_Query();
            if (!string.IsNullOrEmpty(judge_dtl.jdg_oc_code))
            {
                var oc_codes = judge_dtl.jdg_oc_code.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var infos = new List<Company_Mini_Info>();
                foreach (var oc_code in oc_codes)
                {
                    var company = DataAccess.OrgCompanyList_Select(oc_code);

                    if (company != null)
                    {
                        var info = new Company_Mini_Info();
                        info.oc_code = company.oc_code;
                        info.oc_name = company.oc_name;
                        info.oc_area = company.oc_area;
                        infos.Add(info);
                    }
                }
                judge_dtl.company_infos = infos;
            }
            else
                judge_dtl.company_infos = new List<Company_Mini_Info>();

            var body = judge_dtl.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }
        public static Response Process_Dishonest_NewQuery(Request request)
        {


            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Dishonest_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Dishonest_NewQuery();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : new ES_Outcome<ES_Dishonest>() { docs = new List<ES_Doc<ES_Dishonest>>() }.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }

        public static Response Process_Dishonest_Query(Request request)
        {


            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_Info_Query>(); // get request body
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Dishonest_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var list_mb = request_Body.Dishonest_Query();
            var body = list_mb.HasValue ? list_mb.Value.ToJson() : Resp_Dishonests.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

  

            return response;
        }
        public static Response Process_Dishonest_Dtl(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_body = request.GetBody<Req_Query_Dtl>();
            var id = req_body.i_id;
            var dtl = id > 0 ? (DataAccess.Dishonest_Dtl_Query(id)??Dishonest_Dtl.Default) : Dishonest_Dtl.Default;
            var oc_code_length = dtl.code.Length;
            if(oc_code_length < 12 && oc_code_length > 7)   // 判断是否是机构代码 -> 长度是否为9，这里放宽条件
            {
                var company = DataAccess.OrgCompanyList_Select(dtl.code);
                if (company != null)
                {
                    dtl.oc_code = company.oc_code;
                    dtl.oc_name = company.oc_name;
                    dtl.oc_area = company.oc_area;
                }
            }
            var body = dtl.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Judge_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var list = request.GetBody<Req_Oc_Mini>().Judges_Get();
            var body = list.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }

        public static Response Process_Dishonest_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var list = request.GetBody<Req_Oc_Mini>().Dishonest_Get();
            var body = list.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }
        public static Response Process_Brand_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var list = request.GetBody<Req_Oc_Mini>().Brand_Page_Select();
            var body = list != null ? list.ToJson() : new Resp_Brands().ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

            return response;
        }

        public static Response Process_Patent_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            var list = request.GetBody<Req_Oc_Mini>().Patents_Get();
            var body = list.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }
        public static Response Process_Copyrights_Get(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var list = request.GetBody<Req_Oc_Mini>().Copyrights_Get();
            var body = list.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }
        public static Response Process_SoftwareCopyright_Detail(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            //var dtl = request.GetBody<Req_Copyright>().SoftwareCopyright_Get();
            return null;
        }
        public static Response Process_ProductCopyright_Detail(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            return null;
        }

        public static Response Process_Company_ScoreMark(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_body = request.GetBody<Req_Oc_Score>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_body.u_id).Set_Uname(request_body.u_name).Set_Action(Constants.Op_Company_Score_Mark);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);
            var mb = request_body.User_Valid_Check().Select<Resp_Binary>(score => score.Company_ScoreMark());
            var body = mb.HasValue ? mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.PT);

            return response;
        }
        public static Response Process_Company_ScoreDetail(Request request)
        {
            return null;
        }

        public static Response Process_Area()
        {
            Util.Set_Context();
            var area_list = DataAccess.Area_Get();

            var response = new Response(string.Empty, area_list.ToJson());
            return response;
        }
        public static Response Process_Company_Query(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            //var head = new Response_Head().ToJson();    // create response head
            var request_Body = request.GetBody<Company>(); // get request body


            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Ent_Query);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var head = new Response_Head();

            var resp_List_Mb = request_Body.ToMaybe()
                .DoWhenButNone(b => b.q_type == q_type.q_general && string.IsNullOrWhiteSpace(b.oc_name), _ => { head.Action = Message_Action.Logic_Err; head.Text = "请输入非空白字符"; })
                .DoWhenButNone(b => b.q_type != q_type.q_general && Company.Invalid_Get(b), _ => { head.Action = Message_Action.Logic_Err; head.Text = "请输入有效条件"; })
                .Select(b => b.Company_List_Query());   // process request body



            var body = resp_List_Mb.HasValue ? resp_List_Mb.Value.ToJson() : Resp_Company_List.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);



            return response;
        }
        public static Response Process_Company_Recommend()
        {

            //随机种子，用来随机填充
            int seed = Math.Abs(Guid.NewGuid().GetHashCode());
            var rand = new Random(seed);
            var up = rand.Next(10000, 150000);
            var down = rand.Next(500, 1000);
            var company = new Company() { oc_reg_capital_floor = down.ToString(), oc_reg_capital_ceiling = up.ToString(), pg_index = rand.Next(1, 100), pg_size = 30 };
            var resp_mb = company.Company_List_Recommend();

            string body = null;
            if(!resp_mb.HasValue)
            {
                var codes = ConfigurationManager.AppSettings["company_recommend"].Split('|');
                if (codes.Length < 1)
                {
                    codes = new[] { "10001686X", "102016548", "759521215" };

                }
                var s = new DatabaseSearchModel().SetOrder(" oc_id ");
                var sb = new StringBuilder();
                for (int i = 0; i < codes.Length; i++)
                {
                    if (i == 0)
                        sb.Append($" oc_code = {codes[0]} ");
                    sb.Append($" or oc_code = {codes[i]} ");
                }
                var list = DataAccess.OrgCompanyList_Page_Select(s.SetWhere(sb.ToString()));
                var dtls = DataAccess.OrgCompanyDtl_Page_Select(s.SetOrder(" od_id "));

                body = ServiceHandler.Resp_Oc_Abs_Get(list, dtls).ToJson();
            }
            else
                body = resp_mb.Value.ToJson();

            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);
  
            return response;
        }
        public static Response Process_Company_Fresh()
        {


            var codes = DataAccess.Browses_Fresh_Get(int.Parse(ConfigurationManager.AppSettings["oc_fresh_limit"]));

            var list = DataAccess.CompanyList_Buld_Get(codes);
            return null;
        }

        public static Response Process_Company_Dtl_Simple(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var dtl_mb = request.GetBody<Company>().ToMaybe().Where(c => !DataAccess.CompanyBlackList_Exist_ByCode(c.oc_code))
                .Select(c => DataAccess.OrgCompanyDetails_Select(c.oc_code).ToMaybe())
                .Select(dl => dl.To_Company_Detail().ToMaybe());

            var body = dtl_mb.HasValue ? dtl_mb.Value.ToJson() : Resp_Company_Detail.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);


            return response;
        }

        public static Response Process_Company_Detail(Request request)
        {

            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;


            //var head = new Response_Head().ToJson();
            var request_Body = request.GetBody<Company>();
            // create user browser log and insert it into database
            var browserLog = request_Body.To_BrowseLog().Set_App_Ver(pre_Ei.Right.App_Ver).Set_Ip(Util.Get_RemoteIp()).Set_Os_Name(Enum.GetName(typeof(Platform), pre_Ei.Right.Platform));
            DataAccess.BrowseLog_Insert(browserLog);

            var detail_Mb = request_Body.Company_Detail_Query();
            var body = detail_Mb.HasValue ? detail_Mb.Value.ToJson() : Resp_Company_Detail.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);

            #region debug
            //Util.Log_Info("company_detail", Location.Exit, response.ToJson(), "json string of response");
            #endregion

            return response;
        }

        public static Response Process_Company_Trades(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var trades_mb = request.GetBody<Company>().ToMaybe().Where(c => !string.IsNullOrWhiteSpace(c.oc_code)).Select<Trade_Intelli_Tip>(c => c.Company_Trades_Get());

            var body = trades_mb.HasValue ? trades_mb.Value.ToJson() : new Trade_Intelli_Tip().ToJson();
            var response = Normal_Resp_Create(body, EncryptType.AES | EncryptType.Gzip);
            return response;
        }

        //public static Response Process_Company_Hot(Request request)
        //{
        //    #region debug
        //    Util.Log_Info("company_hot", Location.Enter, "entering the company_hot process", "normal");
        //    #endregion

        //    var pre_Mb = request.Preprocess_1(false);
        //    if (pre_Mb.HasValue)
        //        return pre_Mb.Value;

        //    return null;
        //}
        public static Response Process_Company_Intelli_Tip(Request request)
        {
 
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var intelli_Tip_Mb = request.GetBody<Req_Intelli_Tip>().Company_Intelli_Tip();

            //#region debug
            //if (intelli_Tip_Mb.HasValue)
            //    Util.Log_Info("company_intelli_tip", Location.Internal, "company name of first intelli-tip is:" + intelli_Tip_Mb.Value.tip_list.FirstOrDefault()?.oc_name, "normal");
            //else
            //    Util.Log_Info("company_intelli_tip", Location.Internal, "failed to get intelli-tip of companys", "normal");
            //#endregion

            var body = intelli_Tip_Mb.HasValue ? intelli_Tip_Mb.Value.ToJson() : Resp_Intelli_Tip.Default.ToJson();
            var response = Normal_Resp_Create(body);

 

            return response;
        }

        public static Response Process_Company_Invest(Request request)
        {
 
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var invest_Mb = request.GetBody<Company_Mini_Info>().Company_Invest();
            var body = invest_Mb.HasValue ? invest_Mb.Value.ToJson() : Resp_Invest.Default.ToJson();

            var response = Normal_Resp_Create(body);
            //#region debug
            //Util.Log_Info("company_invest", Location.Internal, invest_Mb.HasValue ? "successfully querying data from data" : "no data because of some errors happened", "");
            //Util.Log_Info("company_invest", Location.Exit, response.ToJson(), "json string of response");
            //#endregion
            return response;
        }

        public static Response Process_Company_Map(Request request)
        {
   
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Action(Constants.Op_Map_Get);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);
            var map_Mb = request.GetBody<Req_Oc_Map>().Company_Map();
            var body = map_Mb.HasValue ? map_Mb.Value.ToJson_Fast() : Resp_Oc_Map.Default.ToJson_Fast();
            var response = Normal_Resp_Create(body, EncryptType.Gzip);
      
            return response;
        }

        public static Response Process_Company_Stock_Holder(Request request)
        {
          
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var sh_list_Mb = request.GetBody<Company_Mini_Info>().Company_Stock_Holder();
            var body = sh_list_Mb.HasValue ? sh_list_Mb.Value.ToJson() : Resp_Oc_Sh.Default.ToJson();
            var response = Normal_Resp_Create(body);
     
            return response;
        }

        public static Response Process_Company_Change(Request request)
        {
           
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var change_Mb = request.GetBody<Req_Oc_Mini>().Company_Change().Select(cc => new Resp_Oc_Change() { change_list = cc }.ToMaybe());
            var body = change_Mb.HasValue ? change_Mb.Value.ToJson() : Resp_Oc_Change.Default.ToJson();
            var response = Normal_Resp_Create(body);
      
            return response;
        }

        public static Response Process_Company_Icpl(Request request)
        {
            
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var icpl_list_Mb = request.GetBody<Req_Oc_Mini>().Company_Icpl();
            var body = icpl_list_Mb.HasValue ? new Resp_Icpl() { icpl_list = icpl_list_Mb.Value }.ToJson() : Resp_Icpl.Default.ToJson();
            var response = Normal_Resp_Create(body);
      
            return response;
        }

        public static Response Process_Company_Branch(Request request)
        {
        
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var company_list_Mb = request.GetBody<Req_Oc_Mini>().Company_Branch();
            var body = company_list_Mb.HasValue ? new Resp_Branch() { branch_list = company_list_Mb.Value }.ToJson() : Resp_Branch.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_Annual(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var annual_list_Mb = request.GetBody<Req_Oc_Annual>().Company_Annual();
            var body = annual_list_Mb.HasValue ? new Resp_Annual_Abs() { annual_list = annual_list_Mb.Value }.ToJson() : Resp_Annual_Abs.Default.ToJson();

            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_Annual_Detail(Request request)
        {
    
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var annual_dtl_Mb = request.GetBody<Req_Oc_Annual>().Company_Annual_Detail();
            var body = annual_dtl_Mb.HasValue ? annual_dtl_Mb.Value.ToJson() : Company_Annual_Dtl.Default.ToJson();
            var response = Normal_Resp_Create(body);
            
            return response;
        }
        public static Response Process_Company_Impression(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var impression = request.GetBody<Req_Oc_Mini>().Company_Impression();
            var response = Normal_Resp_Create(impression.ToJson());

            return response;
        }
        public static Response Process_Company_New_Topic(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Comment>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Topic_Add);

            var topic_Mb = request_Body.User_Valid_Check().ShiftWhenOrElse(t => t.CompanyTopic_Redundent_Check(),
                b => b.ToMaybe().Select<Resp_Binary>(t => t.Company_New_Topic()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log)),
                s => new Resp_Binary { remark = "不允许短时间内发表重复帖子", status = false }).Value;
            var body = topic_Mb.HasValue ? topic_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Company_Reply(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Comment>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Reply_Add);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);

            var reply_Mb = request_Body.User_Valid_Check()
                .ShiftWhenOrElse(t => t.CompanyTopicReply_Redundent_Check(),
                b => b.ToMaybe().Select<Resp_Binary>(t => t.Company_Reply()),
                s => new Resp_Binary { remark = "不允许短时间内回复重复评论", status = false }).Value;
            
                
            var body = reply_Mb.HasValue ? reply_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Company_Fresh_Topic(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var topics_Mb = request.GetBody<Req_Oc_Mini>().Company_Fresh_Topic();

            var body = topics_Mb.HasValue ? topics_Mb.Value.ToJson() : Resp_Topics_Abs.Default.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Company_Topic_Query(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var topics = request.GetBody<Req_Oc_Mini>().Company_Topic_Query();
            var body = topics.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_Topic_Detail(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_body = request.GetBody<Req_Topic_Dtl>();
            var topic_dtls_Mb = req_body.Company_Topic_Detail().DoWhen(_ => req_body.u_id.ToInt() > 0, _ => DataAccess.TopicUserTrace_Reset(req_body.topic_id.ToString(), "0", req_body.u_id.ToInt()));
            var body = topic_dtls_Mb.HasValue ? topic_dtls_Mb.Value.ToJson() : new List<Reply_Dtl>().ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Company_Topic_UpDown_Vote(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Topic_Vote>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Topic_Updown_Vote);

            var topic_Mb = request_Body.User_Valid_Check().Select(t => t.Company_Topic_UpDown_Vote().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = topic_Mb.HasValue ? topic_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_UpDown_Vote(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Topic_Vote>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Updown_Vote);

            var bin_Mb = request_Body.User_Valid_Check().Select(t => t.Company_UpDown_Vote().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_Correct(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Correct>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Company_Correct);

            var bin_Mb = request_Body.Company_Correct().Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        #region company favorite
        public static Response Process_Company_Favorite_Add(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Mini>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Favorite_Add);

            var bin_Mb = request_Body.User_Valid_Check().Select(c => c.Company_Favorite_Add()).DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));

            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Company_Favorite_NewAdd(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Favorite_Add>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Favorite_Add);

            var bin_Mb = request_Body.User_Valid_Check().Select(c => c.Company_Favorite_Add()).DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log))
                .DoWhen(bin => bin.status, fun => DataAccess.FavoriteGroup_SetCount(request_Body.g_id.ToInt()));

            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Company_Favorite_Remove(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Mini>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Favorite_Remove);

            var bin_Mb = request_Body.User_Valid_Check().Company_Favorite_Remove().DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }

        public static Response Process_Company_Favorite_NewRemove(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Oc_Mini>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Favorite_Remove);

            var bin_Mb = request_Body.User_Valid_Check().Company_Favorite_NewRemove().DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        #endregion

        public static Response Process_Company_Report_Send(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var req_body = request.GetBody<Req_Oc_Mini>();
            var resp_bin = req_body.Company_Report_Send();
            if(resp_bin.status)
            {
                var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(req_body.u_id).Set_Uname(req_body.u_name).Set_Action(Constants.Op_Oc_Report_Send);
                DataAccess.AppOrgCompanyLog_Insert(op_Log);
            }

            var response = Normal_Resp_Create(resp_bin.ToJson(), EncryptType.PT);
            return response;
        }


        #region user
        public static Response Process_Login(Request request)
        {


            //var request = req.ToObject<Request>();
            var pre_Ei = request.Simple_Preprocess();    // 登录处理
            if (pre_Ei.HasLeft)     // 有左值，说明处理出错，则直接返回左值
                return pre_Ei.Left;

            var request_Body = request.GetBody<User_Login>().X_Parse();     // get the login info of request
            var body = request_Body.User_Login();

            var head = new Response_Head();
            if (body.state > 0)      // unnormal
            {
                head.Action = Message_Action.Logic_Err;
                switch (body.state)
                {
                    case Login_State.Name_Err:
                        head.Text = "用户名不存在";
                        break;
                    case Login_State.Pwd_Err:
                        head.Text = "密码错误";
                        break;
                    case Login_State.State_Err:
                        head.Text = "帐号异常";
                        break;
                    case Login_State.ADBlack_Err:
                        head.Text = "你的账号异常，已被列入黑名单，如有疑问请联系我们客服QQ1713694365";
                        break;
                }
            }
            else
            {
                body.token = new Token(pre_Ei.Right, body.u_id, body.u_name).Compose(t => Cipher_Md5.Md5_16(t)).Induce();
                head.Action = Message_Action.None;
            }
            var response = new Response(head.ToJson().ToEncryption(EncryptType.PT), body.ToJson().ToEncryption(EncryptType.AES | EncryptType.Gzip));

            return response;
        }

        public static Response Process_Open_Login(Request request)
        {


            var pre_Ei = request.Simple_Preprocess();    // 登录处理
            if (pre_Ei.HasLeft)     // 有左值，说明处理出错，则直接返回左值
                return pre_Ei.Left;

            var req_body_mb = request.GetBody<Req_Open_Login>().ToMaybe();

            var user_mb = req_body_mb.Where(b => !string.IsNullOrEmpty(b.us_uid))        // check us_uid validation
                                     .Select(b => (DataAccess.Open_User_Select(b.us_type, b.us_uid) ?? new Users_SocialInfo()).ToMaybe())         // select open user info
                                     .ShiftWhenOrElse(ou => ou.us_id > 0,
                                                      ou => ou.DominoForExistedOpenLogin(req_body_mb.Value),
                                                      _ => req_body_mb.Value.OpenUser_FirstLogin())             // shift to resp_openuser_login
                                     .DoWhen(u => u.status,                                                 // insert login log after login successfully
                                             u => ServiceHandler.LoginLog_Insert(u, req_body_mb.Value));
            var head = new Response_Head();
            string body = "";
            if(!user_mb.HasValue)
            {
                head.Action = Message_Action.Logic_Err;
                head.Text = "绑定登录失败";
            }
            else
            {
                if(!user_mb.Value.status)   // login failed
                {
                    head.Action = Message_Action.Logic_Err;
                    head.Text = user_mb.Value.remark;
                }
                else                        // login succeed
                {
                    if (string.IsNullOrEmpty(user_mb.Value.u_face))
                        user_mb.Value.u_face = Util.UserFace_Get(user_mb.Value.u_id);
                    user_mb.Value.token = new Token(pre_Ei.Right, user_mb.Value.u_id.ToString(), user_mb.Value.u_name).Compose(t => Cipher_Md5.Md5_16(t)).Induce();
                    body = user_mb.Value.ToJson();
                }
            }
            var response = new Response(head.ToJson().ToEncryption(EncryptType.PT), body.ToEncryption(EncryptType.AES | EncryptType.Gzip));


            return response;
        }

        public static Response Process_Register(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var body = new Resp_Binary();
            var request_Body_Mb = request.GetBody<User_Register>().ToMaybe()
                .DoWhenButNone(b => !Judge.IsPhoneNum(b.u_tel), _ => body.remark = "手机号错误")
                .DoWhenButNone(b => DataAccess.User_FromPhoneNum_Select_2(b.u_tel) != null, _ => body.remark = "手机号已被注册")
                .DoWhenButNone(b => b.u_pwd.Length < 6 || b.u_pwd.Length > 16, _ => body.remark = "密码太长或太短");

            var name_r = request_Body_Mb.Select<string>(b => ServiceHandler.UserName_Validate(b.u_name))
                .DoWhen(n => string.IsNullOrEmpty(n), n => body.remark = n);

            var v_code_r = request_Body_Mb.Select<int>(b => ServiceHandler.Code_Verify(b.u_tel, b.verify_code))
                .DoWhen(i => i == 0, _ => body.remark = "验证码错误")
                .DoWhen(i => i == -1, _ => body.remark = "验证码已过期")
                .DoWhen(i => i == 1, _ => body.status = true);


            // if body.status == true, that means it passes all validation and is ready to insert register info into database
            if (body.status)
            {
                var i = DataAccess.User_Insert_2(ServiceHandler.To_UserInfo(pre_Ei.Right, request_Body_Mb.Value));
                if (i > 0)
                    body.remark = "注册成功";
                else if(i == -2)
                {
                    body.status = false;
                    body.remark = $"{request_Body_Mb.Value.u_name} 已被注册，请换一个";
                }
                else
                {
                    body.status = false;
                    body.remark = "注册失败";
                }
            }

            var response = Normal_Resp_Create(body.ToJson(), EncryptType.PT);
            return response;
        }

        public static Response Process_Verify_Code_Get(Request request)
        {

            Util.Set_Context();
            var request_Head = request.GetHead().Value;
            var body = new Resp_Binary();
            var request_Body_Mb = request.GetBody<User_Register>().ToMaybe().DoWhenButNone(b => !Judge.IsPhoneNum(b.u_tel), _ => body.remark = "手机号错误");
            var v_code_Mb = request_Body_Mb
                .Select(b => Util.VerifyCode_Get(b.u_tel).ToMaybe()).Do(v_code =>
                {
                    if (string.IsNullOrEmpty(v_code))
                        body.remark = "获取过多，请更换号码尝试";
                    else if (v_code == "busy")
                        body.remark = "操作频繁，请稍后再试";
                    else if (v_code == "already")
                        body.remark = "验证码已发送，请注意查收";
                    else
                        body.status = true;
                });

            if(body.status) // ready to send validation message
            {
                var log = new User_SMSLogInfo() { Sms_code = v_code_Mb.Value, Sms_phone = request_Body_Mb.Value.u_tel, Sms_purpose = (byte)request_Body_Mb.Value.op_type,
                    Sms_time = DateTime.Now, Sms_type = (byte)(request_Head.Platform == Platform.Android ? 4 : 3) };
                if (ServiceHandler.ShortMsg_Send(request_Head, request_Body_Mb.Value, v_code_Mb.Value))
                {
                    log.Sms_success = (byte)1;
                    body.remark = "验证码获取成功";
                }
                else
                    body.remark = "验证码获取失败";

                DataAccess.ShortMsg_Insert(log);    // insert sending log
            }

            var response = Normal_Resp_Create(body.ToJson(), EncryptType.PT);


            return response;
        }

        public static Response Process_Pwd_Reset(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var body = new Resp_Binary();
            var request_Body_Mb = request.GetBody<User_Register>().ToMaybe().DoWhenButNone(b => !Judge.IsPhoneNum(b.u_tel), _ => body.remark = "手机号错误")   
                .DoWhenButNone(b => b.u_pwd.Length < 6 || b.u_pwd.Length > 16, _ => body.remark = "密码太长或太短");

            var user_Mb = request_Body_Mb.Select<UserInfo>(b => DataAccess.User_FromPhoneNum_Select_2(b.u_tel));

            var v_code_r = request_Body_Mb.Select<int>(b => ServiceHandler.Code_Verify(b.u_tel, b.verify_code))
                .DoWhenButNone(_ => !user_Mb.HasValue, _ => body.remark = "手机号尚未注册")
                .DoWhen(i => i == 0, _ => body.remark = "验证码错误")
                .DoWhen(i => i == -1, _ => body.remark = "验证码已过期")
                .DoWhen(i => i == 1, _ => body.status = true);      /* this case is the last one */

            if(body.status)
            {
                user_Mb.Value.u_pwd = Cipher_Md5.Md5_16_1(request_Body_Mb.Value.u_pwd);
                var i = DataAccess.User_Update(user_Mb.Value);
                ServiceHandler.User_PwdReset_Log_Insert(request_Body_Mb.Value.u_tel);
                if(i > 0)
                {
                    body.remark = "修改成功";
                }
                else
                {
                    body.status = false;
                    body.remark = "修改失败";
                }
            }
            var response = Normal_Resp_Create(body.ToJson(), EncryptType.PT);
            return response;
        }

        public static Response Process_Face_Reset(Request request)
        {

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var response = request.GetBody<Req_Portrait>().Face_Set();


            return response;
        }

        public static Response Process_Info_Set(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var body = request.GetBody<Req_User_Info>().Info_Set(pre_Ei.Right);
            var response = Normal_Resp_Create(body.ToJson(), EncryptType.PT);

            return response;
        }

        public static Response Process_Info_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var body = request.GetBody<Req_User_Info>().Info_Get();
            var response = Normal_Resp_Create(body.ToJson());


            return response;
        }
        public static Response Process_Query(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var queries_Mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(q => q.History_Query().ToMaybe());
            var body = queries_Mb.HasValue ? queries_Mb.Value.ToJson() : new List<History_Query>().ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Ext_SearchHistory(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var ext_mb = request.GetBody<Req_Info_Query>().User_Valid_Check().Ext_SearchHistory_Get();
            var body = ext_mb.HasValue ? ext_mb.Value.ToJson() : Ext_SearchHistory.Default.ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Ext_SearchHistory_Drop(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var bin_mb = request.GetBody<Req_Info_Query>().User_Valid_Check().Select(drop => drop.Ext_SearchHistory_Drop().ToMaybe());
            var body = bin_mb.HasValue ? bin_mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.PT);


            return response;
        }

        public static Response Process_Suggestion_Add(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_body = request.GetBody<Req_Oc_Correct>();//.User_Valid_Check();
            //if(!req_body.HasValue)
            //{
            //    var head = new Response_Head(Message_Action.Logic_Err);
            //    return new Response(head.ToJson().ToEncryption(EncryptType.PT), string.Empty);
            //}
            var bin_mb = req_body.ToMaybe().Where(c => !string.IsNullOrWhiteSpace(c.crect_content)).Select(c => new CompanyInfoCorrectInfo()
            {
                cic_content = c.crect_content.To_Sql_Safe(),
                cic_date = DateTime.Now,
                cic_oc_code = string.Empty,
                cic_oc_name = string.Empty,
                cic_phone = c.u_tel,
                cic_type = 9,
                cic_u_name = c.u_name,
                cic_u_uid = c.u_id.ToInt()
            }.ToMaybe())
            .ShiftWhenOrElse(info => DataAccess.Company_Correct(info) > 0,
                                _ => new Resp_Binary() { status = true, remark = "提交成功" },
                                _ => new Resp_Binary() { remark = "提交失败" });
            var body = bin_mb.HasValue ? bin_mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body, EncryptType.PT);
            return response;
        }

        public static Response Process_Query_Delete(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Query>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Query_History_Remove);

            var bin_Mb = request_Body.User_Valid_Check().Select(q => q.Query_Delete().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }

        public static Response Process_Query_Drop(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Query>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Query_History_Drop);

            var bin_Mb = request_Body.User_Valid_Check().Select(q => q.Query_Drop().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Browse_Get(Request request)
        {


            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var queries_Mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(q => q.Browses_Get().ToMaybe());
            var body = queries_Mb.HasValue ? queries_Mb.Value.ToJson() : new List<Browse_Log>().ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Browse_Delete(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Browse>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Browse_Remove);

            var bin_Mb = request_Body.User_Valid_Check().Select(q => q.Browse_Delete().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Browse_Drop(Request request)
        {


            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Browse>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Oc_Browse_Drop);

            var bin_Mb = request_Body.User_Valid_Check().Select(q => q.Browse_Drop().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);


            return response;
        }
        public static Response Process_Favorites_Get(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Favorites_Get), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var queries_Mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(q => q.Favorites_Get().ToMaybe());
            var body = queries_Mb.HasValue ? queries_Mb.Value.ToJson() : Resp_Favorites.Default.ToJson();
            var response = Normal_Resp_Create(body);

            //#region debug
            //Util.Log_Info(nameof(Process_Favorites_Get), Location.Exit, body, "normal");
            //#endregion
            return response;
        }

        public static Response Process_Favorites_NewGet(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Favorites_Get), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Mb = request.Preprocess2Maybe(true);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var queries_Mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(q => q.Favorites_NewGet().ToMaybe());
            var body = queries_Mb.HasValue ? queries_Mb.Value.ToJson() : Resp_NewFavorites.Default.ToJson();
            var response = Normal_Resp_Create(body);

            //#region debug
            //Util.Log_Info(nameof(Process_Favorites_Get), Location.Exit, body, "normal");
            //#endregion
            return response;
        }

        public static Response Process_Notice_Status(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Status), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var bin_Mb = request.GetBody<Req_User_Info>().UserValidCheck().Select(u => u.Notice_Status().ToMaybe());
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Status), Location.Enter, body, "normal");
            //#endregion
            return response;
        }
        public static Response Process_Notice_Topics_Get(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Topics_Get), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var resp_mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(u => u.Notice_Topics().ToMaybe());
            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : Resp_Topic_Notice.Default.ToJson();
            var response = Normal_Resp_Create(body);

            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Companies), Location.Enter, body, "normal");
            //#endregion
            return response;
        }

        public static Response Process_Notice_Companies(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Companies), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var resp_mb = request.GetBody<Req_Cm_Topic>().User_Valid_Check().Select(u => u.Notice_Companies().ToMaybe());
            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : Resp_Oc_Notice.Default.ToJson();
            var response = Normal_Resp_Create(body);

            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Companies), Location.Enter, body, "normal");
            //#endregion
            return response;
        }
        public static Response Process_Notice_Company_Remove(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Notice_Company_Remove), Location.Enter, string.Empty, "normal");
            //#endregion

            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var bin_Mb = request.GetBody<Req_Oc_Mini>().User_Valid_Check().Select(u => u.Notice_Company_Remove().ToMaybe());
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            #region debug
            //Util.Log_Info(nameof(Process_Notice_Company_Remove), Location.Exit, body, "normal");
            #endregion
            return response;
        }
        public static Response Process_Notice_Topic_Remove(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Notice_Companies), Location.Enter, string.Empty, "normal");
            #endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var bin_Mb = request.GetBody<Req_Topic_Vote>().User_Valid_Check().Select(u => u.Notice_Topic_Remove().ToMaybe());
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            #region debug
            //Util.Log_Info(nameof(Process_Notice_Company_Remove), Location.Exit, body, "normal");
            #endregion
            return response;
        }
        public static Response Process_Notice_Topic_Drop(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Notice_Companies), Location.Enter, string.Empty, "normal");
            #endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var bin_Mb = request.GetBody<Req_Topic_Vote>().User_Valid_Check().Select(u => u.Notice_Topic_Drop().ToMaybe());
            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            #region debug
            //Util.Log_Info(nameof(Process_Notice_Company_Remove), Location.Exit, body, "normal");
            #endregion
            return response;
        }
        #endregion

        #region community

        public static Response Process_Community_Topic_Add(Request request)
        {

            var pre_Ei = request.Preprocess2Either(false, true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Cm_Comment>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Cm_Topic_Add);

            var topic_Mb = request_Body.User_Valid_Check().ShiftWhenOrElse(t => t.CommunityTopic_Redundent_Check(),
                b => b.ToMaybe().Select<Resp_Binary>(d => d.Community_New_Topic()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log)),
                s => new Resp_Binary { remark = "不允许短时间内发表重复的帖子", status = false }).Value;

            var body = topic_Mb.HasValue ? topic_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);

            return response;
        }
        public static Response Process_Community_Reply_Add(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Community_Reply_Add), Location.Enter, string.Empty, "normal");
            #endregion
            var pre_Ei = request.Preprocess2Either(false, true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Cm_Comment>();
            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Cm_Reply_Add);

            var reply_Mb = request_Body.User_Valid_Check().ShiftWhenOrElse(t => t.CommunityTopicReply_Redundent_Check(),
                b=>b.ToMaybe().Select<Resp_Binary>(d=> d.Community_New_Reply()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log)),
                s => new Resp_Binary { remark = "不允许短时间内回复重复的评论", status = false }).Value;

            var body = reply_Mb.HasValue ? reply_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);
            #region debug
            //Util.Log_Info("company_reply", Location.Exit, body, "json of response body");
            #endregion
            return response;
        }

        public static Response Process_Community_Topic_Query(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_Query), Location.Enter, string.Empty, "normal");
            #endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var body = request.GetBody<Req_Cm_Topic>().Community_Topic_Query().ToJson();
            var response = Normal_Resp_Create(body);
            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_Query), Location.Exit, body, "json of response body");
            #endregion
            return response;
        }
        public static Response Process_Topics_Hot(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Topics_Hot), Location.Enter, string.Empty, "normal");
            #endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var body = request.GetBody<Req_Cm_Topic>().Community_Topics_Hot().ToJson();
            var response = Normal_Resp_Create(body);
            #region debug
            //Util.Log_Info(nameof(Process_Topics_Hot), Location.Exit, body, "json of response body");
            #endregion
            return response;
        }

        public static Response Process_Community_Topic_Detail(Request request)
        {
            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_Detail), Location.Enter, string.Empty, "normal");
            #endregion

            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var req_body = request.GetBody<Req_Topic_Dtl>();
            var topic_dtls_Mb = req_body.Community_Topic_Detail().DoWhen(_ => req_body.u_id.ToInt() > 0, _ => DataAccess.TopicUserTrace_Reset(req_body.topic_id.ToString(), "1", req_body.u_id.ToInt()));
            var body = topic_dtls_Mb.HasValue ? topic_dtls_Mb.Value.ToJson() : new List<Reply_Dtl>().ToJson();
            var response = Normal_Resp_Create(body);
            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_Detail), Location.Exit, body, "json of response body");
            #endregion
            return response;
        }
        public static Response Process_Community_Topic_UpDown_Vote(Request request)
        {
            //#region debug
            //Util.Log_Info(nameof(Process_Community_Topic_UpDown_Vote), Location.Enter, string.Empty, "normal");
            //#endregion
            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Topic_Vote>();

            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_UpDown_Vote), Location.Internal, request_Body.op_type.ToString(), "operation type");
            #endregion

            // create user operation log and insert it into database
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Op_Cm_Topic_Updown_Vote);

            var topic_Mb = request_Body.User_Valid_Check().Select(t => t.Community_Topic_UpDown_Vote().ToMaybe()).Do(_ => DataAccess.AppOrgCompanyLog_Insert(op_Log));

            var body = topic_Mb.HasValue ? topic_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();
            var response = Normal_Resp_Create(body);
            #region debug
            //Util.Log_Info(nameof(Process_Community_Topic_UpDown_Vote), Location.Exit, body, "json of response body");
            #endregion
            return response;
        }
        #endregion

        #region favorite
        public static Response Process_Favorite_Group_Get(string u_id)
        {
            var grouplst = DataAccess.FavoriteGroups_Selectbyu_uid(u_id.ToInt()).ToMaybe()
                .DoWhen(gs => gs.Count==0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "竞品", fl_count = 0 }))
                                  .DoWhen(gs => gs.Count == 0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "其他", fl_count = 0 }))
                                 .DoWhen(gs => gs.Count == 0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "关注", fl_count = 0 }))
                                 .DoWhen(gs => gs.Count==0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "客户", fl_count = 0 }))
                                 .DoWhen(gs => gs.Count == 0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "供应商", fl_count = 0 }))
                                 .DoWhen(gs => gs.Count == 0,
                                 action => DataAccess.FavoriteGroup_Insert(new Favorite_Group { u_uid = u_id.ToInt(), g_name = "经销商", fl_count = 0 }));

            grouplst = DataAccess.FavoriteGroups_Selectbyu_uid(u_id.ToInt()).ToMaybe();
            var body = grouplst.HasValue ? grouplst.Value.ToJson() : new List<Favorite_Group>().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Favorites_GetbyId(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            var request_Body = request.GetBody<Req_Favorite_Group>();
            var lst = request_Body.ToMaybe().Where(req => req.u_id.ToInt() > 0 && req.g_id.ToInt() > 0)
                .Select<List<Favorite_Log>>(t => t.Favorites_GetByUidAndGuid());

            lst.Value.ForEach(t => t.oc_name.Replace("<font color=\"red\">", "").Replace("</font>", ""));
            var body = lst.HasValue ? lst.Value.ToJson() : new List<Favorite_Log>().ToJson();

            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Favorite_Group_Insert(Request request)
        {
            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Favorite_Add>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Favorite_Group_Insert);

            var bin_Mb = request_Body.User_Valid_Check().Select(c => c.Favorite_Group_Add()).DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));

            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();

            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Favorite_Group_Del(Request request)
        {
            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Favorite_Add>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Favorite_Group_Del);

            var bin_Mb = request_Body.User_Valid_Check().Select(c => c.Favorite_Group_Del()).DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));

            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();

            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Favorite_Group_Update(Request request)
        {
            var pre_Ei = request.Preprocess2Either(false);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_Favorite_Add>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.Favorite_Group_Update);

            var bin_Mb = request_Body.User_Valid_Check().Select(c => c.Favorite_Group_Update()).DoWhen(bin => bin.status, _ => DataAccess.AppOrgCompanyLog_Insert(op_Log));

            var body = bin_Mb.HasValue ? bin_Mb.Value.ToJson() : Resp_Binary.Default.ToJson();

            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_UnGroupedFavorites_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            var request_Body = request.GetBody<Req_Favorite_Group>();
            var lst = request_Body.ToMaybe().Where(req => req.u_id.ToInt() > 0)
                .Select<List<Favorite_Log>>(t => t.UnFavorites_Get());

            lst.Value.ForEach(t => t.oc_name.Replace("<font color=\"red\">", "").Replace("</font>", ""));

            var body = lst.HasValue ? lst.Value.ToJson() : new List<Favorite_Log>().ToJson();

            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Favorite_Into_Group(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var request_Body = request.GetBody<Req_FavoriteIntoGroup>();
            var lst = request_Body.ToMaybe().Select<Resp_Binary>(t => t.Favorite_Into_Group());

            var body = lst.HasValue ? lst.Value.ToJson() : new Resp_Binary().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Favorite_Out_Group(string fl_id)
        {
            
            var lst = DataAccess.Favorite_Out_Group(fl_id.ToInt()).ToMaybe()
                        .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "添加成功", status = true },
                                        _ => new Resp_Binary() { remark = "添加失败", status = false });

            var body = lst.HasValue ? lst.Value.ToJson() : new Resp_Binary().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Favorite_Note_Add(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var request_Body = request.GetBody<Req_FavoriteNote>();
            var lst = request_Body.ToMaybe().Select<Resp_Binary>(t => t.Favorite_Note_Add());

            var body = lst.HasValue ? lst.Value.ToJson() : new Resp_Binary().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Favorite_Note_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var request_Body = request.GetBody<Req_FavoriteNote>();
            var lst = request_Body.Favorite_Note_SelectPaged();
            var body = lst.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }
        
        public static Response Favorite_Note_UP(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            var request_Body = request.GetBody<Req_FavoriteNote>();
            var lst = request_Body.ToMaybe().Select<Resp_Binary>(t => t.Favorite_Note_UP());

            var body = lst.HasValue ? lst.Value.ToJson() : new Resp_Binary().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Favorite_Note_Del(string n_id)
        {
            var result = DataAccess.Favorite_Note_Del(n_id.ToLong()).ToMaybe()
                       .ShiftWhenOrElse(i => i > 0, _ => new Resp_Binary() { remark = "添加成功", status = true },
                                       _ => new Resp_Binary() { remark = "添加失败", status = false });

            var body = result.HasValue ? result.Value.ToJson() : new Resp_Binary().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_SysNotices_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int rowcount = 0;
            var request_Body = request.GetBody<Req_Cm_Topic>();
            var lst = request_Body.ToMaybe()
                .Select<List<SystemNoticeInfo>>(t => DataAccess.SystemNotice_SelectPagedByUser(t.u_id.ToInt(), t.pg_index > 1 ? ((t.pg_index - 1) * t.pg_size) : 1, t.pg_index * t.pg_size, out rowcount))
                .FuncWhen(t => t.IsNotNull(), t => t.Select(u => new
                {
                    s_id = u.s_id,
                    title = u.s_title,
                    isread = u.isread,
                    date = u.s_date.ToString("yyyy-MM-dd"),

                }));
            var body = lst.HasValue ? new { sysnotices = lst.Value, count = rowcount }.ToJson() : new { sysnotices = new[] { new { } }, count = 0 }.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_SysNoticeDtl_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;

            int rowcount = 0;
            int bycount = 0;
            var req_body = request.GetBody<req_sysnotice>();
            SystemNoticeByUserInfo entity = null;
            DatabaseSearchModel sysnotice = new DatabaseSearchModel().SetWhere($"s_id={req_body.s_id}").SetWhere("s_isvalid=1").SetPageIndex(1).SetPageSize(1).SetOrder("s_date desc");
            var list = DataAccess.SystemNotice_SelectPaged(sysnotice, out rowcount);
            DatabaseSearchModel sysbyuser = new DatabaseSearchModel().SetWhere($"userid={req_body.u_id}").SetWhere($"s_id={req_body.s_id}").SetPageIndex(1).SetPageSize(1).SetOrder("createtime desc");
            var byuser = DataAccess.SystemNoticeByUser_SelectPaged(sysbyuser, out bycount);

            var resp_mb = req_body.ToMaybe().Where(t => t.s_id.ToInt() > 0 && t.u_id.ToInt() > 0).DoWhen(b => list.IsNotNull() && (byuser == null || byuser.Count == 0),
                t => t.ToMaybe().Select<SystemNoticeByUserInfo>(n => new SystemNoticeByUserInfo()
                {
                    s_id = n.s_id.ToInt(),
                    userid = n.u_id.ToInt(),
                    isread = true,
                    isdel = false,
                    createtime = DateTime.Now
                }).Do(f => DataAccess.SystemNoticeByUser_Insert(f)));

            var body = list.ToMaybe().HasValue ? list[0].ToJson() : new SystemNoticeInfo().ToJson();
            var response = Normal_Resp_Create(body);
            return response;

        }

        public static Response Process_SysNotice_SingleDel(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;


            int bycount = 0;
            var req_body = request.GetBody<req_sysnotice>();
            SystemNoticeByUserInfo entity = null;
            
            DatabaseSearchModel sysbyuser = new DatabaseSearchModel().SetWhere($"userid={req_body.u_id}").SetWhere($"s_id={req_body.s_id}").SetPageIndex(1).SetPageSize(1).SetOrder("createtime desc");
            var byuser = DataAccess.SystemNoticeByUser_SelectPaged(sysbyuser, out bycount);

            var result = req_body.ToMaybe().Where(t => t.s_id.ToInt() > 0 && t.u_id.ToInt() > 0)
                .ShiftWhenOrElse(b => (byuser == null || byuser.Count == 0)
                                , t => t.ToMaybe().Select<SystemNoticeByUserInfo>(n => new SystemNoticeByUserInfo()
                                {
                                    s_id = n.s_id.ToInt(),
                                    userid = n.u_id.ToInt(),
                                    isread = false,
                                    isdel = true,
                                    createtime = DateTime.Now
                                }).Select<int>(f => DataAccess.SystemNoticeByUser_Insert(f)).Value
                                , d =>
                                {
                                    byuser[0].isdel = true;
                                    return DataAccess.SystemNoticeByUser_Update(byuser[0]);
                                });

            var resp_mb= result.HasValue&&result.Value>0? new Resp_Binary() { remark = "删除成功", status = true }
                                                        : new Resp_Binary() { remark = "删除失败" };
            var response = Normal_Resp_Create(resp_mb.ToJson());
            return response;
        }

        public static Response Process_SysNotice_AllDel(string u_id)
        {
            var resp_mb = u_id.ToMaybe().FuncWhen(b => b.ToInt() > 0, d => DataAccess.SystemNoticeAll_Del(d.ToInt()))
                .ShiftWhenOrElse(b => b > 0, w => new Resp_Binary() { remark = "删除成功", status = true }
                                        , e => new Resp_Binary() { remark = "删除失败" });
            var body = resp_mb.HasValue ? resp_mb.Value.ToJson() : new Resp_Binary() { remark = "删除失败" }.ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }

        public static Response Process_Claims_Get(Request request)
        {
            var pre_Mb = request.Preprocess2Maybe(false);
            if (pre_Mb.HasValue)
                return pre_Mb.Value;
            int count = 0;
            var req_body = request.GetBody<Req_myClaim>();
            var resp_mb = req_body.ToMaybe().Select<List<ClaimCompanyInfo>>(t => t.ClaimCompany_SelectPaged(out count))
                            .Value.Select(t => new
                            {
                                cccc_oc_code = t.cc_oc_code,
                                cc_oc_name = t.cc_oc_name,
                                cc_date = QZ.Instrument.Utility.Util.DateStringFromNow(t.cc_createTime),
                                cc_id = t.cc_id,
                                cc_status = t.cc_status,
                                visitNum = DataAccess.CompanyEvaluate_Select(t.cc_oc_code).ce_visitNum,
                                oc_data = DataAccess.OrgCompanyExtensionData_SelectPaged(new DatabaseSearchModel().SetTable("OrgCompanyExtensionData_ALL_50000").SetWhere("oc_type=50010").SetWhere($"oc_code={t.cc_oc_code}").SetOrder("oc_id").SetPageIndex(1).SetPageSize(10))
                            });

            var body = resp_mb.IsNotNull() ? resp_mb.ToJson() : new ClaimCompanyInfo().ToJson();
            var response = Normal_Resp_Create(body);
            return response;
        }


        public static Response Process_VipOrder_Submit(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body = request.GetBody<Req_VipOrder>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(request_Body.pay_type == 1 ? Constants.op_AliPay_VipOrder_Submit : Constants.op_WX_VipOrder_Submit);
                DataAccess.AppOrgCompanyLog_Insert(op_Log);   
            var resp_mb = request_Body.Vip_Order_Submit(pre_Ei);
            var response = Normal_Resp_Create(resp_mb.ToJson());
            return response;
        }

        public static Response Process_VipOrder_Notify(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;

            var request_Body= request.GetBody<Req_VipOrder>();
            var op_Log = pre_Ei.Right.To_AppOrgCompanyLog().Set_Uid(request_Body.u_id).Set_Uname(request_Body.u_name).Set_Action(Constants.op_Alipay_VipOrder_Notify);
            DataAccess.AppOrgCompanyLog_Insert(op_Log);
            var resp_mb = request_Body.Vip_Order_Notify();
            var response = Normal_Resp_Create(resp_mb.ToJson());
            return response;
        }

        public static string Process_VipOrder_AliPayNotify(string form)
        {
            var response = Req_Ext.Vip_Order_AliPayNotify(form);
            return response;
        }

        public static Response Process_Invoices_Get(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_myClaim>();

            DatabaseSearchModel model = new DatabaseSearchModel().SetWhere($"invoice_userId={request_Body.u_id}").SetOrder("invoice_state desc,invoice_createTime desc").SetPageIndex(request_Body.pg_index).SetPageSize(request_Body.pg_size);
            int rowcount = 0;
            var resp_mb = DataAccess.InvoiceInfo_SelectPaged(model, out rowcount);
            var json = resp_mb.IsNotNull() ? new { invoices = resp_mb, count = rowcount }.ToJson() : new { invoices = "", count = 0 }.ToJson();
            var response = Normal_Resp_Create(json);
            return response;
        }

        public static Response Process_Invoice_Add(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_InvoiceInfo>();

            var resp_mb = DataAccess.InvoiceInfo_Insert(request_Body.ToMaybe().Select<InvoiceInfo>(t => new InvoiceInfo
            {
                invoice_proName = t.proName,
                invoice_type = t.type,
                invoice_name = t.name,
                invoice_money = t.money,
                invoice_contacts = t.contacts,
                invoice_mobile = t.mobile,
                invoice_desc = t.desc,
                invoice_code = t.code,
                invoice_address = t.address,
                invoice_checkUser = "",
                invoice_checkTime = "",
                invoice_checkRemark = "",
                invoice_userId = t.u_id.ToInt(),
                invoice_user = t.u_name,
                invoice_state = 1,
                invoice_createTime = DateTime.Now
            }).Value);

            var response = Normal_Resp_Create(resp_mb > 0 ? new Resp_Binary { remark = "提交成功", status = true }.ToJson() : new Resp_Binary { remark = "提交失败", status = false }.ToJson());
            return response;
        }

        public static Response Process_Orders_Get(Request request)
        {
            var pre_Ei = request.Preprocess2Either(true);
            if (pre_Ei.HasLeft)
                return pre_Ei.Left;
            var request_Body = request.GetBody<Req_myClaim>();
            int count = 0;
            if (request_Body.type==0)
            {
                var list=request_Body.VipUserOrder_SelectPaged(out count);
                var resp_mb = list.IsNotNull() && list.Count > 0 ? new { outlist = list, count = count }.ToJson() : new { outlist = "", count = 0 }.ToJson();
                return Normal_Resp_Create(resp_mb);
            }
            else
            {
                var list = request_Body.ExcelCompanyOrder_SelectPaged(out count);
                var resp_mb = list.IsNotNull() && list.Count > 0 ? new { outlist = list, count = count }.ToJson() : new { outlist = "", count = 0 }.ToJson();
                return Normal_Resp_Create(resp_mb);
            }
        }

        public static UselessResponse Process_test(string args)
        {
            var response = new UselessResponse(args);
            return response;
        }
        #endregion
    }
}