namespace BitPoker.Logic.GameMechanics
{
    using BitPoker.Logic.Players;

    public interface ITexasHoldemGame
    {
        int HandsPlayed { get; }

        IPlayer Start();
    }
}
