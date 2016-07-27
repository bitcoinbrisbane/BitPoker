using System;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
    public abstract class BaseMessage
    {
        public Int16 Version { get; set; }

        public Guid Id { get; set; }

        /// <summary>
        /// Pub Key Hash
        /// </summary>
        public String BitcoinAddress { get; set; }

        public String Signature { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
