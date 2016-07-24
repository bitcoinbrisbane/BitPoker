using System;

namespace BitPoker.MVC.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException()
        {
        }

        public PlayerNotFoundException(string message)
        : base(message)
        {
        }

        public PlayerNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}