using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Clients.Mocks
{
    public class PeerClient : IPeerClient
    {
        public async Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            List<Peer> peers = new List<Peer>(1);
            return await Task.FromResult<IEnumerable<Peer>>(peers);
        }
    }
}
