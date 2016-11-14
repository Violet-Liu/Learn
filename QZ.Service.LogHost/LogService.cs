using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;
using QZ.Service.Log;

namespace QZ.Service.LogHost
{
    public partial class LogService : ServiceBase
    {
        private static readonly string path = @".\private$\logmsmq";
        private ServiceHost _host;
        public LogService()
        {
            InitializeComponent();
            _host = new ServiceHost(typeof(Logger));
        }

        protected override void OnStart(string[] args)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, true);
            }
            _host.Open();
        }

        protected override void OnStop()
        {
            if(_host != null && _host.State < CommunicationState.Closing)
            {
                _host.Close();
            }
        }
    }
}
