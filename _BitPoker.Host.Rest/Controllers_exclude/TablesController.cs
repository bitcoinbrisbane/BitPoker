using System;
using System.Web.Http.Cors;

namespace BitPoker.Host.Rest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TablesController : Rest.TablesController
    {
        public TablesController()
        {
            base.TableRepo = new BitPoker.Repository.MockTableRepo(); //new Repository.LiteDB.TableRepository("bitpoker.db");
        }
    }
}