namespace BitPoker.Logic.Helpers
{
    using System.Collections.Generic;

    using BitPoker.Models.Cards;

    public interface IHandEvaluator
    {
        BestHand GetBestHand(IEnumerable<Card> cards);
    }
}