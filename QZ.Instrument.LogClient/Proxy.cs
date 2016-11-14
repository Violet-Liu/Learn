using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using QZ.Foundation.Model;

namespace QZ.Instrument.LogClient
{
    public class Proxy
    {
        //private static string Uri = "net.msmq://localhost/private/logmsmq";
        private ILogger _logger;
        public Proxy()
        {
            _logger = Factory.CreateChannel();
        }
        public void Log_Info(Log_M log_M)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required))
            {
                _logger.Log_Info(log_M);
                scope.Complete();
            }
        }
        public void Log_Warn(Log_M log_M)
        {
            _logger.Log_Warn(log_M);
        }
        public void Log_Error(Log_M log_M)
        {
            _logger.Log_Error(log_M);
        }
        public void Log_Fatal(Log_M log_M)
        {
            _logger.Log_Fatal(log_M);
        }
    }
}
