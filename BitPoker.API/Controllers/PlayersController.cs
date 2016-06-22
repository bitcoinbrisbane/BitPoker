using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.API.Controllers
{
    [EnableCors(origins: "", headers: "*", methods: "*")]
    public class PlayersController : BaseController
    {
        private readonly BitPoker.Repository.IPlayerRepository _repo;

        public PlayersController()
        {
            _repo = Repository.Factory.GetPlayerRepository();
        }

        public PlayersController(BitPoker.Repository.IPlayerRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<BitPoker.Models.PlayerInfo> Get()
        {
            return _repo.All();
        }

        public BitPoker.Models.PlayerInfo Get(String address)
        {
            BitPoker.Models.PlayerInfo player = _repo.Find(address);
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
                    _repo.Add(model.Player);
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
