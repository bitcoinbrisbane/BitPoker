using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    public class MessageController : BaseController
    {
        private readonly BitPoker.Repository.IMessagesRepository repo;
        private readonly BitPoker.Repository.IHandRepository handRepo;

        //ALICE AS PER READ ME
        private const String ALICE_WIF = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        
        //BOB
        private const String BOB_WIF = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

        public MessageController()
        {
            this.handRepo = new Repository.InMemoryHandRepo();
        }

        ///Get a mock message
        // GET api/<controller>/5
        public BitPoker.Models.Messages.ActionMessage Get(Guid handId, Int32 index)
        {
            BitcoinSecret alice_secret = new BitcoinSecret(ALICE_WIF, NBitcoin.Network.Main);
            BitcoinSecret bob_secret = new BitcoinSecret(BOB_WIF, NBitcoin.Network.Main);

            BitPoker.Models.Messages.ActionMessage message = handRepo.Find(handId).History[index];
            return new BitPoker.Models.Messages.ActionMessage();
        }

        [HttpPost]
        public Boolean Post(BitPoker.Models.Messages.ActionMessage message)
        {
            //NOTE:  THIS IS WHERE THE STUB AI LOGIC SHOULD EXIST.
            //
            //if (MemoryCache.Default.Contains(message.TableId.ToString()))
            //{
            //    Models.Hand.Table table = (Models.Hand.Table)MemoryCache.Default[message.TableId.ToString()];

            //    if (table != null)
            //    {
            //        ////Alice pub key
            //        //String key = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";
            //        //Byte[] aliceKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(key);
            //        //NBitcoin.PubKey aliceKey = new NBitcoin.PubKey(aliceKeyAsBytes);

            //        //Byte[] userKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(buyInRequest.PubKey);
            //        //NBitcoin.PubKey userKey = new NBitcoin.PubKey(userKeyAsBytes);

            //        //var scriptPubKey = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new[] { aliceKey, userKey });
            //    }
            //}

            BitcoinPubKeyAddress address = new BitcoinPubKeyAddress(message.PublicKey);
            bool verified = address.VerifyMessage(message.ToString(), message.Signature);
            
            if (verified != true)
            {
                throw new Exceptions.SignatureNotValidException();
            }
            else
            {
                //switch (message.Action.ToLower())
                //{

                //}
            }


            //Some API
            var hand = this.handRepo.Find(message.HandId);

            var i = hand.History.Count;
            //message.Index[i];

            return verified;
        }
    }
}