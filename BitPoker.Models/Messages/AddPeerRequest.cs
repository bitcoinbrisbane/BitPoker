using System;

namespace BitPoker.Models.Messages
{
    public class AddPeerRequest : BaseRequest
    {
        public Peer Peer { get; set; }

        public AddPeerRequest()
        {
            base.Version = 1.0M;
        }
    }
}