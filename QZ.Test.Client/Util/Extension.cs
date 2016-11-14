/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.Utility;
using QZ.Instrument.Model;
using QZ.Foundation.Monad;

namespace QZ.Test.Client
{
    public static class Extension
    {
        /// <summary>
        /// AES encrypte with a specified key 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToEncryption(this string input, string key = null)
        {
            return Cipher_Aes.EncryptToBase64(input, key ?? Global.Instance.C_Dyn_Key);
        }

        /// <summary>
        /// Decrypted througn custom protocal
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDecryption(this string input) => Cipher_Protocol.DecyprtToString(input);

        /// <summary>
        /// Visit response head
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static Maybe<string> Visit_Resp_Head(this Response_Head head)
        {
            if (head.Action >= Message_Action.Jump)
            {
                // action executing...
                Console.WriteLine(head.Action.ToString());
            }
            else if(head.Action > Message_Action.None)
            {
                Console.WriteLine(head.Action.ToString() + head.Text);  // print text for user
            }

            // if action > logic_err, then the body of response needn't to be visited
            return head.Action < Message_Action.Logic_Err ? string.Empty : null;
        }

        /// <summary>
        /// Process response head. This is the first step after receiving response at client end
        /// If suceed, return encrypted string of response body, else return null.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Maybe<string> Process_Head(this Response response)
        {
            return response.Head.ToDecryption().DoExt(s => Global.Instance.Response += s).ToObject<Response_Head>().Visit_Resp_Head().Select<string>(s => response.Body);
        }


        public static void Response_Handle<T>(this Response response) => response.Process_Head().Do(body => body.ToDecryption().DoExt(s => Global.Instance.Response += s).ToObject<T>());

        public static void Response_Handle_1<T>(this Response response) => response.Process_Head().Do(body => body.ToDecryption().DoExt(s => Global.Instance.Response += Cipher_Gzip.DeCompress(s.ToObject<Judge_Dtl>().content)));
    }
}
