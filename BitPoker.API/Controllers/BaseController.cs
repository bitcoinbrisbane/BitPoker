using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        public Boolean Verify()
        {
            return false;
        }
    }
}
