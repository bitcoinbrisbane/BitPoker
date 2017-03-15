using NUnit.Framework;
using System;
namespace BitPoker.Controllers.NTests
{
	[TestFixture()]
	public class BuyInControllerTests
	{
		[Test()]
		public void Should_Get_Buy_In_Address()
		{
			BitPoker.Controllers.Rest.BuyInController controller = new Rest.BuyInController();
			controller.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
			controller.Network = NBitcoin.Network.TestNet;

			Guid Id = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16");
			
			String actual = controller.Get(Id);

			//Verified off coinb.in using uncompresssed
			//https://coinb.in/?verify=5241041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd14104f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d52ae#verify

			const String expected = "2MtFGSjUn1FLhwgyf7gAaX3n1wCg29B4wvh";

			Assert.AreEqual(expected, actual);
		}

		[Test()]
		public void Should_Buy_In_For_Minimum()
		{
			BitPoker.Controllers.Rest.BuyInController controller = new Rest.BuyInController();
			controller.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
			controller.Network = NBitcoin.Network.TestNet;

			BitPoker.Models.Messages.BuyInRequest request = new Models.Messages.BuyInRequest()
			{
				BitcoinAddress = "2MtFGSjUn1FLhwgyf7gAaX3n1wCg29B4wvh",
				TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16")
			};

			var actual = controller.Post(request);

			BitPoker.Models.Messages.BuyInResponse expected = new Models.Messages.BuyInResponse();

			Assert.AreEqual(expected, actual);
		}
	}
}