using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    /// <summary>
    /// Peer
    /// </summary>
	public class Peer
	{
		public String UserAgent { get; set; }

        /// <summary>
        /// ID
        /// </summary>
		public String BitcoinAddress { get; set; }

        public String PublicKey { get; set; }

		public String NetworkAddress { get; set; }

		public TimeSpan Latency { get; set; }

		public DateTime LastSeen { get; set; }

		public Peer ()
		{
		}

        public override string ToString()
        {
            return String.Format("{0} User Agent {1}, IP Address {2}", BitcoinAddress, UserAgent, NetworkAddress);
        }
    }
}