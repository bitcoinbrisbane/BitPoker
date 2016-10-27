using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace BitPoker.Models.Messages
{
    public abstract class BaseRequest
    {
        [DataMember]
        public Decimal Version { get; set; }

        [DataMember]
        /// <summary>
        /// Pub Key Hash
        /// </summary>
        public String BitcoinAddress { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }

        //[DataMember]
        //[JsonProperty(PropertyName = "signature")]
        //public String Signature { get; set; }
    }
}
