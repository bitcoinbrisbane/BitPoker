using System;
using System.ComponentModel;
using System.Web.Http;

namespace BitPoker.Controllers
{
    public abstract class BaseController : ApiController, INotifyPropertyChanged
    {
        private String _log;

        public String Log
        {
            get { return _log; }
            set
            {
                _log = value;
                RaisePropertyChanged("Log");
            }
        }

        public Boolean Verify(String address, String message, String signature)
        {
            NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
            var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
            bool verified = pubKey.VerifyMessage(message, signature);

            return verified;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
