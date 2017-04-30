using BitPoker.Models.GameContext;
using System.Collections.Generic;
using BitPoker.Models.Players;

namespace BitPoker.Logic.Players
{
    public class EndRoundContext : IEndRoundContext
    {
        public EndRoundContext(IReadOnlyCollection<PlayerActionAndName> roundActions)
        {
            this.RoundActions = roundActions;
        }

        public IReadOnlyCollection<PlayerActionAndName> RoundActions { get; }
    }
}
