using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Clients.Mocks
{
    public class PeerClient : IPeerClient
    {
		public async Task<Peer> GetPeerAsync(string host)
		{
			//92XB2GQqVF2SuG8KB7hLFq3yZEdCRincUMB2bk51xbNpLwLZSc2
			Peer peer = new Peer() { BitcoinAddress = "n13BduthHMtH99KeSkijwF2ChaYuA4RqTQ", NetworkAddress = "127.0.0.1:5000", UserAgent = "Bitpoker v1 Mock" };
			return await Task.FromResult<Peer>(peer);
		}

        public async Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            List<Peer> peers = new List<Peer>(1);
            return await Task.FromResult<IEnumerable<Peer>>(peers);
        }
    }
}
