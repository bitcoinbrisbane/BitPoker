using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    public interface IMessageClient
    {
        void SendMessage(BitPoker.Models.Messages.ActionMessage message);
    }
}
