using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BitPoker.Controllers.Rest
{
    public abstract class BaseController : ApiController
    {
		private String _privateKey;
		private String _ccPrivateKey; //Colour coin

		public DateTime StartTime { get; set; }

		public DateTime LastRequest { get; set; }

		public Repository.IAddAndReadRepository<Models.Log> LogRepo { get; set; }

		internal String PrivateKey
		{
			set { _privateKey = value; }
		}

		public NBitcoin.Network Network { get; set; }

		/// <summary>
		/// Users bitcoin address
		/// </summary>
		public String LocalBitcoinAddress
		{
			get
			{
				if (!String.IsNullOrEmpty(_privateKey))
				{
					NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret(_privateKey);
					return secret.GetAddress().ToString();
				}
				else
				{
					return "";
				}
			}
		}

		internal void SetPrivateKey(Byte[] key)
		{
			_privateKey = Convert.ToBase64String(key);
		}

		internal void SetCCPrivateKey(Byte[] key)
		{
			_ccPrivateKey = Convert.ToBase64String(key);
		}

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

		internal String SignMessge(String message)
		{
			NBitcoin.BitcoinSecret secret = new NBitcoin.BitcoinSecret(_privateKey, this.Network);
			return secret.PrivateKey.SignMessage(message);
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