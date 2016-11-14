/*
 * Author: Sha Jianjian
 * Date: 2015-05-30
 * Description: Define models of request parameter in WCF service
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QZ.Service.Enterprise
{
    [DataContract]
    public class Request
    {
        [DataMember(Name = "h")]
        public string Head { get; set; }
        [DataMember(Name = "b")]
        public string Body { get; set; }
    }

    //public class Reqeust_Head
    //{
    //    [JsonProperty("screensize")]
    //    public string Screen_Size { get; set; }
    //    [JsonProperty("cookie")]
    //    public string Cookie { get; set; }
    //    [JsonProperty("platform")]
    //    public int Platform { get; set; }
    //    [JsonProperty("appver")]
    //    public string App_Ver { get; set; }
    //    [JsonProperty("token")]
    //    public string Token { get; set; }
    //}
}
