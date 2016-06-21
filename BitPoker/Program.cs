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

namespace BitPoker
{
	public class MainClass
	{
		private static IList<Byte[]> TableDeck;

		private static Stack<String> actions;

		private static ICollection<TexasHoldemPlayer> Players;

		private static NetworkClient _client;
		private static TcpListener listener;

        /// <summary>
        /// Console for test code
        /// </summary>
        /// <param name="args"></param>
		public static void Main (string[] args)
		{
            const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

            BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);

            BitcoinAddress alice = alice_secret.GetAddress();
            BitcoinAddress bob = bob_secret.GetAddress();

            //Create a hand chain for example.
            //TexasHoldemPlayer alice2 = new TexasHoldemPlayer()
            //{
            //    BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",

            //};

            Models.Messages.AddPlayerRequest message = new Models.Messages.AddPlayerRequest();
            message.BitcoinAddress = alice.ToString();
            message.Player = new PlayerInfo() { BitcoinAddress = alice.ToString(), IPAddress = "localhost" };

            message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            String json = JsonConvert.SerializeObject(message);
            StringContent requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            String url = String.Format("{0}players", "https://bitpoker.azurewebsites.net/api/");
            HttpClient httpClient = new HttpClient();

            using (HttpResponseMessage responseMessage = httpClient.PostAsync(url, requestContent).Result)
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    String responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            //String aliceJSON = Newtonsoft.Json.JsonConvert.SerializeObject(alice2);

            Console.WriteLine("1 Parse deck");
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
            NBitcoin.Transaction aliceTx = blockr.GetAsync(new NBitcoin.uint256("f5c5e008f0cb9fc52487deb7531a8019e2d78c51c3c40e53a45248e0712102a3")).Result;
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
    }
}
