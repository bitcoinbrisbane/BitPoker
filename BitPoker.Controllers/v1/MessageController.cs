using System;
using System.Web.Http;
using System.Linq;

namespace BitPoker.Controllers.v1
{
    public class MessageController : BaseController
    {
        public Repository.IPlayerRepository PlayerRepo { get; set; }

        public Repository.IHandRepository HandRepo { get; set; }

        public Repository.ITableRepository TableRepo { get; set; }

        public String privateKey;

        public Models.Contracts.IPokerContract Contract { get; set; }

        public MessageController()
        {
        }

        public Models.IResponse Get(Models.IRequest request)
        {
            Models.Messages.RPCResponse response = new Models.Messages.RPCResponse()
            {
                Id = request.Id
            };

            switch (request.Method.ToUpper())
            {
                case "GetPlayers":
                    response.Result = PlayerRepo.All();
                    break;
                case "GetTables":
                    response.Result = TableRepo.All();
                    break;
                case "GetHands":
                    throw new NotImplementedException();
                    //break;
                default:
                    response.Error = new Models.Messages.Code()
                    {
                        code = "-32601",
                        message = "method not found"
                    };
                    break;
            }

            return response;
        }

        /// <summary>
        /// Message controller either adds to 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public Models.IResponse Post(Models.IRequest request)
        {
            Models.Messages.RPCResponse response = new Models.Messages.RPCResponse()
            {
                Id = request.Id
            };

            switch (request.Method.ToUpper())
            {
                case "JOINTABLE":
                    Models.Messages.JoinTableRequest joinTableRequest = request.Params as Models.Messages.JoinTableRequest;
                    response.Result = JoinTable(joinTableRequest);
                    break;
                case "BUYIN":
                    break;
                case "POST SMALL BLIND":
                case "SMALL BLIND":
                case "SB":
                    AddSmallBlind(request.Params as Models.Messages.ActionMessage);
                    break;
                case "POST BIG BLIND":
                case "BIG BLIND":
                case "BB":
                    //AddBigBlind(request);
                    break;
                default:
                    response.Error = "Method not found";
                    break;
            }

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

            return response;
        }

        public void AddSmallBlind(Models.Messages.ActionMessage message)
        {
            if (message != null)
            {
                //Is the blind the correct amount?
                var table = TableRepo.Find(message.TableId);

                if (table != null && message.Action == "POST SMALL BLIND" && message.Amount == table.BigBlind)
                {
                    //handRepo.AddMessage(message);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private void AddBigBlind(Models.Messages.ActionMessage message)
        {
            if (message != null)
            {
                //Is the blind the correct amount?
                var table = TableRepo.Find(message.TableId);

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

        private Models.Messages.JoinTableResponse JoinTable(Models.Messages.JoinTableRequest request)
        {
            Models.Messages.JoinTableResponse response = new Models.Messages.JoinTableResponse();
            var table = this.TableRepo.Find(request.TableId);

            if (table != null)
            {
                for (Int32 i = 0; i < table.MaxPlayers; i++)
                {
                    if (table.Peers[i] == null)
                    {
                        response.Seat = i;
                        break;
                    }
                }

                return response;
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }
        }

        internal void Shuffle()
        {
            var message = new Models.Messages.DeckResponse();

            message.Deck = new Models.FiftyTwoCardDeck();
            message.Deck.Shuffle();
        }

        internal void Deck()
        {
            var message = new BitPoker.Models.Messages.DeckResponse();

            message.Deck = new BitPoker.Models.FiftyTwoCardDeck();
            message.Deck.Shuffle();
        }
    }
}