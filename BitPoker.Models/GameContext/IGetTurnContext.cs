using System;
using System.Collections.Generic;
using BitPoker.Models.Players;

namespace BitPoker.Models.GameContext
{
	public interface IGetTurnContext
	{
        GameRoundType RoundType { get; }

		IReadOnlyCollection<PlayerActionAndName> PreviousRoundActions { get; }

		Int64 SmallBlind { get; }

		Int64 MoneyLeft { get; }

		Int64 CurrentPot { get; }

		Int64 MyMoneyInTheRound { get; }

		Int64 CurrentMaxBet { get; }

		bool CanCheck { get; }

		Int64 MoneyToCall { get; }

		bool IsAllIn { get; }
	}
}
