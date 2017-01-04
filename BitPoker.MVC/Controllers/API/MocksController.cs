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
        /// <summary>
        /// Add mock repo to in memory repo
        /// </summary>
        /// <returns></returns>
        public String Get()
        {
            BitPoker.Repository.IPlayerRepository playerRepo = Repository.Factory.GetPlayerRepository();
            BitPoker.Repository.IPlayerRepository mockPlayerRepo = new BitPoker.Repository.MockPlayerRepo();

            Int32 playerCount = 0;
            foreach(BitPoker.Models.Peer player in mockPlayerRepo.All())
            {
                playerRepo.Add(player);
                playerCount++;
            }

            playerRepo.Save();

            BitPoker.Repository.ITableRepository tableRepo = Repository.Factory.GetTableRepository();
            BitPoker.Repository.ITableRepository mockTableRepo = new BitPoker.Repository.MockTableRepo();

            Int32 tableCount = 0;
            foreach (BitPoker.Models.Contracts.Table table in mockTableRepo.All())
            {
                tableRepo.Add(table);
                tableCount++;
            }

            tableRepo.Save();

            return String.Format("{0} players added, {1} tables added", playerCount, tableCount);
        }
    }
}
