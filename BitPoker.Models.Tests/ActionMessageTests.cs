using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class ActionMessageTests
    {
        [TestMethod]
        public void Should_Get_UnSigned_Action_Message_ToString()
        {
            Models.Messages.ActionMessage sb = new Models.Messages.ActionMessage()
            {
                Action = "SMALL BLIND",
                Amount = 100000,
                Id = new Guid("47b466e4-c852-49f3-9a6d-5e59c62a98b6"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 0,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0)
            };

            String actual = sb.ToString();
            Assert.AreEqual("47b466e4-c852-49f3-9a6d-5e59c62a98b6398b5fe2-da27-4772-81ce-37fa615719b50SMALL BLIND10000020160817000000", actual);
        }

        [TestMethod]
        public void Should_Get_Signed_Action_Message_ToString()
        {
            Models.Messages.ActionMessage sb = new Models.Messages.ActionMessage()
            {
                Action = "SMALL BLIND",
                Amount = 100000,
                Id = new Guid("47b466e4-c852-49f3-9a6d-5e59c62a98b6"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 0,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0),
                Signature = ""
            };

            String actual = sb.ToString();
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Should_Get_Action_Message_As_JSON()
        {
            Models.Messages.ActionMessage sb = new Models.Messages.ActionMessage()
            {
                Action = "SMALL BLIND",
                Amount = 100000,
                Id = new Guid("47b466e4-c852-49f3-9a6d-5e59c62a98b6"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 0,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0)
            };

            String actual = Newtonsoft.Json.JsonConvert.SerializeObject(sb);
            Assert.IsNotNull(actual);

        }
    }
}
