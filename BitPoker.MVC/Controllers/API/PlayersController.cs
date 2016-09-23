using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.MVC.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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

        /// <summary>
        /// Poll for next action
        /// </summary>
        /// <param name="address"></param>
        /// <param name="handId"></param>
        /// <returns></returns>
        public BitPoker.Models.Hand Get(String address, String handId)
        {
            BitPoker.Models.PlayerInfo player = _repo.Find(address);

            //Do actions


            return new BitPoker.Models.Hand();
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

        [HttpPost]
        public String Post(String id, BitPoker.Models.Messages.ActionMessage model)
        {
            return "ok";
        }
    }
}
