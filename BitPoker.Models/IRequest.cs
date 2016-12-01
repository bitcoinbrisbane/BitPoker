using System;

namespace BitPoker.Models
{
    /// <summary>
    /// JSON RPC Interface
    /// </summary>
	public interface IRequest
	{
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }

        String Method { get; set; }

        String Signature { get; set; }

        Object Params { get; set; }
    }
}