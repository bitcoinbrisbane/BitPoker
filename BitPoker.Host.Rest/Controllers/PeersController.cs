using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Controllers.Rest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PeersController : BaseController //, IPeersController
    {
        public Repository.IGenericRepository<Models.Peer> PeerRepo { get; set; }

        public PeersController()
        {
            //PlayerRepo = new Repository.MockPlayerRepo(@"E:\Repos\bitpoker\BitPoker.Repository\mockplayers.json");
            //PlayerRepo.Add(new Models.Peer() { IPAddress = "https://www.bitpoker.io/api/alice" });

            PeerRepo = new Repository.MockPeerRepo();
        }

        public PeersController(Repository.IGenericRepository<Models.Peer> repo)
        {
            PeerRepo = repo;
        }

        public IEnumerable<Models.Peer> Get()
        {
            AddLog("Get peers");
            return PeerRepo.All();
        }

        public Models.Peer Get(String address)
        {
            AddLog("Get peer");
            Models.Peer player = PeerRepo.Find(address);
            return player;
        }

        //[HttpPost]
        //public Models.IResponse Post(Models.IRequest model)
        //{
        //    if (model.Method == "AddPeer")
        //    {
        //        Models.Messages.RPCResponse response = new Models.Messages.RPCResponse();
        //        Models.Messages.AddPeerRequest request = model.Params as Models.Messages.AddPeerRequest;

        //        //need to include timestamp too
        //        Boolean valid = base.Verify(request.BitcoinAddress, model.Id.ToString(), model.Signature);

        //        if (valid)
        //        {
        //            PeerRepo.Add(request.Peer);
        //            return response;
        //        }
        //        else
        //        {
        //            response.Error = "invalid siganture";
        //            return response;
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
