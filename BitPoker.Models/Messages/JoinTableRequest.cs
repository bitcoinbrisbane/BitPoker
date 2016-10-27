using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest : BaseRequest
    {
        public PlayerInfo Player { get; set; }

        public UInt16 Seat { get; set; }

        public JoinTableRequest()
        {
            base.Version = 1.0M;
        }
    }
}
