using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public class HandController : BaseController
    {
		public BitPoker.Repository.IHandRepository HandRepo { get; set; }

		public BitPoker.Repository.IMessagesRepository MessageRepo { get; set; }

		public HandController()
		{
		}

		[HttpGet]
		public IEnumerable<BitPoker.Models.Hand> Get(string id)
		{
			return HandRepo.All();
		}

		[HttpPost]
		public void Post(BitPoker.Models.Messages.ActionMessage message)
		{
			//message.HandId;
		}

		[HttpPost, Route("call")]
		public void Call()
		{
		}

		[HttpPost, Route("bet")]
		public void Bet()
		{
		}

		[HttpPost, Route("raise")]
		public void Raise()
		{
		}

		[HttpPost, Route("fold")]
		public void Fold()
		{
		}

		[HttpPost, Route("check")]
		public void Check()
		{
		}
    }
}