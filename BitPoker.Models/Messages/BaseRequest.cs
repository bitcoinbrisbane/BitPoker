using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace BitPoker.Models.Messages
{
    public abstract class BaseRequest
    {
        public Guid Id { get; set; }

        [DataMember]
        public Decimal Version { get; set; }

        [DataMember]
        /// <summary>
        /// Pub Key Hash
        /// </summary>
        public String BitcoinAddress { get; set; }

        [DataMember]
        public DateTime TimeStamp { get; set; }

		public String Hash { get; set; }

		public String Signature { get; set; }

		public override string ToString()
		{
			return string.Format("[BaseRequest: Id={0}, Version={1}, BitcoinAddress={2}, TimeStamp={3}, Hash={4}, Signature={5}]", Id, Version, BitcoinAddress, TimeStamp, Hash, Signature);
		}
    }
}