using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

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


			//Create table contract
			BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table () 
			{
				SmallBlind = 10000,
				BigBlind = 20000,
				Id = Guid.NewGuid()
			};

			String json = Newtonsoft.Json.JsonConvert.SerializeObject (table);

			//Sign message
			//NBitcoin.Key key = NBitcoin.Key.Parse (alice_wif, NBitcoin.Network.TestNet);
			//Byte[] signature = key.SignMessage ();


			//Bob joins, send the table contract
			NetworkClient.StartClient(bob.IpAddress, json);


			//Alice is dealer
			alice.NewDeck ();


			//Swap and shuff shuffle
			//bob.SwapDeck(alice.Deck);

			//Know to all.
			TableDeck = bob.Deck;


			NetworkClient.SendMessage (bob.IpAddress, "");

			//Alice is dealer
			//var result = NetworkClient.SendMessage(bob.IpAddress, "Post Small Blind");


		}

//		public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
//		{
//			// Check arguments.
//			if (plainText == null || plainText.Length <= 0)
//				throw new ArgumentNullException("plainText");
//			
//			if (Key == null || Key.Length <= 0)
//				throw new ArgumentNullException("Key");
//			
//			if (IV == null || IV.Length <= 0)
//				throw new ArgumentNullException("Key");
//			
//			byte[] encrypted;
//
//			// Create an Aes object
//			// with the specified key and IV.
//			using (Aes aesAlg = Aes.Create())
//			{
//				aesAlg.Key = Key;
//				aesAlg.IV = IV;
//
//				// Create a decrytor to perform the stream transform.
//				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
//
//				// Create the streams used for encryption.
//				using (MemoryStream msEncrypt = new MemoryStream())
//				{
//					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//					{
//						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
//						{
//							//Write all data to the stream.
//							swEncrypt.Write(plainText);
//						}
//						encrypted = msEncrypt.ToArray();
//					}
//				}
//			}
//
//			// Return the encrypted bytes from the memory stream.
//			return encrypted;
//		}

		/// <summary>
		/// Shuffle the specified list.
		/// </summary>
		/// <param name="list">List.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Shuffle<T>(IList<T> list)
		{
			using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider ()) 
			{
				int n = list.Count;

				while (n > 1) 
				{
					byte[] box = new byte[1];
					do
						provider.GetBytes (box);
					while (!(box [0] < n * (Byte.MaxValue / n)));
					int k = (box [0] % n);
					n--;

					T value = list [k];
					list [k] = list [n];
					list [n] = value;
				}
			}
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
