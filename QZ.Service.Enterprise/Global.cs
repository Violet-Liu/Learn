using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QZ.Instrument.Model;
using QZ.Instrument.Utility;

namespace QZ.Service.Enterprise
{
    public class Global
    {
        private static Global _instance = new Global();
        public static Global Instance { get { return _instance; } }
        private Global()
        { }

        public void Initialize()
        {
            Services = Constants.Service_Path.DeserializeFromXml<Services>();
        }

        public Services Services { get; private set; }
    }
}