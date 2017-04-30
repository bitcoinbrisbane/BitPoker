using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace BitPoker.ETH.Tests
{
	[TestFixture()]
	public class Test
	{
		private readonly Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3();
		private static String CONTRACT_PATH = @"/Users/lucascullen/GitHub/BitcoinBrisbane/BitPoker/bin/BitPoker.ETH.Contracts/";
		private readonly String account0 = "0xb096be53f1efd2e3244515f80ec70c33640d9f9b";
		
		[Test()]
		public async Task Should_List_Account()
		{
			var response = await web3.Personal.ListAccounts.SendRequestAsync();
			Assert.IsTrue(response.Length > 0);
		}
		
		[Test()]
		public async Task Should_Unlock_Account()
		{
			Boolean response = await web3.Personal.UnlockAccount.SendRequestAsync(account0, "Test", 120);
			Assert.IsTrue(response);
		}
		
		[Test()]
		public async Task Should_Deploy_Contract()
		{
			Boolean unlockResponse = await web3.Personal.UnlockAccount.SendRequestAsync(account0, "Test", 120);

			//Contract
			var bytes = GetBytesFromFile(CONTRACT_PATH + "Chip" + ".bin"); 

			//Deploy the contract

			String contractHash = web3.Eth.DeployContract.SendRequestAsync(bytes, account0, new Nethereum.Hex.HexTypes.HexBigInteger(1000000)).Result;
			Assert.IsNotNullOrEmpty(contractHash);
			
			var receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash).Result;
			}
		}
		
		[Test()]
		public async Task Should_Call_Buy_Function()
		{
			var abi = GetABIFromFile(CONTRACT_PATH + "cashier" + ".abi");

			var contractAddress = "0x3e26ec0c77bb35c8e3fb4693eceb9f109f2a94d9e155a6283ce57e26678f4d5d";
			var contract = web3.Eth.GetContract(abi, contractAddress);
			var buyFunction = contract.GetFunction("buy");
			
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
