using BitPoker.Models.Cards;
using System;
using BitPoker.Models.GameContext;

namespace BitPoker.Logic.Players
{
    public class StartHandContext : IStartHandContext
    {
        public StartHandContext(Card firstCard, Card secondCard, Int64 handNumber, Int64 moneyLeft, Int64 smallBlind, string firstPlayerName)
        {
            this.FirstCard = firstCard;
            this.SecondCard = secondCard;
            this.HandNumber = handNumber;
            this.MoneyLeft = moneyLeft;
            this.SmallBlind = smallBlind;
            this.FirstPlayerName = firstPlayerName;
        }

        public Card FirstCard { get; }

        public Card SecondCard { get; }

        public Int64 HandNumber { get; }

        public Int64 MoneyLeft { get; }

        public Int64 SmallBlind { get; }

		[Obsolete]
        public string FirstPlayerName { get; }
    }
}
