using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;

namespace BitPoker.MVC.Tests
{
    [TestClass]
    public class PlayersControllerTests
    {
        private const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        private const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";
        private static BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
        private static BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);
        private static BitcoinAddress alice = alice_secret.GetAddress();
        private static BitcoinAddress bob = bob_secret.GetAddress();

        [TestMethod]
        public void Add_Player_To_InMemory_Repo()
        {
            BitPoker.Models.Messages.AddPlayerRequest message = new BitPoker.Models.Messages.AddPlayerRequest();
            message.BitcoinAddress = alice.ToString();
            message.Player = new BitPoker.Models.PlayerInfo() { BitcoinAddress = alice.ToString(), IPAddress = "localhost" };

            message.Signature = alice_secret.PrivateKey.SignMessage(message.Id.ToString());

            BitPoker.MVC.Controllers.PlayersController controller = new BitPoker.MVC.Controllers.PlayersController();
            controller.Post(message);

            var players = controller.Get();
            Assert.IsNotNull(players);
            
        }
    }
}
