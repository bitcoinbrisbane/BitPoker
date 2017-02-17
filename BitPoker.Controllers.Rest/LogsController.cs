using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    [Route("api/[controller]")]
    public class LogsController : ApiController
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}