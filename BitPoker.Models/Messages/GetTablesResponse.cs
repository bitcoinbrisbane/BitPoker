using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    public class GetTablesResponse : BaseRequest, IMessage
    {
        public IEnumerable<Models.Contracts.Table> Tables { get; set; }
    }
}
