using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QZ.Service.Enterprise
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Community”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Community.svc 或 Community.svc.cs，然后开始调试。
    [ErrorBehavior]
    [VisitorStaticticBehavior]
    public class Community : ICommunity
    {
        public Response Topic_Add(Request request) => ServiceImpl.Process_Community_Topic_Add(request);

        public Response Reply_Add(Request request) => ServiceImpl.Process_Community_Reply_Add(request);

        public Response Topic_UpDown_Vote(Request request) => ServiceImpl.Process_Community_Topic_UpDown_Vote(request);

        public Response Topic_Detail(Request request) => ServiceImpl.Process_Community_Topic_Detail(request);
        public Response Topic_Query(Request request) => ServiceImpl.Process_Community_Topic_Query(request);
        public Response Topics_Hot(Request request) => ServiceImpl.Process_Topics_Hot(request);


    }
}
