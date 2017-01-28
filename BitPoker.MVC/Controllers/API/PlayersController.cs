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

        public IEnumerable<BitPoker.Models.IPlayer> Get()
        {
            return _repo.All();
        }

        public BitPoker.Models.IPlayer Get(String address)
        {
            BitPoker.Models.IPlayer player = _repo.Find(address);
            return player;
        }

        [HttpPost]
        public BitPoker.Models.IResponse Post(BitPoker.Models.IRequest model)
        {
            BitPoker.Models.Messages.RPCResponse response = new BitPoker.Models.Messages.RPCResponse()
            {
                Id = model.Id
            };

            if (model.Method == "AddPlayer")
            {
                BitPoker.Models.Messages.AddPlayerRequest request = model.Params as BitPoker.Models.Messages.AddPlayerRequest;

                //need to include timestamp too
                Boolean valid = base.Verify(request.BitcoinAddress, model.Id.ToString(), model.Signature);

                if (valid)
                {
                    _repo.Add(request.Player);
                    return response;
                }
                else
                {
                    response.Error = "invalid siganture";
                    return response;
                }
            }
            else
            {
                response.Error = "invalid method";
                return response;
            }
        }
    }
}
