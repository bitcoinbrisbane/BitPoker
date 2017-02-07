using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class PeerTests
    {
        [TestMethod]
        public void Should_Get_Peer_As_String()
        {
            Models.Peer peer = new Peer()
            {
                BitcoinAddress = "mgXu8V4ZaSzx9souKLPCnCAE9EhRkm7GZR",
                IPAddress = "192.168.0.1:8080"
            };

            String actual = peer.ToString();
            Assert.AreEqual("mgXu8V4ZaSzx9souKLPCnCAE9EhRkm7GZR, 192.168.0.1:8080", actual);
        }
    }
}
