using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Controllers.Tests
{
    [TestClass]
    public class MessageControllerTests
    {
        [TestMethod, TestCategory("Controllers")]
        public void Should_Join_Table_In_Seat_3()
        {
            //private key 93GnRYsUXD4FPCiV46n8vqKvwHSZQgjnyuBvhNtqRvq3Ac26kVc

            Guid tableId = new Guid("35bc5692-6781-4a79-a5d2-89752edd882e");
            MessageController controller = new MessageController();
            controller.TableRepo = new BitPoker.Repository.MockTableRepo();

            Models.Messages.JoinTableRequest request = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12)
            };

            var result = controller.JoinTable(request);

            Assert.IsNotNull(result, "Object is null");
            Assert.AreEqual(result.Seat, 2);
        }

        [TestMethod, Ignore, TestCategory("Controllers")]
        public void Should_Not_Join_Full_Table()
        {

        }
    }
}
