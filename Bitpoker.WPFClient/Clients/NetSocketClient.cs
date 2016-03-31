using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    /// <summary>
    /// Test class for Netsocket implmenation
    /// </summary>
    public class NetSocketClient : INetworkClient
    {
        public IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers()
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
