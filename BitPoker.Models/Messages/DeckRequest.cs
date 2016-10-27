using System;

namespace BitPoker.Models.Messages
{
    public class DeckRequest : BaseRequest
    {
        public Guid HandId { get; set; }

        public DeckRequest()
        {
            base.Version = 1.0M;
        }
    }
}
