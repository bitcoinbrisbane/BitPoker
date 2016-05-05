using System;

namespace BitPoker.API.Exceptions
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