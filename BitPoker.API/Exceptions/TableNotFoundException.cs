using System;

namespace BitPoker.API.Exceptions
{
    public class TableNotFoundException : Exception
    {
        public TableNotFoundException()
        {
        }

        public TableNotFoundException(string message)
        : base(message)
        {
        }

        public TableNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}