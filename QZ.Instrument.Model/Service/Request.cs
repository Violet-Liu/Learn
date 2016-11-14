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
using Newtonsoft.Json;
using Bogus;
using QZ.Foundation.Utility;

namespace QZ.Instrument.Model
{
    [DataContract]
    public class Request
    {
        [DataMember(Name = "h")]
        public string Head { get; set; }
        [DataMember(Name = "b")]
        public string Body { get; set; }

        public Request(string head, string body)
        {
            Head = head;
            Body = body;
        }
    }
    public class Request_Head
    {
        [JsonProperty("screensize")]
        public string Screen_Size { get; set; }
        [JsonProperty("cookie")]
        public string Cookie { get; set; }
        [JsonProperty("platform")]
        public Platform Platform { get; set; }
        [JsonProperty("appver")]
        public string App_Ver { get; set; }
        [JsonProperty("token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Token { get; set; }

        public Request_Head Set_Token(string token) => Fluent.Assign_0(this, h => h.Token = token);

        public override string ToString()
        {
            return $"screen_size:{Screen_Size}\tcookie:{Cookie}\tplatform:{Platform}\tapp_ver:{App_Ver}\ttoken:{Token}";
        }
        public static Faker<Request_Head> Generators { get; } =
            new Faker<Request_Head>()
            .RuleFor(p => p.Screen_Size, p => "5'")
            .RuleFor(p => p.Cookie, p => "35506505331100101")
            .RuleFor(p => p.Platform, p => Platform.Android)
            .RuleFor(p => p.App_Ver, p => "v0");

        public static IEnumerable<Request_Head> Request_Heads { get; } =
            Generators.Generate(1);
    }
    public enum Platform
    {
        Iphone = 0,
        Ipad,
        Android = 3,
        Winphone
    }
}
