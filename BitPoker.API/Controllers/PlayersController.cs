using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class PlayersController : ApiController
    {
        public IEnumerable<Models.PlayerInfo> Get()
        {
            Models.PlayerInfo alice = new Models.PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
            };

            Models.PlayerInfo bob = new Models.PlayerInfo()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo"
            };

            List<Models.PlayerInfo> mockPlayers = new List<Models.PlayerInfo>();
            mockPlayers.Add(alice);
            mockPlayers.Add(bob);

            return mockPlayers;
        }

        public Models.PlayerInfo Get(String address)
        {
            Models.PlayerInfo mock = new Models.PlayerInfo()
            {
                BitcoinAddress = address
            };

            return mock;
        }
    }
}
