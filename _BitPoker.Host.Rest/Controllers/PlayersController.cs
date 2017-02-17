using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Controllers.Rest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayersController : BaseController, IPlayersController
    {
        public Repository.IPlayerRepository PlayerRepo { get; set; }

        public PlayersController()
        {
            PlayerRepo = new Repository.MockPlayerRepo(@"E:\Repos\bitpoker\BitPoker.Repository\mockplayers.json");
        }

        public PlayersController(Repository.IPlayerRepository repo)
        {
            PlayerRepo = repo;
        }

        public IEnumerable<Models.IPlayer> Get()
        {
            return PlayerRepo.All();
        }

        public Models.IPlayer Get(String address)
        {
            Models.IPlayer player = PlayerRepo.Find(address);
            return player;
        }

        [HttpPost]
        public Models.IResponse Post(Models.IRequest model)
        {
            if (model.Method == "AddPlayer")
            {
                Models.Messages.RPCResponse response = new Models.Messages.RPCResponse();
                Models.Messages.AddPlayerRequest request = model.Params as Models.Messages.AddPlayerRequest;

                //need to include timestamp too
                Boolean valid = true; //base.Verify(request.BitcoinAddress, model.Id.ToString(), model.Signature);

                if (valid)
                {
                    //PlayerRepo.Add(request.Player);
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
                return null;
            }
        }
    }
}
