using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using QZ.Instrument.Utility;

namespace QZ.Service.Enterprise
{
    /// <summary>
    /// Model that used to generate a token.
    /// </summary>
    public class Token
    {
        private IDictionary<string, string> _fields = new Dictionary<string, string>();
        public IDictionary<string, string> Fields { get { return _fields; } }

        /// <summary>
        /// user id, comes from app client
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// user name, comes from app client
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// cookieid, with an unique attribute, which comes from app client
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// the lastest time when user's login
        /// </summary>
        public string Sometime { get; set; }

        public string Ustatus { get; set; }

        public Token()
        { }

        public Token(string cookie, string uid = "", string uname = "",string ustatus="")
        {
            Uid = uid;
            Uname = uname;
            Cookie = cookie;
            Ustatus = ustatus;
            Sometime = DateTime.Now.ToString();
        }

        #region Tokenization
        /// <summary>
        /// Compose all fields
        /// </summary>
        /// <param name="Encrypt_CookieId"></param>
        /// <returns></returns>
        public Token Compose(Func<string, string> Encrypt_CookieId)
        {
            if(string.IsNullOrEmpty(Uid))
                _fields.Add(nameof(Uid), Uid);
            if(string.IsNullOrEmpty(Uname))
                _fields.Add(nameof(Uname), Uname);
            if(string.IsNullOrEmpty(Sometime))
                _fields.Add(nameof(Sometime), Sometime);
            if (string.IsNullOrEmpty(Ustatus))
                _fields.Add(nameof(Ustatus), Ustatus);       
            _fields.Add(nameof(Cookie), Cookie);
            _fields.Add("encrypt_" + nameof(Cookie), Encrypt_CookieId(Cookie));
            
            return this;
        }

        /// <summary>
        /// Induce all fields to generate a token
        /// </summary>
        /// <returns></returns>
        public string Induce()
        {
            return Cipher_Aes.EncryptToBase64(_fields.ToJson(), ConfigurationManager.AppSettings[Constants.S_Tok]);
        }
        #endregion

        #region Detokenization
        /// <summary>
        /// Verify if the token is valid 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="benchmark">value that used to execute comparison</param>
        /// <returns>token is valid, return true, or else return false</returns>
        public static bool Verify(string token, string benchmark, bool login = false)
        {
            if (string.IsNullOrEmpty(benchmark))
                return false;
            try
            {
                var de = Cipher_Aes.DecryptFromBase64(token, ConfigurationManager.AppSettings[Constants.S_Tok]);
                var fields_Mb = de
                .ToMaybe().Select(json => json.ToObject<IDictionary<string, string>>().ToMaybe());

                if (fields_Mb.HasValue)
                {
                    string cookie = null;

                    //if(login)
                    //{
                    //    string uid = null;
                    //    if(fields_Mb.Value.TryGetValue(nameof(Uid), out uid))
                    //    {
                    //        return true;
                    //        //return !string.IsNullOrEmpty(uid);
                    //    }
                        
                    //    return false;
                    //}

                    if (fields_Mb.Value.TryGetValue(nameof(Cookie), out cookie))
                    {
                        return benchmark.Equals(cookie);
                    }
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        public static bool VerifyBlack(string token)
        {
            try
            {
                var de = Cipher_Aes.DecryptFromBase64(token, ConfigurationManager.AppSettings[Constants.S_Tok]);
                var fields_Mb = de
                .ToMaybe().Select(json => json.ToObject<IDictionary<string, string>>().ToMaybe());
                if (fields_Mb.HasValue)
                {
                    string ustatus = "";
                    if (fields_Mb.Value.TryGetValue(nameof(Ustatus), out ustatus))
                    {
                        return ustatus == "5";
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
