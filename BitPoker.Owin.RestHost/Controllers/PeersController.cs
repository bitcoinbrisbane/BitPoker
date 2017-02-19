using System;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class PeersController : BitPoker.Controllers.Rest.PeersController
	{
		public PeersController()
		{
			base.PeerRepo = new BitPoker.Repository.LiteDB.PeerRepository("bitpoker.db");
		}
	}
}