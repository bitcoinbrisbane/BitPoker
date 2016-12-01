using NBitcoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class TableViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //TODO: REMOVE
        //private const String apiUrl = "https://www.bitpoker.io/api";

        private readonly BitPoker.Models.Contracts.Table _table;

        public String Address { get; set; }

        public ICollection<BitPoker.Models.PlayerInfo> Players { get; set;}

        public ICollection<HandViewModel> Hands { get; set; }

        /// <summary>
        /// Not yet used
        /// </summary>
        public IList<BitPoker.NetworkClient.INetworkClient> Clients { get; set; }

        public Clients.IChatBackend Client { get; set; }

        public TableViewModel(BitPoker.Models.Contracts.Table table)
        {
            _table = table;
            //this.Client = new Clients.ChatBackend();
        }

        public async Task<Boolean> JoinTable(BitPoker.Models.Messages.JoinTableRequest request)
        {
            //Check if seat is allocated
            
            //Check amount of BTC in that address to stop spamming
            using (BitPoker.NetworkClient.Blockr client = new BitPoker.NetworkClient.Blockr())
            {
                //Clients.Blockr client = new Clients.Blockr();
                Decimal balance = await client.GetAddressBalanceAsync(request.Player.BitcoinAddress, 2);

                if (balance > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Boolean> StartTable()
        {
            return true;
        }

        public async Task<String> BuyIn(Int16 seat, UInt64 amount)
        {
            if (amount >= _table.MinBuyIn && amount <= _table.MaxBuyIn)
            {
                BitcoinSecret secret = new BitcoinSecret(KeyRepository.GetWif(), Network.TestNet);
                BitcoinAddress address = secret.PubKey.GetAddress(Network.TestNet);

                BitPoker.Models.Messages.BuyInRequest message = new BitPoker.Models.Messages.BuyInRequest()
                {
                    BitcoinAddress = address.ToString(),
                    Amount = amount,
                    //Seat = seat,
                    TimeStamp = DateTime.UtcNow
                };

                //buyIn.Signature = secret.PrivateKey.SignMessage(buyIn.ToString());
                BitPoker.Models.IRequest request = new BitPoker.Models.Messages.RPCRequest()
                {
                    Method = "BuyInRequest"
                };

                request.Params = message;

                return Newtonsoft.Json.JsonConvert.SerializeObject(message);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task Call(UInt64 amount)
        {
            BitPoker.Models.Messages.ActionMessage message = new BitPoker.Models.Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = amount,
                //PublicKey = this.Address.ToString()
            };

            //Sign message
            BitcoinSecret secret = new BitcoinSecret("93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS", NBitcoin.Network.TestNet);
            //message.Signature = secret.PrivateKey.SignMessage(message.ToString());

            BitPoker.Models.IRequest request = new BitPoker.Models.Messages.RPCRequest()
            {
                Method = "BuyInRequest"
            };

            request.Params = message;

            //BitPoker.NetworkClient.IMessageClient client = new BitPoker.NetworkClient.APIClient(apiUrl);

        }
    }
}
