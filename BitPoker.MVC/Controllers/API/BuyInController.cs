using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Runtime.Caching;

namespace BitPoker.MVC.Controllers
{
    public class BuyInController : BaseController
    {
        private readonly BitPoker.Repository.IHandRepository repo;
        private readonly BitPoker.Repository.ITableRepository tableRepo;

        public BuyInController()
        {
            this.tableRepo = new Repository.InMemoryTableRepo();
        }

        [HttpPost]
        public BitPoker.Models.Messages.BuyInResponseMessage Post(BitPoker.Models.Messages.BuyInRequestMessage buyInRequest)
        {
            if (!base.Verify(buyInRequest.BitcoinAddress, buyInRequest.ToString(), buyInRequest.Signature))
            {
                throw new Exceptions.SignatureNotValidException();
            }

            var table = tableRepo.Find(buyInRequest.TableId);

            if (table != null)
            {
                table.Players[2] = new BitPoker.Models.TexasHoldemPlayer()
                {
                    BitcoinAddress = buyInRequest.BitcoinAddress,
                    Stack = buyInRequest.Amount,
                    Position = 2,
                    IsBigBlind = false,
                    IsDealer = true,
                    IsSmallBlind = false,
                    IsTurnToAct = false,
                };

                //			
                const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
                NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
                NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();

                BitPoker.Models.Messages.BuyInResponseMessage response = new BitPoker.Models.Messages.BuyInResponseMessage();
                response.Table = table;


                //Create players
                BitPoker.Models.PlayerInfo[] players = new BitPoker.Models.PlayerInfo[2];
                players[0] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = alice_address.ToString(), UserAgent = "Bitpoker 0.1", IPAddress = "https://bitpoker.azurewebsites.net/api" };
                players[1] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = buyInRequest.BitcoinAddress };

                //Alice in seat 0, you in the sb
                //response.Players[0] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = alice_address.ToString(), UserAgent = "Bitpoker 0.1", IPAddress = "https://bitpoker.azurewebsites.net/api" };
                //response.Players[1] = new BitPoker.Models.PlayerInfo() { BitcoinAddress = buyInRequest.BitcoinAddress };

                //
                //BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table(2, 10);



                //Buy in to mock table
                //if (buyInRequest.TableId.ToString().ToUpper() == "D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363")
                //{

                //}
                //else
                //{
                //    table = null; //GetTableFromCache(buyInRequest.TableId);
                //}

                //Alice pub key
                const String alice_pub_key = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";
                Byte[] alicePubKeyAsBytes = NBitcoin.DataEncoders.Encoders.Hex.DecodeData(alice_pub_key);

                NBitcoin.PubKey alicePubKey = new NBitcoin.PubKey(alicePubKeyAsBytes);
                NBitcoin.PubKey userKey = new NBitcoin.PubKey(buyInRequest.PubKey);

                var scriptPubKey = NBitcoin.PayToMultiSigTemplate.Instance.GenerateScriptPubKey(2, new[] { alicePubKey, userKey });

                //As its heads up, create the first hand and deck
                BitPoker.Models.Hand hand = new BitPoker.Models.Hand(players);

                //table.Hands[0] = hand;

                ////Save back to memory
                CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = new TimeSpan(0, 30, 0) };
                MemoryCache.Default.Set(buyInRequest.TableId.ToString(), table, policy);

                return response;
            }
            else
            {
                throw new Exceptions.TableNotFoundException();
            }
        }
    }
}