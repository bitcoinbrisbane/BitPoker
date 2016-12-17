using System;

namespace BitPoker.Models.Messages
{
    public class ShuffleResponse : RPCResponse
    {
        public IDeck Deck { get; set; }

        public ShuffleResponse()
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
