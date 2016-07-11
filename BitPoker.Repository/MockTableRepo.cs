using System;
using System.Collections.Generic;
using BitPoker.Models.Contracts;

namespace BitPoker.Repository
{
    public class MockTableRepo : ITableRepository
    {
        public Table Find(Guid id)
        {
            //Return a fake contract
            //{D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363}
            if (id.ToString() == "D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363")
            {
                return new Table(2, 10) { Id = new Guid("D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363"), BigBlind = 10000, SmallBlind = 5000 };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Table> All()
        {
            List<Table> tables = new List<Table>(1);
            tables.Add(new Table(2, 10)
            {
                Id = new Guid("D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363"),
                BigBlind = 10000,
                SmallBlind = 5000,
                MaxBuyIn = 20000000,
                MinBuyIn = 10000000
            });

            Table mockTable = new Table(2, 2) { Id = new Guid("29A67F70-AC8D-4280-947A-D42E97224BD8"), BigBlind = 10000, SmallBlind = 5000 };
            //mockTable.Deck = new BitPoker.Models.MockShuffledDeck();

            Models.TexasHoldemPlayer alice = new Models.TexasHoldemPlayer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", Stack = 100000 };
            Models.TexasHoldemPlayer bob = new Models.TexasHoldemPlayer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", Stack = 100000 };
            mockTable.Players.Add(alice);
            mockTable.Players.Add(bob);

            tables.Add(mockTable);

            return tables;
        }

        public void Add(Table item)
        {
        }

        public void Dispose()
        {
        }
    }
}
