using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using QZ.Foundation.Model;

namespace QZ.Instrument.LogClient
{
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
