using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class TablesController : BaseController
    {
        private readonly BitPoker.Repository.ITableRepository _repo;

        public TablesController()
        {
            this._repo = Repository.Factory.GetTableRepository();
        }

        public IEnumerable<BitPoker.Models.Contracts.Table> Get()
        {
            using (BitPoker.Repository.ITableRepository repo = Repository.Factory.GetTableRepository())
            {
                return repo.All();
            }
        }

        public BitPoker.Models.Contracts.Table Get(Guid id)
        {
            using (BitPoker.Repository.ITableRepository repo = Repository.Factory.GetTableRepository())
            {
                return repo.Find(id);
            }
        }

        [HttpPost]
        public void Post(BitPoker.Models.Contracts.TableRequest model)
        {
            if (!base.Verify(model.BitcoinAddress, model.ToString(), model.Signature))
            {
                throw new Exceptions.SignatureNotValidException();
            }

            _repo.Add(model.Table);
        }
    }
}