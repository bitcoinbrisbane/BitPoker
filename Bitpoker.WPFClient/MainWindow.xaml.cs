using Bitpoker.WPFClient.Clients;
using BitPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using BitPoker.Models.ExtensionMethods;
using BitPoker.Models.Messages;

namespace Bitpoker.WPFClient
{
    //Thanks http://www.codeproject.com/Articles/463947/Working-with-Sockets-in-Csharp


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //use client on view model
        [Obsolete]
        private ChatBackend _backend;

        private ViewModels.MainViewModel _viewModel = new ViewModels.MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            String alice_wif = "93Loqe8T3Qn3fCc87AiJHYHJfFFMLy6YuMpXzffyFsiodmAMCZS";
            //const String bob_wif = "91yMBYURGqd38spSA1ydY6UjqWiyD1SBGJDuqPPfRWcpG53T672";

            NBitcoin.BitcoinSecret alice_secret = new NBitcoin.BitcoinSecret(alice_wif, NBitcoin.Network.TestNet);
            //NBitcoin.BitcoinSecret bob_secret = new NBitcoin.BitcoinSecret(bob_wif, NBitcoin.Network.TestNet);

            NBitcoin.BitcoinAddress alice_address = alice_secret.GetAddress();
            //NBitcoin.BitcoinAddress bob_address = bob_secret.GetAddress();

            this.DataContext = _viewModel;

            //_backend = new ChatBackend(this.DisplayIMessage, alice_address.ToString());
            //_backend = new ChatBackend(this.DisplayMessage, alice_address.ToString());

            this.DataContext = _viewModel;
        }

        /// <summary>
        /// Display messages and peform any actions
        /// </summary>
        /// <param name="composite"></param>
        public void DisplayMessage(CompositeType composite)
        {
            string username = composite.Username == null ? "" : composite.Username;
            string message = composite.Message == null ? "" : composite.Message;
            textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);

            IRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<RPCRequest>(composite.Message);

            switch (request.Method.ToUpper())
            {
                case "GETTABLES" :
                
                    using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
                    {
                        var tables = tableRepo.All();
                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(tables);
                        _backend.SendMessage(json);
                    }

                    break;

                case "JOINTABLE" :

                    using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
                    {
                        var tables = tableRepo.All();
                        String json = Newtonsoft.Json.JsonConvert.SerializeObject(tables);
                        _backend.SendMessage(json);
                    }

                    break;
                case "BUYIN" :
                    break;
                case "CHECK":
                    request.Method = "check";
                    break;
            }
        }

        [Obsolete]
        public void DisplayIMessage(IRequest message)
        {
            textBoxChatPane.Text += (message.Method + Environment.NewLine);
        }

        private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                String messageToSend = String.Empty;

                String value = textBoxEntryField.Text.ToUpper().Trim();
                if (value.StartsWith("HELP"))
                {
                    textBoxChatPane.Text += "ADDTABLE SmallBlind BigBlind MinBuyIn MaxBuyIn MinPlayers MaxPlayers" + Environment.NewLine;
                    textBoxChatPane.Text = "GETTABLES";
                    textBoxChatPane.Text = "JOINTABLE";
                    //...
                }

                if (value.StartsWith("NEWADDRESS"))
                {
                    messageToSend = _viewModel.NewAddress();
                }

                if (value.StartsWith("GETTABLES"))
                {
                    RPCRequest request = new RPCRequest();
                    request.Method = "GETTABLES";
                    messageToSend = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                }

                if (value.StartsWith("GETTABLES"))
                {
                    RPCRequest request = new RPCRequest();
                    request.Method = "GETTABLES"; 
                    messageToSend = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                }

                if (value.StartsWith("ADDTABLE"))
                {
                    String[] tableParams = value.Substring(0, 7).Split(' ');
                    _viewModel.AddNewTable(Convert.ToUInt64(tableParams[0]), Convert.ToUInt64(tableParams[1]), Convert.ToUInt64(tableParams[2]), Convert.ToUInt64(tableParams[3]), Convert.ToInt16(tableParams[4]), Convert.ToInt16(tableParams[5]));
                }

                if (value.StartsWith("JOINTABLE"))
                {
                    String[] joinParams = value.Substring(0, 9).Split(' ');
                    Guid tableId = new Guid(joinParams[1]);
                    _viewModel.JoinTable(tableId);
                    //_viewModel.BuyIn();
                }
                //else
                //{
                //    _backend.SendMessage(textBoxEntryField.Text);
                //}

                if (value.StartsWith("BUYIN"))
                {
                    String[] buyInParms = value.Substring(0, 8).Split(' ');
                    //_viewModel.BuyIn();
                }
                //else
                //{
                //    _backend.SendMessage(textBoxEntryField.Text);
                //}

                if (!String.IsNullOrEmpty(messageToSend))
                {
                    _backend.SendMessage(messageToSend);
                    messageToSend = String.Empty;
                }

                textBoxEntryField.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.NewTable(2, 10);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //_viewModel.GetPlayers();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Guid selectedTableId = new Guid();

            //_viewModel.Players
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get user pubkey
            //Load tables
        }
    }
}
