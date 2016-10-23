using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest
    {
        public PlayerInfo Player { get; set; }

        public UInt16 Seat { get; set; }

        public JoinTableRequest()
        {
        }
    }
}
