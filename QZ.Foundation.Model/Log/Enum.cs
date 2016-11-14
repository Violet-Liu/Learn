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

namespace QZ.Foundation.Model
{
    public enum Severity
    {
        Debug=0,
        Info,
        Warn,
        Error,
        Fatal,
        Verbose
    }

    public enum Location
    {
        Enter=0,
        Internal,
        Exit,
    }
}
