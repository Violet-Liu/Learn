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
using System.Runtime.Serialization;
using System.Configuration;
using Newtonsoft.Json;

namespace QZ.Instrument.Model
{
    /// <summary>
    /// Response which is returned from server and then sent to client
    /// </summary>
    [DataContract]
    public class Response
    {
        [DataMember(Name = "h")]
        public string Head { get; set; }
        [DataMember(Name = "b")]
        public string Body { get; set; }
        public Response(string head, string body)
        {
            Head = head;
            Body = body;
        }
    }

    /// <summary>
    /// Header of response
    /// </summary>
    [DataContract]
    public class Response_Head
    {
        /// <summary>
        /// version of current message header
        /// </summary>
        [DataMember(Name = "ver")]
        public string Version { get; set; }
        /// <summary>
        /// text of message
        /// </summary>
        [DataMember(Name = "txt")]
        public string Text { get; set; }
        /// <summary>
        /// action instrumented by message
        /// </summary>
        [DataMember(Name = "act")]
        public Message_Action Action { get; set; }
        /// <summary>
        /// message code
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        public Response_Head(Message_Action action, string text = "", int code = 0)
        {
            this.Action = action;
            this.Text = text;
            this.Code = code;
            this.Version = ConfigurationManager.AppSettings[Constants.S_Msg];
        }
    }


    public enum Message_Action
    {
        None = 0,   // 
        Info,       // show a normal message text
        Warning,    // show a warning message text
        Logic_Err=10,
        Sys_Err = 11,      // show an error message text       // if level large than or equal to Error, then client should not handle the response body
        Jump = 12,       // jump
        Login = 13,       // relogin
        Request = 14
    }
}
