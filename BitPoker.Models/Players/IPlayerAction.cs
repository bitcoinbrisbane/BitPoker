using System;

namespace BitPoker.Models
{
	public interface IPlayerAction
	{
		BitPoker.Models.Players.PlayerActionType Type { get; }

		Int64 Money { get; set; }
	}
}