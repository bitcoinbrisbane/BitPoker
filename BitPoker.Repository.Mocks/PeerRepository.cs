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
        private List<Models.Peer> _peers = new List<Peer>(2);

        public void Add(Peer entity)
        {
            _peers.Add(entity);
        }

        public IEnumerable<Peer> All()
        {
            
            //91e41Z5ghYVak4ssoMfsB8NswBdYoGBfcuCqUm9qaL56EybCyAN
            _peers.Add(new Peer() { UserAgent = "Mock", NetworkAddress = "http://localhost:8081", BitcoinAddress = "myEANpEi4b3oZn8Cjh1uJYJDRMzkap9Rhm" });

            //91jJisRLUdDom6DWVyLkLaMVPVvrpkEidSAqMEntzk8HvRjSGoh
            _peers.Add(new Peer() { UserAgent = "Mock", NetworkAddress = "http://localhost:8082", BitcoinAddress = "msmzhcPcdo1VZHBqCXcgzArAEDWEBicpc1" });

            return _peers;
        }

        public void Delete(Peer entity)
        {
        }

        public Peer Find(string id)
        {
            return _peers.SingleOrDefault(p => p.BitcoinAddress == id);
        }

        public void Update(Peer entity)
        {
        }
    }
}
