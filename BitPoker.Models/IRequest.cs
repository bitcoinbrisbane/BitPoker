using Newtonsoft.Json;
using System;

namespace BitPoker.Models
{
    /// <summary>
    /// JSON RPC Interface
    /// </summary>
	public interface IRequest
	{
        [JsonProperty(PropertyName = "id")]
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }

        [JsonProperty(PropertyName = "method")]
        String Method { get; set; }

        //String Signature { get; set; }

        [JsonProperty(PropertyName = "params")]
        Object Params { get; set; }
    }
}