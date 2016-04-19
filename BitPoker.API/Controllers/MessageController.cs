using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class MessageController : ApiController
    {
        //ALICE AS PER READ ME
        private const String ALICE_WIF = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        
        //BOB
        private const String BOB_WIF = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

        ///Get a mock message
        // GET api/<controller>/5
        public Models.Messages.ActionMessage Get(String id, Int32 index)
        {
            NBitcoin.BitcoinSecret alice_secret = new BitcoinSecret(ALICE_WIF, NBitcoin.Network.Main);
            NBitcoin.BitcoinSecret bob_secret = new BitcoinSecret(BOB_WIF, NBitcoin.Network.Main);

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
                    message = new Models.Messages.ActionMessage()
                    {
                        HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"), //id
                        Action = "POST BB",
                        Amount = 200000,
                        Index = 0,
                        PublicKey = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv"
                    };

                    message.Signature = alice_secret.PrivateKey.SignMessage(message.ToString());
                    return message;
            }

            throw new IndexOutOfRangeException();
        }

        // POST api/<controller>
        public Boolean Post(Models.Messages.ActionMessage message)
        {
            var address = new BitcoinPubKeyAddress(message.PublicKey);
            bool verified = address.VerifyMessage(message.ToString(), message.Signature);
            
            return verified;
        }
    }
}