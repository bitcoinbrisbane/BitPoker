using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    public class ShuffleController : BaseController
    {
        private readonly BitPoker.Repository.IHandRepository repo;

        public ShuffleController()
        {
        }

        /// <summary>
        /// Get deck to shuffle
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="handId"></param>
        public BitPoker.Models.Messages.DeckResponseMessage Get(Guid tableId, Guid handId)
        {
            var message = new BitPoker.Models.Messages.DeckResponseMessage();

            const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();

            message.BitcoinAddress = alice_address.ToString();

            //if (tableId.ToString() == "" && handId.ToString() == "398b5fe2-da27-4772-81ce-37fa615719b5")
            //{
            //    //return mock shuffled deck
            //}
            //else
            //{
            //    //Get new hand
            //    var hand = repo.find(tableId, handId);
            //    hand.Deck.Shuffle();

            //    message.Signature = alice_secret.PrivateKey.SignMessage(hand.Deck.ToString());
            //}

            return message;
        }

        [HttpPost]
        public void Post()
        {
        }
    }
}
