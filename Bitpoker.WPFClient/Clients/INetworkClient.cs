using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    public interface INetworkClient
    {
        IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers();

        Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync();

        Boolean IsConnected { get; }
    }
}
