using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface ITableRepository : IDisposable
    {
        IEnumerable<BitPoker.Models.Contracts.Table> All();

        BitPoker.Models.Contracts.Table Find(Guid id);

        void Add(BitPoker.Models.Contracts.Table entity);

        void Update(BitPoker.Models.Contracts.Table entity);

        void Save();
    }
}
