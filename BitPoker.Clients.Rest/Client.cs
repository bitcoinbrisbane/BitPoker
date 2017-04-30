using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Contracts;
using BitPoker.Models.Messages;
using System.Net.Http;

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

        public async Task<IEnumerable<Peer>> GetPeersAsync(string host)
        {
            //String json = JsonConvert.SerializeObject(request);
            //StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            //using (HttpClient httpClient = new HttpClient())
            //{
            //    using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
            //    {
            //        if (responseMessage.IsSuccessStatusCode)
            //        {
            //            String responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            //            Console.WriteLine(responseContent);
            //        }
            //        else
            //        {
            //            throw new InvalidOperationException();
            //        }
            //    }
            //}


            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(String.Format("{0}/api/peers", host));
                String json = await response.Content.ReadAsStringAsync();

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Peer>>(json);
            }
        }

        public async Task<IEnumerable<IPlayer>> GetPlayers(string host, Guid tableId)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(String.Format("{0}/api/peers", host));
                String json = await response.Content.ReadAsStringAsync();

                //return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Peer>>(json);

                throw new Exception();
            }
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
