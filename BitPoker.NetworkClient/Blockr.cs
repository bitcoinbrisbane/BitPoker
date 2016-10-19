using System;
using System.Threading.Tasks;

namespace BitPoker.NetworkClient
{
    public class Blockr : IBroadCastClient, IDisposable
    {
        public async Task<string> BroadCaastAsync(string tx)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<Decimal> GetAddressBalanceAsync(String address, Int16 confirmations)
        {
            //http://btc.blockr.io/api/v1/address/info/198aMn6ZYAczwrE5NvNTUMyJ5qkfy4g3Hi?confirmations=2
            String url = String.Format("http://tbtc.blockr.io/api/v1/address/info/{0}?confirmations={1}", address, confirmations);

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                String json = await client.GetStringAsync(url);
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Blockr.AddressResponse>(json);

                return response.data.balance;
            }
        }
    }
}
