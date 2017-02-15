using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IAddAndReadRepository<T> : IReadOnlyRepository<T>
    {
        void Add(T entity);
    }
}
