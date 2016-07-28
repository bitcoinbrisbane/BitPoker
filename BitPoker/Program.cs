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

namespace BitPoker
{
	public class MainClass
	{
		private static IList<Byte[]> TableDeck;
        private static List<Byte[]> _keys;

        private static Stack<String> actions;

		private static ICollection<TexasHoldemPlayer> Players;

		private static NetworkClient _client;
		private static TcpListener listener;

        private const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        private const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";
        private const String carol_wif = "91rahqyxZb6R1MMq2rdYomfB8GWsLVqkBMHrUnaepxks73KgfaQ";

        private static BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
        private static BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);
        private static BitcoinSecret carol_secret = new NBitcoin.BitcoinSecret(carol_wif, NBitcoin.Network.TestNet);

        private static BitcoinAddress alice = alice_secret.GetAddress();
        private static BitcoinAddress bob = bob_secret.GetAddress();
        private static BitcoinAddress carol = carol_secret.GetAddress();

        private const String API_URL = "https://www.bitpoker.io/api/";

        private const String MOCK_HAND_ID = "398b5fe2-da27-4772-81ce-37fa615719b5";
        private const String TABLE_ID = "";

        public static async Task MainAsync()
        {

        }

        /// <summary>
        /// Console for test code
        /// </summary>
        /// <param name="args"></param>
		public static void Main (string[] args)
		{
            //Create a hand chain for example.
            //TexasHoldemPlayer alice2 = new TexasHoldemPlayer()
            //{
            //    BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
            //};

            Console.WriteLine("***");
            Console.WriteLine("This console app, under the context of Carol. {0}", carol);
            Console.WriteLine("***");

            Console.WriteLine("1. Add player (Carol) to mock api");
            Console.WriteLine("2. List players");
            Console.WriteLine("3. Add table");
            Console.WriteLine("4. List tables");
            Console.WriteLine("5. Buy in (Table ID {0})", TABLE_ID);
            Console.WriteLine("6. Fold / Muck");
            Console.WriteLine("7. Call");
            Console.WriteLine("8. Bet / Raise");
            Console.WriteLine("R Refresh");
            Console.WriteLine("K Create new keys");
            Console.WriteLine("Q Quit");

            String command = Console.ReadLine();

            while (command != "Q")
            {
                switch (command)
                {
                    case "1":
                        AddPlayer();
                        break;
                    case "2":
                        GetPlayers();
                        break;
                    case "3":
                        AddTable();
                        break;
                    case "4":
                        GetTables();
                        break;
                    case "5":
                        BuyIn(100000);
                        break;
                    case "k":
                    case "K":
                        CreateKeys(52, 16);
                        break;
                }

                command = Console.ReadLine();
            }

            //String aliceJSON = Newtonsoft.Json.JsonConvert.SerializeObject(alice2);

            
            var cards = ConvertToByteArray(@"C:\Users\lucas.cullen\Source\Repos\bitpoker\headsupcolddeck.txt");

            Mnemonic mnemo = new Mnemonic("test", Wordlist.English);

            var key = mnemo.DeriveExtKey();
            var x = key.PrivateKey;

            var s = x.GetBitcoinSecret(Network.Main);
            var b = s.GetAddress();


            Console.WriteLine(b);
            Console.ReadKey();

            IDeck deck = new FiftyTwoCardDeck();
            deck.Shuffle();

            DumpToDisk(deck.Cards, "deck.txt");

            //NBitcoin.Key key = new NBitcoin.Key();
            //var w = key.GetWif(NBitcoin.Network.TestNet);
            //NBitcoin.BitcoinAddress a = w.GetAddress();
            //var ca = a.ToColoredAddress();

			//http://www.codeproject.com/Articles/745134/csharp-async-socket-server
			//https://code.msdn.microsoft.com/windowsdesktop/Communication-through-91a2582b/

            ////CancellationTokenSource cts = new CancellationTokenSource();
            //listener = new TcpListener(IPAddress.Any, 6666);

            //_client = new NetworkClient () { ListeningPort = 11001 };
            //_client.StartListening ();

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

            NBitcoin.Coin[] bobCoins = bobTx
                .Outputs
                .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(bobTx.GetHash(), i), o))
                .ToArray();

            var txBuilder = new NBitcoin.TransactionBuilder();

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

        /// <summary>
        /// Add a player to the stub api.
        /// </summary>
        public static void AddPlayer()
        {
            Models.Messages.AddPlayerRequest message = new Models.Messages.AddPlayerRequest();
            message.BitcoinAddress = carol.ToString();
            message.Player = new PlayerInfo() { BitcoinAddress = carol.ToString(), IPAddress = "localhost" };

            message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}players", API_URL);

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

        /// <summary>
        /// Add a player to the stub api.
        /// </summary>
        public static async Task AddPlayerAsync()
        {
            Models.Messages.AddPlayerRequest message = new Models.Messages.AddPlayerRequest();
            message.BitcoinAddress = carol.ToString();
            message.Player = new PlayerInfo() { BitcoinAddress = carol.ToString(), IPAddress = "localhost" };

            message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}players", API_URL);

            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpResponseMessage responseMessage = await httpClient.PostAsync(url, requestContent))
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

        public static void GetPlayers()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(String.Format("{0}players", API_URL));
                String json = httpClient.GetStringAsync(uri).Result;

                if (!String.IsNullOrEmpty(json))
                {
                    List<PlayerInfo> response = JsonConvert.DeserializeObject<List<PlayerInfo>>(json);

                    foreach (PlayerInfo player in response)
                    {
                        Console.WriteLine("{0} {1} {2}", player.BitcoinAddress, player.IPAddress, player.LastSeen);
                    }
                }
            }
        }

        public static async Task GetPlayersAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(String.Format("{0}players", API_URL));
                String json = await httpClient.GetStringAsync(uri);

                if (!String.IsNullOrEmpty(json))
                {
                    List<PlayerInfo> response = JsonConvert.DeserializeObject<List<PlayerInfo>>(json);

                    foreach (PlayerInfo player in response)
                    {
                        Console.WriteLine("{0} {1} {2}", player.BitcoinAddress, player.IPAddress, player.LastSeen);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a table to mock api under carols address
        /// </summary>
        private static void AddTable()
        {
            Console.WriteLine("Adds a table to mock api under carols address");

            Models.Messages.AddTableRequest message = new Models.Messages.AddTableRequest();
            message.BitcoinAddress = carol.ToString();
            message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}tables", API_URL);

            Post(requestContent, url);
        }

        private static void GetTables()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                Uri uri = new Uri(String.Format("{0}tables", API_URL));
                String json = httpClient.GetStringAsync(uri).Result;
                List<Models.Contracts.Table> response = JsonConvert.DeserializeObject<List<Models.Contracts.Table>>(json);
                
                foreach (Models.Contracts.Table table in response)
                {
                    Console.WriteLine("{0} {1} {2}", table.Id, table.SmallBlind, table.BigBlind);
                }
            }
        }

        private static void BuyIn(Int64 amount)
        {
            Models.Messages.BuyInRequestMessage message = new Models.Messages.BuyInRequestMessage();
            message.BitcoinAddress = carol.ToString();
            message.Amount = amount;
            
            message.Signature = carol_secret.PrivateKey.SignMessage(message.Id.ToString());

            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}buyin", API_URL);

            Post(requestContent, url);
        }

        private static void Post(StringContent requestContent, string url)
        {
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
    }
}
