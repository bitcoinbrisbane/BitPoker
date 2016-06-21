using System;

namespace BitPoker.Models.Messages
{
    public class AddPlayerRequest : BaseMessage
    {
        public PlayerInfo Player { get; set; }

        public AddPlayerRequest()
        {
            base.TimeStamp = DateTime.UtcNow;
            base.Id = Guid.NewGuid();
        }
    }
}
