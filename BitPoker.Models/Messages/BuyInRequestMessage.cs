using System;

namespace BitPoker.Models.Messages
{
    public class BuyInRequestMessage
    {
        public Guid TableId { get; set; }

        public Int64 Amount { get; set; }

        public String PubKey { get; set; }
    }
}
