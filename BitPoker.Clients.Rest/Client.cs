using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Contracts;
using BitPoker.Models.Messages;
using System.Net.Http;
using Newtonsoft.Json;

namespace BitPoker.Clients.Rest
{
    public class Client : IPeerClient, ITableClient, IUserAgentClient
    {
        public Task<BuyInResponse> BuyIn(string host, BuyInRequest param)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Hand>> GetHandHistory(Guid tableId)
        {
            throw new NotImplementedException();
        }

        public Task<Peer> GetPeerAsync(string host)
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

		public async Task<UserAgentResponse> GetUserAgentAsync(string networkAddress)
		{
			using (HttpClient client = new HttpClient())
			{
				String json = await client.GetStringAsync(String.Format("{0}/api/useragent", networkAddress));

				UserAgentResponse response = null; // JsonConvert.DeserializeObject<BE.Models.User.BankDepositDetails>(json);
				return response;
			}

		}

		public Task<JoinTableResponse> Join(string host, JoinTableRequest param)
        {
            throw new NotImplementedException();
        }
    }
}
