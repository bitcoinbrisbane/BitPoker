using System;
using System.Collections.Generic;

namespace BitPoker.Models.Contracts
{
    public interface ITable
    {
        Guid Id { get; set; }

        String HashAlgorithm { get; set; }

        String NetworkAddress { get; set; }

        /// <summary>
        /// Array of players in their seats
        /// </summary>
        Peer[] Peers { get; }
    }
}
