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
        // GET api/<controller>/5
        public Models.Messages.ActionMessage Get(String id)
        {
            return new Models.Messages.ActionMessage();
        }

        // POST api/<controller>
        public Boolean Post(Models.Messages.ActionMessage message)
        {
            var address = new BitcoinPubKeyAddress(message.PublicKey);
            bool verified = address.VerifyMessage(message.Action, message.Signature);
            
            return verified;
        }
    }
}