using System;

namespace BitPoker.Models.Messages
{
    public class DeckRequest : BaseRequest, IMessage
    {
        public Guid HandId { get; set; }

        public DeckRequest()
        {
            base.Version = 1.0M;
        }
    }
}
