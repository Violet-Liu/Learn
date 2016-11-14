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
    public class Calibrater
    {
        public static int Pg_Index_Check(int pg_index)
        {
            if (pg_index < 1 || pg_index > 50)
            {
                return 1;
            }
            return pg_index;
        }
        public static int Pg_Size_Check(int pg_size)
        {
            if (pg_size < 1 || pg_size > 30)
            {
                return 10;
            }
            return pg_size;
        }
    }
}
