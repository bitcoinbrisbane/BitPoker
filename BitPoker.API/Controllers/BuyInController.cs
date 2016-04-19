using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public class BuyInController : ApiController
    {
        public Int32 Post(Models.Messages.BuyInRequestMessage buyInRequest)
        {
            //Alice pub key
            String key = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";
            Byte[] aliceKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(key);
            NBitcoin.PubKey aliceKey = new NBitcoin.PubKey(aliceKeyAsBytes);

            Byte[] userKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(buyInRequest.PubKey);
            NBitcoin.PubKey userKey = new NBitcoin.PubKey(userKeyAsBytes);

            var scriptPubKey = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new[] { aliceKey, userKey });

            return 0;
        }
    }
}