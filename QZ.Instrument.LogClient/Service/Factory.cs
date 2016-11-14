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
using System.ServiceModel;

namespace QZ.Instrument.LogClient
{
    class Factory
    {
        public static ILogger CreateChannel()
        {
            //var binding = new NetMsmqBinding();
            //binding.ExactlyOnce = true;
            //binding.Durable = true;

            //var ei = EndpointIdentity.CreateDnsIdentity("localhost");
            //var epa = new EndpointAddress(uri);

            //var factory = new ChannelFactory<ILogger>(binding, epa);

            var channelFactory = new ChannelFactory<ILogger>("logEndpoint");
            return channelFactory.CreateChannel();
        }
    }
}
