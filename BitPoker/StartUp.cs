using Owin;
using System.Web.Http;

namespace BitPoker
{
    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.

            //config.Routes.MapHttpRoute(name: "Default", url: "{controller}/{action}/{id}",
            //    namespaces: new[] { "[Namespace of the Project that contains your controllers]" },
            //    defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional });

            //config.Routes.MapRoute(
            //    "External",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = "" },
            //    new[] { "BitPoker.Controllers" }
            //);

            appBuilder.UseWebApi(config);
        }
    }
}
