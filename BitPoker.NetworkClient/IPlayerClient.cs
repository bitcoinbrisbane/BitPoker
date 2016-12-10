using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BitPoker.NetworkClient
{
	public interface IPlayerClient : IDisposable
	{
		Task AddPlayer(Models.Peer player);

		Task<IEnumerable<Models.Peer>> GetPlayersAsync();
	}
}