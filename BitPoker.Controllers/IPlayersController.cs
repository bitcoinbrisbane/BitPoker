using System.Collections.Generic;
using BitPoker.Models;
using BitPoker.Repository;
using System;

namespace BitPoker.Controllers
{
    public interface IPlayersController
    {
        IPlayerRepository PlayerRepo { get; set; }

        IEnumerable<Peer> Get();

        Peer Get(string address);

        Hand Get(string address, Guid handId);

        IResponse Post(IRequest model);
    }
}