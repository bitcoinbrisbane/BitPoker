using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public abstract class BaseController : ApiController
    {
        internal Boolean Verify(String address, String message, String signature)
        {
            //NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
            //var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
            //bool verified = pubKey.VerifyMessage(message, signature);

            //return verified;
            return true;
        }

        internal void AddLog(String message)
        {
            //Console.WriteLine(message);

            //if (_logs == null)
            //{
            //    _logs = new List<string>();
            //}

            //_logs.Add(message);
        }
    }
}
