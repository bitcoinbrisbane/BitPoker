using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    public class DeckRequestMessage : BaseMessage
    {
        public Guid TableId { get; set; }

        public Guid HandId { get; set; }
    }
}
