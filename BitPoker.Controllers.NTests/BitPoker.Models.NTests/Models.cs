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
		public void Should_Get_Buy_In_Request_To_String()
		{
			BitPoker.Models.Messages.BuyInRequest request = new BitPoker.Models.Messages.BuyInRequest()
			{
				BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ"
			};

			String expected = "BuyIn";
			Assert.AreEqual(expected, request.ToString());
		}
	}
}
