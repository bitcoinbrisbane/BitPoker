using BitPoker.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatBackend : IChatBackend
    {
        #region Everything we need to receive messages

        DisplayMessageDelegate _displayMessageDelegate = null;
        DisplayIMessageDelegate _displayIMessageDelegate = null;

        /// <summary>
        /// The default constructor is only here for testing purposes.
        /// </summary>
        private ChatBackend()
        {
        }

        [Obsolete]
        /// <summary>
        /// ChatBackend constructor should be called with a delegate that is capable of displaying messages.
        /// </summary>
        /// <param name="dmd">DisplayMessageDelegate</param>
        public ChatBackend(DisplayMessageDelegate dmd, String bitcoinAddress)
        {
            _myUserName = bitcoinAddress;
            _displayMessageDelegate = dmd;
            StartService();
        }

        /// <summary>
        /// ChatBackend constructor should be called with a delegate that is capable of displaying messages.
        /// </summary>
        /// <param name="dmd">DisplayMessageDelegate</param>
        public ChatBackend(DisplayIMessageDelegate dmd, String bitcoinAddress)
        {
            _myUserName = bitcoinAddress;
            _displayIMessageDelegate = dmd;
            StartService();
        }

        /// <summary>
        /// This method gets called by our friends when they want to display a message on our screen.
        /// We're really only returning a string for demonstration purposes … it might be cleaner
        /// to return void and also make this a one-way communication channel.
        /// </summary>
        /// <param name="composite"></param>
        public void DisplayMessage(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (_displayMessageDelegate != null)
            {
                _displayMessageDelegate(composite);
            }
        }

        public void DisplayIRequest(BitPoker.Models.IRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            else
            {
                _displayIMessageDelegate(request);
            }
        }

        public void DisplayIResponse(BitPoker.Models.IResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            else
            {
                //_displayIMessageDelegate(request);
            }
        }

        #endregion // Everything we need to receive messages

        #region Everything we need for bi-directional communication

        public string _myUserName = "Anonymous";
        private ServiceHost host = null;
        private ChannelFactory<IChatBackend> channelFactory = null;
        private IChatBackend _channel;

        /// <summary>
        /// The front-end calls the SendMessage method in order to broadcast a message to our friends
        /// </summary>
        /// <param name="text"></param>
        public void SendMessage(string text)
        {
            _channel.DisplayMessage(new CompositeType(_myUserName, text));
        }

        public void SendRequest(BitPoker.Models.IRequest request)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            SendMessage(json);
        }

        public void SendResponse(BitPoker.Models.IResponse response)
        {
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            SendMessage(json);
        }

        private void StartService()
        {
            host = new ServiceHost(this);
            host.Open();
            channelFactory = new ChannelFactory<IChatBackend>("ChatEndpoint");
            _channel = channelFactory.CreateChannel();
        }

        private void StopService()
        {
            if (host != null)
            {
                _channel.DisplayMessage(new CompositeType("Event", _myUserName + " is leaving the conversation."));
                if (host.State != CommunicationState.Closed)
                {
                    channelFactory.Close();
                    host.Close();
                }
            }
        }

        #endregion 
        // Everything we need for bi-directional communication
    }
}
