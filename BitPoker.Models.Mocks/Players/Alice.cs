using System;
namespace BitPoker.Models.Mocks
{
	public class Alice : IPlayer
	{
		public string BitcoinAddress { get; set; }

		public string IPAddress { get; set; }

		public bool IsBigBlind { get; set; }

		public bool IsDealer { get; set; }

		public bool IsSmallBlind { get; set; }

		public bool IsTurnToAct { get; set; }

		public short Position { get; set; }

		public ulong Stack { get; set; }

		public Alice()
		{
		}
	}
}
