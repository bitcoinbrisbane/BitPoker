using System;

namespace BitPoker.Models.Messages
{
    public class NewPeer : BaseRequest, IMessage
    {
        public PlayerInfo Player { get; set; }
    }
}