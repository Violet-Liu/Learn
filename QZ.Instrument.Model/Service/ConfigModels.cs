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
using System.Xml.Serialization;

namespace QZ.Instrument.Model
{
    [XmlRoot("services")]
    public class Services
    {
        [XmlElement("service")]
        public List<Service> Service_List { get; set; }
    }

    public class Service
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("uri")]
        public string[] Uris { get; set; }
    }

    public class Uri_Metadata
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
