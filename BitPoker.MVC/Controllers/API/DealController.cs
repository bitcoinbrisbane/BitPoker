using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitPoker.MVC.Controllers
{
    public class DealController : ApiController //, IDisposable
    {
        private readonly BitPoker.Repository.ITableRepository tableRepo;
        private readonly BitPoker.Repository.IHandRepository handRepo;

        public DealController()
        {
            this.tableRepo = new Repository.InMemoryTableRepo();
        }

        public DealController(BitPoker.Repository.ITableRepository tableRepo)
        {
            this.tableRepo = tableRepo;
        }

        /// <summary>
        /// Kick off new hand
        /// </summary>
        /// <param name="id">Table id</param>
        /// <returns></returns>
        [HttpPost]
        public String Post(Guid id)
        {
            var table = tableRepo.Find(id);

            if (table != null)
            {
                Guid handId = Guid.NewGuid();
                List<BitPoker.Models.Messages.ActionMessage> actions = new List<BitPoker.Models.Messages.ActionMessage>();

                //todo:  change to position
                var sb = table.Players[0];

                const String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
                NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
                NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();
                const String alice_pubkey = "041FA97EFD760F26E93E91E29FDDF3DDDDD3F543841CF9435BDC156FB73854F4BF22557798BA535A3EE89A62238C5AFC7F8BF1FA0985DC4E1A06C25209BAB78BD1";

                BitPoker.Models.Messages.ActionMessage smallBlind = new BitPoker.Models.Messages.ActionMessage()
                {
                    Index = 0,
                    Action = "SMALL BLIND",
                    Amount = table.SmallBlind,
                    BitcoinAddress = sb.BitcoinAddress,
                    HandId = handId,
                    PublicKey = alice_pubkey,
                    TableId = id
                };

                smallBlind.Signature = alice_secret.PrivateKey.SignMessage(smallBlind.ToString());
                actions.Add(smallBlind);

                var bb = table.Players[1];

                const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";
                NBitcoin.BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);
                NBitcoin.BitcoinAddress bob_address = bob_secret.GetAddress();
                const String bob_pubkey = "04F48396AC675B97EEB54E57554827CC2B937C2DAE285A9198F9582B15C920D91309BC567858DC63357BCD5D24FD8C041CA55DE8BAE62C7315B0BA66FE5F96C20D";

                //NBitcoin.Crypto.Hashes.SHA256(NBitcoin.DataEncoders.Encoders.ASCII.(smallBlind.ToString()));
                Byte[] hash = NBitcoin.Crypto.Hashes.SHA256(NBitcoin.DataEncoders.Encoders.ASCII.DecodeData(smallBlind.ToString()));

                BitPoker.Models.Messages.ActionMessage bigBlind = new BitPoker.Models.Messages.ActionMessage()
                {
                    Index = 1,
                    Action = "BIG BLIND",
                    Amount = table.BigBlind,
                    BitcoinAddress = bb.BitcoinAddress,
                    HandId = handId,
                    PreviousHash = NBitcoin.DataEncoders.Encoders.ASCII.EncodeData(hash),
                    PublicKey = bob_pubkey,
                    TableId = id
                };
            }

            return "";
        }
    }
}
