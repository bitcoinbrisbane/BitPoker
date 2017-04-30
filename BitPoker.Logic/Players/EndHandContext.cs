using System.Collections.Generic;
using BitPoker.Models.Cards;
using BitPoker.Models.GameContext;

namespace BitPoker.Logic.Players
{
    public class EndHandContext : IEndHandContext
    {
        public EndHandContext(Dictionary<string, ICollection<Card>> showdownCards)
        {
            this.ShowdownCards = showdownCards;
        }

        public Dictionary<string, ICollection<Card>> ShowdownCards { get; private set; }
    }
}