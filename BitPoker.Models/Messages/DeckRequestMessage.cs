using System;

namespace BitPoker.Models.Messages
{
    public class DeckRequestMessage : BaseMessage
    {
        public Guid TableId { get; set; }
    }
}
