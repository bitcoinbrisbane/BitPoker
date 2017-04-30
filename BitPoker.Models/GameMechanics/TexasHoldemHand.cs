using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
	public class TexasHoldemGame
	{
		public Int16 PlayerToAct { get; private set; }

		private UInt64 _sb;
		private UInt64 _bb;

		public Boolean SmallBlindPosted { get; private set; }
		
		public Boolean BigBlindPosted { get; private set; }
		
		public Boolean CanBet { get; private set; }
		
		public TexasHoldemGame(IPlayer[] players, UInt64 smallBlind, UInt64 bigBlind)
		{
			_sb = smallBlind;
			_bb = bigBlind;

			SmallBlindPosted = false;
			BigBlindPosted = false;
		}
		
		
		public void Add(IEnumerable<Models.Messages.ActionMessage> actions)
		{
		}
	}
}
