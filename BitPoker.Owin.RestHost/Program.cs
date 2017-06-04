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
            if (args.Length == 0)
            {
                args = new string[1];
                
                //Spin up multiple hosts for testing.
                args[0] = System.Configuration.ConfigurationManager.AppSettings["baseurl"];
            }

            //foreach (String baseAddress in args)
            //{
            //    //Seems to fail on Mac
            //    Task.Run(() =>
            //    {
            //        //Start OWIN host
            //        using (WebApp.Start<Startup>(url: baseAddress))
            //        {
            //            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseAddress);
            //        }
            //    });
            //}


            if (!String.IsNullOrEmpty(args[0]))
            {
                // Start OWIN host 
                using (WebApp.Start<Startup>(url: args[0]))
                {
                    Console.WriteLine("Server running at {0} - press Enter to quit. ", args[0]);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("No base url config setting found");

            }

            Console.ReadLine();
        }
	}
}