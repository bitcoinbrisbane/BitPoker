using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest : BaseRequest
    {
        public Guid TableId { get; set; }

        public Peer NewPlayer { get; set; }

        public String PublicKey { get; set; }

        /// <summary>
        /// Specify the seat
        /// </summary>
        public Int16 Seat { get; set; }

        public JoinTableRequest()
        {
            base.Version = 1.0M;
        }
    }
}