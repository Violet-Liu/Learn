using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace QZ.Service.Enterprise
{
    [DataContract(Name = "CompanyInput", Namespace = "http://schemas.datacontract.org/2004/07/QZ.N2E.CompanyTermService")]
    [Serializable]
    public partial class CompanyInput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<string> LPGDListField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<string> NPGdListField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string addressField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string areaCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool mainLandOrgField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;

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
        public System.Collections.Generic.List<string> LPGDList
        {
            get
            {
                return this.LPGDListField;
            }
            set
            {
                if ((object.ReferenceEquals(this.LPGDListField, value) != true))
                {
                    this.LPGDListField = value;
                    this.RaisePropertyChanged("LPGDList");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<string> NPGdList
        {
            get
            {
                return this.NPGdListField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NPGdListField, value) != true))
                {
                    this.NPGdListField = value;
                    this.RaisePropertyChanged("NPGdList");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                if ((object.ReferenceEquals(this.addressField, value) != true))
                {
                    this.addressField = value;
                    this.RaisePropertyChanged("address");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string areaCode
        {
            get
            {
                return this.areaCodeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.areaCodeField, value) != true))
                {
                    this.areaCodeField = value;
                    this.RaisePropertyChanged("areaCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.codeField, value) != true))
                {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool mainLandOrg
        {
            get
            {
                return this.mainLandOrgField;
            }
            set
            {
                if ((this.mainLandOrgField.Equals(value) != true))
                {
                    this.mainLandOrgField = value;
                    this.RaisePropertyChanged("mainLandOrg");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nameField, value) != true))
                {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
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
    [DataContract(Name = "CompanyRelation", Namespace = "http://schemas.datacontract.org/2004/07/QZ.N2E.CompanyTermService")]
    [Serializable]
    public partial class CompanyRelation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string areaCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string areaSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string branchSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int indexField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>> invisbleNameMetaDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>> invisibleAddrMetaDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<int>>> invisibleAddrRawDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<int>>> invisibleNameRawDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<CompanyRelation> invisibleRelationsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int memoryStoreIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<CompanyRelation> nextRelationsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int parentIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<CompanyRelation> previousRelationsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int relationTypeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int storeTypeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CompanyTerm termField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tradeSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string typeSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int weightField;

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
        public string areaCode
        {
            get
            {
                return this.areaCodeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.areaCodeField, value) != true))
                {
                    this.areaCodeField = value;
                    this.RaisePropertyChanged("areaCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string areaSegment
        {
            get
            {
                return this.areaSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.areaSegmentField, value) != true))
                {
                    this.areaSegmentField = value;
                    this.RaisePropertyChanged("areaSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string branchSegment
        {
            get
            {
                return this.branchSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.branchSegmentField, value) != true))
                {
                    this.branchSegmentField = value;
                    this.RaisePropertyChanged("branchSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.codeField, value) != true))
                {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                if ((this.indexField.Equals(value) != true))
                {
                    this.indexField = value;
                    this.RaisePropertyChanged("index");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>> invisbleNameMetaData
        {
            get
            {
                return this.invisbleNameMetaDataField;
            }
            set
            {
                if ((object.ReferenceEquals(this.invisbleNameMetaDataField, value) != true))
                {
                    this.invisbleNameMetaDataField = value;
                    this.RaisePropertyChanged("invisbleNameMetaData");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>> invisibleAddrMetaData
        {
            get
            {
                return this.invisibleAddrMetaDataField;
            }
            set
            {
                if ((object.ReferenceEquals(this.invisibleAddrMetaDataField, value) != true))
                {
                    this.invisibleAddrMetaDataField = value;
                    this.RaisePropertyChanged("invisibleAddrMetaData");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<int>>> invisibleAddrRawData
        {
            get
            {
                return this.invisibleAddrRawDataField;
            }
            set
            {
                if ((object.ReferenceEquals(this.invisibleAddrRawDataField, value) != true))
                {
                    this.invisibleAddrRawDataField = value;
                    this.RaisePropertyChanged("invisibleAddrRawData");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, System.Collections.Generic.List<int>>> invisibleNameRawData
        {
            get
            {
                return this.invisibleNameRawDataField;
            }
            set
            {
                if ((object.ReferenceEquals(this.invisibleNameRawDataField, value) != true))
                {
                    this.invisibleNameRawDataField = value;
                    this.RaisePropertyChanged("invisibleNameRawData");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<CompanyRelation> invisibleRelations
        {
            get
            {
                return this.invisibleRelationsField;
            }
            set
            {
                if ((object.ReferenceEquals(this.invisibleRelationsField, value) != true))
                {
                    this.invisibleRelationsField = value;
                    this.RaisePropertyChanged("invisibleRelations");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int memoryStoreId
        {
            get
            {
                return this.memoryStoreIdField;
            }
            set
            {
                if ((this.memoryStoreIdField.Equals(value) != true))
                {
                    this.memoryStoreIdField = value;
                    this.RaisePropertyChanged("memoryStoreId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nameField, value) != true))
                {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nameSegment
        {
            get
            {
                return this.nameSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nameSegmentField, value) != true))
                {
                    this.nameSegmentField = value;
                    this.RaisePropertyChanged("nameSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<CompanyRelation> nextRelations
        {
            get
            {
                return this.nextRelationsField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nextRelationsField, value) != true))
                {
                    this.nextRelationsField = value;
                    this.RaisePropertyChanged("nextRelations");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int parentId
        {
            get
            {
                return this.parentIdField;
            }
            set
            {
                if ((this.parentIdField.Equals(value) != true))
                {
                    this.parentIdField = value;
                    this.RaisePropertyChanged("parentId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<CompanyRelation> previousRelations
        {
            get
            {
                return this.previousRelationsField;
            }
            set
            {
                if ((object.ReferenceEquals(this.previousRelationsField, value) != true))
                {
                    this.previousRelationsField = value;
                    this.RaisePropertyChanged("previousRelations");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int relationType
        {
            get
            {
                return this.relationTypeField;
            }
            set
            {
                if ((this.relationTypeField.Equals(value) != true))
                {
                    this.relationTypeField = value;
                    this.RaisePropertyChanged("relationType");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int storeType
        {
            get
            {
                return this.storeTypeField;
            }
            set
            {
                if ((this.storeTypeField.Equals(value) != true))
                {
                    this.storeTypeField = value;
                    this.RaisePropertyChanged("storeType");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public CompanyTerm term
        {
            get
            {
                return this.termField;
            }
            set
            {
                if ((this.termField.Equals(value) != true))
                {
                    this.termField = value;
                    this.RaisePropertyChanged("term");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tradeSegment
        {
            get
            {
                return this.tradeSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.tradeSegmentField, value) != true))
                {
                    this.tradeSegmentField = value;
                    this.RaisePropertyChanged("tradeSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string typeSegment
        {
            get
            {
                return this.typeSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.typeSegmentField, value) != true))
                {
                    this.typeSegmentField = value;
                    this.RaisePropertyChanged("typeSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                if ((this.weightField.Equals(value) != true))
                {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
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
    [DataContract(Name = "CompanyTerm", Namespace = "http://schemas.datacontract.org/2004/07/QZ.N2E.CompanyTermService")]
    [Serializable]
    public partial struct CompanyTerm : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int companyTermIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int investorRelationTermIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int segmentsStoreIdField;

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
        public int companyTermId
        {
            get
            {
                return this.companyTermIdField;
            }
            set
            {
                if ((this.companyTermIdField.Equals(value) != true))
                {
                    this.companyTermIdField = value;
                    this.RaisePropertyChanged("companyTermId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int investorRelationTermId
        {
            get
            {
                return this.investorRelationTermIdField;
            }
            set
            {
                if ((this.investorRelationTermIdField.Equals(value) != true))
                {
                    this.investorRelationTermIdField = value;
                    this.RaisePropertyChanged("investorRelationTermId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int segmentsStoreId
        {
            get
            {
                return this.segmentsStoreIdField;
            }
            set
            {
                if ((this.segmentsStoreIdField.Equals(value) != true))
                {
                    this.segmentsStoreIdField = value;
                    this.RaisePropertyChanged("segmentsStoreId");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    [DataContract(Name = "RelationSimple", Namespace = "http://schemas.datacontract.org/2004/07/QZ.N2E.CompanyTermService")]
    [Serializable]
    public partial class RelationSimple : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string areaCodeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string areaSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string branchSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int indexField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int memoryStoreIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nameSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int parentIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int relationTypeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int storeTypeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int termIdField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tradeSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string typeSegmentField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int weightField;

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
        public string areaCode
        {
            get
            {
                return this.areaCodeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.areaCodeField, value) != true))
                {
                    this.areaCodeField = value;
                    this.RaisePropertyChanged("areaCode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string areaSegment
        {
            get
            {
                return this.areaSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.areaSegmentField, value) != true))
                {
                    this.areaSegmentField = value;
                    this.RaisePropertyChanged("areaSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string branchSegment
        {
            get
            {
                return this.branchSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.branchSegmentField, value) != true))
                {
                    this.branchSegmentField = value;
                    this.RaisePropertyChanged("branchSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                if ((object.ReferenceEquals(this.codeField, value) != true))
                {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                if ((this.indexField.Equals(value) != true))
                {
                    this.indexField = value;
                    this.RaisePropertyChanged("index");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int memoryStoreId
        {
            get
            {
                return this.memoryStoreIdField;
            }
            set
            {
                if ((this.memoryStoreIdField.Equals(value) != true))
                {
                    this.memoryStoreIdField = value;
                    this.RaisePropertyChanged("memoryStoreId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nameField, value) != true))
                {
                    this.nameField = value;
                    this.RaisePropertyChanged("name");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nameSegment
        {
            get
            {
                return this.nameSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.nameSegmentField, value) != true))
                {
                    this.nameSegmentField = value;
                    this.RaisePropertyChanged("nameSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int parentId
        {
            get
            {
                return this.parentIdField;
            }
            set
            {
                if ((this.parentIdField.Equals(value) != true))
                {
                    this.parentIdField = value;
                    this.RaisePropertyChanged("parentId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int relationType
        {
            get
            {
                return this.relationTypeField;
            }
            set
            {
                if ((this.relationTypeField.Equals(value) != true))
                {
                    this.relationTypeField = value;
                    this.RaisePropertyChanged("relationType");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int storeType
        {
            get
            {
                return this.storeTypeField;
            }
            set
            {
                if ((this.storeTypeField.Equals(value) != true))
                {
                    this.storeTypeField = value;
                    this.RaisePropertyChanged("storeType");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int termId
        {
            get
            {
                return this.termIdField;
            }
            set
            {
                if ((this.termIdField.Equals(value) != true))
                {
                    this.termIdField = value;
                    this.RaisePropertyChanged("termId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tradeSegment
        {
            get
            {
                return this.tradeSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.tradeSegmentField, value) != true))
                {
                    this.tradeSegmentField = value;
                    this.RaisePropertyChanged("tradeSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string typeSegment
        {
            get
            {
                return this.typeSegmentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.typeSegmentField, value) != true))
                {
                    this.typeSegmentField = value;
                    this.RaisePropertyChanged("typeSegment");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                if ((this.weightField.Equals(value) != true))
                {
                    this.weightField = value;
                    this.RaisePropertyChanged("weight");
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
    [DataContract(Name = "JsonResult", Namespace = "http://schemas.datacontract.org/2004/07/QZ.N2E.CompanyTermService.Json")]
    [Serializable]
    public partial class JsonResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] bytesField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool compressedField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int linksField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int maxDimensionField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int nodesField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int rawLengthField;

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
        public byte[] bytes
        {
            get
            {
                return this.bytesField;
            }
            set
            {
                if ((object.ReferenceEquals(this.bytesField, value) != true))
                {
                    this.bytesField = value;
                    this.RaisePropertyChanged("bytes");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool compressed
        {
            get
            {
                return this.compressedField;
            }
            set
            {
                if ((this.compressedField.Equals(value) != true))
                {
                    this.compressedField = value;
                    this.RaisePropertyChanged("compressed");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int links
        {
            get
            {
                return this.linksField;
            }
            set
            {
                if ((this.linksField.Equals(value) != true))
                {
                    this.linksField = value;
                    this.RaisePropertyChanged("links");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int maxDimension
        {
            get
            {
                return this.maxDimensionField;
            }
            set
            {
                if ((this.maxDimensionField.Equals(value) != true))
                {
                    this.maxDimensionField = value;
                    this.RaisePropertyChanged("maxDimension");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int nodes
        {
            get
            {
                return this.nodesField;
            }
            set
            {
                if ((this.nodesField.Equals(value) != true))
                {
                    this.nodesField = value;
                    this.RaisePropertyChanged("nodes");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int rawLength
        {
            get
            {
                return this.rawLengthField;
            }
            set
            {
                if ((this.rawLengthField.Equals(value) != true))
                {
                    this.rawLengthField = value;
                    this.RaisePropertyChanged("rawLength");
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

    [ServiceContractAttribute(Namespace = "http://entprise.qianzhan.com/CompanyMapService", ConfigurationName = "CompanyMapService.CompanyMapService")]
    public interface ICompanyMap
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/StoreAmount", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/StoreAmountRespo" +
            "nse")]
        int StoreAmount();

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/TermMapDicCount", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/TermMapDicCountR" +
            "esponse")]
        int TermMapDicCount();

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetVersion", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetVersionRespon" +
            "se")]
        int GetVersion();

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/BuildMap", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/BuildMapResponse" +
            "")]
        int BuildMap(System.Collections.Generic.List<CompanyInput> input, bool strict);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMap", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapResponse")]
        CompanyRelation GetMap(string companyName, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapSimpleList" +
            "", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapSimpleList" +
            "Response")]
        List<RelationSimple> GetMapSimpleList(string company, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapJsonResult" +
            "", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapJsonResult" +
            "Response")]
        JsonResult GetMapJsonResult(string company, int dimession, int minInvisibleFilter, bool enbaleAreaName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapByNameInde" +
            "xId", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapByNameInde" +
            "xIdResponse")]
        CompanyRelation GetMapByNameIndexId(int id, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapSimpleList" +
            "ByNameIndexId", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapSimpleList" +
            "ByNameIndexIdResponse")]
        System.Collections.Generic.List<RelationSimple> GetMapSimpleListByNameIndexId(int id, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapJsonResult" +
            "ByNameIndexId", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapJsonResult" +
            "ByNameIndexIdResponse")]
        JsonResult GetMapJsonResultByNameIndexId(int id, int dimession, int minInvisibleFilter, bool enbaleAreaName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anys", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysResponse")]
        CompanyRelation GetAllInvestCompanys(string companyName, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysByNameIndexId", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysByNameIndexIdResponse")]
        CompanyRelation GetAllInvestCompanysByNameIndexId(int id, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysSimpleListByNameIndexId", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysSimpleListByNameIndexIdResponse")]
        System.Collections.Generic.List<RelationSimple> GetAllInvestCompanysSimpleListByNameIndexId(int id, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysSimpleList", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetAllInvestComp" +
            "anysSimpleListResponse")]
        System.Collections.Generic.List<RelationSimple> GetAllInvestCompanysSimpleList(string companyName, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapList", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListRespon" +
            "se")]
        System.Collections.Generic.List<CompanyRelation> GetMapList(System.Collections.Generic.List<string> companyNameList, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListSimple" +
            "List", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListSimple" +
            "ListResponse")]
        System.Collections.Generic.List<System.Collections.Generic.List<RelationSimple>> GetMapListSimpleList(System.Collections.Generic.List<string> companyNameList, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListJsonRe" +
            "sult", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListJsonRe" +
            "sultResponse")]
        System.Collections.Generic.List<JsonResult> GetMapListJsonResult(System.Collections.Generic.List<string> companyNameList, int dimession, int minInvisibleFilter, bool enbaleAreaName);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIds", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIdsR" +
            "esponse")]
        System.Collections.Generic.List<CompanyRelation> GetMapListByIds(System.Collections.Generic.List<int> companyIndexList, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIdsS" +
            "impleList", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIdsS" +
            "impleListResponse")]
        List<System.Collections.Generic.List<RelationSimple>> GetMapListByIdsSimpleList(System.Collections.Generic.List<int> companyIndexList, int dimession, int minInvisibleFilter);

        [System.ServiceModel.OperationContractAttribute(Action = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIdsJ" +
            "sonResult", ReplyAction = "http://entprise.qianzhan.com/CompanyMapService/CompanyMapService/GetMapListByIdsJ" +
            "sonResultResponse")]
        System.Collections.Generic.List<JsonResult> GetMapListByIdsJsonResult(System.Collections.Generic.List<int> companyIndexList, int dimession, int minInvisibleFilter, bool enbaleAreaName);
    }

    public interface ICompanyMapChannel : ICompanyMap, IClientChannel
    { }
}