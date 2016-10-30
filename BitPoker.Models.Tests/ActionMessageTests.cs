using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class ActionMessageTests
    {
        [TestMethod, TestCategory("Models")]
        public void Should_Get_Small_Blind_Action_Message_As_Hash()
        {
            Messages.ActionMessage message = new Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = 5000000,
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 2,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0),
                PreviousHash = "8ab9f91c002d8ccdbd8a49f7e028d27ca6ef01cf1fdaa4eca637868d8e4adf31"
            };

            String json = JsonConvert.SerializeObject(message);
            Byte[] data = NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(json);
            var actual = NBitcoin.Crypto.Hashes.SHA256(data);

            Assert.AreEqual("cb19bc14bca61bee174e5d6591530ad72b3ab58e0c5a904baec5b5de85c65e88", NBitcoin.DataEncoders.Encoders.Hex.EncodeData(actual));
        }

        [TestMethod, TestCategory("RPC")]
        public void Should_Get_Call_Action_Message_ToString()
        {
            Messages.ActionMessage message = new Messages.ActionMessage()
            {
                Action = "CALL",
                Amount = 5000000,
                HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"),
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Index = 2,
                TimeStamp = new DateTime(2016, 08, 17, 0, 0, 0),
                PreviousHash = "cb19bc14bca61bee174e5d6591530ad72b3ab58e0c5a904baec5b5de85c65e88"
            };

            String actual = JsonConvert.SerializeObject(message);
            Assert.AreEqual("{\"TableId\":\"bf368921-346a-42d8-9cb8-621f9cad5e16\",\"HandId\":\"398b5fe2-da27-4772-81ce-37fa615719b5\",\"Index\":2,\"Action\":\"CALL\",\"Amount\":5000000,\"Tx\":null,\"PreviousHash\":\"cb19bc14bca61bee174e5d6591530ad72b3ab58e0c5a904baec5b5de85c65e88\",\"HashAlgorithm\":\"SHA256\",\"Version\":1.0,\"BitcoinAddress\":\"msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv\",\"TimeStamp\":\"2016-08-17T00:00:00\"}", actual);
        }

        [TestMethod]
        public void Should_Sign_Action_Message()
        {
            String messageToSign = "{\"TableId\":\"bf368921-346a-42d8-9cb8-621f9cad5e16\",\"HandId\":\"398b5fe2-da27-4772-81ce-37fa615719b5\",\"Index\":2,\"Action\":\"CALL\",\"Amount\":5000000,\"Tx\":null,\"PreviousHash\":\"8ab9f91c002d8ccdbd8a49f7e028d27ca6ef01cf1fdaa4eca637868d8e4adf31\",\"HashAlgorithm\":\"SHA256\",\"Version\":1.0,\"BitcoinAddress\":\"msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv\",\"TimeStamp\":\"2016-08-17T00:00:00\"}";
            String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";

            NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            String actual = secret.PrivateKey.SignMessage(messageToSign);

            Assert.IsNotNull(actual);
            Assert.AreEqual("HGlWJ1/MDp9JWiJWtMo5z8Se6JJtyuNZLhK6c3dJEYbRPrZrgPEr9j51R7Jsqu5HlkzOb073oRu+mT+/hBuyTP0=", actual);
        }
    }
}
