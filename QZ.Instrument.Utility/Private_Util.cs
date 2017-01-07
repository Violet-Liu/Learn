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

namespace QZ.Instrument.Utility
{
    /// <summary>
    /// This Util class will provides some method tools which process business logic, so name this Util as Private_Util.
    /// </summary>
    public class Private_Util
    {
        /// <summary>
        /// transform to a displayed oc_code
        /// </summary>
        /// <param name="oc_code"></param>
        /// <returns></returns>
        public static string To_Code_Display(string oc_code)
        {
            if (oc_code.EndsWith("T")) // 台湾
            {
                return oc_code.TrimEnd('T') + "(台湾统一编码)";
            }
            else if (oc_code.EndsWith("K")) // 香港
            {
                return oc_code.TrimEnd('K').TrimStart('0') + "(香港公司编号)";
            }

            return oc_code;
        }
        /// <summary>
        /// transform to a displayed oc_number
        /// </summary>
        /// <param name="oc_number"></param>
        /// <returns></returns>
        public static string To_Number_Display(string oc_number)
        {
            if (oc_number.EndsWith("T") || oc_number.EndsWith("K")) // 台湾
            {
                return "";
            }
            return oc_number;
        }

        public static string Operation_Status_Get(string oc_ext)
        {
            string BusinessStatus = "在业";
            if (string.IsNullOrEmpty(oc_ext) || oc_ext.Contains("在业") || oc_ext.Contains("迁入") || oc_ext.Contains("确立") || oc_ext.Contains("登记成立"))
            {
                BusinessStatus = "在业";
            }
            else if (oc_ext.Contains("吊销"))
            {
                BusinessStatus = "吊销";
            }
            else if (oc_ext.Contains("注销"))
            {
                BusinessStatus = "注销";
            }
            else if (oc_ext.Contains("停业"))
            {
                BusinessStatus = "停业";
            }
            else if (oc_ext.Contains("清算"))
            {
                BusinessStatus = "清算";
            }
            else if (oc_ext.Contains("存续"))
            {
                BusinessStatus = "存续";
            }
            else if (oc_ext.Contains("核准成立") || oc_ext.Contains("核准设立"))
            {
                BusinessStatus = "核准设立";
            }
            else if (oc_ext.Contains("解散"))
            {
                BusinessStatus = "解散";
            }
            else if(oc_ext.Contains("迁出"))
            {
                BusinessStatus = "迁出";
            }
            else
            {
                BusinessStatus = "在业";
            }
            return BusinessStatus;
        }

        public static bool Normal_Filter(string oc_ext)
        {
            if (oc_ext.Contains("吊销"))
                return false;
            if (oc_ext.Contains("注销"))
                return false;
            if (oc_ext.Contains("停业"))
                return false;
            if (oc_ext.Contains("清算"))
                return false;
            if (oc_ext.Contains("解散"))
                return false;
            return true;
        }
    }
}
