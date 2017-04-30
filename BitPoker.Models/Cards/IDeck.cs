using BitPoker.Models.Cards;

namespace BitPoker.Logic.Cards
{
    public interface IDeck
    {
        Card GetNextCard();
    }
}
