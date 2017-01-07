/********************************************************
 *
 *  File:   WebClient1.cs
 * 
 *  Class:  WebClient1
 * 
 *  Description:
 * 
 *  Author: 陈力斌
 * 
 *  Create: 11/16/2016 11:21:36 AM
 * 
 *  Copyright(c) 2016 深圳前瞻资讯股份有限公司 all rights reserved
 * 
 *  Revision history:
 *      R1:
 *          修改作者：   
 *          修改日期：   
 *          修改理由：   
 *                                                 
 *
 ********************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace QZ.Test.Client.Service
{
    public class WebClient1:WebClient
    {
        private int _timeout { get; set; }

        public WebClient1()
        {
            this._timeout = 60000;
        }

        public WebClient1(int timeout)
        {
            this._timeout = timeout;
        }
        
        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
