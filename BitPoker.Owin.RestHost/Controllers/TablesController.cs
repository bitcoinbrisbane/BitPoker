using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[Route("api/[controller]")]
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class TablesController : BitPoker.Controllers.Rest.TablesController
	{
		public TablesController()
		{
			//base.TableRepo = new BitPoker.Repository.LiteDB.TableRepository("bitpoker3.db");
			base.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
		}
	}
}