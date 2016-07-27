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

        /// <summary>
        /// Get all hands played at this table
        /// </summary>
        /// <param name="id">Table Id</param>
        /// <returns></returns>
        public IEnumerable<BitPoker.Models.Messages.ActionMessage> Get(Guid id)
        {
            return _repo.All().Where(m => m.TableId.ToString() == id.ToString());
        }
    }
}
