using System;

namespace BitPoker.MVC.Exceptions
{
    public class SignatureNotValidException : Exception
    {
        public SignatureNotValidException()
        {
        }

        public SignatureNotValidException(string message)
        : base(message)
        {
        }

        public SignatureNotValidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}