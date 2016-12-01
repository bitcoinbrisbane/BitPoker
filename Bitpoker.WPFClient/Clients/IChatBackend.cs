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
        void DisplayIRequest(BitPoker.Models.IRequest request);

        [OperationContract(IsOneWay = true)]
        void DisplayIResponse(BitPoker.Models.IResponse response);

        [Obsolete]
        void SendMessage(string text);

        void SendRequest(BitPoker.Models.IRequest message);

        void SendResponse(BitPoker.Models.IResponse response);
    }

    public delegate void DisplayMessageDelegate(CompositeType data);

    public delegate void DisplayIMessageDelegate(BitPoker.Models.IRequest request);

    public delegate void DisplayIResponseDelegate(BitPoker.Models.IResponse response);
}