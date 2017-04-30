namespace BitPoker.Models.Players
{
	using System.Collections.Generic;
	using BitPoker.Models.Cards;
	using BitPoker.Models.GameContext;

	public abstract class BasePlayer : Models.Players.IPlayerLogic
	{
	        public abstract string Name { get; }
	
	        protected IReadOnlyCollection<Card> CommunityCards { get; private set; }
	
	        protected Card FirstCard { get; private set; }
	
	        protected Card SecondCard { get; private set; }
	
	        public virtual void StartGame(IStartGameContext context)
	        {
	        }
	
	        public virtual void StartHand(IStartHandContext context)
	        {
	            this.FirstCard = context.FirstCard;
	            this.SecondCard = context.SecondCard;
	        }
	
	        public virtual void StartRound(IStartRoundContext context)
	        {
	            this.CommunityCards = context.CommunityCards;
	        }
	
	        public abstract PlayerAction GetTurn(IGetTurnContext context);
	
	        public virtual void EndRound(IEndRoundContext context)
	        {
	        }
	
	        public virtual void EndHand(IEndHandContext context)
	        {
	        }
	
	        public virtual void EndGame(IEndGameContext context)
	        {
	        }
    }
}
