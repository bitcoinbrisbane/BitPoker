using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IHandRepository : IRepository, IDisposable
    {
        IEnumerable<Models.Hand> All();

        void Add(Models.Hand entity);

        Models.Hand Find(Guid id);

        void Update(Models.Hand entity);
    }
}
