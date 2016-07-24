using System;

namespace BitPoker.MVC.Exceptions
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