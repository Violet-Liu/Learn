using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;

namespace QZ.Instrument.Global
{
    public class DataBus
    {
        static DataBus()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Services = (AppDomain.CurrentDomain.BaseDirectory + Constants.Service_Path).DeserializeFromXml<Services>();
            Metadata_Uris = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + Constants.Uri_Metadata_Path).ToObject<List<Uri_Metadata>>();
        }

        public static Services Services { get; private set; }
        private static string[] _uris;// = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.Elasticsearch_Name)).Uris;
        public static string[] Elasticsearch_Uris
        {
            get
            {
                if(_uris == null)
                    _uris = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.Elasticsearch_Name)).Uris;
                return _uris;
            }
            private set { _uris = value; }
        }
        private static string[] _es_5_0_0_Uris;// = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.ES_5_0_0_Name)).Uris;
        public static string[] ES_5_0_0_Uris
        {
            get
            {
                if (_es_5_0_0_Uris == null)
                    _es_5_0_0_Uris = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.ES_5_0_0_Name)).Uris;
                return _es_5_0_0_Uris;
            }
            private set { _es_5_0_0_Uris = value; }
        }
        private static Uri[] _es_5_0_Uris;
        public static Uri[] ES_5_0_Uris
        {
            get
            {
                if (_es_5_0_Uris == null)
                    _es_5_0_Uris = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.ES_5_0_0_Name)).Uris.Select(u => new Uri(u)).ToArray();
                return _es_5_0_Uris;
            }
        }
        public static string[] CompanyNameIndex_Uris { get { return Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.CompanyNameIndex_Name)).Uris; } }
        public static string[] CompanyMap_Uris { get { return Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.CompanyMap_Name)).Uris; } }

        private static string[] _upload_Uris;
        public static string[] Upload_Uris
        {
            get
            {
                if (_upload_Uris == null)
                    _upload_Uris = Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.Upload_Name)).Uris;
                return _upload_Uris;
            }
            private set { _upload_Uris = value; }
        }
        public static string[] Portrait_Uris { get { return Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.Portrait_Upload_Name)).Uris; } }
        public static List<Uri_Metadata> Metadata_Uris { get; private set; }

        public static string[] CompanyTrade_Uris { get { return Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.CompanyTrade_Name)).Uris; } }
        public static string[] ShortMsg_Uris { get { return Services.Service_List.FirstOrDefault(s => s.Name.Equals(Constants.ShortMsg_Name)).Uris; } }
    }
}
