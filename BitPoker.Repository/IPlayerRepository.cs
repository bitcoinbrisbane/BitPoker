using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IPlayerRepository : IDisposable, IRepository
    {
        IEnumerable<Models.PlayerInfo> All();

        Models.PlayerInfo Find(String address);

        void Add(Models.PlayerInfo entity);
    }
}
