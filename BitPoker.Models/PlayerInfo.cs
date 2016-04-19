using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
	public class PlayerInfo
	{
		public String UserAgent { get; set; }

		public String BitcoinAddress { get; set; }

        public String PublicKey { get; set; }

        public Int64 Stack { get; set; }

		public String Address { get; set; }

		public Int32 Latency { get; set; }

		public DateTime LastSeen { get; set; }

        public IEnumerable<Guid> Tables { get; set; }

		public PlayerInfo ()
		{
		}
	}
}