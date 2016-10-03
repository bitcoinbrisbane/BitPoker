using System;
using System.Collections.Generic;

namespace BitPoker.Models.Contracts
{
	public class Table : BaseTable
	{
        /// <summary>
        /// Array of players in their seats
        /// </summary>
        public IList<TexasHoldemPlayer> Players { get; private set; }

        public Table()
        {
            this.Players = new List<TexasHoldemPlayer>(10);
            this.HashAlgorithm = "SHA256";
        }

		public Table (Int16 minPlayers, Int16 maxPlayers)
		{
			this.Id = new Guid ();
            this.MinPlayers = minPlayers;
            this.MaxPlayers = maxPlayers;

            this.Players = new List<TexasHoldemPlayer>(maxPlayers);
		}
	}
}