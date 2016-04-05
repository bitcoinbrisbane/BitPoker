using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    public class BuyInRequestMessage
    {
        public Int64 Amount { get; set; }

        public String PubKey { get; set; }
    }
}
