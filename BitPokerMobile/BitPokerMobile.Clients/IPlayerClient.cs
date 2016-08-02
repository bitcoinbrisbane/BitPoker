using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPokerMobile.Clients
{
	public interface IPlayerClient : IDisposable
	{
		Task<IEnumerable<PCL.Models.PlayerInfo>> GetPlayersAsync();
	}
}