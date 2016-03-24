using System;

namespace BitPoker.Models
{
	public class PlayerInfo
	{
		public String UserAgent { get; set; }

		public String BitcoinAddress { get; set; }

		public String IPAddress { get; set; }

		public Int32 Latency { get; set; }

		public DateTime LastSeen { get; set; }

		public PlayerInfo ()
		{
		}
	}
}