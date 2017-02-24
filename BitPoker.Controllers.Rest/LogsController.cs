using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace BitPoker.Controllers.Rest
{
    public class LogsController : BaseController
    {
        [HttpGet]
        public IEnumerable<Models.Log> Get(Int32 max = 20)
        {
			return LogRepo.All().OrderByDescending(l => l.TimeStamp).Take(max);
        }
    }
}