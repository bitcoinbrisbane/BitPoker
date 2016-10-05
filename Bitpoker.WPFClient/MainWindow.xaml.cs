using Bitpoker.WPFClient.Clients;
using Bitpoker.WPFClient.Models;
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

namespace Bitpoker.WPFClient
{
    //Thanks http://www.codeproject.com/Articles/463947/Working-with-Sockets-in-Csharp


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clients.ChatBackend _backend;

        //SocketPermission permission;
        //Socket sListener;
        //IPEndPoint ipEndPoint;
        //Socket handler;

        public IList<Byte[]> Deck { get; set; }

        //internal for testing
        internal List<Byte[]> _keys;

        public Byte[] KeyHash { get; private set; }

        private Byte[] IV = new Byte[16];

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

            //Start();

            _backend = new ChatBackend(this.DisplayMessage, alice_address.ToString());
        }

        public void DisplayMessage(CompositeType composite)
        {
            string username = composite.Username == null ? "" : composite.Username;
            string message = composite.Message == null ? "" : composite.Message;
            textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);

            String value = composite.Message.ToUpper().Trim();
            if (value.StartsWith("GETTABLES"))
            {
                using (BitPoker.Repository.ITableRepository tableRepo = new BitPoker.Repository.LiteDB.TableRepository(@"poker.db"))
                {
                    var tables = tableRepo.All();
                    String json = Newtonsoft.Json.JsonConvert.SerializeObject(tables);
                    _backend.SendMessage(json);
                }
            }
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
                    //...
                }

                if (value.StartsWith("NEWADDRESS"))
                {
                    messageToSend = _viewModel.NewAddress();
                }

                if (value.StartsWith("GETTABLES"))
                {
                    messageToSend = "GETTABLES";
                }

                if (value.StartsWith("ADDTABLE"))
                {
                    String[] tableParams = value.Substring(0, 7).Split(' ');
                    _viewModel.AddNewTable(Convert.ToUInt64(tableParams[0]), Convert.ToUInt64(tableParams[1]), Convert.ToUInt64(tableParams[2]), Convert.ToUInt64(tableParams[3]), Convert.ToInt16(tableParams[4]), Convert.ToInt16(tableParams[5]));
                }

                if (value.StartsWith("JOINTABLE"))
                {
                    String[] buyInParms = value.Substring(0, 8).Split(' ');
                    //_viewModel.BuyIn();
                }
                else
                {
                    _backend.SendMessage(textBoxEntryField.Text);
                }

                if (!String.IsNullOrEmpty(messageToSend))
                {
                    _backend.SendMessage(messageToSend);
                    messageToSend = String.Empty;
                }

                textBoxEntryField.Clear();
            }
        }

        //private void Start()
        //{
        //    try
        //    {
        //        // Creates one SocketPermission object for access restrictions
        //        permission = new SocketPermission(
        //        NetworkAccess.Accept,     // Allowed to accept connections 
        //        TransportType.Tcp,        // Defines transport types 
        //        "",                       // The IP addresses of local host 
        //        SocketPermission.AllPorts // Specifies all ports 
        //        );

        //        // Listening Socket object 
        //        sListener = null;

        //        // Ensures the code to have permission to access a Socket 
        //        permission.Demand();

        //        // Resolves a host name to an IPHostEntry instance 
        //        IPHostEntry ipHost = Dns.GetHostEntry("");

        //        // Gets first IP address associated with a localhost 
        //        IPAddress ipAddr = ipHost.AddressList[0];

        //        // Creates a network endpoint 
        //        ipEndPoint = new IPEndPoint(ipAddr, 4510);

        //        // Create one Socket object to listen the incoming connection 
        //        sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //        // Associates a Socket with a local endpoint 
        //        sListener.Bind(ipEndPoint);

        //    }
        //    catch (Exception exc) 
        //    { 
        //        MessageBox.Show(exc.ToString()); 
        //    }
        //}

        //private void Listen_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        // Places a Socket in a listening state and specifies the maximum 
        //        // Length of the pending connections queue 
        //        sListener.Listen(10);

        //        // Begins an asynchronous operation to accept an attempt 
        //        AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
        //        sListener.BeginAccept(aCallback, sListener);

        //        //tbStatus.Text = "Server is now listening on " + ipEndPoint.Address + " port: " + ipEndPoint.Port;

        //        //StartListen_Button.IsEnabled = false;
        //        //Send_Button.IsEnabled = true;
        //    }
        //    catch (Exception exc) { MessageBox.Show(exc.ToString()); }
        //}

        

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Convert byte array to string 
                string message = "test message";

                // Prepare the reply message 
                //byte[] byteData = Encoding.Unicode.GetBytes(message);

                // Sends data asynchronously to a connected Socket 
                //handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);


                //IMessageClient client = new ChatBackend();

                //Send_Button.IsEnabled = false;
                //Close_Button.IsEnabled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        //public void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // A Socket which has sent the data to remote host 
        //        Socket handler = (Socket)ar.AsyncState;

        //        // The number of bytes sent to the Socket 
        //        int bytesSend = handler.EndSend(ar);
        //        Console.WriteLine("Sent {0} bytes to Client", bytesSend);
        //    }
        //    catch (Exception exc) { MessageBox.Show(exc.ToString()); }
        //}

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (sListener.Connected)
                //{
                //    sListener.Shutdown(SocketShutdown.Receive);
                //    sListener.Close();
                //}

                //Close_Button.IsEnabled = false;
            }
            catch (Exception exc) 
            { 
                MessageBox.Show(exc.ToString()); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.NewTable(2, 10);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    // Places a Socket in a listening state and specifies the maximum 
            //    // Length of the pending connections queue 
            //    sListener.Listen(10);

            //    // Begins an asynchronous operation to accept an attempt 
            //    AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
            //    sListener.BeginAccept(aCallback, sListener);

            //    //tbStatus.Text = "Server is now listening on " + ipEndPoint.Address + " port: " + ipEndPoint.Port;

            //    //StartListen_Button.IsEnabled = false;
            //    //Send_Button.IsEnabled = true;
            //}
            //catch (Exception exc)
            //{
            //    MessageBox.Show(exc.ToString());
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _viewModel.GetPlayers();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Guid selectedTableId = new Guid();

            //_viewModel.Players
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //if (_viewModel.Table != null)
            //{
            //    _viewModel.Table.Call(5000);
            //}
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get user pubkey

            //Load tables
        }
    }
}
