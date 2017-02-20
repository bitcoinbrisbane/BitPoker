using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Threading;

namespace BitPoker.Owin.RestHost
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string baseAddress = "http://localhost:5000/";

			// Start OWIN host 
			using (WebApp.Start<Startup>(url: baseAddress))
			{
				Console.WriteLine("Server running at {0} - press Enter to quit. ", baseAddress);
				Console.ReadLine();
			}
		}
	}
}