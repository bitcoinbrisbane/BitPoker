using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using Newtonsoft.Json;

namespace BitPoker.Clients.JSONRPC
{
    public class PeerClient : BaseClient, IPeerClient
    {
        public PeerClient(Int16 version = 1)
        {
        }

        public async Task<Peer> GetPeerInfoAsync(string host)
        {
            String endPoint = String.Format("{0}/v{1}/messages", host);
            String json = await GetAsync(endPoint);

            Peer peer = JsonConvert.DeserializeObject<Peer>(json);
            return peer;
        }

        public async Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            String endPoint = String.Format("{0}/players", host);
            String json = await GetAsync(endPoint);

            List<Peer> peers = JsonConvert.DeserializeObject<List<Peer>>(json);
            return peers;
        }
    }
}
