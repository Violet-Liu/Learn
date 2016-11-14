/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * ServerException is used when any server exception is throw out. This exception contains some custom parameters that help to analyze what kind of error happened
 *
 *
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Service.Enterprise
{
    public class ExceptionServer : Exception
    {
        public string u_id { get; set; }
        public string u_name { get; set; }
    }
}
