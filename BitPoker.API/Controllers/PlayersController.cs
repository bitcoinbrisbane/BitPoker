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
        public IEnumerable<BitPoker.Models.PlayerInfo> Get()
        {
            BitPoker.Models.PlayerInfo alice = new BitPoker.Models.PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
            };

            BitPoker.Models.PlayerInfo bob = new BitPoker.Models.PlayerInfo()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo"
            };

            List<BitPoker.Models.PlayerInfo> mockPlayers = new List<BitPoker.Models.PlayerInfo>();
            mockPlayers.Add(alice);
            mockPlayers.Add(bob);

            return mockPlayers;
        }

        public BitPoker.Models.PlayerInfo Get(String address)
        {
            BitPoker.Models.PlayerInfo mock = new BitPoker.Models.PlayerInfo()
            {
                BitcoinAddress = address
            };

            return mock;
        }
    }
}
