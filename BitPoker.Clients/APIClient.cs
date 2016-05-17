using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
	public class APIClient : BitPoker.Clients.IPlayerClient
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
				String json = httpClient.GetStringAsync(String.Format("{0}/api/players",_apiUrl)).Result;
				IEnumerable<BitPoker.Models.PlayerInfo> result = JsonConvert.DeserializeObject<IEnumerable<BitPoker.Models.PlayerInfo>>(json);
				return result;
			}
		}

		public async Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync()
		{
			using (HttpClient httpClient = new HttpClient())
			{
				String json = await httpClient.GetStringAsync(String.Format("{0}/api/players", _apiUrl));
				IEnumerable<BitPoker.Models.PlayerInfo> result = JsonConvert.DeserializeObject<IEnumerable<BitPoker.Models.PlayerInfo>>(json);
				return result;
			}
		}

		public void BuyIn(BitPoker.Models.Messages.BuyInRequestMessage buyIn)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				//String json = Newtonsoft.Json.JsonConvert.SerializeObject

				//var json = httpClient.PostAsync(String.Format("{0}players", _apiUrl)).Result;
				//IEnumerable<BitPoker.Models.PlayerInfo> result = JsonConvert.DeserializeObject<IEnumerable<BitPoker.Models.PlayerInfo>>(json);
				//return result;
			}
		}


		public BitPoker.Models.Contracts.Table GetTable(Guid id)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				var json = httpClient.GetStringAsync(String.Format("{0}/api/table?id={1}", _apiUrl, id)).Result;
				BitPoker.Models.Contracts.Table result = JsonConvert.DeserializeObject<BitPoker.Models.Contracts.Table>(json);
				return result;
			}
		}


		public void SendMessage(BitPoker.Models.Messages.ActionMessage message)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				String json = JsonConvert.SerializeObject(message);
				StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
				String url = String.Format("{0}/api/message", _apiUrl);

				using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
				{
					if (responseMessage.IsSuccessStatusCode)
					{
						String responseContent = responseMessage.Content.ReadAsStringAsync().Result;
						//return Convert.ToInt32(responseContent);
					}
					else
					{
						throw new InvalidOperationException();
					}
				}
			}
		}

		public void Dispose()
		{
		}
	}
}