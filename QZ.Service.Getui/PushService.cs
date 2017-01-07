using System;
using System.Collections.Generic;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using com.igetui.api.openservice.payload;

namespace QZ.Service.Getui
{
    public class PushService
    {

        //参数设置 <-----参数需要重新设置----->
        //http的域名
        private static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";
        //https的域名
        //private static String HOST = "https://api.getui.com/apiex.htm";
        //定义常量, appId、appKey、masterSecret 采用本文档 "第二步 获取访问凭证 "中获得的应用配置
        private static String APPID = "KX21mWt3xo741XdDmWTBQA";
        private static String APPKEY = "4VKRJr7DQP6UTCOOScp5bA";
        private static String MASTERSECRET = "j97JSsvBaF6IcULLlj4B11";
        //private static String DeviceToken = "";  //填写IOS系统的DeviceToken
        /// <summary>
        /// 单个消息推送
        /// </summary>
        /// <param name="TransmissionType">应用启动类型，1：强制应用启动 2：等待应用启动</param>
        /// <param name="TransmissionContent">透传内容</param>
        /// <param name="beginTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="endTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="CLIENTID">个推clientId</param>
        public static void PushMessageToSingle(string TransmissionType, string TransmissionTitle, string TransmissionContent, string beginTime, string endTime, string CLIENTID)
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            //消息模版：TransmissionTemplate:透传模板
            TransmissionTemplate template = TransmissionTemplate(TransmissionType, TransmissionTitle, TransmissionContent, beginTime, endTime);
            // 单推消息模型
            SingleMessage message = new SingleMessage();
            message.IsOffline = true;                                // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
            message.Data = template;
            //判断是否客户端是否wifi环境下推送，2为4G/3G/2G，1为在WIFI环境下，0为不限制环境
            //message.PushNetWorkType = 1;  
            com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
            target.appId = APPID;
            target.clientId = CLIENTID;
            //target.alias = ALIAS;
            try
            {
                String pushResult = push.pushMessageToSingle(message, target);
            }
            catch (RequestException e)
            {
                String requestId = e.RequestId;
                //发送失败后的重发
                String pushResult = push.pushMessageToSingle(message, target, requestId);
            }
        }

        /// <summary>
        /// 多消息推送
        /// </summary>
        /// <param name="Title">通知栏标题</param>
        /// <param name="Text">通知栏内容</param>
        /// <param name="Logo">通知栏显示本地图片</param>
        /// <param name="LogoURL">通知栏显示网络图标</param>
        /// <param name="TransmissionType">应用启动类型，1：强制应用启动  2：等待应用启动</param>
        /// <param name="TransmissionContent">透传内容</param>
        /// <param name="IsRing">接收到消息是否响铃，true：响铃 false：不响铃</param>
        /// <param name="IsVibrate">接收到消息是否震动，true：震动 false：不震动</param>
        /// <param name="IsClearable">接收到消息是否可清除，true：可清除 false：不可清除</param>
        /// <param name="beginTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="endTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="clientIdList">client列表</param>
        public static void PushMessageToList(string Title, string Text, string Logo, string LogoURL, string TransmissionType, string TransmissionContent, bool IsRing, bool IsVibrate, bool IsClearable, string beginTime, string endTime, List<string> clientIdList)
        {
            // 推送主类（方式1，不可与方式2共存）
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            // 推送主类（方式2，不可与方式1共存）此方式可通过获取服务端地址列表判断最快域名后进行消息推送，每10分钟检查一次最快域名
            //IGtPush push = new IGtPush("",APPKEY,MASTERSECRET);
            ListMessage message = new ListMessage();
            NotificationTemplate template = NotificationTemplate(Title, Text, Logo, LogoURL, TransmissionType, TransmissionContent, IsRing, IsVibrate, IsClearable, beginTime, endTime);
            // 用户当前不在线时，是否离线存储,可选
            message.IsOffline = true;
            // 离线有效时间，单位为毫秒，可选
            message.OfflineExpireTime = 1000 * 3600 * 12;
            message.Data = template;
            //message.PushNetWorkType = 0;        //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为不限制网络环境。
            //设置接收者
            List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
            foreach (string item in clientIdList)
            {
                com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                target.appId = APPID;
                target.clientId = item;
                targetList.Add(target);
            }
            String contentId = push.getContentId(message);
            String pushResult = push.pushMessageToList(contentId, targetList);
        }


        /// <summary>
        /// pushMessageToApp
        /// </summary>
        /// <param name="TransmissionType">应用启动类型，1：强制应用启动 2：等待应用启动</param>
        /// <param name="TransmissionContent">透传内容</param>
        /// <param name="beginTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="endTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        public static void pushMessageToApp(string TransmissionType, string TransmissionTitle, string TransmissionContent, string beginTime, string endTime)
        {
            // 推送主类（方式1，不可与方式2共存）
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
            // 推送主类（方式2，不可与方式1共存）此方式可通过获取服务端地址列表判断最快域名后进行消息推送，每10分钟检查一次最快域名
            //IGtPush push = new IGtPush("",APPKEY,MASTERSECRET);
            AppMessage message = new AppMessage();
            // 设置群推接口的推送速度，单位为条/秒，仅对pushMessageToApp（对指定应用群推接口）有效
            message.Speed = 100;
            TransmissionTemplate template = TransmissionTemplate(TransmissionType, TransmissionTitle, TransmissionContent, beginTime, endTime);
            // 用户当前不在线时，是否离线存储,可选
            message.IsOffline = true;
            // 离线有效时间，单位为毫秒，可选  
            message.OfflineExpireTime = 1000 * 3600 * 12;
            message.Data = template;
            //message.PushNetWorkType = 0;        //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为不限制网络环境。
            List<String> appIdList = new List<string>();
            appIdList.Add(APPID);
            //通知接收者的手机操作系统类型
            List<String> phoneTypeList = new List<string>();
            phoneTypeList.Add("ANDROID");
            phoneTypeList.Add("IOS");
            //通知接收者所在省份
            //List<String> provinceList = new List<string>();
            //provinceList.Add("浙江");
            //provinceList.Add("上海");
            //provinceList.Add("北京");
            //List<String> tagList = new List<string>();
            //tagList.Add("开心");
            message.AppIdList = appIdList;
            message.PhoneTypeList = phoneTypeList;
            //message.ProvinceList = provinceList;
            //message.TagList = tagList;
            String pushResult = push.pushMessageToApp(message);
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine("服务端返回结果：" + pushResult);
        }

        /// <summary>
        /// 通知透传模板动作内容
        /// </summary>
        /// <param name="Title">通知栏标题</param>
        /// <param name="Text">通知栏内容</param>
        /// <param name="Logo">通知栏显示本地图片</param>
        /// <param name="LogoURL">通知栏显示网络图标</param>
        /// <param name="TransmissionType">应用启动类型，1：强制应用启动  2：等待应用启动</param>
        /// <param name="TransmissionContent">透传内容</param>
        /// <param name="IsRing">接收到消息是否响铃，true：响铃 false：不响铃</param>
        /// <param name="IsVibrate">接收到消息是否震动，true：震动 false：不震动</param>
        /// <param name="IsClearable">接收到消息是否可清除，true：可清除 false：不可清除</param>
        /// <param name="beginTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="endTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <returns></returns>
        public static NotificationTemplate NotificationTemplate(string Title, string Text, string Logo, string LogoURL, string TransmissionType, string TransmissionContent, bool IsRing, bool IsVibrate, bool IsClearable, string beginTime, string endTime)
        {
            NotificationTemplate template = new NotificationTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            //通知栏标题
            template.Title = Title;
            //通知栏内容     
            template.Text = Text;
            //通知栏显示本地图片
            template.Logo = Logo;
            //通知栏显示网络图标
            template.LogoURL = LogoURL;
            //应用启动类型，1：强制应用启动  2：等待应用启动
            template.TransmissionType = TransmissionType;
            //透传内容  
            template.TransmissionContent = TransmissionContent;
            //接收到消息是否响铃，true：响铃 false：不响铃   
            template.IsRing = IsRing;
            //接收到消息是否震动，true：震动 false：不震动   
            template.IsVibrate = IsVibrate;
            //接收到消息是否可清除，true：可清除 false：不可清除    
            template.IsClearable = IsClearable;
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            String begin = beginTime;
            String end = endTime;
            if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(endTime))
            {
                template.setDuration(begin, end);
            }
            return template;
        }


        /// <summary>
        /// 透传模板动作内容
        /// </summary>
        /// <param name="TransmissionType">应用启动类型，1：强制应用启动 2：等待应用启动</param>
        /// <param name="TransmissionContent">透传内容</param>
        /// <param name="beginTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <param name="endTime">设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）</param>
        /// <returns></returns>
        public static TransmissionTemplate TransmissionTemplate(string TransmissionType, string Transmissiontitle, string TransmissionContent, string beginTime, string endTime)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionType = TransmissionType;
            //透传内容  
            template.TransmissionContent = TransmissionContent;

            APNPayload apnpayload = new APNPayload();
            DictionaryAlertMsg alertMsg = new DictionaryAlertMsg();
            alertMsg.Body = string.Empty;
            alertMsg.ActionLocKey = TransmissionContent;
            alertMsg.LocKey = Transmissiontitle;
            //alertMsg.addLocArg("LocArg");
            alertMsg.addLocArg(Transmissiontitle);
            alertMsg.LaunchImage = "";
            ////IOS8.2支持字段
            alertMsg.Title = "您收到了一条消息";
            alertMsg.TitleLocKey = Transmissiontitle;
            //alertMsg.addTitleLocArg("TitleLocArg");
            alertMsg.addTitleLocArg(Transmissiontitle);
            apnpayload.AlertMsg = alertMsg;
            apnpayload.Badge = 1;
            apnpayload.ContentAvailable = 1;
            ////apnpayload.Category = "";
            //apnpayload.Sound = "test1.wav";
            apnpayload.addCustomMsg("payload", "payload");
            template.setAPNInfo(apnpayload);
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            String begin = beginTime;
            String end = endTime;
            if (!string.IsNullOrEmpty(begin) && !string.IsNullOrEmpty(end))
            {
                template.setDuration(begin, end);
            }
            return template;
        }


        /// <summary>
        /// 网页模板内容
        /// </summary>
        /// <param name="Title">通知栏标题</param>
        /// <param name="Text">通知栏内容</param>
        /// <param name="Logo">通知栏显示本地图片</param>
        /// <param name="LogoURL">通知栏显示网络图标，如无法读取，则显示本地默认图标，可为空</param>
        /// <param name="Url">打开的链接地址</param>
        /// <param name="IsRing">接收到消息是否响铃，true：响铃 false：不响铃 </param>
        /// <param name="IsVibrate">接收到消息是否震动，true：震动 false：不震动</param>
        /// <param name="IsClearable">接收到消息是否可清除，true：可清除 false：不可清除</param>
        /// <returns></returns>
        public static LinkTemplate LinkTemplate(string Title, string Text, string Logo, string LogoURL, string Url, bool IsRing, bool IsVibrate, bool IsClearable)
        {
            LinkTemplate template = new LinkTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            //通知栏标题
            template.Title = Title;
            //通知栏内容 
            template.Text = Text;
            //通知栏显示本地图片 
            template.Logo = Logo;
            //通知栏显示网络图标，如无法读取，则显示本地默认图标，可为空
            template.LogoURL = LogoURL;
            //打开的链接地址    
            template.Url = Url;
            //接收到消息是否响铃，true：响铃 false：不响铃   
            template.IsRing = IsRing;
            //接收到消息是否震动，true：震动 false：不震动   
            template.IsVibrate = IsVibrate;
            //接收到消息是否可清除，true：可清除 false：不可清除
            template.IsClearable = IsClearable;
            return template;
        }

        /// <summary>
        /// 通知栏弹框下载模板
        /// </summary>
        /// <param name="NotyTitle">通知栏标题</param>
        /// <param name="NotyContent">通知栏内容</param>
        /// <param name="NotyIcon">通知栏显示本地图片</param>
        /// <param name="LogoURL">通知栏显示网络图标</param>
        /// <param name="PopTitle">弹框显示标题</param>
        /// <param name="PopContent">弹框显示内容</param>
        /// <param name="PopImage">弹框显示图片</param>
        /// <param name="PopButton1">弹框左边按钮显示文本</param>
        /// <param name="PopButton2">弹框右边按钮显示文本</param>
        /// <param name="LoadTitle">通知栏显示下载标题</param>
        /// <param name="LoadIcon">通知栏显示下载图标,可为空</param>
        /// <param name="LoadUrl">下载地址，不可为空</param>
        /// <param name="IsActived">下载应用完成后，是否弹出安装界面，true：弹出安装界面，false：手动点击弹出安装界面</param>
        /// <param name="IsAutoInstall">应用安装完成后，是否自动启动</param>
        /// <param name="IsBelled">接收到消息是否响铃，true：响铃 false：不响铃</param>
        /// <param name="IsVibrationed">接收到消息是否震动，true：震动 false：不震动</param>
        /// <param name="IsCleared">接收到消息是否可清除，true：可清除 false：不可清除 </param>
        /// <returns></returns>
        public static NotyPopLoadTemplate NotyPopLoadTemplate(string NotyTitle, string NotyContent, string NotyIcon, string LogoURL, string PopTitle, string PopContent, string PopImage, string PopButton1, string PopButton2, string LoadTitle, string LoadIcon, string LoadUrl, bool IsActived, bool IsAutoInstall, bool IsBelled, bool IsVibrationed, bool IsCleared)
        {
            NotyPopLoadTemplate template = new NotyPopLoadTemplate();
            template.AppId = APPID;
            template.AppKey = APPKEY;
            //通知栏标题
            template.NotyTitle = NotyTitle;
            //通知栏内容
            template.NotyContent = NotyContent;
            //通知栏显示本地图片
            template.NotyIcon = NotyIcon;
            //通知栏显示网络图标
            template.LogoURL = LogoURL;
            //弹框显示标题
            template.PopTitle = PopTitle;
            //弹框显示内容    
            template.PopContent = PopContent;
            //弹框显示图片    
            template.PopImage = PopImage;
            //弹框左边按钮显示文本    
            template.PopButton1 = PopButton1;
            //弹框右边按钮显示文本    
            template.PopButton2 = PopButton2;
            //通知栏显示下载标题
            template.LoadTitle = LoadTitle;
            //通知栏显示下载图标,可为空 
            template.LoadIcon = LoadIcon;
            //下载地址，不可为空
            template.LoadUrl = LoadUrl;
            //应用安装完成后，是否自动启动
            template.IsActived = IsActived;
            //下载应用完成后，是否弹出安装界面，true：弹出安装界面，false：手动点击弹出安装界面 
            template.IsAutoInstall = IsAutoInstall;
            //接收到消息是否响铃，true：响铃 false：不响铃
            template.IsBelled = IsBelled;
            //接收到消息是否震动，true：震动 false：不震动   
            template.IsVibrationed = IsVibrationed;
            //接收到消息是否可清除，true：可清除 false：不可清除    
            template.IsCleared = IsCleared;
            return template;
        }
    }
}
