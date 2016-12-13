using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Controllers.Tests
{
    [TestClass]
    public class MessageControllerTests
    {
        private BitPoker.Models.IRequest request;
        private const String REQUEST_ID = "a66a8eb4-ea1f-42bb-b5f2-03456094b1f6";

        [TestInitialize]
        public void Setup()
        {
            request = new Models.Messages.RPCRequest()
            {
                Id = new Guid(REQUEST_ID)
            };
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_Seat_2()
        {
            //private key 93GnRYsUXD4FPCiV46n8vqKvwHSZQgjnyuBvhNtqRvq3Ac26kVc

            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "JoinTable";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12)
            };

            var result = controller.Post(request);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.AreEqual(result.Id.ToString(), REQUEST_ID);

            Models.Messages.JoinTableResponse response = result.Result as Models.Messages.JoinTableResponse;
            Assert.AreEqual(2, response.Seat);
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_First_Empty_Seat()
        {
            //private key 91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf

            Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "JoinTable";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12)
            };

            var result = controller.Post(request);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.AreEqual(result.Id.ToString(), REQUEST_ID);

            Models.Messages.JoinTableResponse response = result.Result as Models.Messages.JoinTableResponse;
            Assert.AreEqual(1, response.Seat);
        }

        [TestMethod, Ignore, TestCategory("Join Table")]
        public void Should_Not_Join_Full_Table()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Buy_In_To_Joined_Table()
        {

        }

        [TestMethod, TestCategory("Buy In")]
        public void Should_Not_Buy_In_Under_The_Min()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Buy_In_Over_The_Max()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Buy_In_With_Unconfirmed_UTXo()
        {

        }

        [TestMethod, TestCategory("Buy In")]
        public void Should_Post_Small_Blind()
        {
            Guid handId = new Guid("c5123f5c-c8d0-4d29-b7bf-111a6330ba62");
            MessageController controller = new MessageController();
            controller.HandRepo = new Repository.MockHandRepo();


        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Be_Able_To_Post_Small_Blind()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Post_Big_Blind()
        {

        }

        [TestMethod, TestCategory("Shuffle")]
        public void Should_Shuffle_Deck()
        {
            Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "Shuffle";
            request.Params = new Models.Messages.ShuffleRequest();

            var response = controller.Post(request);

            Assert.IsNotNull(response);
        }

        [TestMethod, Ignore, TestCategory("")]
        public void Should_Deal_Cards_Heads_Up()
        {

        }
    }
}
