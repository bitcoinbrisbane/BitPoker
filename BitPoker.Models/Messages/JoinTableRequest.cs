using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableRequest : BaseRequest, IMessage
    {
        public Guid TableId { get; set; }

        public String UserAgent { get; set; }

        public String PublicKey { get; set; }
        
        public String NetworkAddress { get; set; }

        /// <summary>
        /// Specify the seat
        /// </summary>
        public Int16 Seat { get; set; }

        public JoinTableRequest()
        {
            base.Version = 1.0M;
        }

        public override string ToString()
        {
            return string.Format("[JoinTableRequest: TableId={0}, UserAgent={1}, PublicKey={2}, NetworkAddress={3}, Seat={4}]", TableId, UserAgent, PublicKey, NetworkAddress, Seat);
        }
    }
}