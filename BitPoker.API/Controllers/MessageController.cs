using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class MessageController : BaseController
    {
        //ALICE AS PER READ ME
        private const String ALICE_WIF = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        
        //BOB
        private const String BOB_WIF = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

        ///Get a mock message
        // GET api/<controller>/5
        public Models.Messages.ActionMessage Get(String id, Int32 index)
        {
            BitcoinSecret alice_secret = new BitcoinSecret(ALICE_WIF, NBitcoin.Network.Main);
            BitcoinSecret bob_secret = new BitcoinSecret(BOB_WIF, NBitcoin.Network.Main);

            Models.Messages.ActionMessage message;

            //Get a fake message at that index
            switch (index)
            {
                case 0:
                    //BOB 0.001 
                    message = new Models.Messages.ActionMessage()
                    {
                        HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"), //id
                        Action = "POST SB",
                        Amount = 100000,
                        Index = 0,
                        PublicKey = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo"
                    };

                    message.Signature = bob_secret.PrivateKey.SignMessage(message.ToString());
                    return message;
                case 1:
                    //ALICE 0.002 
                    message = new Models.Messages.ActionMessage();
                    break;
            }
        
            return new BitPoker.Models.Messages.ActionMessage();
        }

        /// <summary>
        /// Because hands can not be pushed via the api, this api provides a way to get historical action messages.
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="handId"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public BitPoker.Models.Messages.ActionMessage Get(String tableId, String handId, Int32 index)
        {
            if (MemoryCache.Default.Contains(tableId.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[tableId.ToString()];

                Models.Hand hand = table.Hands.SingleOrDefault(h => h.Id.ToString() == handId);

                if (hand != null)
                {
                    return hand.History.SingleOrDefault(h => h.Index == index); 
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        // POST api/<controller>
        [HttpPost]
        public Boolean Post(BitPoker.Models.Messages.ActionMessage message)
        {
            if (MemoryCache.Default.Contains(message.TableId.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[message.TableId.ToString()];

                if (table != null)
        {
                    ////Alice pub key
                    //String key = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";
                    //Byte[] aliceKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(key);
                    //NBitcoin.PubKey aliceKey = new NBitcoin.PubKey(aliceKeyAsBytes);

                    //Byte[] userKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(buyInRequest.PubKey);
                    //NBitcoin.PubKey userKey = new NBitcoin.PubKey(userKeyAsBytes);

                    //var scriptPubKey = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new[] { aliceKey, userKey });
                }
            }

            var address = new BitcoinPubKeyAddress(message.PublicKey);
            bool verified = address.VerifyMessage(message.ToString(), message.Signature);
            
            return verified;
        }
    }
}