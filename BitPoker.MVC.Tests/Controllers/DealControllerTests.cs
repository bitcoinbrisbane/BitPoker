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
            BitPoker.Repository.ITableRepository mockRepo = new BitPoker.Repository.MockTableRepo();
            var controller = new BitPoker.MVC.Controllers.DealController(mockRepo);
            Guid tableId = new Guid("D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363");
            controller.Post(tableId);
        }
    }
}
