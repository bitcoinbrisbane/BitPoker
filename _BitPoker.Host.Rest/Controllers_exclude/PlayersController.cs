using System.Web.Http.Cors;

namespace BitPoker.Host.Rest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayersController : BitPoker.Controllers.Rest.PlayersController
    {
        public PlayersController()
        {
            base.PlayerRepo = new BitPoker.Repository.LiteDB.PlayerRepository<Models.TexasHoldemPlayer>("bitpoker.db");
        }
    }
}