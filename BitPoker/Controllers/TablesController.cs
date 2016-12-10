using System;
using System.Web.Http.Cors;

namespace BitPoker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TablesController : Rest.TablesController
    {
        public TablesController()
        {
            base.TableRepo = new Repository.LiteDB.TableRepository("bitpoker.db");
        }
    }
}