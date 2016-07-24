using System;
using System.Collections.Generic;

namespace BitPoker.Models.Contracts
{
	public class Table : BaseTable
	{
        public IDeck Deck { get; set; }

        public IList<TexasHoldemPlayer> Players { get; set; }

        public Table()
        {
<<<<<<< HEAD
            this.Players = new List<TexasHoldemPlayer>(10);
=======
            this.Players = new List<TexasHoldemPlayer>();
>>>>>>> 0c47c763d3781f3289f1e5806ed85e60441042fd
        }

		public Table (Int16 minPlayers, Int16 maxPlayers)
		{
			this.Id = new Guid ();
            this.MinPlayers = minPlayers;
            this.MaxPlayers = maxPlayers;

            this.Players = new List<TexasHoldemPlayer>(maxPlayers);

            //this.Players[0].IsDealer = true;
            //this.Players[1].IsSmallBlind = true;
		}
	}
}