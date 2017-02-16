using System;
using System.Web.Http;
using Owin;

namespace BitPoker.Owin.RestHost
{
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "AllTests",
				routeTemplate: "Tests",
				defaults: new { controller = "Tests", action = "Get" });
		}

		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration httpConfiguration = new HttpConfiguration();
			WebApiConfig.Register(httpConfiguration);
			appBuilder.UseWebApi(httpConfiguration);
		}
	}
}