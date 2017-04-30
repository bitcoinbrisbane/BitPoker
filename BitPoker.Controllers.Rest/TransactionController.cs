using System;
using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace BitPoker.Controllers.Rest
{
	public class TransactionController : BaseController
	{
		[HttpGet]
		public async Task<String> Get(Int64 amount, String address, Int64 minersFee = 1000)
		{
			//Helper method to create a buy in transaction
			//TODO:  WRAP AND INJECT REPO
			NBitcoin.BlockrTransactionRepository repo = new NBitcoin.BlockrTransactionRepository(NBitcoin.Network.TestNet);

			var utxos = await repo.GetUnspentAsync(base.LocalBitcoinAddress.ToString());
			NBitcoin.Coin[] coins = utxos.OrderByDescending(u => u.Amount).Select(u => new NBitcoin.Coin(u.Outpoint, u.TxOut)).ToArray();

			NBitcoin.Money fee = new NBitcoin.Money(minersFee);
			NBitcoin.IDestination destination = NBitcoin.BitcoinAddress.Create(address, base.Network);

			var txBuilder = new NBitcoin.TransactionBuilder();
			var tx = txBuilder
				.AddCoins(coins)
				.AddKeys(base.Secret)
				.Send(destination, new NBitcoin.Money(amount))
				.SendFees(minersFee)
				.SetChange(base.LocalBitcoinAddress)
				.BuildTransaction(true);

			Debug.Assert(txBuilder.Verify(tx)); //check fully signed

			return tx.ToHex();
		}
	}
}