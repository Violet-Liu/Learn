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

namespace QZ.Instrument.Model
{
    [DbEntity]
    public class User_Mini_Info
    {
        [Id]
        public int u_id { get; set; }
        public string u_name { get; set; }
        public byte u_type { get; set; }
        public string u_face { get; set; }
        public string u_email { get; set; }
        public bool u_email_status { get; set; }
    }
}
