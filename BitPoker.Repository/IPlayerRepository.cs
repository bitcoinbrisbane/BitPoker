using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IPlayerRepository
    {
        IEnumerable<BitPoker.Models.PlayerInfo> All();

        BitPoker.Models.PlayerInfo Find(String address);
    }
}
