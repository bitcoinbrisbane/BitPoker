using System;

namespace BitPoker.Models.Contracts
{
    public class TableRequest
    {
        public String BitcoinAddress { get; set; }

        public String Signature { get; set; }

        public Table Table { get; set; }
    }
}
