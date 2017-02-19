using System;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class TablesController : BitPoker.Controllers.Rest.TablesController
	{
		public TablesController()
		{
			//Inject repo
		}
	}
}