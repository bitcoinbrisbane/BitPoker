using System;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
	public class UserAgentController : BaseController
	{
		[HttpGet]
		public Models.Messages.UserAgentResponse Get()
		{
			return new Models.Messages.UserAgentResponse() 
			{ 
				Agent = "BitPoker c#",
				Version = 0.1M,
				TimeStamp = DateTime.UtcNow,
				BitcoinAddress = _localBitcoinAddress,
				UpTime = DateTime.UtcNow - base.StartTime,
				LastSeen = DateTime.Now - base.LastRequest
			};
		}
	}
}