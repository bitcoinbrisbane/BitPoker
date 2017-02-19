using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    [Route("api/[controller]")]
    public class LogsController : BaseController
    {
		public Repository.IReadOnlyRepository<Models.Log> LogRepo { get; set; }

        [HttpGet]
        public IEnumerable<Models.Log> Get()
        {
			return LogRepo.All();
        }
    }
}