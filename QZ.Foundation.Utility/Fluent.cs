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

namespace QZ.Foundation.Utility
{
    public static class Fluent
    {
        public static T Assign_1<T, U>(T self, Action<U> assign) where T : class, U
        {
            assign(self);
            return self;
        }

        public static T Assign_0<T>(T self, Action<T> assign) where T : class
        {
            assign(self);
            return self;
        }
    }
}
