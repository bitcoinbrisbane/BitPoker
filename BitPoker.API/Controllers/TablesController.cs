using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class TablesController : ApiController
    {
        public Models.Contracts.Table Get(Guid id)
        {
            return new Models.Contracts.Table(2, 10) { Id = id };
        }
    }
}
