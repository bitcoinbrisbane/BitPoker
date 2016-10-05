using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class WalletViewModel
    {
        public BitcoinAddress Address { get; private set; }

        public IObservable<Decimal> Balance { get; private set; }

        public WalletViewModel(String wifKey)
        {
            BitcoinSecret secret = new BitcoinSecret(wifKey, Network.TestNet);
            Address = secret.GetAddress();
        }

        public async Task LoadBalance()
        {

        }
    }
}
