using System;

namespace BitPoker.Models
{
    public class Log
    {
        public Guid Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public String Type { get; set; }

        public String Message { get; set; }
        
        public override string ToString()
        {
            return string.Format("[Log: Id={0}, TimeStamp={1}, Type={2}, Message={3}]", Id, TimeStamp, Type, Message);
        }
    }
}
