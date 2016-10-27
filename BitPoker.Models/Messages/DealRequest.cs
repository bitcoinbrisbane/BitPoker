using System;
using System.Collections.Generic;
using System.Linq;

namespace BitPoker.Models.Messages
{
    public class DealRequest : BaseRequest
    {
        public Guid TableId { get; set; }

        public IDeck Deck { get; set; }

        public DealRequest()
        {
            base.Version = 1.0M;
        }
    }
}
