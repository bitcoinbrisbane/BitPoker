using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker
{
    public class Client
    {
        private TcpClient _client;

        //private StreamReader _sReader;
        //private StreamWriter _sWriter;

        private Boolean _isConnected;

        public Client(String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);

            HandleCommunication();
        }

        public void HandleCommunication()
        {
            using (StreamReader _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII))
            {
                using (StreamWriter _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII))
                {
                    _isConnected = true;
                    String sData = null;
                    while (_isConnected)
                    {
                        Console.Write("&gt; ");
                        sData = Console.ReadLine();

                        // write data and make sure to flush, or the buffer will continue to 
                        // grow, and your data might not be sent when you want it, and will
                        // only be sent once the buffer is filled.
                        _sWriter.WriteLine(sData);
                        _sWriter.Flush();

                        // if you want to receive anything
                        // String sDataIncomming = _sReader.ReadLine();
                    }
                }
            }
        }
    }
}