using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Contracts;
using BitPoker.Models.Messages;
using ZeroMQ;

namespace BitPoker.Clients.ZeroMQ
{
    public class Client : IPeerClient, ITableClient
    {
        private readonly string _privateKey;

        public Client(String privateKey)
        {
            _privateKey = privateKey;
        }

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

        public async Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            using (var context = new ZContext())
            {
                using (var requester = new ZSocket(context, ZSocketType.REQ))
                {
                    // Connect
                    requester.Connect(host);

                    // Send
                    requester.Send(new ZFrame("GETPEERS"));

                    // Receive
                    using (ZFrame reply = requester.ReceiveFrame())
                    {
                        string json = reply.ReadString();
                        return await Task.FromResult<IEnumerable<Peer>>(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Peer>>(json));
                    }
                }
            }
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
