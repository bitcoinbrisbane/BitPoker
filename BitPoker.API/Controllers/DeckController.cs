using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            //Models.Hand hand = new Models.Hand(players);
            Models.Hand hand = base.GetHandFromCache(tableId, handId);

            BitPoker.Models.Messages.DeckResponseMessage response = new BitPoker.Models.Messages.DeckResponseMessage()
            {
                TableId = tableId,
                HandId = handId,
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Deck = hand.Deck
            };

            String x = response.ToString();
            //sign

            return response;
        }
    }
}
