using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using QZ.Service.Log;

namespace QZ.Test.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, true);
            }

            using (var host = new ServiceHost(typeof(Logger)))
            {
                host.Opened += delegate { Console.WriteLine("service has beed started."); };
                host.Open();
                Console.ReadLine();
            }
        }

        private static readonly string path = @".\private$\logmsmq";
    }
}
