using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace BitPoker.Controllers.Rest
{
    public class HandController : BaseController
    {
		public BitPoker.Repository.IHandRepository HandRepo { get; set; }

		public BitPoker.Repository.IMessagesRepository MessageRepo { get; set; }

		public HandController()
		{
			base.PrivateKey = System.Configuration.ConfigurationManager.AppSettings["BitcoinPrivateKey"];
		}

		[HttpGet]
		public IEnumerable<BitPoker.Models.Hand> Get(string id)
		{
			return HandRepo.All().Where(h => h.Id.ToString() == id);
		}

		[HttpPost]
		public void Post(BitPoker.Models.Messages.ActionMessage message)
		{
			if (base.Verify(message) == true)
			{
				Models.Hand hand = HandRepo.All().Single(h => h.Id.ToString() == message.HandId.ToString());

				if (hand != null)
				{
					
					//TexasHoldem.Logic.GameMechanics.TwoPlayersTexasHoldemGame holdem = new TexasHoldem.Logic.GameMechanics.TwoPlayersTexasHoldemGame();
				}
			}
		}
    }
}