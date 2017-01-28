using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Clients.JSONRPC
{
    public class BaseClient
    {
        internal async Task<String> PostAsync(StringContent requestContent, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return await responseMessage.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        internal async Task<String> GetAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                String json = await httpClient.GetStringAsync(url);
                return json;
            }
        }
    }
}
