using System;

namespace BitPoker.Models.Messages
{
    public class DeckResponse : RPCResponse
    {
        public Guid HandId { get; set; }

        public IDeck Deck { get; set; }

        public DeckResponse()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
