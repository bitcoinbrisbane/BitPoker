using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public class MockRepo : IPlayerRepository, ITableRepository
    {
        public IEnumerable<BitPoker.Models.PlayerInfo> All()
        {
            BitPoker.Models.PlayerInfo alice = new BitPoker.Models.PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
            };

            BitPoker.Models.PlayerInfo bob = new BitPoker.Models.PlayerInfo()
            {
                BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo"
            };

            List<BitPoker.Models.PlayerInfo> mockPlayers = new List<BitPoker.Models.PlayerInfo>();
            mockPlayers.Add(alice);
            mockPlayers.Add(bob);

            return mockPlayers;
        }

        public Models.Contracts.Table Find(Guid id)
        {
            //Return a fake contract
            //{D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363}
            return new Models.Contracts.Table(2, 10) { Id = new Guid("D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363") };
        }

        public BitPoker.Models.PlayerInfo Find(String address)
        {
            if (address == "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv")
            {
                BitPoker.Models.PlayerInfo alice = new BitPoker.Models.PlayerInfo()
                {
                    BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
                };

                return alice;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public IEnumerable<Models.Contracts.Table> All()
        {
            throw new NotImplementedException();
        }
    }
}
