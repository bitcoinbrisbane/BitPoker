using System;

namespace BitPoker.Models
{
	public class TexasHoldemPlayer : IPlayer
	{
		//Texas holdem properties
		public Int16 Position { get; set; }

		public Boolean IsDealer { get; set; }

		public Boolean IsSmallBlind { get; set; }

		public Boolean IsBigBlind { get; set; }

		public Boolean IsTurnToAct { get; set; }

		//public ICollection<BitPoker.Models.IMessage> AllowedActions { get; set; }

		public String IPAddress { get; set; }

		public String BitcoinAddress { get; set; }

		public UInt64 Stack { get; set; }

		public TexasHoldemPlayer()
		{
		}
	}
}