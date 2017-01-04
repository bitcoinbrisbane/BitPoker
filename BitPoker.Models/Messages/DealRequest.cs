using System;

namespace BitPoker.Models.Messages
{
    public class DealRequest : BaseRequest
    {
        public Guid HandId { get; set; }

        [Obsolete]
        public IDeck Deck { get; set; }

        public DealRequest()
        {
            base.Version = 1.0M;
        }
    }
}