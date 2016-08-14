using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Models.Tests
{
    [TestClass]
    public class HandTests
    {
        [TestMethod]
        public void Should_Get_Small_Blind_Player()
        {
            Repository.IHandRepository mockHandRepo = new Repository.MockHandRepo();

            Hand hand = new Hand();
        }

        [TestMethod]
        public void Should_Get_Big_Blind_Player()
        {
        }
    }
}
