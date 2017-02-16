using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BitPoker.Net.RestHost.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [EnableCors("AllowSpecificOrigin")]
    public class PlayersController : BaseController, IPlayersController
    {
        public BitPoker.Repository.IPlayerRepository PlayerRepo { get; set; }

        public PlayersController()
        {
            PlayerRepo = new BitPoker.Repository.MockPlayerRepo(@"E:\Repos\bitpoker\BitPoker.Repository\mockplayers.json");
        }

        public PlayersController(BitPoker.Repository.IPlayerRepository repo)
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
