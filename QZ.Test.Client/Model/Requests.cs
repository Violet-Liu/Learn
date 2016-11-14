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
using Bogus;

namespace QZ.Test.Client
{
    public class Req_Index
    {
        private static int _index = 0;
        private static string[] Names = new[] { "来咯哦哦", "" };
        
        public string u_name { get; set; }
        public static Faker<Req_Index> Generators { get; } =
            new Faker<Req_Index>()
            .RuleFor(p => p.u_name, p => Names[_index++]);

        public static IList<Req_Index> Req_Indices { get; } =
            Generators.Generate(2).ToList();
    }
}
