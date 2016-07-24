using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    public class MocksController : ApiController
    {
        public String Get()
        {
            BitPoker.Repository.IPlayerRepository repo = Repository.Factory.GetPlayerRepository();
            BitPoker.Repository.IPlayerRepository mocksRepo = new BitPoker.Repository.MockPlayerRepo();
            //IEnumerable<BitPoker.Models.PlayerInfo> players = 

            foreach(BitPoker.Models.PlayerInfo player in mocksRepo.All())
            {
                repo.Add(player);
            }

            return "ok";
        }
    }
}
