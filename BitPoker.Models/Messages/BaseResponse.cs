using System;

namespace BitPoker.Models.Messages
{
    public class BaseResponse
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Status { get; set; }
    }
}
