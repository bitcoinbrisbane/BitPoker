using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitPoker.API.Models
{
    public class Hand
    {
        private BitPoker.Models.PlayerInfo[] _players;

        public Guid Id { get; set; }

        public Int32 PersonToAct { get; set; }

        public ICollection<BitPoker.Models.Messages.ActionMessage> History { get; set; }

        public BitPoker.Models.IDeck Deck { get; set; }

        public Hand(BitPoker.Models.PlayerInfo[] players)
        {
            _players = players;
            Id = new Guid();
            Id = Guid.NewGuid();
            History = new List<BitPoker.Models.Messages.ActionMessage>();

            this.Deck = new BitPoker.Models.FiftyTwoCardDeck();
        }
    }
}