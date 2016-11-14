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
using log4net;
using QZ.Foundation.Model;

namespace QZ.Service.Log
{
    public class LogUtil
    {
        private const string _info = "Info";
        private const string _warn = "Warn";
        private const string _error = "Error";
        private const string _fatal = "Fatal";

        public static void Log_Info(Log_M log_M)
        {
            LogManager.GetLogger(_info).Info(log_M.ToString());
        }
        public static void Log_Warn(Log_M log_M)
        {
            LogManager.GetLogger(_warn).Warn(log_M.ToString());
        }
        public static void Log_Error(Log_M log_M)
        {
            LogManager.GetLogger(_error).Error(log_M.ToString());
        }
        public static void Log_Fatal(Log_M log_M)
        {
            LogManager.GetLogger(_fatal).Fatal(log_M.ToString());
        }
    }
}
