using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class AddMocksController : ApiController
    {
        public String Get()
        {
            BitPoker.Repository.IPlayerRepository repo = new BitPoker.Repository.MockPlayerRepo();
            IEnumerable<BitPoker.Models.PlayerInfo> players = BitPoker.API.Repository.Factory.GetPlayerRepository().All();

            foreach(BitPoker.Models.PlayerInfo player in players)
            {
                repo.Add(player);
            }

            return "ok";
        }
    }
}
