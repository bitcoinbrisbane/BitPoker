using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BitPoker.Net.RestHost.Controllers
{
    [EnableCors("AllowSpecificOrigin"), Route("api/[controller]")]
    public class TablesController : BaseController, ITablesController
    {
        public BitPoker.Repository.ITableRepository TableRepo { get; set; }

        public TablesController()
        {
            this.TableRepo = new BitPoker.Repository.MockTableRepo();
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

        [HttpPost, Route("join")]
        public Models.Messages.JoinTableResponse JoinTable(Models.Messages.JoinTableRequest request)
        {
            Models.Messages.JoinTableResponse response = new Models.Messages.JoinTableResponse();
            var table = this.TableRepo.Find(request.TableId);

            if (table != null && table.Peers[request.Seat] == null)
            {
                //for (Int32 i = 0; i < table.MaxPlayers; i++)
                //{
                //    if (table.Peers[i] == null)
                //    {
                //        response.Seat = i;
                //        break;
                //    }
                //}

                response.Seat = request.Seat;

                return response;
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }
        }

        [HttpPost, Route("buyin")]
        public Models.Messages.BuyInResponse BuyIn(Models.Messages.BuyInRequest request)
        {
            var table = this.TableRepo.Find(request.TableId);

            if (table != null)
            {
                NBitcoin.Transaction tx = new NBitcoin.Transaction();
                NBitcoin.TransactionBuilder builder = new NBitcoin.TransactionBuilder();
                Boolean isValid = true; // builder.Verify(tx);

                if (isValid)
                {
                    var utxos = tx.Outputs;
                    var sum = tx.Outputs.Sum(u => u.Value);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }

            return new Models.Messages.BuyInResponse();
        }
    }
}