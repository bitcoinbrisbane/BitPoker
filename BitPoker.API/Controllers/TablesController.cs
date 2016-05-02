using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class TablesController : BaseController
    {
        private readonly BitPoker.Repository.ITableRepository repo;

        public TablesController()
        {
            this.repo = new BitPoker.Repository.MockTableRepo();
        }

        public IEnumerable<Models.Contracts.Table> Get()
        {
            return repo.All();
        }

        public Models.Contracts.Table Get(Guid id)
        {
            return repo.Find(id);
        }

        public void Post(Models.Contracts.Table model)
        {
            var repo = new BitPoker.API.Repository.InMemoryTableRepo();
            
        }
    }
}