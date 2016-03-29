using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BitPoker
{
	public delegate void MessageReceivedEventHandler(object sender, EventArgs e);

	public class NetworkClient
	{
		// Invoke the Changed event; called whenever message arrives
		protected virtual void OnReceived(EventArgs e) 
		{
			if (Received != null)
				Received(this, e);
		}

		public event MessageReceivedEventHandler Received;

		public Int32 ListeningPort { get; set; }

		public static async Task<String> SendMessage(String ip, String message)
		{
			// Data buffer for incoming data.
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				// Establish the remote endpoint for the socket.
				// This example uses port 11000 on the local computer.
				//IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
				IPAddress ipAddress = IPAddress.Parse(ip);

				IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

				// Create a TCP/IP  socket.
				Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender.Connect(remoteEP);

					Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes(message);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();

					return "ok";

				} 
				catch (ArgumentNullException ane) 
				{
					return String.Format("ArgumentNullException : {0}", ane);
				} 
				catch (SocketException se) 
				{
					return String.Format("SocketException : {0}", se);
				} 
				catch (Exception e) 
				{
					return String.Format("Unexpected exception : {0}", e);
				}

			} 
			catch (Exception e) 
			{
				return e.ToString();
			}
		}

		public static async Task<String> SendMessage(String ip, String message, String signature)
		{
			return null;
		}


		public static string data = null;

		public void StartListening() 
		{
			// Data buffer for incoming data.
			byte[] bytes = new Byte[1024];

			// Establish the local endpoint for the socket.
			// Dns.GetHostName returns the name of the 
			// host running the application.
			IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];

			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, this.ListeningPort);

			// Create a TCP/IP socket.
			Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and 
			// listen for incoming connections.
			try 
			{
				listener.Bind(localEndPoint);
				listener.Listen(10);

				// Start listening for connections.
				while (true) 
				{
					Console.WriteLine("Waiting for a connection...");

					// Program is suspended while waiting for an incoming connection.
					Socket handler = listener.Accept();
					data = null;

					// An incoming connection needs to be processed.
					while (true) {
						bytes = new byte[1024];
						int bytesRec = handler.Receive(bytes);
						data += Encoding.ASCII.GetString(bytes,0,bytesRec);
						if (data.IndexOf("<EOF>") > -1) {
							break;
						}
					}

					// Show the data on the console.
					Console.WriteLine( "Text received : {0}", data);

					// Echo the data back to the client.
					byte[] msg = Encoding.ASCII.GetBytes(data);

					handler.Send(msg);
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();
				}
			} 
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}
	}
}