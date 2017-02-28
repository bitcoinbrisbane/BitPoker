using System;
using System.Collections.Generic;
using BitPoker.Models;

namespace BitPoker.Models.GameContext
{
	public interface IStartRoundContext
	{
		GameRoundType RoundType { get; }

		IReadOnlyCollection<BitPoker.Models.Cards.Card> CommunityCards { get; }

		Int64 MoneyLeft { get; }

		Int64 CurrentPot { get; }
	}
}
