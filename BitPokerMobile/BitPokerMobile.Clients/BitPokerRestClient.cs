using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitPokerMobile.Clients
{
	public class BitPokerRestClient : IPlayerClient, ITableClient
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

		#region ITableClient implementation

		public async Task<IEnumerable<PCL.Models.TableInfo>> GetTablesAsync()
		{
			String json = await _httpClient.GetStringAsync(String.Format("{0}/tables", _url));

			IEnumerable<PCL.Models.TableInfo> players = JsonConvert.DeserializeObject<IEnumerable<PCL.Models.TableInfo>>(json);
			return players;
		}

		#endregion

		public async Task<String> RefreshMocksAsync()
		{
			String response = await _httpClient.GetStringAsync(String.Format("{0}/mocks", _url));
			return response;
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}
	}
}