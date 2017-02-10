using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BitPoker.Core.RestHost.Controllers
{
    [EnableCors("AllowSpecificOrigin"), Route("api/[controller]")]
    public class TablesController : BaseController, ITablesController
    {
        public Repository.ITableRepository TableRepo { get; set; }

        public TablesController()
        {
            this.TableRepo = new Repository.MockTableRepo();
        }

        [HttpGet]
        public IEnumerable<Models.Contracts.Table> Get()
        {
            AddLog("Get tables");
            return TableRepo.All();
        }

        [HttpGet("{id}")]
        public Models.Contracts.Table Get(Guid id)
        {
            AddLog("Get table");
            return TableRepo.Find(id);
        }

        [HttpPost]
        public void Post(Models.IRequest request)
        {
            //if (request.Method == "AddTableRequest")
            switch (request.Method)
            {
                case "AddTableRequest":
                    Models.Messages.AddTableRequest addTableRequest = request.Params as Models.Messages.AddTableRequest;

                    Boolean valid = true; //!base.Verify(addTableRequest.BitcoinAddress, addTableRequest.ToString(), request.Signature
                    if (!valid)
                    {
                        //throw new Exceptions.SignatureNotValidException();
                        throw new Exception();
                    }
                    else
                    {
                        TableRepo.Add(addTableRequest.Table);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}