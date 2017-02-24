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
				//if (String.IsNullOrEmpty(_localBitcoinAddress))
				//{
				//	base._localBitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ";
				//}
				return _localBitcoinAddress;
			}
			set { _localBitcoinAddress = value; }
		}

		public UserAgentController()
		{
			this.Address = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ";
		}
	}
}