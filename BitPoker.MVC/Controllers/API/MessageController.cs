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
        //private readonly BitPoker.Repository.IMessagesRepository repo;
        private readonly BitPoker.Repository.IHandRepository handRepo;
        private readonly BitPoker.Repository.ITableRepository tableRepo;

        //ALICE AS PER READ ME
        private const String ALICE_WIF = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
        
        //BOB
        private const String BOB_WIF = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

        public MessageController()
        {
            this.handRepo = new Repository.InMemoryHandRepo();
            this.tableRepo = new Repository.InMemoryTableRepo();
        }

        /////Get a mock message
        //public BitPoker.Models.Messages.ActionMessage Get(Guid handId, Int32 index)
        //{
        //    BitcoinSecret alice_secret = new BitcoinSecret(ALICE_WIF, NBitcoin.Network.Main);
        //    BitcoinSecret bob_secret = new BitcoinSecret(BOB_WIF, NBitcoin.Network.Main);

        //    BitPoker.Models.Messages.ActionMessage message = handRepo.Find(handId).History[index];
        //    return new BitPoker.Models.Messages.ActionMessage();
        //}

        /// <summary>
        /// Message controller either adds to 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Boolean Post(BitPoker.Models.IRequest request)
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




            //BitcoinPubKeyAddress address = new BitcoinPubKeyAddress(request.BitcoinAddress);
            //bool verified = address.VerifyMessage(request.ToString(), request.Signature);

            //if (verified != true)
            //{
            //    throw new Exceptions.SignatureNotValidException();
            //}
            //else
            //{
            //    //Some API
            //    var hand = this.handRepo.Find(request.HandId);

            //    foreach(BitPoker.Models.Messages.ActionMessage previousAction in hand.History)
            //    {
            //        //verify
            //    }

            //    var lastAction = hand.History.Last();
            //    String[] allowedAction = new String[1];

            //    switch (lastAction.Action.ToUpper())
            //    {
            //        case "POST SMALL BLIND":
            //        case "SMALL BLIND":
            //        case "SB":
            //            allowedAction[0] = "BIG BLIND";
            //            break;

            //        case "POST BIG BLIND":
            //        case "BIG BLIND":
            //        case "BB":
            //            break;
            //    }

            //    switch(request.Action.ToUpper())
            //    {
            //        case "POST SMALL BLIND":
            //        case "SMALL BLIND":
            //        case "SB":

            //            break;
            //        case "POST BIG BLIND":
            //        case "BIG BLIND":
            //        case "BB":
            //            AddBigBlind(request);
            //            break;
            //    }

            //    //Now notify next player for their action:
            //    hand = this.handRepo.Find(request.HandId);
            //    lastAction = hand.History.Last();

            //    String bitcoinAddress = lastAction.BitcoinAddress;
            //    var player = hand.Players[hand.PlayerToAct];
            //}

            //return verified;
            return false;
        }

        public void AddSmallBlind(BitPoker.Models.Messages.ActionMessage message)
        {

        }

        public void AddBigBlind(BitPoker.Models.Messages.ActionMessage message)
        {
            if (message != null)
            {
                //Is the blind the correct amount?
                var table = tableRepo.Find(message.TableId);

                if (table != null && message.Action == "POST BIG BLIND" && message.Amount == table.BigBlind)
                {
                    //handRepo.AddMessage(message);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}