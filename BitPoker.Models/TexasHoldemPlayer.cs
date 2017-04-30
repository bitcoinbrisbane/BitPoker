using System;
using BitPoker.Models.GameContext;
using BitPoker.Models.Players;

namespace BitPoker.Models
{
	public class TexasHoldemPlayer : BasePlayer, BitPoker.Models.IPlayer, IPlayerLogic
	{
		//Texas holdem properties
		public Int16 Position { get; set; }

		public Boolean IsDealer { get; set; }

		public Boolean IsSmallBlind { get; set; }

		public Boolean IsBigBlind { get; set; }

		//public Boolean IsTurnToAct { get; set; }

		//public ICollection<BitPoker.Models.IMessage> AllowedActions { get; set; }

		public String IPAddress { get; set; }

		public String BitcoinAddress { get; set; }

		public UInt64 Stack { get; set; }
		
		public override string Name { get; }

		public TexasHoldemPlayer()
		{
		}
		
		public TexasHoldemPlayer(String bitcoinAddress)
		{
			this.Name = bitcoinAddress;
		}
		
		public override PlayerAction GetTurn(IGetTurnContext context)
		{
			throw new NotImplementedException();
		}

	}
}