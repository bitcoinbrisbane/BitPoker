using System;

namespace BitPoker.Models.Messages
{
    /// <summary>
    /// Interface that all messages must implement
    /// </summary>
    public interface IMessage
    {
		Guid Id { get; }

        Decimal Version { get; }

		String BitcoinAddress { get; }

		String Hash { get; }

		String Signature { get; }
    }
}
