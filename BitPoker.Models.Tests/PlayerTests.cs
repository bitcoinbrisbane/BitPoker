using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod, TestCategory("RPC")]
        public void Should_Get_Player_As_ToString()
        {
            Peer alice = new Peer()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                NetworkAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            String actual = JsonConvert.SerializeObject(alice);
            Assert.IsNotNull(actual);
            //Assert.AreEqual("{\"UserAgent\":null,\"BitcoinAddress\":\"msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv\",\"PublicKey\":null,\"IPAddress\":\"https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv\",\"Latency\":\"00:00:00.2000000\",\"LastSeen\":\"2016-11-08T10:44:20.9578656Z\"}", actual);
        }

        [TestMethod, TestCategory("RPC")]
        public void Should_Get_Player_As_Json()
        {
            Peer alice = new Peer()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                NetworkAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            String actual = JsonConvert.SerializeObject(alice);
            Assert.IsNotNull(actual);
            //Assert.AreEqual("{\"TableId\":\"bf368921-346a-42d8-9cb8-621f9cad5e16\",\"HandId\":\"398b5fe2-da27-4772-81ce-37fa615719b5\",\"Index\":2,\"Action\":\"CALL\",\"Amount\":5000000,\"Tx\":null,\"PreviousHash\":\"cb19bc14bca61bee174e5d6591530ad72b3ab58e0c5a904baec5b5de85c65e88\",\"HashAlgorithm\":\"SHA256\",\"Version\":1.0,\"BitcoinAddress\":\"msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv\",\"TimeStamp\":\"2016-08-17T00:00:00\"}", actual);
        }
    }
}
