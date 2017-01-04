using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest : BaseRequest
    {
        public Guid TableId { get; set; }

        public Peer Player { get; set; }

        public JoinTableRequest()
        {
            base.Version = 1.0M;
        }
    }
}
