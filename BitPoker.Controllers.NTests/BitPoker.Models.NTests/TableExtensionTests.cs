using NUnit.Framework;
using System;
using BitPoker.Models.ExtensionMethods;

namespace BitPoker.Models.NTests
{
	[TestFixture()]
	public class TableExtensionTests
	{
		[Test()]
		public void Should_Get_Table_Address()
		{
			//Arrange
			//Table as per the readme
			BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table(2, 2) { Id = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"), BigBlind = 10000, SmallBlind = 5000, MinBuyIn = 100000, MaxBuyIn = 200000, MinPlayers = 2, MaxPlayers = 2, HashAlgorithm = "SHA256" };

			BitPoker.Models.Peer alice = new BitPoker.Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", NetworkAddress = "https://www.bitpoker.io/api/alice", UserAgent = "Mock", PublicKey = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1" };
			BitPoker.Models.Peer bob = new BitPoker.Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", NetworkAddress = "http://localhost:5000", UserAgent = "Mock", PublicKey = "04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D" };

			table.Peers[0] = alice;
			table.Peers[1] = bob;

			//Act
			String actual = table.GetScriptAddress();

			//Assert
			const String expected = "2NCrqBsyEk3zXaGLaZ6cG1r6JqX2yjqNnvi";
			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void Should_Get_Table_Redeem_Script()
		{
			//Arrange
			//Table as per the readme
			BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table(2, 2) { Id = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"), BigBlind = 10000, SmallBlind = 5000, MinBuyIn = 100000, MaxBuyIn = 200000, MinPlayers = 2, MaxPlayers = 2, HashAlgorithm = "SHA256" };

			BitPoker.Models.Peer alice = new BitPoker.Models.Peer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv", NetworkAddress = "https://www.bitpoker.io/api/alice", UserAgent = "Mock", PublicKey = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1" };
			BitPoker.Models.Peer bob = new BitPoker.Models.Peer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo", NetworkAddress = "http://localhost:5000", UserAgent = "Mock", PublicKey = "04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D" };

			table.Peers[0] = alice;
			table.Peers[1] = bob;

			//Act
			String actual = table.GetScriptScript();

			//Assert
			const String expected = "2NCrqBsyEk3zXaGLaZ6cG1r6JqX2yjqNnvi";
			Assert.AreEqual(expected, actual);
		}
	}
}
