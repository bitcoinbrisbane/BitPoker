using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using BitPoker.Models;

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

            NBitcoin.IDestination msigAddress = table.Hash.GetAddress(NBitcoin.Network.TestNet);




            var blockr = new NBitcoin.BlockrTransactionRepository(NBitcoin.Network.TestNet);
            NBitcoin.Transaction transaction = blockr.GetAsync(new NBitcoin.uint256("c60193a33174a1252df9deb522bac3e5532e0c756d053e4ac9999ca17a79c74e")).Result;

            NBitcoin.Coin[] coins = transaction
                        .Outputs
                        .Select((o, i) => new NBitcoin.Coin(new NBitcoin.OutPoint(transaction.GetHash(), i), o))
                        .ToArray();

            var txBuilder = new NBitcoin.TransactionBuilder();

            var tx = txBuilder
                .AddKeys(bob_secret.PrivateKey)
                .AddCoins(coins)
                .Send(msigAddress, new NBitcoin.Money(50000000))
                .SetChange(alice)
                .SendFees(NBitcoin.Money.Coins(0.001m))
                .BuildTransaction(true);

            Boolean ok = txBuilder.Verify(tx);

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
