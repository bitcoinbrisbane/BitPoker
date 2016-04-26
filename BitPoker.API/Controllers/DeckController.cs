using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class DeckController : BaseController
    {
        /// <summary>
        /// Get deck for mock hand
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="handId"></param>
        /// <returns></returns>
        public BitPoker.Models.Messages.DeckResponseMessage Get(Guid tableId, Guid handId)
        {
            //As its heads up, create the first hand and deck
            Models.Hand hand = base.GetHandFromCache(tableId, handId);

            //Assume alice
            const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();

            BitPoker.Models.Messages.DeckResponseMessage response = new BitPoker.Models.Messages.DeckResponseMessage()
            {
                TableId = tableId,
                HandId = handId,
                BitcoinAddress = alice_address.ToString(),
                Deck = hand.Deck
            };

            String message = response.ToString();
            response.Signature = alice_secret.PrivateKey.SignMessage(message);

            return response;
        }
    }
}
