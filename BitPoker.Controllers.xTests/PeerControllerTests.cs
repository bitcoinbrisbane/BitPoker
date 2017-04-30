using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BitPoker.Controllers.xTests
{
    public class PeerControllerTests
    {
        [Fact]
        public void Should_Get_All_Peers()
        {
            BitPoker.Controllers.Rest.PeersController controller = new Rest.PeersController();
            controller.PeerRepo = new Repository.Mocks.PeerRepository();

            var peers = controller.Get();

            Assert.NotNull(peers);
        }
    }
}