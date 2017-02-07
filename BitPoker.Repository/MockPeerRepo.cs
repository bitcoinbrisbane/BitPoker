using System;
using System.Collections.Generic;
using BitPoker.Models;
using System.Linq;

namespace BitPoker.Repository
{
    [Obsolete]
    public class MockPeerRepo : IPeerRepository
    {
        private List<Peer> _peers;

        public MockPeerRepo()
        {
            Peer alice = new Peer()
            {
                UserAgent = "Mocks",
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                NetworkAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            Peer bob = new Peer()
            {
                UserAgent = "Mocks",
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                LastSeen = DateTime.UtcNow.AddSeconds(-1),
                NetworkAddress = "https://www.bitpoker.io/api/players/mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            _peers = new List<Peer>(2);

            _peers.Add(alice);
            _peers.Add(bob);
        }

        public MockPeerRepo(String fileName)
        {
            String json = System.IO.File.ReadAllText(fileName);
            _peers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Peer>>(json);
        }

        public void Add(Peer item)
        {
        }

        public IEnumerable<Peer> All()
        {
            return _peers;
        }

        public Peer Find(String id)
        {
            return _peers.FirstOrDefault(p => p.BitcoinAddress == id);
        }

        public void Delete(Peer entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Peer entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}
