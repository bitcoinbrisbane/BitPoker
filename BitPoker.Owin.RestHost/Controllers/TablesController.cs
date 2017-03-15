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
			String tableRepo = System.Configuration.ConfigurationManager.AppSettings["TableRepo"];

			if (!String.IsNullOrEmpty(tableRepo))
			{
				//base.TableRepo = Activator.CreateInstance(Type.GetType(tableRepo));
			}
			else
			{
				base.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
			}
		}
	}
}