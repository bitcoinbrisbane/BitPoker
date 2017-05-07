using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace BitPoker.Controllers.Rest
{
    public class AIActionController : BaseController, IActionController
    {
	public BitPoker.Repository.IHandRepository HandRepo { get; set; }

	public BitPoker.Repository.IMessagesRepository MessageRepo { get; set; }

	public AIActionController()
	{
		base.PrivateKey = System.Configuration.ConfigurationManager.AppSettings["BitcoinPrivateKey"];
	}

	[HttpGet]
	public IEnumerable<BitPoker.Models.Messages.ActionMessage> Get(string id)
	{
		return MessageRepo.All().Where(m => m.HandId.ToString() == id);
	}

	[HttpPost]
	public void Post(BitPoker.Models.Messages.ActionMessage message)
	{
		if (base.Verify(message) == true)
		{
			Models.Hand hand = HandRepo.All().Single(h => h.Id.ToString() == message.HandId.ToString());

			if (hand != null)
			{
				BitPoker.Models.Players.IPlayerLogic player1 = new BitPoker.Models.TexasHoldemPlayer();
				BitPoker.Models.Players.IPlayerLogic player2 = new BitPoker.Models.TexasHoldemPlayer();
					
				BitPoker.Logic.GameMechanics.TwoPlayersTexasHoldemGame holdem = new BitPoker.Logic.GameMechanics.TwoPlayersTexasHoldemGame(player1, player2);
			}
		}
	}
    }
}