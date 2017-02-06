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
                //Get peers that user knows about
                case "GetPeers":
                    response.Result = PlayerRepo.All();
                    break;
                //Get my tables
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
            //Models.Messages.RPCResponse response = new Models.Messages.RPCResponse()
            //{
            //    Id = id.Id
            //};

            throw new NotImplementedException();

            //try
            //{
            //    switch (request.Method.ToUpper())
            //    {
            //        case "GETTABLES":
            //            response.Result = TableRepo.All();
            //            break;
            //        case "JOINTABLE":
            //            Models.Messages.JoinTableRequest joinTableRequest = request.Params as Models.Messages.JoinTableRequest;
            //            response.Result = JoinTable(joinTableRequest);
            //            break;
            //        case "BUYIN":
            //            Models.Messages.BuyInRequest buyInRequest = request.Params as Models.Messages.BuyInRequest;
            //            response.Result = BuyIn(buyInRequest);
            //            break;
            //        case "SHUFFLE":
            //            response.Result = Shuffle();
            //            break;
            //        case "DEAL":
            //            Models.Messages.DealRequest dealRequest = request.Params as Models.Messages.DealRequest;
            //            break;
            //        case "SMALLBLIND":
            //        case "SB":
            //            var smallBlindRequest = request.Params as Models.Messages.ActionMessage;
            //            response.Result = ProcessSmallBlind(smallBlindRequest);
            //            break;
            //        case "BIGBLIND":
            //        case "BB":
            //            var bigBlindRequest = request.Params as Models.Messages.ActionMessage;
            //            response.Result = ProcessBigBlind(bigBlindRequest);
            //            break;
            //        default:
            //            response.Error = new Models.Messages.Code()
            //            {
            //                code = "-32601",
            //                message = "method not found"
            //            };
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.Error = ex.Message;
            //}





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






            //return response;
        }

        private Models.Messages.ActionMessageResponse ProcessSmallBlind(Models.Messages.ActionMessage message)
        {
            if (message != null)
            {
                //Is the blind the correct amount?
                var table = TableRepo.Find(message.TableId);

                if (message.Amount == table.BigBlind)
                {
                    //handRepo.AddMessage(message);
                    return new Models.Messages.ActionMessageResponse();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private Models.Messages.ActionMessageResponse ProcessBigBlind(Models.Messages.ActionMessage message)
        {
            if (message != null)
            {
                //Is the blind the correct amount?
                var table = TableRepo.Find(message.TableId);

                if (table != null && message.Amount == table.BigBlind)
                {
                    //handRepo.AddMessage(message);
                    return new Models.Messages.ActionMessageResponse();
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private Models.Messages.DeckResponse Shuffle()
        {
            return Shuffle(new Models.FiftyTwoCardDeck());
        }

        private Models.Messages.DeckResponse Shuffle(Models.IDeck deck)
        {
            var message = new Models.Messages.DeckResponse()
            {
                Deck = deck
            };

            message.Deck.Shuffle();
            return message;
        }

        private Models.Messages.DeckResponse NewDeck()
        {
            var response = new Models.Messages.DeckResponse()
            {
                Deck = new Models.FiftyTwoCardDeck()
            };

            return response;
        }

        private Models.Messages.DealResponse NewDeal(Guid handId)
        {
            var hand = HandRepo.Find(handId);

            if (hand != null)
            {
                var response = new Models.Messages.DealResponse()
                {
                    Deck = hand.Deck
                };

                return response;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private Models.Messages.JoinTableResponse JoinTable(Models.Messages.JoinTableRequest request)
        {
            Models.Messages.JoinTableResponse response = new Models.Messages.JoinTableResponse();
            var table = this.TableRepo.Find(request.TableId);

            if (table != null && table.Peers[request.Seat] == null)
            {
                //for (Int32 i = 0; i < table.MaxPlayers; i++)
                //{
                //    if (table.Peers[i] == null)
                //    {
                //        response.Seat = i;
                //        break;
                //    }
                //}

                response.Seat = request.Seat;

                return response;
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }
        }

        private Models.Messages.BuyInResponse BuyIn(Models.Messages.BuyInRequest request)
        {
            var table = this.TableRepo.Find(request.TableId);

            if (table != null)
            {
                NBitcoin.Transaction tx = new NBitcoin.Transaction();
                NBitcoin.TransactionBuilder builder = new NBitcoin.TransactionBuilder();
                Boolean isValid = true; // builder.Verify(tx);

                if (isValid)
                {
                    var utxos = tx.Outputs;
                    var sum = tx.Outputs.Sum(u => u.Value);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new ArgumentException("Table id not found");
            }

            return new Models.Messages.BuyInResponse();
        }
    }
}