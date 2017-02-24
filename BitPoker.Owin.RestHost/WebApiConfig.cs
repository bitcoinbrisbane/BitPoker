using System;
using System.Web.Http;
using Owin;

namespace BitPoker.Owin.RestHost
{
	[Obsolete]
	public class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

        }

		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration httpConfiguration = new HttpConfiguration();
			WebApiConfig.Register(httpConfiguration);
			appBuilder.UseWebApi(httpConfiguration);
		}
	}
}