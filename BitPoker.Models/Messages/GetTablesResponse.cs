using System;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
    public class GetTablesResponse : BaseRequest, IMessage
    {
        public IEnumerable<Contracts.Table> Tables { get; set; }
    }
}
