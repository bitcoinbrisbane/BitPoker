using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using BitPoker.Models;

namespace BitPoker
{
	public class MainClass
	{
		private static IList<Byte[]> TableDeck;

		private static Stack<String> actions;

		public static void Main (string[] args)
		{
			//For testing
			const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
			const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

			NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret (alice_wif, NBitcoin.Network.TestNet);
			NBitcoin.BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret (bob_wif, NBitcoin.Network.TestNet);

			NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress ();
			NBitcoin.BitcoinAddress bob_address = bob_secret.GetAddress ();

			TexasHoldemPlayer alice = new TexasHoldemPlayer () 
			{ 
				Position = 0, 
				BitcoinAddress = alice_address.ToString(),
				IsDealer = true,
				IsBigBlind = true,
				Stack = 50000000
			};

			TexasHoldemPlayer bob = new TexasHoldemPlayer () 
			{ 
				Position = 1, 
				BitcoinAddress = bob_address.ToString(),
				IsSmallBlind = true,
				Stack = 4000000,
				IpAddress = "192.168.0.1"
			};


            ////Create table contract
            //BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table () 
            //{
            //    SmallBlind = 10000,
            //    BigBlind = 20000,
            //    Id = Guid.NewGuid()
            //};

            //String json = Newtonsoft.Json.JsonConvert.SerializeObject (table);

            ////Sign message
            ////NBitcoin.Key key = NBitcoin.Key.Parse (alice_wif, NBitcoin.Network.TestNet);
            ////Byte[] signature = key.SignMessage ();


            ////Bob joins, send the table contract
            //NetworkClient.StartClient(bob.IpAddress, json);

            //NetworkClient.SendMessage (bob.IpAddress, "");

            ////Alice is dealer
            ////var result = NetworkClient.SendMessage(bob.IpAddress, "Post Small Blind");


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
