using System;
using System.Collections.Generic;
using BitPoker.Models;
using System.Linq;

namespace BitPoker.Repository
{
    public class MockPlayerRepo : IPlayerRepository
    {
        List<PlayerInfo> _mockPlayers = new List<PlayerInfo>();

        public MockPlayerRepo()
        {
            PlayerInfo alice = new PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "http://www.bitpoker.io/api",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            PlayerInfo bob = new PlayerInfo()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                LastSeen = DateTime.UtcNow.AddSeconds(-1),
                IPAddress = "http://www.bitpoker.io/api",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            _mockPlayers.Add(alice);
            _mockPlayers.Add(bob);

            return _mockPlayers;
        }

        public void Add(PlayerInfo item)
        {
        }

        public IEnumerable<PlayerInfo> All()
        {
            return _mockPlayers;
        }

        public PlayerInfo Find(String address)
        {
            return _mockPlayers.FirstOrDefault(p => p.BitcoinAddress == address);
        }
    }
}
