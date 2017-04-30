using System;
using System.Collections.Generic;
using BitPoker.Models.GameContext;

namespace BitPoker.Logic.Players
{
	public class StartGameContext : IStartGameContext
    {
        public StartGameContext(IReadOnlyCollection<string> playerNames, Int64 startMoney)
        {
            this.PlayerNames = playerNames;
            this.StartMoney = startMoney;
        }

        public IReadOnlyCollection<string> PlayerNames { get; }

		public Int64 StartMoney { get; }
	}
}
