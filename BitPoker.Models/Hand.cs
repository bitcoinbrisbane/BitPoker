using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public class Hand
    {
        private BitPoker.Models.PlayerInfo[] _players;

        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public Int32 PersonToAct { get; set; }

        public IList<Messages.ActionMessage> History { get; set; }

        public IDeck Deck { get; set; }

        public Hand(BitPoker.Models.PlayerInfo[] players)
        {
            _players = players;
            Id = new Guid();
            Id = Guid.NewGuid();
            History = new List<Messages.ActionMessage>();

            this.Deck = new FiftyTwoCardDeck();
        }
    }
}