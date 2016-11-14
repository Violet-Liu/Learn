using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace QZ.Service.Enterprise
{
    [ServiceContractAttribute(Namespace = "http://entprise.qianzhan.com/CompanyNameIndexService", ConfigurationName = "OrgCompanyNameIndexService.CompanyNameIndexService")]
    public interface ICompanyNameIndex
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Stor" +
            "e", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Stor" +
            "eResponse")]
        bool Store(string companyName, string orgCode);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Stor" +
            "eList", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Stor" +
            "eListResponse")]
        int StoreList([System.ServiceModel.MessageParameterAttribute(Name = "storeList")] System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> storeList1);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "h", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hResponse")]
        System.Collections.Generic.KeyValuePair<string, string> Fetch(string companyName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hes", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hesResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> Fetches(System.Collections.Generic.List<string> companyNameList);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hReltaions", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hReltaionsResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> FetchReltaions(string companyName, int page, int pageSize);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hList", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hListResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> FetchList(string companyName, int page, int pageSize);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hAll", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Fetc" +
            "hAllResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> FetchAll(string companyName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Extr" +
            "act", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Extr" +
            "actResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> Extract(string input);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Perf" +
            "ixFetch", ReplyAction = "http://entprise.qianzhan.com/CompanyNameIndexService/CompanyNameIndexService/Perf" +
            "ixFetchResponse")]
        System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, string>> PerfixFetch(System.Collections.Generic.List<string> perfixList, string input, int limit);
    }
    public interface ICompanyNameIndexChannel : ICompanyNameIndex, IClientChannel
    { }
}
