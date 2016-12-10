using System;

namespace BitPoker.Models.Messages
{
    public class JoinTableResponse : RPCResponse, IResponse
    {
        public Int32 Seat { get; set; }
    }
}
