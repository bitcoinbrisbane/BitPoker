using System;

namespace BitPoker.Models.Messages
{
    public class RCPResponse
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public Object Error { get; set; }

        public Object Result { get; set; }
    }
}
