using System;
using System.Collections.Generic;
using BitPoker.Models.Contracts;
using System.Linq;

namespace BitPoker.Repository.Mocks
{
	public class TableRepository : ITableRepository
	{
		private List<Table> _tables = new List<Table>();

		public TableRepository(Int32 n = 3)
		{
			_tables = new List<Table>(n);

			//Table as per the readme
			Table mockHeadsUpTable = new Table(2, 2) { Id = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"), BigBlind = 10000, SmallBlind = 5000, MinBuyIn = 100000, MaxBuyIn = 200000, MinPlayers = 2, MaxPlayers = 2, HashAlgorithm = "SHA256" };

			Models.Peer alice = new Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", NetworkAddress = "https://www.bitpoker.io/api/alice", UserAgent = "Mock", PublicKey = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1" };
			Models.Peer bob = new Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", NetworkAddress = "http://localhost:5000", UserAgent = "Mock", PublicKey = "04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D" };

			mockHeadsUpTable.Peers[0] = alice;
			mockHeadsUpTable.Peers[1] = bob;

			_tables.Add(mockHeadsUpTable);

			//Empty table
			Table mockEmptyTable = new Table(2, 10) { Id = new Guid("35bc5692-6781-4a79-a5d2-89752edd882e"), BigBlind = 10000, SmallBlind = 5000 };
			mockHeadsUpTable.Peers[0] = alice;

			Table mockTableWithEmptySeat = new Table(2, 10) { Id = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c"), BigBlind = 10000, SmallBlind = 5000, MinBuyIn = 100000, MaxBuyIn = 200000 };
			mockTableWithEmptySeat.Peers[0] = alice;
			mockTableWithEmptySeat.Peers[2] = bob;

			_tables.Add(mockEmptyTable);
			_tables.Add(mockTableWithEmptySeat);
		}

		/// <summary>
		/// Inject JSON
		/// </summary>
		/// <param name="fileName"></param>
		public TableRepository(String fileName)
		{
			throw new NotImplementedException();
		}

		public Table Find(Guid id)
		{
			//Return a fake contract
			//{D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363}
			if (id.ToString().ToLower() == "d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363")
			{
				//Models.Peer alice = new Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", IsBigBlind = false, IsDealer = false, IsSmallBlind = true, Position = 1, IsTurnToAct = false, Stack = 1000000 };
				//Models.Peer bob = new Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", IsBigBlind = true, IsDealer = false, IsSmallBlind = false, Position = 2, IsTurnToAct = false, Stack = 1000000 };

				Models.Peer alice = new Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", NetworkAddress = "https://www.bitpoker.io/api/alice" };
				Models.Peer bob = new Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", NetworkAddress = "http://localhost:8080" };

				//4bc7f305-aa16-450a-a3be-aad8fba7f425
				Table table = new Table(2, 10) { Id = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363"), BigBlind = 10000, SmallBlind = 5000 };
				table.Peers[0] = alice;
				table.Peers[1] = bob;

				return table;
			}
			else
			{
				return _tables.Single(t => t.Id.ToString() == id.ToString());
			}
		}

		public IEnumerable<Table> All()
		{
			//List<Table> tables = new List<Table>(2);
			//tables.Add(new Table(2, 10)
			//{
			//    Id = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363"),
			//    BigBlind = 10000,
			//    SmallBlind = 5000,
			//    MaxBuyIn = 20000000,
			//    MinBuyIn = 10000000
			//});

			//Table mockTable = new Table(2, 2) { Id = new Guid("29a67f70-ac8d-4280-947a-d42e97224bd8"), BigBlind = 10000, SmallBlind = 5000 };

			////Models.Peer alice = new Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", Stack = 100000 };
			////Models.Peer bob = new Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", Stack = 100000 };

			//Models.Peer alice = new Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", IPAddress = "https://www.bitpoker.io/api/alice" };
			//Models.Peer bob = new Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", IPAddress = "http://localhost:8080" };
			//mockTable.Peers.Add(alice);
			//mockTable.Peers.Add(bob);

			//tables.Add(mockTable);

			//Table lonelyTable = new Table(2, 10) { Id = new Guid("91dacf01-4c4b-4656-912b-2c3a11f6e516"), BigBlind = 10000, SmallBlind = 5000 };
			//lonelyTable.Peers.Add(alice);

			//return tables;
			return _tables;
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

		public void Save()
		{
		}
	}
}
