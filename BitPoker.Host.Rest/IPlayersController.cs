using System.Collections.Generic;
using BitPoker.Models;
using BitPoker.Repository;
using System;

namespace BitPoker.Controllers
{
    public interface IPlayersController
    {
        IPlayerRepository PlayerRepo { get; set; }

        IEnumerable<IPlayer> Get();

        IPlayer Get(string address);
    }
}