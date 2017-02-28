using System;
using System.Collections.Generic;
using BitPoker.Models.Players;

namespace BitPoker.Models.GameContext
{
	public interface IEndRoundContext
	{
		IReadOnlyCollection<PlayerActionAndName> RoundActions { get; }
	}
}
