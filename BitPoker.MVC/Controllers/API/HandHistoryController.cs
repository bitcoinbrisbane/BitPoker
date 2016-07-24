using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    public class HandHistoryController : BaseController
    {
        private readonly BitPoker.Repository.IMessagesRepository _repo;

        public HandHistoryController()
        {
        }

        public HandHistoryController(BitPoker.Repository.IMessagesRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<BitPoker.Models.Messages.ActionMessage> Get(Guid handId)
        {
            return _repo.All().Where(m => m.HandId.ToString() == handId.ToString());
        }
    }
}
