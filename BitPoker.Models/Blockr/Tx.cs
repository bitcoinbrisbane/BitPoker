using System;

namespace BitPoker.Models.Blockr
{
    public class Tx
    {
        public String tx { get; set; }

        public Decimal amount { get; set; }

        public DateTime utc_time { get; set; }
    }
}
