using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BitPoker.Controllers.Tests
{
    [TestClass]
    public class MessageControllerTests
    {
        

        [TestMethod, TestCategory("Small Blind")]
        public void Should_Post_Small_Blind()
        {
            Guid handId = new Guid("c5123f5c-c8d0-4d29-b7bf-111a6330ba62");
            MessageController controller = new MessageController();
            controller.HandRepo = new Repository.MockHandRepo();

            request.Method = "SmallBlind";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                TimeStamp = new DateTime(2016, 12, 12)
            };

            var response = controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Error);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());
        }

        [TestMethod, Ignore, TestCategory("Small Blind")]
        public void Should_Not_Be_Able_To_Post_Small_Blind()
        {

        }

        [TestMethod, Ignore, TestCategory("Small Blind")]
        public void Should_Not_Post_Small_Blind_With_Invalid_Signature()
        {

        }

        [TestMethod, Ignore, TestCategory("Big Blind")]
        public void Should_Post_Big_Blind()
        {

        }

        [TestMethod, TestCategory("Shuffle")]
        public void Should_Shuffle_Deck()
        {
            //Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            Guid handId = new Guid("91dacf01-4c4b-4656-912b-2c3a11f6e516");
            _controller = new MessageController();
            _controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "Shuffle";
            request.Params = new Models.Messages.ShuffleRequest()
            {
                HandId = handId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());

            Assert.IsNotNull(response.Result);
            Models.Messages.ShuffleResponse shuffleResponse = response.Result as Models.Messages.ShuffleResponse;

            Assert.AreEqual(52, shuffleResponse.Deck.Cards.Count);
        }

        [TestMethod, TestCategory("Shuffle")]
        public void Should_Shuffle_Deck_With_Invalid_HandId()
        {
            Guid handId = new Guid("b1fbb2b7-a4a6-4831-a277-0fb6c2becb02");
            _controller = new MessageController();
            _controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "Shuffle";
            request.Params = new Models.Messages.ShuffleRequest()
            {
                HandId = handId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Error);
        }

        [TestMethod, TestCategory("Deal")]
        public void Should_Deal_Cards_Heads_Up()
        {
            Guid tableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16");
            _controller = new MessageController();
            _controller.HandRepo = new Repository.MockHandRepo();

            request.Method = "Deal";
            request.Params = new Models.Messages.DealRequest()
            {
                TableId = tableId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());

            Assert.IsNotNull(response.Result);
            //Assert.IsNotNull(response.Result);

            Models.Messages.DealResponse dealResponse = response.Result as Models.Messages.DealResponse;
            Assert.AreEqual(52, dealResponse.Deck.Cards.Count);
        }

        [TestCleanup]
        public void TearDown()
        {
            _controller = null;
        }
    }
}
