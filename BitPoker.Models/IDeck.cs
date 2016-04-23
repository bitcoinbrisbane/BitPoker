using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public interface IDeck
    {
        Boolean IsEncrypted { get; }

        ICollection<Byte[]> Cards { get; }

        void New();

        void Shuffle();

        void Encrypt();
    }
}
