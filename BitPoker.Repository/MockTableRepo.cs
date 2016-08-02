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
            if (id.ToString().ToLower() == "d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363")
            {
                Models.TexasHoldemPlayer alice = new Models.TexasHoldemPlayer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", IsBigBlind = false, IsDealer = false, IsSmallBlind = true, Position = 1, IsTurnToAct = false, Stack = 1000000 };
                Models.TexasHoldemPlayer bob = new Models.TexasHoldemPlayer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", IsBigBlind = true, IsDealer = false, IsSmallBlind = false, Position = 2, IsTurnToAct = false, Stack = 1000000 };

                //4bc7f305-aa16-450a-a3be-aad8fba7f425
                Table table = new Table(2, 10) { Id = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363"), BigBlind = 10000, SmallBlind = 5000 };
                table.Players.Add(alice);
                table.Players.Add(bob);

                return table;
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
                Id = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363"),
                BigBlind = 10000,
                SmallBlind = 5000,
                MaxBuyIn = 20000000,
                MinBuyIn = 10000000
            });

            Table mockTable = new Table(2, 2) { Id = new Guid("29a67f70-ac8d-4280-947a-d42e97224bd8"), BigBlind = 10000, SmallBlind = 5000 };
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

        public void Update(Table entity)
        {
        }
    }
}
