using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class HandHistoryController : BaseController
    {
        private readonly BitPoker.Repository.IMessagesRepository repo;

        public HandHistoryController()
        {
            //this.repo = new Repository.InMemoryHandRepo();
        }

        public IEnumerable<BitPoker.Models.Messages.ActionMessage> Get(Guid handId)
        {
            return repo.All().Where(m => m.HandId.ToString() == handId.ToString());
        }
    }
}
