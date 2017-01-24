using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
    public interface ITableClient
    {
        /// <summary>
        /// Get tables from a peer
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Contracts.ITable>> GetTablesAsync(String host);

        /// <summary>
        /// Gets players at the table
        /// </summary>
        /// <param name="host"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.IPlayer>> GetPlayers(String host, Guid tableId);

        Task<Models.Messages.JoinTableResponse> Join(String host, Models.Messages.JoinTableRequest request);

        Task<Models.Messages.BuyInResponse> BuyIn(String host, Models.Messages.BuyInRequest request);

        Task<IEnumerable<Models.Hand>> GetHandHistory(Guid tableId);
    }
}