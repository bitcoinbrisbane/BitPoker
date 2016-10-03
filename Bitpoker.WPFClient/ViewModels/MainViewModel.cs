using NBitcoin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models.ExtensionMethods;

namespace Bitpoker.WPFClient.ViewModels
{
    /// <summary>
    /// View model for table really.
    /// </summary>
    public class MainViewModel
    {
        //SocketPermission permission;
        //Socket sListener;
        //IPEndPoint ipEndPoint;
        //Socket handler;
        public Socket senderSock; 

        private static Random rng = new Random();

        public IList<Clients.INetworkClient> Clients { get; set; }

        /// <summary>
        /// Players on the entire network
        /// </summary>
        public ObservableCollection<BitPoker.Models.PlayerInfo> NetworkPlayers { get; set; }

        //public BitPoker.Models.Contracts.Table Table { get; set; }
        public TableViewModel Table { get; set; }

        internal ICollection<Byte[]> Keys { get; set; }

        internal Byte[] IV { get; set; }

        public WalletViewModel Wallet { get; set; }

        public BitPoker.Repository.ITableRepository TableRepo { get; set; }

        public MainViewModel()
        {
            this.NetworkPlayers = new ObservableCollection<BitPoker.Models.PlayerInfo>();
            this.Clients = new List<Clients.INetworkClient>(1);
            
            this.Clients.Add(new Clients.APIClient("https://www.bitpoker.io/api/"));
            //this.Clients.Add(new Clients.NetSocketClient(IPAddress.Parse("127.0.0.1")));

            Wallet = new WalletViewModel("93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS");
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind)
        {
            AddNewTable(smallBlind, bigBlind, 2, 10);
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind, Int16 minPlayers, Int16 maxPlayers)
        {
            AddNewTable(smallBlind, bigBlind, bigBlind * 20, bigBlind * 100, 2, 10);
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind, UInt64 minBuyIn, UInt64 maxBuyIn, Int16 minPlayers, Int16 maxPlayers)
        {
            BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table()
            {
                SmallBlind = smallBlind,
                BigBlind = bigBlind,
                MinBuyIn = minBuyIn,
                MaxBuyIn = maxBuyIn,
                MinPlayers = minPlayers,
                MaxPlayers = maxPlayers
            };

            if (!table.IsValid())
            {
                throw new AggregateException("Invalid table params");
            }

            using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
            {
                TableRepo.Add(table);
            }
        }

        public void GetPlayers()
        {
            foreach (Clients.INetworkClient client in this.Clients)
            {
                if (client.IsConnected)
                {
                    //TODO: Check player does not exist in collection and zip
                    var players = client.GetPlayers();

                    foreach (BitPoker.Models.PlayerInfo player in players)
                    {
                        this.NetworkPlayers.Add(player);
                    }
                }
            }
        }

        public Int32 BuyIn(Guid tableId, Int64 amount)
        {
            //foreach (Bitpoker.WPFClient.Clients.INetworkClient client in this.Clients)
            //{
            //    if (client.IsConnected)
            //    {
            //        var players = client.GetPlayers();
            //    }
            //}
            return 0;
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
