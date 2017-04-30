using System;
using Microsoft.Owin.Hosting;

namespace BitPoker.Owin.RestHost
{
	/// <summary>
	/// Not yet used
	/// </summary>
	public class OwinService : IDisposable
	{
		private IDisposable _webApp;

		public String Host { get; set; }

		public OwinService()
		{
			this.Host = "http://localhost:5000/";
		}

		public void Start()
		{
			_webApp = WebApp.Start<Startup>(Host);
		}

		public void Stop()
		{
			_webApp.Dispose();
		}

		public void Dispose()
		{
			_webApp.Dispose();
		}
	}
}
