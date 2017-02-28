namespace BitPoker.AI
{
	using System;
	using Models.GameContext;
	using BitPoker.Models.Players;

	internal class CallingStation : BasePlayer
    {
        public override string Name { get; } = "CallingStation_" + Guid.NewGuid();

		public override PlayerAction GetTurn(IGetTurnContext context)
        {
            return PlayerAction.CheckOrCall();
        }
    }
}