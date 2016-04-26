using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;

namespace BitPoker.API.Controllers
{
    public class TablesController : ApiController
    {
        private readonly BitPoker.Repository.ITableRepository repo;

        public TablesController()
        {
            this.repo = new BitPoker.Repository.MockRepo();
        }

        public IEnumerable<BitPoker.Models.Contracts.Table> Get()
        {
            return repo.All();
        }

        public BitPoker.Models.Contracts.Table Get(Guid id)
        {
            return repo.Find(id);
        }
    }
}