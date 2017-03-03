using System;

namespace BitPoker.Controllers.Rest
{
	public class Helpers
	{
		public static Models.IDeck GetNewDeck(Models.IRandom random)
		{
			Models.FiftyTwoCardDeck deck = new Models.FiftyTwoCardDeck();
			deck.Shuffle(random);

			return deck;
		}
	}
}