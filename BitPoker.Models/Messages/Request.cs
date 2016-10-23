using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    [DataContract]
    public class Request : BaseMessage, IMessage
    {
        [DataMember]
        public String Type { get; set; }

        [DataMember]
        public Object Payload { get; set; }

        public Request()
        {
            this.Id = new Guid();
        }
    }
}
