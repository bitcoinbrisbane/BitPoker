using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BitPoker.Models.ExtensionMethods;

namespace BitPoker.MVC.Controllers
{
    public class DealController : BaseController
    {
        private readonly BitPoker.Repository.ITableRepository tableRepo;
        private readonly BitPoker.Repository.IHandRepository handRepo;

        public DealController()
        {
            this.tableRepo = new Repository.InMemoryTableRepo();
            this.handRepo = new Repository.InMemoryHandRepo();
        }

        public DealController(BitPoker.Repository.ITableRepository tableRepo, BitPoker.Repository.IHandRepository handRepo)
        {
            this.tableRepo = tableRepo;
            this.handRepo = handRepo;
        }


        [HttpPost]
        public BitPoker.Models.Hand Post(BitPoker.Models.Messages.DealRequest request)
        {
            var table = tableRepo.Find(request.TableId);

            if (table != null)
            {
                BitPoker.Models.Hand hand = new BitPoker.Models.Hand()
                {
                    Deck = request.Deck
                };

                //todo:  change to position
                var sb = table.Players[0];

                const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
                NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
                NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();
                const String alice_pubkey = "041fa97efd760f26e93e91e29fddf3ddddd3f543841cf9435bdc156fb73854f4bf22557798ba535a3ee89a62238c5afc7f8bf1fa0985dc4e1a06c25209bab78bd1";

                BitPoker.Models.Messages.ActionMessage smallBlind = new BitPoker.Models.Messages.ActionMessage()
                {
                    Id = new Guid("4bc7f305-aa16-450a-a3be-aad8fba7f425"),
                    Index = 0,
                    Action = "SMALL BLIND",
                    Amount = table.SmallBlind,
                    BitcoinAddress = sb.BitcoinAddress,
                    HandId = hand.Id,
                    //PublicKey = alice_pubkey,
                    TableId = request.TableId
                };

                smallBlind.Signature = alice_secret.PrivateKey.SignMessage(smallBlind.ToString());
                hand.AddMessage(smallBlind);

                //var bb = table.Players[1];

                //const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";
                //NBitcoin.BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);
                //NBitcoin.BitcoinAddress bob_address = bob_secret.GetAddress();
                //const String bob_pubkey = "04f48396ac675b97eeb54e57554827cc2b937c2dae285a9198f9582b15c920d91309bc567858dc63357bcd5d24fd8c041ca55de8bae62c7315b0ba66fe5f96c20d";

                ////NBitcoin.Crypto.Hashes.SHA256(NBitcoin.DataEncoders.Encoders.ASCII.(smallBlind.ToString()));
                //Byte[] hash = NBitcoin.Crypto.Hashes.SHA256(NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(smallBlind.ToString()));

                //BitPoker.Models.Messages.ActionMessage bigBlind = new BitPoker.Models.Messages.ActionMessage()
                //{
                //    Id = new Guid("d10cc043-4df3-4d41-8b31-8dd573824c8b"),
                //    Index = 1,
                //    Action = "BIG BLIND",
                //    Amount = table.BigBlind,
                //    BitcoinAddress = bb.BitcoinAddress,
                //    HandId = hand.Id,
                //    PreviousHash = NBitcoin.DataEncoders.Encoders.ASCII.EncodeData(hash),
                //    PublicKey = bob_pubkey,
                //    TableId = request.TableId
                //};

                //bigBlind.Signature = bob_secret.PrivateKey.SignMessage(bigBlind.ToString());
                //hand.AddMessage(bigBlind);

                this.handRepo.Add(hand);
                return hand;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
