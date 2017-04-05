using System;
using System.IO;
using System.Text;

namespace BitPoker.ETH.Console
{
	class MainClass
	{
		private static String CONTRACT_PATH = @"/Users/lucascullen/GitHub/BitcoinBrisbane/BitPoker/BitPoker.ETH.Contracts/Bin/Contracts/";
		private static String CONTRACT_FILE_NAME = "poker";

		public static void Main(string[] args)
		{
			//Connect to Geth node
			Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3();
			var password = "Test";

			var accounts = web3.Personal.ListAccounts.SendRequestAsync().Result;

			for (Int32 i = 0; i < accounts.Length; i++)
			{
				var balance = web3.Eth.GetBalance.SendRequestAsync(accounts[i]).Result;
				System.Console.WriteLine(accounts[i] + " " + balance.Value);
			}

			//var newAccountResponse = web3.Personal.NewAccount.SendRequestAsync(password).Result;
			//accounts = web3.Personal.ListAccounts.SendRequestAsync().Result;

			Boolean unlockResponse = web3.Personal.UnlockAccount.SendRequestAsync(accounts[0], password, 120).Result;

			//

			//Contract
			var bytes = GetBytesFromFile(CONTRACT_PATH + CONTRACT_FILE_NAME + ".bin"); 

			//Deploy the contract

			String contractHash = web3.Eth.DeployContract.SendRequestAsync(bytes, accounts[0], new Nethereum.Hex.HexTypes.HexBigInteger(1000000)).Result;
			//const String txHash = "0x2f432ba0d2b8047a552ea6d6907be67ffbfa679f8f52a04df0876032c4d77409";

			//Write out the response from the contract
			var isMining = web3.Eth.Mining.IsMining.SendRequestAsync().Result;

			if (isMining != true)
			{
				var mResult = web3.Miner.Start.SendRequestAsync().Result;
			}

			var receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;
			System.Console.Write("Processing");

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;
				System.Console.Write(".");
			}

			if (receipt != null)
			{
				System.Console.WriteLine(receipt.BlockHash);
			}

			var abi = GetABIFromFile(CONTRACT_PATH + CONTRACT_FILE_NAME + ".abi");

			var contractAddress = receipt.ContractAddress;
			var contract = web3.Eth.GetContract(abi, contractAddress);


			var lodgeFunction = contract.GetFunction("lodge");
			object[] inputs = new object[3] { 100000, 11000, 30000 };

			var estimated = lodgeFunction.CallAsync<Int64>(inputs).Result;
			System.Console.WriteLine(estimated);

			unlockResponse = web3.Personal.UnlockAccount.SendRequestAsync(accounts[1], password, 120).Result;

			if (unlockResponse == true)
			{
				var estimatedTx = lodgeFunction.SendTransactionAsync(accounts[1], inputs).Result;
				System.Console.WriteLine(estimatedTx);
				receipt = null;

				while (receipt == null)
				{
					System.Threading.Thread.Sleep(3000);
					receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(estimatedTx).Result;
					System.Console.Write(".");
				}
			}

			var individualReturns = contract.GetFunction("individualReturns");
			//var return0 = individualReturns.CallDeserializingToObjectAsync<DTOs.IndividualReturn>("0xb712a7797a7d52fe92d17a5e251aa19784cd18b0").Result;
		}

		private static string GetABIFromFile(String path)
		{
			string abi = File.ReadAllText(path, Encoding.UTF8);
			return abi;
		}

		private static string GetBytesFromFile(String path)
		{
			var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
			{
				String text = streamReader.ReadToEnd();
				return "0x" + text;
			}
		}
	}
}