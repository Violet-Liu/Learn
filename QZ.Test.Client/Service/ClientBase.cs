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
using System.ServiceModel.Description;
using System.Net;
using System.Net.Http;

namespace QZ.Test.Client
{
    public abstract class ClientBase
    {
        public T CreateChannel<T>(string uri)
        {
            var behavior = new WebHttpBehavior();
            behavior.HelpEnabled = true;
            
            behavior.DefaultBodyStyle = System.ServiceModel.Web.WebMessageBodyStyle.Bare;
            behavior.DefaultOutgoingRequestFormat = System.ServiceModel.Web.WebMessageFormat.Json;
            behavior.DefaultOutgoingResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json;

            var binding = new WebHttpBinding();
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            binding.MaxBufferPoolSize = 10485760;
            
            binding.AllowCookies = true;
            binding.CloseTimeout = TimeSpan.FromMinutes(1);
            binding.OpenTimeout = TimeSpan.FromMinutes(1);
            
            binding.WriteEncoding = Encoding.UTF8;

            binding.Security.Mode = WebHttpSecurityMode.None;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

            //var ei = EndpointIdentity.CreateDnsIdentity("localhost");
            var epa = new EndpointAddress(uri);
            
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, epa);
            
            //循环该频道所要连接的服务终结点
            foreach (OperationDescription op in factory.Endpoint.Contract.Operations)
            {
                //op.Behaviors中的IOperationBehavior是所有Behavior的父接口，DataContractSerializerOperationBehavior实现了该接口
                //获取与DataContractSerializerOperationBehavior该操作有关的行为
                DataContractSerializerOperationBehavior dataContractBehavior = op.Behaviors.Find<DataContractSerializerOperationBehavior>() as DataContractSerializerOperationBehavior;

                //设置该操作的最大序列化项数
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = 0x100000;
                }
            }

            //返回一个对应服务的实例如IIRSSalesManage
            return factory.CreateChannel();
        }

        public string Get_Async(string uri)
        {
            var client = new HttpClient();
            var task = client.GetStringAsync(uri);
            return task.Result;
        }
        public Task<string> GetAsync(string uri)
        {
            var client = new HttpClient();
            return  client.GetStringAsync(uri);
        }

        public Task<string> PostAsync(string uri, string request)
        {
            var content = new StringContent(request);
            var client = new HttpClient();
            var task = client.PostAsync(uri, content);
            return task.Result.Content.ReadAsStringAsync();

            //using (var wc = new System.Net.WebClient())
            //{
            //    return wc.UploadDataTaskAsync(uri, Encoding.UTF8.GetBytes(request));
            //}
        }

        public string Post(string uri, string request)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");
                var bytes = client.UploadData(uri, "POST", Encoding.UTF8.GetBytes(request));
                return Encoding.UTF8.GetString(bytes);
            }
        }

        public string Get(string uri)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/json; charset=utf-8");
                return client.DownloadString(uri);
            }
        }
    }
}
