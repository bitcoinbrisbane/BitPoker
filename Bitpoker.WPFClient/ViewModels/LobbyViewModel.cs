using NBitcoin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models.ExtensionMethods;
using System.ComponentModel;
using BitPoker.Models;
using Bitpoker.WPFClient.Clients;
using BitPoker.Models.Messages;

namespace Bitpoker.WPFClient.ViewModels
{
    /// <summary>
    /// View model for table really.
    /// </summary>
    public class LobbyViewModel : BaseViewModel, INotifyPropertyChanged
    {
        //SocketPermission permission;
        //Socket sListener;
        //IPEndPoint ipEndPoint;
        //Socket handler;
        //public Socket senderSock; 
        private readonly String _wifPrivateKey;
        private Key _bitcoinKey;
        private BitcoinSecret _secret;
        
        /// <summary>
        /// Not yet used
        /// </summary>
        public IList<BitPoker.NetworkClient.INetworkClient> Clients { get; set; }

        /// <summary>
        /// For single client
        /// </summary>
        public BitPoker.NetworkClient.INetworkClient NetworkClient { get; set; }

        [Obsolete]
        public IChatBackend Backend { get; private set; }

        /// <summary>
        /// Players on the entire network
        /// </summary>
        public ObservableCollection<PlayerInfo> NetworkPlayers { get; set; }

        public ObservableCollection<BitPoker.Models.Contracts.Table> Tables { get; set; }

        public BitPoker.Models.Contracts.Table SelectedTable
        {
            get;
            internal set;
        }

        public WalletViewModel Wallet { get; set; }

        private IPlayer _me;

        public IPlayer Me
        {
            get { return _me; }
            set
            {
                _me = value;
                NotifyPropertyChanged("Me");
            }
        }

        public ObservableCollection<IRequest> InComingRequests { get; private set; }

        public ObservableCollection<IResponse> InComingResponses { get; set; }

        public ObservableCollection<IRequest> SentRequests { get; set; }

        public ObservableCollection<IResponse> SentResponses { get; set; }

        public ObservableCollection<Models.Log> Logs { get; set; }

        private String _lastMessage;

        public String LastMessage
        {
            get { return _lastMessage; }
            set
            {
                _lastMessage = value;
                NotifyPropertyChanged("LastMessage");
            }
        }

        public LobbyViewModel()
        {
            this._wifPrivateKey = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";

            this.NetworkPlayers = new ObservableCollection<PlayerInfo>();
            this.Clients = new List<BitPoker.NetworkClient.INetworkClient>(1);
            
            this.Clients.Add(new BitPoker.NetworkClient.APIClient("https://www.bitpoker.io/api/"));
            //this.Clients.Add(new Clients.NetSocketClient(IPAddress.Parse("127.0.0.1")));

            this.Tables = new ObservableCollection<BitPoker.Models.Contracts.Table>();

            Wallet = new WalletViewModel(_wifPrivateKey);

            _secret = new BitcoinSecret(_wifPrivateKey, Constants.Network);
            BitcoinAddress address = _secret.GetAddress();

            //move this
            this.Me = new TexasHoldemPlayer()
            {
                Position = 0,
                BitcoinAddress = address.ToString(),
                IsDealer = true,
                IsBigBlind = true,
                Stack = 0
            };

            this.Backend = new ChatBackend(this.ProcessMessage, Wallet.Address.ToString());

            this.InComingRequests = new ObservableCollection<IRequest>();
            this.SentRequests = new ObservableCollection<IRequest>();

            this.InComingResponses = new ObservableCollection<IResponse>();
            this.SentResponses = new ObservableCollection<IResponse>();

            //Announce
            IRequest request = new RPCRequest()
            {
                Method = "NewPeer",
                Params = new NewPeer()
                {
                    BitcoinAddress = Wallet.Address.ToString(),
                    Version = 1.0M,
                    Player = new PlayerInfo()
                    {
                        BitcoinAddress = Wallet.Address.ToString()
                    },
                    TimeStamp = DateTime.UtcNow
                }
            };

            this.Backend.SendRequest(request);
            this.SentRequests.Add(request);

            this.Logs = new ObservableCollection<Models.Log>();
        }

        public String NewAddress()
        {
            PubKey pubKey = _bitcoinKey.PubKey;
            BitcoinAddress address = pubKey.GetAddress(Constants.Network);

            this.Wallet = new WalletViewModel(_bitcoinKey.GetWif(Constants.Network).ToString());

            return String.Format("SETNAME:{0}", address);
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind)
        {
            AddNewTable(smallBlind, bigBlind, 2, 10);
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind, Int16 minPlayers, Int16 maxPlayers)
        {
            AddNewTable(smallBlind, bigBlind, bigBlind * 20, bigBlind * 100, 2, 10);
        }

        public void AddNewTable(UInt64 smallBlind, UInt64 bigBlind, UInt64 minBuyIn, UInt64 maxBuyIn, Int16 minPlayers, Int16 maxPlayers)
        {
            BitPoker.Models.Contracts.Table table = new BitPoker.Models.Contracts.Table()
            {
                SmallBlind = smallBlind,
                BigBlind = bigBlind,
                MinBuyIn = minBuyIn,
                MaxBuyIn = maxBuyIn,
                MinPlayers = minPlayers,
                MaxPlayers = maxPlayers,
                NetworkAddress = "p2p"
            };

            if (!table.IsValid())
            {
                throw new AggregateException("Invalid table params");
            }

            //Check for duplicates
            using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
            {
                tableRepo.Add(table);
                tableRepo.Save();
            }
        }

        /// <summary>
        /// Get local tables.  TODO: add a filter
        /// </summary>
        public void GetTables()
        {
            
        }

        public void GetPeersTables(String address)
        {
            IRequest request = new RPCRequest();

            GetTableRequest tableRequest = new GetTableRequest()
            {
                Recipient = address 
            };

            request.Method = tableRequest.GetType().Name;
            request.Params = tableRequest;

            Backend.SendRequest(request);
            this.SentRequests.Add(request);
        }

        public void JoinTable(Guid tableId)
        {
            using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
            {
                IRequest request = new RPCRequest();
                var table = tableRepo.Find(tableId);

                JoinTableRequest joinTableRequest = new JoinTableRequest()
                {
                    Seat = 1
                };

                request.Method = joinTableRequest.GetType().Name;
                request.Params = joinTableRequest;

                //send
                String json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                Backend.SendRequest(request);
            }
        }

        public async Task GetPeers()
        {
            using (BitPoker.NetworkClient.IPlayerClient client = new BitPoker.NetworkClient.APIClient(""))
            {
                var players = await client.GetPlayersAsync();

                foreach (PlayerInfo player in players)
                {
                    this.NetworkPlayers.Add(player);
                }
            }
        }

        public async Task RefreshWalletBalance()
        {
            using (BitPoker.NetworkClient.Blockr client = new BitPoker.NetworkClient.Blockr())
            {
                String address = this.Wallet.Address.ToString();
                Decimal balance = await client.GetAddressBalanceAsync(address, 1);

                this.Me.Stack = Convert.ToUInt64(balance * Constants.SATOSHI);
            }
        }

        public async Task<TableViewModel> GetTableFromClientAsync(String id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Display messages and peform any actions
        /// </summary>
        /// <param name="composite"></param>
        public void ProcessMessage(CompositeType composite)
        {
            IRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<RPCRequest>(composite.Message);
            this.InComingRequests.Add(request);
            this.LastMessage = composite.Message;

            switch (request.Method)
            {
                case "NewPeer" :
                    NewPeer newPeer = Newtonsoft.Json.JsonConvert.DeserializeObject<NewPeer>(request.Params.ToString());
                    NetworkPlayers.Add(newPeer.Player);
                    break;
                case "NewTable": //Peer has announced a new table
                    break;

                case "GetTables": //Give me your tables

                    using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository("data.db"))
                    {
                        IEnumerable<BitPoker.Models.Contracts.Table> tables = tableRepo.All();

                        //now send
                        IResponse response = new RCPResponse()
                        {
                            Id = request.Id,
                            Result = tables
                        };

                        this.SentRequests.Add(request);
                        Backend.SendResponse(response);

                        break;
                    }

                case "GetTablesResponse": //Add resultant tables
                    GetTablesResponse tableResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTablesResponse>(request.Params.ToString());

                    foreach (BitPoker.Models.Contracts.Table table in tableResponse.Tables)
                    {
                        //TableViewModel tableViewModel = new TableViewModel(table);
                        this.Tables.Add(table);
                    }

                    break;
                case "ActionMessage":
                    ActionMessage actionMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionMessage>(request.Params.ToString());

                    //actionMessage.HandId;

                    break;
            }
        }
    }
}