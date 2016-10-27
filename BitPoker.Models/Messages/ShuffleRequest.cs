using System;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
	public class ShuffleMessage : BaseRequest
	{
		public IEnumerable<String> Cards { get; set; }

		public ShuffleMessage (IEnumerable<String> cards)
		{
			this.Cards = cards;
            base.Version = 1.0M;
        }
	}
}