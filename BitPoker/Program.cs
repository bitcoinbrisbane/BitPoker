using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using BitPoker.Models;
using NBitcoin;

namespace BitPoker
{
	public class MainClass
	{
		private static IList<Byte[]> TableDeck;

		private static Stack<String> actions;

		private static ICollection<TexasHoldemPlayer> Players;

		private static NetworkClient _client;
		private static TcpListener listener;

		public static void Main (string[] args)
		{

            //Mnemonic mnemo = new Mnemonic("build story spirit chuckle wire sketch check make finger seven spy situate", Wordlist.English);
            Mnemonic mnemo = new Mnemonic("antenna endless unknown between glow lucky shed season master bomb tunnel fashion", Wordlist.English);
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
			const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
			const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

			NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret (alice_wif, NBitcoin.Network.TestNet);
			NBitcoin.BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret (bob_wif, NBitcoin.Network.TestNet);

			NBitcoin.BitcoinAddress alice = alice_secret.GetAddress ();
			NBitcoin.BitcoinAddress bob = bob_secret.GetAddress ();

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
	}
}
