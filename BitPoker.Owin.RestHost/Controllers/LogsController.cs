using System;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogsController : BitPoker.Controllers.Rest.LogsController
    {
    }
}
