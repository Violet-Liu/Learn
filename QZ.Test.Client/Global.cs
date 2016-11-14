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
using QZ.Instrument.Model;

namespace QZ.Test.Client
{
    public class Global
    {
        private static Global _instance = new Global();
        public static Global Instance { get { return _instance; } }
        protected Global()
        { }

        #region Global States
        private string _c_Dyn_Key;
        /// <summary>
        /// request data should be aes-encrypted using this key at client end
        /// </summary>
        public string C_Dyn_Key
        {
            get { return _c_Dyn_Key ?? Constants.C_Dyn_Key; }
            set { _c_Dyn_Key = value; }
        }
        #endregion
        public string Token_0 { get; set; } = "upbbYDKRSO+JM9INEJx5b00Z+UrkBwwhETNXWcPCJ3TNUsQ4ODVOe+XMlITk1znNTs7/z84+GmkmPltPidnIpX5lNY7Csn5nMq481HGzIZ0="; // "来咯哦哦" user session token
        /// <summary>
        /// unlogin
        /// </summary>
        public string Token_1 { get; set; } = "1g7mhjIqeKSm5LSZs3J2qe4Rw8BFu9HjEwyGM2IHyK4m1G3t83hysNnHUlXxbGlHQ0lq/dVrEt/khIJwl0I36r/kq7Di8nbYl95Li6jk9iPJ7Y7CFQKs1zoUDv8hhip5";
            //"1g7mhjIqeKSm5LSZs3J2qe4Rw8BFu9HjEwyGM2IHyK4m1G3t83hysNnHUlXxbGlHQ0lq/dVrEt/khIJwl0I36r/kq7Di8nbYl95Li6jk9iPJ7Y7CFQKs1zoUDv8hhip5";
        #region Index
        public Resp_Index Resp_Index { get; set; }
        
        public void Set_Resp_Index(Resp_Index resp_Index)
        {
            this.Resp_Index = resp_Index;
        }
        #endregion

        #region Company_Query
        public Resp_Oc_Abs Resp_Oc_Abs { get; set; }
        public void Set_Resp_Oc_Abs(Resp_Oc_Abs resp_oc_abs)
        {
            this.Resp_Oc_Abs = resp_oc_abs;
        }
        #endregion

        #region response
        public string Response { get; set; }
        public Response Response_1 { get; set; }
        #endregion
    }
}
