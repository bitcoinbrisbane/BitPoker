using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    [Obsolete]
    public class DeckController : BaseController
    {
        private readonly BitPoker.Repository.IHandRepository repo;

        public DeckController()
        {
            repo = new BitPoker.Repository.Mocks.HandRepository();
        }

        /// <summary>
        /// Get deck for mock hand
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="handId"></param>
        /// <returns></returns>
        public BitPoker.Models.Messages.DeckResponse Get(Guid handId)
        {
            //As its heads up, create the first hand and deck
            BitPoker.Models.Hand hand = repo.Find(handId);

            //Assume alice
            const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();

            BitPoker.Models.Messages.DeckResponse response = new BitPoker.Models.Messages.DeckResponse()
            {
                HandId = handId,
                //BitcoinAddress = alice_address.ToString(),
                Deck = hand.Deck
            };

            String message = response.ToString();
            //response.Signature = alice_secret.PrivateKey.SignMessage(message);

            return response;
        }
    }
}
