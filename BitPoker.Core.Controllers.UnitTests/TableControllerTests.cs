using NBitcoin;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitPoker.Core.Controllers.UnitTests
{
    [TestFixture]
    public class TableControllerTests
    {
        private BitPoker.Net.RestHost.Controllers.TablesController _controller;

        [Test, Category("Table Controller")]
        public void Should_Join_Table_In_Seat_2()
        {
            //private key 93GnRYsUXD4FPCiV46n8vqKvwHSZQgjnyuBvhNtqRvq3Ac26kVc

            Guid tableId = new Guid("d6d9890d-0ca2-4b5d-ae98-fa4d45eb4363");
            _controller.TableRepo = new Repository.Mocks.TableRepository();

            //request.Method = "JoinTable";
            //request.Params = new Models.Messages.JoinTableRequest()
            //{
            //    BitcoinAddress = "n4HzHsTzz4kku4X21yaG1rjbqtVNDBsyKZ",
            //    TableId = tableId,
            //    TimeStamp = new DateTime(2016, 12, 12),
            //    Seat = 2,
            //    Version = 1
            //};
            var request = new BitPoker.Models.Messages.JoinTableRequest();
            var response = _controller.JoinTable(request);

            Assert.IsNotNull(response);
            //Assert.IsNull(response.Error);
            //Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            //Models.Messages.JoinTableResponse result = response.Result as Models.Messages.JoinTableResponse;
            //Assert.AreEqual(2, result.Seat);
        }

        [Test]
        public void Should_Join_Table_In_First_Empty_Seat()
        {
            //private key 91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf

            //Guid tableId = new Guid("be7514a3-e73c-4f95-ba26-c398641eea5c");
            //controller.TableRepo = new Repository.MockTableRepo();

            //NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", NBitcoin.Network.TestNet);
            //NBitcoin.BitcoinAddress address = secret.GetAddress();

            //request.Method = "JoinTable";
            //request.Params = new Models.Messages.JoinTableRequest()
            //{
            //    BitcoinAddress = "mypckwJUPVMi8z1kdSCU46hUY9qVQSrZWt",
            //    TableId = tableId,
            //    TimeStamp = new DateTime(2016, 12, 12),
            //    PublicKey = secret.PubKey.ToString()
            //};

            //var response = _controller.Post(request);

            //Assert.IsNotNull(response);
            //Assert.IsNull(response.Error);
            //Assert.AreEqual(response.Id.ToString(), REQUEST_ID);

            //Models.Messages.JoinTableResponse tableResponse = response.Result as Models.Messages.JoinTableResponse;
            //Assert.AreEqual(1, tableResponse.Seat);
        }

        [Test]
        public void Should_Not_Join_Full_Table()
        {

        }

        [Test]
        public void Should_Buy_In_To_Joined_Table()
        {
            BitcoinSecret secret = new BitcoinSecret("91xCHwaMdufE8fmxachVhU12wdTjY7nGbZeGgjx4JQSuSDNizhf", Network.TestNet);

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

        [Test]
        public void Should_Not_Buy_In_Under_The_Min()
        {

        }

        [Test]
        public void Should_Not_Buy_In_Over_The_Max()
        {

        }

        [Test]
        public void Should_Not_Buy_In_With_Unconfirmed_UTXo()
        {

        }

        [Test]
        public void Should_Not_Buy_In_With_Invalid_Tx()
        {

        }
    }
}
