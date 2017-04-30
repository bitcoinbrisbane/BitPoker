using System;
using System.Collections.Generic;
using BitPoker.Models;
using System.Linq;

namespace BitPoker.Repository.Mocks
{
    public class PlayerRepository : IPlayerRepository
    {
        private List<IPlayer> _mocks = new List<IPlayer>();

        public PlayerRepository()
        {
            TexasHoldemPlayer alice = new BitPoker.Models.TexasHoldemPlayer()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                //LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                //Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            TexasHoldemPlayer bob = new TexasHoldemPlayer()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                //LastSeen = DateTime.UtcNow.AddSeconds(-1),
                IPAddress = "https://www.bitpoker.io/api/players/mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                //Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            _mocks.Add(alice);
            _mocks.Add(bob);
        }

        public PlayerRepository(String fileName)
        {
            String json = System.IO.File.ReadAllText(fileName);
            _mocks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IPlayer>>(json);
        }

        public void Add(IPlayer item)
        {
        }

        public IEnumerable<IPlayer> All()
        {
            return _mocks;
        }

        public IPlayer Find(String address)
        {
            return _mocks.FirstOrDefault(p => p.BitcoinAddress == address);
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
