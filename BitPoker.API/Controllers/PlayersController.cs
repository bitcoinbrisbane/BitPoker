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
            List<Models.PlayerInfo> mockPlayers = new List<Models.PlayerInfo>();

            return mockPlayers;
        }
    }
}
