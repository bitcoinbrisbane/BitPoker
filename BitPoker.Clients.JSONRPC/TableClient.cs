using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Contracts;
using BitPoker.Models.Messages;
using Newtonsoft.Json;
using System.Net.Http;

namespace BitPoker.Clients.JSONRPC
{
    public class TableClient : BaseClient, ITableClient
    {
        public async Task<BuyInResponse> BuyIn(String host, BuyInRequest param)
        {
            IRequest request = new Models.Messages.RPCRequest()
            {
                Method = "BuyIn",
                Params = param
            };

            //message.Signature = carol_secret.PrivateKey.SignMessage(message.ToString());

            String endPoint = String.Format("{0}/v1/messages", host);

            String json = JsonConvert.SerializeObject(request);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            String response = await PostAsync(requestContent, endPoint);
            BuyInResponse buyInResponse = JsonConvert.DeserializeObject<BuyInResponse>(response);
            return buyInResponse;
        }

        public async Task<IEnumerable<Hand>> GetHandHistory(Guid tableId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IPlayer>> GetPlayers(string host, Guid tableId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ITable>> GetTablesAsync(string host)
        {
            String endPoint = String.Format("{0}/tables", host);
            String json = await GetAsync(endPoint);

            List<ITable> tables = JsonConvert.DeserializeObject<List<ITable>>(json);
            return tables;
        }

        public async Task<JoinTableResponse> Join(String host, JoinTableRequest param)
        {
            IRequest request = new Models.Messages.RPCRequest()
            {
                Method = "BuyIn",
                Params = param
            };

            String endPoint = String.Format("{0}", host);

            String json = JsonConvert.SerializeObject(request);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String jsonResponse = await PostAsync(requestContent, endPoint);

            JoinTableResponse response = JsonConvert.DeserializeObject<JoinTableResponse>(jsonResponse);
            return response;
        }
    }
}
