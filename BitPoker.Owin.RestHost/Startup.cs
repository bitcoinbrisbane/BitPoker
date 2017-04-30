using System;
using System.Web.Http;
using Owin;

namespace BitPoker.Owin.RestHost
{
	public class Startup
	{
		// This code configures Web API. The Startup class is specified as a type
		// parameter in the WebApp.Start method.
		public void Configuration(IAppBuilder appBuilder)
		{
			// Configure Web API for self-host. 
			HttpConfiguration config = new HttpConfiguration();

			config.EnableCors();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			//config.Routes.MapHttpRoute( name: "ActionApi", routeTemplate: "api/{controller}/{action}/{id}", defaults: new { action = "GET", id = RouteParameter.Optional } );

			appBuilder.UseWebApi(config);
		}
	}
}
