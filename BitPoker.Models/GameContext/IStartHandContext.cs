using System;
using BitPoker.Models.Cards;

namespace BitPoker.Models.GameContext
{
	public interface IStartHandContext
	{
		Card FirstCard { get; }

		Card SecondCard { get; }

		Int64 HandNumber { get; }

		Int64 MoneyLeft { get; }

		Int64 SmallBlind { get; }

		[Obsolete]
		String FirstPlayerName { get; }
	}
}
