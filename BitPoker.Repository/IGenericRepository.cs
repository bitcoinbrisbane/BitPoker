using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IGenericRepository<T> : IReadOnlyRepository<T>
    {
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
