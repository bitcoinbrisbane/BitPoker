using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class MainViewModel
    {

        SocketPermission permission;
        Socket sListener;
        IPEndPoint ipEndPoint;
        Socket handler;
        public Socket senderSock; 

        private static Random rng = new Random();  

        public IObservable<BitPoker.Models.PlayerInfo> Players { get; set; }

        public BitPoker.Models.Contracts.Table Table { get; set; }

        internal ICollection<Byte[]> Keys { get; set; }

        internal Byte[] IV { get; set; }

        public void NewTable(Int16 minPlayers, Int16 maxPlayers)
        {
            this.Table = new BitPoker.Models.Contracts.Table(minPlayers, maxPlayers);
        }

        public void CreateKeys()
        {
            Byte[] allKeys = new Byte[832];

            for (Int32 i = 0; i < 52; i++)
            {
                Byte[] key = new Byte[16];
                rng.NextBytes(key);
                key.CopyTo(allKeys, i * 16);

                this.Keys.Add(key);
                //Console.WriteLine(Convert.ToBase64String(key));
            }

            //Calculate hash on allKeys
        }

        public void Connect(IPAddress ipAddr)
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
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4510);

                // Create one Socket object to setup Tcp connection  
                senderSock = new Socket(ipAddr.AddressFamily, SocketType.Stream,  ProtocolType.Tcp );

                senderSock.NoDelay = false;   // Using the Nagle algorithm  

                // Establishes a connection to a remote host  
                senderSock.Connect(ipEndPoint);

            }
            catch (Exception exc)
            {

            };
        }

        public void SendGetPlayersMessage()
        {
            //Call connected

            //Deseralize json
        }

        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
    }
}
