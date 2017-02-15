using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IReadOnlyRepository<T>
    {
        IEnumerable<T> All();

        T Find(String id);
    }
}
