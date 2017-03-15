using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UserAgentController : BitPoker.Controllers.Rest.UserAgentController
	{
		public UserAgentController()
		{
			//this.Address = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ";

			base.SetPrivateKey(System.Configuration.ConfigurationManager.AppSettings["bitcoinPrivateKey"]);
		}
	}
}