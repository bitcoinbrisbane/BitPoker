using BitPoker.NetworkClient;
using System;
using System.ServiceModel;
using BitPoker.Models.Messages;

namespace Bitpoker.WPFClient.Clients
{
    [ServiceContract]
    public interface IChatBackend //: IMessageClient
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessage(CompositeType composite);

        [OperationContract(IsOneWay = true)]
        void DisplayIMessage(BitPoker.Models.IRequest message);

        [Obsolete]
        void SendMessage(string text);

        void SendIMessage(BitPoker.Models.IRequest message);
    }

    public delegate void DisplayMessageDelegate(CompositeType data);

    public delegate void DisplayIMessageDelegate(BitPoker.Models.IRequest message);
}