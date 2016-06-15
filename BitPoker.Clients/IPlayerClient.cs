using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
	public interface IPlayerClient : IDisposable
	{
		IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers ();

		Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync ();
	}
}