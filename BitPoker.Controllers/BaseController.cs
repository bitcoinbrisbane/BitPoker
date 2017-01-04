using System;
using System.Web.Http;

namespace BitPoker.Controllers
{
    public abstract class BaseController : ApiController
    {
        public Boolean Verify(String address, String message, String signature)
        {
            NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
            var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
            bool verified = pubKey.VerifyMessage(message, signature);

            return verified;
        }
    }
}
