using System;

namespace BitPoker.Models
{
	public interface IMessage
	{
        Guid Id { get; set; }

        Decimal Version { get; set; }

        String Type { get; set; }

        /// <summary>
        /// Pub Key Hash
        /// </summary>
        String BitcoinAddress { get; set; }

        String Signature { get; set; }

        DateTime TimeStamp { get; set; }

        Object Payload { get; set; }
    }
}