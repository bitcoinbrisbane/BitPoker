using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class TableViewModel
    {
        private readonly BitPoker.Models.Contracts.Table _table;

        public String Address { get; set; }

        public ICollection<BitPoker.Models.PlayerInfo> Players { get; set;}

        public ICollection<HandViewModel> Hands { get; set; }

        public TableViewModel(BitPoker.Models.Contracts.Table table)
        {
            _table = table;
        }

        public async Task<Boolean> JoinTable(BitPoker.Models.Messages.JoinTableRequest request)
        {
            //Check if seat is allocated

            //Check BTC of that address to stop spamming
            Clients.Blockr client = new Clients.Blockr();
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

                BitPoker.Models.Messages.BuyInRequestMessage buyIn = new BitPoker.Models.Messages.BuyInRequestMessage()
                {
                    BitcoinAddress = address.ToString(),
                    Amount = amount,
                    Seat = seat,
                    TimeStamp = DateTime.UtcNow
                };

                buyIn.Signature = secret.PrivateKey.SignMessage(buyIn.ToString());

                return Newtonsoft.Json.JsonConvert.SerializeObject(buyIn);
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
            BitPoker.Models.Messages.ActionMessage call = new BitPoker.Models.Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = amount,
                //PublicKey = this.Address.ToString()
            };

            //Sign message
            BitcoinSecret secret = new BitcoinSecret("93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS", NBitcoin.Network.TestNet);
            call.Signature = secret.PrivateKey.SignMessage(call.ToString());

            Bitpoker.WPFClient.Clients.INetworkClient client = new Bitpoker.WPFClient.Clients.APIClient("https://bitpoker.azurewebsites.net/api/");
            //if (client.IsConnected)
            //{
            //    client.SendMessage(call);
            //}
        }
    }
}
