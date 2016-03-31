using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    public class APIClient : INetworkClient
    {
        private readonly String _apiUrl;

        public bool IsConnected
        {
            get { return true; }
        }

        public APIClient(String apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var json = httpClient.GetStringAsync(String.Format("{0}players",_apiUrl)).Result;
                IEnumerable<BitPoker.Models.PlayerInfo> result = JsonConvert.DeserializeObject<IEnumerable<BitPoker.Models.PlayerInfo>>(json);
                return result;
            }
        }


        public Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync()
        {
            //var httpClient = new HttpClient();
            //var content = await httpClient.GetStringAsync(_apiUrl);
            //return await Task.Run(() =&gt; JsonObject.Parse(content));

            throw new NotImplementedException();
        }
    }
}
