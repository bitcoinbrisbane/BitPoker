using System;

namespace BitPoker.Models.Messages
{
    public class NewPeerRequest : BaseRequest, IMessage
    {
        public Peer Player { get; set; }
    }
}