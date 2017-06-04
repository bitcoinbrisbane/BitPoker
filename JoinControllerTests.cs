using NUnit.Framework;
using System;
namespace BitPoker.Controllers.Rest.Tests
{
    [TestFixture()]
    public class JoinControllerTests
    {
        [Test()]
        public void Should_Join_In_Seat_0()
        {
            var controller = new BitPoker.Controllers.Rest.JoinController();
            controller.TableRepo = new BitPoker.Repository.Mocks.TableRepository();
            
            var actual = controller.Post(null);
        }
    }
}
