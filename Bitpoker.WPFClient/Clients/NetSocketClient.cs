using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.Clients
{
    /// <summary>
    /// Test class for Netsocket implmenation
    /// </summary>
    public class NetSocketClient : INetworkClient
    {
        private SocketPermission permission;
        //private Socket sListener;
        private IPEndPoint ipEndPoint;
        private Socket handler;
        private Socket senderSock;
        private IPAddress clientIpAddr;
        private Int32 clientPort;

        public NetSocketClient(IPAddress ipAddr, Int32 port = 4510)
        {
            clientIpAddr = ipAddr;
            clientPort = port;
        }

        public IEnumerable<BitPoker.Models.PlayerInfo> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BitPoker.Models.PlayerInfo>> GetPlayersAsync()
        {
            throw new NotImplementedException();
        }

        private void Connect()
        {
            try
            {
                // Create one SocketPermission for socket access restrictions  
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, "", SocketPermission.AllPorts);

                // Ensures the code to have permission to access a Socket  
                permission.Demand();

                //// Resolves a host name to an IPHostEntry instance             
                //IPHostEntry ipHost = Dns.GetHostEntry("");

                //// Gets first IP address associated with a localhost  
                //IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint  
                IPEndPoint ipEndPoint = new IPEndPoint(clientIpAddr, clientPort);

                // Create one Socket object to setup Tcp connection  
                senderSock = new Socket(clientIpAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                senderSock.NoDelay = false;   // Using the Nagle algorithm  

                // Establishes a connection to a remote host  
                senderSock.Connect(ipEndPoint);

            }
            catch (Exception exc)
            {

            };
        }


        public bool IsConnected
        {
            get { return senderSock.Connected; }
        }
    }
}
