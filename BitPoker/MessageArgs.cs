using System;

namespace BitPoker
{
    public class MessageArgs : EventArgs
    {
        public DateTime TimeStamp { get; private set; }

        public String Message { get; set; }

        public MessageArgs()
        {
            this.TimeStamp = DateTime.UtcNow;
        }
    }
}
