using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.NetworkClient
{
    /// <summary>
    /// Send transactions to the blockchain nodes
    /// </summary>
    public interface IBroadCastClient
    {
        Task<String> BroadCaastAsync(String tx);
    }
}
