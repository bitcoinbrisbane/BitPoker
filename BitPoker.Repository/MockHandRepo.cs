using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Repository
{
    public class MockHandRepo : IHandRepository
    {
        private PlayerInfo alice;
        private PlayerInfo bob;

        public MockHandRepo()
        {
            alice = new PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            bob = new PlayerInfo()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                LastSeen = DateTime.UtcNow.AddSeconds(-1),
                IPAddress = "https://www.bitpoker.io/api/players/mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };
        }

        public MockHandRepo(IPlayerRepository playerRepo)
        {
            alice = playerRepo.Find("msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv");
            alice = playerRepo.Find("mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo");
        }

        public void Add(Hand entity)
        {
        }

        public IEnumerable<Hand> All()
        {
            throw new NotImplementedException();
        }

        public Hand Find(Guid id)
        {
            switch (id.ToString())
            {
                case "398b5fe2-da27-4772-81ce-37fa615719b5":



                    PlayerInfo[] players = new PlayerInfo[2];
                    players[0] = alice;
                    players[1] = bob;

                    Hand hand = new Hand(players)
                    {
                        Id = id
                    };

                    break;
                case "91dacf01-4c4b-4656-912b-2c3a11f6e516":
                    //heads up?
                    break;

            }

            throw new NotImplementedException();
        }

        public void Update(Hand entity)
        {
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
