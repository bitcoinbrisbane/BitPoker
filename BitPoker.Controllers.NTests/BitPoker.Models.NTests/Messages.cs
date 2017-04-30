using NUnit.Framework;
using System;
namespace BitPoker.Models.NTests
{
	[TestFixture()]
	public class Messages
	{
		private const String REQUEST_ID = "a66a8eb4-ea1f-42bb-b5f2-03456094b1f6";
		private const String TABLE_ID = "d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363";

		//wif key 93GVpcVMNpNkFPqLVhWqJRbdn89bX77pUqxcGBiqW8yRwjfkoCr

		[Test()]
		public void Should_Get_Join_Table_Request_ToString()
		{
			BitPoker.Models.Messages.JoinTableRequest request = new BitPoker.Models.Messages.JoinTableRequest()
			{
				Id = new Guid(REQUEST_ID),
				TableId = new Guid(TABLE_ID),
				PublicKey = "026F36F4380413165050FE415B6826DF6485753677378D2AA30034A91DA35A2E6D",
				BitcoinAddress = "mwKNGSDZmGdhJGybLADxVVHrPa3GRmeDjk"

			};

			Assert.AreEqual("", request.ToString());

		}

		[Test()]
		public void Should_Hash_Join_Table_Request()
		{
		}

		[Test()]
		public void Should_Sign_Join_Table_Request()
		{
		}
	}
}