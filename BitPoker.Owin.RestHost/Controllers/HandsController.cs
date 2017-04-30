using System;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class HandsController : BitPoker.Controllers.Rest.HandsController
	{
		public HandsController()
		{
			base.HandRepo = new BitPoker.Repository.Mocks.HandRepository(); //BitPoker.Repository.LiteDB.HandRepository("bitpoker3.db");
			//base.MessageRepo = new BitPoker.Repository.LiteDB.ActionMessgeRepository("bitpoker3.db");
		}
	}
}