using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class JoinController : BitPoker.Controllers.Rest.JoinController
	{
		public JoinController()
		{
			base.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
		}
	}
}