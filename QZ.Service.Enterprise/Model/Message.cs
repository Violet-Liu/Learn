using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace QZ.Service.Enterprise.Model
{
    public class Message
    {
        [JsonProperty("h")]
        public string Header { get; set; }
        [JsonProperty("b")]
        public string Body { get; set; }
    }
}