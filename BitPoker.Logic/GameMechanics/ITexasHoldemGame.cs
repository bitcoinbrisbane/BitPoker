using BitPoker.Logic.Players;

namespace BitPoker.Logic.GameMechanics
{
    public interface ITexasHoldemGame
    {
        int HandsPlayed { get; }

        IPlayer Start();
    }
}
