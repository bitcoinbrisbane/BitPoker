using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BitPoker.NetworkClient
{
	public interface IPlayerClient : IDisposable
	{
		Task AddPlayer(Models.PlayerInfo player);

		Task<IEnumerable<Models.PlayerInfo>> GetPlayersAsync();
	}
}