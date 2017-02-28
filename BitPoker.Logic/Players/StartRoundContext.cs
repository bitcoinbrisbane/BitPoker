using System.Collections.Generic;
using BitPoker.Models;
using BitPoker.Models.Cards;
using System;

namespace BitPoker.Logic.Players
{
	public class StartRoundContext : IStartRoundContext
    {
        public StartRoundContext(GameRoundType roundType, IReadOnlyCollection<Card> communityCards, Int64 moneyLeft, Int64 currentPot)
        {
            this.RoundType = roundType;
            this.CommunityCards = communityCards;
            this.MoneyLeft = moneyLeft;
            this.CurrentPot = currentPot;
        }

        public GameRoundType RoundType { get; }

        public IReadOnlyCollection<Card> CommunityCards { get; }

        public Int64 MoneyLeft { get; }

		public Int64 CurrentPot { get; }
    }
}
