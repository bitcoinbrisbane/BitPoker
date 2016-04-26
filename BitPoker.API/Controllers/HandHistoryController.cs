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
        private readonly BitPoker.Repository.IHandRepository repo;

        public HandHistoryController()
        {
            this.repo = new Repository.InMemoryHandRepo();
        }

        public IEnumerable<Models.Messages.ActionMessage> Get(Guid handId)
        {
            return repo.All(handId)
        }
    }
}
