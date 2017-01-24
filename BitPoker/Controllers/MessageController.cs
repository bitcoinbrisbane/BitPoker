using System.Web.Http.Cors;

namespace BitPoker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MessageController : v1.MessageController
    {
        public MessageController()
        {
            base.PlayerRepo = new BitPoker.Repository.LiteDB.PlayerRepository<Models.TexasHoldemPlayer>("bitpoker.db");
            base.HandRepo = new BitPoker.Repository.LiteDB.HandRepository("bitpoker.db");
            base.TableRepo = new BitPoker.Repository.LiteDB.TableRepository("bitpoker.db");
        }
    }
}
