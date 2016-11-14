using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Service.Enterprise
{
    [System.Runtime.Serialization.DataContractAttribute(Name = "SMSResult", Namespace = "http://schemas.datacontract.org/2004/07/QZ.SRM.SMService")]
    [System.SerializableAttribute()]
    public partial class SMSResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EmayCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double EmayQuotasField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorStrField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long SMSIdentityField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusStrField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessedField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EmayCode
        {
            get
            {
                return this.EmayCodeField;
            }
            set
            {
                if ((this.EmayCodeField.Equals(value) != true))
                {
                    this.EmayCodeField = value;
                    this.RaisePropertyChanged("EmayCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public double EmayQuotas
        {
            get
            {
                return this.EmayQuotasField;
            }
            set
            {
                if ((this.EmayQuotasField.Equals(value) != true))
                {
                    this.EmayQuotasField = value;
                    this.RaisePropertyChanged("EmayQuotas");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorStr
        {
            get
            {
                return this.ErrorStrField;
            }
            set
            {
                if ((object.ReferenceEquals(this.ErrorStrField, value) != true))
                {
                    this.ErrorStrField = value;
                    this.RaisePropertyChanged("ErrorStr");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long SMSIdentity
        {
            get
            {
                return this.SMSIdentityField;
            }
            set
            {
                if ((this.SMSIdentityField.Equals(value) != true))
                {
                    this.SMSIdentityField = value;
                    this.RaisePropertyChanged("SMSIdentity");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StatusStr
        {
            get
            {
                return this.StatusStrField;
            }
            set
            {
                if ((object.ReferenceEquals(this.StatusStrField, value) != true))
                {
                    this.StatusStrField = value;
                    this.RaisePropertyChanged("StatusStr");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Successed
        {
            get
            {
                return this.SuccessedField;
            }
            set
            {
                if ((this.SuccessedField.Equals(value) != true))
                {
                    this.SuccessedField = value;
                    this.RaisePropertyChanged("Successed");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "EmayStatusEnum.SmsPriority", Namespace = "http://schemas.datacontract.org/2004/07/QZ.SRM.SMService")]
    public enum EmayStatusEnumSmsPriority : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        很低 = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        低 = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        中 = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        高 = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        很高 = 5,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "mo", Namespace = "http://schemas.datacontract.org/2004/07/QZ.SRM.SMService.EmaySMSVC")]
    [System.SerializableAttribute()]
    public partial class mo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string addSerialFieldField;

        private string addSerialRevFieldField;

        private string channelnumberFieldField;

        private string mobileNumberFieldField;

        private string sentTimeFieldField;

        private string smsContentFieldField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string addSerialField
        {
            get
            {
                return this.addSerialFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.addSerialFieldField, value) != true))
                {
                    this.addSerialFieldField = value;
                    this.RaisePropertyChanged("addSerialField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string addSerialRevField
        {
            get
            {
                return this.addSerialRevFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.addSerialRevFieldField, value) != true))
                {
                    this.addSerialRevFieldField = value;
                    this.RaisePropertyChanged("addSerialRevField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string channelnumberField
        {
            get
            {
                return this.channelnumberFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.channelnumberFieldField, value) != true))
                {
                    this.channelnumberFieldField = value;
                    this.RaisePropertyChanged("channelnumberField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string mobileNumberField
        {
            get
            {
                return this.mobileNumberFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.mobileNumberFieldField, value) != true))
                {
                    this.mobileNumberFieldField = value;
                    this.RaisePropertyChanged("mobileNumberField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string sentTimeField
        {
            get
            {
                return this.sentTimeFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.sentTimeFieldField, value) != true))
                {
                    this.sentTimeFieldField = value;
                    this.RaisePropertyChanged("sentTimeField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string smsContentField
        {
            get
            {
                return this.smsContentFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.smsContentFieldField, value) != true))
                {
                    this.smsContentFieldField = value;
                    this.RaisePropertyChanged("smsContentField");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "statusReport", Namespace = "http://schemas.datacontract.org/2004/07/QZ.SRM.SMService.EmaySMSVC")]
    [System.SerializableAttribute()]
    public partial class statusReport : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string errorCodeFieldField;

        private string memoFieldField;

        private string mobileFieldField;

        private string receiveDateFieldField;

        private int reportStatusFieldField;

        private long seqIDFieldField;

        private string serviceCodeAddFieldField;

        private string submitDateFieldField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string errorCodeField
        {
            get
            {
                return this.errorCodeFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.errorCodeFieldField, value) != true))
                {
                    this.errorCodeFieldField = value;
                    this.RaisePropertyChanged("errorCodeField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string memoField
        {
            get
            {
                return this.memoFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.memoFieldField, value) != true))
                {
                    this.memoFieldField = value;
                    this.RaisePropertyChanged("memoField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string mobileField
        {
            get
            {
                return this.mobileFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.mobileFieldField, value) != true))
                {
                    this.mobileFieldField = value;
                    this.RaisePropertyChanged("mobileField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string receiveDateField
        {
            get
            {
                return this.receiveDateFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.receiveDateFieldField, value) != true))
                {
                    this.receiveDateFieldField = value;
                    this.RaisePropertyChanged("receiveDateField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public int reportStatusField
        {
            get
            {
                return this.reportStatusFieldField;
            }
            set
            {
                if ((this.reportStatusFieldField.Equals(value) != true))
                {
                    this.reportStatusFieldField = value;
                    this.RaisePropertyChanged("reportStatusField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public long seqIDField
        {
            get
            {
                return this.seqIDFieldField;
            }
            set
            {
                if ((this.seqIDFieldField.Equals(value) != true))
                {
                    this.seqIDFieldField = value;
                    this.RaisePropertyChanged("seqIDField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string serviceCodeAddField
        {
            get
            {
                return this.serviceCodeAddFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.serviceCodeAddFieldField, value) != true))
                {
                    this.serviceCodeAddFieldField = value;
                    this.RaisePropertyChanged("serviceCodeAddField");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
        public string submitDateField
        {
            get
            {
                return this.submitDateFieldField;
            }
            set
            {
                if ((object.ReferenceEquals(this.submitDateFieldField, value) != true))
                {
                    this.submitDateFieldField = value;
                    this.RaisePropertyChanged("submitDateField");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://services.qianzhan.com/", ConfigurationName = "SMService.ISMService")]
    public interface IShortMsg
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ILogOutputable/GetServerLog", ReplyAction = "http://services.qianzhan.com/ILogOutputable/GetServerLogResponse")]
        System.Collections.Generic.List<string> GetServerLog(out int writeIndex, int readerIndex);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ILogOutputable/GetRemarkLog", ReplyAction = "http://services.qianzhan.com/ILogOutputable/GetRemarkLogResponse")]
        string GetRemarkLog();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/IRemoteControlable/Start", ReplyAction = "http://services.qianzhan.com/IRemoteControlable/StartResponse")]
        void Start(System.Collections.Generic.List<string> args);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/IRemoteControlable/SetServerSuppendStatus", ReplyAction = "http://services.qianzhan.com/IRemoteControlable/SetServerSuppendStatusResponse")]
        void SetServerSuppendStatus(bool status);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/IRemoteControlable/SetServerAbortStatus", ReplyAction = "http://services.qianzhan.com/IRemoteControlable/SetServerAbortStatusResponse")]
        void SetServerAbortStatus(bool abort);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/IStatusOuputable/StatusString", ReplyAction = "http://services.qianzhan.com/IStatusOuputable/StatusStringResponse")]
        string StatusString();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/IWatcherable/Beats", ReplyAction = "http://services.qianzhan.com/IWatcherable/BeatsResponse")]
        bool Beats(out bool suppend, out bool abort);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/Regist", ReplyAction = "http://services.qianzhan.com/ISMService/RegistResponse")]
        SMSResult Regist();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/RegistDetailInfo", ReplyAction = "http://services.qianzhan.com/ISMService/RegistDetailInfoResponse")]
        SMSResult RegistDetailInfo();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/Logout", ReplyAction = "http://services.qianzhan.com/ISMService/LogoutResponse")]
        SMSResult Logout();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/GetEachFee", ReplyAction = "http://services.qianzhan.com/ISMService/GetEachFeeResponse")]
        SMSResult GetEachFee();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/GetBalance", ReplyAction = "http://services.qianzhan.com/ISMService/GetBalanceResponse")]
        SMSResult GetBalance();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/ChangeUp", ReplyAction = "http://services.qianzhan.com/ISMService/ChangeUpResponse")]
        SMSResult ChangeUp(string cardNo, string cardPass);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/SendSMSExt", ReplyAction = "http://services.qianzhan.com/ISMService/SendSMSExtResponse")]
        SMSResult SendSMSExt(string taskName, string user, string sendTime, System.Collections.Generic.List<string> mobiles, string smsContent, string addSerial, string srcCharset, EmayStatusEnumSmsPriority smsPriority);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/SendSMS", ReplyAction = "http://services.qianzhan.com/ISMService/SendSMSResponse")]
        SMSResult SendSMS(string taskName, string user, string mobile, string smsContent);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/GetMO", ReplyAction = "http://services.qianzhan.com/ISMService/GetMOResponse")]
        System.Collections.Generic.List<mo> GetMO();

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/SerialPwdUpd", ReplyAction = "http://services.qianzhan.com/ISMService/SerialPwdUpdResponse")]
        SMSResult SerialPwdUpd(string serialNo, string key, string serialPwd, string pwdNew);

        [System.ServiceModel.OperationContractAttribute(Action = "http://services.qianzhan.com/ISMService/GetStatusReportList", ReplyAction = "http://services.qianzhan.com/ISMService/GetStatusReportListResponse")]
        System.Collections.Generic.List<statusReport> GetStatusReportList();
    }
}
