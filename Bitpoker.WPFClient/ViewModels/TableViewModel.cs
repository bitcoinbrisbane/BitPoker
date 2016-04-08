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
        public String Address { get; set; }

        public ICollection<BitPoker.Models.PlayerInfo> Players { get; set;}

        public ICollection<ViewModels.HandViewModel> Hands { get; set; }

        public void Call(Int64 amount)
        {
            BitPoker.Models.Messages.ActionMessage call = new BitPoker.Models.Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = amount,
                PublicKey = this.Address.ToString()
            };

            //Sign message
            BitcoinSecret secret = new BitcoinSecret("93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS", NBitcoin.Network.TestNet);
            call.Signature = secret.PrivateKey.SignMessage(call.ToString());

            Bitpoker.WPFClient.Clients.INetworkClient client = new Bitpoker.WPFClient.Clients.APIClient("https://bitpoker.azurewebsites.net/api/");
            if (client.IsConnected)
            {
                client.SendMessage(call);
            }
        }
    }
}
