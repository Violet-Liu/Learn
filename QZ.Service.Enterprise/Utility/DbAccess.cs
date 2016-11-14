/*
 * Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * This class provides many functions that is convenient to do CRUD operations to database.
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.Model;
using QZ.Instrument.DataAccess;
using QZ.Foundation.Monad;

namespace QZ.Service.Enterprise
{
    public class DbAccess
    {
        public static int LogError_Insert(LogError log)
        {
            var access = new DataAccess<LogError>("SysLog");
            return access.Insert(log);
        }

        public static Maybe<Instrument.Model.User_Mini_Info> User_Get(string u_name)
        {
            var access = new DataAccess<Instrument.Model.User_Mini_Info>("QZNewSite_User");
            var user_Mb = access.Select(u => u.u_name, u_name);
            return user_Mb;
        }
    }
}
