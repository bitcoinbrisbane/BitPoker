using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public class TablesController : BaseController, ITablesController
    {
        public BitPoker.Repository.ITableRepository TableRepo { get; set; }

        public TablesController()
        {
        }

        [HttpGet]
        public IEnumerable<Models.Contracts.Table> Get()
        {
            AddLog("Get tables");
            return TableRepo.All();
        }

        [HttpGet]
        public Models.Contracts.Table Get(Guid id)
        {
            AddLog("Get table");
            return TableRepo.Find(id);
        }

		//[Authorize]
		[HttpPost]
		public void Post(Models.Messages.AddTableRequest request)
		{
			TableRepo.Add(request.Table);
		}

		//[HttpPost, Route("deal")]
		//public Models.Messages.DealResponse Deal(Models.Messages.DealRequest request)
		//{
		//	return new Models.Messages.DealResponse() { Id = request.Id, TimeStamp = DateTime.UtcNow };
		//}
    }
}