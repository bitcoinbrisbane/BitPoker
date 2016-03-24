using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
	public class ShuffleMessage
	{
		public IEnumerable<String> Cards { get; set; }

		public ShuffleMessage (IEnumerable<String> cards)
		{
			this.Cards = cards;
		}
	}
}