using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public class TablesController : BaseController, ITablesController
    {
        public BitPoker.Repository.ITableRepository TableRepo { get; set; }

        public TablesController()
        {
            this.TableRepo = new BitPoker.Repository.MockTableRepo();
        }

        [HttpGet]
        public IEnumerable<Models.Contracts.Table> Get()
        {
            AddLog("Get tables");
            return TableRepo.All();
        }

        [HttpGet]
        public Models.Contracts.Table Get(Guid id)
        {
            AddLog("Get table");
            return TableRepo.Find(id);
        }

		[Authorize]
		[HttpPost]
		public void AddTable(Models.Messages.AddTableRequest request)
		{
			TableRepo.Add(request.Table);
		}

        [HttpPost, Route("join")]
        public Models.Messages.JoinTableResponse JoinTable(Models.Messages.JoinTableRequest request)
		{
			if (base.Verify(request.BitcoinAddress, request.ToString(), request.Signature) == true)
			{
				Models.Messages.JoinTableResponse response = new Models.Messages.JoinTableResponse();
				Models.Contracts.Table table = this.TableRepo.Find(request.TableId);

				if (table != null && table.Peers[request.Seat] == null)
				{
					for (Int32 i = 0; i < table.MaxPlayers; i++)
					{
						if (table.Peers[i] == null)
						{
							response.Seat = i;
							table.Peers[i] = request.NewPlayer;

							TableRepo.Update(table);

							break;
						}
					}

					response.Seat = request.Seat;

					return response;
				}
				else
				{
					throw new ArgumentException("Table id not found");
				}
			}
			else
			{
				throw new ArgumentException("Invalid signature");
			}
        }

        [HttpPost, Route("buyin")]
        public Models.Messages.BuyInResponse BuyIn(Models.Messages.BuyInRequest request)
        {
			Models.Messages.BuyInResponse response = new Models.Messages.BuyInResponse();
            var table = this.TableRepo.Find(request.TableId);

            if (table != null)
			{
				var peer = table.Peers.First(p => p.BitcoinAddress == request.BitcoinAddress);

				if (peer != null)
				{
					Boolean isValid = base.ValidateTx(request.Tx);

					if (isValid)
					{
						//var utxos = tx.Outputs;
						//var sum = tx.Outputs.Sum(u => u.Value);

						NBitcoin.PubKey tablePubkey = new NBitcoin.PubKey(table.PublicKey);
						NBitcoin.PubKey requestPubKey = new NBitcoin.PubKey(peer.PublicKey);


						//Useful to check msig
						//curl -d '{"pubkeys": ["02c716d071a76cbf0d29c29cacfec76e0ef8116b37389fb7a3e76d6d32cf59f4d3", "033ef4d5165637d99b673bcdbb7ead359cee6afd7aaf78d3da9d2392ee4102c8ea", "022b8934cc41e76cb4286b9f3ed57e2d27798395b04dd23711981a77dc216df8ca"], "script_type": "multisig-2-of-3"}' https://api.blockcypher.com/v1/btc/main/addr
						NBitcoin.Script tableRedeemScript = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new NBitcoin.PubKey[] { tablePubkey, requestPubKey });

						return new Models.Messages.BuyInResponse() 
						{ 
							Id = request.Id, 
							TimeStamp = DateTime.UtcNow, 
							RedeemScript = tableRedeemScript.ToString() 
						};
					}
					else
					{
						throw new Exception();
					}
				}
				else
				{
					throw new ArgumentException("Peer not found or not seated");
				}
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }
        }

		[HttpPost, Route("deal")]
		public Models.Messages.DealResponse Deal(Models.Messages.DealRequest request)
		{
			
			return new Models.Messages.DealResponse() { Id = request.Id, TimeStamp = DateTime.UtcNow };
		}
    }
}