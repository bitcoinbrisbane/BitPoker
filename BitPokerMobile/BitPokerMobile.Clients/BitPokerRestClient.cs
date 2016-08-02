using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitPokerMobile.Clients
{
	public class BitPokerRestClient : IPlayerClient
	{
		private readonly String _url = "https://www.bitpoker.io/api/";
		private readonly System.Net.Http.HttpClient _httpClient;

		public BitPokerRestClient()
		{
			_httpClient = new System.Net.Http.HttpClient();
		}

		#region IPlayerClient implementation

		public async Task<IEnumerable<PCL.Models.PlayerInfo>> GetPlayersAsync()
		{
			String json = await _httpClient.GetStringAsync(String.Format("{0}/players", _url));

			IEnumerable<PCL.Models.PlayerInfo> players = JsonConvert.DeserializeObject<IEnumerable<PCL.Models.PlayerInfo>>(json);
			return players;
		}

		#endregion

		public void Dispose()
		{
			_httpClient.Dispose();
		}
	}
}