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
		private readonly String account0 = "0x815b55b3a3bb64b60483aba75c57293dd4c0bbc7";
		private readonly String account1 = "0xb712a7797a7d52fe92d17a5e251aa19784cd18b0";
		private readonly String CONTRACT_ADDRESS = "0xb044b539e539ec4ab9ecbf32745228604571381c";
		
		[Test()]
		public async Task Should_List_Account()
		{
			var response = await web3.Personal.ListAccounts.SendRequestAsync();
			Assert.IsTrue(response.Length > 0);
			Assert.AreEqual(response[0], account0);
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
			var bytes = GetBytesFromFile(CONTRACT_PATH + "Cashier" + ".bin"); 

			//Deploy the contract

			String contractHash = await web3.Eth.DeployContract.SendRequestAsync(bytes, account0, new Nethereum.Hex.HexTypes.HexBigInteger(1000000));
			Assert.IsNotNull(contractHash);
			
			var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash);

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(contractHash);
			}
			
			Assert.IsNotNull(receipt);
		}
		
		[Test()]
		public async Task Should_Call_Buy_Function()
		{
			var abi = GetABIFromFile(CONTRACT_PATH + "Cashier" + ".abi");

			var contractAddress = CONTRACT_ADDRESS; //"0x936595eef5b015f0a0c6e5d4c7aa6d52dfccf3a3";
			var contract = web3.Eth.GetContract(abi, contractAddress);
			var buyFunction = contract.GetFunction("buy");
			Assert.IsNotNull(buyFunction);

			var amount = await buyFunction.CallAsync<Int64>();
		}
		
		[Test()]
		public async Task Should_Tx_Buy_Function()
		{
			Boolean response = await web3.Personal.UnlockAccount.SendRequestAsync(account1, "Test", 120);
			Assert.IsTrue(response);
			
			var abi = GetABIFromFile(CONTRACT_PATH + "Cashier" + ".abi");

			var contractAddress = CONTRACT_ADDRESS; //"0x936595eef5b015f0a0c6e5d4c7aa6d52dfccf3a3";
			var contract = web3.Eth.GetContract(abi, contractAddress);
			var buyFunction = contract.GetFunction("buy");
			
			var tx = await buyFunction.SendTransactionAsync(account1, new Nethereum.Hex.HexTypes.HexBigInteger(30000), new Nethereum.Hex.HexTypes.HexBigInteger(1));
			
			Assert.IsNotNull(tx);
			
			var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(tx);

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(tx);
			}
			
			Assert.IsNotNull(receipt);
		}
		
		[Test()]
		public async Task Should_Get_Mapping_Function()
		{
			Boolean response = await web3.Personal.UnlockAccount.SendRequestAsync(account1, "Test", 120);
			Assert.IsTrue(response);
			
			var abi = GetABIFromFile(CONTRACT_PATH + "Cashier" + ".abi");

			var contractAddress = CONTRACT_ADDRESS; //"0x936595eef5b015f0a0c6e5d4c7aa6d52dfccf3a3";
			var contract = web3.Eth.GetContract(abi, contractAddress);
			var buyFunction = contract.GetFunction("buy");
			
			var tx = await buyFunction.SendTransactionAsync(account1, new Nethereum.Hex.HexTypes.HexBigInteger(30000), new Nethereum.Hex.HexTypes.HexBigInteger(1));
			Assert.IsNotNull(tx);
			var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(tx);

			while (receipt == null)
			{
				System.Threading.Thread.Sleep(3000);
				receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(tx);
			}
			
			var balanceOfFunction = contract.GetFunction("balanceOf");
			Int64 return1 = await balanceOfFunction.CallAsync<Int64>(account1);
			Assert.IsNotNull(return1);
			Assert.IsTrue(return1 > 0);
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
