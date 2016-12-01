using BitPoker.Models.Messages;
using System;

namespace BitPoker.Models
{
    /// <summary>
    /// JSON RPC Response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }

        Object Result { get; set; }

        Object Error { get; set; }
    }
}
