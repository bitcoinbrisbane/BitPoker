using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using BitPoker.Models;
using NBitcoin;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using NBitcoin.BouncyCastle.Security;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Org.BouncyCastle.Security;

namespace BitPoker
{
	public class MainClass
	{
		private static IList<Byte[]> TableDeck;
        private static List<Byte[]> _keys;


        private const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        private const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";
        private const String carol_wif = "91rahqyxZb6R1MMq2rdYomfB8GWsLVqkBMHrUnaepxks73KgfaQ";

        private static BitcoinSecret alice_secret = new BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
        private static BitcoinSecret bob_secret = new BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);
        private static BitcoinSecret carol_secret = new BitcoinSecret(carol_wif, NBitcoin.Network.TestNet);

        private static BitcoinAddress alice = alice_secret.GetAddress();
        private static BitcoinAddress bob = bob_secret.GetAddress();
        private static BitcoinAddress carol = carol_secret.GetAddress();

        private const String API_URL = "https://www.bitpoker.io/api/";

        private const String MOCK_HAND_ID = "398b5fe2-da27-4772-81ce-37fa615719b5";

        private static Repository.IPlayerRepository playerRepo;
        private static Repository.ITableRepository tableRepo;
        private static Repository.IGenericRepository<Peer> peersRepo;
        private static Repository.IHandRepository handRepo;

        private static Peer me;


        /// <summary>
        /// Console for test code
        /// </summary>
        /// <param name="args"></param>
		public static void Main (string[] args)
		{
            Console.WriteLine("{0} This is a console client for bitpoker, making REST or JSON RPC calls to the host", DateTime.UtcNow);

            Server server = new Server();
            server.MessageEvent += Server_MessageEvent;

            Int16 restPort = 8080; ///for rest
            Int16 tcpPort = 5555;

            if (args == null || args.Length < 1)
            {
                Log(String.Format("tcp://127.0.0.1:{0}", tcpPort));
                args = new string[] { carol.ToString(), String.Format("tcp://127.0.0.1:{0}", tcpPort )};
            }
            
            Log("Starting tcp server");
            Task task = new Task(() => server.Listen(args[0]));
            task.Start();
            Log("TCP server running");

            String baseUrl = String.Format("http://localhost:{0}", restPort);
            Log(baseUrl);

            Console.WriteLine("Enter private key, or press enter to use {0}", carol_wif);
            String privateKey = Console.ReadLine();

            if (String.IsNullOrEmpty(privateKey))
            {
                me = new Peer() 
                { 
                    BitcoinAddress = carol.ToString(), 
                    UserAgent = "Console App v0.1", 
                    NetworkAddress = baseUrl 
                };
            }
            else
            {
                carol_secret = new BitcoinSecret(privateKey, NBitcoin.Network.TestNet);
                carol = carol_secret.GetAddress();
                Log(String.Concat("Address set to ", carol));
                me = new Peer() 
                { 
                    BitcoinAddress = carol.ToString(), 
                    UserAgent = "Console App v0.1", 
                    NetworkAddress = baseUrl 
                };
            }

            Log("Setting up local repositories");
            
            //TODO refect
            
            playerRepo = new Repository.LiteDB.PlayerRepository<TexasHoldemPlayer>("bitpoker.db");
            peersRepo = new Repository.LiteDB.PeerRepository("bitpoker.db");
            tableRepo = new Repository.LiteDB.TableRepository("bitpoker.db");
            
            
            
            Log("Setting up clients");
            Clients.ITableClient tableClient = new Clients.Rest.Client();
            Clients.IPeerClient peerClient = new Clients.ZeroMQ.Client(carol_secret.PrivateKey.ToString());  //Clients.Rest.Client();



            Console.WriteLine("***");
            Console.WriteLine("This console app, under the context of Carol. {0}", carol);
            Console.WriteLine("***");

            Console.WriteLine("1. Add me to seed api");
            Console.WriteLine("2. List know peers (local db)");
            Console.WriteLine("21. Add know peer (local db)");
            Console.WriteLine("22. Reload all peers");

            Console.WriteLine("3. Add new table contract");
            Console.WriteLine("4. List tables (local db)");
            Console.WriteLine("41. Get tables from know peers");
            Console.WriteLine("42. Refresh all peer's tables");
            
            Console.WriteLine("6. Join table");
            Console.WriteLine("7. Buy into table");
            Console.WriteLine("8. Add a peer");
            Console.WriteLine("B. Show balance for {0}", carol);

            //Console.WriteLine("Get Hand");
            //Console.WriteLine("6. Fold / Muck");
            //Console.WriteLine("7. Call");
            //Console.WriteLine("8. Bet / Raise");
            //Console.WriteLine("R. Refresh");
            //Console.WriteLine("K. Create new keys");
            //Console.WriteLine("Q. Quit");

            String command = Console.ReadLine();

            while (command != "Q")
            {
                switch (command)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        Console.WriteLine("### {0} Dumping all peers in local database:", DateTime.Now);
                        foreach (Peer peer in peersRepo.All())
                        {
                            Console.WriteLine(peer);
                        }
                        break;
                    case "21":
                        Console.WriteLine("What is the peers address?");
                        String peerAddress = Console.ReadLine();

                        //Peer newPeer = peerClient.GetPeerInfoAsync(peerAddress).Result;
                        //peersRepo.Add(newPeer);

                        break;
                    case "22":
                        Console.WriteLine("*** Reload all peers (todo) ***");
                        break;
                    case "3":
                        Console.WriteLine("Small blind? 1000");
                        UInt64 sb = Convert.ToUInt64(Console.ReadLine().Trim());

                        Console.WriteLine("Big blind? {0}", sb * 2);
                        UInt64 bb = Convert.ToUInt64(Console.ReadLine().Trim());

                        Console.WriteLine("Min buy in? {0}", bb * 20);
                        UInt64 minBuyIn = Convert.ToUInt64(Console.ReadLine().Trim());
                        
                        Console.WriteLine("Max buy in? {0}", bb * 100);
                        UInt64 maxBuyIn = Convert.ToUInt64(Console.ReadLine().Trim());

                        tableRepo.Add(new Models.Contracts.Table() 
                        { 
                            SmallBlind = sb,
                            BigBlind = bb,
                            MinBuyIn = minBuyIn,
                            MaxBuyIn = maxBuyIn,
                            MinPlayers = 2,
                            MaxPlayers = 2
                        });
                        
                        break;
                    case "4":
                        Console.WriteLine("### Dumping all tables in local database:");
                        foreach (Models.Contracts.Table table in tableRepo.All())
                        {
                            Console.WriteLine("{0} {1} {2}", table.Id, table.SmallBlind, table.BigBlind);
                        }
                        break;
                    case "41":
                        foreach (Peer peer in peersRepo.All())
                        {
                            Console.WriteLine("Getting tabls for {0}", peer);

                            IEnumerable<Models.Contracts.ITable> tables = tableClient.GetTablesAsync(peer.NetworkAddress).Result;

                            foreach (Models.Contracts.Table table in tables)
                            {
                                Console.WriteLine("{0} {1} {2}", table.Id, table.SmallBlind, table.BigBlind);
                            }
                        }

                        break;
                    case "6":
                        Console.Write("What is the table id?");
                        Guid tableIdToJoin = new Guid(Console.ReadLine());

                        Models.Contracts.Table tableToJoin = tableRepo.Find(tableIdToJoin);

                        Models.Messages.JoinTableRequest joinTableRequest = new Models.Messages.JoinTableRequest()
                        {
                            BitcoinAddress = carol.ToString(),
                            TableId = tableIdToJoin,
                            NewPlayer = me,
                            TimeStamp = DateTime.UtcNow
                        };

                        foreach (Peer tablePeer in tableToJoin.Peers)
                        {
                            Console.WriteLine("Posting joing request to {0}", tablePeer);
                            var joinTableResponse = tableClient.Join(tablePeer.NetworkAddress, joinTableRequest).Result;

                            Console.WriteLine(joinTableResponse);
                        }

                        //Console.WriteLine("{0} has been added to table {1}");
                        break;
                    case "7":
                        Console.Write("What is the table id?");
                        Guid tableToBuyIn = new Guid(Console.ReadLine());

                        Console.Write("Buy in amount in satoshi?");

                        UInt64 buyInAmount = Convert.ToUInt64(Console.ReadLine());
                        tableClient.BuyIn("", null);
                        break;
                    case "k":
                    case "K":
                        CreateKeys(52, 16);
                        break;
                    case "R":
                        String response = RefreshAsync().Result;
                        Console.WriteLine(response);
                        break;
                }

                command = Console.ReadLine();
            }


            var cards = ConvertToByteArray(@"C:\Users\lucas.cullen\Source\Repos\bitpoker\headsupcolddeck.txt");

            Mnemonic mnemo = new Mnemonic("test", Wordlist.English);

            var key = mnemo.DeriveExtKey();
            var x = key.PrivateKey;

            var s = x.GetBitcoinSecret(Network.Main);
            var b = s.GetAddress();


            Console.WriteLine(b);
            Console.ReadKey();

            IDeck deck = new FiftyTwoCardDeck();
            deck.Shuffle(null);

            DumpToDisk(deck.Cards, "deck.txt");
		}

        private static void Server_MessageEvent(object sender, MessageArgs e)
        {
            Console.WriteLine(e.Message);
        }

        /// <summary>
        /// Write values to disk
        /// </summary>
        /// <param name="data">Data.</param>
        public static void DumpToDisk(IEnumerable<Byte[]> data, String filePath)
		{
			using(StreamWriter writetext = new StreamWriter(filePath))
			{
				foreach (Byte[] row in data) 
				{
					String rowAsBase64 = Convert.ToBase64String (row);
					writetext.WriteLine(rowAsBase64);
				}
			}
		}

        public static IList<Byte[]> ConvertToByteArray(String filePath)
        {
            if (File.Exists(filePath))
            {
                List<Byte[]> data = new List<byte[]>();
                using (System.IO.StreamReader file = new System.IO.StreamReader(filePath))
                {
                    Int32 value = 0;
                    String card = file.ReadLine().ToUpper();
                    switch(card.ToUpper().Substring(0, 1))
                    {
                        case "A":
                            value += 12;
                            break;
                        case "K":
                            value += 11;
                            break;
                        case "Q":
                            value += 10;
                            break;
                        case "J":
                            value += 9;
                            break;
                        case "T":
                            value += 8;
                            break;
                        default:
                            value += Convert.ToInt32(card.ToUpper().Substring(0, 1)) - 2;
                            break;
                    }

                    switch (card.ToUpper().Substring(1, 1))
                    {
                        case "S":
                            value += 0;
                            break;
                        case "C":
                            value += 13;
                            break;
                        case "H":
                            value += 26;
                            break;
                        case "D":
                            value += 39;
                            break;
                    }

                    Byte[] b = new Byte[1];
                    //b = Convert.to(value);
                    //data.Add(b);
                    return data;
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private static void CreateHandChain()
        {
            //Create a hand chain for example.
            //TexasHoldemPlayer alice2 = new TexasHoldemPlayer()
            //{
            //    BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
            //};


            //IDestination d = new BitcoinScriptAddress("2NCSTuV27SC1BF122Xe1wmkNkjo4MJw4W85", NBitcoin.Network.TestNet);
            //var alice_tx = CreateTransaction(alice_secret, d, 10000).Result;
            //String alice_tx_hex = alice_tx.ToHex();

            Repository.IHandRepository handRepo = new Repository.Mocks.HandRepository();
            var hand = handRepo.Find(new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"));

            //String json = JsonConvert.SerializeObject(hand);


            //Repository.MockTableRepo tableRepo = new Repository.MockTableRepo();
            //var tables = tableRepo.All();

            //String tableJSON = JsonConvert.SerializeObject(tables);
        }

        /// <summary>
        /// Add a player to the stub api.
        /// </summary>
        public static void AddPlayer()
        {
            AddPlayer(API_URL);
        }

        /// <summary>
        /// Add a player to the stub api.
        /// </summary>
        public static void AddPlayer(String url)
        {
            Models.Messages.AddPlayerRequest message = new Models.Messages.AddPlayerRequest();
            message.BitcoinAddress = carol.ToString();
            message.Player = new TexasHoldemPlayer()
            {
                BitcoinAddress = carol.ToString(),
                IPAddress = "localhost"
            };

            //message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            IRequest request = new Models.Messages.RPCRequest()
            {
                Method = "AddPlayerRequest"
            };

            request.Params = message;
            //request.Signature = alice_secret.PrivateKey.SignMessage(JsonConvert.SerializeObject(request));

            String json = JsonConvert.SerializeObject(request);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        String responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(responseContent);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        ///// <summary>
        ///// Add a player to the stub api.
        ///// </summary>
        //public static async Task AddPlayerAsync()
        //{
        //    Models.Messages.AddPlayerRequest message = new Models.Messages.AddPlayerRequest();
        //    message.BitcoinAddress = carol.ToString();
        //    message.Player = new PlayerInfo() { BitcoinAddress = carol.ToString(), IPAddress = "localhost" };

        //    //message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());
        //    Models.IRequest request = new Models.Messages.RPCRequest()
        //    {
        //        Method = "AddPlayerRequest"
        //    };

        //    request.Params = message;

        //    String json = JsonConvert.SerializeObject(message);
        //    StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    String url = String.Format("{0}players", API_URL);

        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        using (HttpResponseMessage responseMessage = await httpClient.PostAsync(url, requestContent))
        //        {
        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                String responseContent = responseMessage.Content.ReadAsStringAsync().Result;
        //                Console.WriteLine(responseContent);
        //            }
        //            else
        //            {
        //                throw new InvalidOperationException();
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// Adds a table to mock api under carols address
        /// </summary>
        [Obsolete]
        private static String AddTableToAPI(UInt64 sb, UInt64 bb)
        {
            Console.WriteLine("Adds a table to mock api under carols address");

            Models.Contracts.Table table = new Models.Contracts.Table(2, 10)
            {
                SmallBlind = sb,
                BigBlind = bb,
                HashAlgorithm = "SHA256"
            };

            Models.Messages.AddTableRequest message = new Models.Messages.AddTableRequest();
            
            //message.BitcoinAddress = carol.ToString();
            //message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            IRequest request = new Models.Messages.RPCRequest()
            {
                Method = "AddTableRequest"
            };

            request.Params = message;
            
            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}tables", API_URL);

            String response = Post(requestContent, url);
            return response;
        }

        private static String Post(StringContent requestContent, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
                {
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        private static void CreateKeys(Int32 n, Int32 keyLength)
        {
            _keys = new List<byte[]>(n);
            SecureRandom random = new SecureRandom();
            for (Int32 i = 0; i < n; i++)
            {
                Byte[] key = new Byte[keyLength];
                
                random.NextBytes(key);
                _keys.Add(key);
                Console.WriteLine(Convert.ToBase64String(key));
            }
        }

        private static async Task<Transaction> CreateTransaction(BitcoinSecret secret, IDestination address, Int64 amount)
        {
            //Alice tx
            //d74b4bfc99dd46adb7c30877cc3ce7ea13feb51a6fab3b9b15f75f4e213ac0da
            BlockrTransactionRepository repo = new BlockrTransactionRepository(Network.TestNet);
            IEnumerable<Coin> utxos = await repo.GetUnspentAsync(secret.GetAddress().ToString());

            //Coin[] coins = utxos.OrderByDescending(u => u.Amount).Select(u => new Coin(u.Outpoint, u.TxOut)).ToArray();

            TransactionBuilder txBuilder = new TransactionBuilder();
            Transaction tx = txBuilder
                .AddCoins(utxos)
                .AddKeys(secret)
                .Send(address, new Money(amount))
                .SendFees(new Money(10000))
                .SetChange(address)
                .BuildTransaction(true);

            return tx;
        }

        private static async Task CreateLightningTx()
        {
            //Begin lightning test
            //For testing

            NBitcoin.Transaction aliceFunding = new NBitcoin.Transaction()
            {
                Outputs =
                {
                    new NBitcoin.TxOut("1.0", alice)
                }
            };

            NBitcoin.Coin[] aliceCoinsx = aliceFunding
                .Outputs
                .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(aliceFunding.GetHash(), i), o))
                .ToArray();

            //Create 2 of 2
            NBitcoin.Script table = NBitcoin.PayToMultiSigTemplate
                        .Instance
                        .GenerateScriptPubKey(2, new[] { alice_secret.PubKey, alice_secret.PubKey });


            Console.WriteLine(table);
            Console.WriteLine(table.Hash.GetAddress(NBitcoin.Network.TestNet));

            NBitcoin.IDestination tableAddress = table.Hash.GetAddress(NBitcoin.Network.TestNet);


            var blockr = new NBitcoin.BlockrTransactionRepository(NBitcoin.Network.TestNet);
            Transaction aliceTx = blockr.GetAsync(new NBitcoin.uint256("f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3")).Result;
            NBitcoin.Transaction bobTx = blockr.GetAsync(new NBitcoin.uint256("c60193a33174a1252df9deb522bac3e5532e0c756d053e4ac9999ca17a79c74e")).Result;

            NBitcoin.Coin[] alicCoins = aliceTx
                .Outputs
                .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(aliceTx.GetHash(), i), o))
                .ToArray();

            Coin[] bobCoins = bobTx
                .Outputs
                .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(bobTx.GetHash(), i), o))
                .ToArray();

            var txBuilder = new TransactionBuilder();

            var tx_alice = txBuilder
                .AddKeys(alice_secret.PrivateKey)
                .AddCoins(alicCoins)
                .Send(tableAddress, new NBitcoin.Money(5000000))
                .SetChange(alice)
                .SendFees(NBitcoin.Money.Coins(0.001m))
                .BuildTransaction(true);

            //Console.WriteLine(tx_alice.ToHex());

            string signature = alice_secret.PrivateKey.SignMessage("1msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv398b5fe2-da27-4772-81ce-37fa615719b52CALL 5000000");
            Console.WriteLine(signature);

            Console.ReadKey();

            var tx = txBuilder
                .AddKeys(bob_secret.PrivateKey)
                .AddCoins(bobCoins)
                .Send(tableAddress, new NBitcoin.Money(100000))
                .SetChange(bob)
                .SendFees(NBitcoin.Money.Coins(0.001m))
                .BuildTransaction(true);

            Boolean ok = txBuilder.Verify(tx);

            signature = bob_secret.PrivateKey.SignMessage("CALL 100000");
            Console.WriteLine(signature);

            //BE.Providers.Blockr blockr = new BE.Providers.Blockr(true);
            //return blockr.BroadCastTx(tx.ToHex());

            Console.WriteLine(tx.ToHex());
        }

        public static async Task<String> RefreshAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(String.Format("{0}mocks", API_URL));
                String json = await httpClient.GetStringAsync(uri);

                return json;
            }
        }

        private static void Connect()
        {
            //clientSocket.Connect("127.0.0.1", 8888);
        }

        /// <summary>
        /// Creates the buy in tx
        /// </summary>
        /// <param name="address"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static async Task<String> CreateBuyInTx(String tableAddress, UInt64 amount)
        {
            BlockrTransactionRepository repo = new BlockrTransactionRepository(NBitcoin.Network.TestNet);
            IEnumerable<Coin> utxos = await repo.GetUnspentAsync(carol.ToString());

            TransactionBuilder builder = new TransactionBuilder();
            builder.AddCoins(utxos)
                .AddKeys(carol_secret)
                .BuildTransaction(true);

            //return builder.trans

            return "";
        }

        private static void Log(String message)
        {
            Console.WriteLine("{0} {1}", DateTime.UtcNow, message);
        }
    }
}