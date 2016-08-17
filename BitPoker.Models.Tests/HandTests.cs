using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BitPoker.Models.ExtensionMethods;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class HandTests
    {
        private Hand headsUpHand;

        [TestInitialize]
        public void Setup()
        {
            Repository.IHandRepository mockHandRepo = new Repository.MockHandRepo();
            Repository.IPlayerRepository mockPlayers = new Repository.MockPlayerRepo();

            var aliceAndBob = mockPlayers.All();
            headsUpHand = new Hand(aliceAndBob.ToArray());
        }

        [TestMethod]
        public void Should_Get_Small_Blind_Player_In_Heads_Up()
        {
            Repository.IHandRepository mockHandRepo = new Repository.MockHandRepo();
            Repository.IPlayerRepository mockPlayers = new Repository.MockPlayerRepo();

            var aliceAndBob = mockPlayers.All();
            Hand hand = new Hand(aliceAndBob.ToArray());

            var bb = hand.GetSmallBlind();

            Assert.IsTrue(bb.BitcoinAddress == "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo");
        }

        [TestMethod]
        public void Should_Get_Big_Blind_Player_In_Heads_Up()
        {
            Repository.IHandRepository mockHandRepo = new Repository.MockHandRepo();
            Repository.IPlayerRepository mockPlayers = new Repository.MockPlayerRepo();

            var aliceAndBob = mockPlayers.All();
            Hand hand = new Hand(aliceAndBob.ToArray());

            var bb = hand.GetSmallBlind();

            Assert.IsTrue(bb.BitcoinAddress == "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo");
        }

        [TestMethod]
        public void Should_Get_Hand_ToString()
        {
            String actual = headsUpHand.ToString();
            Assert.AreEqual("2d59577d-0f42-4a11-ae14-78ccf5b4b2e000000000-0000-0000-0000-0000000000000", actual);
        }
    }
}
