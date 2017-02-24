using System;
namespace BitPoker.Controllers.Rest
{
	public class Helpers
	{
		public static Models.IDeck GetNewDeck()
		{
			Models.FiftyTwoCardDeck deck = new Models.FiftyTwoCardDeck();
			deck.Shuffle();

			return deck;
		}
	}
}