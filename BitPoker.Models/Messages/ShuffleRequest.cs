using System;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
	public class ShuffleRequest : BaseRequest
	{
        public Guid HandId { get; set; }

		public IEnumerable<String> Cards { get; set; }

        public ShuffleRequest()
        {
            base.Version = 1.0M;
        }

        public ShuffleRequest (IEnumerable<String> cards)
		{
			this.Cards = cards;
            base.Version = 1.0M;
        }
	}
}