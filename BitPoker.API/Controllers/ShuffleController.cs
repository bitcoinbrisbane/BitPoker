using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class ShuffleController : ApiController
    {
        [HttpPost]
        public Models.IDeck Post(Models.IDeck deck)
        {
            return deck;
        }
    }
}
