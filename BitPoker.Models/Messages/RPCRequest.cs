using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BitPoker.Models.Messages
{
    [DataContract]
    public class RPCRequest : IRequest
    {
        [JsonProperty(PropertyName = "jsonRPC")]
        public String JsonRPC { get { return "2.0"; } }

        [DataMember]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "method")]
        public String Method { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "params")]
        public Object Params { get; set; }

        [DataMember]
        public String Signature { get; set; }
    }
}
