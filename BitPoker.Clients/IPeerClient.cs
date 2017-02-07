using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
    public interface IPeerClient
    {
        /// <summary>
        /// Gets peer info, such as user agent
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<Models.Peer> GetPeerInfoAsync(String host);

        /// <summary>
        /// Get peers from a peer
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Peer>> GetPeersAsync(String host);
    }
}