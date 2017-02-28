using System.Collections.Generic;
using BitPoker.Models.Cards;

namespace BitPoker.Models.GameContext
{
	public interface IEndHandContext
	{
		Dictionary<string, ICollection<Card>> ShowdownCards { get; }
	}
}
