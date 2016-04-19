using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class DeckController : BaseController
    {
        public BitPoker.Models.Messages.DeckMessage Get(Guid tableId, Guid handId)
        {
            return null;
        }
    }
}
