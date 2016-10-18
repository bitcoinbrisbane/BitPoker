using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    public interface INetworkClient
    {
        Boolean IsConnected { get; }

        IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers();

        Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync();

        BitPoker.Models.Contracts.Table GetTable(Guid id);
    }
}