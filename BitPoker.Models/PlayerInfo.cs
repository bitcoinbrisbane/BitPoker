using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    /// <summary>
    /// Player DTO
    /// </summary>
	public class PlayerInfo
	{
		public String UserAgent { get; set; }

		public String BitcoinAddress { get; set; }

        public String PublicKey { get; set; }

		public String IPAddress { get; set; }

		public TimeSpan Latency { get; set; }

		public DateTime LastSeen { get; set; }

        //public IEnumerable<Guid> Tables { get; set; }

		public PlayerInfo ()
		{
		}
	}
}