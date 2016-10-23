using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BitPoker.Models.Messages
{
    public abstract class BaseMessage
    {
        [DataMember]
        public Decimal Version { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        /// <summary>
        /// Pub Key Hash
        /// </summary>
        public String BitcoinAddress { get; set; }

        [DataMember]
        public String Signature { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }
    }
}
