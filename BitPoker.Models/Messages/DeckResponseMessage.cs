using System;

namespace BitPoker.Models.Messages
{
    public class DeckResponseMessage : BaseMessage
    {
        public Guid TableId { get; set; }

        public Guid HandId { get; set; }

        public IDeck Deck { get; set; }

        public DeckResponseMessage()
        {
            base.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
