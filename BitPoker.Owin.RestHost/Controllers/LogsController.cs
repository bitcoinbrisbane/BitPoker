using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost.Controllers
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogsController : BitPoker.Controllers.Rest.LogsController
    {
		public LogsController()
		{
			base.LogRepo = new Repository.InMemoryLogRepository(1000);
		}
    }
}