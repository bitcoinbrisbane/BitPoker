using System;
using System.Collections.Generic;
using System.Linq;

namespace BitPoker.Models
{
    public class Hand : IHand
    {
        //private List<Messages.ActionMessage> _history;

        public IPlayer[] Players { get; set; }

        /// <summary>
        /// Hand number of the table
        /// </summary>
        public Int64 Index { get; set; }

        [Obsolete("Use index")]
        public Guid Id { get; set; }

        public Guid TableId { get; set; }

        //public Int16 PlayerToAct { get; set; }

        //public IReadOnlyList<Messages.ActionMessage> History { get { return _history; } }

        public IDeck Deck { get; set; }

	public Int64 TimeStamp { get; set; }

        public Guid PreviousHandId { get; set; }

        public String PreviousHandHash { get; set; }

		//public ICollection<String> AllowedActions { get; set; }

   //     public Hand(Guid tableId, IPlayer[] players)
   //     {
			//this.TableId = tableId;
   //         this.Players = players;
   //         this.PlayerToAct = 1;
   //         Id = Guid.NewGuid();

   //         _history = new List<Messages.ActionMessage>();

   //         this.Deck = new FiftyTwoCardDeck();
   //     }

   //     public Hand(Guid tableId, IPlayer[] players, String previousHandHash, Guid previousHandId)
   //     {
			//this.TableId = tableId;
   //         this.Players = players;
   //         this.PlayerToAct = 0; //dealer
   //         Id = Guid.NewGuid();

			//this.PreviousHandHash = previousHandHash;
			//this.PreviousHandId = previousHandId;

   //         _history = new List<Messages.ActionMessage>();

   //         this.Deck = new FiftyTwoCardDeck();
   //     }

   //     public Boolean AddMessage(Messages.ActionMessage message)
   //     {
			//var lastAction = this.History.OrderByDescending(h => h.Index).Last();

			////if (Players.Where(p => p.BitcoinAddress == message.BitcoinAddress)
			////{
				
			////}
			    
   //         _history.Add(message);
   //         return true;
   //     }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5:yyyyMMddHHmmss}", Id, TableId, TimeStamp, PreviousHandId, PreviousHandHash, TimeStamp);
        }
    }
}