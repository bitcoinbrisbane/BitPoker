using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IPlayerRepository : IDisposable, IRepository
    {
        IEnumerable<Models.IPlayer> All();

        Models.IPlayer Find(String address);

        void Add(Models.IPlayer entity);
    }
}
