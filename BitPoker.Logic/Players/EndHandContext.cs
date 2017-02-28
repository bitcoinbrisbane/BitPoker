namespace BitPoker.Logic.Players
{
    using System.Collections.Generic;
    using BitPoker.Models.Cards;

    public class EndHandContext
    {
        public EndHandContext(Dictionary<string, ICollection<Card>> showdownCards)
        {
            this.ShowdownCards = showdownCards;
        }

        public Dictionary<string, ICollection<Card>> ShowdownCards { get; private set; }
    }
}