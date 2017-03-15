using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class BuyInController : BitPoker.Controllers.Rest.BuyInController
	{
		public BuyInController()
		{
			base.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
		}
	}
}