using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BitPoker.Controllers.Tests
{
    [TestClass]
    public class TableControllerTests
    {
        private BitPoker.Models.IRequest request;
        private const String REQUEST_ID = "a66a8eb4-ea1f-42bb-b5f2-03456094b1f6";

        private BitPoker.Controllers.Rest.TablesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new BitPoker.Controllers.Rest.TablesController();
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_Seat_2()
        {
            //private key 93GnRYsUXD4FPCiV46n8vqKvwHSZQgjnyuBvhNtqRvq3Ac26kVc

            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            _controller.TableRepo = new Repository.MockTableRepo();

            Models.Messages.JoinTableRequest request = new Models.Messages.JoinTableRequest()
            {
                Id = new Guid(REQUEST_ID),
                BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ",
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12),
                Seat = 2,
                Version = 1
            };

            Models.Messages.JoinTableResponse response = _controller.JoinTable(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Seat);
        }

        [TestMethod, TestCategory("Join Table")]
        public void Should_Join_Table_In_First_Empty_Seat()
        {
            //private key 91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf

            Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            _controller.TableRepo = new Repository.MockTableRepo();

            NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress address = secret.GetAddress();

            Models.Messages.JoinTableRequest request = new Models.Messages.JoinTableRequest()
            {
                Id = new Guid(REQUEST_ID),
                BitcoinAddress = address.ToString(),
                TableId = tableId,
                TimeStamp = new DateTime(2016, 12, 12),
                PublicKey = secret.PubKey.ToString(),
                Version = 1
            };

            Models.Messages.JoinTableResponse response = _controller.JoinTable(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Seat);
        }

        [TestMethod, Ignore, TestCategory("Join Table")]
        public void Should_Not_Join_Full_Table()
        {

        }

        [TestMethod, TestCategory("Buy In")]
        public void Should_Buy_In_To_Joined_Table()
        {
            //BitcoinSecret secret = new BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", Network.TestNet);

            //controller.TableRepo = new Repository.MockTableRepo();

            ////"2NAxESoeuyA4KqudU3v9fh5idiBorgLpkUj";
            //BitcoinScriptAddress tableAddress = new BitcoinScriptAddress("2NAxESoeuyA4KqudU3v9fh5idiBorgLpkUj", Network.TestNet);

            //BlockrTransactionRepository repo = new BlockrTransactionRepository(Network.TestNet);
            //BitcoinAddress myAddress = BitcoinAddress.Create("mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt", Network.TestNet);

            ////TODO:  MAKE A JSON OBJECT
            ////var utxos = await repo.GetUnspentAsync(myAddress.ToString());

            ////Coin[] coins = utxos.OrderByDescending(u => u.Amount).Select(u => new Coin(u.Outpoint, u.TxOut)).ToArray();

            ////Money minersFee = new Money(50000);

            ////var txBuilder = new TransactionBuilder();
            ////var tx = txBuilder
            ////    //.AddCoins(coins)
            ////    .AddCoins(utxos)
            ////    .AddKeys(secret)
            ////    .Send(tableAddress, new Money(100000))
            ////    .SendFees(minersFee)
            ////    .SetChange(myAddress)
            ////    .BuildTransaction(true);

            ////Debug.Assert(txBuilder.Verify(tx)); //check fully signed

            //request.Method = "BuyIn";
            //request.Params = new Models.Messages.BuyInRequest()
            //{
            //    BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
            //    //TableId = tableId,
            //    TimeStamp = new DateTime(2016, 12, 12) //,
            //    //TxID = "af651c3435b5a11a8d7792dbc1d20a20a23fce0beb0b6931bf0ce407bfd28a0a"
            //};

            //var response = _controller.Post(request);

            //Assert.IsNotNull(response);
            //Assert.IsNull(response.Error);
            //Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            //Models.Messages.BuyInResponse buyInResponse = response.Result as Models.Messages.BuyInResponse;
            //Assert.IsNotNull(buyInResponse);
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
    }
}
