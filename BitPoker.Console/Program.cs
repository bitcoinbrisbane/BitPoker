using System;
using NBitcoin;

namespace BitPoker.Console
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			//n4SKTwh8xxNMSH7uN2xRZym7iXCZNwy8vj
			//NBitcoin.IssuanceCoin
			//KwNCQvNd66JJcihC7Tp96gvGjT4pCckeomrExQHHSig6nknUWGYV
			//BitcoinSecret alice = new BitcoinSecret("KwNCQvNd66JJcihC7Tp96gvGjT4pCckeomrExQHHSig6nknUWGYV", Network.Main);
			//String sig = alice.PrivateKey.SignMessage("I, Lucas, am the owner of address 1ETFquqKhv4skLJeREh4LTZ46pcD1w5LZe");

			//System.Console.Write(sig);
			BitcoinSecret alice = new BitcoinSecret("93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS", Network.TestNet);

			System.Console.WriteLine(alice.GetAddress().ToColoredAddress());

			System.Console.ReadLine();

			System.Console.WriteLine("1. List known peers");

		}

		private void ListPeers()
		{
			
		}
	}
}