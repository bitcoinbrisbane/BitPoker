using System;

namespace BitPoker.Models.Messages
{
    public class DealResponse : BaseResponse
    {
        public IDeck Deck { get; set; }

        public DealResponse()
        {
        }
    }
}