using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Controllers.Rest
{
    //[EnableCors("*", "*", "*")]
    public class PeersController : BaseController
    {
        public BitPoker.Repository.IGenericRepository<Models.Peer> PeerRepo { get; set; }

        public PeersController()
        {
            PeerRepo = new BitPoker.Repository.Mocks.PeerRepository();
        }

        [HttpGet]
        public IEnumerable<Models.Peer> Get()
        {
            AddLog("Get peers");
            return PeerRepo.All();
        }

        [HttpGet]
        public Models.Peer Get(String address)
        {
            AddLog("Get peer");
            Models.Peer player = PeerRepo.Find(address);
            return player;
        }

		//Todo: Authorization
		[HttpPost]
		public void Post(Models.Messages.AddPeerRequest request)
		{
			if (request != null)
			{
				if (base.Verify(request))
				{
					Models.Peer peer = new Models.Peer()
					{
						NetworkAddress = request.NetworkAddress
					};

					PeerRepo.Add(peer);
				}
			}
		}
    }
}
