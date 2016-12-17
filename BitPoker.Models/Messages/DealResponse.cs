using System;

namespace BitPoker.Models.Messages
{
    public class DealResponse : RPCResponse
    {
        public IDeck Deck { get; set; }

        public DealResponse()
        {
        }
    }
}