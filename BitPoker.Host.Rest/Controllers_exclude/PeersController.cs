using System;
using System.Web.Http.Cors;
using System.Configuration;

namespace BitPoker.Host.Rest.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PeersController : Rest.PeersController
    {
        public PeersController()
        {
            //String repo = System.Configuration.ConfigurationManager.AppSettings["PeerRepo"];
            //base.PeerRepo = (Repository.IGenericRepository<Models.Peer>)Activator.CreateInstance(Type.GetType(repo)); // new Repository.Mocks.PeerRepository(); //Repository.LiteDB.PeerRepository("bitpoker.db");

            base.PeerRepo = new Repository.MockPeerRepo(); //Repository.LiteDB.PeerRepository("bitpoker.db");
        }
    }
}