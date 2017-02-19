using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public class HandController : BaseController
    {
		public BitPoker.Repository.IHandRepository HandRepo { get; set; }

		[HttpGet]
		public IEnumerable<BitPoker.Models.Hand> Get(string id)
		{
			return HandRepo.All();
		}
    }
}