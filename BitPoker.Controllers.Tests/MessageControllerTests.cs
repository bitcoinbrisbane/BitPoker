using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BitPoker.Controllers.Tests
{
    [TestClass]
    public class MessageControllerTests
    {
        private BitPoker.Models.IRequest request;
        private const String REQUEST_ID = "a66a8eb4-ea1f-42bb-b5f2-03456094b1f6";

        private MessageController _controller;

        [TestInitialize]
        public void Setup()
        {
            request = new Models.Messages.RPCRequest()
            {
                Id = new Guid(REQUEST_ID)
            };

            _controller = new MessageController();
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_Seat_2()
        {
            //private key 93GnRYsUXD4FPCiV46n8vqKvwHSZQgjnyuBvhNtqRvq3Ac26kVc

            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "JoinTable";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12),
                Version = 1
            };

            var response = controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Error);
            Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            Models.Messages.JoinTableResponse result = response.Result as Models.Messages.JoinTableResponse;
            Assert.AreEqual(2, result.Seat);
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_First_Empty_Seat()
        {
            //private key 91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf

            Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress address = secret.GetAddress();

            request.Method = "JoinTable";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12),
                PublicKey = secret.PubKey.ToString()
            };

            var response = controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Error);
            Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            Models.Messages.JoinTableResponse tableResponse = response.Result as Models.Messages.JoinTableResponse;
            Assert.AreEqual(1, tableResponse.Seat);
        }

        [TestMethod, Ignore, TestCategory("Join Table")]
        public void Should_Not_Join_Full_Table()
        {

        }

        [TestMethod, TestCategory("Buy In")]
        public void Should_Buy_In_To_Joined_Table()
        {
            BitcoinSecret secret = new BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", Network.TestNet);

            

            MessageController controller = new MessageController();
            controller.TableRepo = new Repository.MockTableRepo();

            //"2NAxESoeuyA4KqudU3v9fh5idiBorgLpkUj";
            BitcoinScriptAddress tableAddress = new BitcoinScriptAddress("2NAxESoeuyA4KqudU3v9fh5idiBorgLpkUj", Network.TestNet);

            BlockrTransactionRepository repo = new BlockrTransactionRepository(Network.TestNet);
            BitcoinAddress myAddress = BitcoinAddress.Create("mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt", Network.TestNet);

            //TODO:  MAKE A JSON OBJECT
            //var utxos = await repo.GetUnspentAsync(myAddress.ToString());

            //Coin[] coins = utxos.OrderByDescending(u => u.Amount).Select(u => new Coin(u.Outpoint, u.TxOut)).ToArray();

            //Money minersFee = new Money(50000);

            //var txBuilder = new TransactionBuilder();
            //var tx = txBuilder
            //    //.AddCoins(coins)
            //    .AddCoins(utxos)
            //    .AddKeys(secret)
            //    .Send(tableAddress, new Money(100000))
            //    .SendFees(minersFee)
            //    .SetChange(myAddress)
            //    .BuildTransaction(true);

            //Debug.Assert(txBuilder.Verify(tx)); //check fully signed

            request.Method = "BuyIn";
            request.Params = new Models.Messages.BuyInRequest()
            {
                BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
                //TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12),
                TxID = "af651c3435b5a11a8d7792dbc1d20a20a23fce0beb0b6931bf0ce407bfd28a0a"
            };

            var response = controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Error);
            Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            Models.Messages.BuyInResponse buyInResponse = response.Result as Models.Messages.BuyInResponse;
            Assert.IsNotNull(buyInResponse);
        }

        [TestMethod, TestCategory("Buy In")]
        public void Should_Not_Buy_In_Under_The_Min()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Buy_In_Over_The_Max()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Buy_In_With_Unconfirmed_UTXo()
        {

        }

        [TestMethod, Ignore, TestCategory("Buy In")]
        public void Should_Not_Buy_In_With_Invalid_Tx()
        {

        }

        [TestMethod, TestCategory("Small Blind")]
        public void Should_Post_Small_Blind()
        {
            Guid handId = new Guid("c5123f5c-c8d0-4d29-b7bf-111a6330ba62");
            MessageController controller = new MessageController();
            controller.HandRepo = new Repository.MockHandRepo();

            request.Method = "SmallBlind";
            request.Params = new Models.Messages.JoinTableRequest()
            {
                BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
                TableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16"),
                TimeStamp = new DateTime(2016, 12, 12)
            };

            var response = controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Error);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());
        }

        [TestMethod, Ignore, TestCategory("Small Blind")]
        public void Should_Not_Be_Able_To_Post_Small_Blind()
        {

        }

        [TestMethod, Ignore, TestCategory("Small Blind")]
        public void Should_Not_Post_Small_Blind_With_Invalid_Signature()
        {

        }

        [TestMethod, Ignore, TestCategory("Big Blind")]
        public void Should_Post_Big_Blind()
        {

        }

        [TestMethod, TestCategory("Shuffle")]
        public void Should_Shuffle_Deck()
        {
            //Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            Guid handId = new Guid("91dacf01-4c4b-4656-912b-2c3a11f6e516");
            _controller = new MessageController();
            _controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "Shuffle";
            request.Params = new Models.Messages.ShuffleRequest()
            {
                HandId = handId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());

            Assert.IsNotNull(response.Result);
            Models.Messages.ShuffleResponse shuffleResponse = response.Result as Models.Messages.ShuffleResponse;

            Assert.AreEqual(52, shuffleResponse.Deck.Cards.Count);
        }

        [TestMethod, TestCategory("Shuffle")]
        public void Should_Shuffle_Deck_With_Invalid_HandId()
        {
            Guid handId = new Guid("b1fbb2b7-a4a6-4831-a277-0fb6c2becb02");
            _controller = new MessageController();
            _controller.TableRepo = new Repository.MockTableRepo();

            request.Method = "Shuffle";
            request.Params = new Models.Messages.ShuffleRequest()
            {
                HandId = handId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Error);
        }

        [TestMethod, TestCategory("Deal")]
        public void Should_Deal_Cards_Heads_Up()
        {
            Guid tableId = new Guid("bf368921-346a-42d8-9cb8-621f9cad5e16");
            _controller = new MessageController();
            _controller.HandRepo = new Repository.MockHandRepo();

            request.Method = "Deal";
            request.Params = new Models.Messages.DealRequest()
            {
                TableId = tableId
            };

            var response = _controller.Post(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(REQUEST_ID, response.Id.ToString());

            Assert.IsNotNull(response.Result);
            //Assert.IsNotNull(response.Result);

            Models.Messages.DealResponse dealResponse = response.Result as Models.Messages.DealResponse;
            Assert.AreEqual(52, dealResponse.Deck.Cards.Count);
        }

        [TestCleanup]
        public void TearDown()
        {
            _controller = null;
        }
    }
}
