using System;

namespace BitPoker.Models.Messages
{
    public class DeckResponse
    {
        public Guid TableId { get; set; }

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
