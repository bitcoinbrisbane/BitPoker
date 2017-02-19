using System;
using NUnit.Framework;

namespace BitPoker.Models.NTests
{
	[TestFixture()]
	public class Models
	{
		private const String REQUEST_ID = "a66a8eb4-ea1f-42bb-b5f2-03456094b1f6";
		private const String TABLE_ID = "d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363";

		[Test()]
		public void Should_Get_Table_Bitcoin_Address()
		{
			BitPoker.Models.Contracts.Table table = new Contracts.Table(2,2);

			table.Peers[0] = new Peer()
			{
				BitcoinAddress = "mwKNGSDZmGdhJGybLADxVVHrPa3GRmeDjk",
				PublicKey = "026F36F4380413165050FE415B6826DF6485753677378D2AA30034A91DA35A2E6D"
			};

			Assert.AreEqual("mwKNGSDZmGdhJGybLADxVVHrPa3GRmeDjk", table.BitcoinAddress);
		}

		[Test()]
		public void Should_Get_Table_Pulic_Key()
		{
			BitPoker.Models.Contracts.Table table = new Contracts.Table(2, 2);

			table.Peers[0] = new Peer()
			{
				BitcoinAddress = "mwKNGSDZmGdhJGybLADxVVHrPa3GRmeDjk",
				PublicKey = "026F36F4380413165050FE415B6826DF6485753677378D2AA30034A91DA35A2E6D"
			};

			Assert.AreEqual("mwKNGSDZmGdhJGybLADxVVHrPa3GRmeDjk", table.BitcoinAddress);
		}
	}
}
