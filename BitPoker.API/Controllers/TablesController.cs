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

        public IEnumerable<BitPoker.Models.Contracts.Table> Get()
        {
            return repo.All();
        }

        public BitPoker.Models.Contracts.Table Get(Guid id)
        {
            return repo.Find(id);
        }

        [HttpPost]
        public void Post(BitPoker.Models.Contracts.TableRequest model)
        {
            BitPoker.Repository.ITableRepository repo = new Repository.InMemoryTableRepo();

            if (!base.Verify(model.BitcoinAddress, model.ToString(), model.Signature))
            {
                throw new Exceptions.SignatureNotValidException();
            }

            repo.Add(model.Table);
        }
    }
}