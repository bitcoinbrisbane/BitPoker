using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BitPoker.Net.RestHost.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Models.Log> Get()
        {
            return new List<Models.Log>();
        }
    }
}
