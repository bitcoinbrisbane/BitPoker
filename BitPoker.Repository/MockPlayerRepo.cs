using System;
using System.Collections.Generic;
using BitPoker.Models;
using System.Linq;

namespace BitPoker.Repository
{
    public class MockPlayerRepo : IPlayerRepository
    {
        List<Peer> _players = new List<Peer>();

        public MockPlayerRepo()
        {
            Peer alice = new Peer()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            Peer bob = new Peer()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                LastSeen = DateTime.UtcNow.AddSeconds(-1),
                IPAddress = "https://www.bitpoker.io/api/players/mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            _players.Add(alice);
            _players.Add(bob);
        }

        public MockPlayerRepo(String fileName)
        {
            String json = System.IO.File.ReadAllText(fileName);
            _players = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Peer>>(json);
        }

        public void Add(Peer item)
        {
        }

        public IEnumerable<Peer> All()
        {
            return _players;
        }

        public Peer Find(String address)
        {
            return _players.FirstOrDefault(p => p.BitcoinAddress == address);
        }

        public Int32 Save()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
