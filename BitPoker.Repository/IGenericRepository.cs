using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> All();

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        T Find(String id);
    }
}
