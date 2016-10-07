using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest : BaseMessage
    {
        public PlayerInfo Player { get; set; }

        public UInt16 Seat { get; set; }

        public JoinTableRequest()
        {
            base.TimeStamp = DateTime.UtcNow;
            base.Id = Guid.NewGuid();
        }
    }
}
