using System;
using System.Collections.Generic;
using System.Threading.Tasks;
            

namespace BitPoker.NetworkClient
{
	public class HttpClient : IPlayerClient
	{
		private readonly String _url;

		public HttpClient(String url)
		{
			_url = url;	
		}

		public Task<IEnumerable<PlayerInfo>> GetPlayersAsync()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				Uri uri = new Uri(String.Format("{0}players", API_URL));
				String response = await httpClient.GetStringAsync(uri);

				if (!String.IsNullOrEmpty(json))
				{
					List<PlayerInfo> players = JsonConvert.DeserializeObject<List<PlayerInfo>>(response);
					return players;
				}
			}
		}

		public static void GetTablesAsync()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				Uri uri = new Uri(String.Format("{0}tables", API_URL));
				String json = httpClient.GetStringAsync(uri).Result;
				List<Models.Contracts.Table> response = JsonConvert.DeserializeObject<List<Models.Contracts.Table>>(json);

				foreach (Models.Contracts.Table table in response)
				{
					Console.WriteLine("{0} {1} {2}", table.Id, table.SmallBlind, table.BigBlind);
				}
			}
		}
	}
}
