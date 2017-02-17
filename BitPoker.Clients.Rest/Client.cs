using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Contracts;
using BitPoker.Models.Messages;

namespace BitPoker.Clients.Rest
{
    public class Client : IPeerClient, ITableClient
    {
        public Task<BuyInResponse> BuyIn(string host, BuyInRequest param)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hand>> GetHandHistory(Guid tableId)
        {
            throw new NotImplementedException();
        }

        public Task<Peer> GetPeerInfoAsync(string host)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IPlayer>> GetPlayers(string host, Guid tableId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ITable>> GetTablesAsync(string host)
        {
            throw new NotImplementedException();
        }

        public Task<JoinTableResponse> Join(string host, JoinTableRequest param)
        {
            throw new NotImplementedException();
        }
    }
}
