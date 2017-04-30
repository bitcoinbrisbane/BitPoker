using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
    public interface IPeerClient
    {
        /// <summary>
        /// Gets peer from repo
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<Models.Peer> GetPeerAsync(String host);

        /// <summary>
        /// Get peers from local repo
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Peer>> GetPeersAsync(String host);
    }
}