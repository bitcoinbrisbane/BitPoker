using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.MVC.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TablesController : BaseController
    {
        private readonly BitPoker.Repository.ITableRepository _repo;

        public TablesController()
        {
            this._repo = Repository.Factory.GetTableRepository();
        }

        public TablesController(BitPoker.Repository.ITableRepository repo)
        {
            this._repo = repo;
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
        public void Post(BitPoker.Models.IRequest request)
        {
            if (request.Method == "AddTableRequest")
            {
                BitPoker.Models.Messages.AddTableRequest model = request.Params as BitPoker.Models.Messages.AddTableRequest;
                Boolean valid = true; //base.Verify(model.BitcoinAddress, model.ToString(), request.Signature

                if (!valid)
                {
                    throw new Exceptions.SignatureNotValidException();
                }
                else
                {
                    _repo.Add(model.Table);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}