using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayersController : BitPoker.Controllers.Rest.PlayersController
    {
        public PlayersController()
        {
            base.PlayerRepo = new BitPoker.Repository.LiteDB.PlayerRepository("bitpoker.db");
        }
    }
}
