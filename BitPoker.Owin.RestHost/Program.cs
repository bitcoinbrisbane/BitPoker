using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace BitPoker.Owin.RestHost
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string baseAddress = "http://localhost:5001/";

			// Start OWIN host 
			using (WebApp.Start<Startup>(url: baseAddress))
			{
				// Create HttpCient and make a request to api/values 
				HttpClient client = new HttpClient();

				var response = client.GetAsync(baseAddress + "api/logs").Result;

				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);
			}

			Console.ReadLine();
		}
	}
}
