using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitPoker.Owin.RestHost
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			//Task.Run(() =>
			//{
			//	string baseAddress = "http://localhost:5000/";

			//	// Start OWIN host 
			//	using (WebApp.Start<Startup>(url: baseAddress))
			//	{
			//		Console.WriteLine("Server running at {0} - press Enter to quit. ", baseAddress);
			//	}
			//});

			String baseAddress = System.Configuration.ConfigurationManager.AppSettings["baseurl"];

			if (!String.IsNullOrEmpty(baseAddress))
			{
				// Start OWIN host 
				using (WebApp.Start<Startup>(url: baseAddress))
				{
					Console.WriteLine("Server running at {0} - press Enter to quit. ", baseAddress);
					Console.ReadLine();
				}
			}
			else
			{
				Console.WriteLine("No base url config setting found");
			}

			//Console.ReadLine();
		}
	}
}