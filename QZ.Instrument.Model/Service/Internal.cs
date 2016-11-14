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
    public class User_Dtl : User_Mini_Info
    {
        public string u_pwd { get; set; }
        public int u_status { get; set; }
        public int login_count { get; set; }
        public string last_login_time { get; set; }
        public string cur_login_time { get; set; }
        public int u_exp { get; set; }     /* 积分 */

    }

    public class User_Login_Result
    {
        public Login_State State { get; set; }
        public int u_id { get; set; }
        public string u_name { get; set; }
    }

    public class Es_Brand_Query
    {
        public string oc_code { get; set; }
        public string reg_no { get; set; }  /* register number */
        public string name { get; set; }    /* brand name */
        public string applicant { get; set; }
        public int pg_index { get; set; }
        public int pg_size { get; set; }
    }
    public class Es_Patent_Query
    {
        public string oc_code { get; set; }
        public string oc_name { get; set; }
        public string patent_no { get; set; }       /* patent number */
        public string patent_public_no { get; set; }    /* public number */
        public string name { get; set; }

    }

    public class Resp_OpenUser_Login
    {
        /// <summary>
        /// login status
        /// </summary>
        public bool status { get; set; }
        public string remark { get; set; }
        public int u_id { get; set; }
        public string u_name { get; set; }
        public string u_face { get; set; }
        public string token { get; set; }
    }

    public class Index_Pic
    {
        public string title { get; set; } = string.Empty;
        public string img_src { get; set; }
        public string href { get; set; }
    }
}
