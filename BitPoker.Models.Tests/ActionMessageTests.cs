using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class ActionMessageTests
    {
        [TestMethod, TestCategory("Models")]
        public void Should_Get_Small_Blind_Action_Message_ToString()
        {
            Messages.ActionMessage sb = new Models.Messages.ActionMessage()
            {
                Action = "SMALL BLIND",
                Amount = 100000,
                Id = new Guid("5d9c465d-9420-4df7-a7cb-ae97c1e1f89d"),
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 0,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0)
            };

            String actual = sb.ToString();
            Assert.AreEqual("5d9c465d-9420-4df7-a7cb-ae97c1e1f89dmsPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv398b5fe2-da27-4772-81ce-37fa615719b50SMALL BLIND10000020160817000000", actual);
        }

        [TestMethod]
        public void Should_Get_Call_Action_Message_ToString()
        {
            Messages.ActionMessage sb = new Models.Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = 5000000,
                Id = new Guid("47b466e4-c852-49f3-9a6d-5e59c62a98b6"),
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 2,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0)
            };

            String actual = sb.ToString();
            Assert.AreEqual("47b466e4-c852-49f3-9a6d-5e59c62a98b6msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv398b5fe2-da27-4772-81ce-37fa615719b52CALL500000020160817000000", actual);
        }

        [TestMethod]
        public void Should_Sign_Action_Message()
        {
            String messageToSign = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv398b5fe2-da27-4772-81ce-37fa615719b50SMALL BLIND10000020160817000000";
            String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";

            NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            String actual = secret.PrivateKey.SignMessage(messageToSign);

            Assert.IsNotNull(actual);
            Assert.AreEqual("HEMKjFpcBGnfXEXGHA1s9XqVTL1qLfwToMIFNmT6tAZwOyNS1SyAPrjXmJasGQCD3+O0Dp9PalyTdtA847gQCys=", actual);
        }

        [TestMethod]
        public void Should_Get_Action_Message_As_JSON()
        {
            Messages.ActionMessage sb = new Messages.ActionMessage()
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

        [TestMethod]
        public void Should_Serialize_Action_As_XML()
        {
            Messages.ActionMessage sb = new Messages.ActionMessage()
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

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Messages.ActionMessage));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, sb);
                var xml = sww.ToString(); // Your XML

                Assert.IsNotNull(xml);
            }
        }
    }
}
