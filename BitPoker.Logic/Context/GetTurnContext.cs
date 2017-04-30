using System;
using System.Collections.Generic;
using BitPoker.Models;
using BitPoker.Models.GameContext;
using BitPoker.Models.Players;

namespace BitPoker.Logic.Players
{
	public class GetTurnContext : IGetTurnContext
    {
        public GetTurnContext(
            GameRoundType roundType,
            IReadOnlyCollection<PlayerActionAndName> previousRoundActions,
            Int64 smallBlind,
            Int64 moneyLeft,
            Int64 currentPot,
            Int64 myMoneyInTheRound,
            Int64 currentMaxBet)
        {
            this.RoundType = roundType;
            this.PreviousRoundActions = previousRoundActions;
            this.SmallBlind = smallBlind;
            this.MoneyLeft = moneyLeft;
            this.CurrentPot = currentPot;
            this.MyMoneyInTheRound = myMoneyInTheRound;
            this.CurrentMaxBet = currentMaxBet;
        }

        public GameRoundType RoundType { get; }

        public IReadOnlyCollection<PlayerActionAndName> PreviousRoundActions { get; }

        public Int64 SmallBlind { get; }

        public Int64 MoneyLeft { get; }

        public Int64 CurrentPot { get; }

        public Int64 MyMoneyInTheRound { get; }

        public Int64 CurrentMaxBet { get; }

        public bool CanCheck => this.MyMoneyInTheRound == this.CurrentMaxBet;

        public Int64 MoneyToCall => this.CurrentMaxBet - this.MyMoneyInTheRound;

        public bool IsAllIn => this.MoneyLeft <= 0;
    }
}
