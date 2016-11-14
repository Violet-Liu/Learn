using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using QZ.Foundation.Model;

namespace QZ.Service.Log
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface ILogger
    {
        [OperationContract(IsOneWay = true)]
        void Log_Info(Log_M log_M);
        [OperationContract(IsOneWay = true)]
        void Log_Warn(Log_M log_M);
        [OperationContract(IsOneWay = true)]
        void Log_Error(Log_M log_M);
        [OperationContract(IsOneWay = true)]
        void Log_Fatal(Log_M log_M);
    }
}
