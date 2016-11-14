/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * SpSet is a set of names of store procedures. Listing there store procedure names can help reduce redundance when you coding DAL classes.
 * Make sure store procedure names is abided by the following rules(naming conventions)
 *
 *
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Instrument.DataAccess
{
    public class SpSet
    {
        public static readonly IDictionary<string, string> Dict = new Dictionary<string, string>()
        {
            ["AppTeiziReply_Insert"] = "Proc_AppTeiziReply_Insert",
            ["AppTeiziReply_Select_By_atr_id"] = "Proc_AppTeiziReply_Selectbyatr_id",
        };
        
    }
}
