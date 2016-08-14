using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.MVC.Controllers.API
{
    public class AliceController : ApiController
    {
        readonly BitPoker.Repository.IPlayerRepository playerRepo;

        public AliceController()
        {
            playerRepo = new BitPoker.Repository.MockPlayerRepo();
        }

        public BitPoker.Models.PlayerInfo Get()
        {
            return playerRepo.Find("msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv");
        }

        [HttpPost]
        public BitPoker.Models.Messages.ActionMessage Post(BitPoker.Models.Messages.ActionMessage message)
        {
            if (!String.IsNullOrEmpty(message.Signature))
            {
                switch (message.Action.ToUpper())
                {
                    case "SMALL BLIND":
                        break;
                    case "CALL":
                        break;
                }
            }

            throw new NotImplementedException();
        }
    }
}
