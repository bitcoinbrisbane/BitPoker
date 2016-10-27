using System;

namespace BitPoker.Models.Messages
{
    /// <summary>
    /// Interface that all messages must implement
    /// </summary>
    public interface IMessage
    {
        Decimal Version { get; }

        //String Signature { get; set; }
    }
}
