using System;

namespace BitPoker.Models.Messages
{
	public class UserAgentResponse : BaseResponse
	{
		public String Agent { get; set; }

		public Decimal Version { get; set; }

		public String BitcoinAddress { get; set; }

		public TimeSpan UpTime { get; set; }

		public TimeSpan LastSeen { get; set; }

		public UserAgentResponse()
		{
		}
	}
}