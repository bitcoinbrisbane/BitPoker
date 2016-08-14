using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public class Hand
    {
        public BitPoker.Models.PlayerInfo[] Players { get; set; }

        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public Int16 PersonToAct { get; set; }

        public Int16 Round { get; set; }

        public IList<Messages.ActionMessage> History { get; set; }

        public IDeck Deck { get; set; }

        public Hand()
        {
        }

        public Hand(PlayerInfo[] players)
        {
            this.Players = players;
            this.PersonToAct = 0;
            Id = Guid.NewGuid();
            History = new List<Messages.ActionMessage>();

            this.Deck = new FiftyTwoCardDeck();
        }
    }
}