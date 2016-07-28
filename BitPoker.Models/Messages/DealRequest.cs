using System;
using System.Collections.Generic;
using System.Linq;

namespace BitPoker.Models.Messages
{
    public class DealRequest : BaseMessage
    {
        public Guid TableId { get; set; }

        public IDeck Deck { get; set; }
    }
}
