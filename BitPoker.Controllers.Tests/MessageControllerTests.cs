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

        [TestMethod, TestCategory("Controllers")]
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

        [TestMethod, Ignore, TestCategory("Controllers")]
        public void Should_Not_Join_Full_Table()
        {

        }
    }
}
