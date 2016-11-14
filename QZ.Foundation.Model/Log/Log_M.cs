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
using System.Runtime.Serialization;

namespace QZ.Foundation.Model
{
    [DataContract]
    public class Log_M
    {
        [DataMember]
        public long Id { get; set; }
        /// <summary>
        /// request uri
        /// </summary>
        [DataMember]
        public string Uri { get; set; }
        ///// <summary>
        ///// log severity
        ///// </summary>
        //[JsonProperty("severity")]
        //public Severity Severity { get; set; }
        /// <summary>
        /// location of log
        /// </summary>
        [DataMember]
        public Location Location { get; set; }
        /// <summary>
        /// log message
        /// </summary>
        [DataMember]
        public string Message { get; set; }
        /// <summary>
        /// artifical analysis
        /// </summary>
        [DataMember]
        public string Analysis { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}\tUri:{Uri}\tLocation:{Location}\tMessage:{Message}\tAnalysis:{Analysis}";
        }
    }
}
