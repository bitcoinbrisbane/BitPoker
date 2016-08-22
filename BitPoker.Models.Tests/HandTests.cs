using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BitPoker.Models.ExtensionMethods;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class HandTests
    {
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
        public void Should_Get_Big_Blind_Player()
        {
        }
    }
}
