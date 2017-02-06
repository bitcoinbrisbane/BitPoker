using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Host.Rest
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                String baseUrl = "http://localhost:8080";
                using (WebApp.Start<StartUp>(url: baseUrl))
                {
                    Console.WriteLine("Node started in own thread");
                    System.Threading.Thread.Sleep(-1);
                }
            });

            Console.ReadLine();
        }
    }
}
