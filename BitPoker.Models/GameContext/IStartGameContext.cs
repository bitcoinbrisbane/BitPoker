using System;
using System.Collections.Generic;

namespace BitPoker.Models.GameContext
{
	public interface IStartGameContext
	{
		IReadOnlyCollection<string> PlayerNames { get; }

		Int64 StartMoney { get; }
	}
}
