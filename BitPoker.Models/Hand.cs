using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public class Hand
    {
        public PlayerInfo[] Players { get; set; }

        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        public Int16 PersonToAct { get; set; }

        public Int16 Round { get; set; }

        public IList<Messages.ActionMessage> History { get; set; }

        public IDeck Deck { get; set; }

		public Int64 TimeStamp { get; set; }

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

		public override string ToString()
		{
			return string.Format("{0}-{1}, PersonToAct={3}, History={4}, Deck={5}]", Id, TableId, PersonToAct, History, Deck);
		}
    }
}