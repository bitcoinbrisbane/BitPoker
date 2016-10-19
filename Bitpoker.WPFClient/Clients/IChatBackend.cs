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

    //[DataContract]
    //public class CompositeType
    //{
    //    private string _username = "Anonymous";
    //    private string _message = "";

    //    public CompositeType() { }

    //    public CompositeType(string u, string m)
    //    {
    //        _username = u;
    //        _message = m;
    //    }

    //    [DataMember]
    //    public string Username
    //    {
    //        get { return _username; }
    //        set { _username = value; }
    //    }

    //    [DataMember]
    //    public string Message
    //    {
    //        get { return _message; }
    //        set { _message = value; }
    //    }
    //}

    public delegate void DisplayMessageDelegate(CompositeType data);
}