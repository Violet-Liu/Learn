using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace QZ.Service.Enterprise.Model
{
    public class Req_User
    {
        [JsonProperty("u_id")]
        public int Id { get; set; }
        [JsonProperty("u_name")]
        public string Name { get; set; }
        [JsonProperty("u_tel")]
        public string Tel { get; set; }
    }
}