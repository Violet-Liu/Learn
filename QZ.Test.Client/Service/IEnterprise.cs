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
using QZ.Instrument.Model;

namespace QZ.Test.Client
{
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "Service.IEnterprise")]
    public interface IEnterprise
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEnterprise/Index", ReplyAction = "http://tempuri.org/IEnterprise/IndexResponse")]
        string Index(string request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEnterprise/Index", ReplyAction = "http://tempuri.org/IEnterprise/IndexResponse")]
        Task<string> IndexAsync(string request);
    }
}
