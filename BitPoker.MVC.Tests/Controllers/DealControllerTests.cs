using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.MVC.Tests.Controllers
{
    [TestClass]
    public class DealControllerTests
    {
        [TestMethod]
        public void Should_Get_New_Deal()
        {
            BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.MockTableRepo();
            BitPoker.Repository.IHandRepository handRepo = new BitPoker.Repository.MockHandRepo();

            var controller = new BitPoker.MVC.Controllers.DealController(tableRepo, handRepo);
            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            BitPoker.Models.Hand hand = controller.Post(tableId);

            Assert.IsNotNull(hand);
        }
    }
}
