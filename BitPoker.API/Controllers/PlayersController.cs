using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class PlayersController : BaseController
    {
        private readonly BitPoker.Repository.IPlayerRepository repo;

        public PlayersController()
        {
            repo = Repository.Factory.GetPlayerRepository();
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

        [HttpPost]
        public String Post(BitPoker.Models.Messages.AddPlayerRequest model)
        {
            if (model.BitcoinAddress == model.Player.BitcoinAddress)
            {
                //need to include timestamp too
                Boolean valid = base.Verify(model.BitcoinAddress, model.Id.ToString(), model.Signature);

                if (valid)
                {
                    repo.Add(model.Player);
                    return "ok";
                }
                else
                {
                    return "invalid";
                }
            }
            else
            {
                return "addresses do not match";
            }
        }
    }
}
