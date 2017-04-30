using BitPoker.Models.Players;

namespace BitPoker.Models.GameMechanics
{
    public interface ITexasHoldemGame
    {
        int HandsPlayed { get; }

        IPlayer Start();
    }
}
