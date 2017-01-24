using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
    public interface IPeerClient
    {
        /// <summary>
        /// Get peers from a peer
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Peer>> GetPeersAsync(String host);
    }
}