using System;

namespace BitPokerMobile.PCL.Models
{
	public class PlayerInfo
	{
		public String UserAgent { get; set; }

		public String BitcoinAddress { get; set; }

		public String PublicKey { get; set; }

		//public Int64 Stack { get; set; }

		public String IPAddress { get; set; }

		public TimeSpan Latency { get; set; }

		public DateTime LastSeen { get; set; }

		public PlayerInfo()
		{
		}
	}
}