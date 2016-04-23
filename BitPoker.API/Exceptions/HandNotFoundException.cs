using System;

namespace BitPoker.API.Exceptions
{
    public class HandNotFoundException : Exception
    {
        public HandNotFoundException()
        {
        }

        public HandNotFoundException(string message)
        : base(message)
        {
        }

        public HandNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}