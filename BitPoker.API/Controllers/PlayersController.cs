using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly BitPoker.Repository.IPlayerRepository repo;

        public PlayersController()
        {
            repo = new BitPoker.Repository.MockPlayerRepo();
        }

        public IEnumerable<BitPoker.Models.PlayerInfo> Get()
        {
            return repo.All();
        }

        public BitPoker.Models.PlayerInfo Get(String address)
        {
            BitPoker.Models.PlayerInfo player = repo.Find(address);
            return player;
        }
    }
}
