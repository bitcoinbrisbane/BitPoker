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

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            //config.Routes.MapHttpRoute(
            //        name: "x",

            //        routeTemplate: "api/{controller}/{id}",
            //        defaults: new { id = RouteParameter.Optional });
        }

		public void Configuration(IAppBuilder appBuilder)
		{
			HttpConfiguration httpConfiguration = new HttpConfiguration();
			WebApiConfig.Register(httpConfiguration);
			appBuilder.UseWebApi(httpConfiguration);
		}
	}
}