using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class HandHistoryController : BaseController
    {
        public IEnumerable<Models.Messages.ActionMessage> Get(Guid tableId, Guid handId)
        {
            var hand = base.GetHandFromCache(tableId, handId);
            return hand.History;
        }
    }
}
