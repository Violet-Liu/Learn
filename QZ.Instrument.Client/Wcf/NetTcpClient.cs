/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace QZ.Instrument.Client
{
    public class NetTcpClient<T> where T : class
    {
        private static NetTcpBinding _binding;
        static NetTcpClient()
        {
            Initialize();
        }

        private static void Initialize()
        {
            var binding = new NetTcpBinding(SecurityMode.None, false);
            binding.CloseTimeout = new TimeSpan(0, 0, 10);
            binding.ListenBacklog = 0x1000;
            binding.MaxBufferPoolSize = Int32.MaxValue;
            binding.MaxBufferSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = Int32.MaxValue;
            binding.OpenTimeout = binding.CloseTimeout;

            binding.SendTimeout = new TimeSpan(0, 10, 0);
            binding.PortSharingEnabled = false;
            binding.TransactionFlow = false;
            binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
            binding.ReaderQuotas.MaxDepth = Int32.MaxValue;
            binding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
            binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
            _binding = binding;
        }


        public static T CreateChannel(string uri)
        {
            var factory = new ChannelFactory<T>(_binding, new EndpointAddress(uri));
            return factory.CreateChannel();
        }
    }
}
