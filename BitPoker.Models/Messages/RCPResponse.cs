using Newtonsoft.Json;
using System;

namespace BitPoker.Models.Messages
{
    public class RCPResponse : IResponse
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "error")]
        public Object Error { get; set; }

        [JsonProperty(PropertyName = "result")]
        public Object Result { get; set; }
    }
}
