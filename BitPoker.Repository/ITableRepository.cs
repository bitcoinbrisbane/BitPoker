using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface ITableRepository
    {
        IEnumerable<Models.Contracts.Table> All();

        Models.Contracts.Table Find(Guid id);
    }
}
