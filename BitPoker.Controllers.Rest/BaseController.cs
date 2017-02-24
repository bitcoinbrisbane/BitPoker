using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public abstract class BaseController : ApiController
    {
		public DateTime StartTime { get; set; }

		public DateTime LastRequest { get; set; }

		public Repository.IAddAndReadRepository<Models.Log> LogRepo { get; set; }

		/// <summary>
		/// Users bitcoin address
		/// </summary>
		public String _localBitcoinAddress;

		internal Boolean Verify(Models.Messages.IMessage message)
		{
			//NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
			//var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
			//bool verified = pubKey.VerifyMessage(message, signature);

			//return verified;
			return Verify(message.BitcoinAddress, message.Id.ToString(), message.Signature);
		}

        internal Boolean Verify(String address, String message, String signature)
        {
            //NBitcoin.BitcoinAddress a = NBitcoin.BitcoinAddress.Create(address);
            var pubKey = new NBitcoin.BitcoinPubKeyAddress(address);
            bool verified = pubKey.VerifyMessage(message, signature);

            //return verified;
            return true;
        }

		internal Boolean ValidateTx(String tx)
		{
			NBitcoin.Transaction transaction = new NBitcoin.Transaction();
			NBitcoin.TransactionBuilder builder = new NBitcoin.TransactionBuilder();

			return true;
		}

        internal void AddLog(String message)
        {
			LastRequest = DateTime.UtcNow;

			//Guid id = Guid.NewGuid();
			//LogRepo.Add(new Models.Log() { Id = id, Message = message, TimeStamp = DateTime.UtcNow });
        }
    }
}