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
        private NBitcoin.BitcoinAddress _address;

        public IObservable<Decimal> Balance { get; set; }

        public WalletViewModel(String wifKey)
        {
            NBitcoin.BitcoinSecret secret = new BitcoinSecret(wifKey, NBitcoin.Network.TestNet);
            _address = secret.GetAddress();
        }

        public async Task LoadBalance()
        {

        }
    }
}
