using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Controllers.Rest
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TablesController : BaseController, ITablesController
    {
        public Repository.ITableRepository TableRepo { get; set; }

        public TablesController()
        {
            this.TableRepo = new Repository.MockTableRepo();
        }

        public TablesController(Repository.ITableRepository repo)
        {
            this.TableRepo = repo;
        }

        public IEnumerable<Models.Contracts.Table> Get()
        {
            return TableRepo.All();
        }

        public Models.Contracts.Table Get(Guid id)
        {
            return TableRepo.Find(id);
        }

        [HttpPost]
        public void Post(Models.IRequest request)
        {
            if (request.Method == "AddTableRequest")
            {
                Models.Messages.AddTableRequest model = request.Params as Models.Messages.AddTableRequest;

                if (!base.Verify(model.BitcoinAddress, model.ToString(), request.Signature))
                {
                    //throw new Exceptions.SignatureNotValidException();
                    throw new Exception();
                }
                else
                {
                    TableRepo.Add(model.Table);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}