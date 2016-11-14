using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using QZ.Foundation.Monad;
using QZ.Foundation.Model;
using QZ.Foundation.Utility;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
namespace QZ.Service.Enterprise
{
    public static class Extension
    {
        #region Custom Encryption
        /// <summary>
        /// Convert to protocol encryption
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToEncryption(this string input, EncryptType type)
        {
            string c_dyn_key = (type & EncryptType.ResetKey) > 0 ? ConfigurationManager.AppSettings[Constants.C_Dyn] : string.Empty;
            return Cipher_Protocol.EncryptAsBase64(input, c_dyn_key, ConfigurationManager.AppSettings[Constants.S_Dyn], type);
        }

        /// <summary>
        /// Decrypte input string with client key
        /// </summary>
        /// <param name="input"></param>
        /// <param name="c_dyn_key"></param>
        /// <returns></returns>
        public static string Decrypte(this string input, string c_dyn = Constants.C_Dyn)
        {
            return Cipher_Aes.DecryptFromBase64(input, ConfigurationManager.AppSettings[c_dyn]);
        }
        #endregion

        public static string ToSafety(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return Regex.Replace(System.Text.RegularExpressions.Regex.Replace(input, "[';\"\\r\\n]+", string.Empty), "CHAR<\\d+>", string.Empty, RegexOptions.IgnoreCase);
        }

        #region Preprocess request head

        public static Either<string, string> Validate(this Request request)
        {
            Util.Set_Context();   // switch content type of response. It is maybe required by ios client 

            var validation_Mb = request.GetHead()
                .Select<bool>(h => Token.Verify(h.Token, h.Cookie))
                .Select<string>(boolean => boolean ? string.Empty : Constructor.Create_TokErr_Response().ToJson());

            if (validation_Mb.HasValue)
            {
                if (!string.IsNullOrEmpty(validation_Mb.Value))
                    return validation_Mb.Value.ToLeft<string, string>();
                else
                    return new Either<string, string>();
            }
            else
                return Constructor.Create_KeyErr_Response().ToJson().ToLeft<string, string>();
        }

        /// <summary>
        /// validate head of client request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Has left value, left value is error response, or else right value is existed and stores cookie</returns>
        public static Either<Response, string> Preprocess(this Request request)
        {
            Util.Set_Context();   // switch content type of response. It is maybe required by ios client 

            // validate the token
            var validation_Mb = request.GetHead().Select<bool, Either<Response, string>>(
                h => Token.Verify(h.Token, h.Cookie), 
                // check verify result: true -> cookie in right value; false -> token error response in left value
                (h, boolean) => boolean ? h.Cookie.ToRight<Response, string>() : Constructor.Create_TokErr_Response().ToLeft<Response, string>());
            
            return validation_Mb.HasValue ? validation_Mb.Value : Constructor.Create_KeyErr_Response().ToLeft<Response, string>();
        }

        /// <summary>
        /// Preprocess the head of request with statistic ip accessing
        /// </summary>
        /// <param name="request"></param>
        /// <param name="flag">true, do ip statisticing</param>
        /// <returns>Encryption key error or token error response, or else null response</returns>
        public static Maybe<Response> Preprocess2Maybe(this Request request, bool flag = false, bool login = false)
        {
            Util.Set_Context();   // switch content type of response. It is maybe required by ios client 

            var head_Mb = request.GetHead();

            if (!head_Mb.HasValue)
                return Constructor.Create_KeyErr_Response().ToMaybe();

            // validate the token
            var validation_Mb = head_Mb.Select<bool, Response>(
                h => Token.Verify(h.Token, h.Cookie, login),
                // check verify result: true -> no significant return value; false -> token error response for return value
                (h, boolean) => boolean ? null : Constructor.Create_TokErr_Response());


            // statistic ip accessing 
            head_Mb.Where(_ => !validation_Mb.HasValue && flag)  // filter to make sure the token verification is passed
                   //.Where(_ => flag)    // this flag marks whether do ip statisticing
                   .DoWhen(_ => !VisitorStatisticHandler.AuthorizeIp() && new Random().Next(0, 4) % 3 == 0,
                           _ => Thread.Sleep(new Random().Next(2000, 6000)))
                   .DoWhen(h => h.Cookie != null && h.Cookie.Length > 0,
                           h => VisitorStatisticHandler.counter.AddCookie(h.Cookie, Util.Get_RemoteIp()));

            return validation_Mb.HasValue ? validation_Mb.Value : null;
        }

        /// <summary>
        /// Preprocess the head of request with statistic ip accessing
        /// it's used in situation of inserting user operation log into database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="flag">true, do ip statisticing</param>
        /// <param name="login">true -> check if uid info in the token is null or empty</param>
        /// <returns>key error or token error in left value, or else request head in right value</returns>
        public static Either<Response, Request_Head> Preprocess2Either(this Request request, bool flag = false, bool login = false)
        {
            Util.Set_Context();

            var head_Mb = request.GetHead();

            if (!head_Mb.HasValue)
                return Constructor.Create_KeyErr_Response().ToLeft<Response, Request_Head>();

            // validate the token
            var validation_Mb = head_Mb.Select<bool, Response>(
                h => Token.Verify(h.Token, h.Cookie, login),
                // check verify result: true -> no significant return value; false -> token error response for return value
                (h, boolean) => boolean ? null : Constructor.Create_TokErr_Response());

            // statistic ip accessing 
            head_Mb.Where(_ => !validation_Mb.HasValue && flag)  // filter to make sure the token verification is passed, if not passed, ip statisticing will not be processed
                   .DoWhen(_ => !VisitorStatisticHandler.AuthorizeIp() && new Random().Next(0, 4) % 3 == 0,
                           _ => Thread.Sleep(new Random().Next(2000, 6000)))
                   .DoWhen(h => h.Cookie != null && h.Cookie.Length > 0,
                           h => VisitorStatisticHandler.counter.AddCookie(h.Cookie, Util.Get_RemoteIp()));

            return validation_Mb.HasValue ? validation_Mb.Value.ToLeft<Response, Request_Head>() : head_Mb.Value.ToRight<Response, Request_Head>();
        }
        /// <summary>
        /// Preprocess the head of request without validating token
        /// used when token validation is not needed
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Encryption Key error response in left value or cookie in right value</returns>
        public static Either<Response, string> Simple_Preprocess(this Request request, string key = Constants.C_Dyn)
        {
            Util.Set_Context();

            var head_Mb = request.GetHead(key);

            return head_Mb.HasValue ? head_Mb.Value.Cookie.ToRight<Response, string>() : Constructor.Create_KeyErr_Response().ToLeft<Response, string>();
        } 


        /// <summary>
        /// Get second part of request
        /// If second part value is None, cipher key may be dismatch.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="c_dyn"></param>
        /// <returns></returns>
        public static Maybe<Request_Head> GetHead(this Request request, string c_dyn = Constants.C_Dyn)
        {
            // string ->(Decrypt) -> string ->(Dejson) -> Head
            try
            {
                return Cipher_Aes.DecryptFromBase64(request.Head, ConfigurationManager.AppSettings[c_dyn]).ToMaybe()
                    //.Do(json => Util.Log_Info("request head --->", Location.Internal, json, "print request head\n"))
                    .Select<Request_Head>(json => json.ToObject<Request_Head>());
            }
            catch(Exception e)
            {
                return None<Request_Head>.Default;
            }
        }
        #endregion

        /// <summary>
        /// Check if token carried by request is valid
        /// </summary>
        /// <param name="head_Mb"></param>
        /// <returns></returns>
        public static Service_EXE_State CheckToken(this Maybe<Request_Head> head_Mb)
        {
            if (!head_Mb.HasValue)
                return Service_EXE_State.C_Key_Err;

            //var token = new Token();
            return Token.Verify(head_Mb.Value.Token, head_Mb.Value.Cookie) ? Service_EXE_State.Normal : Service_EXE_State.S_Token_Err;
        }

        public static T GetBody<T>(this Request request, string c_dyn = Constants.C_Dyn)
        {
            try
            {
                var request_Body = Cipher_Aes.DecryptFromBase64(request.Body, ConfigurationManager.AppSettings[c_dyn]);

                return request_Body.ToObject<T>();
            }
            catch(Exception e)
            {
                //Util.Log_Info("request body handling", Location.Internal, e.Message, "failed to parse the request body\n\n");
                return default(T);
            }
        }

        public static Maybe<T> Body_Get<T>(this Request request, string c_dyn = Constants.C_Dyn) =>
            Exception_Wrap<T>(() => Cipher_Aes.DecryptFromBase64(request.Body, ConfigurationManager.AppSettings[c_dyn]).ToObject<T>());
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">executing function</param>
        /// <param name="t">default T</param>
        /// <returns></returns>
        public static Maybe<T> Exception_Wrap<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch(Exception e)
            {
                return None<T>.Default;
            }
        }
        public static Maybe<Req_Oc_Comment> User_Valid_Check(this Req_Oc_Comment topic)
        {
            var u_id = topic.u_id.ToInt();
            return u_id > 0 ? topic : None<Req_Oc_Comment>.Default;
        }
        public static Maybe<Req_Cm_Comment> User_Valid_Check(this Req_Cm_Comment comment)
        {
            var u_id = comment.u_id.ToInt();
            return u_id > 0 ? comment : None<Req_Cm_Comment>.Default;
        }
        public static Maybe<Req_Topic_Vote> User_Valid_Check(this Req_Topic_Vote topic)
        {
            var u_id = topic.u_id.ToInt();
            return u_id > 0 ? topic : None<Req_Topic_Vote>.Default;
        }
        public static Maybe<Req_Cm_Topic> User_Valid_Check(this Req_Cm_Topic query)
        {
            var u_id = query.u_id.ToInt();
            return u_id > 0 ? query : None<Req_Cm_Topic>.Default;
        }
        public static Maybe<Req_Info_Query> User_Valid_Check(this Req_Info_Query query)
        {
            var u_id = query.u_id.ToInt();
            return u_id > 0 ? query : None<Req_Info_Query>.Default;
        }
        public static Maybe<Req_Query> User_Valid_Check(this Req_Query query)
        {
            var u_id = query.u_id.ToInt();
            return u_id > 0 ? query : None<Req_Query>.Default;
        }
        public static Maybe<Req_Browse> User_Valid_Check(this Req_Browse browse)
        {
            var u_id = browse.u_id.ToInt();
            return u_id > 0 ? browse : None<Req_Browse>.Default;
        }
        public static Maybe<Req_User_Info> UserValidCheck(this Req_User_Info user)
        {
            var u_id = user.u_id.ToInt();
            return u_id > 0 ? user : None<Req_User_Info>.Default;
        }
        public static Maybe<Req_Oc_Mini> User_Valid_Check(this Req_Oc_Mini company)
        {
            var u_id = company.u_id.ToInt();
            return u_id > 0 ? company : None<Req_Oc_Mini>.Default;
        }
        public static Maybe<Req_Oc_Score> User_Valid_Check(this Req_Oc_Score score)
        {
            var u_id = score.u_id.ToInt();
            return u_id > 0 ? score : None<Req_Oc_Score>.Default;
        }
        public static Maybe<Req_Oc_Correct> User_Valid_Check(this Req_Oc_Correct correct)
        {
            var u_id = correct.u_id.ToInt();
            return u_id > 0 ? correct : None<Req_Oc_Correct>.Default;
        }

        public static bool CompanyTopic_Redundent_Check(this Req_Oc_Comment comment) =>
            Util.PostFilter($"CompanyTopicCache_{comment.oc_code}_{comment.u_id}", comment.topic_content);

        public static bool CommunityTopic_Redundent_Check(this Req_Cm_Comment comment) =>
            Util.PostFilter($"CommunityTopicCache_{comment.u_id}", comment.topic_content);


        public static Dishonest_Dtl Company_Compensate(this Dishonest_Dtl dtl)
        {
            if (dtl.code.Length > 9)
                return dtl;

            var company = DataAccess.OrgCompanyList_Select(dtl.code);
            if (company != null)
            {
                dtl.oc_code = company.oc_code;
                dtl.oc_name = company.oc_name;
                dtl.oc_area = company.oc_area;
            }
            return dtl;
        }

        public static Brand_Dtl Company_Compensate(this Brand_Dtl dtl)
        {
            if (dtl.oc_code.Length > 9)
                return dtl;

            var company = DataAccess.OrgCompanyList_Select(dtl.oc_code);
            if (company != null)
            {
                //dtl.oc_code = company.oc_code;
                dtl.oc_name = company.oc_name;
                dtl.oc_area = company.oc_area;
            }
            return dtl;
        }
        public static Patent_Dtl Company_Compensate(this Patent_Dtl dtl)
        {
            if(dtl.oc_code.Length > 0)
            {
                var company = DataAccess.OrgCompanyList_Select(dtl.oc_code);
                if (company != null)
                {
                    //dtl.oc_code = company.oc_code;
                    dtl.oc_name = company.oc_name;
                    dtl.oc_area = company.oc_area;
                }
            }
            return dtl;
        }
        public static List<Company_Sh> MoneyRatio_Compensate(this List<Company_Sh> list)
        {
            var sum = list.Sum(l => l.sh_money) *100;
            if (sum > 0)
                list.ForEach(l => l.sh_money_ratio = Math.Round(l.sh_money / sum,2));
            return list;
        }
    }
}
