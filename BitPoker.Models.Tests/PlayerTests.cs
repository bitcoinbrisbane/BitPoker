using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Should_Get_Player_As_ToString()
        {
            PlayerInfo alice = new PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            String actual = alice.ToString();

        }

        [TestMethod]
        public void Should_Get_Player_As_Json()
        {
            PlayerInfo alice = new PlayerInfo()
            {
                BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                LastSeen = DateTime.UtcNow.AddSeconds(-5),
                IPAddress = "https://www.bitpoker.io/api/players/msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv",
                Latency = new TimeSpan(0, 0, 0, 0, 200)
            };

            String actual = alice.ToString();

        }
    }
}
