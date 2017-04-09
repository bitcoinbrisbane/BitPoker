using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.MVC.Tests.Controllers
{
    [TestClass]
    public class DealControllerTests
    {
        [TestMethod, TestCategory("Controllers"), Ignore]
        public void Should_Get_New_Deal()
        {
            BitPoker.Repository.ITableRepository mockRepo = new BitPoker.Repository.Mocks.TableRepository();
            var controller = new BitPoker.MVC.Controllers.DealController(mockRepo, null);
            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            //controller.Post(tableId);
        }
    }
}