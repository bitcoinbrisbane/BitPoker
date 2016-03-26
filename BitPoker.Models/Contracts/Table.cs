using System;
using System.Collections.Generic;

namespace BitPoker.Models.Contracts
{
	public class Table
	{
		public Guid Id { get; set; }

		public Int64 SmallBlind { get; set; }

		public Int64 BigBlind { get; set; }

		public Int16 MaxPlayers { get; set; }

		public Int16 MinPlayers { get; set; }

        public ICollection<Byte[]> Deck { get; set; }

		public Table ()
		{
			this.Id = new Guid ();
            this.Deck = new List<Byte[]>();
		}
	}
}