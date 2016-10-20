using Bitpoker.WPFClient.Models;
using BitPoker.NetworkClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Bitpoker.WPFClient.Clients
{
    [ServiceContract]
    public interface IChatBackend : IMessageClient
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessage(CompositeType composite);

        void SendMessage(string text);
    }

    public delegate void DisplayMessageDelegate(CompositeType data);
}