using System;

namespace BitPoker.Models.Messages
{
    public class AddTableRequest : BaseRequest
    {
        public Contracts.Table Table { get; set; }

        public AddTableRequest()
        {
        }
    }
}
