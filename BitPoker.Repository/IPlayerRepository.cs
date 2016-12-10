using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IPlayerRepository : IDisposable, IRepository
    {
        IEnumerable<Models.Peer> All();

        Models.Peer Find(String address);

        void Add(Models.Peer entity);
    }
}
