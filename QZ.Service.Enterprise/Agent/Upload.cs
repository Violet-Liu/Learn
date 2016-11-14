using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZ.Service.Enterprise
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "UploadSoap", Namespace = "http://service.qianzhan.com/")]
    public partial class Upload : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;

        private System.Threading.SendOrPostCallback UploadImageGenerateQZReportCoverOperationCompleted;

        private System.Threading.SendOrPostCallback UploadImage4AlbumWithFixedSizeOperationCompleted;

        private System.Threading.SendOrPostCallback UploadImage4AlbumWithFixedSize1OperationCompleted;

        private System.Threading.SendOrPostCallback UploadImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadImageWithFixedThumbnailsOperationCompleted;

        private System.Threading.SendOrPostCallback UploadUserHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadUserHeaderSourceImgOperationCompleted;

        private System.Threading.SendOrPostCallback UploadUserHeaderImage1OperationCompleted;

        private System.Threading.SendOrPostCallback UploadPeopleHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadCommonHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadReportHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadAnalystHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadReportCatalogHeaderImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadFixedNameImageOperationCompleted;

        private System.Threading.SendOrPostCallback CreateRotateImageOperationCompleted;

        private System.Threading.SendOrPostCallback UploadImageWithFixedThumb4OutsidePicOperationCompleted;

        private System.Threading.SendOrPostCallback UploadFileOperationCompleted;

        private System.Threading.SendOrPostCallback UploadFileFixNameOperationCompleted;

        private System.Threading.SendOrPostCallback UploadFileFixName1OperationCompleted;

        private System.Threading.SendOrPostCallback UploadFileWithDirOperationCompleted;

        private System.Threading.SendOrPostCallback GenerateReportPdfOperationCompleted;

        private System.Threading.SendOrPostCallback GenerateReportWordRemoteOperationCompleted;

        private System.Threading.SendOrPostCallback UserHeaderInitOperationCompleted;

        private System.Threading.SendOrPostCallback ZoneHeaderInitOperationCompleted;

        private System.Threading.SendOrPostCallback CreateThumbImagesOperationCompleted;

        private System.Threading.SendOrPostCallback CreateThumbImages1OperationCompleted;

        private System.Threading.SendOrPostCallback CreateThumbImages4ScaleOperationCompleted;

        private System.Threading.SendOrPostCallback Select_ImageLogInfo_ByGuidOperationCompleted;

        private System.Threading.SendOrPostCallback Select_ImageLogInfo_ByPagedOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteImageByUrlOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteImageOperationCompleted;

        private System.Threading.SendOrPostCallback CheckImagePassOperationCompleted;

        private System.Threading.SendOrPostCallback RestoreImageOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public Upload()
        {
            this.Url = "http://localhost:8807/Upload.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;

        /// <remarks/>
        public event UploadImageGenerateQZReportCoverCompletedEventHandler UploadImageGenerateQZReportCoverCompleted;

        /// <remarks/>
        public event UploadImage4AlbumWithFixedSizeCompletedEventHandler UploadImage4AlbumWithFixedSizeCompleted;

        /// <remarks/>
        public event UploadImage4AlbumWithFixedSize1CompletedEventHandler UploadImage4AlbumWithFixedSize1Completed;

        /// <remarks/>
        public event UploadImageCompletedEventHandler UploadImageCompleted;

        /// <remarks/>
        public event UploadImageWithFixedThumbnailsCompletedEventHandler UploadImageWithFixedThumbnailsCompleted;

        /// <remarks/>
        public event UploadUserHeaderImageCompletedEventHandler UploadUserHeaderImageCompleted;

        /// <remarks/>
        public event UploadUserHeaderSourceImgCompletedEventHandler UploadUserHeaderSourceImgCompleted;

        /// <remarks/>
        public event UploadUserHeaderImage1CompletedEventHandler UploadUserHeaderImage1Completed;

        /// <remarks/>
        public event UploadPeopleHeaderImageCompletedEventHandler UploadPeopleHeaderImageCompleted;

        /// <remarks/>
        public event UploadCommonHeaderImageCompletedEventHandler UploadCommonHeaderImageCompleted;

        /// <remarks/>
        public event UploadReportHeaderImageCompletedEventHandler UploadReportHeaderImageCompleted;

        /// <remarks/>
        public event UploadAnalystHeaderImageCompletedEventHandler UploadAnalystHeaderImageCompleted;

        /// <remarks/>
        public event UploadReportCatalogHeaderImageCompletedEventHandler UploadReportCatalogHeaderImageCompleted;

        /// <remarks/>
        public event UploadFixedNameImageCompletedEventHandler UploadFixedNameImageCompleted;

        /// <remarks/>
        public event CreateRotateImageCompletedEventHandler CreateRotateImageCompleted;

        /// <remarks/>
        public event UploadImageWithFixedThumb4OutsidePicCompletedEventHandler UploadImageWithFixedThumb4OutsidePicCompleted;

        /// <remarks/>
        public event UploadFileCompletedEventHandler UploadFileCompleted;

        /// <remarks/>
        public event UploadFileFixNameCompletedEventHandler UploadFileFixNameCompleted;

        /// <remarks/>
        public event UploadFileFixName1CompletedEventHandler UploadFileFixName1Completed;

        /// <remarks/>
        public event UploadFileWithDirCompletedEventHandler UploadFileWithDirCompleted;

        /// <remarks/>
        public event GenerateReportPdfCompletedEventHandler GenerateReportPdfCompleted;

        /// <remarks/>
        public event GenerateReportWordRemoteCompletedEventHandler GenerateReportWordRemoteCompleted;

        /// <remarks/>
        public event UserHeaderInitCompletedEventHandler UserHeaderInitCompleted;

        /// <remarks/>
        public event ZoneHeaderInitCompletedEventHandler ZoneHeaderInitCompleted;

        /// <remarks/>
        public event CreateThumbImagesCompletedEventHandler CreateThumbImagesCompleted;

        /// <remarks/>
        public event CreateThumbImages1CompletedEventHandler CreateThumbImages1Completed;

        /// <remarks/>
        public event CreateThumbImages4ScaleCompletedEventHandler CreateThumbImages4ScaleCompleted;

        /// <remarks/>
        public event Select_ImageLogInfo_ByGuidCompletedEventHandler Select_ImageLogInfo_ByGuidCompleted;

        /// <remarks/>
        public event Select_ImageLogInfo_ByPagedCompletedEventHandler Select_ImageLogInfo_ByPagedCompleted;

        /// <remarks/>
        public event DeleteImageByUrlCompletedEventHandler DeleteImageByUrlCompleted;

        /// <remarks/>
        public event DeleteImageCompletedEventHandler DeleteImageCompleted;

        /// <remarks/>
        public event CheckImagePassCompletedEventHandler CheckImagePassCompleted;

        /// <remarks/>
        public event RestoreImageCompletedEventHandler RestoreImageCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/HelloWorld", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld()
        {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void HelloWorldAsync()
        {
            this.HelloWorldAsync(null);
        }

        /// <remarks/>
        public void HelloWorldAsync(object userState)
        {
            if ((this.HelloWorldOperationCompleted == null))
            {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }

        private void OnHelloWorldOperationCompleted(object arg)
        {
            if ((this.HelloWorldCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImageGenerateQZReportCover", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImageGenerateQZReportCover(string image_guid, string configName, string dirName, string originalFileName, string userName, string typeName, string[] thumbList, string title, string subTitle, string descript, bool save2Check)
        {
            object[] results = this.Invoke("UploadImageGenerateQZReportCover", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        userName,
                        typeName,
                        thumbList,
                        title,
                        subTitle,
                        descript,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImageGenerateQZReportCoverAsync(string image_guid, string configName, string dirName, string originalFileName, string userName, string typeName, string[] thumbList, string title, string subTitle, string descript, bool save2Check)
        {
            this.UploadImageGenerateQZReportCoverAsync(image_guid, configName, dirName, originalFileName, userName, typeName, thumbList, title, subTitle, descript, save2Check, null);
        }

        /// <remarks/>
        public void UploadImageGenerateQZReportCoverAsync(string image_guid, string configName, string dirName, string originalFileName, string userName, string typeName, string[] thumbList, string title, string subTitle, string descript, bool save2Check, object userState)
        {
            if ((this.UploadImageGenerateQZReportCoverOperationCompleted == null))
            {
                this.UploadImageGenerateQZReportCoverOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImageGenerateQZReportCoverOperationCompleted);
            }
            this.InvokeAsync("UploadImageGenerateQZReportCover", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        userName,
                        typeName,
                        thumbList,
                        title,
                        subTitle,
                        descript,
                        save2Check}, this.UploadImageGenerateQZReportCoverOperationCompleted, userState);
        }

        private void OnUploadImageGenerateQZReportCoverOperationCompleted(object arg)
        {
            if ((this.UploadImageGenerateQZReportCoverCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImageGenerateQZReportCoverCompleted(this, new UploadImageGenerateQZReportCoverCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImage4AlbumWithFixedSize", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImage4AlbumWithFixedSize(string image_guid, string configName, string originalFileName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadImage4AlbumWithFixedSize", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImage4AlbumWithFixedSizeAsync(string image_guid, string configName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            this.UploadImage4AlbumWithFixedSizeAsync(image_guid, configName, originalFileName, datas, fileExt, thumbList, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadImage4AlbumWithFixedSizeAsync(string image_guid, string configName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadImage4AlbumWithFixedSizeOperationCompleted == null))
            {
                this.UploadImage4AlbumWithFixedSizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImage4AlbumWithFixedSizeOperationCompleted);
            }
            this.InvokeAsync("UploadImage4AlbumWithFixedSize", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check}, this.UploadImage4AlbumWithFixedSizeOperationCompleted, userState);
        }

        private void OnUploadImage4AlbumWithFixedSizeOperationCompleted(object arg)
        {
            if ((this.UploadImage4AlbumWithFixedSizeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImage4AlbumWithFixedSizeCompleted(this, new UploadImage4AlbumWithFixedSizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImage4AlbumWithFixedSize1", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImage4AlbumWithFixedSize1(string image_guid, string configName, string dirName, string originalFileName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadImage4AlbumWithFixedSize1", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImage4AlbumWithFixedSize1Async(string image_guid, string configName, string dirName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            this.UploadImage4AlbumWithFixedSize1Async(image_guid, configName, dirName, originalFileName, datas, fileExt, thumbList, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadImage4AlbumWithFixedSize1Async(string image_guid, string configName, string dirName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadImage4AlbumWithFixedSize1OperationCompleted == null))
            {
                this.UploadImage4AlbumWithFixedSize1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImage4AlbumWithFixedSize1OperationCompleted);
            }
            this.InvokeAsync("UploadImage4AlbumWithFixedSize1", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check}, this.UploadImage4AlbumWithFixedSize1OperationCompleted, userState);
        }

        private void OnUploadImage4AlbumWithFixedSize1OperationCompleted(object arg)
        {
            if ((this.UploadImage4AlbumWithFixedSize1Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImage4AlbumWithFixedSize1Completed(this, new UploadImage4AlbumWithFixedSize1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImage(string configName, string relaPath, string fixFileName, string thumbFileName, string originalFileName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadImage", new object[] {
                        configName,
                        relaPath,
                        fixFileName,
                        thumbFileName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImageAsync(string configName, string relaPath, string fixFileName, string thumbFileName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            this.UploadImageAsync(configName, relaPath, fixFileName, thumbFileName, originalFileName, datas, fileExt, thumbList, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadImageAsync(string configName, string relaPath, string fixFileName, string thumbFileName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadImageOperationCompleted == null))
            {
                this.UploadImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImageOperationCompleted);
            }
            this.InvokeAsync("UploadImage", new object[] {
                        configName,
                        relaPath,
                        fixFileName,
                        thumbFileName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check}, this.UploadImageOperationCompleted, userState);
        }

        private void OnUploadImageOperationCompleted(object arg)
        {
            if ((this.UploadImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImageCompleted(this, new UploadImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImageWithFixedThumbnails", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImageWithFixedThumbnails(string image_guid, string configName, string originalFileName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadImageWithFixedThumbnails", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImageWithFixedThumbnailsAsync(string image_guid, string configName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check)
        {
            this.UploadImageWithFixedThumbnailsAsync(image_guid, configName, originalFileName, datas, fileExt, thumbList, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadImageWithFixedThumbnailsAsync(string image_guid, string configName, string originalFileName, byte[] datas, string fileExt, string[] thumbList, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadImageWithFixedThumbnailsOperationCompleted == null))
            {
                this.UploadImageWithFixedThumbnailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImageWithFixedThumbnailsOperationCompleted);
            }
            this.InvokeAsync("UploadImageWithFixedThumbnails", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        datas,
                        fileExt,
                        thumbList,
                        createUser,
                        save2Check}, this.UploadImageWithFixedThumbnailsOperationCompleted, userState);
        }

        private void OnUploadImageWithFixedThumbnailsOperationCompleted(object arg)
        {
            if ((this.UploadImageWithFixedThumbnailsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImageWithFixedThumbnailsCompleted(this, new UploadImageWithFixedThumbnailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadUserHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadUserHeaderImage(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadUserHeaderImage", new object[] {
                        image_guid,
                        configName,
                        userId,
                        types,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadUserHeaderImageAsync(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadUserHeaderImageAsync(image_guid, configName, userId, types, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadUserHeaderImageAsync(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadUserHeaderImageOperationCompleted == null))
            {
                this.UploadUserHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadUserHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadUserHeaderImage", new object[] {
                        image_guid,
                        configName,
                        userId,
                        types,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadUserHeaderImageOperationCompleted, userState);
        }

        private void OnUploadUserHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadUserHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadUserHeaderImageCompleted(this, new UploadUserHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadUserHeaderSourceImg", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadUserHeaderSourceImg(string image_guid, string configName, string userId, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadUserHeaderSourceImg", new object[] {
                        image_guid,
                        configName,
                        userId,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadUserHeaderSourceImgAsync(string image_guid, string configName, string userId, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadUserHeaderSourceImgAsync(image_guid, configName, userId, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadUserHeaderSourceImgAsync(string image_guid, string configName, string userId, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadUserHeaderSourceImgOperationCompleted == null))
            {
                this.UploadUserHeaderSourceImgOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadUserHeaderSourceImgOperationCompleted);
            }
            this.InvokeAsync("UploadUserHeaderSourceImg", new object[] {
                        image_guid,
                        configName,
                        userId,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadUserHeaderSourceImgOperationCompleted, userState);
        }

        private void OnUploadUserHeaderSourceImgOperationCompleted(object arg)
        {
            if ((this.UploadUserHeaderSourceImgCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadUserHeaderSourceImgCompleted(this, new UploadUserHeaderSourceImgCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadUserHeaderImage1", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadUserHeaderImage1(string image_guid, string configName, string userId, string typeName, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadUserHeaderImage1", new object[] {
                        image_guid,
                        configName,
                        userId,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadUserHeaderImage1Async(string image_guid, string configName, string userId, string typeName, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadUserHeaderImage1Async(image_guid, configName, userId, typeName, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadUserHeaderImage1Async(string image_guid, string configName, string userId, string typeName, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadUserHeaderImage1OperationCompleted == null))
            {
                this.UploadUserHeaderImage1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadUserHeaderImage1OperationCompleted);
            }
            this.InvokeAsync("UploadUserHeaderImage1", new object[] {
                        image_guid,
                        configName,
                        userId,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadUserHeaderImage1OperationCompleted, userState);
        }

        private void OnUploadUserHeaderImage1OperationCompleted(object arg)
        {
            if ((this.UploadUserHeaderImage1Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadUserHeaderImage1Completed(this, new UploadUserHeaderImage1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadPeopleHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadPeopleHeaderImage(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadPeopleHeaderImage", new object[] {
                        image_guid,
                        configName,
                        userId,
                        types,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadPeopleHeaderImageAsync(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadPeopleHeaderImageAsync(image_guid, configName, userId, types, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadPeopleHeaderImageAsync(string image_guid, string configName, string userId, string[] types, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadPeopleHeaderImageOperationCompleted == null))
            {
                this.UploadPeopleHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadPeopleHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadPeopleHeaderImage", new object[] {
                        image_guid,
                        configName,
                        userId,
                        types,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadPeopleHeaderImageOperationCompleted, userState);
        }

        private void OnUploadPeopleHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadPeopleHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadPeopleHeaderImageCompleted(this, new UploadPeopleHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadCommonHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadCommonHeaderImage(string image_guid, string configName, string starId, string[] dirs, string[] types, string[] sizes, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadCommonHeaderImage", new object[] {
                        image_guid,
                        configName,
                        starId,
                        dirs,
                        types,
                        sizes,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadCommonHeaderImageAsync(string image_guid, string configName, string starId, string[] dirs, string[] types, string[] sizes, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadCommonHeaderImageAsync(image_guid, configName, starId, dirs, types, sizes, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadCommonHeaderImageAsync(string image_guid, string configName, string starId, string[] dirs, string[] types, string[] sizes, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadCommonHeaderImageOperationCompleted == null))
            {
                this.UploadCommonHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadCommonHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadCommonHeaderImage", new object[] {
                        image_guid,
                        configName,
                        starId,
                        dirs,
                        types,
                        sizes,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadCommonHeaderImageOperationCompleted, userState);
        }

        private void OnUploadCommonHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadCommonHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadCommonHeaderImageCompleted(this, new UploadCommonHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadReportHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadReportHeaderImage(string image_guid, string configName, int zoneId, string userName, string typeName, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadReportHeaderImage", new object[] {
                        image_guid,
                        configName,
                        zoneId,
                        userName,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadReportHeaderImageAsync(string image_guid, string configName, int zoneId, string userName, string typeName, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadReportHeaderImageAsync(image_guid, configName, zoneId, userName, typeName, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadReportHeaderImageAsync(string image_guid, string configName, int zoneId, string userName, string typeName, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadReportHeaderImageOperationCompleted == null))
            {
                this.UploadReportHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadReportHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadReportHeaderImage", new object[] {
                        image_guid,
                        configName,
                        zoneId,
                        userName,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadReportHeaderImageOperationCompleted, userState);
        }

        private void OnUploadReportHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadReportHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadReportHeaderImageCompleted(this, new UploadReportHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadAnalystHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadAnalystHeaderImage(string configName, string dirName, string userName, string[] thumbList, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadAnalystHeaderImage", new object[] {
                        configName,
                        dirName,
                        userName,
                        thumbList,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadAnalystHeaderImageAsync(string configName, string dirName, string userName, string[] thumbList, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadAnalystHeaderImageAsync(configName, dirName, userName, thumbList, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadAnalystHeaderImageAsync(string configName, string dirName, string userName, string[] thumbList, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadAnalystHeaderImageOperationCompleted == null))
            {
                this.UploadAnalystHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadAnalystHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadAnalystHeaderImage", new object[] {
                        configName,
                        dirName,
                        userName,
                        thumbList,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadAnalystHeaderImageOperationCompleted, userState);
        }

        private void OnUploadAnalystHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadAnalystHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadAnalystHeaderImageCompleted(this, new UploadAnalystHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadReportCatalogHeaderImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadReportCatalogHeaderImage(string image_guid, string configName, int reportId, int catalogId, string userName, string typeName, string imgSourceUrl, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, bool save2Check)
        {
            object[] results = this.Invoke("UploadReportCatalogHeaderImage", new object[] {
                        image_guid,
                        configName,
                        reportId,
                        catalogId,
                        userName,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadReportCatalogHeaderImageAsync(string image_guid, string configName, int reportId, int catalogId, string userName, string typeName, string imgSourceUrl, byte[] datas, bool save2Check)
        {
            this.UploadReportCatalogHeaderImageAsync(image_guid, configName, reportId, catalogId, userName, typeName, imgSourceUrl, datas, save2Check, null);
        }

        /// <remarks/>
        public void UploadReportCatalogHeaderImageAsync(string image_guid, string configName, int reportId, int catalogId, string userName, string typeName, string imgSourceUrl, byte[] datas, bool save2Check, object userState)
        {
            if ((this.UploadReportCatalogHeaderImageOperationCompleted == null))
            {
                this.UploadReportCatalogHeaderImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadReportCatalogHeaderImageOperationCompleted);
            }
            this.InvokeAsync("UploadReportCatalogHeaderImage", new object[] {
                        image_guid,
                        configName,
                        reportId,
                        catalogId,
                        userName,
                        typeName,
                        imgSourceUrl,
                        datas,
                        save2Check}, this.UploadReportCatalogHeaderImageOperationCompleted, userState);
        }

        private void OnUploadReportCatalogHeaderImageOperationCompleted(object arg)
        {
            if ((this.UploadReportCatalogHeaderImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadReportCatalogHeaderImageCompleted(this, new UploadReportCatalogHeaderImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadFixedNameImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadFixedNameImage(string image_guid, string configName, string objectId, string fileExt, string typeName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, int imgWidth, int imgHeight)
        {
            object[] results = this.Invoke("UploadFixedNameImage", new object[] {
                        image_guid,
                        configName,
                        objectId,
                        fileExt,
                        typeName,
                        datas,
                        imgWidth,
                        imgHeight});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadFixedNameImageAsync(string image_guid, string configName, string objectId, string fileExt, string typeName, byte[] datas, int imgWidth, int imgHeight)
        {
            this.UploadFixedNameImageAsync(image_guid, configName, objectId, fileExt, typeName, datas, imgWidth, imgHeight, null);
        }

        /// <remarks/>
        public void UploadFixedNameImageAsync(string image_guid, string configName, string objectId, string fileExt, string typeName, byte[] datas, int imgWidth, int imgHeight, object userState)
        {
            if ((this.UploadFixedNameImageOperationCompleted == null))
            {
                this.UploadFixedNameImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFixedNameImageOperationCompleted);
            }
            this.InvokeAsync("UploadFixedNameImage", new object[] {
                        image_guid,
                        configName,
                        objectId,
                        fileExt,
                        typeName,
                        datas,
                        imgWidth,
                        imgHeight}, this.UploadFixedNameImageOperationCompleted, userState);
        }

        private void OnUploadFixedNameImageOperationCompleted(object arg)
        {
            if ((this.UploadFixedNameImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFixedNameImageCompleted(this, new UploadFixedNameImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/CreateRotateImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CreateRotateImage(string relaFilePath)
        {
            object[] results = this.Invoke("CreateRotateImage", new object[] {
                        relaFilePath});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void CreateRotateImageAsync(string relaFilePath)
        {
            this.CreateRotateImageAsync(relaFilePath, null);
        }

        /// <remarks/>
        public void CreateRotateImageAsync(string relaFilePath, object userState)
        {
            if ((this.CreateRotateImageOperationCompleted == null))
            {
                this.CreateRotateImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateRotateImageOperationCompleted);
            }
            this.InvokeAsync("CreateRotateImage", new object[] {
                        relaFilePath}, this.CreateRotateImageOperationCompleted, userState);
        }

        private void OnCreateRotateImageOperationCompleted(object arg)
        {
            if ((this.CreateRotateImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateRotateImageCompleted(this, new CreateRotateImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadImageWithFixedThumb4OutsidePic", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] UploadImageWithFixedThumb4OutsidePic(string image_guid, string configName, string url, string umd, string fileExt, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, int imgWidth, int imgHeight, bool withThumb, bool save2Check)
        {
            object[] results = this.Invoke("UploadImageWithFixedThumb4OutsidePic", new object[] {
                        image_guid,
                        configName,
                        url,
                        umd,
                        fileExt,
                        datas,
                        imgWidth,
                        imgHeight,
                        withThumb,
                        save2Check});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void UploadImageWithFixedThumb4OutsidePicAsync(string image_guid, string configName, string url, string umd, string fileExt, byte[] datas, int imgWidth, int imgHeight, bool withThumb, bool save2Check)
        {
            this.UploadImageWithFixedThumb4OutsidePicAsync(image_guid, configName, url, umd, fileExt, datas, imgWidth, imgHeight, withThumb, save2Check, null);
        }

        /// <remarks/>
        public void UploadImageWithFixedThumb4OutsidePicAsync(string image_guid, string configName, string url, string umd, string fileExt, byte[] datas, int imgWidth, int imgHeight, bool withThumb, bool save2Check, object userState)
        {
            if ((this.UploadImageWithFixedThumb4OutsidePicOperationCompleted == null))
            {
                this.UploadImageWithFixedThumb4OutsidePicOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadImageWithFixedThumb4OutsidePicOperationCompleted);
            }
            this.InvokeAsync("UploadImageWithFixedThumb4OutsidePic", new object[] {
                        image_guid,
                        configName,
                        url,
                        umd,
                        fileExt,
                        datas,
                        imgWidth,
                        imgHeight,
                        withThumb,
                        save2Check}, this.UploadImageWithFixedThumb4OutsidePicOperationCompleted, userState);
        }

        private void OnUploadImageWithFixedThumb4OutsidePicOperationCompleted(object arg)
        {
            if ((this.UploadImageWithFixedThumb4OutsidePicCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadImageWithFixedThumb4OutsidePicCompleted(this, new UploadImageWithFixedThumb4OutsidePicCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadFile", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadFile(string image_guid, string configName, string originalFileName, string strExtName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadFile", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadFileAsync(string image_guid, string configName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check)
        {
            this.UploadFileAsync(image_guid, configName, originalFileName, strExtName, datas, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadFileAsync(string image_guid, string configName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadFileOperationCompleted == null))
            {
                this.UploadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileOperationCompleted);
            }
            this.InvokeAsync("UploadFile", new object[] {
                        image_guid,
                        configName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check}, this.UploadFileOperationCompleted, userState);
        }

        private void OnUploadFileOperationCompleted(object arg)
        {
            if ((this.UploadFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileCompleted(this, new UploadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadFileFixName", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadFileFixName(string configName, string dirName, string fileName, string originalFileName, string strExtName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadFileFixName", new object[] {
                        configName,
                        dirName,
                        fileName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadFileFixNameAsync(string configName, string dirName, string fileName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check)
        {
            this.UploadFileFixNameAsync(configName, dirName, fileName, originalFileName, strExtName, datas, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadFileFixNameAsync(string configName, string dirName, string fileName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadFileFixNameOperationCompleted == null))
            {
                this.UploadFileFixNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileFixNameOperationCompleted);
            }
            this.InvokeAsync("UploadFileFixName", new object[] {
                        configName,
                        dirName,
                        fileName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check}, this.UploadFileFixNameOperationCompleted, userState);
        }

        private void OnUploadFileFixNameOperationCompleted(object arg)
        {
            if ((this.UploadFileFixNameCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileFixNameCompleted(this, new UploadFileFixNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadFileFixName1", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadFileFixName1(string configName, string[] dirNames, string fileName, string originalFileName, string strExtName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadFileFixName1", new object[] {
                        configName,
                        dirNames,
                        fileName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadFileFixName1Async(string configName, string[] dirNames, string fileName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check)
        {
            this.UploadFileFixName1Async(configName, dirNames, fileName, originalFileName, strExtName, datas, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadFileFixName1Async(string configName, string[] dirNames, string fileName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadFileFixName1OperationCompleted == null))
            {
                this.UploadFileFixName1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileFixName1OperationCompleted);
            }
            this.InvokeAsync("UploadFileFixName1", new object[] {
                        configName,
                        dirNames,
                        fileName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check}, this.UploadFileFixName1OperationCompleted, userState);
        }

        private void OnUploadFileFixName1OperationCompleted(object arg)
        {
            if ((this.UploadFileFixName1Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileFixName1Completed(this, new UploadFileFixName1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UploadFileWithDir", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo UploadFileWithDir(string image_guid, string configName, string dirName, string originalFileName, string strExtName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] datas, string createUser, bool save2Check)
        {
            object[] results = this.Invoke("UploadFileWithDir", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check});
            return ((UploadedImageInfo)(results[0]));
        }

        /// <remarks/>
        public void UploadFileWithDirAsync(string image_guid, string configName, string dirName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check)
        {
            this.UploadFileWithDirAsync(image_guid, configName, dirName, originalFileName, strExtName, datas, createUser, save2Check, null);
        }

        /// <remarks/>
        public void UploadFileWithDirAsync(string image_guid, string configName, string dirName, string originalFileName, string strExtName, byte[] datas, string createUser, bool save2Check, object userState)
        {
            if ((this.UploadFileWithDirOperationCompleted == null))
            {
                this.UploadFileWithDirOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileWithDirOperationCompleted);
            }
            this.InvokeAsync("UploadFileWithDir", new object[] {
                        image_guid,
                        configName,
                        dirName,
                        originalFileName,
                        strExtName,
                        datas,
                        createUser,
                        save2Check}, this.UploadFileWithDirOperationCompleted, userState);
        }

        private void OnUploadFileWithDirOperationCompleted(object arg)
        {
            if ((this.UploadFileWithDirCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileWithDirCompleted(this, new UploadFileWithDirCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/GenerateReportPdf", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] GenerateReportPdf([System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] parseBytes, string r_gid, string r_title, string r_short, string r_publishDate)
        {
            object[] results = this.Invoke("GenerateReportPdf", new object[] {
                        parseBytes,
                        r_gid,
                        r_title,
                        r_short,
                        r_publishDate});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void GenerateReportPdfAsync(byte[] parseBytes, string r_gid, string r_title, string r_short, string r_publishDate)
        {
            this.GenerateReportPdfAsync(parseBytes, r_gid, r_title, r_short, r_publishDate, null);
        }

        /// <remarks/>
        public void GenerateReportPdfAsync(byte[] parseBytes, string r_gid, string r_title, string r_short, string r_publishDate, object userState)
        {
            if ((this.GenerateReportPdfOperationCompleted == null))
            {
                this.GenerateReportPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateReportPdfOperationCompleted);
            }
            this.InvokeAsync("GenerateReportPdf", new object[] {
                        parseBytes,
                        r_gid,
                        r_title,
                        r_short,
                        r_publishDate}, this.GenerateReportPdfOperationCompleted, userState);
        }

        private void OnGenerateReportPdfOperationCompleted(object arg)
        {
            if ((this.GenerateReportPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateReportPdfCompleted(this, new GenerateReportPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/GenerateReportWordRemote", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UploadedImageInfo[] GenerateReportWordRemote([System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] parseBytes, string r_lang, string r_content, string r_gid, string r_title, string r_short, string r_publishDate)
        {
            object[] results = this.Invoke("GenerateReportWordRemote", new object[] {
                        parseBytes,
                        r_lang,
                        r_content,
                        r_gid,
                        r_title,
                        r_short,
                        r_publishDate});
            return ((UploadedImageInfo[])(results[0]));
        }

        /// <remarks/>
        public void GenerateReportWordRemoteAsync(byte[] parseBytes, string r_lang, string r_content, string r_gid, string r_title, string r_short, string r_publishDate)
        {
            this.GenerateReportWordRemoteAsync(parseBytes, r_lang, r_content, r_gid, r_title, r_short, r_publishDate, null);
        }

        /// <remarks/>
        public void GenerateReportWordRemoteAsync(byte[] parseBytes, string r_lang, string r_content, string r_gid, string r_title, string r_short, string r_publishDate, object userState)
        {
            if ((this.GenerateReportWordRemoteOperationCompleted == null))
            {
                this.GenerateReportWordRemoteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerateReportWordRemoteOperationCompleted);
            }
            this.InvokeAsync("GenerateReportWordRemote", new object[] {
                        parseBytes,
                        r_lang,
                        r_content,
                        r_gid,
                        r_title,
                        r_short,
                        r_publishDate}, this.GenerateReportWordRemoteOperationCompleted, userState);
        }

        private void OnGenerateReportWordRemoteOperationCompleted(object arg)
        {
            if ((this.GenerateReportWordRemoteCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerateReportWordRemoteCompleted(this, new GenerateReportWordRemoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/UserHeaderInit", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UserHeaderInit(string userName, string sex)
        {
            object[] results = this.Invoke("UserHeaderInit", new object[] {
                        userName,
                        sex});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void UserHeaderInitAsync(string userName, string sex)
        {
            this.UserHeaderInitAsync(userName, sex, null);
        }

        /// <remarks/>
        public void UserHeaderInitAsync(string userName, string sex, object userState)
        {
            if ((this.UserHeaderInitOperationCompleted == null))
            {
                this.UserHeaderInitOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserHeaderInitOperationCompleted);
            }
            this.InvokeAsync("UserHeaderInit", new object[] {
                        userName,
                        sex}, this.UserHeaderInitOperationCompleted, userState);
        }

        private void OnUserHeaderInitOperationCompleted(object arg)
        {
            if ((this.UserHeaderInitCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserHeaderInitCompleted(this, new UserHeaderInitCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/ZoneHeaderInit", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool ZoneHeaderInit(int zoneId)
        {
            object[] results = this.Invoke("ZoneHeaderInit", new object[] {
                        zoneId});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void ZoneHeaderInitAsync(int zoneId)
        {
            this.ZoneHeaderInitAsync(zoneId, null);
        }

        /// <remarks/>
        public void ZoneHeaderInitAsync(int zoneId, object userState)
        {
            if ((this.ZoneHeaderInitOperationCompleted == null))
            {
                this.ZoneHeaderInitOperationCompleted = new System.Threading.SendOrPostCallback(this.OnZoneHeaderInitOperationCompleted);
            }
            this.InvokeAsync("ZoneHeaderInit", new object[] {
                        zoneId}, this.ZoneHeaderInitOperationCompleted, userState);
        }

        private void OnZoneHeaderInitOperationCompleted(object arg)
        {
            if ((this.ZoneHeaderInitCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ZoneHeaderInitCompleted(this, new ZoneHeaderInitCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/CreateThumbImages", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] CreateThumbImages(string relaFilePath, string[] thumbList)
        {
            object[] results = this.Invoke("CreateThumbImages", new object[] {
                        relaFilePath,
                        thumbList});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void CreateThumbImagesAsync(string relaFilePath, string[] thumbList)
        {
            this.CreateThumbImagesAsync(relaFilePath, thumbList, null);
        }

        /// <remarks/>
        public void CreateThumbImagesAsync(string relaFilePath, string[] thumbList, object userState)
        {
            if ((this.CreateThumbImagesOperationCompleted == null))
            {
                this.CreateThumbImagesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateThumbImagesOperationCompleted);
            }
            this.InvokeAsync("CreateThumbImages", new object[] {
                        relaFilePath,
                        thumbList}, this.CreateThumbImagesOperationCompleted, userState);
        }

        private void OnCreateThumbImagesOperationCompleted(object arg)
        {
            if ((this.CreateThumbImagesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateThumbImagesCompleted(this, new CreateThumbImagesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/CreateThumbImages1", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] CreateThumbImages1(string configName, string relaFilePath, string[] thumbList)
        {
            object[] results = this.Invoke("CreateThumbImages1", new object[] {
                        configName,
                        relaFilePath,
                        thumbList});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void CreateThumbImages1Async(string configName, string relaFilePath, string[] thumbList)
        {
            this.CreateThumbImages1Async(configName, relaFilePath, thumbList, null);
        }

        /// <remarks/>
        public void CreateThumbImages1Async(string configName, string relaFilePath, string[] thumbList, object userState)
        {
            if ((this.CreateThumbImages1OperationCompleted == null))
            {
                this.CreateThumbImages1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateThumbImages1OperationCompleted);
            }
            this.InvokeAsync("CreateThumbImages1", new object[] {
                        configName,
                        relaFilePath,
                        thumbList}, this.CreateThumbImages1OperationCompleted, userState);
        }

        private void OnCreateThumbImages1OperationCompleted(object arg)
        {
            if ((this.CreateThumbImages1Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateThumbImages1Completed(this, new CreateThumbImages1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/CreateThumbImages4Scale", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] CreateThumbImages4Scale(string relaFilePath, string[] thumbList, string configName, decimal whVal, decimal whOffset)
        {
            object[] results = this.Invoke("CreateThumbImages4Scale", new object[] {
                        relaFilePath,
                        thumbList,
                        configName,
                        whVal,
                        whOffset});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void CreateThumbImages4ScaleAsync(string relaFilePath, string[] thumbList, string configName, decimal whVal, decimal whOffset)
        {
            this.CreateThumbImages4ScaleAsync(relaFilePath, thumbList, configName, whVal, whOffset, null);
        }

        /// <remarks/>
        public void CreateThumbImages4ScaleAsync(string relaFilePath, string[] thumbList, string configName, decimal whVal, decimal whOffset, object userState)
        {
            if ((this.CreateThumbImages4ScaleOperationCompleted == null))
            {
                this.CreateThumbImages4ScaleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateThumbImages4ScaleOperationCompleted);
            }
            this.InvokeAsync("CreateThumbImages4Scale", new object[] {
                        relaFilePath,
                        thumbList,
                        configName,
                        whVal,
                        whOffset}, this.CreateThumbImages4ScaleOperationCompleted, userState);
        }

        private void OnCreateThumbImages4ScaleOperationCompleted(object arg)
        {
            if ((this.CreateThumbImages4ScaleCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateThumbImages4ScaleCompleted(this, new CreateThumbImages4ScaleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/Select_ImageLogInfo_ByGuid", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ImageLogInfo Select_ImageLogInfo_ByGuid(string img_guid)
        {
            object[] results = this.Invoke("Select_ImageLogInfo_ByGuid", new object[] {
                        img_guid});
            return ((ImageLogInfo)(results[0]));
        }

        /// <remarks/>
        public void Select_ImageLogInfo_ByGuidAsync(string img_guid)
        {
            this.Select_ImageLogInfo_ByGuidAsync(img_guid, null);
        }

        /// <remarks/>
        public void Select_ImageLogInfo_ByGuidAsync(string img_guid, object userState)
        {
            if ((this.Select_ImageLogInfo_ByGuidOperationCompleted == null))
            {
                this.Select_ImageLogInfo_ByGuidOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelect_ImageLogInfo_ByGuidOperationCompleted);
            }
            this.InvokeAsync("Select_ImageLogInfo_ByGuid", new object[] {
                        img_guid}, this.Select_ImageLogInfo_ByGuidOperationCompleted, userState);
        }

        private void OnSelect_ImageLogInfo_ByGuidOperationCompleted(object arg)
        {
            if ((this.Select_ImageLogInfo_ByGuidCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Select_ImageLogInfo_ByGuidCompleted(this, new Select_ImageLogInfo_ByGuidCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/Select_ImageLogInfo_ByPaged", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ImageLogInfo[] Select_ImageLogInfo_ByPaged(System.DateTime logDate, string where, string order, int pageIndex, int pageSize, bool bCount, out int iRows)
        {
            object[] results = this.Invoke("Select_ImageLogInfo_ByPaged", new object[] {
                        logDate,
                        where,
                        order,
                        pageIndex,
                        pageSize,
                        bCount});
            iRows = ((int)(results[1]));
            return ((ImageLogInfo[])(results[0]));
        }

        /// <remarks/>
        public void Select_ImageLogInfo_ByPagedAsync(System.DateTime logDate, string where, string order, int pageIndex, int pageSize, bool bCount)
        {
            this.Select_ImageLogInfo_ByPagedAsync(logDate, where, order, pageIndex, pageSize, bCount, null);
        }

        /// <remarks/>
        public void Select_ImageLogInfo_ByPagedAsync(System.DateTime logDate, string where, string order, int pageIndex, int pageSize, bool bCount, object userState)
        {
            if ((this.Select_ImageLogInfo_ByPagedOperationCompleted == null))
            {
                this.Select_ImageLogInfo_ByPagedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelect_ImageLogInfo_ByPagedOperationCompleted);
            }
            this.InvokeAsync("Select_ImageLogInfo_ByPaged", new object[] {
                        logDate,
                        where,
                        order,
                        pageIndex,
                        pageSize,
                        bCount}, this.Select_ImageLogInfo_ByPagedOperationCompleted, userState);
        }

        private void OnSelect_ImageLogInfo_ByPagedOperationCompleted(object arg)
        {
            if ((this.Select_ImageLogInfo_ByPagedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Select_ImageLogInfo_ByPagedCompleted(this, new Select_ImageLogInfo_ByPagedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/DeleteImageByUrl", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteImageByUrl(string image_url)
        {
            object[] results = this.Invoke("DeleteImageByUrl", new object[] {
                        image_url});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void DeleteImageByUrlAsync(string image_url)
        {
            this.DeleteImageByUrlAsync(image_url, null);
        }

        /// <remarks/>
        public void DeleteImageByUrlAsync(string image_url, object userState)
        {
            if ((this.DeleteImageByUrlOperationCompleted == null))
            {
                this.DeleteImageByUrlOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteImageByUrlOperationCompleted);
            }
            this.InvokeAsync("DeleteImageByUrl", new object[] {
                        image_url}, this.DeleteImageByUrlOperationCompleted, userState);
        }

        private void OnDeleteImageByUrlOperationCompleted(object arg)
        {
            if ((this.DeleteImageByUrlCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteImageByUrlCompleted(this, new DeleteImageByUrlCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/DeleteImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int DeleteImage(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode)
        {
            object[] results = this.Invoke("DeleteImage", new object[] {
                        img_guid,
                        img_absoluePath,
                        img_fileNames,
                        checkUser,
                        checkRemark,
                        validCode});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void DeleteImageAsync(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode)
        {
            this.DeleteImageAsync(img_guid, img_absoluePath, img_fileNames, checkUser, checkRemark, validCode, null);
        }

        /// <remarks/>
        public void DeleteImageAsync(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode, object userState)
        {
            if ((this.DeleteImageOperationCompleted == null))
            {
                this.DeleteImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteImageOperationCompleted);
            }
            this.InvokeAsync("DeleteImage", new object[] {
                        img_guid,
                        img_absoluePath,
                        img_fileNames,
                        checkUser,
                        checkRemark,
                        validCode}, this.DeleteImageOperationCompleted, userState);
        }

        private void OnDeleteImageOperationCompleted(object arg)
        {
            if ((this.DeleteImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteImageCompleted(this, new DeleteImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/CheckImagePass", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int CheckImagePass(string[] img_guids, string checkUser, string checkRemark)
        {
            object[] results = this.Invoke("CheckImagePass", new object[] {
                        img_guids,
                        checkUser,
                        checkRemark});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void CheckImagePassAsync(string[] img_guids, string checkUser, string checkRemark)
        {
            this.CheckImagePassAsync(img_guids, checkUser, checkRemark, null);
        }

        /// <remarks/>
        public void CheckImagePassAsync(string[] img_guids, string checkUser, string checkRemark, object userState)
        {
            if ((this.CheckImagePassOperationCompleted == null))
            {
                this.CheckImagePassOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckImagePassOperationCompleted);
            }
            this.InvokeAsync("CheckImagePass", new object[] {
                        img_guids,
                        checkUser,
                        checkRemark}, this.CheckImagePassOperationCompleted, userState);
        }

        private void OnCheckImagePassOperationCompleted(object arg)
        {
            if ((this.CheckImagePassCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckImagePassCompleted(this, new CheckImagePassCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://service.qianzhan.com/RestoreImage", RequestNamespace = "http://service.qianzhan.com/", ResponseNamespace = "http://service.qianzhan.com/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int RestoreImage(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode)
        {
            object[] results = this.Invoke("RestoreImage", new object[] {
                        img_guid,
                        img_absoluePath,
                        img_fileNames,
                        checkUser,
                        checkRemark,
                        validCode});
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void RestoreImageAsync(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode)
        {
            this.RestoreImageAsync(img_guid, img_absoluePath, img_fileNames, checkUser, checkRemark, validCode, null);
        }

        /// <remarks/>
        public void RestoreImageAsync(string img_guid, string img_absoluePath, string img_fileNames, string checkUser, string checkRemark, int validCode, object userState)
        {
            if ((this.RestoreImageOperationCompleted == null))
            {
                this.RestoreImageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRestoreImageOperationCompleted);
            }
            this.InvokeAsync("RestoreImage", new object[] {
                        img_guid,
                        img_absoluePath,
                        img_fileNames,
                        checkUser,
                        checkRemark,
                        validCode}, this.RestoreImageOperationCompleted, userState);
        }

        private void OnRestoreImageOperationCompleted(object arg)
        {
            if ((this.RestoreImageCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RestoreImageCompleted(this, new RestoreImageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://service.qianzhan.com/")]
    public partial class UploadedImageInfo
    {

        private string fileNameField;

        private string absoluteUrlField;

        private string fullUrlField;

        private string errorField;

        private int widthField;

        private int heightField;

        private bool isSourceField;

        private int lengthField;

        /// <remarks/>
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        public string AbsoluteUrl
        {
            get
            {
                return this.absoluteUrlField;
            }
            set
            {
                this.absoluteUrlField = value;
            }
        }

        /// <remarks/>
        public string FullUrl
        {
            get
            {
                return this.fullUrlField;
            }
            set
            {
                this.fullUrlField = value;
            }
        }

        /// <remarks/>
        public string Error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        public int Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public int Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        public bool IsSource
        {
            get
            {
                return this.isSourceField;
            }
            set
            {
                this.isSourceField = value;
            }
        }

        /// <remarks/>
        public int Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1067.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://service.qianzhan.com/")]
    public partial class ImageLogInfo
    {

        private int img_idField;

        private string img_typeField;

        private string img_guidField;

        private string img_nameField;

        private int img_lengthField;

        private string img_thumbnailField;

        private string img_sourceUrlField;

        private string img_absoluteDirField;

        private string img_fileNamesField;

        private string img_serviceUrlField;

        private string img_createUserField;

        private System.DateTime img_createDateField;

        private int img_statusField;

        private string img_checkUserField;

        private System.DateTime img_checkDateField;

        private string img_checkRemarkField;

        /// <remarks/>
        public int img_id
        {
            get
            {
                return this.img_idField;
            }
            set
            {
                this.img_idField = value;
            }
        }

        /// <remarks/>
        public string img_type
        {
            get
            {
                return this.img_typeField;
            }
            set
            {
                this.img_typeField = value;
            }
        }

        /// <remarks/>
        public string img_guid
        {
            get
            {
                return this.img_guidField;
            }
            set
            {
                this.img_guidField = value;
            }
        }

        /// <remarks/>
        public string img_name
        {
            get
            {
                return this.img_nameField;
            }
            set
            {
                this.img_nameField = value;
            }
        }

        /// <remarks/>
        public int img_length
        {
            get
            {
                return this.img_lengthField;
            }
            set
            {
                this.img_lengthField = value;
            }
        }

        /// <remarks/>
        public string img_thumbnail
        {
            get
            {
                return this.img_thumbnailField;
            }
            set
            {
                this.img_thumbnailField = value;
            }
        }

        /// <remarks/>
        public string img_sourceUrl
        {
            get
            {
                return this.img_sourceUrlField;
            }
            set
            {
                this.img_sourceUrlField = value;
            }
        }

        /// <remarks/>
        public string img_absoluteDir
        {
            get
            {
                return this.img_absoluteDirField;
            }
            set
            {
                this.img_absoluteDirField = value;
            }
        }

        /// <remarks/>
        public string img_fileNames
        {
            get
            {
                return this.img_fileNamesField;
            }
            set
            {
                this.img_fileNamesField = value;
            }
        }

        /// <remarks/>
        public string img_serviceUrl
        {
            get
            {
                return this.img_serviceUrlField;
            }
            set
            {
                this.img_serviceUrlField = value;
            }
        }

        /// <remarks/>
        public string img_createUser
        {
            get
            {
                return this.img_createUserField;
            }
            set
            {
                this.img_createUserField = value;
            }
        }

        /// <remarks/>
        public System.DateTime img_createDate
        {
            get
            {
                return this.img_createDateField;
            }
            set
            {
                this.img_createDateField = value;
            }
        }

        /// <remarks/>
        public int img_status
        {
            get
            {
                return this.img_statusField;
            }
            set
            {
                this.img_statusField = value;
            }
        }

        /// <remarks/>
        public string img_checkUser
        {
            get
            {
                return this.img_checkUserField;
            }
            set
            {
                this.img_checkUserField = value;
            }
        }

        /// <remarks/>
        public System.DateTime img_checkDate
        {
            get
            {
                return this.img_checkDateField;
            }
            set
            {
                this.img_checkDateField = value;
            }
        }

        /// <remarks/>
        public string img_checkRemark
        {
            get
            {
                return this.img_checkRemarkField;
            }
            set
            {
                this.img_checkRemarkField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImageGenerateQZReportCoverCompletedEventHandler(object sender, UploadImageGenerateQZReportCoverCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImageGenerateQZReportCoverCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImageGenerateQZReportCoverCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImage4AlbumWithFixedSizeCompletedEventHandler(object sender, UploadImage4AlbumWithFixedSizeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImage4AlbumWithFixedSizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImage4AlbumWithFixedSizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImage4AlbumWithFixedSize1CompletedEventHandler(object sender, UploadImage4AlbumWithFixedSize1CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImage4AlbumWithFixedSize1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImage4AlbumWithFixedSize1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImageCompletedEventHandler(object sender, UploadImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImageWithFixedThumbnailsCompletedEventHandler(object sender, UploadImageWithFixedThumbnailsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImageWithFixedThumbnailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImageWithFixedThumbnailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadUserHeaderImageCompletedEventHandler(object sender, UploadUserHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadUserHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadUserHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadUserHeaderSourceImgCompletedEventHandler(object sender, UploadUserHeaderSourceImgCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadUserHeaderSourceImgCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadUserHeaderSourceImgCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadUserHeaderImage1CompletedEventHandler(object sender, UploadUserHeaderImage1CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadUserHeaderImage1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadUserHeaderImage1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadPeopleHeaderImageCompletedEventHandler(object sender, UploadPeopleHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadPeopleHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadPeopleHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadCommonHeaderImageCompletedEventHandler(object sender, UploadCommonHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadCommonHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadCommonHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadReportHeaderImageCompletedEventHandler(object sender, UploadReportHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadReportHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadReportHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadAnalystHeaderImageCompletedEventHandler(object sender, UploadAnalystHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadAnalystHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadAnalystHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadReportCatalogHeaderImageCompletedEventHandler(object sender, UploadReportCatalogHeaderImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadReportCatalogHeaderImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadReportCatalogHeaderImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadFixedNameImageCompletedEventHandler(object sender, UploadFixedNameImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFixedNameImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFixedNameImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CreateRotateImageCompletedEventHandler(object sender, CreateRotateImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateRotateImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateRotateImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadImageWithFixedThumb4OutsidePicCompletedEventHandler(object sender, UploadImageWithFixedThumb4OutsidePicCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadImageWithFixedThumb4OutsidePicCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadImageWithFixedThumb4OutsidePicCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadFileCompletedEventHandler(object sender, UploadFileCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadFileFixNameCompletedEventHandler(object sender, UploadFileFixNameCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileFixNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFileFixNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadFileFixName1CompletedEventHandler(object sender, UploadFileFixName1CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileFixName1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFileFixName1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UploadFileWithDirCompletedEventHandler(object sender, UploadFileWithDirCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileWithDirCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UploadFileWithDirCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GenerateReportPdfCompletedEventHandler(object sender, GenerateReportPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerateReportPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GenerateReportPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void GenerateReportWordRemoteCompletedEventHandler(object sender, GenerateReportWordRemoteCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerateReportWordRemoteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GenerateReportWordRemoteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public UploadedImageInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((UploadedImageInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void UserHeaderInitCompletedEventHandler(object sender, UserHeaderInitCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserHeaderInitCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UserHeaderInitCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void ZoneHeaderInitCompletedEventHandler(object sender, ZoneHeaderInitCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ZoneHeaderInitCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ZoneHeaderInitCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CreateThumbImagesCompletedEventHandler(object sender, CreateThumbImagesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateThumbImagesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateThumbImagesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CreateThumbImages1CompletedEventHandler(object sender, CreateThumbImages1CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateThumbImages1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateThumbImages1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CreateThumbImages4ScaleCompletedEventHandler(object sender, CreateThumbImages4ScaleCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateThumbImages4ScaleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateThumbImages4ScaleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void Select_ImageLogInfo_ByGuidCompletedEventHandler(object sender, Select_ImageLogInfo_ByGuidCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Select_ImageLogInfo_ByGuidCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal Select_ImageLogInfo_ByGuidCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ImageLogInfo Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ImageLogInfo)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void Select_ImageLogInfo_ByPagedCompletedEventHandler(object sender, Select_ImageLogInfo_ByPagedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Select_ImageLogInfo_ByPagedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal Select_ImageLogInfo_ByPagedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ImageLogInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ImageLogInfo[])(this.results[0]));
            }
        }

        /// <remarks/>
        public int iRows
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void DeleteImageByUrlCompletedEventHandler(object sender, DeleteImageByUrlCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteImageByUrlCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteImageByUrlCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void DeleteImageCompletedEventHandler(object sender, DeleteImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void CheckImagePassCompletedEventHandler(object sender, CheckImagePassCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckImagePassCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CheckImagePassCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void RestoreImageCompletedEventHandler(object sender, RestoreImageCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RestoreImageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal RestoreImageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}