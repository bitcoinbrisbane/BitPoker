using System;

namespace BitPoker.Models
{
    public class Log
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Type { get; set; }

        public String Message { get; set; }
    }
}
