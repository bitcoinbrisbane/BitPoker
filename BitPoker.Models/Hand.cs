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

        public Int16 Round { get; set; }

        public IReadOnlyList<Messages.ActionMessage> History { get { return _history; } }

        public IDeck Deck { get; set; }

		public Int64 TimeStamp { get; set; }

        public Guid PreviousHandId { get; set; }

        public String PreviousHandHash { get; set; }

        public Hand()
        {
            this.PlayerToAct = 1;
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
            //validate first
            _history.Add(message);
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5:yyyyMMddHHmmss}", Id, TableId, TimeStamp, PreviousHandId, PreviousHandHash, TimeStamp);
        }
    }
}