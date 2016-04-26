using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;

namespace BitPoker.API.Controllers
{
    public class BuyInController : BaseController
    {
        [HttpPost]
        public BitPoker.Models.Messages.BuyInResponseMessage Post(BitPoker.Models.Messages.BuyInRequestMessage buyInRequest)
        {
            if (!base.Verify(buyInRequest.BitcoinAddress, buyInRequest.Signature))
            {
                //throw new 
            }

            //			
            const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();

            BitPoker.Models.Messages.BuyInResponseMessage response = new BitPoker.Models.Messages.BuyInResponseMessage(2);

            //Create players
            BitPoker.Models.PlayerInfo[] players = new BitPoker.Models.PlayerInfo[2];
            players[0] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = alice_address.ToString(), UserAgent = "Bitpoker 0.1", Address = "https://bitpoker.azurewebsites.net/api", Stack = 1000000 };
            players[1] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = buyInRequest.BitcoinAddress, Stack = buyInRequest.Amount };

            //Alice in seat 0, you in the sb
            response.Players[0] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = alice_address.ToString(), UserAgent = "Bitpoker 0.1", Address = "https://bitpoker.azurewebsites.net/api", Stack = 1000000 };
            response.Players[1] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = buyInRequest.BitcoinAddress, Stack = buyInRequest.Amount };

            //
            Models.Table table = new Models.Table();

            //Buy in to mock table
            if (buyInRequest.TableId.ToString().ToUpper() == "D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363")
            {

            }
            else
            {
                table = GetTableFromCache(buyInRequest.TableId);
            }

            //Alice pub key
            const String alice_pub_key = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";
            Byte[] alicePubKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(alice_pub_key);

            NBitcoin.PubKey alicePubKey = new NBitcoin.PubKey(alicePubKeyAsBytes);
            NBitcoin.PubKey userKey = new NBitcoin.PubKey(buyInRequest.PubKey);

            var scriptPubKey = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new[] { alicePubKey, userKey });
            
            //As its heads up, create the first hand and deck
            Models.Hand hand = new Models.Hand(players);

            table.Hands[0] = hand;

            ////Save back to memory
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = new TimeSpan(0, 30, 0)};
            MemoryCache.Default.Set(buyInRequest.TableId.ToString(), table, policy); 
                
            return response;
        }
    }
}