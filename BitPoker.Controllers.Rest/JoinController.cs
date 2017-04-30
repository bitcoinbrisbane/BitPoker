using System;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
	public class JoinController : BaseController
	{
		public BitPoker.Repository.ITableRepository TableRepo { get; set; }

		[HttpPost]
		public Models.Messages.JoinTableResponse Post(Models.Messages.JoinTableRequest request)
		{
			if (base.Verify(request.BitcoinAddress, request.ToString(), request.Signature) == true)
			{
				Models.Messages.JoinTableResponse response = new Models.Messages.JoinTableResponse();
				Models.Contracts.Table table = this.TableRepo.Find(request.TableId);

				if (table != null)
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
	}
}