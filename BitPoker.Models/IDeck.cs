using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public interface IDeck
    {
        Boolean IsEncrypted { get; }

        IList<Byte[]> Cards { get; }

        void New();

        void Shuffle(IRandom random);

        void Encrypt();
    }
}
