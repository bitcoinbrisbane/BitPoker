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
            BitPoker.Repository.IPlayerRepository playerRepo = Repository.Factory.GetPlayerRepository();
            BitPoker.Repository.IPlayerRepository mockPlayerRepo = new BitPoker.Repository.MockPlayerRepo();

            foreach(BitPoker.Models.PlayerInfo player in mockPlayerRepo.All())
            {
                playerRepo.Add(player);
            }

            playerRepo.Save();

            BitPoker.Repository.ITableRepository tableRepo = Repository.Factory.GetTableRepository();
            BitPoker.Repository.ITableRepository mockTableRepo = new BitPoker.Repository.MockTableRepo();

            foreach (BitPoker.Models.Contracts.Table player in mockTableRepo.All())
            {
                tableRepo.Add(player);
            }

            tableRepo.Save();

            return "ok";
        }
    }
}
