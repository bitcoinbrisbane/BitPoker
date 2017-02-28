using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UserAgentController : BitPoker.Controllers.Rest.UserAgentController
	{
		/// <summary>
		/// My address to validate my requests
		/// </summary>
		/// <value>The address.</value>
		public String Address
		{
			get
			{
				return base.LocalBitcoinAddress;
			}
		}

		public UserAgentController()
		{
			//this.Address = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ";
		}
	}
}