using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Repository.Mocks
{
    public class PeerRepository : IGenericRepository<Models.Peer>
    {
        public void Add(Peer entity)
        {
        }

        public IEnumerable<Peer> All()
        {
            List<Models.Peer> peers = new List<Peer>(2);
            //91e41Z5ghYVak4ssoMfsB8NswBdYoGBfcuCqUm9qaL56EybCyAN
            peers.Add(new Peer() { UserAgent = "Mock", IPAddress = "http://localhost:8081", BitcoinAddress = "myEANpEi4b3oZn8Cjh1uJYJDRMzkap9Rhm" });

            //91jJisRLUdDom6DWVyLkLaMVPVvrpkEidSAqMEntzk8HvRjSGoh
            peers.Add(new Peer() { UserAgent = "Mock", IPAddress = "http://localhost:8082", BitcoinAddress = "msmzhcPcdo1VZHBqCXcgzArAEDWEBicpc1" });

            return peers;
        }

        public void Delete(Peer entity)
        {
        }

        public Peer Find(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Peer entity)
        {
        }
    }
}
