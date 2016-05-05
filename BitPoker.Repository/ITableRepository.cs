using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface ITableRepository : IDisposable
    {
        IEnumerable<Models.Contracts.Table> All();

        Models.Contracts.Table Find(Guid id);

        void Add(Models.Contracts.Table item);
    }
}
