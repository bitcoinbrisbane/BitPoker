using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public class Hand
    {
        private List<Messages.ActionMessage> _history;

        public PlayerInfo[] Players { get; private set; }

        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public Int16 PlayerToAct { get; private set; }

        public Int16 Dealer { get; private set; }

        public IReadOnlyList<Messages.ActionMessage> History { get { return _history; } }

        public IDeck Deck { get; set; }

        public Hand()
        {
            this.PlayerToAct = 1;
            this.Dealer = 0;
            Id = Guid.NewGuid();
            _history = new List<Messages.ActionMessage>();

            this.Deck = new FiftyTwoCardDeck();
        }

        public Hand(PlayerInfo[] players)
        {
            this.Players = players;
            this.PlayerToAct = 1;
            Id = Guid.NewGuid();
            _history = new List<Messages.ActionMessage>();

            this.Deck = new FiftyTwoCardDeck();
        }

        public Boolean AddMessage(Messages.ActionMessage message)
        {
            _history.Add(message);

            return true;
        }
    }
}