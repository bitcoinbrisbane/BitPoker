using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BitPoker.Core.RestHost
{
    public abstract class BaseController : Controller
    {
        private List<String> _logs;

        public IEnumerable<String> Logs { get { return _logs; } }

        public Boolean Verify(String address, String message, String signature)
        {
            NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
            var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
            bool verified = pubKey.VerifyMessage(message, signature);

            return verified;
        }

        public void AddLog(String message)
        {
            Console.WriteLine(message);

            if (_logs == null)
            {
                _logs = new List<string>();
            }

            _logs.Add(message);
        }
    }
}
