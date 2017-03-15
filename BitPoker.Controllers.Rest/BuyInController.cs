using System;
using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using BitPoker.Models.ExtensionMethods;

namespace BitPoker.Controllers.Rest
{
	public class BuyInController : BaseController
	{
		public BitPoker.Repository.ITableRepository TableRepo { get; set; }

		public BitPoker.Repository.IPeerRepository PeersRepo { get; set; }

		[HttpGet]
		public String Get(Guid tableId)
		{
			var table = TableRepo.Find(tableId);

			if (table != null)
			{
				return table.GetScriptAddress();
			}
			else
			{
				throw new ArgumentException("Table not found");
			}
		}

		[HttpPost, ActionName("buyin")]
		public Models.Messages.BuyInResponse Post(Models.Messages.BuyInRequest request)
		{
			var table = this.TableRepo.Find(request.TableId);

			if (table != null)
			{
				//var peer = this.PeersRepo.All().First(p => p.BitcoinAddress == request.BitcoinAddress);

				//if (peer != null)
				//{
					Boolean isValid = base.ValidateTx(request.Tx);

					if (isValid)
					{
						//var utxos = tx.Outputs;
						//var sum = tx.Outputs.Sum(u => u.Value);

						//NBitcoin.PubKey tablePubkey = new NBitcoin.PubKey(table.PublicKey);
						//NBitcoin.PubKey requestPubKey = new NBitcoin.PubKey(peer.PublicKey);


						//Useful to check msig
						//curl -d '{"pubkeys": ["02c716d071a76cbf0d29c29cacfec76e0ef8116b37389fb7a3e76d6d32cf59f4d3", "033ef4d5165637d99b673bcdbb7ead359cee6afd7aaf78d3da9d2392ee4102c8ea", "022b8934cc41e76cb4286b9f3ed57e2d27798395b04dd23711981a77dc216df8ca"], "script_type": "multisig-2-of-3"}' https://api.blockcypher.com/v1/btc/main/addr
						//NBitcoin.Script tableRedeemScript = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new NBitcoin.PubKey[] { tablePubkey, requestPubKey });

						return new Models.Messages.BuyInResponse()
						{
							Id = request.Id,
							TimeStamp = DateTime.UtcNow
							//RedeemScript = tableRedeemScript.ToString()
						};
					}
					else
					{
						throw new Exception();
					}
				//}
				//else
				//{
				//	throw new ArgumentException("Peer not found or not seated");
				//}
			}
			else
			{
				throw new ArgumentException("Table id not found");
			}
		}
	}
}