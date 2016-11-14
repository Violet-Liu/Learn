using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QZ.Foundation.Model;

namespace QZ.Service.Log
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Logger : ILogger
    {
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Log_Error(Log_M log_M)
        {
            LogUtil.Log_Error(log_M);
        }
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Log_Fatal(Log_M log_M)
        {
            LogUtil.Log_Fatal(log_M);
        }
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Log_Info(Log_M log_M)
        {
            LogUtil.Log_Info(log_M);
        }
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Log_Warn(Log_M log_M)
        {
            LogUtil.Log_Warn(log_M);
        }
    }
}
