using BitPoker.Models.Cards;
using System;

namespace BitPoker.Logic.Players
{
    public class StartHandContext : IStartHandContext
    {
        public StartHandContext(Card firstCard, Card secondCard, int handNumber, int moneyLeft, int smallBlind, string firstPlayerName)
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
