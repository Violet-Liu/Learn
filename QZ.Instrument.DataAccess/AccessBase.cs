﻿/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace QZ.Instrument.DataAccess
{
    public class AccessBase
    {
        static AccessBase()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }
    }
}
