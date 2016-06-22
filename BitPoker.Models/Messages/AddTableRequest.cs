using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    public class AddTableRequest : BaseMessage
    {
        public Models.Contracts.Table Table { get; set; }

        public AddTableRequest()
        {
            base.TimeStamp = DateTime.UtcNow;
            base.Id = Guid.NewGuid();
        }
    }
}
