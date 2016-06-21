using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    /// <summary>
    /// Player not in a game perhaps
    /// </summary>
	public class PlayerInfo
	{
		public String UserAgent { get; set; }

		public String BitcoinAddress { get; set; }

        public String PublicKey { get; set; }

        //public Int64 Stack { get; set; }

		public String IPAddress { get; set; }

		public TimeSpan Latency { get; set; }

		public DateTime LastSeen { get; set; }

        //public IEnumerable<Guid> Tables { get; set; }

		public PlayerInfo ()
		{
		}
	}
}