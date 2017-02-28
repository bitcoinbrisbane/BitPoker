using System;
using BitPoker.Models.GameContext;

namespace BitPoker.Models.Players
{
    public interface IPlayer
    {
        String Name { get; }

        void StartGame(IStartGameContext context);

        void StartHand(IStartHandContext context);

        void StartRound(IStartRoundContext context);

        PlayerAction GetTurn(IGetTurnContext context);

        void EndRound(IEndRoundContext context);

        void EndHand(IEndHandContext context);

        void EndGame(IEndGameContext context);
    }
}