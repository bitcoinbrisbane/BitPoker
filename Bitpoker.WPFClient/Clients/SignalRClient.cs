using BitPoker.NetworkClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using BitPoker.Models.Messages;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Bitpoker.WPFClient.Clients
{
    [HubName("BitPoker")]
    public class SignalRClient : Hub, IMessageClient
    {
        public void SendIMessage(BitPoker.Models.IRequest message)
        {
            Clients.Others().addMessage(message.ToString());
        }

        public void SendMessage(ActionMessage message)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(ActionMessage message)
        {
            throw new NotImplementedException();
        }
    }
}