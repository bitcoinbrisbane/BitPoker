using BitPoker.Models.GameContext;

namespace BitPoker.Logic.Players
{
    public class EndGameContext : IEndGameContext
    {
        public EndGameContext(string winnerName)
        {
            this.WinnerName = winnerName;
        }

        public string WinnerName { get; }
    }
}
